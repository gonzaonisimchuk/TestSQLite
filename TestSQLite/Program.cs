namespace TestSQLite
{
    using System;
    using Microsoft.Data.Sqlite;

    class Program
    {
        static void Main(string[] args)
        {
            var db = new SqliteConnection("Filename=sqlite.db");
            db.Open();
            var tableCommand = "CREATE TABLE IF NOT EXISTS Personas (Id INTEGER PRIMARY KEY AUTOINCREMENT, Nombre NVARCHAR(200) NULL)";
            var createTable = new SqliteCommand(tableCommand, db);
            createTable.ExecuteReader();

            //var command = new SqliteCommand("INSERT INTO Personas VALUES (NULL, @Entry)", db);
            var command = new SqliteCommand("Update Personas set Nombre = @Entry Where Id = 4", db);
            //Use parameterized query to prevent SQL injection attacks
            Console.WriteLine("Nombre:");
            var nombre = Console.ReadLine();
            command.Parameters.AddWithValue("@Entry", nombre);
            command.ExecuteReader();

            Console.WriteLine("Nombres registrados:");
            var query = new SqliteCommand("SELECT * from Personas", db).ExecuteReader();
            while (query.Read())
            {
                Console.WriteLine($"{query.GetInt32(0)}, {query.GetString(1)}");
            }
            Console.ReadLine();
            db.Close();
        }
    }
}
