using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSalesX.Entities
{
    public class Artist
    {
        //Attributes
        public string Name { get; set; }

        public string Type { get; set; }

        public string Genre { get; set; }


        //Constructor
        public Artist(string _name, string _type, string _genre)
        {
            this.Name = _name;
            this.Type = _type;
            this.Genre = _genre;
        }
    }
}
