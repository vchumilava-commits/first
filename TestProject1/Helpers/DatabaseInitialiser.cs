using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;
using Dapper;

namespace Tests1.Helpers
{
        public static class DatabaseInitializer
        {
            public static async Task InitializeAsync(SqliteConnection connection)
            {
                await CreateTablesAsync(connection);
                await SeedCategoriesAsync(connection);
                await SeedUsersAsync(connection);
                await SeedAddressesAsync(connection);
                await SeedProductsAsync(connection);
                await SeedOrdersAsync(connection);
                await SeedOrderItemsAsync(connection);
                await SeedReviewsAsync(connection);
            }

            // =========================================================
            // TABLES
            // =========================================================

            private static async Task CreateTablesAsync(
                SqliteConnection connection)
            {
                const string sql = """
            PRAGMA foreign_keys = ON;

            CREATE TABLE IF NOT EXISTS Users
            (
                Id          INTEGER PRIMARY KEY AUTOINCREMENT,
                FirstName   TEXT NOT NULL,
                LastName    TEXT NOT NULL,
                Email       TEXT NOT NULL UNIQUE,
                Phone       TEXT,
                CreatedAt   TEXT NOT NULL
            );

            CREATE TABLE IF NOT EXISTS Addresses
            (
                Id          INTEGER PRIMARY KEY AUTOINCREMENT,
                UserId      INTEGER NOT NULL,
                City        TEXT NOT NULL,
                Street      TEXT NOT NULL,
                House       TEXT NOT NULL,
                Apartment   TEXT,

                FOREIGN KEY (UserId)
                    REFERENCES Users(Id)
                    ON DELETE CASCADE
            );

            CREATE TABLE IF NOT EXISTS Categories
            (
                Id          INTEGER PRIMARY KEY AUTOINCREMENT,
                Name        TEXT NOT NULL UNIQUE
            );

            CREATE TABLE IF NOT EXISTS Products
            (
                Id          INTEGER PRIMARY KEY AUTOINCREMENT,
                Name        TEXT NOT NULL,
                Description TEXT,
                Price       REAL NOT NULL,
                Stock       INTEGER NOT NULL DEFAULT 0,
                CategoryId  INTEGER NOT NULL,

                FOREIGN KEY (CategoryId)
                    REFERENCES Categories(Id)
            );

            CREATE TABLE IF NOT EXISTS Orders
            (
                Id          INTEGER PRIMARY KEY AUTOINCREMENT,
                UserId      INTEGER NOT NULL,
                OrderDate   TEXT NOT NULL,
                Status      TEXT NOT NULL,
                TotalPrice  REAL NOT NULL DEFAULT 0,

                FOREIGN KEY (UserId)
                    REFERENCES Users(Id)
            );

            CREATE TABLE IF NOT EXISTS OrderItems
            (
                Id          INTEGER PRIMARY KEY AUTOINCREMENT,
                OrderId     INTEGER NOT NULL,
                ProductId   INTEGER NOT NULL,
                Quantity    INTEGER NOT NULL,
                UnitPrice   REAL NOT NULL,

                FOREIGN KEY (OrderId)
                    REFERENCES Orders(Id)
                    ON DELETE CASCADE,

                FOREIGN KEY (ProductId)
                    REFERENCES Products(Id)
            );

            CREATE TABLE IF NOT EXISTS Reviews
            (
                Id          INTEGER PRIMARY KEY AUTOINCREMENT,
                UserId      INTEGER NOT NULL,
                ProductId   INTEGER NOT NULL,
                Rating      INTEGER NOT NULL,
                Comment     TEXT,
                CreatedAt   TEXT NOT NULL,

                FOREIGN KEY (UserId)
                    REFERENCES Users(Id)
                    ON DELETE CASCADE,

                FOREIGN KEY (ProductId)
                    REFERENCES Products(Id)
                    ON DELETE CASCADE,

                CHECK (Rating BETWEEN 1 AND 5),

                UNIQUE(UserId, ProductId)
            );
            """;

                await connection.ExecuteAsync(sql);
            }


            // =========================================================
            // USERS
            // =========================================================

