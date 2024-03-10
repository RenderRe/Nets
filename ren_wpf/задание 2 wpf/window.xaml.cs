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

namespace задание_2_wpf
{
    /// <summary>
    /// Логика взаимодействия для window.xaml
    /// </summary>
    public partial class window : Window
    {

        tovEntities entities = new tovEntities();
        public window()
        {
            InitializeComponent();
            foreach (var vid in entities.VidTovara)
                TovaryList.Items.Add(vid);

        }

        private void TovaryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // получает элемент в текущем выделении или null, если выделение пусто
            var selected_tovar = TovaryList.SelectedItem as VidTovara;

            if (selected_tovar != null)
            {
                textBoxName.Text = selected_tovar.vid;
                //comboBox_TypeOfTovar.SelectedIndex = selected_tovar.Vid - 1;
            }
            else
            {
                textBoxName.Text = "";
            }

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // Сообщение с предупреждением об удалении
            var rezult = MessageBox.Show("Вы действительно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (rezult == MessageBoxResult.No)
                return;

            var delete_tovar = TovaryList.SelectedItem as VidTovara;
            if (delete_tovar != null)
            {
                try
                {
                    var exist_ = (from structure in entities.Tovar where structure.vid == delete_tovar.id select structure).First();
                    // Запись найдена
                    MessageBox.Show("Запись удалить нельзя!\nСуществуют товары данного вида продукции!", "Ощибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch
                {
                    TovaryList.Items.Remove(delete_tovar);
                }


            }
            else
                MessageBox.Show("Нет удаляемых объектов!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // получает элемент в текущем выделении или null, если выделение пусто
            var tovar = TovaryList.SelectedItem as VidTovara;
            if (textBoxName.Text == "")
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            if (tovar == null)
                {
                    tovar = new VidTovara();
                    // добавляет сущность (состояние Added), при сохранении изменений 
                    // сущности в состоянии Added вставляются в БД
                    entities.VidTovara.Add(tovar); // 10: entities.AddToTovari(add_tovar);

                    // добавляет элемент в коллекцию
                    TovaryList.Items.Add(tovar);
                }
                tovar.vid = textBoxName.Text;
                entities.SaveChanges();
                TovaryList.Items.Refresh();

                // Добавить сообщение о сохранении записи


        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            textBoxName.Text = "";
            TovaryList.SelectedIndex = -1;
            textBoxName.Focus();

        }
    }
}
