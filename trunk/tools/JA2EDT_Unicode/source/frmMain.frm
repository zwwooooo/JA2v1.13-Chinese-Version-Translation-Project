VERSION 5.00
Begin VB.Form frmMain 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "JA2EDT Tool by TBS Neohdy"
   ClientHeight    =   8520
   ClientLeft      =   45
   ClientTop       =   390
   ClientWidth     =   10095
   Icon            =   "frmMain.frx":0000
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   568
   ScaleMode       =   3  'Pixel
   ScaleWidth      =   673
   StartUpPosition =   1  '所有者中心
   Begin VB.PictureBox picPIG 
      AutoRedraw      =   -1  'True
      BorderStyle     =   0  'None
      Height          =   480
      Left            =   4800
      Picture         =   "frmMain.frx":08CA
      ScaleHeight     =   32
      ScaleMode       =   3  'Pixel
      ScaleWidth      =   32
      TabIndex        =   17
      Top             =   7560
      Visible         =   0   'False
      Width           =   480
   End
   Begin VB.PictureBox pic 
      Appearance      =   0  'Flat
      AutoRedraw      =   -1  'True
      BackColor       =   &H80000005&
      BorderStyle     =   0  'None
      ForeColor       =   &H80000008&
      Height          =   480
      Left            =   4800
      ScaleHeight     =   32
      ScaleMode       =   3  'Pixel
      ScaleWidth      =   32
      TabIndex        =   16
      Top             =   1080
      Width           =   480
   End
   Begin VB.Frame Frame1 
      Caption         =   "Txt to Edt"
      ForeColor       =   &H00FF0000&
      Height          =   8295
      Left            =   120
      TabIndex        =   0
      Top             =   120
      Width           =   9855
      Begin VB.OptionButton Option1 
         Height          =   495
         Index           =   1
         Left            =   4560
         Picture         =   "frmMain.frx":1194
         Style           =   1  'Graphical
         TabIndex        =   19
         Top             =   5640
         Width           =   735
      End
      Begin VB.OptionButton Option1 
         Height          =   495
         Index           =   0
         Left            =   4560
         Picture         =   "frmMain.frx":15D6
         Style           =   1  'Graphical
         TabIndex        =   18
         Top             =   4800
         Value           =   -1  'True
         Width           =   735
      End
      Begin VB.TextBox txtSecond 
         Alignment       =   2  'Center
         Height          =   375
         Left            =   1920
         TabIndex        =   15
         Top             =   7560
         Width           =   1455
      End
      Begin VB.CheckBox chkUpdate 
         Caption         =   "Update file ( Only TXT to EDT )"
         Height          =   255
         Left            =   6240
         TabIndex        =   13
         Top             =   6840
         Value           =   1  'Checked
         Width           =   3255
      End
      Begin VB.DirListBox DirEdt 
         Height          =   3240
         Left            =   5400
         TabIndex        =   12
         Top             =   720
         Width           =   4215
      End
      Begin VB.DirListBox DirTxt 
         Height          =   3240
         Left            =   240
         TabIndex        =   11
         Top             =   720
         Width           =   4215
      End
      Begin VB.TextBox txtBlank 
         Alignment       =   2  'Center
         Height          =   375
         Left            =   1920
         TabIndex        =   9
         Text            =   "480"
         Top             =   6960
         Width           =   1455
      End
      Begin VB.CommandButton cmdReflesh 
         Caption         =   "Reflesh List"
         Height          =   855
         Left            =   4560
         Picture         =   "frmMain.frx":1A18
         Style           =   1  'Graphical
         TabIndex        =   8
         Top             =   2160
         Width           =   735
      End
      Begin VB.CommandButton cmdGo 
         Caption         =   "Let's DO it!"
         Height          =   855
         Left            =   6120
         Picture         =   "frmMain.frx":1D22
         Style           =   1  'Graphical
         TabIndex        =   7
         Top             =   7200
         Width           =   3255
      End
      Begin VB.CommandButton cmdTxtPath 
         Caption         =   "Change Path"
         Height          =   375
         Left            =   2040
         TabIndex        =   6
         Top             =   240
         Width           =   1335
      End
      Begin VB.CommandButton cmdEdtPath 
         Caption         =   "Change Path"
         Height          =   375
         Left            =   7200
         TabIndex        =   5
         Top             =   240
         Width           =   1335
      End
      Begin VB.FileListBox fileEDT 
         Height          =   2430
         Left            =   5400
         Pattern         =   "*.edt"
         TabIndex        =   3
         Top             =   4200
         Width           =   4215
      End
      Begin VB.FileListBox fileTXT 
         Height          =   2430
         Left            =   240
         Pattern         =   "*.txt"
         TabIndex        =   1
         Top             =   4200
         Width           =   4215
      End
      Begin VB.Label Label3 
         Caption         =   "The Second Begin:"
         Height          =   255
         Left            =   360
         TabIndex        =   14
         Top             =   7680
         Width           =   1695
      End
      Begin VB.Label Label2 
         Caption         =   "Value of Range:"
         Height          =   255
         Left            =   360
         TabIndex        =   10
         Top             =   7080
         Width           =   1575
      End
      Begin VB.Label Label1 
         Caption         =   "Edt Files (*.edt):"
         Height          =   255
         Index           =   1
         Left            =   5400
         TabIndex        =   4
         Top             =   360
         Width           =   2175
      End
      Begin VB.Label Label1 
         Caption         =   "Txt Files (*.txt):"
         Height          =   255
         Index           =   0
         Left            =   240
         TabIndex        =   2
         Top             =   360
         Width           =   2175
      End
   End
   Begin VB.Menu mnuDelTxt 
      Caption         =   "DeleteTXT"
      Visible         =   0   'False
      Begin VB.Menu mnuDelTxtFile 
         Caption         =   "Delete the TXT File"
      End
   End
   Begin VB.Menu mnuDelEdt 
      Caption         =   "DeleteEDT"
      Visible         =   0   'False
      Begin VB.Menu mnuDelEdtFile 
         Caption         =   "Delete the EDT File"
      End
   End
