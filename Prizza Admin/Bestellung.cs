using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prizza_Admin
{
    public class Bestellung
    {
        public string ObjectId { get; set; }
        private string bestellnummer;
        private DateTime bestelldatum;
        private string kunde;
        private string bestellpositionen;
        private double total;


        public string DisplayString { get; set; }

        public string Bestellnummer
        {
            get { return bestellnummer; }
            set { bestellnummer = value; }
        }
        public DateTime Bestelldatum
        {
            get { return bestelldatum; }
            set { bestelldatum = value; }
        }

        public string Kunde
        {
            get { return kunde; }
            set { kunde = value; }
        }

        public string Bestellpositionen
        {
            get { return bestellpositionen; }
            set { bestellpositionen = value; }
        }
        public double Total
        {
            get { return total; }
            set { total = value; }
        }

        public Bestellung(string objectId, string bestellnummer, DateTime bestelldatum, string kunde, string bestellpositionen, double total)
        {
            ObjectId = objectId;
            Bestellnummer = bestellnummer;
            Bestelldatum = bestelldatum;
            Kunde = kunde;
            Bestellpositionen = bestellpositionen;
            Total = total;

            DisplayString = "Bestellungs Nummer: " + bestellnummer;
        }

    }
}