            private static async Task SeedUsersAsync(
                SqliteConnection connection)
            {
                const string sql = """
            INSERT INTO Users
                (FirstName, LastName, Email, Phone, CreatedAt)
            VALUES
                (@FirstName, @LastName, @Email, @Phone, @CreatedAt);
            """;

                var users = new[]
                {
            new
            {
                FirstName = "Иван",
                LastName = "Петров",
                Email = "ivan.petrov@mail.ru",
                Phone = "+79990000001",
                CreatedAt = "2025-01-15"
            },

            new
            {
                FirstName = "Анна",
                LastName = "Соколова",
                Email = "anna.sokolova@mail.ru",
                Phone = "+79990000002",
                CreatedAt = "2025-01-20"
            },

            new
            {
                FirstName = "Максим",
                LastName = "Иванов",
                Email = "max.ivanov@mail.ru",
                Phone = "+79990000003",
                CreatedAt = "2025-02-05"
            },

            new
            {
                FirstName = "Елена",
                LastName = "Кузнецова",
                Email = "elena.kuznetsova@mail.ru",
                Phone = "+79990000004",
                CreatedAt = "2025-02-18"
            },

            new
            {
                FirstName = "Дмитрий",
                LastName = "Смирнов",
                Email = "dmitry.smirnov@mail.ru",
                Phone = "+79990000005",
                CreatedAt = "2025-03-01"
            },

            new
            {
                FirstName = "Ольга",
                LastName = "Попова",
                Email = "olga.popova@mail.ru",
                Phone = "+79990000006",
                CreatedAt = "2025-03-15"
            },

            new
            {
                FirstName = "Алексей",
                LastName = "Васильев",
                Email = "alexey.vasiliev@mail.ru",
                Phone = "+79990000007",
                CreatedAt = "2025-04-02"
            },

            new
            {
                FirstName = "Мария",
                LastName = "Павлова",
                Email = "maria.pavlova@mail.ru",
                Phone = "+79990000008",
                CreatedAt = "2025-04-19"
            },

            new
            {
                FirstName = "Андрей",
                LastName = "Морозов",
                Email = "andrey.morozov@mail.ru",
                Phone = "+79990000009",
                CreatedAt = "2025-05-03"
            },

            new
            {
                FirstName = "Наталья",
                LastName = "Волкова",
                Email = "natalia.volkova@mail.ru",
                Phone = "+79990000010",
                CreatedAt = "2025-05-22"
            },

            new
            {
                FirstName = "Сергей",
                LastName = "Алексеев",
                Email = "sergey.alekseev@mail.ru",
                Phone = "+79990000011",
                CreatedAt = "2025-06-10"
            },

            new
            {
                FirstName = "Ирина",
                LastName = "Лебедева",
                Email = "irina.lebedeva@mail.ru",
                Phone = "+79990000012",
                CreatedAt = "2025-06-25"
            },

            new
            {
                FirstName = "Роман",
                LastName = "Козлов",
                Email = "roman.kozlov@mail.ru",
                Phone = "+79990000013",
                CreatedAt = "2025-07-08"
            },

            new
            {
                FirstName = "Светлана",
                LastName = "Новикова",
                Email = "svetlana.novikova@mail.ru",
                Phone = "+79990000014",
                CreatedAt = "2025-07-21"
            },

            new
            {
                FirstName = "Павел",
                LastName = "Орлов",
                Email = "pavel.orlov@mail.ru",
                Phone = "+79990000015",
                CreatedAt = "2025-08-05"
            }
        };

                await connection.ExecuteAsync(sql, users);
            }


            // =========================================================
            // ADDRESSES
            // =========================================================

            private static async Task SeedAddressesAsync(
                SqliteConnection connection)
            {
                const string sql = """
            INSERT INTO Addresses
                (UserId, City, Street, House, Apartment)
            VALUES
                (@UserId, @City, @Street, @House, @Apartment);
            """;

                var addresses = new[]
                {
            new { UserId = 1, City = "Москва", Street = "Ленинский проспект", House = "10", Apartment = "25" },
            new { UserId = 2, City = "Санкт-Петербург", Street = "Невский проспект", House = "15", Apartment = "41" },
            new { UserId = 3, City = "Москва", Street = "Тверская улица", House = "20", Apartment = "18" },
            new { UserId = 4, City = "Казань", Street = "Баумана", House = "12", Apartment = "7" },
            new { UserId = 5, City = "Москва", Street = "Арбат", House = "30", Apartment = "52" },
            new { UserId = 6, City = "Сочи", Street = "Курортный проспект", House = "5", Apartment = "12" },
            new { UserId = 7, City = "Екатеринбург", Street = "Ленина", House = "45", Apartment = "33" },
            new { UserId = 8, City = "Москва", Street = "Профсоюзная", House = "70", Apartment = "16" },
            new { UserId = 9, City = "Новосибирск", Street = "Красный проспект", House = "25", Apartment = "80" },
            new { UserId = 10, City = "Самара", Street = "Московское шоссе", House = "100", Apartment = "14" },
            new { UserId = 11, City = "Москва", Street = "Мясницкая", House = "8", Apartment = "22" },
            new { UserId = 12, City = "Уфа", Street = "Октября", House = "55", Apartment = "31" },
            new { UserId = 13, City = "Ростов-на-Дону", Street = "Большая Садовая", House = "40", Apartment = "9" },
            new { UserId = 14, City = "Москва", Street = "Варшавское шоссе", House = "60", Apartment = "45" },
            new { UserId = 15, City = "Краснодар", Street = "Красная улица", House = "18", Apartment = "11" }
        };

                await connection.ExecuteAsync(sql, addresses);
            }


