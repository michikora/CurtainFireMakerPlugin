﻿namespace CurtainFireMakerPlugin.Forms
{
    partial class ExportSettingForm
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
            this.label5 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.scriptText = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.exportDirText = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.modelDescriptionText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.modelNameText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.scriptFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.exportDirDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 15);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 12);
            this.label5.TabIndex = 21;
            this.label5.Text = "スクリプト";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(403, 28);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(45, 23);
            this.button5.TabIndex = 20;
            this.button5.Text = "参照";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.Click_Script);
            // 
            // scriptText
            // 
            this.scriptText.BackColor = System.Drawing.SystemColors.Window;
            this.scriptText.Location = new System.Drawing.Point(14, 30);
            this.scriptText.Name = "scriptText";
            this.scriptText.ReadOnly = true;
            this.scriptText.Size = new System.Drawing.Size(383, 19);
            this.scriptText.TabIndex = 19;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.exportDirText);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.modelDescriptionText);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.modelNameText);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(10, 88);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(456, 219);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "出力設定";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "出力先";
            // 
            // exportDirText
            // 
            this.exportDirText.Location = new System.Drawing.Point(8, 36);
            this.exportDirText.Name = "exportDirText";
            this.exportDirText.Size = new System.Drawing.Size(383, 19);
            this.exportDirText.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(397, 34);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(45, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "参照";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Click_ExportDir);
            // 
            // modelDescriptionText
            // 
            this.modelDescriptionText.Location = new System.Drawing.Point(8, 122);
            this.modelDescriptionText.Multiline = true;
            this.modelDescriptionText.Name = "modelDescriptionText";
            this.modelDescriptionText.Size = new System.Drawing.Size(413, 85);
            this.modelDescriptionText.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 107);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "モデル説明";
            // 
            // modelNameText
            // 
            this.modelNameText.Location = new System.Drawing.Point(8, 79);
            this.modelNameText.Name = "modelNameText";
            this.modelNameText.Size = new System.Drawing.Size(413, 19);
            this.modelNameText.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 64);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "モデル名";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(300, 311);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 17;
            this.button4.Text = "キャンセル";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Click_Cancel);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(91, 311);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 16;
            this.button3.Text = "出力";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Click_OK);
            // 
            // scriptFileDialog
            // 
            this.scriptFileDialog.FileName = "openFileDialog1";
            this.scriptFileDialog.Filter = "|*.py";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(14, 55);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(128, 16);
            this.checkBox1.TabIndex = 22;
            this.checkBox1.Text = "ログを開いたままにする";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // ExportSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 344);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.scriptText);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ExportSettingForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "出力設定";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox scriptText;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox exportDirText;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox modelDescriptionText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox modelNameText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.OpenFileDialog scriptFileDialog;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.FolderBrowserDialog exportDirDialog;
    }
}