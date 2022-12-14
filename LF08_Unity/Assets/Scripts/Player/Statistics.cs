using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Text;
using System.Data;
using System.Data.SQLite;


    public class Statistics : MonoBehaviour
{
    string help = "https://www.codeguru.com/dotnet/using-sqlite-in-a-c-application/";
    string sqlConnection = "C:/Users/marce/Desktop/Code/Berufsschule/LF8 DB";

    public void getStatistics()
    {
        SQLiteConnection sqlite_conn;
        sqlite_conn = CreateConnection();
    }

    public static SQLiteConnection CreateConnection()
    {
        SQLiteConnection sqlite_conn;
        // Create a new database connection:
        sqlite_conn = new SQLiteConnection("Data Source=" + sqlConnection + "gamedb.db");
         // Open the connection:
         try
        {
            sqlite_conn.Open();
        }
        catch (Exception ex)
        {
            Console.Write("Connection Error");
        }
        return sqlite_conn;
    }

}