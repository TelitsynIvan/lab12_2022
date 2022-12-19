using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Pages;

public class AssemblyViewAll : PageModel
{
    [BindProperty] 
    public List<Assembly> Assemblies { get; set; } = new List<Assembly>();

    public void OnGet(AssemblyContext db)
    {
        Assemblies = db.Assemblies.ToList();
        db.Parts.Load();
    }
}