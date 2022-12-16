using Mono.Data.Sqlite;
using System.Data;
using System;
using System.IO;
using System.Reflection;
using System.Text;
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
        _devPathDB = "URI=file:" + "F:\\GitHubResp\\LF08\\LF08_Unity\\Assets\\DB\\gamedb.db";
        _pathDB = "URI=file:" + Directory.GetCurrentDirectory() + "\\gamedb.db";
        _localPath = "F:\\GitHubResp\\LF08\\LF08_Unity\\Assets\\DB\\gamedb.db";

        Debug.Log(_pathDB);

        CreateDBFile();

        IDbConnection dbConnection = CreateDbConnection();

        CreateTables(dbConnection);
        //InsertData(dbConnection);
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


        Action<IDataReader, string> processData = (reader, tableNameDbCheck) =>
        {
            if (reader.Read())
            {
                reader.GetString(0);
                
            }
        };
        



        IDbCommand createTablesCommand = dbConnection.CreateCommand();
        Debug.Log("Running CreateTables");
        createTablesCommand.CommandText = @"
        DROP TABLE IF EXISTS PLAYER;
        DROP TABLE IF EXISTS PLAYER_STATS;

        CREATE TABLE PLAYER (
            ID          INTEGER   NOT NULL
                                  PRIMARY KEY,
            PLAYER_NAME TEXT (25) NOT NULL
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
        createTablesCommand.ExecuteReader();
    }

    private static IDbConnection CreateDbConnection()
    {
        Debug.Log("Establishing connection...");
        IDbConnection dbConnection = new SqliteConnection(_pathDB);
        dbConnection.Open();
        Debug.Log("Connection established");

        return dbConnection;
    }

    public static void ProcessSelectStatement<T>(string query, Action<IDataReader, T> processData)
    {
        IDbConnection dbConnection = CreateDbConnection();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = query;
        IDataReader dataReader = dbCommand.ExecuteReader();

        T obj = Activator.CreateInstance<T>();
        while (dataReader.Read()) processData(dataReader, obj);

        dbConnection.Close();
    }

    public static void ProcessInsertStatement<T>(string query, T obj)
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