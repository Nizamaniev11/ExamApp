# ExamApp

## Description
This project is an exam registration system for school students. It allows for the registration of subjects, students, and exam results.

## Backend Setup

### Technologies Used
- C#
- ASP.NET Core
- Entity Framework Core
- MS SQL Server
- NLog for logging
- Swagger for API documentation

### Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download) (version 8 or higher)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express or higher)
- [Visual Studio / Rider](https://www.jetbrains.com/rider/) (for development)

### Setting Up the Backend

1. **Clone the repository**:
   ```bash
   git clone https://github.com/Nizamaniev11/ExamApp.git
   ```

2. **Navigate to the project directory**:
   ```bash
   cd ExamApp
   ```

3. **Restore the dependencies**:
   ```bash
   dotnet restore
   ```

4. **Update the connection string in \`appsettings.json\` to match your SQL Server instance**:
   ```json
   "ConnectionStrings": {
   "DefaultConnection": "Server=your_server;Database=your_database;User Id=your_user;Password=your_password;"
   }
   ```

5. **Run migrations to create the database schema and update to apply to database**:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

6. **Run the application**:
   ```bash
   dotnet run
   ```

7. **Access Swagger UI at** [http://localhost:5176/swagger](http://localhost:5176/swagger) **to explore the API.**

