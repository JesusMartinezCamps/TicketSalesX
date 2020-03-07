using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TicketSalesX.Models
{
    [XmlRoot("TicketSales")]
    public class TicketSaleRq
    {
        [Required]
        [DataMember]
        public string FestivalName { get; set; }


        [Required]
        [DataMember]
        public string TicketNumber { get; set; }
    }
}
