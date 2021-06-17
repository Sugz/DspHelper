using DspHelper.Models;
using DspHelper.Views;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace DspHelper.ViewModels
{
    public class DspItemViewModel : ObservableObject
    {
        private DspItem _Item;
        private int _Column;
        private int _Row;
        private DspItem _EditingItem; 

        private RelayCommand<bool> _EditItemCommand;
        private RelayCommand _AcceptEditCommand;
        private RelayCommand _AddRecipeCommand;
        private RelayCommand<DspRecipe> _AddRecipeInputCommand;
        private RelayCommand<DspRecipe> _AddRecipeOutputCommand;
        private RelayCommand<DspRecipe> _AddRecipeBuildingCommand;
        

        public DspItem Item
        {
            get => _Item;
            set => SetProperty(ref _Item, value);
        }

        public int Column
        {
            get => _Column;
            set => SetProperty(ref _Column, value);
        }

        public int Row
        {
            get => _Row;
            set => SetProperty(ref _Row, value);
        }

        public DspItem EditingItem
        {
            get => _EditingItem;
            private set => SetProperty(ref _EditingItem, value);
        }



        public RelayCommand<bool> EditItemCommand =>
            _EditItemCommand ??= new((value) => EditingItem = value ? _Item.ShallowCopy() : null);

        public RelayCommand AcceptEditCommand =>
            _AcceptEditCommand ??= new(AcceptEdit);

        public RelayCommand AddRecipeCommand => 
            _AddRecipeCommand ??= new(Item.AddRecipe);

        public RelayCommand<DspRecipe> AddRecipeInputCommand => 
            _AddRecipeInputCommand ??= new((recipe) => AddRecipeItem(recipe, true));

        public RelayCommand<DspRecipe> AddRecipeOutputCommand => 
            _AddRecipeOutputCommand ??= new((recipe) => AddRecipeItem(recipe, false));

        public RelayCommand<DspRecipe> AddRecipeBuildingCommand =>
            _AddRecipeBuildingCommand ??= new(AddRecipeBuilding);



        public event EventHandler ItemEdited;
        private void OnItemEdited()
        {
            EventHandler handler = ItemEdited;
            handler?.Invoke(this, new EventArgs());
        }


        private void AcceptEdit()
        {
            Item = _EditingItem;
            EditingItem = null;
            OnItemEdited();
        }

        private void AddRecipeItem(DspRecipe recipe, bool isInput)
        {
            DspItemPicker itemPickerDialog = new();
            if (itemPickerDialog.ShowDialog().Value)
            {
                ICollection<DspRecipeItem> collection = isInput ? recipe.Inputs : recipe.Outputs;
                collection.Add(new DspRecipeItem() { Item = itemPickerDialog.PickedItem.Item });
            }
        }

        private void AddRecipeBuilding(DspRecipe recipe)
        {
            DspItemPicker itemPickerDialog = new();
            if (itemPickerDialog.ShowDialog().Value)
                recipe.Buildings.Add(itemPickerDialog.PickedItem.Item);
        }




        public DspItemViewModel(DspItem item, int column, int row)
        {
            Item = item;
            Column = column;
            Row = row;
        }
    }
}
