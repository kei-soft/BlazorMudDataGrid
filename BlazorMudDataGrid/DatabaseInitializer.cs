using System.Data;

using Dapper;

namespace MudBlazorSample.Components
{
    public static class DatabaseInitializer
    {
        public static void Initialize(IDbConnection connection)
        {
            connection.Execute(@"
            CREATE TABLE IF NOT EXISTS Products (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Price REAL NOT NULL,
                Quantity INTEGER NOT NULL
            )");

            // 샘플 데이터 삽입 -  최초 실행 후 주석 처리
            connection.Execute(@"
            INSERT OR IGNORE INTO Products (Name, Price, Quantity) 
            VALUES 
                ('Laptop', 40, 5),
                ('Keyboard', 50, 20),
                ('Mouse', 30, 15)");
        }
    }
}
