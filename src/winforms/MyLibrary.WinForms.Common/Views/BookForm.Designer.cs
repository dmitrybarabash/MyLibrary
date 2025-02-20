namespace MyLibrary.WinForms.Common.Views
{
    partial class BookForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BookForm));
            groupBoxBook = new System.Windows.Forms.GroupBox();
            numericUpDownPrice = new System.Windows.Forms.NumericUpDown();
            numericUpDownPages = new System.Windows.Forms.NumericUpDown();
            comboBoxPress = new System.Windows.Forms.ComboBox();
            comboBoxAuthor = new System.Windows.Forms.ComboBox();
            labelPrice = new System.Windows.Forms.Label();
            textBoxName = new System.Windows.Forms.TextBox();
            labelPages = new System.Windows.Forms.Label();
            labelPress = new System.Windows.Forms.Label();
            labelName = new System.Windows.Forms.Label();
            labelAuthor = new System.Windows.Forms.Label();
            buttonCancel = new System.Windows.Forms.Button();
            buttonOK = new System.Windows.Forms.Button();
            groupBoxBook.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownPrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownPages).BeginInit();
            SuspendLayout();
            // 
            // groupBoxBook
            // 
            groupBoxBook.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            groupBoxBook.Controls.Add(numericUpDownPrice);
            groupBoxBook.Controls.Add(numericUpDownPages);
            groupBoxBook.Controls.Add(comboBoxPress);
            groupBoxBook.Controls.Add(comboBoxAuthor);
            groupBoxBook.Controls.Add(labelPrice);
            groupBoxBook.Controls.Add(textBoxName);
            groupBoxBook.Controls.Add(labelPages);
            groupBoxBook.Controls.Add(labelPress);
            groupBoxBook.Controls.Add(labelName);
            groupBoxBook.Controls.Add(labelAuthor);
            groupBoxBook.Location = new System.Drawing.Point(24, 25);
            groupBoxBook.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            groupBoxBook.Name = "groupBoxBook";
            groupBoxBook.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            groupBoxBook.Size = new System.Drawing.Size(637, 289);
            groupBoxBook.TabIndex = 0;
            groupBoxBook.TabStop = false;
            groupBoxBook.Text = "Информация о книге";
            // 
            // numericUpDownPrice
            // 
            numericUpDownPrice.DecimalPlaces = 2;
            numericUpDownPrice.Location = new System.Drawing.Point(165, 178);
            numericUpDownPrice.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            numericUpDownPrice.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericUpDownPrice.Name = "numericUpDownPrice";
            numericUpDownPrice.Size = new System.Drawing.Size(100, 27);
            numericUpDownPrice.TabIndex = 7;
            // 
            // numericUpDownPages
            // 
            numericUpDownPages.Location = new System.Drawing.Point(165, 131);
            numericUpDownPages.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            numericUpDownPages.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
            numericUpDownPages.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownPages.Name = "numericUpDownPages";
            numericUpDownPages.Size = new System.Drawing.Size(100, 27);
            numericUpDownPages.TabIndex = 5;
            numericUpDownPages.Value = new decimal(new int[] { 300, 0, 0, 0 });
            // 
            // comboBoxPress
            // 
            comboBoxPress.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBoxPress.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxPress.FormattingEnabled = true;
            comboBoxPress.Location = new System.Drawing.Point(165, 226);
            comboBoxPress.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            comboBoxPress.Name = "comboBoxPress";
            comboBoxPress.Size = new System.Drawing.Size(437, 28);
            comboBoxPress.TabIndex = 9;
            comboBoxPress.SelectedIndexChanged += PressSelectedHandler;
            // 
            // comboBoxAuthor
            // 
            comboBoxAuthor.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBoxAuthor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxAuthor.FormattingEnabled = true;
            comboBoxAuthor.Location = new System.Drawing.Point(165, 41);
            comboBoxAuthor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            comboBoxAuthor.Name = "comboBoxAuthor";
            comboBoxAuthor.Size = new System.Drawing.Size(437, 28);
            comboBoxAuthor.TabIndex = 1;
            comboBoxAuthor.SelectedIndexChanged += AuthorSelectedHandler;
            // 
            // labelPrice
            // 
            labelPrice.AutoSize = true;
            labelPrice.Location = new System.Drawing.Point(33, 184);
            labelPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelPrice.Name = "labelPrice";
            labelPrice.Size = new System.Drawing.Size(48, 20);
            labelPrice.TabIndex = 6;
            labelPrice.Text = "Цена:";
            // 
            // textBoxName
            // 
            textBoxName.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBoxName.Location = new System.Drawing.Point(165, 86);
            textBoxName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new System.Drawing.Size(437, 27);
            textBoxName.TabIndex = 3;
            textBoxName.TextChanged += textBoxName_TextChanged;
            // 
            // labelPages
            // 
            labelPages.AutoSize = true;
            labelPages.Location = new System.Drawing.Point(33, 138);
            labelPages.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelPages.Name = "labelPages";
            labelPages.Size = new System.Drawing.Size(82, 20);
            labelPages.TabIndex = 4;
            labelPages.Text = "Страницы:";
            // 
            // labelPress
            // 
            labelPress.AutoSize = true;
            labelPress.Location = new System.Drawing.Point(33, 231);
            labelPress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelPress.Name = "labelPress";
            labelPress.Size = new System.Drawing.Size(106, 20);
            labelPress.TabIndex = 8;
            labelPress.Text = "Издательство:";
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Location = new System.Drawing.Point(33, 92);
            labelName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelName.Name = "labelName";
            labelName.Size = new System.Drawing.Size(80, 20);
            labelName.TabIndex = 2;
            labelName.Text = "Название:";
            // 
            // labelAuthor
            // 
            labelAuthor.AutoSize = true;
            labelAuthor.Location = new System.Drawing.Point(33, 46);
            labelAuthor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelAuthor.Name = "labelAuthor";
            labelAuthor.Size = new System.Drawing.Size(54, 20);
            labelAuthor.TabIndex = 0;
            labelAuthor.Text = "Автор:";
            // 
            // buttonCancel
            // 
            buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            buttonCancel.Location = new System.Drawing.Point(558, 336);
            buttonCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new System.Drawing.Size(104, 39);
            buttonCancel.TabIndex = 2;
            buttonCancel.Text = "Отмена";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            buttonOK.Enabled = false;
            buttonOK.Location = new System.Drawing.Point(441, 336);
            buttonOK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new System.Drawing.Size(104, 39);
            buttonOK.TabIndex = 1;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = true;
            // 
            // BookForm
            // 
            AcceptButton = buttonOK;
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = buttonCancel;
            ClientSize = new System.Drawing.Size(687, 398);
            Controls.Add(groupBoxBook);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOK);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "BookForm";
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Load += LoadedHandler;
            groupBoxBook.ResumeLayout(false);
            groupBoxBook.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownPrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownPages).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxBook;
        private System.Windows.Forms.NumericUpDown numericUpDownPages;
        private System.Windows.Forms.ComboBox comboBoxAuthor;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelPages;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelAuthor;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.NumericUpDown numericUpDownPrice;
        private System.Windows.Forms.ComboBox comboBoxPress;
        private System.Windows.Forms.Label labelPrice;
        private System.Windows.Forms.Label labelPress;
    }
}