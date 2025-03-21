using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Company.Session03.PL.Models;
using Company.Session03.PL.Services;
using System.Text;

namespace Company.Session03.PL.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    //private readonly IServiceScope serviceScope01;
    //private readonly IServiceScope serviceScope02;
    //private readonly ITarnsentService tarnsentService01;
    //private readonly ITarnsentService tarnsentService02;
    //private readonly ISengeltonService sengeltonService01;
    //private readonly ISengeltonService sengeltonService02;

    public HomeController(
        ILogger<HomeController> logger
        
        //,IServiceScope serviceScope01,
        //IServiceScope serviceScope02,
        //ITarnsentService tarnsentService01,
        //ITarnsentService tarnsentService02,
        //ISengeltonService sengeltonService01,
        //ISengeltonService sengeltonService02
        )
    {
        _logger = logger;
        //this.serviceScope01 = serviceScope01;
        //this.serviceScope02 = serviceScope02;
        //this.tarnsentService01 = tarnsentService01;
        //this.tarnsentService02 = tarnsentService02;
        //this.sengeltonService01 = sengeltonService01;
        //this.sengeltonService02 = sengeltonService02;
    }

    //public string TestLifeTime()

    //{
    //    StringBuilder Builder = new StringBuilder();

    //    //Builder.Append($"serviceScope01 : {serviceScope01.GetGuid()}");
    //    //Builder.Append($"serviceScope01 : {serviceScope02.GetGuid()}");
    //    Builder.Append($"tarnsentService01 : {tarnsentService01.GetGuid()}");
    //    Builder.Append($"tarnsentService02 : {tarnsentService02.GetGuid()}");
    //    Builder.Append($"sengeltonService01 : {sengeltonService01.GetGuid()}");
    //    Builder.Append($"sengeltonService02 : {sengeltonService02.GetGuid()}");


    //    return Builder.ToString();
    //}

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
}
