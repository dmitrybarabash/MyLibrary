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
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.toolStripFileItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripFileExitItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripBooksItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripBooksAddItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripBooksEditItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripBooksDeleteItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripRefItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripRefAuthorsItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripRefPressesItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripHelpItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripHelpAboutItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listView = new System.Windows.Forms.ListView();
            this.columnAuthor = new System.Windows.Forms.ColumnHeader();
            this.columnName = new System.Windows.Forms.ColumnHeader();
            this.columnPages = new System.Windows.Forms.ColumnHeader();
            this.columnPrice = new System.Windows.Forms.ColumnHeader();
            this.columnPress = new System.Windows.Forms.ColumnHeader();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.menuStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStripMain.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.menuStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripFileItem,
            this.toolStripBooksItem,
            this.toolStripRefItem,
            this.toolStripHelpItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStripMain.Size = new System.Drawing.Size(280, 28);
            this.menuStripMain.TabIndex = 4;
            this.menuStripMain.Text = "Главное меню";
            // 
            // toolStripFileItem
            // 
            this.toolStripFileItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripFileExitItem});
            this.toolStripFileItem.Name = "toolStripFileItem";
            this.toolStripFileItem.Size = new System.Drawing.Size(59, 24);
            this.toolStripFileItem.Text = "&Файл";
            // 
            // toolStripFileExitItem
            // 
            this.toolStripFileExitItem.Name = "toolStripFileExitItem";
            this.toolStripFileExitItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.X)));
            this.toolStripFileExitItem.Size = new System.Drawing.Size(183, 26);
            this.toolStripFileExitItem.Text = "В&ыход";
            this.toolStripFileExitItem.Click += new System.EventHandler(this.toolStripFileExitItem_Click);
            // 
            // toolStripBooksItem
            // 
            this.toolStripBooksItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripBooksAddItem,
            this.toolStripBooksEditItem,
            this.toolStripBooksDeleteItem});
            this.toolStripBooksItem.Name = "toolStripBooksItem";
            this.toolStripBooksItem.Size = new System.Drawing.Size(65, 24);
            this.toolStripBooksItem.Text = "&Книги";
            // 
            // toolStripBooksAddItem
            // 
            this.toolStripBooksAddItem.Name = "toolStripBooksAddItem";
            this.toolStripBooksAddItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.toolStripBooksAddItem.Size = new System.Drawing.Size(221, 26);
            this.toolStripBooksAddItem.Text = "&Добавить...";
            this.toolStripBooksAddItem.Click += new System.EventHandler(this.AddBookHandler);
            // 
            // toolStripBooksEditItem
            // 
            this.toolStripBooksEditItem.Name = "toolStripBooksEditItem";
            this.toolStripBooksEditItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.toolStripBooksEditItem.Size = new System.Drawing.Size(221, 26);
            this.toolStripBooksEditItem.Text = "&Изменить...";
            this.toolStripBooksEditItem.Click += new System.EventHandler(this.EditBookHandler);
            // 
            // toolStripBooksDeleteItem
            // 
            this.toolStripBooksDeleteItem.Name = "toolStripBooksDeleteItem";
            this.toolStripBooksDeleteItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.toolStripBooksDeleteItem.Size = new System.Drawing.Size(221, 26);
            this.toolStripBooksDeleteItem.Text = "&Удалить...";
            this.toolStripBooksDeleteItem.Click += new System.EventHandler(this.DeleteBookHandler);
            // 
            // toolStripRefItem
            // 
            this.toolStripRefItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripRefAuthorsItem,
            this.toolStripRefPressesItem});
            this.toolStripRefItem.Name = "toolStripRefItem";
            this.toolStripRefItem.Size = new System.Drawing.Size(117, 24);
            this.toolStripRefItem.Text = "&Справочники";
            // 
            // toolStripRefAuthorsItem
            // 
            this.toolStripRefAuthorsItem.Name = "toolStripRefAuthorsItem";
            this.toolStripRefAuthorsItem.Size = new System.Drawing.Size(185, 26);
            this.toolStripRefAuthorsItem.Text = "&Авторы";
            this.toolStripRefAuthorsItem.Click += new System.EventHandler(this.EditAuthorsHandler);
            // 
            // toolStripRefPressesItem
            // 
            this.toolStripRefPressesItem.Name = "toolStripRefPressesItem";
            this.toolStripRefPressesItem.Size = new System.Drawing.Size(185, 26);
            this.toolStripRefPressesItem.Text = "&Издательства";
            this.toolStripRefPressesItem.Click += new System.EventHandler(this.EditPressesHandler);
            // 
            // toolStripHelpItem
            // 
            this.toolStripHelpItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripHelpAboutItem});
            this.toolStripHelpItem.Name = "toolStripHelpItem";
            this.toolStripHelpItem.Size = new System.Drawing.Size(30, 24);
            this.toolStripHelpItem.Text = "&?";
            // 
            // toolStripHelpAboutItem
            // 
            this.toolStripHelpAboutItem.Name = "toolStripHelpAboutItem";
            this.toolStripHelpAboutItem.Size = new System.Drawing.Size(187, 26);
            this.toolStripHelpAboutItem.Text = "О программе";
            this.toolStripHelpAboutItem.Click += new System.EventHandler(this.toolStripHelpAboutItem_Click);
            // 
            // listView
            // 
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnAuthor,
            this.columnName,
            this.columnPages,
            this.columnPrice,
            this.columnPress});
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(17, 32);
            this.listView.Margin = new System.Windows.Forms.Padding(4);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(942, 585);
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_MouseDoubleClick);
            this.listView.Resize += new System.EventHandler(this.listView_Resize);
            // 
            // columnAuthor
            // 
            this.columnAuthor.Name = "columnAuthor";
            this.columnAuthor.Text = "Автор";
            this.columnAuthor.Width = 200;
            // 
            // columnName
            // 
            this.columnName.Name = "columnName";
            this.columnName.Text = "Название";
            this.columnName.Width = 400;
            // 
            // columnPages
            // 
            this.columnPages.Name = "columnPages";
            this.columnPages.Text = "Страницы";
            this.columnPages.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnPages.Width = 80;
            // 
            // columnPrice
            // 
            this.columnPrice.Name = "columnPrice";
            this.columnPrice.Text = "Цена";
            this.columnPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnPrice.Width = 80;
            // 
            // columnPress
            // 
            this.columnPress.Name = "columnPress";
            this.columnPress.Text = "Издательство";
            this.columnPress.Width = 150;
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDelete.Location = new System.Drawing.Point(974, 105);
            this.buttonDelete.Margin = new System.Windows.Forms.Padding(4);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(109, 28);
            this.buttonDelete.TabIndex = 3;
            this.buttonDelete.Text = "Удалить";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.DeleteBookHandler);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEdit.Location = new System.Drawing.Point(974, 69);
            this.buttonEdit.Margin = new System.Windows.Forms.Padding(4);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(109, 28);
            this.buttonEdit.TabIndex = 2;
            this.buttonEdit.Text = "Изменить";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.EditBookHandler);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.Location = new System.Drawing.Point(974, 33);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(109, 28);
            this.buttonAdd.TabIndex = 1;
            this.buttonAdd.Text = "Добавить";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.AddBookHandler);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1098, 634);
            this.Controls.Add(this.menuStripMain);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.buttonAdd);
            this.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MyLibrary";
            this.Load += new System.EventHandler(this.LoadedHandler);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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

