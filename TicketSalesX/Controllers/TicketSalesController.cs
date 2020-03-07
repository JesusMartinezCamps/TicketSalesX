using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketSalesX.Entities;
using System.Text.Json;
using System.Net;
using System.IO;
using TicketSalesX.Business;
using TicketSalesX.Models;
using System.Data;

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
            List<Artist> artists = DataAccess.GetArtistsByFestivalName(festivalName);
            var json = JsonSerializer.Serialize(artists);
            return Ok(json);
        }

        //Ticket Stock
        [Route("/TicketSales/stock/")]
        [HttpGet]
        public ActionResult FestivalTicketStock()
        {
            List<Ticket> ticketPrice = DataAccess.GetStock();
            var json = JsonSerializer.Serialize(ticketPrice);
            return Ok(json);
        }

        //Ticket Sales
        [Route("/TicketSales/comprar/")]
        [HttpPost]
        public ActionResult FestivalTicketSale([FromBody]TicketSaleRq ticketSaleRq)
        {
            TicketSaleRs ticketSaleRs = new TicketSaleRs();
            ticketSaleRs.DateTime = DateTime.Now.Hour.ToString() + ':' + DateTime.Now.Minute.ToString();
            ticketSaleRs.Cost = SalerManager.CalculateTicketPrice(ticketSaleRq).ToString();

            DataAccess.UpdateStock(ticketSaleRq);

            return Ok(ticketSaleRs);
        }
    }
}