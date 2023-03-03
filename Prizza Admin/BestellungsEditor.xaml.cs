using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Text.RegularExpressions;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Controls.Primitives;
using System.Collections;

namespace Prizza_Admin
{
    /// <summary>
    /// Interaction logic for BestellungsEditor.xaml
    /// </summary>
    public partial class BestellungsEditor : Window
    {
        List<Kunde> Kunden = new List<Kunde>();
        List<Pizza> Pizzas = new List<Pizza>();
        List<Bestellung> Bestellungen = new List<Bestellung>();

        private List<BsonDocument> _pizzaDocs;

        private double zuschlag;
        private double groessezuschlag;
        private string initalsize;
        private string allExtraZutaten = "";
        private double finalprice;
        private int stueckzahl = 1;
        private double totalcost;

        BsonArray orderarray = new BsonArray();
        public static List<BsonDocument> pizzaDocs { get; set; }

        static IMongoClient client = new MongoClient("mongodb://localhost:27017");

        // Access the "PizzaShop" database
        IMongoDatabase database = client.GetDatabase("PizzaShop");

        private string objectidfromselectedkunde;
        public BestellungsEditor()
        {
            InitializeComponent();
            InsertExtraZutaten();
            InsertGroessen();
            zuschlag = 0.0;
            groessezuschlag = 0.0;
            new PizzaShopModell().ReadDatabase(KundeListBox, PizzaListBox, Kunden, Pizzas);
            new BestellungsModel().ReadBestellungen(BestellungListBox, Bestellungen);
            bAddExtraZutat.IsEnabled = false;
        }
        public void InsertExtraZutaten()
        {
            List<string> extrazutatenlist = new List<string>();

            extrazutatenlist.Add("Speck +1.50 CHF");
            extrazutatenlist.Add("Ei +1.50 CHF");
            extrazutatenlist.Add("Mais +1.50 CHF");
            extrazutatenlist.Add("Karotten +1.50 CHF");
            extrazutatenlist.Add("Schokolade +5.00 CHF");
            extrazutatenlist.Add("Zwiebeln +2.00 CHF");
            extrazutatenlist.Add("Kiwi +2.00 CHF");
            extrazutatenlist.Add("Oliven +2.00 CHF");

            foreach (var extrazutat in extrazutatenlist)
            {
                ExtraZutatenComboBox.Items.Add(extrazutat);
            }


        }
        public void InsertGroessen()
        {
            List<string> groessenlist = new List<string>();

            groessenlist.Add("Small");
            groessenlist.Add("Medium");
            groessenlist.Add("Large");

            foreach (var groesse in groessenlist)
            {
                SizesComboBox.Items.Add(groesse);
            }
        }
        private void KundeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedKunde = KundeListBox.SelectedItems.Cast<Kunde>().ToList();
            string selectedKundeValue = "";
            foreach (var kunde in selectedKunde)
            {
                selectedKundeValue = kunde.Adresse + "";
            }

            var filter = Builders<BsonDocument>.Filter.Eq("Adresse", selectedKundeValue);
            var collectionKunde = database.GetCollection<BsonDocument>("kunde");
            var documentsKunde = collectionKunde.Find(filter).FirstOrDefault();


            tselectedkunde.Text = documentsKunde["Adresse"].ToString();

            objectidfromselectedkunde = documentsKunde["_id"].ToString();


        }
        private void ListBoxPizza_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedPizza = PizzaListBox.SelectedItems.Cast<Pizza>().ToList();
            string selectedPizzaValue = "";
            foreach (var pizza in selectedPizza)
            {
                selectedPizzaValue = pizza.Name + "";
            }

            var filter = Builders<BsonDocument>.Filter.Eq("Name", selectedPizzaValue);
            var collectionPizza = database.GetCollection<BsonDocument>("pizzas");
            var documentsPizza = collectionPizza.Find(filter).FirstOrDefault();

