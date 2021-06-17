using DspHelper.Helpers;
using DspHelper.Models;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace DspHelper.ViewModels.ItemsCollection
{
    public class DspBuildingsCollectionViewModel : DspItemsCollection
    {
        protected override DspItemType GetItemsType() => DspItemType.Building;

    }
}
