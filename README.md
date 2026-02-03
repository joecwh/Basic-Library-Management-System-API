# 📚 Library Management System

A simple **ASP.NET Core + Entity Framework Core + SQL Server** backend project that demonstrates basic CRUD operations using a clean architecture style with migrations and a real database.

This project is designed for learning purposes and can be extended into a full library management system in the future.

---

## 🚀 Features

Current version supports:

* ✅ Book entity with basic fields
* ✅ SQL Server database using EF Core
* ✅ Code-first migrations
* ✅ Clean separation of DbContext
* ✅ Configurable connection string via `appsettings.json`

---

## 🧱 Tech Stack

* .NET 8
* ASP.NET Core
* Entity Framework Core
* AutoMapper
* SQL Server
* In Memory Database (Testing)

---

## 📁 Project Structure (Simplified)

```
YourProject
│
├── Models/
│   └── Book.cs
│
├── Data/
│   └── ApplicationDbContext.cs
│
├── appsettings.json
└── Program.cs
```

---

## 🗄️ Database

The system uses a SQL Server database named:

```
LibraryDb
```

Connection string (in `appsettings.json`):

```json
{
  "ConnectionStrings": {
    "LibraryDb": "Server=.;Database=LibraryDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

> If you use LocalDB, replace with:

```json
"Server=(localdb)\\mssqllocaldb;Database=LibraryDb;Trusted_Connection=True;"
```

---

## ▶️ How to Run the Project

### **Step 1 — Restore packages**

Run in terminal:

```bash
dotnet restore
```

---

### **Step 2 — Check Program.cs**

Make sure you have:

```csharp
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("LibraryDb")
    ));
```

---

### **Step 3 — Create the database (First time only)**

Open **Package Manager Console** in Visual Studio:

```
PM> add-migration InitialCommit
PM> update-database
```

If successful, you should see:

```
Applying migration 'InitialCommit'...
Done.
```

You can then check SQL Server — the **LibraryDb** database should exist.

---

## 🧪 How to Test

### Option A — Using SQL Server Management Studio (SSMS)

1. Open SSMS
2. Connect to `(local)`
3. Expand Databases
4. You should see **LibraryDb**
5. Expand Tables → `Books`
6. Try inserting a record:

```sql
INSERT INTO Books (Title, Author, Publisher, ISBN)
VALUES ('Clean Code', 'Robert C. Martin', 'Prentice Hall', '9780132350884');
```

---

### Option B — Using Code (Optional)

Add a quick test in `Program.cs`:

```csharp
using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

db.Books.Add(new Book
{
    Title = "Test Book",
    Author = "John Doe",
    Publisher = "Demo Pub",
    ISBN = "1234567890"
});

db.SaveChanges();
```

Run the app once — then check SQL Server.

---

## 🛠️ Troubleshooting

### ❌ "ConnectionString property has not been initialized"

Make sure your `appsettings.json` contains:

```json
"ConnectionStrings": {
  "LibraryDb": "..."
}
```

and **Program.cs** uses `"LibraryDb"` exactly.

---

### ❌ Migration fails

Try:

```
PM> remove-migration
PM> add-migration InitialCommit
PM> update-database
```

---

## 📌 Future Improvements

Planned features:

* 📘 Add `Member` entity
* 📗 Add `BorrowRecord`
* 📙 Add Web API endpoints
* 📕 Add Swagger
* 📔 Add authentication

---

## 👨‍💻 Author

Joe Chow
Software Developer
Malaysia

---
