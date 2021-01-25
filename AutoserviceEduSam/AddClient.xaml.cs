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
    /// Логика взаимодействия для AddClient.xaml
    /// </summary>
    public partial class AddClient : Window
    {
        private DataGrid _grid;
        public AddClient(DataGrid gr)
        {
            InitializeComponent();
            _grid = gr;
        }

        private void AddClientBtn_Click(object sender, RoutedEventArgs e)
        {
            using (Context db = new Context())
            {

                Client client = new Client
                {
                    FirstName = ClientName.Text,
                    LastName = ClientLastName.Text,
                    Patronymic = ClientPatronymic.Text,
                    Birthday = Convert.ToDateTime(ClientBirthday.Text),
                    RegistrationDate = Convert.ToDateTime(ClientRegistrationDate.Text),
                    Email = ClientEmail.Text,
                    Phone = ClientPhone.Text,
                    GenderCode = ClientGenderCode.Text,
                    PhotoPath = ClientPhotoPath.Text
                };
                if (client != null)
                {
                    db.Client.Add(client);

                    db.SaveChanges();
                    _grid.ItemsSource = db.Client.ToList();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Вы не ввели значения");
                }
            }
        }
        string FilePath;
        private void ClientPhotoPath_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            FilePath = openFileDialog.FileName;
            ClientPhotoPath.Text = FilePath;

        }
    }
}
