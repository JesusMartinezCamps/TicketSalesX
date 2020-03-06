using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketSalesX.Entities;
using System.Text.Json;


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
            List<Artist> artists = Business.DataAccess.GetArtistsByFestivalName(festivalName);
            var json = JsonSerializer.Serialize(artists);
            return Ok(json);
        }



        //Ticket Stock
        [Route("/TicketSales/stock/")]
        [HttpGet]
        public ActionResult FestivalTicketStock()
        {
            List<Ticket> ticketPrice = Business.DataAccess.GetStock();
            var json = JsonSerializer.Serialize(ticketPrice);
            return Ok(json);
        }
    }
}