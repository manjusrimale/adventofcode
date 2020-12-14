using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AdventOfCode2020_Day1.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace AdventOfCode2020_Day1.Controllers
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public string ParseFile(IFormFile file)
        {
            if(file==null || file.Length == 0)
            {
                return ViewBag.Message = "No file selected.";
            }
            List<int> numbers = new List<int>();
            int number;
            using(var reader=new StreamReader(file.OpenReadStream()))
            {
                while (!reader.EndOfStream)
                {
                    if (Int32.TryParse(reader.ReadLine(), out number))
                        numbers.Add(number);
                    else
                    {
                        return ViewBag.Message = "Error parsing input: " + reader.ReadLine();
                    }
                }
            }
            return Find2020(numbers);
        }

        private string Find2020(List<int> numbers)
        {
            numbers.Sort();
            int head = 0, tail = numbers.Count - 1, target = 2020;
            while (head < tail)
            {
                int sum = numbers[head] + numbers[tail];
                if ( target == sum)
                {
                    return ViewBag.Message = (numbers[head] * numbers[tail]).ToString();
                }
                else if(target < sum)
                {
                    tail --;
                }
                else
                {
                    head++;
                }
            }
            return ViewBag.Message = "No match found";
        }
    }
}
