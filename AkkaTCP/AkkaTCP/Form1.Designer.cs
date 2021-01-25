
namespace AkkaTCP
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
            this.RichTextMsg = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // RichTextMsg
            // 
            this.RichTextMsg.Location = new System.Drawing.Point(45, 124);
            this.RichTextMsg.Name = "RichTextMsg";
            this.RichTextMsg.Size = new System.Drawing.Size(666, 373);
            this.RichTextMsg.TabIndex = 0;
            this.RichTextMsg.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 533);
            this.Controls.Add(this.RichTextMsg);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox RichTextMsg;
    }
}