            if (documentsPizza != null)
            {
                tname.Text = documentsPizza["Name"].ToString();
                tzutaten.Text = documentsPizza["Zutaten"].ToString();
                tEinzelpreis.Text = documentsPizza["Einzenpreis"].ToString();
                tKCAL.Text = documentsPizza["KCAL"].ToString();
                tDurchmesser.Text = documentsPizza["Durchmesser"].ToString();
                SizesComboBox.Text = documentsPizza["Groesse"].ToString();
                initalsize = documentsPizza["Groesse"].ToString();

            }
        }

        private void ExtraZutatenComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bAddExtraZutat.IsEnabled= true;
        }

        private void SizesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void bAddExtraZutat_Click(object sender, RoutedEventArgs e)
        {
            String CurrentSelection = ExtraZutatenComboBox.Text;
            if (CurrentSelection != null)
            {
                //Reduziert den String auf die Zutat
                string pattern2 = @"^.*\+";
                string result2 = Regex.Match(CurrentSelection, pattern2).Value;
                result2 = result2.TrimEnd('+').TrimEnd();
                allExtraZutaten += result2 + ", ";

                //Reduziert den String auf den Preis
                string pattern = @"(?<=\+)\d+(\.\d+)?(?= CHF)";
                string result = Regex.Match(CurrentSelection, pattern).Value;
                zuschlag += Convert.ToDouble(result);
                ExtraZutatenComboBox.SelectedIndex = -1;
                bAddExtraZutat.IsEnabled = false;
            }
            
        }

        private void newPrice_Click(object sender, RoutedEventArgs e)
        {
            String CurrentSelection = SizesComboBox.Text;
            String EinzelPreisStr = tEinzelpreis.Text;
            double EinzelPreis = Convert.ToDouble(EinzelPreisStr);

            if (CurrentSelection != null)
            {
                switch (CurrentSelection)
                {
                    case "Small":
                        if (initalsize == "Small")
                        {
                            groessezuschlag = 0.0;
                        }
                        else
                        {
                            groessezuschlag = 15.0;
                        }
                        break;
                    case "Medium":
                        if (initalsize == "Medium")
                        {
                            groessezuschlag = 0.0;
                        }
                        else
                        {
                            groessezuschlag = 25.0;
                        }
                        break;
                    case "Large":
                        if (initalsize == "Large")
                        {
                            groessezuschlag = 0.0;
                        }
                        else
                        {
                            groessezuschlag = 50.0;
                        }
                        break;
                }
            }

            if (initalsize == SizesComboBox.Text)
            {
                double newprice = EinzelPreis + zuschlag;
                finalprice = (EinzelPreis + zuschlag);
                tNewPrice.Text = "" + finalprice;
            }
            else
            {
                double newprice = EinzelPreis + zuschlag;
                finalprice = (((((EinzelPreis) / 100)) * groessezuschlag) + newprice);
                tNewPrice.Text = "" + finalprice;

            }
        } 

        private void BestellungAbsenden(object sender, RoutedEventArgs e)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            IMongoCollection<BsonDocument> collectionbestellung = database.GetCollection<BsonDocument>("bestellung");

            
            newPrice_Click(sender, e);
            ContinueOrder(sender, e);

            //Generiert eine Zufallszahl zwischen 1-1000
            Random rand = new Random();
            int ordernumber = rand.Next(0, 1000);
            string orderstring = ordernumber.ToString();

            BsonDocument doc = new BsonDocument
            {
                 {"Bestellnummer", orderstring},
                 {"Bestelldatum", Convert.ToDateTime(dateTime)},
                 {"Kunden ID:", objectidfromselectedkunde},
                 {"Bestellpositionen", orderarray},
                 {"Total", totalcost},
            };
            collectionbestellung.InsertOne(doc);
            this.Close();
        }

        private void ContinueOrder(object sender, RoutedEventArgs e)
        {
            totalcost += finalprice;
            BsonDocument newPizza = new BsonDocument
            {
                {"Pizza", tname.Text},
                {"Extrazutaten", allExtraZutaten},
                {"Preis", finalprice},
                {"Stückzahl", 1},
                {"Kommentare", tUserComments.Text},
            };
            orderarray.Add(newPizza);

            //Setzt alle relevanten Variablen zurück
            tname.Text = "";
            groessezuschlag = 0.0;
            allExtraZutaten = "";
            tzutaten.Text = "";
            tEinzelpreis.Text = "";
            tKCAL.Text = "";
            tDurchmesser.Text = "";
            ExtraZutatenComboBox.SelectedIndex = -1;
            SizesComboBox.SelectedIndex = -1;
            tUserComments.Text = "";
            tNewPrice.Text = "";
            bAddExtraZutat.IsEnabled = false;
        }

        private void delorder(object sender, RoutedEventArgs e)
        {
            var selectedOrder = BestellungListBox.SelectedItems.Cast<Bestellung>().ToList();
            string selectedBestellungValue = "";
            foreach (var bestellung in selectedOrder)
            {
                selectedBestellungValue = bestellung.Bestellnummer + "";
            }
            var filter = Builders<BsonDocument>.Filter.Eq("Bestellnummer", selectedBestellungValue);
            IMongoCollection<BsonDocument> collectionbestellung = database.GetCollection<BsonDocument>("bestellung");
            collectionbestellung.DeleteOne(filter);


        }
    }
}
