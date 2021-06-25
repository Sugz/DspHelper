using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;


namespace DspHelper.Models
{

    public enum DspItemType
    {
        Component,
        Building,
        Source
    }

    public class DspItem : ObservableObject
    {
        private string _Name;
        private ImageSource _Icon;
        private DspItemType _Type;


        public string Name
        {
            get => _Name;
            set => SetProperty(ref _Name, value);
        }

        public ImageSource Icon
        {
            get => _Icon;
            set => SetProperty(ref _Icon, value);
        }

        public DspItemType Type
        {
            get => _Type;
            set => SetProperty(ref _Type, value);
        }


        public ObservableCollection<DspRecipe> Recipes { get; } = new();


        public DspItem(string name, ImageSource icon, DspItemType type)
        {
            Name = name;
            Icon = icon;
            Type = type;
        }


        public DspRecipe AddRecipe()
        {
            DspRecipe recipe = new();
            recipe.Outputs.Add(new DspRecipeItem() { Item = this });
            Recipes.Add(recipe);
            return recipe;
        }


        public DspItem ShallowCopy()
        {
            return (DspItem)MemberwiseClone();
        }
    }
}
