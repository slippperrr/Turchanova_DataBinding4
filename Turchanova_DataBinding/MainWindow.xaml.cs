using System;
using System.Collections.Generic;
using System.IO;
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
using System.Text.Json;

namespace Turchanova_DataBinding
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Service CurrentService = new Service();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = CurrentService;
            GenerateNewId();
        }
        private void GenerateNewId()
        {
            int id = 0;
            while (File.Exists($"{id}.json"))
            {
                id++;
            }
            CurrentService.Id = id;
        }
        private string FilePath(int id) => $"{id}.json";

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var json = JsonSerializer.Serialize(CurrentService);
                File.WriteAllText(FilePath(CurrentService.Id), json);
                Message.Text = $"Сохранено: {FilePath(CurrentService.Id)}";
            }
            catch (Exception ex)
            {
                Message.Text = "Ошибка сохранения: " + ex.Message;
            }
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            GenerateNewId();
            CurrentService.Name = "";
            CurrentService.Executor = "";
            CurrentService.Price = 0;
            CurrentService.Quantity = 0;
            FindId.Text = "";
            Message.Text = "";
        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(FindId.Text, out int id))
            {
                Message.Text = "Введите корректный ID (с нуля)";
                return;
            }

            var path = FilePath(id);
            if (!File.Exists(path))
            {
                Message.Text = $"Услуги с ID ({id}) нет!";
                return;
            }

            try
            {
                var json = File.ReadAllText(path);
                var ser = JsonSerializer.Deserialize<Service>(json);
                if (ser == null) throw new Exception("Пустой файл");

                CurrentService.Id = ser.Id;
                CurrentService.Name = ser.Name;
                CurrentService.Executor = ser.Executor;
                CurrentService.Price = ser.Price;
                CurrentService.Quantity = ser.Quantity;

                Message.Text = $"Загружено: ID = {id}";
            }
            catch (Exception ex)
            {
                Message.Text = "Ошибка чтения: " + ex.Message;
            }
        }
    }
}
