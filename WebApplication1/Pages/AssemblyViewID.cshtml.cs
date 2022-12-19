using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Pages;

public class AssemblyViewID : PageModel
{
    [BindProperty]
    public Assembly? Assembly { get; set; }

    public List<Part> Parts { get; set; } = new List<Part>();

    public void OnGet(AssemblyContext db)
    {
        object? data = RouteData.Values["Name"];
        string? name = data.ToString();
        Assembly = db.Assemblies.FirstOrDefault(u => u.Name == name);
        db.Parts.Where(u=>u.AssemblyId==Assembly.Id).Load();
        Parts = Assembly.Parts;
    }
}