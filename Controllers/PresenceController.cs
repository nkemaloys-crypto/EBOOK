using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EBOOK.Data;   // Pour trouver AppDbContext
using EBOOK.Models; // Pour trouver TakeAttendanceViewModel

namespace EBOOK.Controllers
{
    public class PresenceController : Controller
    {
        // On utilise bien AppDbContext ici
        private readonly AppDbContext _context; 

        public PresenceController(AppDbContext context)
        {
            _context = context;
        }

        // Action pour afficher l'historique
        public async Task<IActionResult> Index()
        {
            var history = await _context.Presences
                .Include(p => p.Student)
                .Include(p => p.Course)
                .OrderByDescending(p => p.Date)
                .ToListAsync();
            return View(history);
        }

        // GET: /Presence/TakeAttendance
        public async Task<IActionResult> TakeAttendance(int courseId)
        {
            var course = await _context.Courses
                .Include(c => c.Classroom)
                .ThenInclude(cl => cl.Students)
                .FirstOrDefaultAsync(c => c.Id == courseId);

            if (course == null) return NotFound();

            var viewModel = new TakeAttendanceViewModel
            {
                CourseId = course.Id,
                CourseTitle = course.Title,
                ClassroomName = course.Classroom.Name,
                Students = course.Classroom.Students.Select(s => new StudentAttendanceItem
                {
                    StudentId = s.Id,
                    FullName = $"{s.FirstName} {s.LastName}"
                }).ToList()
            };

            return View(viewModel);
        }

        // POST: /Presence/TakeAttendance
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TakeAttendance(TakeAttendanceViewModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in model.Students)
                {
                    var presence = new Presence
                    {
                        Date = model.Date,
                        CourseId = model.CourseId,
                        StudentId = item.StudentId,
                        IsPresent = item.IsPresent,
                        Remarks = item.Remarks
                    };
                    _context.Presences.Add(presence);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}