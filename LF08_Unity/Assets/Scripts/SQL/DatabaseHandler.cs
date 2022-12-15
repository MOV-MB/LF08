using Mono.Data.Sqlite; // 1
using System.Data; // 1
using System;
using UnityEngine;

public class DatabaseHandler : MonoBehaviour
{
    // Resources:
    // https://www.mono-project.com/docs/database-access/providers/sqlite/

    [SerializeField] private int _goldCount = 0;
    private readonly string _path = "URI=file:" + Application.dataPath + "/Database/Database.db"; 

    void Start() 
    {
        IDbConnection dbConnection = CreateAndOpenDatabase();
        IDbCommand dbCommandReadValues = dbConnection.CreateCommand(); 
        dbCommandReadValues.CommandText = "SELECT * FROM HitCountTableSimple"; 
        IDataReader dataReader = dbCommandReadValues.ExecuteReader(); 

        while (dataReader.Read()) 
        {
            _goldCount = dataReader.GetInt32(1); 
        }

        dbConnection.Close(); 
    }

    //
    
    private IDbConnection CreateAndOpenDatabase()
    {
        // Open a connection to the database.
        IDbConnection dbConnection = new SqliteConnection(_path); 
        dbConnection.Open(); // 6

        // Create a table for the gold count in the database if it does not exist yet.
        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand(); 
        dbCommandCreateTable.CommandText = "CREATE TABLE IF NOT EXISTS HitCountTableSimple (id INTEGER PRIMARY KEY, hits INTEGER )"; // 7
        dbCommandCreateTable.ExecuteReader(); // 8
        return dbConnection;
    }
}