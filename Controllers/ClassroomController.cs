// C:\Users\dimit\EBOOK\Controllers\ClassroomController.cs

using EBOOK.Data;
using EBOOK.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EBOOK.Controllers
{
    public class ClassroomController : Controller
    {
        private readonly AppDbContext _context;

        public ClassroomController(AppDbContext context)
        {
            _context = context;
        }

        // ==========================================================
        // 1. INDEX (READ All)
        // ==========================================================
        // GET: /Classroom
        public async Task<IActionResult> Index()
        {
            // Récupère toutes les classes
            var classrooms = await _context.Classrooms
                .OrderByDescending(c => c.Year) // Classe par année décroissante
                .ToListAsync();
            return View(classrooms);
        }
        
        // ==========================================================
        // 2. CREATE (GET)
        // ==========================================================
        // GET: /Classroom/Create
        public IActionResult Create()
        {
            return View();
        }

        // ==========================================================
        // 2. CREATE (POST)
        // ==========================================================
        // POST: /Classroom/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Year")] Classroom classroom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classroom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(classroom);
        }

        // ==========================================================
        // 3. DETAILS (READ One)
        // ==========================================================
        // GET: /Classroom/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Récupère la classe en incluant les étudiants et les cours pour l'affichage
            var classroom = await _context.Classrooms
                .Include(c => c.Students) // Liste des étudiants
                .Include(c => c.Courses) // Liste des cours
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (classroom == null)
            {
                return NotFound();
            }

            return View(classroom);
        }

        // ==========================================================
        // 4. EDIT (GET)
        // ==========================================================
        // GET: /Classroom/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classroom = await _context.Classrooms.FindAsync(id);
            if (classroom == null)
            {
                return NotFound();
            }
            return View(classroom);
        }

        // ==========================================================
        // 4. EDIT (POST)
        // ==========================================================
        // POST: /Classroom/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Year")] Classroom classroom)
        {
            if (id != classroom.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classroom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Classrooms.Any(e => e.Id == classroom.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(classroom);
        }
        
        // ==========================================================
        // 5. DELETE (GET)
        // ==========================================================
        // GET: /Classroom/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Important: Inclure les étudiants pour afficher un avertissement de suppression des enfants
            var classroom = await _context.Classrooms
                .Include(c => c.Students)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (classroom == null)
            {
                return NotFound();
            }

            return View(classroom);
        }

        // ==========================================================
        // 5. DELETE (POST confirmed)
        // ==========================================================
        // POST: /Classroom/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var classroom = await _context.Classrooms
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (classroom != null)
            {
                // Note : EF Core (avec SQL Server) gère souvent la suppression en cascade 
                // des étudiants si vous l'avez configuré dans OnModelCreating (ou par défaut). 
                // Sinon, vous devriez supprimer explicitement les étudiants ici.
                
                _context.Classrooms.Remove(classroom);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}