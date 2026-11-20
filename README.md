# MyLibrary Application

## What is this?

**MyLibrary application** is a demo project that is written in .NET 11 and illustrates working with:

#### Database:

 - Microsoft SQL Server

#### .NET database frameworks:

 - ADO.NET (Connected Layer, Disconnected Layer, Disconnected Layer with LINQ to DataSet)
 - Entity Framework Core

#### Design patterns:

 - Repository

#### GUI frameworks:

 - Windows Forms with MVP pattern
 - WPF with MVVM pattern

## How to run?

1. Create **MyLibrary** database in Microsoft SQL Server using `database\MyLibrary.sql` script file.

2. There are 8 projects (4 for Windows Forms and 4 for WPF):
   - `src\winforms\MyLibrary.WinForms.CL` and `src\wpf\MyLibrary.Wpf.CL` *(ADO.NET Connected Layer)*
   - `src\winforms\MyLibrary.WinForms.DL` and `src\wpf\MyLibrary.Wpf.DL` *(ADO.NET Disconnected Layer)*
   - `src\winforms\MyLibrary.WinForms.DLWithLinqToDataSet` and `src\wpf\MyLibrary.Wpf.DLWithLinqToDataSet` *(ADO.NET Disconnected Layer with LINQ to DataSet)*
   - `src\winforms\MyLibrary.WinForms.EF` and `src\wpf\MyLibrary.Wpf.EF` *(Entity Framework Core)*

   Each of these projects contains an `appsettings.json` file with a connection string to the **MyLibrary** database.
   Specify your connection, if the default connection string doesn't suit you.

   Alternatively, you can specify the connection string in user secrets. They are shared by all projects
   and take precedence over `appsettings.json`:

   ```
   dotnet user-secrets set "ConnectionStrings:Database" "<your connection string>" --project src\common\MyLibrary.Common
   ```

   In Visual Studio you can also use the **Manage User Secrets** command from the context menu of any of these projects.

   After that, just set the project as a Startup Project and run it.
