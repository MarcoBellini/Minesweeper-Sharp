namespace Minesweeper_Sharp
{
    partial class AboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            Ok_Button = new Button();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            linkLabel1 = new LinkLabel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // Ok_Button
            // 
            Ok_Button.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Ok_Button.Location = new Point(236, 90);
            Ok_Button.Name = "Ok_Button";
            Ok_Button.Size = new Size(65, 27);
            Ok_Button.TabIndex = 0;
            Ok_Button.Text = "OK";
            Ok_Button.UseVisualStyleBackColor = true;
            Ok_Button.Click += Ok_Button_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.malware_png;
            pictureBox1.Location = new Point(12, 25);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(60, 50);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(92, 25);
            label1.Name = "label1";
            label1.Size = new Size(206, 30);
            label1.TabIndex = 2;
            label1.Text = "Minesweeper Sharp";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(98, 56);
            label2.Name = "label2";
            label2.Size = new Size(184, 15);
            label2.TabIndex = 3;
            label2.Text = "Ver. 1  developed by Marco Bellini";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(135, 96);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(83, 15);
            linkLabel1.TabIndex = 4;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Github Project";
            // 
            // AboutForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(313, 129);
            Controls.Add(linkLabel1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Controls.Add(Ok_Button);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AboutForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "About Minesweeper";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Ok_Button;
        private PictureBox pictureBox1;
        private Label label1;
        private Label label2;
        private LinkLabel linkLabel1;
    }
}