using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketSalesX.Business;

namespace TicketSalesX.Controllers
{
    [Route("api/TicketSales")]
    [ApiController]
    public class TicketSalesController : ControllerBase
    {

        //List of festival artists
        [Route("/TicketSales/cartel/{festivalName}")]
        [HttpGet]
        public ActionResult FestivalArtists(string festivalName)
        {

            return (StatusCode(200));
        }
    }
}