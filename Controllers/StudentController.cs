using EBOOK.Models;
using EBOOK.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering; 

namespace EBOOK.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        // ==========================================================
        // ACCÈS PUBLIC : Tout le monde peut voir la liste
        // ==========================================================
        public async Task<IActionResult> Index()
        {
            var students = await _context.Students
                .Include(s => s.Classroom)
                .ToListAsync();
            return View(students);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var student = await _context.Students
                .Include(s => s.Classroom)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (student == null) return NotFound();

            return View(student);
        }

        // ==========================================================
        // ACCÈS RÉSERVÉ : Seul l'Admin peut modifier
        // ==========================================================
        public async Task<IActionResult> Edit(int? id)
        {
            // VERIFICATION DE SÉCURITÉ
            if (!UserIsAdmin()) return Forbid(); 

            if (id == null) return NotFound();

            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();
            
            ViewBag.ClassroomId = new SelectList(
                await _context.Classrooms.ToListAsync(),
                "Id", "Name", student.ClassroomId
            );
            
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,DateOfBirth,ClassroomId,IsDelegate,IsAdmin")] Student student)
        {
            // VERIFICATION DE SÉCURITÉ
            if (!UserIsAdmin()) return Forbid();

            if (id != student.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Students.Any(e => e.Id == student.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            
            ViewBag.ClassroomId = new SelectList(
                await _context.Classrooms.ToListAsync(),
                "Id", "Name", student.ClassroomId
            );
            
            return View(student);
        }

        // ==========================================================
        // LOGIQUE DE SÉCURITÉ (SIMULATION)
        // ==========================================================
        private bool UserIsAdmin()
        {
            // Pour l'instant, on retourne 'true' pour que tu puisses tester.
            // Quand ton système de Login sera prêt, on vérifiera ici 
            // si l'utilisateur connecté a le flag 'IsAdmin'.
            return true; 
        }
    }
}