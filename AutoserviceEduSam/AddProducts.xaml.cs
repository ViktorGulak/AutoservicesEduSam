using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace AutoserviceEduSam
{
    /// <summary>
    /// Логика взаимодействия для AddProducts.xaml
    /// </summary>
    public partial class AddProducts : Window
    {
        private DataGrid _grid;
        public AddProducts(DataGrid gr)
        {
            InitializeComponent();
            this._grid = gr;
        }

        private void AddProductBtn_Click(object sender, RoutedEventArgs e)
        {
            using (Context db = new Context())
            {

                Product product = new Product
                {
                    Title = ProductName.Text,
                    Cost = Convert.ToDecimal(ProductCost.Text),
                    Description = ProductDescription.Text,
                    MainImagePath = ProductPhotoPath.Text,
                    IsActive = Convert.ToBoolean(ProductIsActive.Text),
                    ManufacturerID = Convert.ToInt32(ManufacturerID.Text)
                };
                if (product != null)
                {
                    db.Product.Add(product);

                    db.SaveChanges();
                    _grid.ItemsSource = db.Product.ToList();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Вы не ввели значения");
                }
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
