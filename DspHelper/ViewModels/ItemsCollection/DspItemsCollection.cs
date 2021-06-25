using DspHelper.Helpers;
using DspHelper.Models;
using DspHelper.Services;
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


        public ObservableCollection<DspItemViewModel> Items { get; }




        public DspItemsCollection()
        {
            _Type = GetItemsType();
            DspItemsCollectionSerializer serializer = (DspItemsCollectionSerializer)App.Current.Services.GetService(
                typeof(DspItemsCollectionSerializer));

            (Items, Columns, Rows) = serializer.GetItemsCollection(_Type);
        }


        protected abstract DspItemType GetItemsType();
    }
}
