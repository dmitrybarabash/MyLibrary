using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using MyLibrary.Common.DomainModel;
using MyLibrary.Common.Options;

namespace MyLibrary.Common.DLWithLinqToDataSet.DomainModel
{
    public class Database : IDatabase
    {
        private readonly SqlConnection connection = new SqlConnection();
        private readonly DataSet dataSet = new DataSet("MyLibrary");
        private readonly SqlDataAdapter authorsAdapter;
        private readonly SqlDataAdapter pressesAdapter;
        private readonly SqlDataAdapter booksAdapter;

        public Database()
        {
            connection.ConnectionString = GlobalOptions.ConnectionString;
            connection.Open();

            //
            // Команды адаптера для таблицы Authors
            //

            authorsAdapter = new SqlDataAdapter("SELECT * FROM Authors", connection);

            authorsAdapter.InsertCommand = new SqlCommand("INSERT INTO Authors (Name) VALUES (@Name)", connection);
            authorsAdapter.InsertCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 50, "Name");

            authorsAdapter.UpdateCommand = new SqlCommand("UPDATE Authors SET Name = @Name WHERE Id = @Id", connection);
            authorsAdapter.UpdateCommand.Parameters.Add("@Id", SqlDbType.BigInt, 8, "Id");
            authorsAdapter.UpdateCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 50, "Name");

            authorsAdapter.DeleteCommand = new SqlCommand("DELETE FROM Authors WHERE Id = @Id", connection);
            authorsAdapter.DeleteCommand.Parameters.Add("@Id", SqlDbType.BigInt, 8, "Id");


            //
            // Команды адаптера для таблицы Presses
            //

            pressesAdapter = new SqlDataAdapter("SELECT * FROM Presses", connection);

