using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using MyLibrary.Common.DomainModel;
using MyLibrary.Common.Options;

namespace MyLibrary.Common.DLWithLinqToDataSet.DomainModel;

public class Database : IDatabase
{
    private readonly SqlConnection _connection = new();
    private readonly DataSet _dataSet = new("MyLibrary");
    private readonly SqlDataAdapter _authorsAdapter;
    private readonly SqlDataAdapter _pressesAdapter;
    private readonly SqlDataAdapter _booksAdapter;

    public Database()
    {
        _connection.ConnectionString = GlobalOptions.ConnectionString;
        _connection.Open();

        //
        // Команды адаптера для таблицы Authors
        //

        _authorsAdapter = new SqlDataAdapter("SELECT * FROM Authors", _connection);

        _authorsAdapter.InsertCommand = new SqlCommand("INSERT INTO Authors (Name) VALUES (@Name)", _connection);
        _authorsAdapter.InsertCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 50, "Name");

        _authorsAdapter.UpdateCommand = new SqlCommand("UPDATE Authors SET Name = @Name WHERE Id = @Id", _connection);
        _authorsAdapter.UpdateCommand.Parameters.Add("@Id", SqlDbType.BigInt, 8, "Id");
        _authorsAdapter.UpdateCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 50, "Name");

        _authorsAdapter.DeleteCommand = new SqlCommand("DELETE FROM Authors WHERE Id = @Id", _connection);
        _authorsAdapter.DeleteCommand.Parameters.Add("@Id", SqlDbType.BigInt, 8, "Id");


        //
        // Команды адаптера для таблицы Presses
        //

        _pressesAdapter = new SqlDataAdapter("SELECT * FROM Presses", _connection);

        _pressesAdapter.InsertCommand = new SqlCommand("INSERT INTO Presses (Name) VALUES (@Name)", _connection);
        _pressesAdapter.InsertCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 50, "Name");

        _pressesAdapter.UpdateCommand = new SqlCommand("UPDATE Presses SET Name = @Name WHERE Id = @Id", _connection);
        _pressesAdapter.UpdateCommand.Parameters.Add("@Id", SqlDbType.BigInt, 8, "Id");
        _pressesAdapter.UpdateCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 50, "Name");

        _pressesAdapter.DeleteCommand = new SqlCommand("DELETE FROM Presses WHERE Id = @Id", _connection);
        _pressesAdapter.DeleteCommand.Parameters.Add("@Id", SqlDbType.BigInt, 8, "Id");


        //
        // Команды адаптера для таблицы Books
        //

        _booksAdapter = new SqlDataAdapter("SELECT * FROM Books", _connection);

        _booksAdapter.InsertCommand = new SqlCommand("INSERT INTO Books (AuthorFk, Name, Pages, Price, PressFk) VALUES (@AuthorFk, @Name, @Pages, @Price, @PressFk)", _connection);
        _booksAdapter.InsertCommand.Parameters.Add("@AuthorFk", SqlDbType.BigInt, 8, "AuthorFk");
        _booksAdapter.InsertCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 100, "Name");
        _booksAdapter.InsertCommand.Parameters.Add("@Pages", SqlDbType.Int, 4, "Pages");
        _booksAdapter.InsertCommand.Parameters.Add("@Price", SqlDbType.Money, 8, "Price");
        _booksAdapter.InsertCommand.Parameters.Add("@PressFk", SqlDbType.BigInt, 8, "PressFk");

        _booksAdapter.UpdateCommand = new SqlCommand("UPDATE Books SET AuthorFk = @AuthorFk, Name = @Name, Pages = @Pages, Price = @Price, PressFk = @PressFk WHERE Id = @Id", _connection);
        _booksAdapter.UpdateCommand.Parameters.Add("@Id", SqlDbType.BigInt, 8, "Id");
        _booksAdapter.UpdateCommand.Parameters.Add("@AuthorFk", SqlDbType.BigInt, 8, "AuthorFk");
        _booksAdapter.UpdateCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 100, "Name");
        _booksAdapter.UpdateCommand.Parameters.Add("@Pages", SqlDbType.Int, 4, "Pages");
        _booksAdapter.UpdateCommand.Parameters.Add("@Price", SqlDbType.Money, 8, "Price");
        _booksAdapter.UpdateCommand.Parameters.Add("@PressFk", SqlDbType.BigInt, 8, "PressFk");

        _booksAdapter.DeleteCommand = new SqlCommand("DELETE FROM Books WHERE Id = @Id", _connection);
        _booksAdapter.DeleteCommand.Parameters.Add("@Id", SqlDbType.BigInt, 8, "Id");


        //
        // Считываем БД в DataSet
        //

        ReloadDataSet();
    }

    private void ReloadAuthors()
    {
        // Сначала очищаем дочернюю Books и только после этого Authors
        if (_dataSet.Tables.Contains("Books"))
            _dataSet.Tables["Books"].Clear();
        if (_dataSet.Tables.Contains("Authors"))
            _dataSet.Tables["Authors"].Clear();

        // Пересчитываем Authors, после чего обратно пересчитываем и Books
        _authorsAdapter.Fill(_dataSet, "Authors");
        _booksAdapter.Fill(_dataSet, "Books");
    }

    private void ReloadPresses()
    {
        // Сначала очищаем дочернюю Books и только после этого Presses
        if (_dataSet.Tables.Contains("Books"))
            _dataSet.Tables["Books"].Clear();
        if (_dataSet.Tables.Contains("Presses"))
            _dataSet.Tables["Presses"].Clear();

        // Пересчитываем Presses, после чего обратно пересчитываем и Books
        _pressesAdapter.Fill(_dataSet, "Presses");
        _booksAdapter.Fill(_dataSet, "Books");
    }

    private void ReloadBooks()
    {
        // Очищаем Books
        if (_dataSet.Tables.Contains("Books"))
            _dataSet.Tables["Books"].Clear();

        // Пересчитываем Books
        _booksAdapter.Fill(_dataSet, "Books");
    }

    private void ReloadDataSet()
    {
        _dataSet.Clear();

        // Сначала очищаем дочернюю Books и только после этого Authors и Presses
        if (_dataSet.Tables.Contains("Books"))
            _dataSet.Tables["Books"].Clear();
        if (_dataSet.Tables.Contains("Authors"))
            _dataSet.Tables["Authors"].Clear();
        if (_dataSet.Tables.Contains("Presses"))
            _dataSet.Tables["Presses"].Clear();

        // Пересчитываем Authors и Presses, после чего обратно пересчитываем и Books
        _authorsAdapter.Fill(_dataSet, "Authors");
        _pressesAdapter.Fill(_dataSet, "Presses");
        _booksAdapter.Fill(_dataSet, "Books");
    }

    #region IDisposable implementation

    public void Dispose()
    {
        if (_connection.State == ConnectionState.Open)
            _connection.Close();
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
        //    _dataSet.Tables["Authors"]
        //        .AsEnumerable()
        //        .Select(row => new Author
        //        {
        //            Id = Convert.ToInt64(row["Id"]),
        //            Name = row.Field<string>("Name")
        //        })
        //        .ToList();

        // Синтаксис запросов
        var authors =
            from row in _dataSet.Tables["Authors"].AsEnumerable()
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
        //var dataRow = _dataSet.Tables["Authors"]
        //    .AsEnumerable()
        //    .Where(row => Convert.ToInt64(row["Id"]) == id)
        //    .Select(row => row)
        //    .SingleOrDefault();

        // Синтаксис запросов
        var dataRow =
            (from row in _dataSet.Tables["Authors"].AsEnumerable()
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
        DataTable authorsTable = _dataSet.Tables["Authors"];
        DataRow newRow = authorsTable.NewRow();
        newRow["Name"] = author.Name;
        authorsTable.Rows.Add(newRow);
        _authorsAdapter.Update(authorsTable);

        // Переменная для автоматически сгенерированного значения автоинкремента
        // только что добавленной записи
        long id;
        // Получаем последнее выданное БД значение автоинкремента
        string sqlGetIdString = "SELECT @@IDENTITY";
        using (var commandId = new SqlCommand(sqlGetIdString, _connection))
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
        DataTable authorsTable = _dataSet.Tables["Authors"];
        foreach (DataRow row in authorsTable.Rows)
            if (Convert.ToInt64(row["Id"]) == author.Id)
                row["Name"] = author.Name;
        _authorsAdapter.Update(authorsTable);
    }

    public void DeleteAuthor(long id)
    {
        DataTable authorsTable = _dataSet.Tables["Authors"];

        // Вариант с каскадным удалением из Books в памяти без ее пересчитывания
        //foreach (DataRow row in authorsTable.Rows)
        //{
        //    if (Convert.ToInt64(row["Id"]) == id)
        //    {
        //        // Сначала удаляем книги этого автора
        //        DataTable booksTable = _dataSet.Tables["Books"];
        //        foreach (DataRow bookRow in booksTable.Rows)
        //        {
        //            if (Convert.ToInt64(bookRow["AuthorFk"]) == id)
        //                bookRow.Delete();
        //        }
        //        _booksAdapter.Update(booksTable);

        //        // А потом уже самого автора
        //        row.Delete();
        //    }
        //}
        //_authorsAdapter.Update(authorsTable);

        // Вариант с каскадным удалением из Books в БД и пересчитыванием ее в память
        foreach (DataRow row in authorsTable.Rows)
            if (Convert.ToInt64(row["Id"]) == id)
                row.Delete();
        _authorsAdapter.Update(authorsTable);

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
        //    _dataSet.Tables["Presses"]
        //        .AsEnumerable()
        //        .Select(row => new Press
        //        {
        //            Id = Convert.ToInt64(row["Id"]),
        //            Name = row.Field<string>("Name")
        //        })
        //        .ToList();

        // Синтаксис запросов
        var presses =
            from row in _dataSet.Tables["Presses"].AsEnumerable()
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
        //var dataRow = _dataSet.Tables["Presses"]
        //    .AsEnumerable()
        //    .Where(row => Convert.ToInt64(row["Id"]) == id)
        //    .Select(row => row)
        //    .SingleOrDefault();

        // Синтаксис запросов
        var dataRow =
            (from row in _dataSet.Tables["Presses"].AsEnumerable()
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
        DataTable pressesTable = _dataSet.Tables["Presses"];
        DataRow newRow = pressesTable.NewRow();
        newRow["Name"] = press.Name;
        pressesTable.Rows.Add(newRow);
        _pressesAdapter.Update(pressesTable);

        // Переменная для автоматически сгенерированного значения автоинкремента
        // только что добавленной записи
        long id;
        // Получаем последнее выданное БД значение автоинкремента
        string sqlGetIdString = "SELECT @@IDENTITY";
        using (var commandId = new SqlCommand(sqlGetIdString, _connection))
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
        DataTable pressesTable = _dataSet.Tables["Presses"];
        foreach (DataRow row in pressesTable.Rows)
            if (Convert.ToInt64(row["Id"]) == press.Id)
                row["Name"] = press.Name;
        _pressesAdapter.Update(pressesTable);
    }

    public void DeletePress(long id)
    {
        DataTable pressesTable = _dataSet.Tables["Presses"];

        // Вариант с каскадным удалением из Books в памяти без ее пересчитывания
        //foreach (DataRow row in pressesTable.Rows)
        //{
        //    if (Convert.ToInt64(row["Id"]) == id)
        //    {
        //        // Сначала удаляем книги этого издательства
        //        DataTable booksTable = _dataSet.Tables["Books"];
        //        foreach (DataRow bookRow in booksTable.Rows)
        //        {
        //            if (Convert.ToInt64(bookRow["PressFk"]) == id)
        //                bookRow.Delete();
        //        }
        //        _booksAdapter.Update(booksTable);

        //        // А потом уже само издательство
        //        row.Delete();
        //    }
        //}
        //_pressesAdapter.Update(pressesTable);

        // Вариант с каскадным удалением из Books в БД и пересчитыванием ее в память
        foreach (DataRow row in pressesTable.Rows)
            if (Convert.ToInt64(row["Id"]) == id)
                row.Delete();
        _pressesAdapter.Update(pressesTable);

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
        //    _dataSet.Tables["Books"]
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
            from row in _dataSet.Tables["Books"].AsEnumerable()
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
        //var dataRow = _dataSet.Tables["Books"]
        //    .AsEnumerable()
        //    .Where(row => Convert.ToInt64(row["Id"]) == id)
        //    .Select(row => row)
        //    .SingleOrDefault();

        // Синтаксис запросов
        var dataRow =
            (from row in _dataSet.Tables["Books"].AsEnumerable()
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
        DataTable booksTable = _dataSet.Tables["Books"];
        DataRow newRow = booksTable.NewRow();
        newRow["Name"] = book.Name;
        newRow["AuthorFk"] = book.AuthorFk;
        newRow["Name"] = book.Name;
        newRow["Pages"] = book.Pages;
        newRow["Price"] = book.Price;
        newRow["PressFk"] = book.PressFk;
        booksTable.Rows.Add(newRow);
        _booksAdapter.Update(booksTable);

        // Переменная для автоматически сгенерированного значения автоинкремента
        // только что добавленной записи
        long id;
        // Получаем последнее выданное БД значение автоинкремента
        string sqlGetIdString = "SELECT @@IDENTITY";
        using (var commandId = new SqlCommand(sqlGetIdString, _connection))
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
        DataTable booksTable = _dataSet.Tables["Books"];
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
        _booksAdapter.Update(booksTable);
    }

    public void DeleteBook(long id)
    {
        DataTable booksTable = _dataSet.Tables["Books"];
        foreach (DataRow row in booksTable.Rows)
            if (Convert.ToInt64(row["Id"]) == id)
                row.Delete();
        _booksAdapter.Update(booksTable);
    }

    #endregion
}
