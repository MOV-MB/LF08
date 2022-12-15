using System;
using Mono.Data.Sqlite;
using System.Data;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Statistics : MonoBehaviour
{
     // help: https://www.codeguru.com/dotnet/using-sqlite-in-a-c-application/
    private static readonly string _sqlConnection = "C:/Users/marce/Desktop/Code/Berufsschule/LF8 DB";

    public void GetStatistics()
    {
        SQLiteConnection sqliteConn = CreateConnection();
    }

    public static SQLiteConnection CreateConnection()
    {
        SQLiteConnection sqliteConn;
        // Create a new database connection:
        sqliteConn = new SQLiteConnection("Data Source=" + _sqlConnection + "gamedb.db");
        // Open the connection:
        try
        {
            // sqlite_conn.Open();
        }
        catch (Exception ex)
        {
            Debug.Log("Error: " + ex.Message);
        }

        return sqliteConn;
    }
}