            pressesAdapter.InsertCommand = new SqlCommand("INSERT INTO Presses (Name) VALUES (@Name)", connection);
            pressesAdapter.InsertCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 50, "Name");

            pressesAdapter.UpdateCommand = new SqlCommand("UPDATE Presses SET Name = @Name WHERE Id = @Id", connection);
            pressesAdapter.UpdateCommand.Parameters.Add("@Id", SqlDbType.BigInt, 8, "Id");
            pressesAdapter.UpdateCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 50, "Name");

            pressesAdapter.DeleteCommand = new SqlCommand("DELETE FROM Presses WHERE Id = @Id", connection);
            pressesAdapter.DeleteCommand.Parameters.Add("@Id", SqlDbType.BigInt, 8, "Id");


            //
            // Команды адаптера для таблицы Books
            //

            booksAdapter = new SqlDataAdapter("SELECT * FROM Books", connection);

            booksAdapter.InsertCommand = new SqlCommand("INSERT INTO Books (AuthorFk, Name, Pages, Price, PressFk) VALUES (@AuthorFk, @Name, @Pages, @Price, @PressFk)", connection);
            booksAdapter.InsertCommand.Parameters.Add("@AuthorFk", SqlDbType.BigInt, 8, "AuthorFk");
            booksAdapter.InsertCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 100, "Name");
            booksAdapter.InsertCommand.Parameters.Add("@Pages", SqlDbType.Int, 4, "Pages");
            booksAdapter.InsertCommand.Parameters.Add("@Price", SqlDbType.Money, 8, "Price");
            booksAdapter.InsertCommand.Parameters.Add("@PressFk", SqlDbType.BigInt, 8, "PressFk");

            booksAdapter.UpdateCommand = new SqlCommand("UPDATE Books SET AuthorFk = @AuthorFk, Name = @Name, Pages = @Pages, Price = @Price, PressFk = @PressFk WHERE Id = @Id", connection);
            booksAdapter.UpdateCommand.Parameters.Add("@Id", SqlDbType.BigInt, 8, "Id");
            booksAdapter.UpdateCommand.Parameters.Add("@AuthorFk", SqlDbType.BigInt, 8, "AuthorFk");
            booksAdapter.UpdateCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 100, "Name");
            booksAdapter.UpdateCommand.Parameters.Add("@Pages", SqlDbType.Int, 4, "Pages");
            booksAdapter.UpdateCommand.Parameters.Add("@Price", SqlDbType.Money, 8, "Price");
            booksAdapter.UpdateCommand.Parameters.Add("@PressFk", SqlDbType.BigInt, 8, "PressFk");

            booksAdapter.DeleteCommand = new SqlCommand("DELETE FROM Books WHERE Id = @Id", connection);
            booksAdapter.DeleteCommand.Parameters.Add("@Id", SqlDbType.BigInt, 8, "Id");


            //
            // Считываем БД в DataSet
            //

            ReloadDataSet();
        }

        private void ReloadAuthors()
        {
            // Сначала очищаем дочернюю Books и только после этого Authors
            if (dataSet.Tables.Contains("Books"))
                dataSet.Tables["Books"].Clear();
            if (dataSet.Tables.Contains("Authors"))
                dataSet.Tables["Authors"].Clear();

            // Пересчитываем Authors, после чего обратно пересчитываем и Books
            authorsAdapter.Fill(dataSet, "Authors");
            booksAdapter.Fill(dataSet, "Books");
        }

        private void ReloadPresses()
        {
            // Сначала очищаем дочернюю Books и только после этого Presses
            if (dataSet.Tables.Contains("Books"))
                dataSet.Tables["Books"].Clear();
            if (dataSet.Tables.Contains("Presses"))
                dataSet.Tables["Presses"].Clear();

            // Пересчитываем Presses, после чего обратно пересчитываем и Books
            pressesAdapter.Fill(dataSet, "Presses");
            booksAdapter.Fill(dataSet, "Books");
        }

        private void ReloadBooks()
        {
            // Очищаем Books
            if (dataSet.Tables.Contains("Books"))
                dataSet.Tables["Books"].Clear();

            // Пересчитываем Books
            booksAdapter.Fill(dataSet, "Books");
        }

        private void ReloadDataSet()
        {
            dataSet.Clear();

            // Сначала очищаем дочернюю Books и только после этого Authors и Presses
            if (dataSet.Tables.Contains("Books"))
                dataSet.Tables["Books"].Clear();
            if (dataSet.Tables.Contains("Authors"))
                dataSet.Tables["Authors"].Clear();
            if (dataSet.Tables.Contains("Presses"))
                dataSet.Tables["Presses"].Clear();

            // Пересчитываем Authors и Presses, после чего обратно пересчитываем и Books
            authorsAdapter.Fill(dataSet, "Authors");
            pressesAdapter.Fill(dataSet, "Presses");
            booksAdapter.Fill(dataSet, "Books");
        }

        #region IDisposable implementation

        public void Dispose()
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }

        #endregion

        #region IDatabase implementation

        //
        // Authors management
        //

        public IEnumerable<Author> GetAllAuthors()
        {
            // Синтаксис методов
            //return
            //    dataSet.Tables["Authors"]
            //        .AsEnumerable()
            //        .Select(row => new Author
            //        {
            //            Id = Convert.ToInt64(row["Id"]),
            //            Name = row.Field<string>("Name")
            //        })
            //        .ToList();

            // Синтаксис запросов
            var authors =
                from row in dataSet.Tables["Authors"].AsEnumerable()
                select new Author
                {
                    Id = Convert.ToInt64(row["Id"]),
                    Name = row.Field<string>("Name")
                };
            return authors.ToList();
        }

        public Author GetAuthorById(long id)
        {
            // Синтаксис методов
            //var dataRow = dataSet.Tables["Authors"]
            //    .AsEnumerable()
            //    .Where(row => Convert.ToInt64(row["Id"]) == id)
            //    .Select(row => row)
            //    .SingleOrDefault();

            // Синтаксис запросов
            var dataRow =
                (from row in dataSet.Tables["Authors"].AsEnumerable()
                 where Convert.ToInt64(row["Id"]) == id
                 select row)
                .SingleOrDefault();

            return
                dataRow is null ?
                null :              // Если не нашли
                new Author          // Если нашли
                {
                    Id = Convert.ToInt64(dataRow["Id"]),
                    Name = dataRow.Field<string>("Name")
                };
        }

        public long InsertAuthor(Author author)
        {
            DataTable authorsTable = dataSet.Tables["Authors"];
            DataRow newRow = authorsTable.NewRow();
            newRow["Name"] = author.Name;
            authorsTable.Rows.Add(newRow);
            authorsAdapter.Update(authorsTable);

            // Переменная для автоматически сгенерированного значения автоинкремента
            // только что добавленной записи
            long id;
            // Получаем последнее выданное БД значение автоинкремента
            string sqlGetIdString = "SELECT @@IDENTITY";
            using (var commandId = new SqlCommand(sqlGetIdString, connection))
            {
                object obj = commandId.ExecuteScalar();
                id = Convert.ToInt64(obj);
            }

            // Пересчитываем таблицу, чтобы этот автоинкрементный ключ появился и в DataSet'е в памяти
            ReloadAuthors();

            return id;
        }

        public void UpdateAuthor(Author author)
        {
            DataTable authorsTable = dataSet.Tables["Authors"];
            foreach (DataRow row in authorsTable.Rows)
                if (Convert.ToInt64(row["Id"]) == author.Id)
                    row["Name"] = author.Name;
            authorsAdapter.Update(authorsTable);
        }

        public void DeleteAuthor(long id)
        {
            DataTable authorsTable = dataSet.Tables["Authors"];

            // Вариант с каскадным удалением из Books в памяти без ее пересчитывания
            //foreach (DataRow row in authorsTable.Rows)
            //{
            //    if (Convert.ToInt64(row["Id"]) == id)
            //    {
            //        // Сначала удаляем книги этого автора
            //        DataTable booksTable = dataSet.Tables["Books"];
            //        foreach (DataRow bookRow in booksTable.Rows)
            //        {
            //            if (Convert.ToInt64(bookRow["AuthorFk"]) == id)
            //                bookRow.Delete();
            //        }
            //        booksAdapter.Update(booksTable);

            //        // А потом уже самого автора
            //        row.Delete();
            //    }
            //}
            //authorsAdapter.Update(authorsTable);

            // Вариант с каскадным удалением из Books в БД и пересчитыванием ее в память
            foreach (DataRow row in authorsTable.Rows)
                if (Convert.ToInt64(row["Id"]) == id)
                    row.Delete();
            authorsAdapter.Update(authorsTable);

            // Поскольку в БД сработало каскадное удаление и из таблицы Books
            // также были удалены книги удаленного автора, то пересчитываем ее
            ReloadBooks();
        }


        //
        // Presses management
        //

        public IEnumerable<Press> GetAllPresses()
        {
            // Синтаксис методов
            //return
            //    dataSet.Tables["Presses"]
            //        .AsEnumerable()
            //        .Select(row => new Press
            //        {
            //            Id = Convert.ToInt64(row["Id"]),
            //            Name = row.Field<string>("Name")
            //        })
            //        .ToList();

            // Синтаксис запросов
            var presses =
                from row in dataSet.Tables["Presses"].AsEnumerable()
                select new Press
                {
                    Id = Convert.ToInt64(row["Id"]),
                    Name = row.Field<string>("Name")
                };
            return presses.ToList();
        }

        public Press GetPressById(long id)
        {
            // Синтаксис методов
            //var dataRow = dataSet.Tables["Presses"]
            //    .AsEnumerable()
            //    .Where(row => Convert.ToInt64(row["Id"]) == id)
            //    .Select(row => row)
            //    .SingleOrDefault();

            // Синтаксис запросов
            var dataRow =
                (from row in dataSet.Tables["Presses"].AsEnumerable()
                 where Convert.ToInt64(row["Id"]) == id
                 select row)
                .SingleOrDefault();

            return dataRow is null ?
                null :              // Если не нашли
                new Press           // Если нашли
                {
                    Id = Convert.ToInt64(dataRow["Id"]),
                    Name = dataRow.Field<string>("Name")
                };
        }

        public long InsertPress(Press press)
        {
            DataTable pressesTable = dataSet.Tables["Presses"];
            DataRow newRow = pressesTable.NewRow();
            newRow["Name"] = press.Name;
            pressesTable.Rows.Add(newRow);
            pressesAdapter.Update(pressesTable);

            // Переменная для автоматически сгенерированного значения автоинкремента
            // только что добавленной записи
            long id;
            // Получаем последнее выданное БД значение автоинкремента
            string sqlGetIdString = "SELECT @@IDENTITY";
            using (var commandId = new SqlCommand(sqlGetIdString, connection))
            {
                object obj = commandId.ExecuteScalar();
                id = Convert.ToInt64(obj);
            }

            // Пересчитываем таблицу, чтобы этот автоинкрементный ключ появился и в DataSet'е в памяти
            ReloadPresses();

            return id;
        }

        public void UpdatePress(Press press)
        {
            DataTable pressesTable = dataSet.Tables["Presses"];
            foreach (DataRow row in pressesTable.Rows)
                if (Convert.ToInt64(row["Id"]) == press.Id)
                    row["Name"] = press.Name;
            pressesAdapter.Update(pressesTable);
        }

        public void DeletePress(long id)
        {
            DataTable pressesTable = dataSet.Tables["Presses"];

            // Вариант с каскадным удалением из Books в памяти без ее пересчитывания
            //foreach (DataRow row in pressesTable.Rows)
            //{
            //    if (Convert.ToInt64(row["Id"]) == id)
            //    {
            //        // Сначала удаляем книги этого издательства
            //        DataTable booksTable = dataSet.Tables["Books"];
            //        foreach (DataRow bookRow in booksTable.Rows)
            //        {
            //            if (Convert.ToInt64(bookRow["PressFk"]) == id)
            //                bookRow.Delete();
            //        }
            //        booksAdapter.Update(booksTable);

            //        // А потом уже само издательство
            //        row.Delete();
            //    }
            //}
            //pressesAdapter.Update(pressesTable);

            // Вариант с каскадным удалением из Books в БД и пересчитыванием ее в память
            foreach (DataRow row in pressesTable.Rows)
                if (Convert.ToInt64(row["Id"]) == id)
                    row.Delete();
            pressesAdapter.Update(pressesTable);

            // Поскольку в БД сработало каскадное удаление и из таблицы Books
            // также были удалены книги удаленного автора, то пересчитываем ее
            ReloadBooks();
        }


        //
        // Books management
        //

        public IEnumerable<Book> GetAllBooks()
        {
            // Синтаксис методов
            //return
            //    dataSet.Tables["Books"]
            //        .AsEnumerable()
            //        .Select(row => new Book
            //        {
            //            Id = Convert.ToInt64(row["Id"]),
            //            AuthorFk = Convert.ToInt64(row["AuthorFk"]),
            //            Name = row.Field<string>("Name"),
            //            Pages = row.Field<int>("Pages"),
            //            Price = row.Field<decimal>("Price"),
            //            PressFk = Convert.ToInt64(row["PressFk"])
            //        })
            //        .ToList();

            // Синтаксис запросов
            var books =
                from row in dataSet.Tables["Books"].AsEnumerable()
                select new Book
                {
                    Id = Convert.ToInt64(row["Id"]),
                    AuthorFk = Convert.ToInt64(row["AuthorFk"]),
                    Name = row.Field<string>("Name"),
                    Pages = row.Field<int>("Pages"),
                    Price = row.Field<decimal>("Price"),
                    PressFk = Convert.ToInt64(row["PressFk"])
                };
            return books.ToList();
        }

        public Book GetBookById(long id)
        {
            // Синтаксис методов
            //var dataRow = dataSet.Tables["Books"]
            //    .AsEnumerable()
            //    .Where(row => Convert.ToInt64(row["Id"]) == id)
            //    .Select(row => row)
            //    .SingleOrDefault();

            // Синтаксис запросов
            var dataRow =
                (from row in dataSet.Tables["Books"].AsEnumerable()
                 where Convert.ToInt64(row["Id"]) == id
                 select row)
                .SingleOrDefault();

            return dataRow is null ?
                null :              // Если не нашли
                new Book            // Если нашли
                {
                    Id = Convert.ToInt64(dataRow["Id"]),
                    AuthorFk = Convert.ToInt64(dataRow["AuthorFk"]),
                    Name = dataRow.Field<string>("Name"),
                    Pages = dataRow.Field<int>("Pages"),
                    Price = dataRow.Field<decimal>("Price"),
                    PressFk = Convert.ToInt64(dataRow["PressFk"])
                };
        }

        public long InsertBook(Book book)
        {
            DataTable booksTable = dataSet.Tables["Books"];
            DataRow newRow = booksTable.NewRow();
            newRow["Name"] = book.Name;
            newRow["AuthorFk"] = book.AuthorFk;
            newRow["Name"] = book.Name;
            newRow["Pages"] = book.Pages;
            newRow["Price"] = book.Price;
            newRow["PressFk"] = book.PressFk;
            booksTable.Rows.Add(newRow);
            booksAdapter.Update(booksTable);

            // Переменная для автоматически сгенерированного значения автоинкремента
            // только что добавленной записи
            long id;
            // Получаем последнее выданное БД значение автоинкремента
            string sqlGetIdString = "SELECT @@IDENTITY";
            using (var commandId = new SqlCommand(sqlGetIdString, connection))
            {
                object obj = commandId.ExecuteScalar();
                id = Convert.ToInt64(obj);
            }

            // Пересчитываем таблицу, чтобы этот автоинкрементный ключ появился и в DataSet'е в памяти
            ReloadBooks();

            return id;
        }

        public void UpdateBook(Book book)
        {
            DataTable booksTable = dataSet.Tables["Books"];
            foreach (DataRow row in booksTable.Rows)
            {
                if (Convert.ToInt64(row["Id"]) == book.Id)
                {
                    row["Name"] = book.Name;
                    row["AuthorFk"] = book.AuthorFk;
                    row["Name"] = book.Name;
                    row["Pages"] = book.Pages;
                    row["Price"] = book.Price;
                    row["PressFk"] = book.PressFk;
                }
            }
            booksAdapter.Update(booksTable);
        }

        public void DeleteBook(long id)
        {
            DataTable booksTable = dataSet.Tables["Books"];
            foreach (DataRow row in booksTable.Rows)
                if (Convert.ToInt64(row["Id"]) == id)
                    row.Delete();
            booksAdapter.Update(booksTable);
        }

        #endregion
    }
}