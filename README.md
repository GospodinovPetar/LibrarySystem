# Library System

A robust library management application built with ASP.NET Core MVC. This system allows administrators to manage books, readers, and loans efficiently. The application supports CRUD operations, advanced sorting, searching, and relationship management between entities.

---

## Features

### Core Functionalities
- **Book Management**: Add, edit, delete, and view books with fields such as title, author, ISBN, publication date, price, and stock.
- **Reader Management**: Add, edit, delete, and view readers, including personal details like name, email, phone number, and membership date.
- **Loan Management**: Manage loans by associating books with readers and tracking them efficiently.

### Enhancements

#### **Search Functionality**
- Integrated search bars across all entities (`Books`, `Readers`, and `Loans`).
- Allows users to search:
  - Books by title or author.
  - Readers by name.
  - Loans by book title or reader name.

#### **Sorting Functionality**
- Sorting options for all entities:
  - Books: Sort by title or author.
  - Readers: Sort by name or membership date.
  - Loans: Sort by book title or reader name.
- Dynamic column headers in tables with clickable sorting links.

#### **Relationship Management**
- In the `Loans` module, dropdowns allow the selection of books and readers:
  - Uses `SelectList` to populate dropdowns with existing data from the `Books` and `Readers` tables.
  - Ensures loans are properly associated with existing entities.

#### **Dynamic Views**
- Dynamic table generation in the `Index` views for all entities.
- Updated navigation links and headers to reflect the added functionalities.

---

## Setup Instructions

### Prerequisites
1. Install [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/).
2. Install the .NET 6.0 (or later) SDK.
3. Set up a SQL Server or SQL Server Express instance for the database.

### Running the Project

1. Clone the repository:
   ```bash
   git clone <repository-url>
   cd LibrarySystem

### Updating Database
2. Update the database connection string in the appsettings.json file:
   ```bash
   "ConnectionStrings": {
    "LibraryContext": "Your-Database-Connection-String-Here"
    }
**Note: Visual Studio Code does not support relative paths for database connections, so you must use an absolute path if working with local SQLite.**

3. Apply migrations and create the database:
   ```bash
   dotnet ef database update
   ```
4. Run the application:
   ```bash
   dotnet run
   ```        
