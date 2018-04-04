// --------------------------------------------------------------------------------------------------------------------
//   Copyright (c) Zaak (aaron.zaak@gmail.com). All rights reserved.  
//   Licensed under the Apache License 2.0. See LICENSE file in the project root for full license information.  
// --------------------------------------------------------------------------------------------------------------------

using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;

using PropellerheadCustomers.GuiService.Models;

namespace PropellerheadCustomers.GuiService.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult About()
        {
            this.ViewData["Message"] = "Technical interview for Propellerhead";

            return this.View();
        }

        public IActionResult Contact()
        {
            this.ViewData["ZaakProfile"] = "https://www.linkedin.com/in/aslezak/";

            return this.View();
        }

        public IActionResult Error()
        {
            return this.View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier});
        }
    }
}