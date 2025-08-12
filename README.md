# .Net 8 Web API Project

This project is a .NET 8 Web API using Entity Framework Core with a stored procedure example.

## Features
- ASP.NET Core 8 Web API
- Entity Framework Core
- SQL Server Integration
- Stored Procedure Example
- Swagger UI Enabled

## Getting Started

### 1. Prerequisites
- .NET 8 SDK installed
- SQL Server instance
- Visual Studio or VS Code

### 2. Clone the Repository
```bash
git clone https://github.com/username/repo-name.git
cd repo-name
```

### 3. DB Setup
- Create a SQL Table.
```sql
CREATE TABLE Customers (
    CustomerId INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100),
    Email NVARCHAR(100)
);

CREATE TABLE Orders (
    OrderId INT PRIMARY KEY IDENTITY,
    CustomerId INT,
    OrderDate DATETIME,
    TotalAmount DECIMAL(10,2),
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
);

-- Insert Sample Data
INSERT INTO Customers (Name, Email) VALUES ('Kapil Deshmukh', 'kapil@example.com');
INSERT INTO Orders (CustomerId, OrderDate, TotalAmount)
VALUES (1, GETDATE(), 500.00), (1, GETDATE(), 1000.00);
```
- Create a Store Procedure Table.
```sql
CREATE PROCEDURE GetCustomerOrders
    @CustomerId INT
AS
BEGIN
    SELECT 
        c.CustomerId, c.Name, c.Email,
        o.OrderId, o.OrderDate, o.TotalAmount
    FROM Customers c
    INNER JOIN Orders o ON c.CustomerId = o.CustomerId
    WHERE c.CustomerId = @CustomerId
END
```
### 4. Invoke API using API-Key
- curl -H "X-API-KEY: YOUR_SECURE_API_KEY" https://localhost:7087/api/Customer/1/orders
