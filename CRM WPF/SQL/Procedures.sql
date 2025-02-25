CREATE OR ALTER PROCEDURE AddCustomer
    @Name NVARCHAR(255),
    @Email NVARCHAR(255),
    @Phone NVARCHAR(50),
    @Company NVARCHAR(255) = NULL,
    @Address NVARCHAR(255) = NULL,
    @City NVARCHAR(100) = NULL,
    @ZipCode NVARCHAR(20) = NULL,
    @Country NVARCHAR(100) = NULL,
    @RegistrationDate DATETIME = NULL,
    @LastPurchaseDate DATETIME = NULL,
    @Notes NVARCHAR(MAX) = NULL,
    @AdditionalInfo NVARCHAR(MAX) = NULL,
    @NewCustomerId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    IF @RegistrationDate IS NULL
        SET @RegistrationDate = GETDATE();

    INSERT INTO Customers (Name, Email, Phone, Company, Address, City, ZipCode, Country, RegistrationDate, LastPurchaseDate, Notes, AdditionalInfo)
    VALUES (@Name, @Email, @Phone, @Company, @Address, @City, @ZipCode, @Country, @RegistrationDate, @LastPurchaseDate, @Notes, @AdditionalInfo);
    
    SET @NewCustomerId = SCOPE_IDENTITY();
END;
GO

--------------------------------------------

CREATE OR ALTER PROCEDURE AddProduct
    @Name NVARCHAR(255),
    @Category NVARCHAR(100),
    @Price DECIMAL(18,2),
    @StockQuantity INT,
    @NewProductId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Products (Name, Category, Price, StockQuantity)
    VALUES (@Name, @Category, @Price, @StockQuantity);

    SET @NewProductId = SCOPE_IDENTITY();
END;
GO

----------------------------------------------

CREATE OR ALTER PROCEDURE AddTransaction
    @CustomerId INT,
    @ProductId INT,
    @Value DECIMAL(18,2),
    @Date DATETIME = NULL,
    @Quantity INT,
    @Category NVARCHAR(100),
    @NewTransactionId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    IF @Date IS NULL
        SET @Date = GETDATE();

    INSERT INTO Transactions (CustomerId, ProductId, Value, Date, Month, Year, Quantity, Category)
    VALUES (@CustomerId, @ProductId, @Value, @Date, MONTH(@Date), YEAR(@Date), @Quantity, @Category);

    SET @NewTransactionId = SCOPE_IDENTITY();
END;
GO

---------------------------------------------------

CREATE OR ALTER PROCEDURE AddCalendarEvent
    @CustomerId INT,
    @Title NVARCHAR(255),
    @Description NVARCHAR(MAX),
    @StartTime DATETIME,
    @EndTime DATETIME,
    @DayOfWeek INT,
    @EventColor NVARCHAR(50),
    @NewEventId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Events (CustomerId, Title, Description, StartTime, EndTime, DayOfWeek, EventColor)
    VALUES (@CustomerId, @Title, @Description, @StartTime, @EndTime, @DayOfWeek, @EventColor);

    SET @NewEventId = SCOPE_IDENTITY();
END;
GO