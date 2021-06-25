using DspHelper.Helpers;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Linq;

namespace DspHelper.Models
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum DspRecipeType
    {
        [Description("Time (s)")]
        Time,
        [Description("Chances (%)")]
        Chances
    }

    public class DspRecipe : ObservableObject
    {
        private DspRecipeType _Type = DspRecipeType.Time;
        private int _Value = 1;


        public DspRecipeType Type
        {
            get => _Type;
            set => SetProperty(ref _Type, value);
        }

        public int Value
        {
            get => _Value;
            set => SetProperty(ref _Value, value);
        }


        public ObservableCollection<DspRecipeItem> Inputs { get; } = new();
        public ObservableCollection<DspRecipeItem> Outputs { get; } = new();
        public ObservableCollection<DspItem> Buildings { get; } = new();
    }
}
