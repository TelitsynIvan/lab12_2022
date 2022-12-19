namespace WebApplication1.Models;

public class Part
{
    public int Id { get; set; }
    public int AssemblyId { get; set; }
    public Assembly Assembly { get; set; }
    public int DetailId { get; set; }
    public string DetailName { get; set; }
    public Detail Detail { get; set; }
    public int Quantity { get; set; }
}