            // =========================================================
            // CATEGORIES
            // =========================================================

            private static async Task SeedCategoriesAsync(
                SqliteConnection connection)
            {
                const string sql = """
            INSERT INTO Categories (Name)
            VALUES (@Name);
            """;

                var categories = new[]
                {
            new { Name = "Смартфоны" },
            new { Name = "Ноутбуки" },
            new { Name = "Наушники" },
            new { Name = "Телевизоры" },
            new { Name = "Бытовая техника" },
            new { Name = "Аксессуары" }
        };

                await connection.ExecuteAsync(sql, categories);
            }


            // =========================================================
            // PRODUCTS
            // =========================================================

            private static async Task SeedProductsAsync(
                SqliteConnection connection)
            {
                const string sql = """
            INSERT INTO Products
                (Name, Description, Price, Stock, CategoryId)
            VALUES
                (@Name, @Description, @Price, @Stock, @CategoryId);
            """;

                var products = new[]
                {
            new
            {
                Name = "iPhone 15",
                Description = "Смартфон Apple",
                Price = 79990,
                Stock = 15,
                CategoryId = 1
            },

            new
            {
                Name = "Samsung Galaxy S24",
                Description = "Флагманский смартфон Samsung",
                Price = 69990,
                Stock = 20,
                CategoryId = 1
            },

            new
            {
                Name = "Xiaomi Redmi Note 13",
                Description = "Бюджетный смартфон Xiaomi",
                Price = 24990,
                Stock = 35,
                CategoryId = 1
            },

            new
            {
                Name = "MacBook Air M3",
                Description = "Ноутбук Apple",
                Price = 129990,
                Stock = 10,
                CategoryId = 2
            },

            new
            {
                Name = "Lenovo IdeaPad 5",
                Description = "Ноутбук Lenovo",
                Price = 74990,
                Stock = 18,
                CategoryId = 2
            },

            new
            {
                Name = "ASUS VivoBook 15",
                Description = "Ноутбук ASUS",
                Price = 65990,
                Stock = 12,
                CategoryId = 2
            },

            new
            {
                Name = "AirPods Pro 2",
                Description = "Беспроводные наушники Apple",
                Price = 24990,
                Stock = 30,
                CategoryId = 3
            },

            new
            {
                Name = "Sony WH-1000XM5",
                Description = "Беспроводные наушники Sony",
                Price = 29990,
                Stock = 14,
                CategoryId = 3
            },

            new
            {
                Name = "JBL Tune 770NC",
                Description = "Наушники JBL",
                Price = 8990,
                Stock = 25,
                CategoryId = 3
            },

            new
            {
                Name = "LG OLED C3",
                Description = "OLED телевизор LG",
                Price = 119990,
                Stock = 7,
                CategoryId = 4
            },

            new
            {
                Name = "Samsung QLED Q70",
                Description = "QLED телевизор Samsung",
                Price = 99990,
                Stock = 8,
                CategoryId = 4
            },

            new
            {
                Name = "Dyson V15",
                Description = "Беспроводной пылесос Dyson",
                Price = 54990,
                Stock = 9,
                CategoryId = 5
            },

            new
            {
                Name = "Philips Airfryer",
                Description = "Аэрогриль Philips",
                Price = 12990,
                Stock = 20,
                CategoryId = 5
            },

            new
            {
                Name = "Apple Watch Series 9",
                Description = "Умные часы Apple",
                Price = 42990,
                Stock = 13,
                CategoryId = 6
            },

            new
            {
                Name = "Anker PowerBank",
                Description = "Внешний аккумулятор",
                Price = 4990,
                Stock = 40,
                CategoryId = 6
            },

            new
            {
                Name = "Logitech MX Master 3S",
                Description = "Беспроводная мышь",
                Price = 9990,
                Stock = 22,
                CategoryId = 6
            },

            new
            {
                Name = "Samsung 45W Charger",
                Description = "Зарядное устройство Samsung",
                Price = 3990,
                Stock = 50,
                CategoryId = 6
            },

            new
            {
                Name = "USB-C Hub",
                Description = "USB-C концентратор",
                Price = 5990,
                Stock = 30,
                CategoryId = 6
            }
        };

                await connection.ExecuteAsync(sql, products);
            }


