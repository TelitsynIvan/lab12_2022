using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Pages;

public class AssemblyDeleting : PageModel
{
    [BindProperty]
    public string Name { get; set; } = "";

    public void OnGet()
    {
    }

    public IActionResult OnPost(AssemblyContext db)
    {
        Assembly? temp = db.Assemblies.FirstOrDefault(u => u.Name == Name);
        db.Parts.Where(u => u.AssemblyId == temp.Id).Load();
        if (temp != null)
        {
            db.Assemblies.Remove(temp);
            db.SaveChanges();
            return Redirect("/AssemblyDeletedOk");
        }
        else
        {
            return Redirect("/NoAssemblyDelete");
        }
    }
}