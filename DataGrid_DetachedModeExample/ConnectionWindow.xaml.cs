using System;
using System.Windows;

namespace DataGrid_DetachedModeExample
{
    public partial class ConnectionWindow : Window
    {
        public string ConnectionString { get; private set; }

        public ConnectionWindow()
        {
            InitializeComponent();
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            string dataSource = DataSourceTextBox.Text;
            string database = DatabaseTextBox.Text;

            if (string.IsNullOrWhiteSpace(dataSource) || string.IsNullOrWhiteSpace(database))
            {
                MessageBox.Show("Введите базу правильно.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ConnectionString = $"Data Source={dataSource};Initial Catalog={database};Integrated Security=SSPI;";
            DialogResult = true;
            Close();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close(); 
           
        }
    }
}
