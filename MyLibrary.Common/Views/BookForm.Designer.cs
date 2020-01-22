namespace MyLibrary.Common.Views
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
            this.groupBoxBook = new System.Windows.Forms.GroupBox();
            this.numericUpDownPrice = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownPages = new System.Windows.Forms.NumericUpDown();
            this.comboBoxPress = new System.Windows.Forms.ComboBox();
            this.comboBoxAuthor = new System.Windows.Forms.ComboBox();
            this.labelPrice = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelPages = new System.Windows.Forms.Label();
            this.labelPress = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.labelAuthor = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupBoxBook.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPages)).BeginInit();
            this.SuspendLayout();
            //
            // groupBoxBook
            //
            this.groupBoxBook.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxBook.Controls.Add(this.numericUpDownPrice);
            this.groupBoxBook.Controls.Add(this.numericUpDownPages);
            this.groupBoxBook.Controls.Add(this.comboBoxPress);
            this.groupBoxBook.Controls.Add(this.comboBoxAuthor);
            this.groupBoxBook.Controls.Add(this.labelPrice);
            this.groupBoxBook.Controls.Add(this.textBoxName);
            this.groupBoxBook.Controls.Add(this.labelPages);
            this.groupBoxBook.Controls.Add(this.labelPress);
            this.groupBoxBook.Controls.Add(this.labelName);
            this.groupBoxBook.Controls.Add(this.labelAuthor);
            this.groupBoxBook.Location = new System.Drawing.Point(24, 20);
            this.groupBoxBook.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxBook.Name = "groupBoxBook";
            this.groupBoxBook.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxBook.Size = new System.Drawing.Size(637, 231);
            this.groupBoxBook.TabIndex = 0;
            this.groupBoxBook.TabStop = false;
            this.groupBoxBook.Text = "Информация о книге";
            //
            // numericUpDownPrice
            //
            this.numericUpDownPrice.DecimalPlaces = 2;
            this.numericUpDownPrice.Location = new System.Drawing.Point(165, 142);
            this.numericUpDownPrice.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDownPrice.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownPrice.Name = "numericUpDownPrice";
            this.numericUpDownPrice.Size = new System.Drawing.Size(100, 22);
            this.numericUpDownPrice.TabIndex = 7;
            //
            // numericUpDownPages
            //
            this.numericUpDownPages.Location = new System.Drawing.Point(165, 105);
            this.numericUpDownPages.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDownPages.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDownPages.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownPages.Name = "numericUpDownPages";
            this.numericUpDownPages.Size = new System.Drawing.Size(100, 22);
            this.numericUpDownPages.TabIndex = 5;
            this.numericUpDownPages.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            //
            // comboBoxPress
            //
            this.comboBoxPress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxPress.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPress.FormattingEnabled = true;
            this.comboBoxPress.Location = new System.Drawing.Point(165, 181);
            this.comboBoxPress.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxPress.Name = "comboBoxPress";
            this.comboBoxPress.Size = new System.Drawing.Size(437, 24);
            this.comboBoxPress.TabIndex = 9;
            this.comboBoxPress.SelectedIndexChanged += new System.EventHandler(this.PressSelectedHandler);
            //
            // comboBoxAuthor
            //
            this.comboBoxAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxAuthor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAuthor.FormattingEnabled = true;
            this.comboBoxAuthor.Location = new System.Drawing.Point(165, 33);
            this.comboBoxAuthor.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxAuthor.Name = "comboBoxAuthor";
            this.comboBoxAuthor.Size = new System.Drawing.Size(437, 24);
            this.comboBoxAuthor.TabIndex = 1;
            this.comboBoxAuthor.SelectedIndexChanged += new System.EventHandler(this.AuthorSelectedHandler);
            //
            // labelPrice
            //
            this.labelPrice.AutoSize = true;
            this.labelPrice.Location = new System.Drawing.Point(33, 147);
            this.labelPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPrice.Name = "labelPrice";
            this.labelPrice.Size = new System.Drawing.Size(47, 17);
            this.labelPrice.TabIndex = 6;
            this.labelPrice.Text = "Цена:";
            //
            // textBoxName
            //
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.Location = new System.Drawing.Point(165, 69);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(437, 22);
            this.textBoxName.TabIndex = 3;
            this.textBoxName.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            //
            // labelPages
            //
            this.labelPages.AutoSize = true;
            this.labelPages.Location = new System.Drawing.Point(33, 110);
            this.labelPages.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPages.Name = "labelPages";
            this.labelPages.Size = new System.Drawing.Size(78, 17);
            this.labelPages.TabIndex = 4;
            this.labelPages.Text = "Страницы:";
            //
            // labelPress
            //
            this.labelPress.AutoSize = true;
            this.labelPress.Location = new System.Drawing.Point(33, 185);
            this.labelPress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPress.Name = "labelPress";
            this.labelPress.Size = new System.Drawing.Size(104, 17);
            this.labelPress.TabIndex = 8;
            this.labelPress.Text = "Издательство:";
            //
            // labelName
            //
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(33, 74);
            this.labelName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(76, 17);
            this.labelName.TabIndex = 2;
            this.labelName.Text = "Название:";
            //
            // labelAuthor
            //
            this.labelAuthor.AutoSize = true;
            this.labelAuthor.Location = new System.Drawing.Point(33, 37);
            this.labelAuthor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAuthor.Name = "labelAuthor";
            this.labelAuthor.Size = new System.Drawing.Size(51, 17);
            this.labelAuthor.TabIndex = 0;
            this.labelAuthor.Text = "Автор:";
            //
            // buttonCancel
            //
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(558, 269);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(104, 31);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            //
            // buttonOK
            //
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Enabled = false;
            this.buttonOK.Location = new System.Drawing.Point(441, 269);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(104, 31);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            //
            // BookForm
            //
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(687, 318);
            this.Controls.Add(this.groupBoxBook);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BookForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.LoadedHandler);
            this.groupBoxBook.ResumeLayout(false);
            this.groupBoxBook.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPages)).EndInit();
            this.ResumeLayout(false);

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