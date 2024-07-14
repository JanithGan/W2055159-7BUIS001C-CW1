using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartStudyApp.Data;
using SmartStudyApp.Models;

namespace SmartStudyApp.Controllers;

public class ProfileController(ApplicationDbContext _context) : Controller
{
    public async Task<IActionResult> Index()
    {
        var student = await _context.Students.FirstOrDefaultAsync();
        if (student == null) return NotFound();

        SetModulesForStudent(student);

        return View(student);
    }

    public async Task<IActionResult> Edit()
    {
        var student = await _context.Students.FirstOrDefaultAsync();
        if (student == null) return NotFound();

        SetModulesForStudent(student);

        return View(student);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("StudentId,Name,StudentNumber,EnrolledModuleCodes")]
        Student student)
    {
        if (!ModelState.IsValid) return View(student);

        try
        {
            _context.Update(student);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!StudentExists(student.StudentId))
                return NotFound();
            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    private bool StudentExists(int id)
    {
        return _context.Students.Any(e => e.StudentId == id);
    }

    private async void SetModulesForStudent(Student student)
    {
        var studentModuleCodes = student.EnrolledModuleCodes;

        // Get all modules
        var modules = await _context.Modules.ToListAsync();
        ViewBag.Modules = modules;
        ViewBag.EnrolledModules = modules.Where(m => studentModuleCodes.Contains(m.ModuleCode)).ToList();
        ViewBag.UnEnrolledModules = modules.Where(m => !studentModuleCodes.Contains(m.ModuleCode)).ToList();
    }
}