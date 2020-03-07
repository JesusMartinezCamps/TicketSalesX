using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSalesX.Entities;
using TicketSalesX.Models;

namespace TicketSalesX.Business
{
    public class DataAccess
    {
        //Attributes
        private static readonly string artistsFile = @"C:\Users\GsusM\source\repos\TicketSalesX\TicketSalesX\DataStorage\Artists.txt";
        private static readonly string festivalsFile = @"C:\Users\GsusM\source\repos\TicketSalesX\TicketSalesX\DataStorage\Festivals.txt";
        private static readonly string ticketsFile = @"C:\Users\GsusM\source\repos\TicketSalesX\TicketSalesX\DataStorage\Tickets.txt";

        //Public Methods
        public static List<Artist> GetArtistsByFestivalName(string festivalName)
        {
            List<Artist> artistList = new List<Artist>();

            if (FestivalExist(festivalName))
            {
                StreamReader file = new StreamReader(festivalsFile);
                string[] lineSplit = file.ReadLine().Split(',');

                return GetArtistsData(lineSplit, artistList);
            }
            return artistList;
        }

        public static List<Ticket> GetStock()
        {
            int i = 0;
            string line;
            StreamReader file = new StreamReader(ticketsFile);
            Ticket ticket;
            List<Ticket> ticketList = new List<Ticket>();

            while ((line = file.ReadLine()) != null)
            {
                string[] lineSplit = line.Split(',');

                if (i == 0)
                {
                    //First line contain the Festival Names
                    foreach (var festivalName in lineSplit)
                    {
                        ticket = new Ticket(festivalName);
                        ticketList.Add(ticket);
                    }
                }
                else
                {
                    foreach (var _ticket in ticketList)
                    {
                        UpdateTicketData(_ticket);
                    }
                }

                i++;
            }

            return ticketList;
        }

        public static void UpdateStock(TicketSaleRq ticketSaleRq)
        {
            StringBuilder sb = new StringBuilder("");
            string[] lines = File.ReadAllLines(festivalsFile);
            string oldValue, newValue, output = "";

            foreach (string _line in lines)
            {
                if (!_line.Contains(ticketSaleRq.FestivalName))
                {
                    sb.Append("\r" + _line);
                }
                else
                {

                    oldValue = _line.Split(',').Last();

                    newValue = (Int32.Parse(oldValue) + Int32.Parse(ticketSaleRq.TicketNumber)).ToString();
                    output = _line.Replace(oldValue.ToString(), newValue + "\r");

                    sb.Append("\r" + output);
                }
            }
            if (File.Exists(festivalsFile))
            {
                File.Delete(festivalsFile);
            }
            using (FileStream fs = new FileStream(festivalsFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
            {
                StreamWriter sw = new StreamWriter(fs);
                sw.BaseStream.Seek(0, SeekOrigin.Begin);
                sw.Write(sb.ToString());
                sw.Flush();
                sw.Close();
            }

            /*
            while ((line = file.ReadLine()) != null)
            {
                if (line.Contains(ticketSaleRq.FestivalName))
                {
                    oldValue = int.Parse(line.Split(',').Last());
                    newValue = (oldValue + int.Parse(ticketSaleRq.TicketNumber)).ToString();
                    string output = "\r" + line.Replace(oldValue.ToString(), newValue + "\r");

                    line.Remove(0);
                    file.Close();
                    using (StreamWriter writer = new StreamWriter(festivalsFile, true))
                    {
                        {
                            writer.Write(output);
                        }
                        writer.Close();
                        break;
                    }
                }
            }
            */
        }

        //Private Methods
        private static bool FestivalExist(string festivalName)
        {
            string line;
            StreamReader file = new StreamReader(festivalsFile);

            while ((line = file.ReadLine()) != null)
            {
                if (line.Contains(festivalName))
                {
                    file.Close();
                    return true;
                }
            }

            file.Close();
            return false;
        }


        private static List<Artist> GetArtistsData(string[] artistListTxt, List<Artist> artistList)
        {
            Artist _artist;
            string line;

            foreach (var artist in artistListTxt)
            {
                StreamReader file = new StreamReader(artistsFile);
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Contains(artist))
                    {
                        string[] artistData = file.ReadLine().Split(',');

                        _artist = new Artist(artistData[0], artistData[1], artistData[2]);
                        artistList.Add(_artist);
                        break;
                    }
                }
            }

            return artistList;
        }


