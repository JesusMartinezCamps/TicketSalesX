using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSalesX.Entities
{
    public class Ticket
    {
        //Attributes
        public string FestivalName { get; set; }
        public string NextPrice { get; set; }
        public string ActualPrice { get; set; }
        public string TicketsSold { get; set; }

        public string TicketsLeft { get; set; }

        //Constructors
        public Ticket(string _festivalName)
        {
            this.FestivalName = _festivalName;
        }

        public Ticket(string _festivalName, string _ticketsSold, string _actualPrice, string _nextPrice)
        {
            this.FestivalName = _festivalName;
            this.TicketsSold = _ticketsSold;
            this.ActualPrice = _actualPrice;
            this.NextPrice = _nextPrice;
            
        }
    }
}
