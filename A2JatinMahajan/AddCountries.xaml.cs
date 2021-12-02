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
    public partial class AddCountries : Window
    {
        WorldDBDataSetTableAdapters.CountryTableAdapter AdpCountries;
        WorldDBDataSet.CountryDataTable TabCountries;

        WorldDBDataSetTableAdapters.ContinentTableAdapter AdpContinents;
        WorldDBDataSet.ContinentDataTable TabContinents;
        public AddCountries()
        {
            InitializeComponent();
            AdpCountries = new WorldDBDataSetTableAdapters.CountryTableAdapter();
            AdpContinents = new WorldDBDataSetTableAdapters.ContinentTableAdapter();
        }

        public void FillCountry()
        {
            TabCountries = AdpCountries.GetCountries();

        }

        private void cmbContinent_Loaded(object sender, RoutedEventArgs e)
        {

            TabContinents = AdpContinents.GetContinents();
            comboContinent.ItemsSource = TabContinents;
            comboContinent.DisplayMemberPath = "ContinentName";
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (comboContinent.SelectedIndex == -1)
            {
                lblContinent.Content = "* This Field is Mandatory.";
            }
            else if (txtCountry.Text.Equals(""))
            {
                lblCountry.Content = "* This Field is Mandatory.";
            }
            else
            {
                string countryName = txtCountry.Text;
                string lang = txtLanguage.Text;
                string curr = txtCurrency.Text;
                DataRowView drv = (DataRowView)comboContinent.SelectedItem;
                string a = drv["ContinentId"].ToString();
                AdpCountries.InsertQuery(countryName, lang, curr, Convert.ToInt32(a));
                FillCountry();
                MessageBox.Show("New Country Added", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }
    }
}
