using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;

namespace WebApplication1.Pages;

public class AllDetails : PageModel
{
    public List<Detail> DisplayedDetails = new List<Detail>();
    public void OnGet(AssemblyContext db)
    {
        DisplayedDetails = db.Details.ToList();
    }
}