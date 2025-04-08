# management-template
BUILD ON .NET 8.0

INSTALL AS PROJECT TEMPLATE
To Install:
```dotnet new install /path/to/solution/MANAGEMENT.TEMPLATE```
To Uninstall: 
```dotnet new uninstall /path/to/solution/MANAGEMENT.TEMPLATE```

HOW TO USE
1. Replace 'table_name' in Model.cs and AppDBContext.cs
2. Modify Controller
3. Modify ConnectionString in appsettings.json


INCLUDES
1. File Logging (Serilog)
2. Disable CORS on Localhost (Development Only)



