using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TimeTableGeneratorProject.Models;

namespace TimeTableGeneratorProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GenerateHours(int WorkingDays, int SubjectsPerDay, int TotalSubjects)
        {
            int totalHoursForWeek = WorkingDays * SubjectsPerDay;
            ViewBag.TotalHoursForWeek = totalHoursForWeek;
            ViewBag.WorkingDays = WorkingDays;
            ViewBag.SubjectsPerDay = SubjectsPerDay;
            ViewBag.TotalSubjects = TotalSubjects;
            return View();
        }

        [HttpPost]
        public ActionResult TimeTable(List<SubjectHours> subjectHours, int WorkingDays, int SubjectsPerDay)
        {
            if (subjectHours == null || !subjectHours.Any())
            {
                ViewBag.ErrorMessage = "No subjects provided.";
                return View();
            }

            List<string> subjects = new List<string>();
            foreach (var subject in subjectHours)
            {
                for (int i = 0; i < subject.Hours; i++)
                {
                    subjects.Add(subject.Name);
                }
            }
            Random random = new Random();
            subjects = subjects.OrderBy(x => random.Next()).ToList();
            Console.WriteLine("Shuffled Subjects: " + string.Join(", ", subjects));

            ViewBag.TimeTable = subjects;
            ViewBag.WorkingDays = WorkingDays;
            ViewBag.SubjectsPerDay = SubjectsPerDay;

            return View();
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
}
