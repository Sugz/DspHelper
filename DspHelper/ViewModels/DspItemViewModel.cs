using DspHelper.Models;
using DspHelper.Views;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private DspRecipe _EditingRecipe;

        private RelayCommand<bool> _EditItemCommand;
        private RelayCommand _AcceptEditCommand;
        private RelayCommand _AddRecipeCommand;
        private RelayCommand<object> _AddRecipeItemCommand;
        

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


        public DspRecipe EditingRecipe
        {
            get => _EditingRecipe;
            set => SetProperty(ref _EditingRecipe, value);
        }



        public RelayCommand<bool> EditItemCommand =>
            _EditItemCommand ??= new(EditItem);

        public RelayCommand AcceptEditCommand =>
            _AcceptEditCommand ??= new(AcceptEdit);

        public RelayCommand AddRecipeCommand => 
            _AddRecipeCommand ??= new(() => EditingRecipe = Item.AddRecipe());


        public RelayCommand<object> AddRecipeItemCommand =>
            _AddRecipeItemCommand ??= new(AddRecipeItem);



        public delegate void ItemEditedEventHandler(DspItemViewModel sender, ItemEditedEventArgs e);
        public event ItemEditedEventHandler ItemEdited;
        private void OnItemEdited(DspItem editedItem)
        {
            ItemEditedEventHandler handler = ItemEdited;
            handler?.Invoke(this, new ItemEditedEventArgs(editedItem));
        }



        private void EditItem(bool value)
        {
            EditingItem = value ? _Item.ShallowCopy() : null;
            if (EditingItem is not null && EditingItem.Recipes.Count > 0)
                EditingRecipe = EditingItem.Recipes[0];
        }


        private void AcceptEdit()
        {
            OnItemEdited(_EditingItem);
            Item = _EditingItem;
            EditingItem = null;
        }


        private void AddRecipeItem(object dspRecipeItemType)
        {
            if (dspRecipeItemType is DspRecipeItemType type)
            {
                DspItemPicker itemPickerDialog = new();
                itemPickerDialog.Type = type switch 
                {
                    DspRecipeItemType.Input => DspItemPickerType.Components | DspItemPickerType.NaturalSources,
                    DspRecipeItemType.Output => DspItemPickerType.Components | DspItemPickerType.Buildings,
                    _ => DspItemPickerType.Buildings,
                };

                if (itemPickerDialog.ShowDialog().Value)
                {
                    switch (type)
                    {
                        case DspRecipeItemType.Input:
                            _EditingRecipe.Inputs.Add(new DspRecipeItem() { Item = itemPickerDialog.PickedItem.Item });
                            break;
                        case DspRecipeItemType.Output:
                            _EditingRecipe.Outputs.Add(new DspRecipeItem() { Item = itemPickerDialog.PickedItem.Item });
                            break;
                        case DspRecipeItemType.Building:
                        default:
                            _EditingRecipe.Buildings.Add(itemPickerDialog.PickedItem.Item);
                            break;
                    }
                }
            }

        }

        public DspItemViewModel(DspItem item, int column, int row)
        {
            Item = item;
            Column = column;
            Row = row;
        }
    }


    public class ItemEditedEventArgs : EventArgs
    {
        public DspItem EditedItem { get; init; }

        public ItemEditedEventArgs(DspItem editedItem)
        {
            EditedItem = editedItem;
        }
    }
}
