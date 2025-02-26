# CRM WPF(MVVM Pattern) + ASP.NET Core API with SQL Server and Entity Framework Core

CRM WPF is a desktop application for managing clients, transactions, and sales reports.  
The system consists of a WPF client application and a backend API built with ASP.NET Core, providing data storage and business logic.

## Features

✅ Client Management – Add, edit, delete, and search clients  
✅ Transaction Tracking – Manage purchases and transactions  
✅ Product Catalog – Manage product inventory  
✅ Sales Reports & Charts – View sales statistics with LiveCharts  
✅ Calendar & Events – Schedule meetings and events with clients  
✅ CSV & Excel Export – Export transactions and reports  
✅ API Communication – Uses ASP.NET Core API with Entity Framework Core  

---

## Technologies Used

Frontend (WPF)
- C# (WPF, MVVM Pattern) – UI and application logic  
- LiveCharts – Data visualization (bar and pie charts)  
- Dependency Injection (Microsoft.Extensions.DependencyInjection) – Service management  
- HttpClient & System.Net.Http.Json – API communication  
- ExcelExportService & CsvExportService – Exporting data  

Backend (ASP.NET Core API)
- ASP.NET Core 8.0 – Web API backend  
- Entity Framework Core – Database management (MS SQL)  
- Microsoft SQL Server – Data storage  
- Swagger (Swashbuckle) – API documentation  
- Repository Pattern & Dependency Injection – Clean architecture  
