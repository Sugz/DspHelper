using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace DspHelper.Models
{
    public enum DspRecipeItemType
    {
        Input,
        Output,
        Building
    }

    public class DspRecipeItem : ObservableObject
    {
        private DspItem _Item;
        private int _Quantity = 1;

        public DspItem Item
        {
            get => _Item;
            set => SetProperty(ref _Item, value);
        }

        public int Quantity
        {
            get => _Quantity;
            set => SetProperty(ref _Quantity, value);
        }
    }
}
