using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DspHelper.Views
{
    /// <summary>
    /// Interaction logic for DspItemsView.xaml
    /// </summary>
    public partial class DspItemsView : UserControl
    {
        public DspItemsView()
        {
            InitializeComponent();
        }

        private void OnTextBoxPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(e.Text == "?" || int.TryParse(e.Text, out _));
        }
    }
}
