using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models;

namespace ContosoUniversity.Pages.Students
{
    public class DetailsModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public DetailsModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        public Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Students == null)
                return NotFound();
            
            var student = await _context.Students
                .Include(x => x.Enrollments)
                .ThenInclude(x => x.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            
            if (student == null)
                return NotFound();
            
            Student = student;

            return Page();
        }
    }
}