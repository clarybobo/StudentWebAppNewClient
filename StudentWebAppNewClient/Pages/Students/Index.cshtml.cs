using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentWebAppNewClient.Data.Interfaces;
using StudentWebAppNewClient.Data.Models;

namespace StudentWebAppNewClient.Pages.Students
{
    public class IndexModel(IStudentService studentService) : PageModel
    {
        private readonly IStudentService studentService = studentService;

        public List<Student> Students { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Students = await studentService.GetStudentsAsync();
        }
    }
}