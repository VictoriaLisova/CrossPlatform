using Lab5.Models;
using Lab5ClassLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers
{
    [Authorize]
    public class Lab3Controller : Controller
    {
        private ILabNumber lab;
        private Lab3Model model;
        public Lab3Controller()
        {
            lab = new Lab3();
            model = new Lab3Model();
        }
        public IActionResult Index()
        {
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(int N, int D, int V, int R, string trip)
        {
            char[] split = new char[] { ' ', ',', '.', ':', ';' };
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string file_name = "input.txt";
            if(N > 0 && D > 0 && V > 0 && V <= N && R > 0 && !string.IsNullOrEmpty(trip))
            {
                model.N = N;
                model.D = D;
                model.V = V;
                model.R = R;
                var trips = trip.Split(split).Where(x => x != "").ToList();
                if(trips.Count / 4 == R)
                {
                    model.Trips = trip;
                    var file = System.IO.File.Create(Path.Combine(path, file_name));
                    using (var sw = new StreamWriter(file))
                    {
                        sw.WriteLine($"{N} {D} {V} {R} {string.Join(' ', trips)}");
                    }
                    lab.PathToInputFile = Path.Combine(path, file_name);
                    model.Result = int.Parse(lab.Run());
                    System.IO.File.Delete(Path.Combine(path, file_name));
                }
            }
            return View(model);
        }

        public IActionResult Reset()
        {
            lab = new Lab3();
            model = new Lab3Model();
            return RedirectToAction("Index");
        }
    }
}
