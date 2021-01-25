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
    /// Логика взаимодействия для ChangeClient.xaml
    /// </summary>
    public partial class ChangeClient : Window
    {
        private DataGrid MainWindowGrid = null;
        public ChangeClient(string FirstName, string LastName, string Patronymic, string Birthday, string RegistrationDate, string Email, string Phone, string GenderCode, string PhotoPath, DataGrid dg)
        {
            InitializeComponent();
            ClientName.Text = FirstName;
            ClientLastName.Text = LastName;
            ClientPatronymic.Text = Patronymic;
            ClientBirthday.Text = Birthday;
            ClientRegistrationDate.Text = RegistrationDate;
            ClientEmail.Text = Email;
            ClientPhone.Text = Phone;
            ClientGenderCode.Text = GenderCode;
            ClientPhotoPath.Text = PhotoPath;
            this.MainWindowGrid = dg;
        }

        private void ChangeClientBtn_Click(object sender, RoutedEventArgs e)
        {
            Client client = MainWindowGrid.SelectedItem as Client;
            if (client == null)
            {
                return;
            }

            Client newclient = new Client
            {
                FirstName = ClientName.Text,
                LastName = ClientLastName.Text,
                Patronymic = ClientPatronymic.Text,
                Birthday = Convert.ToDateTime(ClientBirthday.Text),
                RegistrationDate = Convert.ToDateTime(ClientRegistrationDate.Text),
                Email = ClientEmail.Text,
                Phone = ClientPhone.Text,
                GenderCode = Convert.ToString(ClientGenderCode.Text),
                PhotoPath = ClientPhotoPath.Text
            };
            newclient.ID = client.ID;
            using (Context db = new Context())
            {
                db.Entry(newclient).State = EntityState.Modified;
                db.SaveChanges();
                MainWindowGrid.ItemsSource = db.Client.ToList();
                this.Close();
            }

        }

        private void ClientPhotoPath_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
        }
    }
}
