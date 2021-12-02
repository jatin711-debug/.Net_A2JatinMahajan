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

namespace A2JatinMahajan
{
    /// <summary>
    /// Interaction logic for AddContinents.xaml
    /// </summary>
    public partial class AddContinents : Window
    {
        WorldDBDataSetTableAdapters.ContinentTableAdapter adpContinents;


        WorldDBDataSet.ContinentDataTable tblContinents;
        public AddContinents()
        {
            InitializeComponent();
            adpContinents = new WorldDBDataSetTableAdapters.ContinentTableAdapter();
            tblContinents = new WorldDBDataSet.ContinentDataTable();
        }

        public void FillContinent()
        {
            tblContinents = adpContinents.GetContinents();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (txtContinet.Text.Equals(""))
            {
                lblError.Content = " * This field is required";
            }
            else
            {

                FillContinent();

                String contName = txtContinet.Text;

                adpContinents.Insert(contName);

                FillContinent();


                MessageBox.Show("New Continent Added", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