            // =========================================================
            // ORDERS
            // =========================================================

            private static async Task SeedOrdersAsync(
                SqliteConnection connection)
            {
                const string sql = """
            INSERT INTO Orders
                (UserId, OrderDate, Status, TotalPrice)
            VALUES
                (@UserId, @OrderDate, @Status, @TotalPrice);
            """;

                var orders = new[]
                {
            new { UserId = 1, OrderDate = "2026-01-10", Status = "Delivered", TotalPrice = 84980 },
            new { UserId = 2, OrderDate = "2026-01-15", Status = "Delivered", TotalPrice = 24990 },
            new { UserId = 3, OrderDate = "2026-01-20", Status = "Cancelled", TotalPrice = 129990 },
            new { UserId = 4, OrderDate = "2026-02-02", Status = "Delivered", TotalPrice = 29990 },
            new { UserId = 5, OrderDate = "2026-02-10", Status = "Delivered", TotalPrice = 84970 },
            new { UserId = 6, OrderDate = "2026-02-18", Status = "Shipped", TotalPrice = 54990 },
            new { UserId = 7, OrderDate = "2026-03-01", Status = "Delivered", TotalPrice = 9990 },
            new { UserId = 8, OrderDate = "2026-03-12", Status = "Processing", TotalPrice = 42990 },
            new { UserId = 9, OrderDate = "2026-03-20", Status = "Delivered", TotalPrice = 12990 },
            new { UserId = 10, OrderDate = "2026-04-01", Status = "Delivered", TotalPrice = 79990 },
            new { UserId = 11, OrderDate = "2026-04-10", Status = "Shipped", TotalPrice = 34970 },
            new { UserId = 12, OrderDate = "2026-04-22", Status = "Delivered", TotalPrice = 69990 },
            new { UserId = 13, OrderDate = "2026-05-03", Status = "Processing", TotalPrice = 12880 },
            new { UserId = 14, OrderDate = "2026-05-15", Status = "Delivered", TotalPrice = 24990 },
            new { UserId = 15, OrderDate = "2026-06-01", Status = "Delivered", TotalPrice = 119990 },
            new { UserId = 1, OrderDate = "2026-06-15", Status = "Processing", TotalPrice = 15970 },
            new { UserId = 3, OrderDate = "2026-07-01", Status = "Delivered", TotalPrice = 9990 }
        };

                await connection.ExecuteAsync(sql, orders);
            }


            // =========================================================
            // ORDER ITEMS
            // =========================================================

