using System.Collections.Generic;
using System.Linq;

public class PlayerStatsManager
{
    private static PlayerStatsManager _instance;
    public PlayerStats PlayerStats = new();
    

    public static PlayerStatsManager Instance
    {
        get { return _instance ??= new PlayerStatsManager(); }
    }

    public List<PlayerStats> GetTopPlayers()
    {
        string query = "SELECT * FROM PLAYER_STATS INNER JOIN PLAYER ON PLAYER_STATS.ID = PLAYER.ID ORDER BY SCORE DESC LIMIT 5";

        List<PlayerStats> topPlayers = DatabaseHelper.ProcessSelectStatement(
            query,
            dataReader => new PlayerStats {
                Id = dataReader.GetInt32(0),
                Score = dataReader.GetInt32(1),
                KillCount = dataReader.GetInt32(2),
                DeathCount = dataReader.GetInt32(3),
                PlayerName = dataReader.GetString(4)
            }
        );

        return topPlayers;
    }

    public PlayerStats GetPlayerStats(string playerName)
    {
        string query = $"SELECT * FROM PLAYER_STATS\r\nWHERE ID = (SELECT ID FROM PLAYER WHERE PLAYER_NAME = {playerName})";
        List<PlayerStats> playerStatsList = DatabaseHelper.ProcessSelectStatement(
            query,
            dataReader => new PlayerStats {
                Id = dataReader.GetInt32(0),
                Score = dataReader.GetInt32(1),
                KillCount = dataReader.GetInt32(2),
                DeathCount = dataReader.GetInt32(3)
            }
        );

        return playerStatsList.FirstOrDefault();
    }

    public void SavePlayerStats()
    {
        if (PlayerStats.isNewPlayer)
        {
            string query = @"INSERT INTO PLAYER (PLAYER_NAME)
                            VALUES (@PLAYER_NAME)

                            INSERT INTO PLAYER_STATS (ID, SCORE, KILLCOUNT, DEATH_COUNT)
                            VALUES (LAST_INSERT_ROWID(), @SCORE, @KILLCOUNT, @DEATH_COUNT);";

            DatabaseHelper.ProcessInsertStatement(query, PlayerStats);
        }
        else
        {
            string query = "UPDATE PLAYER_STATS SET Score = @Score, KillCount = @KillCount, DeathCount = @DeathCount WHERE Id = @Id";
            DatabaseHelper.ProcessUpdateStatement(query, PlayerStats);
        }
    }

   
}
