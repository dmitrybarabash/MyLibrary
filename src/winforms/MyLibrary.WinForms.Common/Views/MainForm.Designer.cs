namespace MyLibrary.WinForms.Common.Views
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            menuStripMain = new System.Windows.Forms.MenuStrip();
            toolStripFileItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripFileExitItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripBooksItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripBooksAddItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripBooksEditItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripBooksDeleteItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripRefItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripRefAuthorsItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripRefPressesItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripHelpItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripHelpAboutItem = new System.Windows.Forms.ToolStripMenuItem();
            listView = new System.Windows.Forms.ListView();
            columnAuthor = new System.Windows.Forms.ColumnHeader();
            columnName = new System.Windows.Forms.ColumnHeader();
            columnPages = new System.Windows.Forms.ColumnHeader();
            columnPrice = new System.Windows.Forms.ColumnHeader();
            columnPress = new System.Windows.Forms.ColumnHeader();
            buttonDelete = new System.Windows.Forms.Button();
            buttonEdit = new System.Windows.Forms.Button();
            buttonAdd = new System.Windows.Forms.Button();
            menuStripMain.SuspendLayout();
            SuspendLayout();
            // 
            // menuStripMain
            // 
            menuStripMain.Dock = System.Windows.Forms.DockStyle.None;
            menuStripMain.Font = new System.Drawing.Font("Segoe UI", 9F);
            menuStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripFileItem, toolStripBooksItem, toolStripRefItem, toolStripHelpItem });
            menuStripMain.Location = new System.Drawing.Point(0, 0);
            menuStripMain.Name = "menuStripMain";
            menuStripMain.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            menuStripMain.Size = new System.Drawing.Size(280, 28);
            menuStripMain.TabIndex = 4;
            menuStripMain.Text = "Главное меню";
            // 
            // toolStripFileItem
            // 
            toolStripFileItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripFileExitItem });
            toolStripFileItem.Name = "toolStripFileItem";
            toolStripFileItem.Size = new System.Drawing.Size(59, 24);
            toolStripFileItem.Text = "&Файл";
            // 
            // toolStripFileExitItem
            // 
            toolStripFileExitItem.Name = "toolStripFileExitItem";
            toolStripFileExitItem.ShortcutKeys = System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.X;
            toolStripFileExitItem.Size = new System.Drawing.Size(183, 26);
            toolStripFileExitItem.Text = "В&ыход";
            toolStripFileExitItem.Click += toolStripFileExitItem_Click;
            // 
            // toolStripBooksItem
            // 
            toolStripBooksItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripBooksAddItem, toolStripBooksEditItem, toolStripBooksDeleteItem });
            toolStripBooksItem.Name = "toolStripBooksItem";
            toolStripBooksItem.Size = new System.Drawing.Size(65, 24);
            toolStripBooksItem.Text = "&Книги";
            // 
            // toolStripBooksAddItem
            // 
            toolStripBooksAddItem.Name = "toolStripBooksAddItem";
            toolStripBooksAddItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N;
            toolStripBooksAddItem.Size = new System.Drawing.Size(221, 26);
            toolStripBooksAddItem.Text = "&Добавить...";
            toolStripBooksAddItem.Click += AddBookHandler;
            // 
            // toolStripBooksEditItem
            // 
            toolStripBooksEditItem.Name = "toolStripBooksEditItem";
            toolStripBooksEditItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E;
            toolStripBooksEditItem.Size = new System.Drawing.Size(221, 26);
            toolStripBooksEditItem.Text = "&Изменить...";
            toolStripBooksEditItem.Click += EditBookHandler;
            // 
            // toolStripBooksDeleteItem
            // 
            toolStripBooksDeleteItem.Name = "toolStripBooksDeleteItem";
            toolStripBooksDeleteItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D;
            toolStripBooksDeleteItem.Size = new System.Drawing.Size(221, 26);
            toolStripBooksDeleteItem.Text = "&Удалить...";
            toolStripBooksDeleteItem.Click += DeleteBookHandler;
            // 
            // toolStripRefItem
            // 
            toolStripRefItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripRefAuthorsItem, toolStripRefPressesItem });
            toolStripRefItem.Name = "toolStripRefItem";
            toolStripRefItem.Size = new System.Drawing.Size(117, 24);
            toolStripRefItem.Text = "&Справочники";
            // 
            // toolStripRefAuthorsItem
            // 
            toolStripRefAuthorsItem.Name = "toolStripRefAuthorsItem";
            toolStripRefAuthorsItem.Size = new System.Drawing.Size(185, 26);
            toolStripRefAuthorsItem.Text = "&Авторы";
            toolStripRefAuthorsItem.Click += EditAuthorsHandler;
            // 
            // toolStripRefPressesItem
            // 
            toolStripRefPressesItem.Name = "toolStripRefPressesItem";
            toolStripRefPressesItem.Size = new System.Drawing.Size(185, 26);
            toolStripRefPressesItem.Text = "&Издательства";
            toolStripRefPressesItem.Click += EditPressesHandler;
            // 
            // toolStripHelpItem
            // 
            toolStripHelpItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripHelpAboutItem });
            toolStripHelpItem.Name = "toolStripHelpItem";
            toolStripHelpItem.Size = new System.Drawing.Size(30, 24);
            toolStripHelpItem.Text = "&?";
            // 
            // toolStripHelpAboutItem
            // 
            toolStripHelpAboutItem.Name = "toolStripHelpAboutItem";
            toolStripHelpAboutItem.Size = new System.Drawing.Size(187, 26);
            toolStripHelpAboutItem.Text = "О программе";
            toolStripHelpAboutItem.Click += toolStripHelpAboutItem_Click;
            // 
            // listView
            // 
            listView.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { columnAuthor, columnName, columnPages, columnPrice, columnPress });
            listView.FullRowSelect = true;
            listView.GridLines = true;
            listView.Location = new System.Drawing.Point(17, 32);
            listView.Margin = new System.Windows.Forms.Padding(4);
            listView.Name = "listView";
            listView.Size = new System.Drawing.Size(942, 585);
            listView.TabIndex = 0;
            listView.UseCompatibleStateImageBehavior = false;
            listView.View = System.Windows.Forms.View.Details;
            listView.MouseDoubleClick += listView_MouseDoubleClick;
            listView.Resize += listView_Resize;
            // 
            // columnAuthor
            // 
            columnAuthor.Name = "columnAuthor";
            columnAuthor.Text = "Автор";
            columnAuthor.Width = 200;
            // 
            // columnName
            // 
            columnName.Name = "columnName";
            columnName.Text = "Название";
            columnName.Width = 400;
            // 
            // columnPages
            // 
            columnPages.Name = "columnPages";
            columnPages.Text = "Страницы";
            columnPages.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            columnPages.Width = 80;
            // 
            // columnPrice
            // 
            columnPrice.Name = "columnPrice";
            columnPrice.Text = "Цена";
            columnPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            columnPrice.Width = 80;
            // 
            // columnPress
            // 
            columnPress.Name = "columnPress";
            columnPress.Text = "Издательство";
            columnPress.Width = 150;
            // 
            // buttonDelete
            // 
            buttonDelete.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            buttonDelete.Location = new System.Drawing.Point(974, 105);
            buttonDelete.Margin = new System.Windows.Forms.Padding(4);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new System.Drawing.Size(109, 28);
            buttonDelete.TabIndex = 3;
            buttonDelete.Text = "Удалить";
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += DeleteBookHandler;
            // 
            // buttonEdit
            // 
            buttonEdit.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            buttonEdit.Location = new System.Drawing.Point(974, 69);
            buttonEdit.Margin = new System.Windows.Forms.Padding(4);
            buttonEdit.Name = "buttonEdit";
            buttonEdit.Size = new System.Drawing.Size(109, 28);
            buttonEdit.TabIndex = 2;
            buttonEdit.Text = "Изменить";
            buttonEdit.UseVisualStyleBackColor = true;
            buttonEdit.Click += EditBookHandler;
            // 
            // buttonAdd
            // 
            buttonAdd.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            buttonAdd.Location = new System.Drawing.Point(974, 33);
            buttonAdd.Margin = new System.Windows.Forms.Padding(4);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new System.Drawing.Size(109, 28);
            buttonAdd.TabIndex = 1;
            buttonAdd.Text = "Добавить";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += AddBookHandler;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1098, 634);
            Controls.Add(menuStripMain);
            Controls.Add(listView);
            Controls.Add(buttonDelete);
            Controls.Add(buttonEdit);
            Controls.Add(buttonAdd);
            Font = new System.Drawing.Font("Tahoma", 8F);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStripMain;
            Name = "MainForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "MyLibrary";
            Load += LoadedHandler;
            menuStripMain.ResumeLayout(false);
            menuStripMain.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem toolStripFileItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripFileExitItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripHelpItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripHelpAboutItem;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader columnAuthor;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.ColumnHeader columnPages;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.ColumnHeader columnPrice;
        private System.Windows.Forms.ColumnHeader columnPress;
        private System.Windows.Forms.ToolStripMenuItem toolStripRefItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripRefAuthorsItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripRefPressesItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripBooksItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripBooksAddItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripBooksEditItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripBooksDeleteItem;
    }
}