End
Attribute VB_Name = "frmMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private Declare Sub CopyMemory Lib "kernel32" Alias "RtlMoveMemory" (ByVal Destination As Long, ByVal Source As Long, ByVal Length As Long)

Private Type TypeStr
    pos As Long
    str As String
End Type

Private Sub cmdEdtPath_Click()
'选取Edt文件夹
    
    Dim s As String
    s = BrowseForFolderByPath(fileEDT.Path, "Browse for EDT file Folder:")
    If s <> "" Then
        DirEdt.Path = s
        fileEDT.Path = s
        If Option1(0).Value = True Then '为了更新左上角的数量
            Call Option1_Click(0)
        Else
            Call Option1_Click(1)
        End If
    End If

End Sub

Private Sub cmdGo_Click()
    Dim ret As Boolean
    Dim i As Integer
    Dim cnt As Integer
    
    If Option1(0).Value = True Then
    'TXT2EDT
        For i = 0 To fileTXT.ListCount - 1
            ret = Txt2Edt(fileTXT.Path & "\" & fileTXT.List(i), fileEDT.Path & "\" & Left(fileTXT.List(i), Len(fileTXT.List(i)) - 4) & ".edt", txtBlank)
            If ret = True Then cnt = cnt + 1
        Next
        If cnt < fileTXT.ListCount Then
            MsgBox cnt & "Some files Failed. Please Check.", vbExclamation, "Exclamation"
        ElseIf cnt > 0 Then
            MsgBox cnt & "  TXT files changed to EDT files.", vbInformation, "Successfully"
        End If
    Else
    'EDT2TXT
        For i = 0 To fileEDT.ListCount - 1
            ret = Edt2Txt(fileEDT.Path & "\" & fileEDT.List(i), fileTXT.Path & "\" & Left(fileEDT.List(i), Len(fileEDT.List(i)) - 4) & ".txt", txtBlank)
            If ret = True Then cnt = cnt + 1
        Next
        If cnt < fileEDT.ListCount Then
            MsgBox cnt & "Some files Failed. Please Check.", vbExclamation, "Exclamation"
        ElseIf cnt > 0 Then
            MsgBox cnt & "  EDT files changed to TXT files.", vbInformation, "Successfully"
        End If
    End If
    
    Call cmdReflesh_Click
        
End Sub


Private Function Txt2Edt(TxtFileName As String, EdtFileName As String, iBlank As Integer) As Boolean

On Error GoTo Error

    Dim str As String
    Dim iStrLng As Integer
    Dim b() As Byte
    Dim Fin As Integer
    Dim Fout As Integer
    Dim i As Long
    Dim j As Long
    Dim lMaxPos As Long
    Dim atStr() As TypeStr
    
    
    '读取文本从TXT
    Fin = FreeFile
    Open TxtFileName For Input As #Fin
    Do Until EOF(Fin)
        Line Input #Fin, str
        str = Trim(str)
        If Right(str, 1) = ":" Then
        '文字的位置定义行
            i = i + 1
            ReDim Preserve atStr(i)
            atStr(i).pos = CLng(Left(str, Len(str) - 1))
            If lMaxPos < atStr(i).pos Then lMaxPos = atStr(i).pos '存储最大的文字位置定义
        ElseIf str <> "" Then
        '要转换的文字行
            atStr(i).str = str
        Else
        '空行,什么也不做
        End If
    Loop
    Close #Fin
    
    
    '写入文本到EDT
    Fout = FreeFile
    If chkUpdate.Value = 0 Then
    '覆盖模式
        If Dir(EdtFileName) <> "" Then Kill EdtFileName
        Open EdtFileName For Binary As #Fout
        ReDim b(lMaxPos + iBlank - 1) '生成空的文件
        Put #Fout, , b
    Else
    '更新模式
        Open EdtFileName For Binary As #Fout
    End If
    
    For i = 1 To UBound(atStr)
        If atStr(i).str <> "" Then
            iStrLng = Len(atStr(i).str) * 2 + 2 '文字使用的字节数（包括结尾的 00 00）
            '输出文字
            ReDim b(iStrLng - 1)
            CopyMemory ByVal VarPtr(b(0)), ByVal StrPtr(atStr(i).str), ByVal iStrLng '生成UTF-16
            
            '变换JA2所需要的格式
            For j = 0 To UBound(b) - 1 Step 2
                If (b(j) = 0 And b(j + 1) = 0) Or (b(j) = 32 And b(j + 1) = 0) Then
                '不处理00与空格
                ElseIf b(j) = 33 And b(j + 1) = 50 Then '处理㈠
                    b(j) = 179
                    b(j + 1) = 0
                ElseIf b(j) = 34 And b(j + 1) = 50 Then '处理㈡
                    b(j) = 180
                    b(j + 1) = 0
                ElseIf b(j) = 255 Then '处理进位+1
                    b(j) = 0
                    b(j + 1) = b(j + 1) + 1
                Else '处理+1
                    b(j) = b(j) + 1
                End If
            Next j
            Put #Fout, atStr(i).pos + 1, b '在指定位置输出
        End If
    Next i
    
    Close #Fout


    '成功退出
    Txt2Edt = True
    Exit Function
    
Error:
    '出错退出
    MsgBox Err.Number & vbCrLf & Err.Description
    Txt2Edt = False
    
End Function

Private Function Edt2Txt(EdtFileName As String, TxtFileName As String, iBlank As Integer) As Boolean

On Error GoTo Error

    Dim str As String
    Dim iStrLng As Integer
    Dim b() As Byte
    Dim Fin As Integer
    Dim Fout As Integer
    Dim i As Long
    
    Fin = FreeFile
    Open EdtFileName For Binary As #Fin
    
    If Dir("c:\Tmp.edt") <> "" Then Kill "c:\Tmp.edt"
    Fout = FreeFile
    Open "c:\Tmp.edt" For Binary As #Fout
    
    ReDim b(1)
    For i = 1 To FileLen(EdtFileName) / 2
        Get #Fin, , b
        If (b(0) = 0 And b(1) = 0) Or (b(0) = 32 And b(1) = 0) Then
        '不处理00与空格
        ElseIf b(0) = 179 And b(1) = 0 Then '处理㈠
            b(0) = 33
            b(1) = 50
        ElseIf b(0) = 180 And b(1) = 0 Then '处理㈡
            b(0) = 34
            b(1) = 50
        ElseIf b(0) <> 0 Then '处理-1
            b(0) = b(0) - 1
        Else '处理借位-1
            b(0) = 255
            b(1) = b(1) - 1
        End If
        Put #Fout, , b
    Next i
    
    Close #Fout
    Close #Fin
    
    
    Fin = FreeFile
    Open "c:\Tmp.edt" For Binary As #Fin
    
    If Dir(TxtFileName) <> "" Then Kill TxtFileName
    Fout = FreeFile
    Open TxtFileName For Output As #Fout
    
    For i = 0 To FileLen(EdtFileName) / iBlank - 1
        ReDim b(iBlank - 1)
        Get #Fin, , b
        For iStrLng = 0 To iBlank - 1 Step 2
        '在iBlank个字节内，按每两个进行处理，直到遇见00 00
            If b(iStrLng) = 0 And b(iStrLng + 1) = 0 Then
                Exit For
            End If
        Next iStrLng
        'iStrLng为有效字符串的字节长度
        If iStrLng <> 0 Then
            ReDim Preserve b(iStrLng - 1)
            str = CStr(b)
        Else
            str = ""
        End If
        If str = "" Then 'zwwooooo: 对话为空输出"zwwooooo"字段，方便整理
            'Print #Fout, "zwwooooo"
            'Print #Fout, "zwwooooo"
            'Print #Fout, "zwwooooo"
        Else
            Print #Fout, CStr(i * iBlank) & ":"
            Print #Fout, str
            Print #Fout, ""
        End If
    Next i

    If txtSecond <> "" And txtSecond <> "0" Then
        '跳过前部分
        ReDim b(CInt(txtSecond) - 1)
        Get #Fin, 1, b
        For i = 0 To (FileLen(EdtFileName) - CInt(txtSecond)) / iBlank - 1
            ReDim b(iBlank - 1)
            Get #Fin, , b
            For iStrLng = 0 To iBlank - 1 Step 2
            '在iBlank个字节内，按每两个进行处理，直到遇见00 00
                If b(iStrLng) = 0 And b(iStrLng + 1) = 0 Then
                    Exit For
                End If
            Next iStrLng
            'iStrLng为有效字符串的字节长度
            If iStrLng <> 0 Then
                ReDim Preserve b(iStrLng - 1)
                str = CStr(b)
                '只输出不为空的字符串
                If str = "" Then 'zwwooooo: 对话为空输出"zwwooooo"字段，方便整理
                    Print #Fout, "zwwooooo"
                    Print #Fout, "zwwooooo"
                    Print #Fout, "zwwooooo"
                Else
                    Print #Fout, CStr(i * iBlank + CInt(txtSecond)) & ":"
                    Print #Fout, str
                    Print #Fout, ""
                End If
            End If
        Next i
    End If

    Close #Fout
    Close #Fin
    
       
    Edt2Txt = True
    
    Exit Function
    
Error:
    MsgBox Err.Number & vbCrLf & Err.Description

    Edt2Txt = False
    
End Function

Private Sub cmdReflesh_Click()
'刷新文件列表
    fileTXT.Refresh
    fileEDT.Refresh
    
End Sub

Private Sub cmdTxtPath_Click()
'选取Txt文件夹
    Dim s As String
    s = BrowseForFolderByPath(fileTXT.Path, "Browse for TXT file Folder:")

    If s <> "" Then
        DirTxt.Path = s
        fileTXT.Path = s
        If Option1(0).Value = True Then '为了更新左上角的数量
            Call Option1_Click(0)
        Else
            Call Option1_Click(1)
        End If
    End If
    
End Sub

Private Sub DirEdt_Change()
    fileEDT.Path = DirEdt.Path
    If Option1(0).Value = True Then
        Call Option1_Click(0)
    Else
        Call Option1_Click(1)
    End If

End Sub

Private Sub DirTxt_Change()
    fileTXT.Path = DirTxt.Path
    If Option1(0).Value = True Then
        Call Option1_Click(0)
    Else
        Call Option1_Click(1)
    End If
    
End Sub

Private Sub fileEDT_DblClick()
    Dim ret As Boolean
    Dim s As String
    
    s = fileEDT.List(fileEDT.ListIndex)
    ret = Edt2Txt(fileEDT.Path & "\" & s, fileTXT.Path & "\" & Left(s, Len(s) - 4) & ".txt", txtBlank)
    If ret = True Then MsgBox """" & s & """ has been changed to TXT files.", vbInformation, "Successfully"
    Call cmdReflesh_Click
    Option1(1).Value = True
    
End Sub

Private Sub fileEDT_MouseUp(Button As Integer, Shift As Integer, X As Single, Y As Single)
    If Button = vbRightButton Then
        If fileEDT.ListCount <> 0 Then
            Me.PopupMenu mnuDelEdt
        End If
    End If

End Sub

Private Sub fileTXT_DblClick()
    Dim ret As Boolean
    Dim s As String
    
    s = fileTXT.List(fileTXT.ListIndex)
    ret = Txt2Edt(fileTXT.Path & "\" & s, fileEDT.Path & "\" & Left(s, Len(s) - 4) & ".edt", txtBlank)
    If ret = True Then MsgBox """" & s & """ has been changed to EDT files.", vbInformation, "Successfully"
    Call cmdReflesh_Click
    Option1(0).Value = True
    
End Sub

Private Sub fileTXT_MouseUp(Button As Integer, Shift As Integer, X As Single, Y As Single)
    If Button = vbRightButton Then
        If fileTXT.ListCount <> 0 Then
            Me.PopupMenu mnuDelTxt
        End If
    End If


End Sub

Private Sub Form_Load()
    DirTxt.Path = App.Path
    DirEdt.Path = App.Path
    DirTxt.ToolTipText = App.Path
    DirEdt.ToolTipText = App.Path
    Me.Caption = Me.Caption & " v" & App.Major & "." & App.Minor & "." & App.Revision
    Call Option1_Click(0)
    
End Sub

Private Sub mnuDelEdtFile_Click()
    If Dir(fileEDT.Path & "\" & fileEDT.List(fileEDT.ListIndex)) <> "" Then
        Kill fileEDT.Path & "\" & fileEDT.List(fileEDT.ListIndex)
        Call cmdReflesh_Click
    End If
    
End Sub

Private Sub mnuDelTxtFile_Click()
    If Dir(fileTXT.Path & "\" & fileTXT.List(fileTXT.ListIndex)) <> "" Then
        Kill fileTXT.Path & "\" & fileTXT.List(fileTXT.ListIndex)
        Call cmdReflesh_Click
    End If

End Sub

Private Sub Option1_Click(Index As Integer)
    If Index = 0 Then
        Frame1.Caption = "Txt to Edt ( " & fileTXT.ListCount & " Files)"

    Else
        Frame1.Caption = "Edt to Txt ( " & fileEDT.ListCount & " Files)"

    End If
    Option1(Index).BackColor = vbRed
    Option1((Index + 1) Mod 2).BackColor = &H8000000F
    Call FUN
    
End Sub

Private Sub txtBlank_LostFocus()
    If IsNumeric(txtBlank) = False Then
        txtBlank.SetFocus
    End If
    
End Sub

Public Function BrowseForFolderByPath(sSelPath As String, sTitle As String) As String

    Dim BI As BROWSEINFO
    Dim pidl As Long
    Dim lpSelPath As Long
    Dim sPath As String * MAX_PATH
    
    With BI
        .hOwner = Me.hWnd
        .pidlRoot = 0
        .lpszTitle = sTitle
        .lpfn = FARPROC(AddressOf BrowseCallbackProcStr)
        
        lpSelPath = LocalAlloc(LPTR, Len(sSelPath))
        MoveMemory ByVal lpSelPath, ByVal sSelPath, Len(sSelPath)
        .lParam = lpSelPath
    
    End With
    
    pidl = SHBrowseForFolder(BI)
    
    If pidl Then
        If SHGetPathFromIDList(pidl, sPath) Then
            BrowseForFolderByPath = Left$(sPath, InStr(sPath, vbNullChar) - 1)
        End If
        Call CoTaskMemFree(pidl)
    End If
    
    Call LocalFree(lpSelPath)
    
End Function

Private Sub FUN()
    Dim i As Integer
    Dim j As Integer
    
    If Option1(0).Value = True Then
        For i = 0 To 31
            For j = 0 To 31
                pic.PSet (i, j), picPIG.Point(31 - i, j)
            Next j
        Next i
    Else
        For i = 0 To 31
            For j = 0 To 31
                pic.PSet (i, j), picPIG.Point(i, j)
            Next j
        Next i
    
    End If
    

End Sub
