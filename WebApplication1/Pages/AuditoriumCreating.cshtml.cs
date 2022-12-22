using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;

namespace WebApplication1.Pages;

public class AuditoriumCreating : PageModel
{
    //private AssemblyContext _context;
    [BindProperty] 
    public Auditorium temp { get; set; } = new Auditorium() { Name = "", NumberOfSeats = 0, Description = ""};
    public string Message { get; private set; } = "Добавление Аудитории:";
    public void OnGet()
    {
        // Auditorium test = new Auditorium() {Name = "nail", NumberOfSeats = 1};
        // db.Auditoriums.Add(test);
        // db.SaveChanges();
    }

    public void OnPost(ApplicationContext db)
    {
        Auditorium? dublicat = db.Auditoriums.FirstOrDefault(u=>u.Name==temp.Name);
        if (dublicat == null)
        {
           db.Auditoriums.Add(temp);
           db.SaveChanges();
           Message = $"Добавлена аудитория:\n наименование: {temp.Name}, количество посадочных мест: {temp.NumberOfSeats} ";
        }
        else
        {
            Message = "Аудитория с таким наименованием уже существует";
        }
    }
}