            private static async Task SeedOrderItemsAsync(
                SqliteConnection connection)
            {
                const string sql = """
            INSERT INTO OrderItems
                (OrderId, ProductId, Quantity, UnitPrice)
            VALUES
                (@OrderId, @ProductId, @Quantity, @UnitPrice);
            """;

                var items = new[]
                {
            // Order 1
            new { OrderId = 1, ProductId = 1, Quantity = 1, UnitPrice = 79990 },
            new { OrderId = 1, ProductId = 15, Quantity = 1, UnitPrice = 4990 },

            // Order 2
            new { OrderId = 2, ProductId = 3, Quantity = 1, UnitPrice = 24990 },

            // Order 3
            new { OrderId = 3, ProductId = 4, Quantity = 1, UnitPrice = 129990 },

            // Order 4
            new { OrderId = 4, ProductId = 8, Quantity = 1, UnitPrice = 29990 },

            // Order 5
            new { OrderId = 5, ProductId = 2, Quantity = 1, UnitPrice = 69990 },
            new { OrderId = 5, ProductId = 17, Quantity = 1, UnitPrice = 3990 },
            new { OrderId = 5, ProductId = 15, Quantity = 1, UnitPrice = 4990 },
            new { OrderId = 5, ProductId = 9, Quantity = 1, UnitPrice = 8990 },

            // Order 6
            new { OrderId = 6, ProductId = 12, Quantity = 1, UnitPrice = 54990 },

            // Order 7
            new { OrderId = 7, ProductId = 16, Quantity = 1, UnitPrice = 9990 },

            // Order 8
            new { OrderId = 8, ProductId = 14, Quantity = 1, UnitPrice = 42990 },

            // Order 9
            new { OrderId = 9, ProductId = 13, Quantity = 1, UnitPrice = 12990 },

            // Order 10
            new { OrderId = 10, ProductId = 1, Quantity = 1, UnitPrice = 79990 },

            // Order 11
            new { OrderId = 11, ProductId = 7, Quantity = 1, UnitPrice = 24990 },
            new { OrderId = 11, ProductId = 15, Quantity = 2, UnitPrice = 4990 },

            // Order 12
            new { OrderId = 12, ProductId = 2, Quantity = 1, UnitPrice = 69990 },

            // Order 13
            new { OrderId = 13, ProductId = 9, Quantity = 1, UnitPrice = 8990 },
            new { OrderId = 13, ProductId = 17, Quantity = 1, UnitPrice = 3990 },

            // Order 14
            new { OrderId = 14, ProductId = 7, Quantity = 1, UnitPrice = 24990 },

            // Order 15
            new { OrderId = 15, ProductId = 10, Quantity = 1, UnitPrice = 119990 },

            // Order 16
            new { OrderId = 16, ProductId = 18, Quantity = 1, UnitPrice = 5990 },
            new { OrderId = 16, ProductId = 15, Quantity = 2, UnitPrice = 4990 },

            // Order 17
            new { OrderId = 17, ProductId = 16, Quantity = 1, UnitPrice = 9990 }
        };

                await connection.ExecuteAsync(sql, items);
            }


            // =========================================================
            // REVIEWS
            // =========================================================

            private static async Task SeedReviewsAsync(
                SqliteConnection connection)
            {
                const string sql = """
            INSERT INTO Reviews
                (UserId, ProductId, Rating, Comment, CreatedAt)
            VALUES
                (@UserId, @ProductId, @Rating, @Comment, @CreatedAt);
            """;

                var reviews = new[]
                {
            new { UserId = 1, ProductId = 1, Rating = 5, Comment = "Отличный телефон", CreatedAt = "2026-01-20" },
            new { UserId = 2, ProductId = 3, Rating = 4, Comment = "За свои деньги хороший вариант", CreatedAt = "2026-01-25" },
            new { UserId = 4, ProductId = 8, Rating = 5, Comment = "Очень хорошие наушники", CreatedAt = "2026-02-10" },
            new { UserId = 5, ProductId = 2, Rating = 5, Comment = "Отличный смартфон", CreatedAt = "2026-02-20" },
            new { UserId = 6, ProductId = 12, Rating = 4, Comment = "Хороший пылесос", CreatedAt = "2026-02-28" },
            new { UserId = 7, ProductId = 16, Rating = 5, Comment = "Очень удобная мышь", CreatedAt = "2026-03-10" },
            new { UserId = 8, ProductId = 14, Rating = 5, Comment = "Часы понравились", CreatedAt = "2026-03-20" },
            new { UserId = 9, ProductId = 13, Rating = 4, Comment = "Работает хорошо", CreatedAt = "2026-03-30" },
            new { UserId = 10, ProductId = 1, Rating = 5, Comment = "Очень доволен покупкой", CreatedAt = "2026-04-15" },
            new { UserId = 11, ProductId = 7, Rating = 5, Comment = "Отличные наушники", CreatedAt = "2026-04-20" },
            new { UserId = 12, ProductId = 2, Rating = 4, Comment = "Хороший телефон", CreatedAt = "2026-05-01" },
            new { UserId = 13, ProductId = 9, Rating = 4, Comment = "Нормальный звук", CreatedAt = "2026-05-10" },
            new { UserId = 14, ProductId = 7, Rating = 5, Comment = "Все отлично", CreatedAt = "2026-05-25" },
            new { UserId = 15, ProductId = 10, Rating = 5, Comment = "Отличный телевизор", CreatedAt = "2026-06-10" },
            new { UserId = 3, ProductId = 16, Rating = 5, Comment = "Очень удобная мышь", CreatedAt = "2026-07-10" }
        };

                await connection.ExecuteAsync(sql, reviews);
            }
        }
    }

