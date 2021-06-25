using DspHelper.Helpers;
using DspHelper.Models;
using DspHelper.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace DspHelper.Services
{
    public class DspItemsCollectionSerializer
    {
        private bool _IsUpdated;
        private readonly XDocument _Document;



        public DspItemsCollectionSerializer()
        {
            _Document = XDocument.Load(Constants.DataFile);
        }



        public void Serialize()
        {
            if (_IsUpdated)
            {

            }
        }


        public (ObservableCollection<DspItemViewModel>, int columns, int rows) GetItemsCollection(DspItemType type)
        {
            ObservableCollection<DspItemViewModel> items = new();
            int columns = new();
            int rows = new();

            foreach (XElement element in _Document.Element("Items").Element($"{type}s").Elements())
            {
                string name = element.Attribute("Name").Value;
                ImageSource icon = new BitmapImage(new Uri(element.Attribute("Icon").Value));
                DspItem item = new(name, icon, type);

                int column = Convert.ToInt32(element.Attribute("Column").Value);
                int row = Convert.ToInt32(element.Attribute("Row").Value);

                if (columns < column + 1)
                    columns = column + 1;
                if (rows < row + 1)
                    rows = row + 1;


                DspItemViewModel itemVm = new(item, column, row);
                itemVm.ItemEdited += OnItemEdited;
                items.Add(itemVm);
            }

            return (items, columns, rows);
        }



        private void OnItemEdited(DspItemViewModel sender, ItemEditedEventArgs e)
        {
            DspItem editedItem = e.EditedItem;

            XElement collection = _Document.Element("Items").Element($"{sender.Item.Type}s");
            if (collection.Elements($"{sender.Item.Type}").SingleOrDefault(
                x => x.Attribute("Name").Value == sender.Item.Name) is XElement element)
            {
                element.Attribute("Name").Value = editedItem.Name;
                element.Attribute("Icon").Value = editedItem.Icon.ToString();
            }

            _IsUpdated = true;
        }
    }
}
