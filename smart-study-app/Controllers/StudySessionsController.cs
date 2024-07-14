using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartStudyApp.Data;
using SmartStudyApp.Models;

namespace SmartStudyApp.Controllers;

public class StudySessionsController(ApplicationDbContext _context) : Controller
{
    public IActionResult Index()
    {
        var studySessions = _context.StudySessions.ToList() ?? [];
        ViewBag.StudySessions = studySessions;
        return PartialView("_StudySessions", studySessions);
    }

    public async Task<IActionResult> Create()
    {
        var student = await _context.Students.FirstOrDefaultAsync();

        // Get all modules
        var modules = await _context.Modules.ToListAsync();
        ViewBag.Modules = modules;
        ViewBag.EnrolledModules = modules.Where(m => student.EnrolledModuleCodes.Contains(m.ModuleCode)).ToList();

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("ModuleCode,StartTime,EndTime,Notes,IsRecurrent")] StudySession studySession)
    {
        if (!ModelState.IsValid) return View(studySession);

        _context.StudySessions.Add(studySession);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index", "Sessions");
    }
}