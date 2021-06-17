using DspHelper.Models;
using DspHelper.ViewModels;
using DspHelper.ViewModels.ItemsCollection;
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

namespace DspHelper.Views.Controls
{
    /// <summary>
    /// Interaction logic for DspItemPickerControl.xaml
    /// </summary>
    public partial class DspItemSelectorControl : UserControl
    {

        private Grid _ItemsPanel;


        public int CellSize { get; set; }

        public DspItemType Type
        {
            get => (DspItemType)GetValue(TypeProperty);
            set => SetValue(TypeProperty, value);
        }
        public ICommand SelectItemCommand
        {
            get => (ICommand)GetValue(SelectItemCommandProperty);
            set => SetValue(SelectItemCommandProperty, value);
        }



        // DependencyProperty for Type
        public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
            "Type",
            typeof(DspItemType),
            typeof(DspItemSelectorControl),
            new FrameworkPropertyMetadata(DspItemType.Component, (d, e) => ((DspItemSelectorControl)d).SetDataContext())
        );

        // DependencyProperty for PickItemCommand
        public static readonly DependencyProperty SelectItemCommandProperty = DependencyProperty.Register(
            "SelectItemCommand",
            typeof(ICommand),
            typeof(DspItemSelectorControl),
            new FrameworkPropertyMetadata(null)
        );


        public DspItemSelectorControl()
        {
            InitializeComponent();
            GetItemsPanel();
            SetDataContext();
        }


        private void GetItemsPanel()
        {
            itemsControl.ApplyTemplate();
            if (itemsControl.Template.FindName("itemsPresenter", itemsControl) is ItemsPresenter ip)
            {
                ip.ApplyTemplate();
                if (itemsControl.ItemsPanel.FindName("itemsPanel", ip) is Grid grid)
                    _ItemsPanel = grid;
            }
        }


        private void SetDataContext()
        {
            switch (Type)
            {
                case DspItemType.Component:
                    itemsControl.DataContext = App.Current.Services.GetService(
                        typeof(DspComponentsCollectionViewModel));
                    break;
                case DspItemType.Building:
                    itemsControl.DataContext = App.Current.Services.GetService(
                        typeof(DspBuildingsCollectionViewModel));
                    break;
                case DspItemType.Source:
                    itemsControl.DataContext = App.Current.Services.GetService(
                        typeof(DspSourcesCollectionViewModel));
                    break;
            }

            SetItemPanelColumnsAndRows();
        }


        private void SetItemPanelColumnsAndRows()
        {
            if (_ItemsPanel is not null)
            {
                _ItemsPanel.ColumnDefinitions.Clear();
                _ItemsPanel.RowDefinitions.Clear();

                for (int i = 0; i < ((DspItemsCollection)itemsControl.DataContext).Columns; i++)
                    _ItemsPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength() });

                for (int i = 0; i < ((DspItemsCollection)itemsControl.DataContext).Rows; i++)
                    _ItemsPanel.RowDefinitions.Add(new RowDefinition() { Height = new GridLength() });
            }
        }

        private void OnItemButtonClick(object sender, RoutedEventArgs e)
        {
            SelectItemCommand.Execute(((Button)sender).DataContext);
        }
    }
}
