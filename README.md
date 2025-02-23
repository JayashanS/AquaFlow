# AquaFlow


## Setting up the Database

### 1. Configure the Database Connection String

In the `appsettings.json` file of the `AquaFlow.API` project, add your database connection string in the `ConnectionStrings` section.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "YourConnectionStringHere"
  }
}
```

Make sure to replace `YourConnectionStringHere` with the actual connection string to your database.

### 2. Add EF Core Migrations

Navigate to the `AquaFlow.API` folder and use the following command to add a migration:

```bash
dotnet ef migrations add Update --project ..\AquaFlow.DataAccess\AquaFlow.DataAccess.csproj --startup-project .
```

This command generates a migration based on changes in your models.

### 3. Update the Database

Once the migration is added, apply it to the database using the following command:

```bash
dotnet ef database update --project ..\AquaFlow.DataAccess\AquaFlow.DataAccess.csproj --startup-project .
```

This command updates the database schema based on the latest migrations.

### 4. Run the API

Once the migrations are applied successfully, run the API project:

```bash
dotnet run
```

Your API should now be up and running with the latest database schema applied.
