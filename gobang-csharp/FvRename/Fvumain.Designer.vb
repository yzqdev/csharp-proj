<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Fvumain
   Inherits System.Windows.Forms.Form

   'Form 重写 Dispose，以清理组件列表。
   <System.Diagnostics.DebuggerNonUserCode()> _
   Protected Overrides Sub Dispose(ByVal disposing As Boolean)
      Try
         If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
         End If
      Finally
         MyBase.Dispose(disposing)
      End Try
   End Sub

   'Windows 窗体设计器所必需的
   Private components As System.ComponentModel.IContainer

   '注意: 以下过程是 Windows 窗体设计器所必需的
   '可以使用 Windows 窗体设计器修改它。
   '不要使用代码编辑器修改它。
   <System.Diagnostics.DebuggerStepThrough()> _
   Private Sub InitializeComponent()
      Me.components = New System.ComponentModel.Container
      Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Fvumain))
      Me.mnuMain = New System.Windows.Forms.MenuStrip
      Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem
      Me.mnuFilOpnFolder = New System.Windows.Forms.ToolStripMenuItem
      Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
      Me.mnuFilExit = New System.Windows.Forms.ToolStripMenuItem
      Me.mnuEdit = New System.Windows.Forms.ToolStripMenuItem
      Me.mnuEdtRnmFile = New System.Windows.Forms.ToolStripMenuItem
      Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
      Me.mnuEdtClrContent = New System.Windows.Forms.ToolStripMenuItem
      Me.mnuHelp = New System.Windows.Forms.ToolStripMenuItem
      Me.mnuHlpAbout = New System.Windows.Forms.ToolStripMenuItem
      Me.Label1 = New System.Windows.Forms.Label
      Me.Label2 = New System.Windows.Forms.Label
      Me.Label3 = New System.Windows.Forms.Label
      Me.Label4 = New System.Windows.Forms.Label
      Me.txbCurDirectory = New System.Windows.Forms.TextBox
      Me.txbRnmText1 = New System.Windows.Forms.TextBox
      Me.txbRnmText2 = New System.Windows.Forms.TextBox
      Me.txbRnmText3 = New System.Windows.Forms.TextBox
      Me.txbFilQuantity = New System.Windows.Forms.TextBox
      Me.cmbRnmStyle1 = New System.Windows.Forms.ComboBox
      Me.cmbRnmStyle2 = New System.Windows.Forms.ComboBox
      Me.lsbFilList = New System.Windows.Forms.ListBox
      Me.fddFodDialog = New System.Windows.Forms.FolderBrowserDialog
      Me.tltToltip = New System.Windows.Forms.ToolTip(Me.components)
      Me.mnuMain.SuspendLayout()
      Me.SuspendLayout()
      '
      'mnuMain
      '
      Me.mnuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuEdit, Me.mnuHelp})
      Me.mnuMain.Location = New System.Drawing.Point(0, 0)
      Me.mnuMain.Name = "mnuMain"
      Me.mnuMain.Size = New System.Drawing.Size(434, 25)
      Me.mnuMain.TabIndex = 0
      Me.mnuMain.Text = "MenuStrip1"
      '
      'mnuFile
      '
      Me.mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFilOpnFolder, Me.ToolStripSeparator1, Me.mnuFilExit})
      Me.mnuFile.Name = "mnuFile"
      Me.mnuFile.Size = New System.Drawing.Size(58, 21)
      Me.mnuFile.Text = "文件(&F)"
      '
      'mnuFilOpnFolder
      '
      Me.mnuFilOpnFolder.Name = "mnuFilOpnFolder"
      Me.mnuFilOpnFolder.Size = New System.Drawing.Size(167, 22)
      Me.mnuFilOpnFolder.Text = "打开文件夹(&O) ..."
      '
      'ToolStripSeparator1
      '
      Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
      Me.ToolStripSeparator1.Size = New System.Drawing.Size(164, 6)
      '
      'mnuFilExit
      '
      Me.mnuFilExit.Name = "mnuFilExit"
      Me.mnuFilExit.Size = New System.Drawing.Size(167, 22)
      Me.mnuFilExit.Text = "退出(&X)"
      '
      'mnuEdit
      '
      Me.mnuEdit.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuEdtRnmFile, Me.ToolStripSeparator2, Me.mnuEdtClrContent})
      Me.mnuEdit.Name = "mnuEdit"
      Me.mnuEdit.Size = New System.Drawing.Size(59, 21)
      Me.mnuEdit.Text = "编辑(&E)"
      '
      'mnuEdtRnmFile
      '
      Me.mnuEdtRnmFile.Name = "mnuEdtRnmFile"
      Me.mnuEdtRnmFile.Size = New System.Drawing.Size(152, 22)
      Me.mnuEdtRnmFile.Text = "重命名文件(&R)"
      '
      'ToolStripSeparator2
      '
      Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
      Me.ToolStripSeparator2.Size = New System.Drawing.Size(149, 6)
      '
      'mnuEdtClrContent
      '
      Me.mnuEdtClrContent.Name = "mnuEdtClrContent"
      Me.mnuEdtClrContent.Size = New System.Drawing.Size(152, 22)
      Me.mnuEdtClrContent.Text = "清除内容(&C)"
      '
      'mnuHelp
      '
      Me.mnuHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuHlpAbout})
      Me.mnuHelp.Name = "mnuHelp"
      Me.mnuHelp.Size = New System.Drawing.Size(61, 21)
      Me.mnuHelp.Text = "帮助(&H)"
      '
      'mnuHlpAbout
      '
      Me.mnuHlpAbout.Name = "mnuHlpAbout"
      Me.mnuHlpAbout.Size = New System.Drawing.Size(129, 22)
      Me.mnuHlpAbout.Text = "关于(&A) ..."
      '
      'Label1
      '
      Me.Label1.AutoSize = True
      Me.Label1.Location = New System.Drawing.Point(8, 32)
      Me.Label1.Name = "Label1"
      Me.Label1.Size = New System.Drawing.Size(59, 12)
      Me.Label1.TabIndex = 1
      Me.Label1.Text = "当前目录:"
      '
      'Label2
      '
      Me.Label2.AutoSize = True
      Me.Label2.Location = New System.Drawing.Point(8, 80)
      Me.Label2.Name = "Label2"
      Me.Label2.Size = New System.Drawing.Size(71, 12)
      Me.Label2.TabIndex = 2
      Me.Label2.Text = "重命名方式:"
      '
      'Label3
      '
      Me.Label3.AutoSize = True
      Me.Label3.Location = New System.Drawing.Point(8, 144)
      Me.Label3.Name = "Label3"
      Me.Label3.Size = New System.Drawing.Size(71, 12)
      Me.Label3.TabIndex = 3
      Me.Label3.Text = "重命名文本:"
      '
      'Label4
      '
      Me.Label4.AutoSize = True
      Me.Label4.Location = New System.Drawing.Point(8, 288)
      Me.Label4.Name = "Label4"
      Me.Label4.Size = New System.Drawing.Size(59, 12)
      Me.Label4.TabIndex = 4
      Me.Label4.Text = "文件总数:"
      '
      'txbCurDirectory
      '
      Me.txbCurDirectory.Location = New System.Drawing.Point(72, 32)
      Me.txbCurDirectory.Name = "txbCurDirectory"
      Me.txbCurDirectory.Size = New System.Drawing.Size(360, 21)
      Me.txbCurDirectory.TabIndex = 5
      Me.tltToltip.SetToolTip(Me.txbCurDirectory, "当前选取的要处理的目录")
      '
      'txbRnmText1
      '
      Me.txbRnmText1.Location = New System.Drawing.Point(8, 160)
      Me.txbRnmText1.Name = "txbRnmText1"
      Me.txbRnmText1.Size = New System.Drawing.Size(136, 21)
      Me.txbRnmText1.TabIndex = 6
      '
      'txbRnmText2
      '
      Me.txbRnmText2.Location = New System.Drawing.Point(8, 184)
      Me.txbRnmText2.Name = "txbRnmText2"
      Me.txbRnmText2.Size = New System.Drawing.Size(136, 21)
      Me.txbRnmText2.TabIndex = 7
      '
      'txbRnmText3
      '
      Me.txbRnmText3.Location = New System.Drawing.Point(8, 208)
      Me.txbRnmText3.Name = "txbRnmText3"
      Me.txbRnmText3.Size = New System.Drawing.Size(136, 21)
      Me.txbRnmText3.TabIndex = 8
      '
      'txbFilQuantity
      '
      Me.txbFilQuantity.Location = New System.Drawing.Point(8, 304)
      Me.txbFilQuantity.Name = "txbFilQuantity"
      Me.txbFilQuantity.Size = New System.Drawing.Size(136, 21)
      Me.txbFilQuantity.TabIndex = 9
      '
      'cmbRnmStyle1
      '
      Me.cmbRnmStyle1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
      Me.cmbRnmStyle1.FormattingEnabled = True
      Me.cmbRnmStyle1.Location = New System.Drawing.Point(8, 96)
      Me.cmbRnmStyle1.Name = "cmbRnmStyle1"
      Me.cmbRnmStyle1.Size = New System.Drawing.Size(136, 20)
      Me.cmbRnmStyle1.TabIndex = 10
      '
      'cmbRnmStyle2
      '
      Me.cmbRnmStyle2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
      Me.cmbRnmStyle2.FormattingEnabled = True
      Me.cmbRnmStyle2.Location = New System.Drawing.Point(8, 120)
      Me.cmbRnmStyle2.Name = "cmbRnmStyle2"
      Me.cmbRnmStyle2.Size = New System.Drawing.Size(136, 20)
      Me.cmbRnmStyle2.TabIndex = 11
      '
      'lsbFilList
      '
      Me.lsbFilList.FormattingEnabled = True
      Me.lsbFilList.HorizontalScrollbar = True
      Me.lsbFilList.ItemHeight = 12
      Me.lsbFilList.Location = New System.Drawing.Point(152, 56)
      Me.lsbFilList.Name = "lsbFilList"
      Me.lsbFilList.Size = New System.Drawing.Size(280, 268)
      Me.lsbFilList.TabIndex = 12
      '
      'Fvumain
      '
      Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
      Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
      Me.ClientSize = New System.Drawing.Size(434, 328)
      Me.Controls.Add(Me.lsbFilList)
      Me.Controls.Add(Me.cmbRnmStyle2)
      Me.Controls.Add(Me.cmbRnmStyle1)
      Me.Controls.Add(Me.txbFilQuantity)
      Me.Controls.Add(Me.txbRnmText3)
      Me.Controls.Add(Me.txbRnmText2)
      Me.Controls.Add(Me.txbRnmText1)
      Me.Controls.Add(Me.txbCurDirectory)
      Me.Controls.Add(Me.Label4)
      Me.Controls.Add(Me.Label3)
      Me.Controls.Add(Me.Label2)
      Me.Controls.Add(Me.Label1)
      Me.Controls.Add(Me.mnuMain)
      Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
      Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
      Me.MainMenuStrip = Me.mnuMain
      Me.MaximizeBox = False
      Me.Name = "Fvumain"
      Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
      Me.Text = "批量重命名文件"
      Me.mnuMain.ResumeLayout(False)
      Me.mnuMain.PerformLayout()
      Me.ResumeLayout(False)
      Me.PerformLayout()

   End Sub
   Friend WithEvents mnuMain As System.Windows.Forms.MenuStrip
   Friend WithEvents mnuFile As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents mnuEdit As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents mnuHelp As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents Label1 As System.Windows.Forms.Label
   Friend WithEvents Label2 As System.Windows.Forms.Label
   Friend WithEvents Label3 As System.Windows.Forms.Label
   Friend WithEvents Label4 As System.Windows.Forms.Label
   Friend WithEvents txbCurDirectory As System.Windows.Forms.TextBox
   Friend WithEvents txbRnmText1 As System.Windows.Forms.TextBox
   Friend WithEvents txbRnmText2 As System.Windows.Forms.TextBox
   Friend WithEvents txbRnmText3 As System.Windows.Forms.TextBox
   Friend WithEvents txbFilQuantity As System.Windows.Forms.TextBox
   Friend WithEvents cmbRnmStyle1 As System.Windows.Forms.ComboBox
   Friend WithEvents cmbRnmStyle2 As System.Windows.Forms.ComboBox
   Friend WithEvents lsbFilList As System.Windows.Forms.ListBox
   Friend WithEvents fddFodDialog As System.Windows.Forms.FolderBrowserDialog
   Friend WithEvents tltToltip As System.Windows.Forms.ToolTip
   Friend WithEvents mnuFilOpnFolder As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents mnuEdtRnmFile As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents mnuEdtClrContent As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents mnuFilExit As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents mnuHlpAbout As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
   Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator

End Class
