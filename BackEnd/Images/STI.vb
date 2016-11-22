Imports System
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Text
Imports System.IO
Imports System.Runtime.InteropServices

Friend Class STI
    Implements IDisposable

    Private disposedValue As Boolean = False        ' To detect redundant calls
    Private stream As Stream
    Private br As BinaryReader
    Private header As StiHeader
    Private palette As Byte()
    Private frame As Image()
    Private frameHeader As StiSubImageHeader()

    Structure StiHeader
        Public Const TotalHeaderSize As Integer = 64
        Public Const BytesPerPixel As Integer = 3
        Public Const IdLength As Integer = 4
        Public originalSize As UInt32
        Public storedSize As UInt32
        Public transparentValue As UInt32
        Public flags As UInt32
        Public height As UInt16
        Public width As UInt16
        Public numberOfColors As UInt32
        Public numberOfSubImages As UInt16
        Public redDepth As Byte
        Public greenDepth As Byte
        Public blueDepth As Byte
        Public depth As Byte
        Public appDataSize As UInt32
    End Structure

    Structure StiSubImageHeader
        Public Const TotalSubImageHeaderSize As Integer = 16
        Public dataOffset As UInt32
        Public dataLength As UInt32
        Public offsetX As Int16
        Public offsetY As Int16
        Public height As UInt16
        Public width As UInt16
    End Structure

    'Opens an STI file
    Public Sub New(ByVal fileName As String)
        Me.New(New FileStream(fileName, FileMode.Open, FileAccess.Read))
    End Sub

    ' Opens an STI image from a stream.
    Public Sub New(ByVal stream As Stream)
        Me.stream = stream
        br = New BinaryReader(stream, Encoding.Default)
        ParseHeader()
    End Sub


    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free managed resources when explicitly called
                If stream IsNot Nothing Then stream.Dispose()

                For Each i As Image In frame
                    If i IsNot Nothing Then
                        i.Dispose()
                    End If
                Next
            End If

            ' TODO: free shared unmanaged resources
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

    Private Sub ParseHeader()
        stream.Position = 0

        Dim enc As Encoding = Encoding.ASCII
        Dim idstring As String = enc.GetString(br.ReadBytes(StiHeader.IdLength))

        If idstring <> "STCI" Then Throw New Exception("Image format differs from STI format.")

        header.originalSize = br.ReadUInt32()
        header.storedSize = br.ReadUInt32()
        header.transparentValue = br.ReadUInt32()
        header.flags = br.ReadUInt32()
        header.height = br.ReadUInt16()
        header.width = br.ReadUInt16()

        header.numberOfColors = br.ReadUInt32()
        header.numberOfSubImages = br.ReadUInt16()
        header.redDepth = br.ReadByte()
        header.greenDepth = br.ReadByte()
        header.blueDepth = br.ReadByte()

        header.depth = br.ReadByte()
        header.appDataSize = br.ReadUInt32()

        ' skip padding bytes
        br.BaseStream.Position = 64

        ' load palette
        palette = br.ReadBytes(CInt(header.numberOfColors) * StiHeader.BytesPerPixel)

        ReDim frame(header.numberOfSubImages)
        ReDim frameHeader(header.numberOfSubImages)

        For i As Integer = 0 To header.numberOfSubImages - 1
            frameHeader(i).dataOffset = br.ReadUInt32()
            frameHeader(i).dataLength = br.ReadUInt32()
            frameHeader(i).offsetX = br.ReadInt16()
            frameHeader(i).offsetY = br.ReadInt16()
            frameHeader(i).height = br.ReadUInt16()
            frameHeader(i).width = br.ReadUInt16()
        Next

    End Sub

    ' Provides access to single frames within the STI image.
    Default Public ReadOnly Property STIImage(ByVal index As Integer) As Image
        Get
            Return LoadFrame(index)
        End Get
    End Property

    ' Loads and decodes a single frame of the STI image.
    Public Function LoadFrame(ByVal index As Integer) As Image

        If frame(index) Is Nothing Then
            'load the data first
            DecodeData(index)
        End If
        Return frame(index)
    End Function

    ' Loads and decodes all frames within the STI image.
    Public Sub Load()
        ' load all subframes
        For i As Integer = 0 To header.numberOfSubImages - 1
            LoadFrame(i)
        Next
    End Sub

    ' Gets the number of frames within the STI image.
    Public ReadOnly Property NumberOfSubImages() As Integer
        Get
            Return header.numberOfSubImages
        End Get
    End Property

    Private Sub DecodeData(ByVal index As Integer)
        Dim decodedData As New MemoryStream(frameHeader(index).width * frameHeader(index).height * 4)    ' 32 bbp
        Using decodedData
            Dim decodedWriter As New BinaryWriter(decodedData)
            Dim encodedReader As BinaryReader = br

            ' make sure stream is at correct position
            stream.Position = StiHeader.TotalHeaderSize _
                    + (header.numberOfColors * StiHeader.BytesPerPixel) _
                    + (header.numberOfSubImages * StiSubImageHeader.TotalSubImageHeaderSize) _
                    + frameHeader(index).dataOffset

            ' The following algorithm has been developed on basis of
            ' information from the LOTB forums. This is not identical
            ' to the solution in the original JA2 sourcecode.

            Dim byteCount As Integer = 0

            While byteCount < frameHeader(index).dataLength
                Dim currentByte As Byte = encodedReader.ReadByte()
                byteCount += 1
                ' we always start with a control byte
                If (currentByte And &H80) > 0 Then
                    ' we have some transparent pixels
                    For i As Integer = 1 To currentByte And &H7F
                        decodedWriter.Write(palette(0 + 2))
                        decodedWriter.Write(palette(0 + 1))
                        decodedWriter.Write(palette(0))
                        decodedWriter.Write(CByte(255))
                    Next
                Else
                    For i As Integer = 1 To currentByte
                        ' we have some non-transparent pixels
                        Dim colorIndex As Byte = encodedReader.ReadByte()

                        decodedWriter.Write(palette(colorIndex * 3 + 2))
                        decodedWriter.Write(palette(colorIndex * 3 + 1))
                        decodedWriter.Write(palette(colorIndex * 3))
                        decodedWriter.Write(CByte(255))

                        byteCount += 1
                    Next

                End If

            End While

            ' this is not the fastest way, but perfectly safe                
            Dim bmp As Bitmap = New Bitmap(frameHeader(index).width, frameHeader(index).height, PixelFormat.Format32bppArgb)
            Dim bmpData As BitmapData = bmp.LockBits(New Rectangle(0, 0, frameHeader(index).width, frameHeader(index).height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb)

            Marshal.Copy(decodedData.ToArray(), 0, bmpData.Scan0, CInt(decodedData.Length))

            bmp.UnlockBits(bmpData)

            frame(index) = bmp
        End Using

    End Sub

End Class
