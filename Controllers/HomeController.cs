using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Ajouté pour CountAsync et Include
using EBOOK.Models;
using EBOOK.Data; // Ajouté pour accéder à ton AppDbContext

namespace EBOOK.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context; // Ajouté pour les requêtes SQL

    // On injecte le logger ET le context de base de données
    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    // 1. PAGE D'ACCUEIL : Simple et rapide
    public IActionResult Index()
    {
        return View();
    }

    // 2. TABLEAU DE BORD : Calculs et statistiques
    public async Task<IActionResult> Dashboard()
    {
        // On compte les entités pour les badges du haut
        ViewBag.TotalStudents = await _context.Students.CountAsync();
        ViewBag.TotalCourses = await _context.Courses.CountAsync();
    
        // On compte les absences du jour
        ViewBag.AbsencesToday = await _context.Presences
            .Where(p => p.Date.Date == DateTime.Today && !p.IsPresent)
            .CountAsync();

        // On récupère les 5 derniers cours pour la table du bas
        var recentCourses = await _context.Courses
            .Include(c => c.Classroom)
            .OrderByDescending(c => c.Id)
            .Take(5)
            .ToListAsync();

        return View(recentCourses);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}