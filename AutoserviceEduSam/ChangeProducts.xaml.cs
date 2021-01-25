using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Shapes;

namespace AutoserviceEduSam
{
    /// <summary>
    /// Логика взаимодействия для ChangeProducts.xaml
    /// </summary>
    public partial class ChangeProducts : Window
    {
        private DataGrid MainWindowGrid = null;
        public ChangeProducts( string title, decimal cost, string description, string mainImagePath, bool isActive, int manufacturerId, DataGrid dg)
        {
            InitializeComponent();
            ProductName.Text = title;
            ProductCost.Text = Convert.ToString(cost);
            ProductDescription.Text = description;
            ProductPhotoPath.Text = mainImagePath;
            ProductIsActive.Text = Convert.ToString(isActive);
            ManufacturerID.Text = Convert.ToString(manufacturerId);
            this.MainWindowGrid = dg;
        }

        private void ChangeProductBtn_Click(object sender, RoutedEventArgs e)
        {
            Product product = MainWindowGrid.SelectedItem as Product;
            if (product == null)
            {
                return;
            }

            Product newProduct = new Product
            {
                Title = ProductName.Text,
                Cost = Convert.ToDecimal(ProductCost.Text),
                Description = ProductDescription.Text,
                MainImagePath = ProductPhotoPath.Text,
                IsActive = Convert.ToBoolean(ProductIsActive.Text),
                ManufacturerID = Convert.ToInt32(ManufacturerID.Text)
            };
            newProduct.ID = product.ID;
            using (Context db = new Context())
            {
                db.Entry(newProduct).State = EntityState.Modified;
                db.SaveChanges();
                MainWindowGrid.ItemsSource = db.Product.ToList();
                this.Close();
            }
        }

        string FilePath;
        private void ProductPhotoPath_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            FilePath = openFileDialog.FileName;
            ProductPhotoPath.Text = FilePath;
        }
    }
}
