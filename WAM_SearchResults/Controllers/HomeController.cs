using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WAM_SearchResults.Models;

namespace WAM_SearchResults.Controllers
{
    public class HomeController : Controller
    {
        //db = new DatabaseContext();
        private List<Employee> Employees = new List<Employee>();

        public HomeController()
        {
            Employee temp = new Employee();
            temp.name = "Amy Smith";
            temp.title = "CEO";
            Employees.Add(temp);

            temp = new Employee();
            temp.name = "John Doe";
            temp.title = "Vice President";
            Employees.Add(temp);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(string userInput)
        {
            string result;
            List<Employee> resultsArray = new List<Employee>();

            if (userInput != "")
            {
                foreach (var employee in Employees) 
                {
                    if (employee.name.Contains(userInput) || employee.title.Contains(userInput))
                    {
                        resultsArray.Add(employee);
                    }

                }
            }

            else
            {
                resultsArray = Employees; 
            }

            result = JsonConvert.SerializeObject(resultsArray, Formatting.None,
                      new JsonSerializerSettings
                      {
                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                          ContractResolver = new CamelCasePropertyNamesContractResolver()
                      });
            return Content(result, "application/json");


        }

    }
}