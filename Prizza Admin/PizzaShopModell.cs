using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Prizza_Admin
{
    public class PizzaShopModell
    {

        public static List<BsonDocument> pizzaDocs { get; set; }
        
        static IMongoClient client = new MongoClient("mongodb://localhost:27017");
        // Access the "PizzaShop" database
        IMongoDatabase database = client.GetDatabase("PizzaShop");

        public PizzaShopModell()
        {
            // Access the "pizzas" collection
            var collection = database.GetCollection<BsonDocument>("pizzas");

            // Retrieve all documents in the "pizzas" collection
            var pizzaDocs = collection.Find(new BsonDocument()).ToList();
            



        }
        public void ReadDatabase(ListBox lbxKunden, ListBox lbxEsse, List<Kunde> Kunden, List<Pizza> Pizzas)
        {
            Kunden.Clear();
            Pizzas.Clear();
            var filter = new BsonDocument();

            var collection = database.GetCollection<BsonDocument>("kunde"); 
            var documents = collection.Find(filter).ToList();
            foreach (var doc in documents)
            {
                Kunden.Add(new Kunde(doc["_id"] + "", doc["Nachname"] + "", doc["Vorname"] + "", doc["Adresse"] + "", doc["Telefon"] + "", doc["Email"] + "", doc["Geburtsdatum"].ToUniversalTime().ToLocalTime()));
            }

            var collectionPizza = database.GetCollection<BsonDocument>("pizzas"); 
            var documentsPizza = collectionPizza.Find(filter).ToList();
            foreach (var doc1 in documentsPizza)
            {
                Pizzas.Add(new Pizza(doc1["_id"] + "", doc1["Name"] + "", new List<string>() { doc1["Zutaten"]+ "" }, Convert.ToDouble(doc1["Einzenpreis"]), Convert.ToDouble(doc1["KCAL"]), Convert.ToDouble(doc1["Durchmesser"]), doc1["Groesse"] + ""));
            }

            lbxKunden.Items.Clear();
            lbxEsse.Items.Clear();
            AddRange(lbxKunden, Kunden);
            AddRangePizza(lbxEsse, Pizzas);
        }
        private void AddRange(ListBox listbox, List<Kunde> list)
        {
            foreach (Kunde str in list)
            { 
                listbox.Items.Add(str);
            }
        }
        private void AddRangePizza(ListBox listbox, List<Pizza> list)
        {
            foreach (Pizza str in list)
            {
                listbox.Items.Add(str);
            }
        }
    }





}
