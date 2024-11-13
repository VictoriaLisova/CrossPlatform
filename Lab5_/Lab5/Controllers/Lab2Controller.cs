using Lab5.Models;
using Lab5ClassLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Threading.Tasks.Dataflow;

namespace Lab5.Controllers
{
    [Authorize]
    public class Lab2Controller : Controller
    {
        private ILabNumber lab;
        private Lab2Model model;
        public Lab2Controller()
        {
            lab = new Lab2();
            model = new Lab2Model();
        }
        public IActionResult Index()
        {
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(int n, int k, int t, string ti, string pi, string si)
        {
            char[] split = new char[] { ' ', '.', ',', ':', ';' };
            string path = AppDomain.CurrentDomain.BaseDirectory;
            if(n > 0 && k > 0 && t > 0 && !string.IsNullOrEmpty(ti) && !string.IsNullOrEmpty(pi) && !string.IsNullOrEmpty(si))
            {
                model.N = n;
                model.K = k;
                model.T = t;
                var time_parts = ti.Split(split).Where(x => x != "").ToList();
                var value_parts = pi.Split(split).Where(x => x != "").ToList();
                var door_openess = si.Split(split).Where(x => x != "").ToList();
                if(time_parts.Count == n && value_parts.Count == n && door_openess.Count == n)
                {
                    model.Ti = ti;
                    model.Pi = pi;
                    model.Si = si;
                    var file = System.IO.File.Create(Path.Combine(path, "input.txt"));
                    using(StreamWriter sw = new StreamWriter(file))
                    {
                        sw.WriteLine($"{model.N} {model.K} {model.T}");
                        sw.WriteLine(string.Join(" ", time_parts));
                        sw.WriteLine(string.Join(" ", value_parts));
                        sw.WriteLine(string.Join(" ", door_openess));
                    }
                    lab.PathToInputFile = Path.Combine(path, "input.txt");
                    string result = lab.Run();
                    if (!string.IsNullOrEmpty(result))
                    {
                        model.Result = int.Parse(result);
                    }
                    System.IO.File.Delete(lab.PathToInputFile);
                }
            }
            return View(model);
        }

        public IActionResult Reset()
        {
            lab = new Lab2();
            model = new Lab2Model();
            return RedirectToAction("Index");
        }
    }
}