        private static void UpdateTicketData(Ticket ticket)
        {
            string line;
            string actualTicketNumber;
            StreamReader file = new StreamReader(festivalsFile);

            while ((line = file.ReadLine()) != null)
            {
                if (line.Contains(ticket.FestivalName))
                {
                    actualTicketNumber = line.Split(',').Last();
                    ticket.ActualPrice = GetActualTicketPrice(actualTicketNumber);
                    ticket.TicketsLeft = GetTicketsLeft(actualTicketNumber);
                    ticket.NextPrice = GetNextTicketPrice(actualTicketNumber);
                    break;
                }
            }

            file.Close();
        }

        private static string GetTicketsLeft(string actualTicketNumber)
        {
            string line;
            string ticketsLeft = "";
            StreamReader file = new StreamReader(ticketsFile);
            int intActualTicketNumber;
            int intTicketLimit;

            try
            {
                intActualTicketNumber = Int32.Parse(actualTicketNumber);

                while ((line = file.ReadLine()) != null)
                {
                    if (line.Contains("TicketLimit"))
                    {
                        intTicketLimit = Int32.Parse(line.Split(',')[1]);

                        if (intActualTicketNumber < intTicketLimit)
                        {
                            ticketsLeft = (intTicketLimit - intActualTicketNumber).ToString();
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            file.Close();
            return ticketsLeft;
        }

        private static string GetActualTicketPrice(string actualTicketNumber)
        {
            string line;
            string actualPrice = "";
            StreamReader file = new StreamReader(ticketsFile);
            int intActualTicketNumber;
            int intTicketLimit;

            try
            {
                intActualTicketNumber = Int32.Parse(actualTicketNumber);

                while ((line = file.ReadLine()) != null)
                {
                    if (line.Contains("TicketLimit"))
                    {
                        intTicketLimit = Int32.Parse(line.Split(',')[1]);

                        if (intActualTicketNumber < intTicketLimit)
                        {
                            actualPrice = line.Split(',')[3];
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            file.Close();
            return actualPrice;
        }

        private static string GetNextTicketPrice(string actualTicketNumber)
        {
            string line;
            string nextPrice = "";
            StreamReader file = new StreamReader(ticketsFile);
            int intActualTicketNumber;
            int intTicketLimit;
            bool isNextLine = false;

            try
            {
                intActualTicketNumber = Int32.Parse(actualTicketNumber);

                while ((line = file.ReadLine()) != null)
                {
                    if (isNextLine)
                    {
                        nextPrice = line.Split(',')[3];
                        break;
                    }
                    else
                    {
                        if (line.Contains("TicketLimit"))
                        {
                            intTicketLimit = Int32.Parse(line.Split(',')[1]);

                            if (intActualTicketNumber < intTicketLimit)
                                isNextLine = true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            file.Close();
            return nextPrice;
        }

        public static string GetFestivalTicketPrice(string festivalName)
        {
            //posible refact para poner en un metodo a parte el coger el numero de tickets vendidos
            string line;
            string festivalTicketNumber = "0";
            string ticketPrice;
            StreamReader file = new StreamReader(festivalsFile);

            while ((line = file.ReadLine()) != null)
            {
                if (line.Contains(festivalName))
                {
                    festivalTicketNumber = line.Split(',').Last();
                    break;
                }
            }

            file.Close();
            ticketPrice = GetActualTicketPrice(festivalTicketNumber);
            return ticketPrice;
        }
    }
}
