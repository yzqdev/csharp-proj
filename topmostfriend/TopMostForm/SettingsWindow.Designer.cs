namespace TopMostForm
{
    partial class SettingsWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.hotKeyGroup = new System.Windows.Forms.GroupBox();
            this.toggleHotKey = new System.Windows.Forms.Label();
            this.flagsGroup = new System.Windows.Forms.GroupBox();
            this.langGroup = new System.Windows.Forms.GroupBox();
            this.otherGroup = new System.Windows.Forms.GroupBox();
            this.hotKeyGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // hotKeyGroup
            // 
            this.hotKeyGroup.Controls.Add(this.toggleHotKey);
            this.hotKeyGroup.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.hotKeyGroup.Location = new System.Drawing.Point(6, 6);
            this.hotKeyGroup.Name = "hotKeyGroup";
            this.hotKeyGroup.Size = new System.Drawing.Size(412, 70);
            this.hotKeyGroup.TabIndex = 0;
            this.hotKeyGroup.TabStop = false;
            this.hotKeyGroup.Text = "hotkey";
            // 
            // toggleHotKey
            // 
            this.toggleHotKey.AutoSize = true;
            this.toggleHotKey.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.toggleHotKey.Location = new System.Drawing.Point(16, 30);
            this.toggleHotKey.Name = "toggleHotKey";
            this.toggleHotKey.Size = new System.Drawing.Size(45, 19);
            this.toggleHotKey.TabIndex = 0;
            this.toggleHotKey.Text = "label1";
            // 
            // flagsGroup
            // 
            this.flagsGroup.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.flagsGroup.Location = new System.Drawing.Point(6, 80);
            this.flagsGroup.Name = "flagsGroup";
            this.flagsGroup.Size = new System.Drawing.Size(412, 170);
            this.flagsGroup.TabIndex = 1;
            this.flagsGroup.TabStop = false;
            this.flagsGroup.Text = "groupBox1";
            // 
            // langGroup
            // 
            this.langGroup.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.langGroup.Location = new System.Drawing.Point(6, 254);
            this.langGroup.Name = "langGroup";
            this.langGroup.Size = new System.Drawing.Size(412, 55);
            this.langGroup.TabIndex = 2;
            this.langGroup.TabStop = false;
            this.langGroup.Text = "groupBox2";
            // 
            // otherGroup
            // 
            this.otherGroup.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.otherGroup.Location = new System.Drawing.Point(6, 313);
            this.otherGroup.Name = "otherGroup";
            this.otherGroup.Size = new System.Drawing.Size(412, 55);
            this.otherGroup.TabIndex = 3;
            this.otherGroup.TabStop = false;
            this.otherGroup.Text = "groupBox3";
            // 
            // SettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 398);
            this.Controls.Add(this.otherGroup);
            this.Controls.Add(this.langGroup);
            this.Controls.Add(this.flagsGroup);
            this.Controls.Add(this.hotKeyGroup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MdiChildrenMinimizedAnchorBottom = false;
            this.MinimizeBox = false;
            this.Name = "SettingsWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SettingsWindow";
            this.TopMost = true;
            this.hotKeyGroup.ResumeLayout(false);
            this.hotKeyGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox hotKeyGroup;
        private GroupBox flagsGroup;
        private GroupBox langGroup;
        private GroupBox otherGroup;
        private Label toggleHotKey;
    }
}