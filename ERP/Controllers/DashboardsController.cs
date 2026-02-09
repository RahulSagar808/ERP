using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Controllers;

public class DashboardsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Employee()
    {
        return View();
    }
    public IActionResult Marketing()
    {
        return View();
    }
    public IActionResult Inventery()
    {
        return View();
    }
    public IActionResult Conaultancy()
    {
        return View();
    }
}
