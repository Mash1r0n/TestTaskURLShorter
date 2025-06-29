# 🚀 Local Back-End Initialization Guide

You can use these steps to correctly set up and initialize the back-end application locally:

---

## 1️⃣ Configure the Database Connection

In `infrastructure/Persistence/AppDbContext.cs`, **temporarily** add the following code to enable local database configuration:

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=UrlShorterTestTaskDB;Integrated Security=SSPI");
}
```

---

## 2️⃣ Apply Database Migrations

Choose **one** of the following methods based on your development environment:

- **Visual Studio**
  1. Open the **Package Manager Console**.
  2. Set **Infrastructure** as the default project (both in the console and in Solution Explorer).
  3. Run:
     ```powershell
     Update-Database
     ```
  4. Wait until the migration process completes.

- **Command Line / Other IDE**
  1. In your terminal, run:
     ```sh
     dotnet ef database update --project Infrastructure --startup-project Infrastructure
     ```

---

## 3️⃣ Clean Up
**Remove** the code you added in _Step 1_ from `AppDbContext.cs`.

---

## 4️⃣ Set the Correct Startup Project
Change the default startup/run project to **Web** in your solution/project settings.

---

## 5️⃣ Run the Application
Start the application as usual.
