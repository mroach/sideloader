using MarkdownSharp;
using Newtonsoft.Json;
using Sideloader.CatalogApi;
using Sideloader.FlightSim;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace Sideloader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Language = System.Windows.Markup.XmlLanguage.GetLanguage(CultureInfo.CurrentUICulture.IetfLanguageTag);
        }

        private void addinsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                return;
            }

            var item = (Package)e.AddedItems[0];

            if (item.Description.Length > 0)
            {
                var md = new Markdown();
                var css = "body, html {margin: 0; padding: 0; font-family: 'Segoe UI', sans-serif}";
                var html = String.Format("<html><head><style type=\"text/css\">{0}</style></head><body>{1}</body></html>", css, md.Transform(item.Description));
                Console.WriteLine(html);
                InfoBrowser.NavigateToString(html);
            }
            else
            {
                InfoBrowser.Navigate("about:blank");
            }

            Console.WriteLine(item.Description);

            InfoTitle.Text = item.Title;
        }

        private async void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                return;
            }
            var listItem = (ListViewItem)e.AddedItems[0];
            var packageType = listItem.Tag.ToString();

            var registry = PackageRegistry.GetInstance(packageType);
            var client = new Client();

            var result = await client.GetIndexAsync(packageType);
            registry.Clear();
            registry.AddMany(result.Collection);

            addinsGrid.ItemsSource = PackageRegistry.GetInstance(packageType).All();
        }
    }
}
