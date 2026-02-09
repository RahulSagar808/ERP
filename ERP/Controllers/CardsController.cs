using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Controllers;
public class CardsController : Controller
{
  public IActionResult Basic() => View();
}
