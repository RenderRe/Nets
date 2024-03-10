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

namespace задание_2_wpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        tovEntities entities = new tovEntities();
        public MainWindow()
        {
            InitializeComponent();
            foreach (var tovar in entities.Tovar)
                TovaryList.Items.Add(tovar);
            foreach (var vid in entities.VidTovara)
                comboBox_TypeOfTovar.Items.Add(vid);

        }

        private void TovaryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // получает элемент в текущем выделении или null, если выделение пусто
            var selected_tovar = TovaryList.SelectedItem as Tovar;

            if (selected_tovar != null)
            {
                textBoxName.Text = selected_tovar.nazvanie;
                //comboBox_TypeOfTovar.SelectedIndex = selected_tovar.Vid - 1;
                comboBox_TypeOfTovar.SelectedItem = (from vid in entities.VidTovara where vid.id == selected_tovar.vid select vid).Single<VidTovara>();
            }
            else
            {
                textBoxName.Text = "";
                comboBox_TypeOfTovar.SelectedIndex = -1; // выделение пусто
            }

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // Сообщение с предупреждением об удалении
            var rezult = MessageBox.Show("Вы действительно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (rezult == MessageBoxResult.No)
                return;

            var delete_tovar = TovaryList.SelectedItem as Tovar;
            if (delete_tovar != null)
            {
                // Помечает данную сущность как Deleted. 
                // При сохранении изменений данная сущность будет удалена
                entities.Tovar.Remove(delete_tovar); // 10: entities.DeleteObject(delete_tovar);

                entities.SaveChanges();// Сохраняет все изменения
                textBoxName.Clear();

                // Удаляет ссылку указанного элемента из коллекции
                TovaryList.Items.Remove(delete_tovar);

                comboBox_TypeOfTovar.SelectedIndex = -1; // выделение пусто

                // Добавить сообщение об удалении записи

            }
            else
                MessageBox.Show("Нет удаляемых объектов!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // получает элемент в текущем выделении или null, если выделение пусто
            var tovar = TovaryList.SelectedItem as Tovar;
            if (textBoxName.Text == "" || comboBox_TypeOfTovar.SelectedIndex == -1)
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                if (tovar == null)
                {
                    tovar = new Tovar();
                    // добавляет сущность (состояние Added), при сохранении изменений 
                    // сущности в состоянии Added вставляются в БД
                    entities.Tovar.Add(tovar); // 10: entities.AddToTovari(add_tovar);

                    // добавляет элемент в коллекцию
                    TovaryList.Items.Add(tovar);
                }
                tovar.nazvanie = textBoxName.Text;
                tovar.vid = (comboBox_TypeOfTovar.SelectedItem as VidTovara).id;
                entities.SaveChanges();
                TovaryList.Items.Refresh();

                // Добавить сообщение о сохранении записи

            }

        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            textBoxName.Text = "";
            TovaryList.SelectedIndex = -1;
            comboBox_TypeOfTovar.SelectedIndex = -1;
            textBoxName.Focus();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var window_ = new window();
            window_.ShowDialog();

        }
    }
}
