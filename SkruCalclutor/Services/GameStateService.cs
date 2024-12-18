using SkruCalclutor.Models;

public class GameStateService
{
    public List<Player> Players { get; set; } = new List<Player>();

    public void InitializePlayers(int numOfPlayer)
    {
        Players.Clear();
        for (int i = 1; i <= numOfPlayer; i++)
        {
            Players.Add(new Player { Name = $"Player {i}", Score = 0, IsFavorite = false });
        }
    }

    public void UpdatePlayerScore(string playerName, int updatedScore)
    {
        var player = Players.FirstOrDefault(p => p.Name == playerName);
        if (player != null)
        {
            player.Score = updatedScore;
            // Find the minimum score among all players
            var minScore = Players.Min(p => p.Score);

            // Mark all players with the minimum score as favorites
            foreach (var p in Players.Where(p => p.Score == minScore))
            {
                p.IsFavorite = true;
            }

            // Ensure that all other players without the minimum score do not have the star
            foreach (var p in Players.Where(p => p.Score != minScore))
            {
                p.IsFavorite = false;
            }
        }
    }
}
