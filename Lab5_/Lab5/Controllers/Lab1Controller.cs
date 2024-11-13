using Lab5.Models;
using Lab5ClassLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Lab5.Controllers
{
    [Authorize]
    public class Lab1Controller : Controller
    {
        private ILabNumber lab;
        private Lab1Model model;
        public Lab1Controller()
        {
            lab = new Lab1();
            model = new Lab1Model();
        }
        public IActionResult Index()
        {
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Index(int words_num, int words_search_num, string maze_words, string search_words)
        {
            char[] split = new char[]{ ' ', ',', '.', ':', ';' };
            string path = AppDomain.CurrentDomain.BaseDirectory;
            if(words_num != 0 && words_search_num != 0 && !string.IsNullOrEmpty(maze_words) && !string.IsNullOrEmpty(search_words))
            {
                if(words_num > words_search_num)
                {
                    model.Input_N = words_num;
                    model.Input_M = words_search_num;
                }
                var maze = maze_words.Split(split).Where(x => x != ""&& x.Length == words_num).ToList();
                var words = search_words.Split(split).Where(y => y != "").ToList();

                maze.ForEach(x => x.ToUpper());
                words.ForEach(x => x.ToUpper());

                if (maze.Count == words_num && words.Count == words_search_num)
                {
                    model.Maze_Words = maze_words;
                    model.Search_words = search_words;
                   
                    var file = System.IO.File.Create(Path.Combine(path, "input.txt"));
              
                    using(StreamWriter sw = new StreamWriter(file))
                    {
                        sw.WriteLine($"{model.Input_N} {model.Input_M}");
                        foreach (var item in maze)
                            sw.WriteLine(item);
                        foreach(var item in words)
                            sw.WriteLine(item);
                    }
                    lab.PathToInputFile = Path.Combine(path, "input.txt");
                    string result = lab.Run();
                    if (!string.IsNullOrEmpty(result))
                    {
                        model.Output = result;
                    }
                    System.IO.File.Delete(lab.PathToInputFile);
                }
            }
            return View(model);
        }

        public IActionResult Reset()
        {
            model = new Lab1Model();
            lab = new Lab1();
            return RedirectToAction("Index");
        }
    }
}
