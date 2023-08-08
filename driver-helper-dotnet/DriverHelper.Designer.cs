namespace driver_helper_dotnet
{
    partial class DriverHelper
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.statuslbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.progresslbl = new System.Windows.Forms.Label();
            this.progresslblM = new System.Windows.Forms.Label();
            this.readBtn = new System.Windows.Forms.Button();
            this.groupNametxt = new System.Windows.Forms.TextBox();
            this.groupName = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(-1, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(802, 449);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cancelBtn);
            this.tabPage1.Controls.Add(this.statuslbl);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.progresslbl);
            this.tabPage1.Controls.Add(this.progresslblM);
            this.tabPage1.Controls.Add(this.readBtn);
            this.tabPage1.Controls.Add(this.groupNametxt);
            this.tabPage1.Controls.Add(this.groupName);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(794, 421);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "分頁";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(579, 179);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(100, 41);
            this.cancelBtn.TabIndex = 7;
            this.cancelBtn.Text = "終止任務";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // statuslbl
            // 
            this.statuslbl.AutoSize = true;
            this.statuslbl.Location = new System.Drawing.Point(504, 37);
            this.statuslbl.Name = "statuslbl";
            this.statuslbl.Size = new System.Drawing.Size(0, 15);
            this.statuslbl.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(409, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "狀態:";
            // 
            // progresslbl
            // 
            this.progresslbl.AutoSize = true;
            this.progresslbl.Location = new System.Drawing.Point(504, 74);
            this.progresslbl.Name = "progresslbl";
            this.progresslbl.Size = new System.Drawing.Size(23, 15);
            this.progresslbl.TabIndex = 4;
            this.progresslbl.Text = "0%";
            // 
            // progresslblM
            // 
            this.progresslblM.AutoSize = true;
            this.progresslblM.Location = new System.Drawing.Point(409, 74);
            this.progresslblM.Name = "progresslblM";
            this.progresslblM.Size = new System.Drawing.Size(36, 15);
            this.progresslblM.TabIndex = 3;
            this.progresslblM.Text = "進度:";
            // 
            // readBtn
            // 
            this.readBtn.Location = new System.Drawing.Point(126, 179);
            this.readBtn.Name = "readBtn";
            this.readBtn.Size = new System.Drawing.Size(100, 41);
            this.readBtn.TabIndex = 2;
            this.readBtn.Text = "讀取";
            this.readBtn.UseVisualStyleBackColor = true;
            this.readBtn.Click += new System.EventHandler(this.readBtn_Click);
            // 
            // groupNametxt
            // 
            this.groupNametxt.Location = new System.Drawing.Point(126, 29);
            this.groupNametxt.Name = "groupNametxt";
            this.groupNametxt.Size = new System.Drawing.Size(100, 23);
            this.groupNametxt.TabIndex = 1;
            // 
            // groupName
            // 
            this.groupName.AutoSize = true;
            this.groupName.Location = new System.Drawing.Point(31, 29);
            this.groupName.Name = "groupName";
            this.groupName.Size = new System.Drawing.Size(62, 15);
            this.groupName.TabIndex = 0;
            this.groupName.Text = "群組名稱:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // DriverHelper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "DriverHelper";
            this.Text = "DriverHelper";
            this.Load += new System.EventHandler(this.DriverHelper_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TextBox groupNametxt;
        private Label groupName;
        private Button readBtn;
        private OpenFileDialog openFileDialog1;
        private Label progresslbl;
        private Label progresslblM;
        private Label statuslbl;
        private Label label2;
        private Button cancelBtn;
        private Label readLineLbl;
    }
}