using SQLite;

namespace Contexts.Models;

public class SavedPlayer
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; } = "No Name";
}
