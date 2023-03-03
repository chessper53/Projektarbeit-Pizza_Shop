using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Prizza_Admin
{
    public class BestellungsModel
    {
        static IMongoClient client = new MongoClient("mongodb://localhost:27017");
        // Access the "PizzaShop" database
        IMongoDatabase database = client.GetDatabase("PizzaShop");

        public void ReadBestellungen(ListBox lbxBestellung, List<Bestellung> Bestellungen)
        {
            Bestellungen.Clear();
            var filter = new BsonDocument();

            var collectionBestellung = database.GetCollection<BsonDocument>("bestellung");
            var documentsBestellung = collectionBestellung.Find(filter).ToList();
            foreach (var doc1 in documentsBestellung)
            {
                Bestellungen.Add(new Bestellung(doc1["_id"] + "", doc1["Bestellnummer"] + "", doc1["Bestelldatum"].ToUniversalTime().ToLocalTime(), doc1["Kunden ID:"] + "" , doc1["Bestellpositionen"] + "", Convert.ToDouble(doc1["Total"])));
            }

            lbxBestellung.Items.Clear();
            AddRangeBestellung(lbxBestellung, Bestellungen);
        }
        private void AddRangeBestellung(ListBox listbox, List<Bestellung> list)
        {
            foreach (Bestellung str in list)
            {
                listbox.Items.Add(str);
            }
        }

    }
    
}
