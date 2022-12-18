using System.Collections.Generic;
using System.Linq;

public class PlayerStatsManager
{
    private static PlayerStatsManager _instance;
    public PlayerStats PlayerStatsLocal = new();
    

    public static PlayerStatsManager Instance
    {
        get { return _instance ??= new PlayerStatsManager(); }
    }

    public List<PlayerStats> GetTopPlayers()
    {
        string query = @"SELECT 
                        PS.*, 
                        P.PLAYER_NAME
                        FROM PLAYER_STATS PS
                        INNER JOIN PLAYER P ON PS.ID = P.ID
                        ORDER BY PS.SCORE DESC LIMIT 5";

        List<PlayerStats> topPlayers = DatabaseHelper.ProcessSelectStatement(query, null,dataReader => new PlayerStats {
            Id = dataReader.GetInt32(0),
            Score = dataReader.GetInt64(1),
            KillCount = dataReader.GetInt64(2),
            DeathCount = dataReader.GetInt64(3),
            PlayerName = dataReader.GetString(4) 
        });
        return topPlayers;
    }

    public PlayerStats GetPlayerStats(string playerName)
    {
        string query = "SELECT * FROM PLAYER_STATS WHERE ID = (SELECT ID FROM PLAYER WHERE PLAYER_NAME = @playerName );";
        List<PlayerStats> playerStatsList = DatabaseHelper.ProcessSelectStatement(query, playerName, dataReader => new PlayerStats {
            Id = dataReader.GetInt32(0),
            Score = dataReader.GetInt64(1),
            KillCount = dataReader.GetInt64(2),
            DeathCount = dataReader.GetInt64(3)
        });

        return playerStatsList.FirstOrDefault();
    }

    public void SavePlayerStats()
    {
        if (PlayerStatsLocal.IsNewPlayer)
        {
            string query = @"INSERT INTO PLAYER (PLAYER_NAME) VALUES (@PlayerName);
                            INSERT INTO PLAYER_STATS (ID, SCORE, KILLCOUNT, DEATH_COUNT)
                            VALUES (LAST_INSERT_ROWID(), @Score, @KillCount, @DeathCount);";

            Dictionary<string, object> parameters = new()
            {
                { "@PlayerName",PlayerStatsLocal.PlayerName },
                { "@Score", PlayerStatsLocal.Score},
                { "@KillCount", PlayerStatsLocal.KillCount },
                { "@DeathCount", PlayerStatsLocal.DeathCount}
            };

            DatabaseHelper.ProcessInsertStatement(query, parameters);
        }
        else
        {
            string query = "UPDATE PLAYER_STATS SET Score = Score, KillCount = KillCount, DeathCount = DeathCount WHERE Id = Id";
            DatabaseHelper.ProcessUpdateStatement(query, PlayerStatsLocal);
        }
    }

   
}
