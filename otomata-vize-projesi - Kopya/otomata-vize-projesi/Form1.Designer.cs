namespace otomata_vize_projesi
{
    partial class Form1
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
            this.alfabeTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.RegexTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.kelimeMiktarTextBox = new System.Windows.Forms.TextBox();
            this.uretBtn = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // alfabeTextBox
            // 
            this.alfabeTextBox.Location = new System.Drawing.Point(181, 26);
            this.alfabeTextBox.Name = "alfabeTextBox";
            this.alfabeTextBox.Size = new System.Drawing.Size(198, 23);
            this.alfabeTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Alfabeyi Giriniz (\",\" ile ayrılır) :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Düzenli İfadeyi Giriniz :";
            // 
            // RegexTextBox
            // 
            this.RegexTextBox.Location = new System.Drawing.Point(181, 55);
            this.RegexTextBox.Name = "RegexTextBox";
            this.RegexTextBox.Size = new System.Drawing.Size(198, 23);
            this.RegexTextBox.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Kaç Kelime Üretilsin :";
            // 
            // kelimeMiktarTextBox
            // 
            this.kelimeMiktarTextBox.Location = new System.Drawing.Point(181, 84);
            this.kelimeMiktarTextBox.Name = "kelimeMiktarTextBox";
            this.kelimeMiktarTextBox.Size = new System.Drawing.Size(198, 23);
            this.kelimeMiktarTextBox.TabIndex = 4;
            // 
            // uretBtn
            // 
            this.uretBtn.Location = new System.Drawing.Point(229, 113);
            this.uretBtn.Name = "uretBtn";
            this.uretBtn.Size = new System.Drawing.Size(84, 39);
            this.uretBtn.TabIndex = 6;
            this.uretBtn.Text = "ÜRET";
            this.uretBtn.UseVisualStyleBackColor = true;
            this.uretBtn.Click += new System.EventHandler(this.uretBtn_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(12, 207);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(367, 304);
            this.listBox1.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "Üretilen Kelimeler :";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 561);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.uretBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.kelimeMiktarTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RegexTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.alfabeTextBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox alfabeTextBox;
        private Label label1;
        private Label label2;
        private TextBox RegexTextBox;
        private Label label3;
        private TextBox kelimeMiktarTextBox;
        private Button uretBtn;
        private ListBox listBox1;
        private Label label4;
    }
}