-- Tabela Klientów
CREATE TABLE Customers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) UNIQUE NOT NULL,
    Phone NVARCHAR(50) NOT NULL,
    Company NVARCHAR(255),
    Address NVARCHAR(255),
    City NVARCHAR(100),
    ZipCode NVARCHAR(20),
    Country NVARCHAR(100),
    RegistrationDate DATETIME NOT NULL DEFAULT GETDATE(),
    LastPurchaseDate DATETIME NULL,
    Notes NVARCHAR(MAX),
    AdditionalInfo NVARCHAR(MAX)
);

-- Tabela Produktów (Towarów)
CREATE TABLE Products (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL UNIQUE,
    Category NVARCHAR(100),
    Price DECIMAL(18,2) NOT NULL,
    StockQuantity INT NOT NULL DEFAULT 0
);

-- Tabela Transakcji
CREATE TABLE Transactions (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT NOT NULL,
    ProductId INT NOT NULL,
    Value DECIMAL(18,2) NOT NULL,
    Date DATETIME NOT NULL DEFAULT GETDATE(),
    Month INT NOT NULL,
    Year INT NOT NULL,
    Quantity INT NOT NULL,
    Category NVARCHAR(100),
    FOREIGN KEY (CustomerId) REFERENCES Customers(Id) ON DELETE CASCADE,
    FOREIGN KEY (ProductId) REFERENCES Products(Id) ON DELETE CASCADE
);

-- Tabela Wydarzeñ (np. spotkania z klientami)
CREATE TABLE CalendarEvents (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT NOT NULL,
    Title NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX),
    StartTime DATETIME NOT NULL,
    EndTime DATETIME NOT NULL,
    DayOfWeek INT NOT NULL CHECK (DayOfWeek BETWEEN 0 AND 6),
    EventColor NVARCHAR(50), -- Przechowujemy jako kod HEX lub nazwa koloru
    FOREIGN KEY (CustomerId) REFERENCES Customers(Id) ON DELETE CASCADE
);

-- Tabela U¿ytkowników
CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(100) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
);

-- Tabela zmian w bazie
CREATE TABLE AuditLog (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NULL,
    Username NVARCHAR(100) NULL,
    TableName NVARCHAR(100) NOT NULL,
    ColumnName NVARCHAR(100) NOT NULL,
    OldValue NVARCHAR(MAX) NULL,
    NewValue NVARCHAR(MAX) NULL,
    OperationType NVARCHAR(10) NOT NULL CHECK (OperationType IN ('INSERT', 'UPDATE', 'DELETE')),
    ChangeTime DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE SET NULL
);