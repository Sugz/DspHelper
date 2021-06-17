using DspHelper.Models;
using DspHelper.ViewModels;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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


namespace DspHelper.Views
{

    [Flags]
    public enum DspItemPickerType
    {
        Components = 1,
        Buildings = 2,
        NaturalSources = 4,
    }


    /// <summary>
    /// Interaction logic for DspItemPicker.xaml
    /// </summary>
    public partial class DspItemPicker : Window
    {
        private DspItemPickerType _Type;


        public DspItemViewModel PickedItem { get; private set; }
        public DspItemPickerType Type 
        { 
            get => _Type; 
            set
            {
                _Type = value;

                componentsView.Visibility = value.HasFlag(DspItemPickerType.Components) ? Visibility.Visible : Visibility.Collapsed;
                buildingsView.Visibility = value.HasFlag(DspItemPickerType.Buildings) ? Visibility.Visible : Visibility.Collapsed;
                sourcesView.Visibility = value.HasFlag(DspItemPickerType.NaturalSources) ? Visibility.Visible : Visibility.Collapsed;
            }
        }





        private RelayCommand<DspItemViewModel> _PickItemCommand;
        public RelayCommand<DspItemViewModel> PickItemCommand => _PickItemCommand ??= new(PickItem);

        private void PickItem(DspItemViewModel item)
        {
            PickedItem = item;
            DialogResult = true;
            Close();
        }





        public DspItemPicker()
        {
            InitializeComponent();
        }
    }
}
