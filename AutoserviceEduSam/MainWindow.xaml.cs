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

namespace AutoserviceEduSam
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Переменны для редакьтирования клиента
        string FirstName;
        string LastName;
        string Patronymic;
        string Birthday;
        string RegistrationDate;
        string Email;
        string Phone;
        string GenderCode;
        string PhotoPath;

        // Переменные для редактирования продукта
        string Title;
        decimal Cost;
        string Description;
        string MainImagePath;
        bool IsActive;
        int ManufacturerId;

        public MainWindow()
        {
            InitializeComponent();
            using (Context db = new Context())
            {
                grid.ItemsSource = db.Client.ToList();
            }
        }

        private void Lists_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             using (Context db = new Context())
            {
                if (Lists.SelectedIndex == 0)
                {
                    grid.ItemsSource = db.Client.ToList();
                    GridTitle.Content = "Клиенты";
                }
                else if (Lists.SelectedIndex == 1)
                {
                    grid.ItemsSource = db.Service.ToList();
                    GridTitle.Content = "Услуги";
                }
                else if (Lists.SelectedIndex == 2)
                {
                   grid.ItemsSource = db.Product.ToList();
                    GridTitle.Content = "Продукты";
                }
            }
        }

        

        private void Operations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Lists.SelectedIndex == 0 && Operations.SelectedIndex == 0 )
            {
                AddClient addClient = new AddClient(grid);
                addClient.ShowDialog();
                //Lists.SelectedIndex = 3;
                Operations.SelectedIndex = 3;
            }
            else if (Lists.SelectedIndex == 0 && Operations.SelectedIndex == 1 )
            {
               
                Client client = grid.SelectedItem as Client;
                if (client != null)
                {
                    FirstName = client.FirstName;
                    LastName = client.LastName;
                    Patronymic = client.Patronymic;
                    Birthday = Convert.ToString(client.Birthday);
                    RegistrationDate = Convert.ToString(client.RegistrationDate);
                    Email = client.Email;
                    Phone = client.Phone;
                    GenderCode = Convert.ToString(client.GenderCode);
                    PhotoPath = client.PhotoPath;
                    var dg = grid;
                    ChangeClient changeClient = new ChangeClient(FirstName, LastName, Patronymic, Birthday, RegistrationDate, Email, Phone, GenderCode, PhotoPath, dg);
                    changeClient.ShowDialog();
                    Lists.SelectedIndex = 3;
                    Operations.SelectedIndex = 3;
                }
                else
                {
                    MessageBox.Show("Ошибка ввода данных!!! \n Возможно вы не выброли строку, которую хотите изменить");
                }
                
            }
            else if (Lists.SelectedIndex == 0 && Operations.SelectedIndex == 2 )
            {
                Client client = grid.SelectedItem as Client;
                if(client != null)
                {
                    using (Context db = new Context())
                    {
                        db.Entry(client).State = System.Data.Entity.EntityState.Deleted;
                        db.SaveChanges();
                        grid.ItemsSource = db.Client.ToList();
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка при  удалении данных!!! \n Возможно вы не выброли строку, которую хотите удалить");
                }
            }
            
            if(Lists.SelectedIndex == 2 && Operations.SelectedIndex == 0)
            {
                AddProducts addProduct = new AddProducts(grid);
                addProduct.ShowDialog();
                //Lists.SelectedIndex = 3;
                Operations.SelectedIndex = 3;
            }
            else if(Lists.SelectedIndex == 2 && Operations.SelectedIndex == 1)
            {
                Product product = grid.SelectedItem as Product;
                if (product != null)
                {
                    Title = product.Title;
                    Cost = product.Cost;
                    Description = product.Description;
                    MainImagePath = product.MainImagePath;
                    IsActive = product.IsActive;
                    ManufacturerId = (int)product.ManufacturerID;
                    var dg = grid;
                    ChangeProducts changeProduct = new ChangeProducts(Title, Cost, Description, MainImagePath, IsActive, ManufacturerId, dg);
                    changeProduct.ShowDialog();
                    //Lists.SelectedIndex = 3;
                    Operations.SelectedIndex = 3;
                }
                else
                {
                    MessageBox.Show("Ошибка ввода данных!!! \n Возможно вы не выброли строку, которую хотите изменить");
                }
            }
            else if(Lists.SelectedIndex == 2 && Operations.SelectedIndex == 2)
            {
                Product product = grid.SelectedItem as Product;
                if (product != null)
                {
                    using (Context db = new Context())
                    {
                        db.Entry(product).State = System.Data.Entity.EntityState.Deleted;
                        db.SaveChanges();
                        grid.ItemsSource = db.Product.ToList();
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка при  удалении данных!!! \n Возможно вы не выброли строку, которую хотите удалить");
                }
            }
        }

        private void ContextMenuAdd_Click(object sender, RoutedEventArgs e)
        {
            if (Lists.SelectedIndex == 0 || Lists.SelectedIndex == 3)
            {
                AddClient addClient = new AddClient(grid);
                addClient.Show();
                //Lists.SelectedIndex = 3;
                Operations.SelectedIndex = 3;
            }
            else if(Lists.SelectedIndex == 2)
            {
                AddProducts addProduct = new AddProducts(grid);
                addProduct.ShowDialog();
                //Lists.SelectedIndex = 3;
                Operations.SelectedIndex = 3;
            }
        }

        private void ContextMenuChange_Click(object sender, RoutedEventArgs e)
        {
            if (Lists.SelectedIndex == 0)
            {
                Client client = grid.SelectedItem as Client;
                if (client != null)
                {
                    FirstName = client.FirstName;
                    LastName = client.LastName;
                    Patronymic = client.Patronymic;
                    Birthday = Convert.ToString(client.Birthday);
                    RegistrationDate = Convert.ToString(client.RegistrationDate);
                    Email = client.Email;
                    Phone = client.Phone;
                    GenderCode = Convert.ToString(client.GenderCode);
                    PhotoPath = client.PhotoPath;
                    var dg = grid;
                    ChangeClient change = new ChangeClient(FirstName, LastName, Patronymic, Birthday, RegistrationDate, Email, Phone, GenderCode, PhotoPath, dg);
                    change.ShowDialog();
                    //Lists.SelectedIndex = 3;
                    Operations.SelectedIndex = 3;
                }
                else
                {
                    MessageBox.Show("Ошибка ввода данных!!! \n Возможно вы не выброли строку, которую хотите изменить");
                }
            }
            else if(Lists.SelectedIndex == 2)
            {
                Product product = grid.SelectedItem as Product;
                if (product != null)
                {
                    Title = product.Title;
                    Cost = product.Cost;
                    Description = product.Description;
                    MainImagePath = product.MainImagePath;
                    IsActive = product.IsActive;
                    ManufacturerId = (int)product.ManufacturerID;
                    var dg = grid;
                    ChangeProducts changeProduct = new ChangeProducts(Title, Cost, Description, MainImagePath, IsActive, ManufacturerId, dg);
                    changeProduct.ShowDialog();
                    //Lists.SelectedIndex = 3;
                    Operations.SelectedIndex = 3;
                }
                else
                {
                    MessageBox.Show("Ошибка ввода данных!!! \n Возможно вы не выброли строку, которую хотите изменить");
                }
            }
        }

        private void grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Lists.SelectedIndex == 0)
            {
                Client client = grid.SelectedItem as Client;
                if (client != null)
                {
                    Photo.Source = new BitmapImage(new Uri(client.PhotoPath));
                }
                else
                {
                    string mainPhotoPath = @"C:\Users\gulak.vv0142\Desktop\КОД.1.9_КлинтыАвтосервиса\Общие ресурсы\service_logo.png";

                    Photo.Source = new BitmapImage(new Uri(mainPhotoPath));
                    return;
                }
            }  
            else if(Lists.SelectedIndex == 2)
            {
                Product product = grid.SelectedItem as Product;
                if (product != null)
                {
                    Photo.Source = new BitmapImage(new Uri(product.MainImagePath));
                }
                else
                {
                    string mainPhotoPath = @"C:\Users\gulak.vv0142\Desktop\КОД.1.9_КлинтыАвтосервиса\Общие ресурсы\service_logo.png";

                    Photo.Source = new BitmapImage(new Uri(mainPhotoPath));
                    return;
                }
            }
        }

        // Методы для поиска значений, введённых пользователем
        private List<Client> FindValue(string searchQuery)
        {
            List<Client> _clients = new List<Client>();
            using (Context db = new Context())
            {
                foreach (Client client in db.Client.ToList())
                {
                    if ($"{client.FirstName} {client.LastName} {client.Patronymic} {client.Email} {client.Phone}".IndexOf(searchQuery) >= 0)
                    {
                        _clients.Add(client);
                    }
                }
            }
            return _clients;
        }
        private List<Product> FindValuePr(string searchQuery)
        {
            List<Product> _products = new List<Product>();
            using (Context db = new Context())
            {
                foreach (Product product in db.Product.ToList())
                {
                    if ($"{product.Title} {product.Cost} {product.IsActive}".IndexOf(searchQuery) >= 0)
                    {
                        _products.Add(product);
                    }
                }
            }
            return _products;
        }

        private void FindValueField_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Lists.SelectedIndex == 0)
            {
                List<Client> clients = FindValue(FindValueField.Text);
                grid.ItemsSource = clients;
            }
            else if(Lists.SelectedIndex == 2)
            {
                List<Product> products = FindValuePr(FindValueField.Text);
                grid.ItemsSource = products;
            }
        }

        private void ContextMenuremove_Click(object sender, RoutedEventArgs e)
        {
            if (Lists.SelectedIndex == 0)
            {
                Client client = grid.SelectedItem as Client;
                if (client != null)
                {
                    using (Context db = new Context())
                    {
                        db.Entry(client).State = System.Data.Entity.EntityState.Deleted;
                        db.SaveChanges();
                        grid.ItemsSource = db.Client.ToList();
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка при  удалении данных!!! \n Возможно вы не выброли строку, которую хотите удалить");
                }
            }
            else if(Lists.SelectedIndex == 2)
            {
                Product product = grid.SelectedItem as Product;
                if (product != null)
                {
                    using (Context db = new Context())
                    {
                        db.Entry(product).State = System.Data.Entity.EntityState.Deleted;
                        db.SaveChanges();
                        grid.ItemsSource = db.Product.ToList();
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка при  удалении данных!!! \n Возможно вы не выброли строку, которую хотите удалить");
                }
            }
        }

        private void NumberOfRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Lists.SelectedIndex == 0)
            {

                if (NumberOfRecords.SelectedIndex == 1)
                {

                    List<Client> clients = FilterRecordsCount(1);
                    grid.ItemsSource = clients;
                }
                else if (NumberOfRecords.SelectedIndex == 2)
                {
                    List<Client> clients = FilterRecordsCount(3);
                    grid.ItemsSource = clients;
                }
                else if (NumberOfRecords.SelectedIndex == 3)
                {
                    List<Client> clients = FilterRecordsCount(5);
                    grid.ItemsSource = clients;
                }
                else if (NumberOfRecords.SelectedIndex == 4)
                {
                    List<Client> clients = FilterRecordsCount(10);
                    grid.ItemsSource = clients;
                }
                else
                {
                    if (grid != null)
                    {
                        using (Context db = new Context())
                        {
                            grid.ItemsSource = db.Client.ToList();
                        }
                    }
                }
            }
            else if(Lists.SelectedIndex == 2)
            {
                if (NumberOfRecords.SelectedIndex == 1)
                {

                    List<Product> products = FilterRecordsCountPr(1);
                    grid.ItemsSource = products;
                }
                else if (NumberOfRecords.SelectedIndex == 2)
                {
                    List<Product> products = FilterRecordsCountPr(3);
                    grid.ItemsSource = products;
                }
                else if (NumberOfRecords.SelectedIndex == 3)
                {
                    List<Product> products = FilterRecordsCountPr(5);
                    grid.ItemsSource = products;
                }
                else if (NumberOfRecords.SelectedIndex == 4)
                {
                    List<Product> products = FilterRecordsCountPr(10);
                    grid.ItemsSource = products;
                }
                else
                {
                    if (grid != null)
                    {
                        using (Context db = new Context())
                        {
                            grid.ItemsSource = db.Product.ToList();
                        }
                    }
                }
            }
        }

        // фильтр кол-ва записей в списке клиентов
        private List<Client> FilterRecordsCount(int recordCount)
        {
            List<Client> _clients = new List<Client>(recordCount);
            using (Context db = new Context())
            {
                foreach (Client client in db.Client.ToList())
                {
                    if (_clients.Count >= recordCount)
                    {
                        break;                      
                    }
                    else
                    {
                        _clients.Add(client);
                    }
                }
            }
            return _clients;
        }

        // фильтр кол-ва записей в списке продуктов
        private List<Product> FilterRecordsCountPr(int recordCount)
        {
            List<Product> _products = new List<Product>(recordCount);
            using (Context db = new Context())
            {
                foreach (Product product in db.Product.ToList())
                {
                    if (_products.Count >= recordCount)
                    {
                        break;
                    }
                    else
                    {
                        _products.Add(product);
                    }
                }
            }
            return _products;
        }


    }
}
