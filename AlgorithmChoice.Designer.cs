namespace AutoPickingSys
{
    partial class AlgorithmChoice
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_StartAnalysis = new System.Windows.Forms.Button();
            this.checkBox_backlight = new System.Windows.Forms.CheckBox();
            this.btn_modelset = new System.Windows.Forms.Button();
            this.checkBox_sidelight = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_StartAnalysis);
            this.groupBox1.Controls.Add(this.checkBox_backlight);
            this.groupBox1.Controls.Add(this.btn_modelset);
            this.groupBox1.Controls.Add(this.checkBox_sidelight);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(266, 116);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "光照方式";
            // 
            // btn_StartAnalysis
            // 
            this.btn_StartAnalysis.Location = new System.Drawing.Point(125, 71);
            this.btn_StartAnalysis.Name = "btn_StartAnalysis";
            this.btn_StartAnalysis.Size = new System.Drawing.Size(125, 31);
            this.btn_StartAnalysis.TabIndex = 5;
            this.btn_StartAnalysis.Text = "开始分析";
            this.btn_StartAnalysis.UseVisualStyleBackColor = true;
            this.btn_StartAnalysis.Click += new System.EventHandler(this.btn_StartAnalysis_Click);
            // 
            // checkBox_backlight
            // 
            this.checkBox_backlight.AutoSize = true;
            this.checkBox_backlight.Checked = true;
            this.checkBox_backlight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_backlight.Location = new System.Drawing.Point(30, 35);
            this.checkBox_backlight.Name = "checkBox_backlight";
            this.checkBox_backlight.Size = new System.Drawing.Size(72, 16);
            this.checkBox_backlight.TabIndex = 2;
            this.checkBox_backlight.Text = "背光模式";
            this.checkBox_backlight.UseVisualStyleBackColor = true;
            this.checkBox_backlight.CheckedChanged += new System.EventHandler(this.checkBox_backlight_CheckedChanged);
            // 
            // btn_modelset
            // 
            this.btn_modelset.Location = new System.Drawing.Point(125, 27);
            this.btn_modelset.Name = "btn_modelset";
            this.btn_modelset.Size = new System.Drawing.Size(125, 31);
            this.btn_modelset.TabIndex = 4;
            this.btn_modelset.Text = "配置（功能未完善）";
            this.btn_modelset.UseVisualStyleBackColor = true;
            this.btn_modelset.Click += new System.EventHandler(this.btn_modelset_Click);
            // 
            // checkBox_sidelight
            // 
            this.checkBox_sidelight.AutoSize = true;
            this.checkBox_sidelight.Enabled = false;
            this.checkBox_sidelight.Location = new System.Drawing.Point(30, 79);
            this.checkBox_sidelight.Name = "checkBox_sidelight";
            this.checkBox_sidelight.Size = new System.Drawing.Size(72, 16);
            this.checkBox_sidelight.TabIndex = 3;
            this.checkBox_sidelight.Text = "侧光模式";
            this.checkBox_sidelight.UseVisualStyleBackColor = true;
            this.checkBox_sidelight.CheckedChanged += new System.EventHandler(this.checkBox_sidelight_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(281, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "功能尚未完善，师兄请忽视这一段，直接点开始分析";
            // 
            // AlgorithmChoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 160);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "AlgorithmChoice";
            this.Text = "AlgorithmChoice";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox_backlight;
        private System.Windows.Forms.CheckBox checkBox_sidelight;
        private System.Windows.Forms.Button btn_modelset;
        private System.Windows.Forms.Button btn_StartAnalysis;
        private System.Windows.Forms.Label label1;
    }
}