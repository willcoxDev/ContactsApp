using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Xml;
using System.Xml.Linq;
using Windows.Storage;
using System.Collections.ObjectModel;
using System.Reflection;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ContactsApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private ObservableCollection<string> lstd = new ObservableCollection<string>();
        public ObservableCollection<string> myList { get { return lstd; } }
        public MainPage()
        {
            this.InitializeComponent();

        }

        private void Grid_Loading(FrameworkElement sender, object args)
        {
            loadContacts();
        }
        public void loadContacts()
        {
            const string fileName = "Contacts.xml";

            var assembly = typeof(MainPage).GetTypeInfo().Assembly;

            var path = assembly.GetManifestResourceNames()
                .FirstOrDefault(n => n.EndsWith(fileName, StringComparison.OrdinalIgnoreCase));

            if (path == null)
                throw new Exception("File not found");

            using (var stream = assembly.GetManifestResourceStream(path))
            using (var reader = XmlReader.Create(stream))
            {
                while (reader.Read())
                {
                    if (reader.Name.Equals("FirstName") && (reader.NodeType == XmlNodeType.Element))
                    {
                        lstd.Add(reader.ReadElementContentAsString());
                    }
                }
            }

            DataContext = this; // better to move this inside the constructor
        }
    }
}
