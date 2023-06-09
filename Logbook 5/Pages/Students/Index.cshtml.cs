using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models;
using Microsoft.Extensions.Configuration;
using NuGet.Configuration;

namespace ContosoUniversity.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly Data.SchoolContext _context;
        private readonly IConfiguration _configuration;

        public IndexModel(Data.SchoolContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Student> Students { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            NameSort = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
                pageIndex = 1;
            else
                searchString = CurrentFilter;

            CurrentFilter = searchString;
            var studentsIQ = from s in _context.Students select s;

            if (!string.IsNullOrEmpty(searchString))
                studentsIQ = studentsIQ.Where(x => x.LastName.Contains(searchString)
                                                   || x.FirstMidName.Contains(searchString));

            studentsIQ = sortOrder switch
            {
                "name_desc" => studentsIQ.OrderByDescending(s => s.LastName),
                "Date" => studentsIQ.OrderBy(s => s.EnrollmentDate),
                "date_desc" => studentsIQ.OrderByDescending(s => s.EnrollmentDate),
                _ => studentsIQ.OrderBy(s => s.LastName)
            };

            var pageSize = _configuration.GetValue("PageSize", 4);

            Students = await PaginatedList<Student>.CreateAsync(studentsIQ.AsNoTracking(), pageIndex ?? 1,
                pageSize);
        }
    }
}