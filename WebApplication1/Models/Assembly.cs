namespace WebApplication1.Models;

public class Assembly
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<Part> Parts { get; set; } = new();
}