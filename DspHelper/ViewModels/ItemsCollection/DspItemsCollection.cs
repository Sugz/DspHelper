using DspHelper.Helpers;
using DspHelper.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;


namespace DspHelper.ViewModels.ItemsCollection
{

    public abstract class DspItemsCollection : ObservableObject
    {
        private DspItemType _Type;
        private int _Columns;
        private int _Rows;

        public int Columns
        {
            get => _Columns;
            set => SetProperty(ref _Columns, value);
        }

        public int Rows
        {
            get => _Rows;
            set => SetProperty(ref _Rows, value);
        }


        public ObservableCollection<DspItemViewModel> Items { get; } = new();

        public DspItemsCollection()
        {
            _Type = GetItemsType();
            Deserialize();

            //foreach (DspItemViewModel item in Items)
            //    item.ItemEdited += (s, e) => Serialize(item);
        }


        //protected abstract void Serialize(DspItemViewModel item);
        //protected abstract void Deserialize();

        protected abstract DspItemType GetItemsType();



        private void Deserialize()
        {
            XDocument doc = XDocument.Load(Constants.DataFile);
            foreach (XElement element in doc.Element("Items").Element($"{_Type}s").Elements())
            {
                string name = element.Attribute("Name").Value;
                ImageSource icon = new BitmapImage(new Uri(element.Attribute("Icon").Value));
                DspItem item = new(name, icon, _Type);

                int column = Convert.ToInt32(element.Attribute("Column").Value);
                int row = Convert.ToInt32(element.Attribute("Row").Value);

                if (Columns < column + 1)
                    Columns = column + 1;
                if (Rows < row + 1)
                    Rows = row + 1;

                Items.Add(new DspItemViewModel(item, column, row));
            }
        }
    }
}
