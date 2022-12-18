using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;

namespace WebApplication1.Pages;

public class DetailDelete : PageModel
{
    [BindProperty]
    public string Name { get; set; } = "";

    public void OnGet()
    {
    }

    public IActionResult OnPost(AssemblyContext db)
    {
        Detail? temp = db.Details.FirstOrDefault(u => u.Name == Name);
        if (temp != null)
        {
            db.Details.Remove(temp);
            db.SaveChanges();
            return Redirect("/DetailDeletedOk");
        }
        else
        {
            return Redirect("/NoDetailDelete");
        }
    }
}