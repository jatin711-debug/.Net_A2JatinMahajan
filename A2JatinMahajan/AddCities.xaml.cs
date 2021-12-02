using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;

namespace A2JatinMahajan
{
    public partial class AddCities : Window
    {
        WorldDBDataSetTableAdapters.CityTableAdapter Adp_Cities;
        WorldDBDataSet.CityDataTable TabCities;

        WorldDBDataSetTableAdapters.CountryTableAdapter Adp_Countries;
        WorldDBDataSet.CountryDataTable tabCountries;
        public AddCities()
        {
            InitializeComponent();
            Adp_Cities = new WorldDBDataSetTableAdapters.CityTableAdapter();
            Adp_Countries = new WorldDBDataSetTableAdapters.CountryTableAdapter();
        }

        public void FillCity()
        {
            TabCities = Adp_Cities.GetCities();

        }

        private void cmbCountry_Loaded(object sender, RoutedEventArgs e)
        {

            tabCountries = Adp_Countries.GetCountries();
            cmbCountry.ItemsSource = tabCountries;
            cmbCountry.DisplayMemberPath = "CountryName";

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (cmbCountry.SelectedIndex == -1)
            {
                lblCountry.Content = "* This Field is required.";
            }
            else if (txtCity.Text.Equals(""))
            {
                lblCity.Content = "* This Field is required.";
            }
            else
            {

                string cityName = txtCity.Text;
                bool isCapital = (bool)checkCapital.IsChecked;
                string population = txtPopulation.Text;
                DataRowView drv = (DataRowView)cmbCountry.SelectedItem;
                string a = drv["CountryId"].ToString();
                Adp_Cities.InsertQuery(cityName, isCapital, population, Convert.ToInt32(a));
                FillCity();
                MessageBox.Show("Successfully Added New Country ", "Success!!!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
