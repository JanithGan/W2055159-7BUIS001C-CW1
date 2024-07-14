using Microsoft.AspNetCore.Mvc;
using SmartStudyApp.Data;
using SmartStudyApp.Models;

namespace SmartStudyApp.Controllers;

public class BreaksController(ApplicationDbContext _context) : Controller
{
    public IActionResult Index()
    {
        var breaks = _context.Breaks.ToList() ?? [];
        ViewBag.Breaks = breaks;
        return PartialView("_Breaks", breaks);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("StartTime,EndTime,Notes,IsRecurrent")] Break _break)
    {
        if (!ModelState.IsValid) return View(_break);

        _context.Breaks.Add(_break);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index", "Sessions");
    }
}