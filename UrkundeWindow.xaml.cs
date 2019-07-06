﻿using System.Windows;
using System.Data.OleDb;
using System;

namespace asav
{
    /// <summary>
    /// Interaktionslogik für Window1.xaml
    /// </summary>
    public partial class UrkundeWindow : Window
    {

        private OleDbConnection conn;

        public UrkundeWindow(OleDbConnection conn)
        {
            InitializeComponent();
            this.conn = conn;

            OleDbCommand command = new OleDbCommand("SELECT * FROM mschuljahr ORDER BY mschuljahr.schuljahr DESC", conn);
            OleDbDataAdapter adap = new OleDbDataAdapter(command);
            schuelerDataSet.mschuljahrDataTable table = new schuelerDataSet.mschuljahrDataTable();
            adap.Fill(table);
            schuljahrComboBox.ItemsSource = table;
        }

        private void ListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void SchuljahrComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void AlleSchuljahre_Checked(object sender, RoutedEventArgs e)
        {
            schuljahrComboBox.IsEnabled = false;
            
        }

        private void AlleSchuljahre_Unchecked(object sender, RoutedEventArgs e)
        {
            schuljahrComboBox.IsEnabled = true;
        }
    }
}
