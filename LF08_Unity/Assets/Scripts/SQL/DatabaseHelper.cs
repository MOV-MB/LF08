using Mono.Data.Sqlite;
using System.Data; 
using System;
using System.IO;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class DatabaseHelper : MonoBehaviour
{
    private static string _devPath;
    private static string _path;

    void Awake()
    {
        InitializeDatabase();
    }

    private static IDbConnection InitializeDatabase()
    {
        _devPath = "URI=file:" + "F:/GitHubResp/LF08/LF08_Unity/Assets/gamedb.db"; 
        _path = "URI=file:" + Application.dataPath + "gamedb.db"; 

        Debug.Log(_devPath);

        CreateDBFile();

        IDbConnection dbConnection = CreateDbConnection();

        CreateTables(dbConnection);

        return dbConnection ?? throw new NullReferenceException("DB Connection could not be established");
    }

    // ReSharper disable once InconsistentNaming
    private static void CreateDBFile()
    {
        if(!File.Exists(_path))
            File.Create(Application.dataPath + "gamedb.db");

        Debug.Log("DB File created");
    }

    private static void CreateTables(IDbConnection dbConnection)
    {
        if (dbConnection == null) return;
        IDbCommand createTablesCommand = dbConnection.CreateCommand();
        createTablesCommand.ExecuteReader();
    }

    private static IDbConnection CreateDbConnection()
    {
        if (!File.Exists(_path)) return null;
        IDbConnection dbConnection = new SqliteConnection(_devPath); 
        dbConnection.Open();

        return dbConnection;
    }

    public static void ProcessSelectStatement<T>(string query, Action<IDataReader, T> processData)
    {
        IDbConnection dbConnection = InitializeDatabase();
        IDbCommand dbCommand = dbConnection.CreateCommand(); 
        dbCommand.CommandText = query; 
        IDataReader dataReader = dbCommand.ExecuteReader(); 

        T obj = Activator.CreateInstance<T>();
        while (dataReader.Read()) 
        {
            processData(dataReader, obj);
        }

        dbConnection.Close(); 
    }
    public static void ProcessInsertStatement<T>(string query, T obj)
    {
        IDbConnection dbConnection = InitializeDatabase();
        IDbCommand dbCommand = dbConnection.CreateCommand(); 
        dbCommand.CommandText = query; 

        // Set the values of the parameters in the INSERT query
        foreach (var property in obj.GetType().GetProperties())
        {
            var parameter = dbCommand.CreateParameter();
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
        IDbConnection dbConnection = InitializeDatabase();
        IDbCommand dbCommand = dbConnection.CreateCommand(); 
        dbCommand.CommandText = query; 

        // Set the values of the parameters in the UPDATE query
        foreach (var property in obj.GetType().GetProperties())
        {
            var parameter = dbCommand.CreateParameter();
            parameter.ParameterName = property.Name;
            parameter.Value = property.GetValue(obj);
            dbCommand.Parameters.Add(parameter);
        }

        // Execute the UPDATE query
        dbCommand.ExecuteNonQuery();

        dbConnection.Close(); 
    }

    


}