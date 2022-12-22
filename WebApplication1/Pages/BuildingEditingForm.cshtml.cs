using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Pages;

public class BuildingEditingForm : PageModel
{
    private ApplicationContext context;
    [BindProperty]
    public Building? Building { get; set; } = new Building() { Name = "", Id = 0};
    public List<AuditoriumGroup> AuditoriumGroupsTemp { get; set; } = new List<AuditoriumGroup>();

    public BuildingEditingForm(ApplicationContext db)
    {
        context = db;
    }
    
    public void OnGet()
    {
        object? data = RouteData.Values["Name"];
        string? name = data.ToString();
        Building = context.Buildings.FirstOrDefault(u => u.Name == name);
        context.AuditoriumGroups.Where(u => u.BuildingId == Building.Id).Load();
        AuditoriumGroupsTemp = Building.AuditoriumGroups.ToList();
    }

    public IActionResult OnPost(List<AuditoriumGroup> AuditoriumGroupsData)
    {
        List<AuditoriumGroup> OldAuditoriumGroups = context.AuditoriumGroups.Where(u=>u.BuildingId==Building.Id).ToList();
        List<AuditoriumGroup> AuditoriumGroupsToDelete = new List<AuditoriumGroup>();
        List<AuditoriumGroup> AuditoriumGroupsToAdd = new List<AuditoriumGroup>();
        List<AuditoriumGroup> AuditoriumGroupsToUpdate = new List<AuditoriumGroup>();
        if (AuditoriumGroupsData.Count < OldAuditoriumGroups.Count)
        {
            foreach (var VARIABLE in OldAuditoriumGroups)
            {
                if (!AuditoriumGroupsData.Exists(u => u.AuditoriumName == VARIABLE.AuditoriumName))
                {
                    AuditoriumGroupsToDelete.Add(VARIABLE);
                }
                else
                {
                    AuditoriumGroupsToUpdate.Add(VARIABLE);
                }
            }
        }

        if (AuditoriumGroupsData.Count > OldAuditoriumGroups.Count)
        {
            foreach (var VARIABLE in AuditoriumGroupsData)
            {
                if (!OldAuditoriumGroups.Exists(u => u.AuditoriumName == VARIABLE.AuditoriumName))
                {
                    Auditorium? Auditorium = context.Auditoriums.FirstOrDefault(u => u.Name == VARIABLE.AuditoriumName);
                    if (Auditorium != null)
                    {
                      AuditoriumGroupsToAdd.Add(VARIABLE);  
                    }
                    else
                    {
                        return Redirect($"/NoAuditoriumEdit/{VARIABLE.AuditoriumName}");
                    }
                }
                else
                {
                    AuditoriumGroupsToUpdate.Add(VARIABLE);
                }
            }
        }

        if (AuditoriumGroupsData.Count == OldAuditoriumGroups.Count)
        {
            AuditoriumGroupsToUpdate = AuditoriumGroupsData;
        }

        if (AuditoriumGroupsToDelete.Count != 0)
        {
            context.AuditoriumGroups.RemoveRange(AuditoriumGroupsToDelete);
        }
        
        if (AuditoriumGroupsToAdd.Count != 0)
        {
            for (int i = 0; i < AuditoriumGroupsToAdd.Count; i++)
            {
                AuditoriumGroupsToAdd[i].BuildingId = Building.Id;
                Auditorium? Auditorium = context.Auditoriums.FirstOrDefault(u => u.Name == AuditoriumGroupsToAdd[i].AuditoriumName);
                AuditoriumGroupsToAdd[i].AuditoriumId = Auditorium.Id;
                AuditoriumGroupsToAdd[i].Auditorium = Auditorium;
            }
            context.AuditoriumGroups.AddRange(AuditoriumGroupsToAdd); 
        }

        if (AuditoriumGroupsToUpdate.Count != 0)
        {
            for (int i = 0; i < AuditoriumGroupsToUpdate.Count; i++)
            {
                OldAuditoriumGroups[OldAuditoriumGroups.FindIndex(u => u.AuditoriumName == AuditoriumGroupsToUpdate[i].AuditoriumName)].Quantity =
                    AuditoriumGroupsToUpdate[i].Quantity;
            }
            context.AuditoriumGroups.UpdateRange(OldAuditoriumGroups);
        }
        else
        {
            context.AuditoriumGroups.UpdateRange(OldAuditoriumGroups);
        }

        Building.AuditoriumGroups = context.AuditoriumGroups.Where(u => u.BuildingId == Building.Id).ToList();
        context.Buildings.Update(Building);
        context.SaveChanges();
        return Redirect("/BuildingEditingOk");
    }
}