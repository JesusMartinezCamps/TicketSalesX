using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSalesX.Models;

namespace TicketSalesX.Business
{
    public class SalerManager
    {
        //Constructor
        public SalerManager()
        {

        }

        public static int CalculateTicketPrice(TicketSaleRq ticketSaleRq)
        {
            string actualTicketPrice = DataAccess.GetFestivalTicketPrice(ticketSaleRq.FestivalName);
            int totalPrice;
            try
            {
                totalPrice = int.Parse(actualTicketPrice) * int.Parse(ticketSaleRq.TicketNumber);
            }
            catch (Exception)
            {

                throw;
            }
            return totalPrice;
        }
    }
}
