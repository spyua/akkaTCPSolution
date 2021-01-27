
namespace TcpWindowsForm
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.RichTextMsg = new System.Windows.Forms.RichTextBox();
            this.LocalCntLabel = new System.Windows.Forms.Label();
            this.RemoteCntLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // RichTextMsg
            // 
            this.RichTextMsg.Location = new System.Drawing.Point(40, 84);
            this.RichTextMsg.Name = "RichTextMsg";
            this.RichTextMsg.Size = new System.Drawing.Size(727, 332);
            this.RichTextMsg.TabIndex = 0;
            this.RichTextMsg.Text = "";
            // 
            // LocalCntLabel
            // 
            this.LocalCntLabel.AutoSize = true;
            this.LocalCntLabel.Font = new System.Drawing.Font("新細明體", 15F);
            this.LocalCntLabel.Location = new System.Drawing.Point(36, 50);
            this.LocalCntLabel.Name = "LocalCntLabel";
            this.LocalCntLabel.Size = new System.Drawing.Size(50, 20);
            this.LocalCntLabel.TabIndex = 1;
            this.LocalCntLabel.Text = "Local";
            // 
            // RemoteCntLabel
            // 
            this.RemoteCntLabel.AutoSize = true;
            this.RemoteCntLabel.Font = new System.Drawing.Font("新細明體", 15F);
            this.RemoteCntLabel.Location = new System.Drawing.Point(36, 21);
            this.RemoteCntLabel.Name = "RemoteCntLabel";
            this.RemoteCntLabel.Size = new System.Drawing.Size(67, 20);
            this.RemoteCntLabel.TabIndex = 2;
            this.RemoteCntLabel.Text = "Remote";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.RemoteCntLabel);
            this.Controls.Add(this.LocalCntLabel);
            this.Controls.Add(this.RichTextMsg);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox RichTextMsg;
        private System.Windows.Forms.Label LocalCntLabel;
        private System.Windows.Forms.Label RemoteCntLabel;
    }
}

