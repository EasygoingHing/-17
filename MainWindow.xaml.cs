using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Крутолапов
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        RatingTennisistovEntities _dataBase = RatingTennisistovEntities.GetContext();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Загружаем таблицу из базы данных.
            _dataBase.RatingTables.Load();
            //Загружаем таблицу в DataGrid с отслеживанием контекста.
            DataGrid.ItemsSource = _dataBase.RatingTables.Local.ToBindingList();
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            AddRecord ar = new AddRecord();
            ar.ShowDialog();
            DataGrid.Focus();
        }

        private void btEdit_Click(object sender, RoutedEventArgs e)
        {
            int indexRow = DataGrid.SelectedIndex;
            if (indexRow != -1)
            {
                RatingTable row = (RatingTable)DataGrid.Items[indexRow];
                SupClass._fIOSportsmena = row.FIOSportsmena;
                

                EditRecord f = new EditRecord();
                f.ShowDialog();
                DataGrid.Items.Refresh();
                DataGrid.Focus();
            }
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;

            result = MessageBox.Show("Удалить запись?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    RatingTable row = (RatingTable)DataGrid.SelectedItems[0];
                    _dataBase.RatingTables.Remove(row);
                    _dataBase.SaveChanges();
                }
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show("Выберите запись");
                }
            }
        }



        private void btFind_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                for (int i = 0; i < DataGrid.Items.Count; i++)
                {
                    var row = (RatingTable)DataGrid.Items[i];
                    string findContent = row.FIOSportsmena.ToString();

                    if (findContent != null && findContent.Contains(TB.Text))
                    {
                        var item = DataGrid.Items[i];
                        DataGrid.SelectedItem = item;//выделяем элемент
                        DataGrid.ScrollIntoView(item);//скролим к нему в окно
                        DataGrid.Focus();
                        break;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Запись с заданным не найдена.", "Запись не найдена!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        List<RatingTable> _ratingTable;

        private void btFilterRec_Click(object sender, RoutedEventArgs e)
        {
            _ratingTable = _dataBase.RatingTables.ToList();
            var filtered = _ratingTable.Where(_ratingTable => _ratingTable.FIOTrenera == FilterTB.Text);
            DataGrid.ItemsSource = filtered;
        }

        private void Exit_click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
