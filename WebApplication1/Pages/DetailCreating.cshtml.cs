using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;

namespace WebApplication1.Pages;

public class DetailCreating : PageModel
{
    //private AssemblyContext _context;
    [BindProperty] 
    public Detail temp { get; set; } = new Detail() { Name = "", Quantity = 0 };
    public string Message { get; private set; } = "Добавление Детали:";
    public void OnGet()
    {
        // Detail test = new Detail() {Name = "nail", Quantity = 1};
        // db.Details.Add(test);
        // db.SaveChanges();
    }

    public void OnPost(AssemblyContext db)
    {
        Detail? dublicat = db.Details.FirstOrDefault(u=>u.Name==temp.Name);
        if (dublicat == null)
        {
           db.Details.Add(temp);
           db.SaveChanges();
           Message = $"Добавлена деталь:\n наименование: {temp.Name}, количество: {temp.Quantity} ";
        }
        else
        {
            Message = "Деталь с таким наименованием уже существует";
        }
    }
}