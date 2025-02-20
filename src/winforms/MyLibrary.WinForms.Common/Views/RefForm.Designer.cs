namespace MyLibrary.WinForms.Common.Views
{
    partial class RefForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RefForm));
            listView = new System.Windows.Forms.ListView();
            columnName = new System.Windows.Forms.ColumnHeader();
            buttonDelete = new System.Windows.Forms.Button();
            buttonEdit = new System.Windows.Forms.Button();
            buttonAdd = new System.Windows.Forms.Button();
            buttonClose = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // listView
            // 
            listView.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { columnName });
            listView.FullRowSelect = true;
            listView.GridLines = true;
            listView.Location = new System.Drawing.Point(19, 23);
            listView.Margin = new System.Windows.Forms.Padding(4);
            listView.Name = "listView";
            listView.Size = new System.Drawing.Size(334, 489);
            listView.TabIndex = 0;
            listView.UseCompatibleStateImageBehavior = false;
            listView.View = System.Windows.Forms.View.Details;
            listView.MouseDoubleClick += listView_MouseDoubleClick;
            // 
            // columnName
            // 
            columnName.Text = "";
            columnName.Width = 300;
            // 
            // buttonDelete
            // 
            buttonDelete.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            buttonDelete.Location = new System.Drawing.Point(373, 95);
            buttonDelete.Margin = new System.Windows.Forms.Padding(4);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new System.Drawing.Size(95, 28);
            buttonDelete.TabIndex = 3;
            buttonDelete.Text = "Удалить";
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += DeleteHandler;
            // 
            // buttonEdit
            // 
            buttonEdit.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            buttonEdit.Location = new System.Drawing.Point(373, 59);
            buttonEdit.Margin = new System.Windows.Forms.Padding(4);
            buttonEdit.Name = "buttonEdit";
            buttonEdit.Size = new System.Drawing.Size(95, 28);
            buttonEdit.TabIndex = 2;
            buttonEdit.Text = "Изменить";
            buttonEdit.UseVisualStyleBackColor = true;
            buttonEdit.Click += EditHandler;
            // 
            // buttonAdd
            // 
            buttonAdd.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            buttonAdd.Location = new System.Drawing.Point(373, 23);
            buttonAdd.Margin = new System.Windows.Forms.Padding(4);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new System.Drawing.Size(95, 28);
            buttonAdd.TabIndex = 1;
            buttonAdd.Text = "Добавить";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += AddHandler;
            // 
            // buttonClose
            // 
            buttonClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            buttonClose.Location = new System.Drawing.Point(373, 484);
            buttonClose.Margin = new System.Windows.Forms.Padding(4);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new System.Drawing.Size(95, 28);
            buttonClose.TabIndex = 4;
            buttonClose.Text = "Закрыть";
            buttonClose.UseVisualStyleBackColor = true;
            buttonClose.Click += buttonClose_Click;
            // 
            // RefForm
            // 
            AcceptButton = buttonClose;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = buttonClose;
            ClientSize = new System.Drawing.Size(488, 534);
            Controls.Add(buttonClose);
            Controls.Add(listView);
            Controls.Add(buttonDelete);
            Controls.Add(buttonEdit);
            Controls.Add(buttonAdd);
            Font = new System.Drawing.Font("Tahoma", 8F);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RefForm";
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Load += LoadedHandler;
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonClose;
    }
}