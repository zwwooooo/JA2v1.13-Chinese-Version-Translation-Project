
============================================================================

新版 JA2 1.13 说明（源码SVN4452+）

============================================================================


=== Changelog ===

修改日志具体看 svn 更新日志，这里记录的是比较重大的bug修正。

- 2011.11.11
o 源码：修正中文字体在战术下面下的拖影bug（svn4799）
o ja2_cn.exe: 升级到4815

- 2011.11.08
o 修正源码中对中文的处理函数，解决一直以来中文不会自动换行的bug，修正的源码文件已提交熊窝
o 同时把 files.edt、rip.edt、email.edt、help.edt、impass.edt 里面以前为了解决换行人工加上的“±”，并提交熊窝 SVN
o ps: 最新的 TBS113 lang zh_CN 为 4787，这个版本的汉化补丁需要 4785 以上的 ja2.exe 源码才能正常执行。
o 专用字体使用 ja2font3 v4

- 2011.11.03
o TBS Chinese(GB) patch 更名为 TBS 1.13 lang zh_CN patch，简称 TBS113 lang zh_CN


=== 1.13中文版安装方法 ===

1. 安装好 JA2 Gold 1.10/1.12
2. 把 1.13 Gamedata svnxxxx 覆盖到游戏目录
3. 把 TBS 1.13 lang zh_CN 覆盖到游戏目录
4. 打开 ja2.ini
		- 找到 VFS_CONFIG_INI = vfs_config.JA2113.ini 改为 VFS_CONFIG_INI = vfs_config.JA2113.CN.ini
		- [最新版] 找到 USE_WINFONTS = 0 改为 USE_WINFONTS = 1
5. 安装专用字体 ja2font3 （ja2font3_v4.7z)，如果系统以前安装过就不用再次安装了
6. 运行 ja2_cn.exe


=== 相关资源 ===

- 熊窝安装包
o 参考此贴/文：http://tbsgame.net/bbs/index.php?showtopic=63017 | http://zww.me/archives/25462

- ja2.exe源码编译和zww's SCI(一键包) 
o http://tbsgame.net/bbs/index.php?showtopic=67481

- zww's SCI
o http://zww.me/ja2


============================================================================

最新更新/有问题请到下面几个地方提问：

TBS论坛（铁资论坛）
http://tbsgame.net/bbs/

百度铁血联盟吧
http://tieba.baidu.com/f?kw=%CC%FA%D1%AA%C1%AA%C3%CB

ZWWoOoOo's 博客
http://zww.me/ja2

============================================================================

相关教程和下载

1.13 svn地址：
https://ja2svn.dyndns.org/source/ja2_v1.13_data/GameDir

1.13中文补丁（TBS 1.13 lang zh_CN SVN）熊窝SVN地址：
https://ja2svn.dyndns.org/source/ja2/trunk/GameData/Chinese_Version

1.13中文补丁（TBS 1.13 lang zh_CN SVN）国内SVN地址：
http://ja2v113cn.googlecode.com/svn/trunk/tbs-chinese-svn

IoV MOD 下载地址：（原名 DBB Cosplay）
http://www.gun-world.net/ja2mod/ja2.htm
http://tbsgame.net/bbs