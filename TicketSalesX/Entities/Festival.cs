using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSalesX.Entities
{
    public class Festival
    {
        //Attributes
        private string Name { get; set; }

        private Artist[] Artists { get; set; }

        private int TicketsSold { get; set; }
    }
}
