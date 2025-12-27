using EBOOK.Data;
using EBOOK.Models;
using Microsoft.AspNetCore.Mvc;   
using Microsoft.EntityFrameworkCore;

namespace EBOOK.Controllers
{
    public class CoursesController : Controller
    {
        private readonly AppDbContext _context;

        public CoursesController(AppDbContext context)
        {
            _context = context;
        }

        // ==========================================================
        // ACCÈS PUBLIC : Tout le monde peut voir l'emploi du temps
        // ==========================================================
        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses
                                        .Include(c => c.Classroom)
                                        .Include(c => c.Teacher)
                                        .ToListAsync();
            return View(courses);
        }

        // ==========================================================
        // ACCÈS RÉSERVÉ : Création (Admin uniquement)
        // ==========================================================
        public IActionResult Create()
        {
            if (!UserIsAdmin()) return Forbid();

            ViewBag.Classrooms = _context.Classrooms.ToList();
            ViewBag.Teachers = _context.Users.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,ClassroomId,TeacherId")] Course course)
        {
            if (!UserIsAdmin()) return Forbid();

            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Classrooms = _context.Classrooms.ToList();
            ViewBag.Teachers = _context.Users.ToList();
            return View(course);
        }

        // ==========================================================
        // ACCÈS RÉSERVÉ : Édition (Admin uniquement)
        // ==========================================================
        public async Task<IActionResult> Edit(int? id)
        {
            if (!UserIsAdmin()) return Forbid();

            if (id == null) return NotFound();

            var course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound();

            ViewBag.Classrooms = _context.Classrooms.ToList();
            ViewBag.Teachers = _context.Users.ToList();
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ClassroomId,TeacherId")] Course course)
        {
            if (!UserIsAdmin()) return Forbid();

            if (id != course.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Classrooms = _context.Classrooms.ToList();
            ViewBag.Teachers = _context.Users.ToList();
            return View(course);
        }

        // ==========================================================
        // ACCÈS RÉSERVÉ : Suppression (Admin uniquement)
        // ==========================================================
        public async Task<IActionResult> Delete(int? id)
        {
            if (!UserIsAdmin()) return Forbid();

            if (id == null) return NotFound();

            var course = await _context.Courses
                .Include(c => c.Classroom)
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (course == null) return NotFound();

            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!UserIsAdmin()) return Forbid();

            var course = await _context.Courses.FindAsync(id);
            if (course != null) 
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // ==========================================================
        // LOGIQUE DE SÉCURITÉ ET VÉRIFICATIONS
        // ==========================================================
        
        private bool UserIsAdmin()
        {
            // Simulation : laisser à 'true' pour tester tes boutons.
            // Passer à 'false' pour simuler un utilisateur bloqué.
            return true; 
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
        
    } // Fin de la classe CoursesController
}