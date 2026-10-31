using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Turchanova_DataBinding
{
    public class Service : INotifyPropertyChanged
    {
        private int id;
        private string name = "";
        private string executor = "";
        private int price = 0;
        private int quantity_h = 0;

        public int Id
        {
            get => id;
            set { id = value; OnPropertyChanged(); }
        }

        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged(); }
        }

        public string Executor
        {
            get => executor;
            set { executor = value; OnPropertyChanged(); }
        }

        public int Price
        {
            get => price;
            set
            {
                price = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ServiceCoast));
            }
        }

        public int Quantity
        {
            get => quantity_h;
            set { quantity_h = value; OnPropertyChanged();
                OnPropertyChanged(nameof(ServiceCoast));
            }

        }

        public int ServiceCoast => Price * Quantity;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
