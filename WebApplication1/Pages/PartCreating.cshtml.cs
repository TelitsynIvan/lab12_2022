using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;

namespace WebApplication1.Pages;

public class PartCreating : PageModel
{
    private AssemblyContext context;
    [BindProperty] 
    private Assembly Assembly { get; set; } = new Assembly();

    public PartCreating(AssemblyContext db)
    {
        context = db;
    }
    
    public void OnGet()
    {
    }

    public IActionResult OnPost(List<Part> partsData)
    {
        object? data = RouteData.Values["Name"];
        string? name = data.ToString();
        Assembly = context.Assemblies.FirstOrDefault(u => u.Name == name);
        List<Part> PartsforDB = new List<Part>();
        foreach (var VARIABLE in partsData)
        {
            VARIABLE.Assembly = Assembly;
            VARIABLE.AssemblyId = Assembly.Id;
            Detail? detail = context.Details.FirstOrDefault(u => u.Name == VARIABLE.DetailName);
            if (detail != null)
            {
                VARIABLE.Detail = detail;
                VARIABLE.DetailId = detail.Id;
                PartsforDB.Add(VARIABLE);
            }
            else
            {
                return Redirect($"/NoDetailEdit/{VARIABLE.DetailName}");
            }
        }
        context.Parts.AddRange(PartsforDB);
        context.SaveChanges();
        return Redirect("/AssemblyCreatingOk");
    }
}