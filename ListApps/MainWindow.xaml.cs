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
using ListApps.Models;
using System.ComponentModel;
using ListApps.Services;
using System.Data;

namespace ListApps
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string Path = $"{Environment.CurrentDirectory}\\appDataList.json";
        private BindingList<AppModel> _appDataList;
        private FileIOServices _fileIoServices;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _fileIoServices = new FileIOServices(Path);
            try
            {
                _appDataList = _fileIoServices.LoadData();

            }
            catch(Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
                Close();
            }


            dgApps.ItemsSource = _appDataList;
            _appDataList.ListChanged += _appDataList_ListChanged;
        }

        private void _appDataList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if(e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.ItemChanged)
            {
                try
                {
                    _fileIoServices.SaveData(sender);

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}");
                    Close();
                }

            }
        }
    }
}
