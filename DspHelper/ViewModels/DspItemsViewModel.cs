using DspHelper.Models;
using DspHelper.Views;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace DspHelper.ViewModels
{
    public class DspItemsViewModel : ObservableObject
    {
        private DspItemViewModel _SelectedItem;

        private RelayCommand<DspItemViewModel> _SelectItemCommand;


        public DspItemViewModel SelectedItem
        {
            get => _SelectedItem;
            set => SetProperty(ref _SelectedItem, value);
            //set
            //{
            //    DspItemPicker itemPickerDialog = new() { Type = 
            //        DspItemPickerType.Components | 
            //        DspItemPickerType.Buildings | 
            //        DspItemPickerType.NaturalSources};

            //    if (itemPickerDialog.ShowDialog().Value)
            //    {
            //        return;
            //    }
            //}
        }


        public RelayCommand<DspItemViewModel> SelectItemCommand => 
            _SelectItemCommand ??= new((item) => SelectedItem = item);
    }
}
