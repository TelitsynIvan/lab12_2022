namespace WebApplication1.Models;

public class Building
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<AuditoriumGroup> AuditoriumGroups { get; set; } = new();
}