using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace DataGrid_DetachedModeExample
{
    public partial class MainWindow : Window
    {
        private SqlDataAdapter dataAdapter;
        private DataSet dataSet;
        private SqlConnection connection;
        private string connectionString;

        public MainWindow()
        {
            InitializeComponent();
            ShowConnectionWindow();
        }

        private void ShowConnectionWindow()
        {
            ConnectionWindow connectionWindow = new ConnectionWindow();
            if (connectionWindow.ShowDialog() == true)
            {
                connectionString = connectionWindow.ConnectionString;
                InitDbObjects();
            }
            else
            {
                Close();
            }
        }

        private void InitDbObjects()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            LoadTables();
        }

        private void LoadTables()
        {
            DataTable schemaTable = connection.GetSchema("Tables");
            foreach (DataRow row in schemaTable.Rows)
            {
                TablesComboBox.Items.Add(row[2].ToString());
            }
        }

        private void TablesComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (TablesComboBox.SelectedItem != null)
            {
                string tableName = TablesComboBox.SelectedItem.ToString();
                LoadTableData(tableName);
            }
        }

        private void LoadTableData(string tableName)
        {
            dataSet = new DataSet();
            string selectQuery = $"SELECT * FROM {tableName}";
            dataAdapter = new SqlDataAdapter(selectQuery, connection);
            new SqlCommandBuilder(dataAdapter);
            dataAdapter.Fill(dataSet, tableName);
            dataGrid.ItemsSource = dataSet.Tables[tableName].DefaultView;
        }

        private void FillButton_Click(object sender, RoutedEventArgs e)
        {
            if (TablesComboBox.SelectedItem != null)
            {
                string tableName = TablesComboBox.SelectedItem.ToString();
                LoadTableData(tableName);
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (TablesComboBox.SelectedItem != null)
            {
                string tableName = TablesComboBox.SelectedItem.ToString();
                dataAdapter.Update(dataSet, tableName);
                LoadTableData(tableName);
            }
        }
    }
}
