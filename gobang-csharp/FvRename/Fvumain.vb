Option Explicit On

'@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
Imports System.IO

'@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
''' ---------------------------------------------------------------------------
''' <summary> [主窗体] </summary>
''' +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Public Class Fvumain
   ' 组合框事件委托（用于调用组合框事件）。
   Private Delegate Sub bdlCmbEvent(ByVal sender As System.Object, ByVal e As System.EventArgs)

   '%%%%%%%%%
   Private bblLoadding As Boolean                                 '是否正在加载主窗体。

   '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
   Private Sub Fvumain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
      ' [主窗体加载事件]
      ' 正在加载主窗体。
      bblLoadding = True
      ' 初始化重命名方式组合框。
      cmbRnmStyle1.Items.Add("重命名文件")
      cmbRnmStyle1.Items.Add("重命名文件夹")
      cmbRnmStyle2.Items.Add("添加文本到左边")
      cmbRnmStyle2.Items.Add("插入文本到中间")
      cmbRnmStyle2.Items.Add("添加文本到右边")
      cmbRnmStyle2.Items.Add("替换指定文本")
      cmbRnmStyle2.Items.Add("替换指定位置的文本")
      cmbRnmStyle1.Text = "重命名文件"                            '（此时会调用 cmbRnmStyle1_SelectedIndexChanged()）
      cmbRnmStyle2.Text = "添加文本到左边"
      ' 初始化重命名文本文本框。
      txbRnmText1.Enabled = True : txbRnmText2.Enabled = False : txbRnmText3.Enabled = False
      tltToltip.SetToolTip(txbRnmText1, "请输入将要添加到左边的文本")
      ' 显示注意事项。
      lsbFilList.Items.Add("【注意事项】")
      lsbFilList.Items.Add("      在重命名文件或文件夹前，建议你先备份文")
      lsbFilList.Items.Add("件，因为本操作将会覆盖原来的文件或文件夹！")
      ' 正在加载主窗体为假。
      bblLoadding = False
   End Sub

   '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
   Private Sub cmbRnmStyle1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbRnmStyle1.SelectedIndexChanged
      ' [重命名方式1组合框 选取索引改变事件]
      ' 防止在主窗体加载事件中调用本函数。
      If (bblLoadding = True) Then Exit Sub
      ' 声明重命名方式2组合框事件委托。
      Dim lce1 As New bdlCmbEvent(AddressOf cmbRnmStyle2_SelectedIndexChanged)
      ' 调用重命名方式2组合框事件。
      lce1(New Object, New System.EventArgs)
   End Sub

   Private Sub cmbRnmStyle2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbRnmStyle2.SelectedIndexChanged
      ' [重命名方式2组合框 选取索引改变事件]
      ' 防止在主窗体加载事件中调用本函数。
      If (bblLoadding = True) Then Exit Sub
      ' 根据重命名方式2组合框文本。
      Select Case cmbRnmStyle2.Text
         Case Is = "添加文本到左边"
            txbRnmText1.Enabled = True : txbRnmText2.Enabled = False : txbRnmText3.Enabled = False
            txbRnmText2.Text = "" : txbRnmText3.Text = ""
            tltToltip.SetToolTip(txbRnmText1, "请输入将要添加到左边的文本")
         Case Is = "插入文本到中间"
            txbRnmText1.Enabled = True : txbRnmText2.Enabled = True : txbRnmText3.Enabled = False
            txbRnmText3.Text = ""
            tltToltip.SetToolTip(txbRnmText1, "请输入将要插入到中间的文本") : tltToltip.SetToolTip(txbRnmText2, "请输入插入位置")
         Case Is = "添加文本到右边"
            txbRnmText1.Enabled = True : txbRnmText2.Enabled = False : txbRnmText3.Enabled = False
            txbRnmText2.Text = "" : txbRnmText3.Text = ""
            tltToltip.SetToolTip(txbRnmText1, "请输入将要添加到右边的文本")
         Case Is = "替换指定文本"
            txbRnmText1.Enabled = True : txbRnmText2.Enabled = True : txbRnmText3.Enabled = False
            txbRnmText3.Text = ""
            tltToltip.SetToolTip(txbRnmText1, "请输入查找文本") : tltToltip.SetToolTip(txbRnmText2, "请输入替换文本")
         Case Is = "替换指定位置的文本"
            txbRnmText1.Enabled = True : txbRnmText2.Enabled = True : txbRnmText3.Enabled = True
            tltToltip.SetToolTip(txbRnmText1, "请输入替换文本的开始位置") : tltToltip.SetToolTip(txbRnmText2, "请输入替换文本的结束位置") : tltToltip.SetToolTip(txbRnmText3, "请输入替换文本")
      End Select
   End Sub

   '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
   Private Sub mnuFilOpnFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFilOpnFolder.Click
      ' [打开文件夹]
      Dim ii As Integer
      Dim lvalAllFiles As New ArrayList
      Dim lvst1 As String

      ' 设置并显示文件夹对话框。
      fddFodDialog.Description = "请选取一个要处理的文件夹"
      fddFodDialog.SelectedPath = "D:\"                           '设置初始目录。
      If (fddFodDialog.ShowDialog(Me) <> DialogResult.OK) Then Exit Sub
      ' 获取并显示选取目录。
      txbCurDirectory.Text = fddFodDialog.SelectedPath            '显示当前目录。
      txbCurDirectory.SelectionStart = txbCurDirectory.TextLength
      ' 清零。
      txbFilQuantity.Text = "" : lsbFilList.Items.Clear()
      lsbFilList.Sorted = False                                   '正向排序为假。

      ' 若重命名方式1为重命名文件。
      If (cmbRnmStyle1.Text = "重命名文件") Then
         ' 获取当前目录下的全部文件。
         If (bfnGetFiles(txbCurDirectory.Text, True, lvalAllFiles) = False) Then Return
         txbFilQuantity.Text = lvalAllFiles.Count                 '显示文件总数。
         ' 添加全部文件名称到列表框。
         For ii = 0 To (lvalAllFiles.Count - 1)
            lvst1 = Path.GetFileName(lvalAllFiles(ii))
            lsbFilList.Items.Add(lvst1)                           '文件名称。
         Next ii
         ' 若重命名方式1为重命名文件夹。
      ElseIf (cmbRnmStyle1.Text = "重命名文件夹") Then
         ' 获取当前目录下的全部文件夹。
         If (bfnGetFiles(txbCurDirectory.Text, False, lvalAllFiles) = False) Then Return
         txbFilQuantity.Text = lvalAllFiles.Count                 '显示文件夹总数。
         ' 添加全部文件夹名称到列表框。
         For ii = 0 To (lvalAllFiles.Count - 1)
            lvst1 = Path.GetFileName(lvalAllFiles(ii))
            lsbFilList.Items.Add(lvst1)                           '文件夹名称。
         Next ii
      End If
      lsbFilList.Sorted = True                                    '正向排序为真。
   End Sub

   Private Sub mnuFilExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFilExit.Click
      ' [退出]
      Me.Close()
   End Sub

   '$$$$$$$$$
   Private Sub mnuEdtRnmFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEdtRnmFile.Click
      ' [重命名文件]
      Dim ii, lvit1, lvit2, lvit21, lvit22, lvitM1 As Integer
      Dim lvitSrcFilNamLength, lvitSrcFilTitLength As Integer
      Dim lvst1, lvst2 As String
      Dim lvstSrcFilPath, lvstSrcFilName, lvstSrcFilTitle, lvstSrcFilExtention, lvstDstFilPath, lvstDstFilName As String

      '###########################################################
      If (Directory.Exists(txbCurDirectory.Text) = False) Then Exit Sub
      lvitM1 = lsbFilList.Items.Count
      If (lvitM1 <= 0) Then Exit Sub
      lvstSrcFilTitle = "" : lvstSrcFilExtention = "" : lvstDstFilName = ""

      '###########################################################
      ' 遍历每个文件。
      For ii = 0 To lvitM1 - 1
         ' 获取源文件名称。
         lvstSrcFilName = lsbFilList.Items(ii)                    '源文件名（或源文件夹名）。
         lvitSrcFilNamLength = Len(lvstSrcFilName)                '源文件名长度（或源文件夹名长度）。
         If (cmbRnmStyle1.Text = "重命名文件") Then
            lvit1 = InStrRev(lvstSrcFilName, ".")
            lvitSrcFilTitLength = lvit1 - 1                       '源文件标题长度（无扩展名）。
            If (lvitSrcFilTitLength <= 0) Then GoTo csError
            lvstSrcFilTitle = Strings.Left(lvstSrcFilName, lvitSrcFilTitLength)   '源文件标题（无扩展名）。
            lvstSrcFilExtention = Mid(lvstSrcFilName, lvit1)      '源文件扩展名（含点符）。
            lvstSrcFilExtention = LCase$(lvstSrcFilExtention)
         End If

         ' 获取目标文件名称。
         Select Case cmbRnmStyle2.Text
            Case Is = "添加文本到左边"
               lvstDstFilName = txbRnmText1.Text & lvstSrcFilName '目标文件名11。
            Case Is = "插入文本到中间"
               If (txbRnmText2.Text = "") Then GoTo csError
               lvit21 = CInt(txbRnmText2.Text)                    '插入位置。
               If (cmbRnmStyle1.Text = "重命名文件") Then
                  If (lvit21 <= lvitSrcFilTitLength) Then         '若插入位置合理。
                     lvit1 = lvit21 - 1                           '左边长度。
                     lvit2 = lvitSrcFilTitLength - lvit1          '右边长度。
                     lvst1 = Strings.Left(lvstSrcFilTitle, lvit1)
                     lvst2 = Strings.Right(lvstSrcFilTitle, lvit2)
                     lvstDstFilName = lvst1 & txbRnmText1.Text & lvst2 & lvstSrcFilExtention   '目标文件名21。
                  Else
                     GoTo csNext                                  '(不作任何处理)
                  End If
               ElseIf (cmbRnmStyle1.Text = "重命名文件夹") Then
                  If (lvit21 <= lvitSrcFilNamLength) Then         '若插入位置合理。
                     lvit1 = lvit21 - 1                           '左边长度。
                     lvit2 = lvitSrcFilNamLength - lvit1          '右边长度。
                     lvst1 = Strings.Left(lvstSrcFilName, lvit1)
                     lvst2 = Strings.Right(lvstSrcFilName, lvit2)
                     lvstDstFilName = lvst1 & txbRnmText1.Text & lvst2   '目标文件名22。
                  Else
                     GoTo csNext                                  '(不作任何处理)
                  End If
               End If
            Case Is = "添加文本到右边"
               If (cmbRnmStyle1.Text = "重命名文件") Then
                  lvstDstFilName = lvstSrcFilTitle & txbRnmText1.Text & lvstSrcFilExtention   '目标文件名31。
               ElseIf (cmbRnmStyle1.Text = "重命名文件夹") Then
                  lvstDstFilName = lvstSrcFilName & txbRnmText1.Text   '目标文件名32。
               End If
            Case Is = "替换指定文本"
               If (cmbRnmStyle1.Text = "重命名文件") Then
                  lvit1 = InStr(lvstSrcFilTitle, txbRnmText1.Text)   '查找文本的位置。
                  If (lvit1 > 0) Then
                     lvst1 = Replace(lvstSrcFilTitle, txbRnmText1.Text, txbRnmText2.Text)   '替换文本。
                     lvstDstFilName = lvst1 & lvstSrcFilExtention '目标文件名41。
                  Else
                     GoTo csNext                                  '(不作任何处理)
                  End If
               ElseIf (cmbRnmStyle1.Text = "重命名文件夹") Then
                  lvit1 = InStr(lvstSrcFilName, txbRnmText1.Text) '查找文本的位置。
                  If (lvit1 > 0) Then
                     lvstDstFilName = Replace(lvstSrcFilName, txbRnmText1.Text, txbRnmText2.Text)   '目标文件名42。
                  Else
                     GoTo csNext                                  '(不作任何处理)
                  End If
               End If
            Case Is = "替换指定位置的文本"
               lvit21 = CInt(txbRnmText1.Text)                    '替换文本的开始位置。
               lvit22 = CInt(txbRnmText2.Text)                    '替换文本的结束位置。
               If (lvit21 > 0) And (lvit22 >= lvit21) Then
                  If (cmbRnmStyle1.Text = "重命名文件") Then
                     If (lvit21 <= lvitSrcFilTitLength) And (lvit22 <= lvitSrcFilTitLength) Then
                        lvst1 = Mid(lvstSrcFilTitle, lvit21, (lvit22 - lvit21) + 1)   '获取查找文本。
                        lvst2 = Replace(lvstSrcFilTitle, lvst1, txbRnmText3.Text, lvit21, 1)   '替换文本（只替换一次）。
                        lvstDstFilName = lvst2 & lvstSrcFilExtention   '目标文件名51。
                     Else
                        GoTo csNext                               '(不作任何处理)
                     End If
                  ElseIf (cmbRnmStyle1.Text = "重命名文件夹") Then
                     If (lvit21 <= lvitSrcFilNamLength) And (lvit22 <= lvitSrcFilNamLength) Then
                        lvst1 = Mid(lvstSrcFilName, lvit21, (lvit22 - lvit21) + 1)   '获取查找文本。
                        lvstDstFilName = Replace(lvstSrcFilName, lvst1, txbRnmText3.Text, lvit21, 1)   '目标文件名52。
                     Else
                        GoTo csNext                               '(不作任何处理)
                     End If
                  End If
               Else
                  MessageBox.Show("替换文本的位置错误，请重新输入！", "错误")
                  Exit Sub
               End If
         End Select
         If (lvstSrcFilName.Trim() = "") Or (lvstDstFilName.Trim() = "") Then GoTo csError

         ' 获取源、目标文件路径。
         lvstSrcFilPath = txbCurDirectory.Text & "\" & lvstSrcFilName
         lvstDstFilPath = txbCurDirectory.Text & "\" & lvstDstFilName

         ' 重命名文件或文件夹。
         If (lvstSrcFilName <> lvstDstFilName) And (File.Exists(lvstDstFilPath) = False) Then   '若源文件名和目标文件名不同、目标文件不存在。
            FileSystem.Rename(lvstSrcFilPath, lvstDstFilPath)
         End If
csNext:
      Next ii
      If (cmbRnmStyle1.Text = "重命名文件") Then
         MessageBox.Show("已成功重命名全部文件！", "提示")
      ElseIf (cmbRnmStyle1.Text = "重命名文件夹") Then
         MessageBox.Show("已成功重命名全部文件夹！", "提示")
      End If
      Exit Sub

      '###########################################################
csError:
      MessageBox.Show("重命名文件时发生错误！", "错误")
   End Sub

   Private Sub mnuEdtClrContent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEdtClrContent.Click
      ' [清除内容]
      txbCurDirectory.Text = "" : txbFilQuantity.Text = ""
      lsbFilList.Items.Clear()
   End Sub

   Private Sub mnuHlpAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuHlpAbout.Click
      ' [关于]
      MessageBox.Show("批量重命名文件软件" & vbCrLf & vbCrLf & "SongFuSheng，2021-3-20日。", "关于")
   End Sub

   '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
   ''' ------------------------------------------------------------------------
   ''' <summary> [获取指定目录下的全部文件或文件夹] </summary>
   ''' <param name="ostSpfDirectory"> 要从中获取文件或文件夹的指定目录（不搜索子目录）。</param>
   ''' <param name="oblGetFile"> 是否获取文件。</param>
   ''' <param name="oalRtnAllFiles"> 返回获取的全部文件或文件夹路径的数组。</param>
   ''' <returns> 若获取的文件或文件夹总数大于或等于0则返回真，否则返回假。</returns>
   ''' ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
   Private Function bfnGetFiles(ByVal ostSpfDirectory As String, ByVal oblGetFile As Boolean, ByRef oalRtnAllFiles As ArrayList) As Boolean
      Dim lvdiBasFolder, lvdiFolder As DirectoryInfo
      Dim lvfiFile As FileInfo

      ' 初始化。
      bfnGetFiles = False
      If (Directory.Exists(ostSpfDirectory) = False) Then Exit Function
      oalRtnAllFiles = New ArrayList
      ' 定义基本文件夹。
      lvdiBasFolder = New DirectoryInfo(ostSpfDirectory)

      ' 获取指定目录（不搜索子目录）下的全部文件或文件夹。
      ' 若是获取文件。
      If (oblGetFile = True) Then
         ' 遍历基本文件夹下的每个子文件。
         For Each lvfiFile In lvdiBasFolder.GetFiles()
            ' 若当前文件非隐藏文件。
            If (lvfiFile.Attributes And FileAttributes.Hidden) <> FileAttributes.Hidden Then
               ' 添加一个文件路径到数组中。
               oalRtnAllFiles.Add(lvfiFile.FullName)
            End If
         Next
         ' 若是获取文件夹。
      Else
         ' 遍历基本文件夹下的每个子文件夹。
         For Each lvdiFolder In lvdiBasFolder.GetDirectories()
            ' 若当前文件夹非隐藏文件夹。
            If (lvdiFolder.Attributes And FileAttributes.Hidden) <> FileAttributes.Hidden Then
               ' 添加一个文件夹路径到数组中。
               oalRtnAllFiles.Add(lvdiFolder.FullName)
            End If
         Next
      End If
      bfnGetFiles = True
   End Function

End Class
