using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Prizza_Admin
{
    public class Kunde
    {
        private string objectId;
        private string nachname;
        private string vorname;
        private string adresse;
        private string telefon;
        private string email;
        private DateTime geburtsdatum;
        public string DisplayString { get; set; }
        public string Nachname
        {
            get { return nachname; }
            set { nachname = value; }
        }
        public string Adresse
        {
            get { return adresse; }
            set { adresse = value; }
        }

        public string ObjectId
        {
            get { return objectId; }
            set { objectId = value; }
        }
        public string Vorname
        {
            get { return vorname; }
            set { vorname = value; }
        }
        public string Telefon
        {
            get { return telefon; }
            set { telefon = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public DateTime Geburtsdatum
        {
            get { return geburtsdatum; }
            set { geburtsdatum = value; }
        }
        public Kunde(string objectId, string nachname, string vorname, string adresse, string telefon, string email, DateTime geburtsdatum)
        {
            ObjectId = objectId;
            Nachname = nachname;
            Vorname = vorname;
            Adresse = adresse;
            Telefon = telefon;
            Email = email;
            Geburtsdatum = geburtsdatum; 
            DisplayString = vorname + " " + nachname;
        }
    }
}