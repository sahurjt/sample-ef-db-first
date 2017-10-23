using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;

namespace BlogExampleApi.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            
            ViewBag.Title = "Home Page";
            HttpClient c = new HttpClient
            {
                BaseAddress = new Uri("https://google.co.in")
            };
            string result = c.GetStringAsync("").Result;
            return result;
            
        }
    }
}
