using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MyLibrary.Common.DomainModel;
using MyLibrary.Common.Options;

namespace MyLibrary.CL.DomainModel
{
    public class Database : IDatabase
    {
        private readonly SqlConnection connection = new SqlConnection();

        public Database()
        {
            connection.ConnectionString = Options.ConnectionString;
            connection.Open();
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
            var authors = new List<Author>();

            string sqlString = "SELECT Id, Name FROM Authors";

            using (var command = new SqlCommand(sqlString, connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    authors.Add(new Author
                    {
                        Id = Convert.ToInt64(reader["Id"]),
                        Name = (string)reader["Name"]
                    });
                }
            }

            return authors;
        }

        public Author GetAuthorById(long id)
        {
            string sqlString = "SELECT Id, Name FROM Authors WHERE Id = @Id";

            using (var command = new SqlCommand(sqlString, connection))
            {
                command.Parameters.Add(new SqlParameter("@Id", id));

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Если нашли
                    if (reader.Read())
                        return new Author
                        {
                            Id = Convert.ToInt64(reader["Id"]),
                            Name = (string)reader["Name"]
                        };
                }
            }

            // Если не нашли
            return null;
        }

        public long InsertAuthor(Author author)
        {
            string sqlString = "INSERT INTO Authors (Name) VALUES (@Name)";
            using (var command = new SqlCommand(sqlString, connection))
            {
                command.Parameters.Add(new SqlParameter("@Name", author.Name));
                command.ExecuteNonQuery();
            }

            long id;

            string sqlGetIdString = "SELECT @@IDENTITY";
            using (var commandId = new SqlCommand(sqlGetIdString, connection))
            {
                object obj = commandId.ExecuteScalar();
                id = Convert.ToInt64(obj);
            }

            return id;
        }

        public void UpdateAuthor(Author author)
        {
            string sqlString = "UPDATE Authors SET Name = @Name WHERE Id = @Id";
            using (var command = new SqlCommand(sqlString, connection))
            {
                command.Parameters.Add(new SqlParameter("@Id", author.Id));
                command.Parameters.Add(new SqlParameter("@Name", author.Name));
                command.ExecuteNonQuery();
            }
        }

        public void DeleteAuthor(long id)
        {
            string sqlString = "DELETE FROM Authors WHERE Id = @Id";
            using (var command = new SqlCommand(sqlString, connection))
            {
                command.Parameters.Add(new SqlParameter("@Id", id));
                command.ExecuteNonQuery();
            }
        }


        //
        // Presses management
        //

        public IEnumerable<Press> GetAllPresses()
        {
            var presses = new List<Press>();

            string sqlString = "SELECT Id, Name FROM Presses";

            using (var command = new SqlCommand(sqlString, connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    presses.Add(new Press
                    {
                        Id = Convert.ToInt64(reader["Id"]),
                        Name = (string)reader["Name"]
                    });
                }
            }

            return presses;
        }

        public Press GetPressById(long id)
        {
            string sqlString = "SELECT Id, Name FROM Presses WHERE Id = @Id";

            using (var command = new SqlCommand(sqlString, connection))
            {
                command.Parameters.Add(new SqlParameter("@Id", id));

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Если нашли
                    if (reader.Read())
                        return new Press
                        {
                            Id = Convert.ToInt64(reader["Id"]),
                            Name = (string)reader["Name"]
                        };
                }
            }

