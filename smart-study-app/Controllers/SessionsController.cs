using System.Collections;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using SmartStudyApp.Data;
using SmartStudyApp.Models;

namespace SmartStudyApp.Controllers;

public class SessionsController(ApplicationDbContext _context) : Controller
{
    public IActionResult Index()
    {
        ViewBag.Breaks = _context.Breaks.ToList() ?? [];
        ViewBag.StudySessions = _context.StudySessions.ToList() ?? [];
        return View();
    }

    public IActionResult ExportReport()
    {
        var studySessions = _context.StudySessions.ToList()
            .Select(s => new ReportRecord
            {
                Type = "StudySession",
                Module = s.ModuleCode,
                StartTime = s.StartTime.ToString(),
                EndTime = s.EndTime.ToString(),
                IsRecurrent = s.IsRecurrent,
                Notes = s.Notes
            }).ToList();

        var breaks = _context.Breaks.ToList()
            .Select(s => new ReportRecord
            {
                Type = "Break",
                Module = "None",
                StartTime = s.StartTime.ToString(),
                EndTime = s.EndTime.ToString(),
                IsRecurrent = s.IsRecurrent,
                Notes = s.Notes
            }).ToList();

        var combinedData = studySessions.Concat(breaks).ToList();
        combinedData.Sort((x, y) => x.StartTime.CompareTo(y.StartTime));

        // Assign row numbers
        for (var i = 0; i < combinedData.Count; i++) combinedData[i].Number = i + 1;

        using var memoryStream = new MemoryStream();
        using var streamWriter = new StreamWriter(memoryStream);
        using var csvWriter = new CsvWriter(streamWriter, new CsvConfiguration(CultureInfo.InvariantCulture));

        csvWriter.WriteRecords((IEnumerable)combinedData);
        streamWriter.Flush();
        return File(memoryStream.ToArray(), "text/csv", DateTime.Now + "-smart-study-report.csv");
    }
}