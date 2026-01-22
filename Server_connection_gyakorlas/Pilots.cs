using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_connection_gyakorlas
{
    public class Pilots
    {
        public int ID { get; set; }
        public int LicenseID { get; set; }
        public string nev { get; set; }
        public DateOnly SzuletesiDatum { get; set; }
        public int RepuloOrak { get; set; }
    }
}
