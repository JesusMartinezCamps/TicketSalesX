using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSalesX.Entities
{
    public class Artist
    {
        //Attributes
        private string Name { get; set; }

        private string Type { get; set; }

        private string Genre { get; set; }


        //Constructor
        public Artist(string _name, string _type, string _genre)
        {
            this.Name = _name;
            this.Type = _type;
            this.Genre = _genre;
        }
    }
}
