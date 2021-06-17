using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using System.Xml;
using System.Xml.Linq;

namespace DspDataGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal string ExecutableDirectory;
        internal string DataFile;

        public MainWindow()
        {
            InitializeComponent();

            ExecutableDirectory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            DataFile = $"{ExecutableDirectory}/data.xml";
            txt.Text = DataFile;
        }


        private XElement CreateItemCollection(Grid grid, string collectionName)
        {
            XElement collectionRoot = new($"{collectionName}s");

            int column, row;
            ImageSource icon;
            string name;

            foreach (Image img in grid.Children)
            {
                column = Grid.GetColumn(img);
                row = Grid.GetRow(img);
                icon = img.Source;
                name = icon.ToString().Split('/').Last()[5..^4].Replace("_", " "); //[5..^4] to remove "Icon_" & ".png"

                XElement item = new(collectionName);
                item.Add(new XAttribute("Name", name));
                item.Add(new XAttribute("Icon", icon));
                item.Add(new XAttribute("Column", column));
                item.Add(new XAttribute("Row", row));

                collectionRoot.Add(item);
            }

            return collectionRoot;
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            XElement items = new("Items");
            items.Add(CreateItemCollection(components_grid, "Component"));
            items.Add(CreateItemCollection(buildings_grid, "Building"));
            items.Add(CreateItemCollection(sources_grid, "Source"));
            

            /*
            XElement components = new("Components");
            XElement buildings = new("Buildings");
            XElement sources = new("Sources");
            items.Add(components);
            items.Add(buildings);
            items.Add(sources);

            
            int column, row;
            ImageSource icon;
            string name;

            foreach (Image img in components_grid.Children)
            {
                column = Grid.GetColumn(img);
                row = Grid.GetRow(img);
                icon = img.Source;
                name = icon.ToString().Split('/').Last()[5..^4].Replace("_", " "); //[5..^4] to remove "Icon_" & ".png"

                XElement component = new("Component");
                component.Add(new XAttribute("Name", name));
                component.Add(new XAttribute("Icon", icon));
                component.Add(new XAttribute("Column", column));
                component.Add(new XAttribute("Row", row));

                components.Add(component);
            }

            foreach (Image img in sources_grid.Children)
            {
                column = Grid.GetColumn(img);
                row = Grid.GetRow(img);
                icon = img.Source;
                name = icon.ToString().Split('/').Last()[5..^4].Replace("_", " "); //[5..^4] to remove "Icon_" & ".png"

                XElement building = new("Source");
                building.Add(new XAttribute("Name", name));
                building.Add(new XAttribute("Icon", icon));
                building.Add(new XAttribute("Column", column));
                building.Add(new XAttribute("Row", row));

                buildings.Add(building);
            }

            foreach (Image img in buildings_grid.Children)
            {
                column = Grid.GetColumn(img);
                row = Grid.GetRow(img);
                icon = img.Source;
                name = icon.ToString().Split('/').Last()[5..^4].Replace("_", " "); //[5..^4] to remove "Icon_" & ".png"

                XElement building = new("Building");
                building.Add(new XAttribute("Name", name));
                building.Add(new XAttribute("Icon", icon));
                building.Add(new XAttribute("Column", column));
                building.Add(new XAttribute("Row", row));

                buildings.Add(building);
            }
            */

            using XmlTextWriter writer = new(DataFile, null);
            writer.Formatting = Formatting.Indented;

            XDocument doc = new();
            doc.Add(items);
            doc.Save(writer);
        }




    }
}
