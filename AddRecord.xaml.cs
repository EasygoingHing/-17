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


namespace Крутолапов
{
    /// <summary>
    /// Логика взаимодействия для AddRecord.xaml
    /// </summary>
    public partial class AddRecord : Window
    {
        public AddRecord()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Доступ к контексту данных.
        /// </summary>
        RatingTennisistovEntities _dataBase = RatingTennisistovEntities.GetContext();
        /// <summary>
        /// Элемент таблицы.
        /// </summary>
        RatingTable _object = new RatingTable();        

        private void btAddRecord_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if(tbFIOsportsmena.Text.Length == 0) errors.AppendLine("Введите ФИО");
            else if(tbFIOsportsmena.Text.Length < 10)
                    errors.AppendLine("ФИО слишком короткое");

            if (tbGender.Text != "Муж" && tbGender.Text != "Жен")
                errors.AppendLine("Введите пол Муж/Жен");

            if (DataPicker.Text.Length == 0) errors.AppendLine("Введите дату");

            if (tbFIOtrenera.Text.Length == 0) errors.AppendLine("Введите ФИО");
            else if (tbFIOtrenera.Text.Length < 10)
                errors.AppendLine("ФИО слишком короткое");

            if(tbNazvaniyStran.Text.Length == 0) errors.AppendLine("Введите название страны");
            if (tbRating2017.Text.Length == 0) errors.AppendLine("Введите рейтинг спортсмена");
            if (tbRating2018.Text.Length == 0) errors.AppendLine("Введите рейтинг спортсмена");
            if (tbRating2019.Text.Length == 0) errors.AppendLine("Введите рейтинг спортсмена");
            if (tbRating2020.Text.Length == 0) errors.AppendLine("Введите рейтинг спортсмена");
            if (tbRating2021.Text.Length == 0) errors.AppendLine("Введите рейтинг спортсмена");


            if(errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            //Создаем элемент таблицы
            //RatingTable _object = new RatingTable();
            //Заполняем этот элемент
            _object.FIOSportsmena = tbFIOsportsmena.Text;
            _object.Gender = tbGender.Text;
            _object.YearOfBirth = (DateTime)DataPicker.SelectedDate;
            _object.FIOTrenera = tbFIOtrenera.Text;
            _object.NazvaniyStran = tbNazvaniyStran.Text;
            try
            {
                _object.Rating2017 = int.Parse(tbRating2017.Text);
                _object.Rating2018 = int.Parse(tbRating2018.Text);
                _object.Rating2019 = int.Parse(tbRating2019.Text);
                _object.Rating2020 = int.Parse(tbRating2020.Text);
                _object.Rating2021 = int.Parse(tbRating2021.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("В поле рейтинга вводите числа");
            }
            _object.Rating2017 = int.Parse(tbRating2017.Text);
            _object.Rating2018 = int.Parse(tbRating2018.Text);
            _object.Rating2019 = int.Parse(tbRating2019.Text);
            _object.Rating2020 = int.Parse(tbRating2020.Text);
            _object.Rating2021 = int.Parse(tbRating2021.Text);

            try
            {
                //Добавляем в БД
                _dataBase.RatingTables.Add(_object);
                _dataBase.SaveChanges();
                //MessageBox.Show("Информация сохранена")
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }        
    }
}
