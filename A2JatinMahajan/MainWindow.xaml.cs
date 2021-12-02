using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

namespace A2JatinMahajan
{

    public partial class MainWindow : Window
    {
        WorldDBDataSetTableAdapters.CityTableAdapter Cities;
        WorldDBDataSetTableAdapters.ContinentTableAdapter Continents;
        WorldDBDataSetTableAdapters.CountryTableAdapter Countries;

        WorldDBDataSet.CityDataTable tab_Cities;
        WorldDBDataSet.ContinentDataTable tab_Continents;
        WorldDBDataSet.CountryDataTable tab_Countries;

        public MainWindow()
        {
            InitializeComponent();
            Cities = new WorldDBDataSetTableAdapters.CityTableAdapter();
            Continents = new WorldDBDataSetTableAdapters.ContinentTableAdapter();
            Countries = new WorldDBDataSetTableAdapters.CountryTableAdapter();
        }

        public void GetContinent()
        {
            tab_Continents = Continents.GetContinents();
            cmbContinents.ItemsSource = tab_Continents;
        }

        public void GetCountry()
        {
            tab_Countries = Countries.GetCountries();
        }

        public void GetCity()
        {
            tab_Cities = Cities.GetCities();
        }

        private void cmbContinents_Loaded(object sender, RoutedEventArgs e)
        {
            GetContinent();
            cmbContinents.DisplayMemberPath = "ContinentName";
        }

        private void cmbContinents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int id = cmbContinents.SelectedIndex + 1;
            tab_Countries = Countries.GetById(id);
            if (tab_Countries.Count > 0)
            {
                lstCountries.ItemsSource = tab_Countries;
                lstCountries.DisplayMemberPath = "CountryName";
            }
        }

        private void lstCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string countryName = "";
            int response = lstCountries.SelectedIndex;

            if (response != -1)
            {
                DataRowView drv = (DataRowView)lstCountries.SelectedItem;
                countryName = drv["CountryId"].ToString();
                int countryId = Convert.ToInt32(countryName);

                tab_Cities = Cities.GetByCityName(countryId);
                if (tab_Cities.Count > 0)
                {
                    grdCities.ItemsSource = tab_Cities;
                    tab_Countries = Countries.GetCountries();
                    var row = tab_Countries[countryId - 1];
                    lblLang.Content = row.Language.ToString();
                    lblCurrency.Content = row.Currency.ToString();
                }
            }
            else
            {
                cmbContinents_SelectionChanged(sender, e);
            }
        }

        private void btnAddContinents_Click(object sender, RoutedEventArgs e)
        {
            AddContinents continent = new AddContinents();
            continent.Closed += new EventHandler(Refresh1);
            continent.Show();
        }

        private void Refresh1(object sender, EventArgs e)
        {

            GetContinent();
            cmbContinents.DisplayMemberPath = "ContinentName";

        }

        private void btnAddCountry_Click(object sender, RoutedEventArgs e)
        {

            AddCountries country = new AddCountries();
            country.Closed += new EventHandler(Refresh2);
            country.Show();

        }

        private void Refresh2(object sender, EventArgs e)
        {
            GetCountry();
        }

        private void btnAddCities_Click(object sender, RoutedEventArgs e)
        {

            AddCities city = new AddCities();
            city.Closed += new EventHandler(Refresh3);
            city.Show();

        }

        private void Refresh3(object sender, EventArgs e)
        {
            GetCity();

        }
    }
}

