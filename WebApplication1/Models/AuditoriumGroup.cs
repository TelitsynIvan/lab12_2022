namespace WebApplication1.Models;

public class AuditoriumGroup
{
    public int Id { get; set; }
    public int BuildingId { get; set; }
    public Building Building { get; set; }
    public int AuditoriumId { get; set; }
    public string AuditoriumName { get; set; }
    public Auditorium Auditorium { get; set; }
    public int Quantity { get; set; }
}