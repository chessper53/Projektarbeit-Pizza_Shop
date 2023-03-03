using Microsoft.VisualBasic;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Printing;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Prizza_Admin
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class Window1 : Window
    {
        List<Kunde> Kunden = new List<Kunde>();
        List<Pizza> Pizzas = new List<Pizza>();
        private List<BsonDocument> _pizzaDocs;

        public static List<BsonDocument> pizzaDocs { get; set; }

        static IMongoClient client = new MongoClient("mongodb://localhost:27017");

        // Access the "PizzaShop" database
        IMongoDatabase database = client.GetDatabase("PizzaShop");

        public Window1()
        {
            InitializeComponent();
            new PizzaShopModell().ReadDatabase(KundeListBox, PizzaListBox, Kunden, Pizzas);
            bsaveKunde.IsEnabled = false;
            bsavePizza.IsEnabled = false;
            InsertGroessen();

        }
        public Window1(List<BsonDocument> pizzaDocs)
        {
            _pizzaDocs = pizzaDocs;
            foreach (var pizza in pizzaDocs)
            {
                PizzaListBox.Items.Add(pizza);
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

        private void bsave_Click(object sender, RoutedEventArgs e)
        {
            string entireTex = tname.Text + tzutaten.Text;
            string entireInt = tEinzelpreis.Text + tKCAL.Text + tDurchmesser.Text;

            if (!System.Text.RegularExpressions.Regex.IsMatch(entireTex, "^[a-zA-Z]"))
            {
                MessageBox.Show("Bitte geben sie nur Buchstaben bei Name und Zutaten ein.");
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(entireInt, @"^\d+$"))
            {
                MessageBox.Show("Bitte geben sie nur Zahlen bei Einzelpreis, KCAL und Durchmesser ein.");
            }
            else
            {
                var selectedPizza = PizzaListBox.SelectedItems.Cast<Pizza>().ToList();
                string selectedPizzaValue = "";
                foreach (var pizza in selectedPizza)
                {
                    selectedPizzaValue = pizza.Name + "";
                }

                var filter = Builders<BsonDocument>.Filter.Eq("Name", selectedPizzaValue);
                var collectionPizza = database.GetCollection<BsonDocument>("pizzas");

                var update = Builders<BsonDocument>.Update
                    .Set("Name", tname.Text)
                    .Set("Zutaten", tzutaten.Text)
                    .Set("Einzenpreis", tEinzelpreis.Text)
                    .Set("KCAL", tKCAL.Text)
                    .Set("Durchmesser", tDurchmesser.Text)
                    .Set("Groesse", SizesComboBox.Text);

                collectionPizza.UpdateOne(filter, update);
                PizzaListBox.SelectedIndex = -1;
            }

        }
        private void KundeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bsaveKunde.IsEnabled= true;
            var selectedKunde = KundeListBox.SelectedItems.Cast<Kunde>().ToList();
            string selectedKundeValue = "";
            foreach (var kunde in selectedKunde)
            {
                selectedKundeValue = kunde.Adresse + "";
            }
            var filter = Builders<BsonDocument>.Filter.Eq("Adresse", selectedKundeValue);
            var collectionKunde = database.GetCollection<BsonDocument>("kunde");
            var documentsKunde = collectionKunde.Find(filter).FirstOrDefault();
            if (documentsKunde != null)
            {
                String Date = documentsKunde["Geburtsdatum"].ToString();
                Convert.ToDateTime(Date);

                tvorname.Text = documentsKunde["Vorname"].ToString();
                tnachname.Text = documentsKunde["Nachname"].ToString();
                tAdresse.Text = documentsKunde["Adresse"].ToString();
                tTelefon.Text = documentsKunde["Telefon"].ToString();
                tmail.Text = documentsKunde["Email"].ToString();
                tGeburt.Text = Date;

            }
        }

        private void baddPizza_Click(object sender, RoutedEventArgs e)
        {
            string entireTex = tname.Text + tzutaten.Text;
            string entireInt = tEinzelpreis.Text + tKCAL.Text + tDurchmesser.Text;

            if (!System.Text.RegularExpressions.Regex.IsMatch(entireTex, "^[a-zA-Z]"))
            {
                MessageBox.Show("Bitte geben sie nur Buchstaben bei Name und Zutaten ein.");
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(entireInt, @"^\d+$"))
            {
                MessageBox.Show("Bitte geben sie nur Zahlen bei Einzelpreis, KCAL und Durchmesser ein.");
            }
            else
            {
                IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("pizzas");
                BsonDocument doc = new BsonDocument
                {
                 {"Name", tname.Text},
                 {"Zutaten", tzutaten.Text},
                 {"Einzenpreis", tEinzelpreis.Text},
                 {"KCAL", tKCAL.Text},
                 {"Durchmesser", tDurchmesser.Text},
                 {"Groesse", SizesComboBox.Text},
                 {"Extrazutaten", ExtraZutatenComboBox.Text}
                };

                collection.InsertOne(doc);
            }
        }

        private void tvorname_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void baddKunde_Click(object sender, RoutedEventArgs e)
        {
            String Date = tGeburt.Text;
            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("kunde");
            BsonDocument doc = new BsonDocument
            {
                 {"Nachname", tnachname.Text},
                 {"Vorname", tvorname.Text},
                 {"Adresse", tAdresse.Text},
                 {"Telefon", tTelefon.Text},
                 {"Email", tmail.Text},
                 {"Geburtsdatum", Convert.ToDateTime(Date)} 
            };

            collection.InsertOne(doc);
            

        }

        private void bsaveKunde_Click(object sender, RoutedEventArgs e)
        {
            IMongoCollection<BsonDocument> collectionkunde = database.GetCollection<BsonDocument>("kunde");
            var selectedKunde = KundeListBox.SelectedItems.Cast<Kunde>().ToList();
            string selectedKundeValue = "";
            foreach (var kunde in selectedKunde)
            {
                selectedKundeValue = kunde.Adresse + "";
            }
            var filter = Builders<BsonDocument>.Filter.Eq("Adresse", selectedKundeValue);

            String Date = tGeburt.Text;
            var update = Builders<BsonDocument>.Update
                .Set("Nachname", tnachname.Text)
                .Set("Vorname", tvorname.Text)
                .Set("Adresse", tAdresse.Text)
                .Set("Telefon", tTelefon.Text)
                .Set("Email", tmail.Text)
                .Set("Geburtsdatum", Convert.ToDateTime(Date));

            collectionkunde.UpdateOne(filter, update);

            KundeListBox.SelectedIndex = -1;
        }

        private void ListBoxPizza_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bsavePizza.IsEnabled = true;
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
            }
        }

        private void tname_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void SizesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void bdeletePizza(object sender, RoutedEventArgs e)
        {
            var collectionPizza = database.GetCollection<BsonDocument>("pizzas");
            var selectedPizza = PizzaListBox.SelectedItems.Cast<Pizza>().ToList();
            string selectedPizzaValue = "";
            foreach (var pizza in selectedPizza)
            {
                selectedPizzaValue = pizza.Name + "";
            }

            var filter = Builders<BsonDocument>.Filter.Eq("Name", selectedPizzaValue);
            collectionPizza.DeleteOneAsync(filter);

        }

        private void bdeleteKunde(object sender, RoutedEventArgs e)
        {
            bsaveKunde.IsEnabled = true;
            var selectedKunde = KundeListBox.SelectedItems.Cast<Kunde>().ToList();
            string selectedKundeValue = "";
            foreach (var kunde in selectedKunde)
            {
                selectedKundeValue = kunde.Adresse + "";
            }
            var filter = Builders<BsonDocument>.Filter.Eq("Adresse", selectedKundeValue);
            var collectionKunde = database.GetCollection<BsonDocument>("kunde");

            collectionKunde.DeleteOneAsync(filter);
        }

        private void validatePizzaFields()
        {
            string entireTex = tname.Text + tzutaten.Text;
            string entireInt = tEinzelpreis.Text + tKCAL.Text + tDurchmesser.Text;

            if (!System.Text.RegularExpressions.Regex.IsMatch(entireTex, "^[a-zA-Z]"))
            {
                MessageBox.Show("Bitte geben sie nur Buchstaben bei Name und Zutaten ein.");
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(entireInt, @"^\d+$"))
            {
                MessageBox.Show("Bitte geben sie nur Zahlen bei Einzelpreis, KCAL und Durchmesser ein.");
            }
            else
            {
                
            }
            
        }
    }
}
