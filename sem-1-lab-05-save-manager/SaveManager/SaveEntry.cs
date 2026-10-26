using Newtonsoft.Json;
namespace SaveManager;

public class SaveEntry
{
    public string Name { get; set; }
    public string File { get; set; }
    public DateTime UpdatedAt { get; set; }
}