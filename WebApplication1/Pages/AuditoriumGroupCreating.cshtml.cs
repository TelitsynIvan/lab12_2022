using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;

namespace WebApplication1.Pages;

public class AuditoriumGroupCreating : PageModel
{
    private ApplicationContext context;
    [BindProperty] 
    private Building Building { get; set; } = new Building();

    public AuditoriumGroupCreating(ApplicationContext db)
    {
        context = db;
    }
    
    public void OnGet()
    {
    }

    public IActionResult OnPost(List<AuditoriumGroup> AuditoriumGroupsData)
    {
        object? data = RouteData.Values["Name"];
        string? name = data.ToString();
        Building = context.Buildings.FirstOrDefault(u => u.Name == name);
        List<AuditoriumGroup> AuditoriumGroupsforDB = new List<AuditoriumGroup>();
        foreach (var VARIABLE in AuditoriumGroupsData)
        {
            VARIABLE.Building = Building;
            VARIABLE.BuildingId = Building.Id;
            Auditorium? Auditorium = context.Auditoriums.FirstOrDefault(u => u.Name == VARIABLE.AuditoriumName);
            if (Auditorium != null)
            {
                VARIABLE.Auditorium = Auditorium;
                VARIABLE.BuildingId = Auditorium.Id;
                AuditoriumGroupsforDB.Add(VARIABLE);
            }
            else
            {
                return Redirect($"/NoAuditoriumEdit/{VARIABLE.AuditoriumName}");
            }
        }
        context.AuditoriumGroups.AddRange(AuditoriumGroupsforDB);
        context.SaveChanges();
        return Redirect("/BuildingCreatingOk");
    }
}