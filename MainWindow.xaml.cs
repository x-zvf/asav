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
using Microsoft.Win32;
using System.Data.OleDb;

namespace asav
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OleDbConnection conn;

        public MainWindow()
        {
            InitializeComponent();
        }

        /*
         * Mit der Access-Datenbank in path verbinden und testen, dass die Verbindung funktioniert.
         * Wirft eine OleDbException bei Fehler.
         * */
        private OleDbConnection ConnectDatabase(string path)
        {
            OleDbConnection c = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path);
            c.Open();
            c.Close();
            return c;
        }

        private void DisconnectDatabase(OleDbConnection conn)
        {
            if (conn != null)
            {
                conn.Close();
            }
        }

        private void LoadDatabase()
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Datenbanken (*.accdb)|*.accdb|All files (*.*)|*.*";
            if (file.ShowDialog() == true)
            {
                try
                {
                    conn = ConnectDatabase(file.FileName);
                    activityButton.IsEnabled = true;
                    mintecButton.IsEnabled = true;
                    actionHintLabel.Content = "Datenbank geladen!";
                    actionHintLabel.FontWeight = FontWeights.Normal;
                } catch (OleDbException e)
                {
                    MessageBox.Show("Konnte die Datenbank nicht laden: \n" + e.Message, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        private void Window_Close(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisconnectDatabase(conn);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadDatabase();
        }

        private void ActivityButton_Click(object sender, RoutedEventArgs e)
        {
            AktivitaetEintragenWindow window = new AktivitaetEintragenWindow(conn);
            this.IsEnabled = false;
            window.ShowDialog();
            this.IsEnabled = true;
        }

        private void MintecButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ActivityButton_Click stub");
        }
    }
}
