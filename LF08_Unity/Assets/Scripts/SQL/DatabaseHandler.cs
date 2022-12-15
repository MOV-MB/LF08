using Mono.Data.Sqlite; // 1
using System.Data; // 1
using System;
using UnityEngine;

public class DatabaseHandler : MonoBehaviour
{
    // Resources:
    // https://www.mono-project.com/docs/database-access/providers/sqlite/

    [SerializeField] private int _goldCount = 0;
    private readonly string _path = "URI=file:" + Application.dataPath + "/Database/Database.db"; // Path to database.

    void Start() // 13
    {
        // Read all values from the table.
        IDbConnection dbConnection = CreateAndOpenDatabase(); // 14
        IDbCommand dbCommandReadValues = dbConnection.CreateCommand(); // 15
        dbCommandReadValues.CommandText = "SELECT * FROM HitCountTableSimple"; // 16
        IDataReader dataReader = dbCommandReadValues.ExecuteReader(); // 17

        while (dataReader.Read()) // 18
        {
            // The `id` has index 0, our `hits` have the index 1.
            _goldCount = dataReader.GetInt32(1); // 19
        }

        // Remember to always close the connection at the end.
        dbConnection.Close(); // 20
    }

    private IDbConnection CreateAndOpenDatabase() // 3
    {
        // Open a connection to the database.
        IDbConnection dbConnection = new SqliteConnection(_path); // 5
        dbConnection.Open(); // 6

        // Create a table for the hit count in the database if it does not exist yet.
        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand(); // 6
        dbCommandCreateTable.CommandText = "CREATE TABLE IF NOT EXISTS HitCountTableSimple (id INTEGER PRIMARY KEY, hits INTEGER )"; // 7
        dbCommandCreateTable.ExecuteReader(); // 8
        Animator animator = GetComponent<Animator>();
        return dbConnection;
    }
}