            // Если не нашли
            return null;
        }

        public long InsertPress(Press press)
        {
            string sqlString = "INSERT INTO Presses (Name) VALUES (@Name)";
            using (var command = new SqlCommand(sqlString, connection))
            {
                command.Parameters.Add(new SqlParameter("@Name", press.Name));
                command.ExecuteNonQuery();
            }

            long id;

            string sqlGetIdString = "SELECT @@IDENTITY";
            using (var commandId = new SqlCommand(sqlGetIdString, connection))
            {
                object obj = commandId.ExecuteScalar();
                id = Convert.ToInt64(obj);
            }

            return id;
        }

        public void UpdatePress(Press press)
        {
            string sqlString = "UPDATE Presses SET Name = @Name WHERE Id = @Id";
            using (var command = new SqlCommand(sqlString, connection))
            {
                command.Parameters.Add(new SqlParameter("@Id", press.Id));
                command.Parameters.Add(new SqlParameter("@Name", press.Name));
                command.ExecuteNonQuery();
            }
        }

        public void DeletePress(long id)
        {
            string sqlString = "DELETE FROM Presses WHERE Id = @Id";
            using (var command = new SqlCommand(sqlString, connection))
            {
                command.Parameters.Add(new SqlParameter("@Id", id));
                command.ExecuteNonQuery();
            }
        }


        //
        // Books management
        //

        public IEnumerable<Book> GetAllBooks()
        {
            var books = new List<Book>();

            string sqlString = "SELECT Id, AuthorFk, Name, Pages, Price, PressFk FROM Books";

            using (var command = new SqlCommand(sqlString, connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    books.Add(new Book
                    {
                        Id = Convert.ToInt64(reader["Id"]),
                        AuthorFk = Convert.ToInt64(reader["AuthorFk"]),
                        Name = (string)reader["Name"],
                        Pages = (int)reader["Pages"],
                        Price = (decimal)reader["Price"],
                        PressFk = Convert.ToInt64(reader["PressFk"])
                    });
                }
            }

            return books;
        }

        public Book GetBookById(long id)
        {
            string sqlString = "SELECT Id, AuthorFk, Name, Pages, Price, PressFk FROM Books WHERE Id = @Id";

            using (var command = new SqlCommand(sqlString, connection))
            {
                command.Parameters.Add(new SqlParameter("@Id", id));

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Если нашли
                    if (reader.Read())
                        return new Book
                        {
                            Id = Convert.ToInt64(reader["Id"]),
                            AuthorFk = Convert.ToInt64(reader["AuthorFk"]),
                            Name = (string)reader["Name"],
                            Pages = (int)reader["Pages"],
                            Price = (decimal)reader["Price"],
                            PressFk = Convert.ToInt64(reader["PressFk"])
                        };
                }
            }

            // Если не нашли
            return null;
        }

        public long InsertBook(Book book)
        {
            string sqlString = "INSERT INTO Books (AuthorFk, Name, Pages, Price, PressFk) VALUES (@AuthorFk, @Name, @Pages, @Price, @PressFk)";
            using (var command = new SqlCommand(sqlString, connection))
            {
                command.Parameters.Add(new SqlParameter("@AuthorFk", book.AuthorFk));
                command.Parameters.Add(new SqlParameter("@Name", book.Name));
                command.Parameters.Add(new SqlParameter("@Pages", book.Pages));
                command.Parameters.Add(new SqlParameter("@Price", book.Price));
                command.Parameters.Add(new SqlParameter("@PressFk", book.PressFk));
                command.ExecuteNonQuery();
            }

            long id;

            string sqlGetIdString = "SELECT @@IDENTITY";
            using (var commandId = new SqlCommand(sqlGetIdString, connection))
            {
                object obj = commandId.ExecuteScalar();
                id = Convert.ToInt64(obj);
            }

            return id;
        }

        public void UpdateBook(Book book)
        {
            string sqlString = "UPDATE Books SET AuthorFk = @AuthorFk, Name = @Name, Pages = @Pages, Price = @Price, PressFk = @PressFk WHERE Id = @Id";
            using (var command = new SqlCommand(sqlString, connection))
            {
                command.Parameters.Add(new SqlParameter("@Id", book.Id));
                command.Parameters.Add(new SqlParameter("@AuthorFk", book.AuthorFk));
                command.Parameters.Add(new SqlParameter("@Name", book.Name));
                command.Parameters.Add(new SqlParameter("@Pages", book.Pages));
                command.Parameters.Add(new SqlParameter("@Price", book.Price));
                command.Parameters.Add(new SqlParameter("@PressFk", book.PressFk));
                command.ExecuteNonQuery();
            }
        }

        public void DeleteBook(long id)
        {
            string sqlString = "DELETE FROM Books WHERE Id = @Id";
            using (var command = new SqlCommand(sqlString, connection))
            {
                command.Parameters.Add(new SqlParameter("@Id", id));
                command.ExecuteNonQuery();
            }
        }

        #endregion
    }
}