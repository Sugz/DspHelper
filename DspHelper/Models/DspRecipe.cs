using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Linq;

namespace DspHelper.Models
{
    public class DspRecipe : ObservableObject
    {
        private int _Time = 1;

        public int Time
        {
            get => _Time;
            set => SetProperty(ref _Time, value);
        }

        public ObservableCollection<DspRecipeItem> Inputs { get; } = new();
        public ObservableCollection<DspRecipeItem> Outputs { get; } = new();
        public ObservableCollection<DspItem> Buildings { get; } = new();
    }
}
