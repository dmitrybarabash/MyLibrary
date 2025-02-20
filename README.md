# MyLibrary Application

## What is this?

**MyLibrary application** is a demo project that is written in .NET 9 and illustrates working with:

#### Database:

 - Microsoft SQL Server

#### .NET database frameworks:

 - ADO.NET (Connected Layer, Disconnected Layer, Disconnected Layer with LINQ to DataSet)
 - Entity Framework Core

#### Design patterns:

 - Repository

#### GUI frameworks:

 - Windows Forms with MVP pattern
 - ~~WPF with MVVM pattern~~ (*not implemented yet; planning to do it in the future*)

## How to run?

1. Create **MyLibrary** database in Microsoft SQL Server using `database\MyLibrary.sql` script file.

2. There are 4 projects:
   - `src\winforms\MyLibrary.WinForms.CL` *(ADO.NET Connected Layer)*
   - `src\winforms\MyLibrary.WinForms.DL` *(ADO.NET Disconnected Layer)*
   - `src\winforms\MyLibrary.WinForms.DLWithLinqToDataSet` *(ADO.NET Disconnected Layer with LINQ to DataSet)*
   - `src\winforms\MyLibrary.WinForms.EF` *(Entity Framework Core)*

   Each of these projects contains an `appsettings.json` file with a connection string to the **MyLibrary** database.
   Specify your connection, if the default connection string doesn't suit you.
   After that, just set the project as a Startup Project and run it.
