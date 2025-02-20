namespace MyLibrary.WinForms.Common.Views
{
    partial class RefItemForm
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
            groupBoxItem = new System.Windows.Forms.GroupBox();
            textBoxItem = new System.Windows.Forms.TextBox();
            labelItem = new System.Windows.Forms.Label();
            buttonOk = new System.Windows.Forms.Button();
            buttonCancel = new System.Windows.Forms.Button();
            groupBoxItem.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxItem
            // 
            groupBoxItem.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            groupBoxItem.Controls.Add(textBoxItem);
            groupBoxItem.Controls.Add(labelItem);
            groupBoxItem.Location = new System.Drawing.Point(22, 18);
            groupBoxItem.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            groupBoxItem.Name = "groupBoxItem";
            groupBoxItem.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            groupBoxItem.Size = new System.Drawing.Size(519, 108);
            groupBoxItem.TabIndex = 0;
            groupBoxItem.TabStop = false;
            // 
            // textBoxItem
            // 
            textBoxItem.Location = new System.Drawing.Point(133, 45);
            textBoxItem.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            textBoxItem.Name = "textBoxItem";
            textBoxItem.Size = new System.Drawing.Size(358, 27);
            textBoxItem.TabIndex = 1;
            textBoxItem.TextChanged += textBoxItem_TextChanged;
            // 
            // labelItem
            // 
            labelItem.AutoSize = true;
            labelItem.Location = new System.Drawing.Point(19, 48);
            labelItem.Name = "labelItem";
            labelItem.Size = new System.Drawing.Size(54, 20);
            labelItem.TabIndex = 0;
            labelItem.Text = "Автор:";
            // 
            // buttonOk
            // 
            buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            buttonOk.Enabled = false;
            buttonOk.Location = new System.Drawing.Point(311, 151);
            buttonOk.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new System.Drawing.Size(112, 40);
            buttonOk.TabIndex = 1;
            buttonOk.Text = "OK";
            buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            buttonCancel.Location = new System.Drawing.Point(429, 151);
            buttonCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new System.Drawing.Size(112, 40);
            buttonCancel.TabIndex = 2;
            buttonCancel.Text = "Отмена";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // RefItemForm
            // 
            AcceptButton = buttonOk;
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = buttonCancel;
            ClientSize = new System.Drawing.Size(564, 215);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOk);
            Controls.Add(groupBoxItem);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RefItemForm";
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            groupBoxItem.ResumeLayout(false);
            groupBoxItem.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxItem;
        private System.Windows.Forms.TextBox textBoxItem;
        private System.Windows.Forms.Label labelItem;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
    }
}