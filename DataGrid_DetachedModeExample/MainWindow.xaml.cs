using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
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

namespace DataGrid_DetachedModeExample
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SqlDataAdapter studentsDataAdapter;
        private DataSet studentsDataSet;
        private const string STUDENTS_TABLE_NAME = "students_t";

        public MainWindow()
        {
            InitializeComponent();
            // 
            InitDbObjects();
        }

        private void InitDbObjects()
        {
            // 1. создать подключение к БД
            string connectionString = @"
                Data Source=LAPTOP-SCD1CHJF\SQLEXPRESS;
                Initial Catalog=students_db_pv324;
                Integrated Security=SSPI;
                Timeout=5
            ";
            SqlConnection connection = new SqlConnection(connectionString);
            // 2. подготовить dataAdapter
            string selectQuery = "SELECT * FROM students_t;";
            studentsDataAdapter = new SqlDataAdapter(selectQuery, connection);
            new SqlCommandBuilder(studentsDataAdapter);
            // 
            studentsDataSet = null;
        }

        private void fillButton_Click(object sender, RoutedEventArgs e)
        {
            studentsDataSet = new DataSet();
            studentsDataAdapter.Fill(studentsDataSet, STUDENTS_TABLE_NAME);
            // привяжем таблицу DataSet в качестве источника данных для DataGrid
            studentsDataGrid.ItemsSource = studentsDataSet.Tables[STUDENTS_TABLE_NAME].DefaultView;
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            studentsDataAdapter.Update(studentsDataSet, STUDENTS_TABLE_NAME);
            fillButton_Click(sender, e);    // вызвать обработчик обновления данных следом
        }
    }
}
