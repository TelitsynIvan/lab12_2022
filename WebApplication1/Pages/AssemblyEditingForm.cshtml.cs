using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Pages;

public class AssemblyEditingForm : PageModel
{
    private AssemblyContext context;
    [BindProperty]
    public Assembly? Assembly { get; set; } = new Assembly() { Name = "", Id = 0};
    public List<Part> PartsTemp { get; set; } = new List<Part>();

    public AssemblyEditingForm(AssemblyContext db)
    {
        context = db;
    }
    
    public void OnGet()
    {
        object? data = RouteData.Values["Name"];
        string? name = data.ToString();
        Assembly = context.Assemblies.FirstOrDefault(u => u.Name == name);
        context.Parts.Where(u => u.AssemblyId == Assembly.Id).Load();
        PartsTemp = Assembly.Parts.ToList();
    }

    public IActionResult OnPost(List<Part> PartsData)
    {
        List<Part> OldParts = context.Parts.Where(u=>u.AssemblyId==Assembly.Id).ToList();
        List<Part> PartsToDelete = new List<Part>();
        List<Part> PartsToAdd = new List<Part>();
        List<Part> PartsToUpdate = new List<Part>();
        if (PartsData.Count < OldParts.Count)
        {
            foreach (var VARIABLE in OldParts)
            {
                if (!PartsData.Exists(u => u.DetailName == VARIABLE.DetailName))
                {
                    PartsToDelete.Add(VARIABLE);
                }
                else
                {
                    PartsToUpdate.Add(VARIABLE);
                }
            }
        }

        if (PartsData.Count > OldParts.Count)
        {
            foreach (var VARIABLE in PartsData)
            {
                if (!OldParts.Exists(u => u.DetailName == VARIABLE.DetailName))
                {
                    Detail? detail = context.Details.FirstOrDefault(u => u.Name == VARIABLE.DetailName);
                    if (detail != null)
                    {
                      PartsToAdd.Add(VARIABLE);  
                    }
                    else
                    {
                        return Redirect($"/NoDetailEdit/{VARIABLE.DetailName}");
                    }
                }
                else
                {
                    PartsToUpdate.Add(VARIABLE);
                }
            }
        }

        if (PartsData.Count == OldParts.Count)
        {
            PartsToUpdate = PartsData;
        }

        if (PartsToDelete.Count != 0)
        {
            context.Parts.RemoveRange(PartsToDelete);
        }
        
        if (PartsToAdd.Count != 0)
        {
            for (int i = 0; i < PartsToAdd.Count; i++)
            {
                PartsToAdd[i].AssemblyId = Assembly.Id;
                Detail? detail = context.Details.FirstOrDefault(u => u.Name == PartsToAdd[i].DetailName);
                PartsToAdd[i].DetailId = detail.Id;
                PartsToAdd[i].Detail = detail;
                if (OldParts.Count != 0)
                {
                    PartsToAdd[i].Id = OldParts.Find(u => u.DetailId == PartsToAdd[i].DetailId).Id;
                }
            }
            context.Parts.AddRange(PartsToAdd); 
        }

        if (PartsToUpdate.Count != 0)
        {
            for (int i = 0; i < PartsToUpdate.Count; i++)
            {
                OldParts[OldParts.FindIndex(u => u.DetailName == PartsToUpdate[i].DetailName)].Quantity =
                    PartsToUpdate[i].Quantity;
            }
            context.Parts.UpdateRange(OldParts);
        }
        else
        {
            context.Parts.UpdateRange(OldParts);
        }

        Assembly.Parts = context.Parts.Where(u => u.AssemblyId == Assembly.Id).ToList();
        context.Assemblies.Update(Assembly);
        context.SaveChanges();
        return Redirect("/AssemblyEditingOk");
    }
}