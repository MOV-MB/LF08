using Mono.Data.Sqlite;
using System.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class DatabaseHelper : MonoBehaviour
{
    private static string _devPathDB;
    private static string _pathDB;
    private static string _localPath;

    private void Awake()
    {
        InitializeDatabase();
    }

    private static void InitializeDatabase()
    {
        _devPathDB = "URI=file:" + "F:\\GitHubResp\\LF08\\LF08_Unity\\Assets\\DB\\Gamedb.db";
        _pathDB = "URI=file:" + Directory.GetCurrentDirectory() + "\\Gamedb.db";
        _localPath = Directory.GetCurrentDirectory() + "\\Assets\\DB\\Gamedb.db";

        Debug.Log(_localPath);

        CreateDBFile();
        IDbConnection dbConnection = CreateDbConnection();
        CreateTables(dbConnection);
        dbConnection.Close();
    }

    // ReSharper disable once InconsistentNaming
    private static void CreateDBFile()
    {
        if (File.Exists(_localPath)) return;
        File.Create(_localPath);

        Debug.Log("DB File created");
    }

    private static void CreateTables(IDbConnection dbConnection)
    {
        if (dbConnection == null) return;

        IDbCommand createTablesCommand = dbConnection.CreateCommand();
        Debug.Log("Running CreateTables");
        createTablesCommand.CommandText = @"
        CREATE TABLE PLAYER (
        ID          INTEGER   NOT NULL
                          PRIMARY KEY,
                          AUTOINCREMENT
                          
        PLAYER_NAME TEXT NOT NULL
        ); 
        CREATE TABLE PLAYER_STATS (
        ID          INTEGER REFERENCES Player(ID) 
                        NOT NULL
                        PRIMARY KEY,
        SCORE 	   INTEGER,                    
        KILLCOUNT  INTEGER,
        DEATH_COUNT INTEGER
);
        ";

        try
        {
            createTablesCommand.ExecuteReader();
        }
        catch (SqliteException sqliteException)
        {
            Debug.Log("Tables are already initialized Skipping...");
        }
        
    }

    private static IDbConnection CreateDbConnection()
    {
        Debug.Log("Establishing connection...");
        IDbConnection dbConnection = new SqliteConnection(_devPathDB);
        dbConnection.Open();
        Debug.Log("Connection established");

        return dbConnection;
    }

    public static List<T> ProcessSelectStatement<T>(string query, string playerName, Func<IDataReader, T> mapData)
    {
        IDbConnection dbConnection = CreateDbConnection();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = query;

        if (playerName != null)
        {
            IDbDataParameter dbDataParameter = dbCommand.CreateParameter();
            dbDataParameter.ParameterName = "@playerName";
            dbDataParameter.Value = playerName;
            dbCommand.Parameters.Add(dbDataParameter);
        }
        List<T> results = new();
        try
        {
            IDataReader dataReader = dbCommand.ExecuteReader();

            while (dataReader.Read())
            {
                T obj = mapData(dataReader);
                results.Add(obj);
            }

            dbConnection.Close();

        }
        catch (SqliteException e)
        {
            Debug.Log(e.Message);
        }
        return results;
    }
    public static void ProcessInsertStatement(string query, Dictionary<string, object> parameters)
    {
        IDbConnection dbConnection = CreateDbConnection();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = query;

        // Set the values of the parameters in the INSERT query
        foreach (var parameter in parameters)
        {
            IDbDataParameter dbParameter = dbCommand.CreateParameter();
            dbParameter.ParameterName = parameter.Key;
            dbParameter.Value = parameter.Value;
            dbCommand.Parameters.Add(dbParameter);
        }

        // Execute the INSERT query
        dbCommand.ExecuteNonQuery();

        dbConnection.Close();
    }

    public static void ProcessBasicInsertStatement<T>(string query, T obj)
    {
        IDbConnection dbConnection = CreateDbConnection();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = query;

        // Set the values of the parameters in the INSERT query
        foreach (PropertyInfo property in obj.GetType().GetProperties())
        {
            IDbDataParameter parameter = dbCommand.CreateParameter();
            parameter.ParameterName = property.Name;
            parameter.Value = property.GetValue(obj);
            dbCommand.Parameters.Add(parameter);
        }

        // Execute the INSERT query
        dbCommand.ExecuteNonQuery();

        dbConnection.Close();
    }
    public static void ProcessUpdateStatement<T>(string query, T obj)
    {
        IDbConnection dbConnection = CreateDbConnection();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = query;

        // Set the values of the parameters in the UPDATE query
        foreach (PropertyInfo property in obj.GetType().GetProperties())
        {
            IDbDataParameter parameter = dbCommand.CreateParameter();
            parameter.ParameterName = property.Name;
            parameter.Value = property.GetValue(obj);
            dbCommand.Parameters.Add(parameter);
        }

        // Execute the UPDATE query
        dbCommand.ExecuteNonQuery();

        dbConnection.Close();
    }

    
}