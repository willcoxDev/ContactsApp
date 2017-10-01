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
using System.Diagnostics;
using Windows.ApplicationModel;
using Windows.Storage.Streams;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ContactsApp
{
    public sealed partial class MainPage : Page
    {

        //string XMLPath = Path.Combine(Package.Current.InstalledLocation.Path, "Contacts.xml");
        private ObservableCollection<string> lstd = new ObservableCollection<string>();
        public ObservableCollection<string> myList { get { return lstd; } }

        private XDocument contactsDoc;


        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Grid_Loading(FrameworkElement sender, object args)
        {
            createContacts();

            loadContacts();
        }

        public async void createContacts()
        {
            XmlDocument doc = new XmlDocument();

            XmlElement el0 = (XmlElement)doc.AppendChild(doc.CreateElement("Contacts"));
            XmlElement el0_0 = (XmlElement)el0.AppendChild(doc.CreateElement("Contact"));
            XmlElement el0_0_0 = (XmlElement)el0_0.AppendChild(doc.CreateElement("ID"));
            el0_0_0.InnerText = "salpea";
            XmlElement el0_0_1 = (XmlElement)el0_0.AppendChild(doc.CreateElement("FirstName"));
            el0_0_1.InnerText = "Sally";
            XmlElement el0_0_2 = (XmlElement)el0_0.AppendChild(doc.CreateElement("LastName"));
            el0_0_2.InnerText = "Pearson";
            XmlElement el0_0_3 = (XmlElement)el0_0.AppendChild(doc.CreateElement("Mobile"));
            el0_0_3.InnerText = "0431529562";
            XmlElement el0_0_4 = (XmlElement)el0_0.AppendChild(doc.CreateElement("Email"));
            el0_0_4.InnerText = "sallyp@hotmail.com";

            XmlElement el0_1 = (XmlElement)el0.AppendChild(doc.CreateElement("Contact"));
            XmlElement el0_1_0 = (XmlElement)el0_1.AppendChild(doc.CreateElement("ID"));
            el0_1_0.InnerText = "gresul";
            XmlElement el0_1_1 = (XmlElement)el0_1.AppendChild(doc.CreateElement("FirstName"));
            el0_1_1.InnerText = "Greg";
            XmlElement el0_1_2 = (XmlElement)el0_1.AppendChild(doc.CreateElement("LastName"));
            el0_1_2.InnerText = "Sullivan";
            XmlElement el0_1_3 = (XmlElement)el0_1.AppendChild(doc.CreateElement("Mobile"));
            el0_1_3.InnerText = "0432928381";
            XmlElement el0_1_4 = (XmlElement)el0_1.AppendChild(doc.CreateElement("Email"));
            el0_1_4.InnerText = "gregsul@outlook.com";

            XmlElement el0_2 = (XmlElement)el0.AppendChild(doc.CreateElement("Contact"));
            XmlElement el0_2_0 = (XmlElement)el0_2.AppendChild(doc.CreateElement("ID"));
            el0_2_0.InnerText = "chrmac";
            XmlElement el0_2_1 = (XmlElement)el0_2.AppendChild(doc.CreateElement("FirstName"));
            el0_2_1.InnerText = "Christie";
            XmlElement el0_2_2 = (XmlElement)el0_2.AppendChild(doc.CreateElement("LastName"));
            el0_2_2.InnerText = "Mack";
            XmlElement el0_2_3 = (XmlElement)el0_2.AppendChild(doc.CreateElement("Mobile"));
            el0_2_3.InnerText = "0421231231";
            XmlElement el0_2_4 = (XmlElement)el0_2.AppendChild(doc.CreateElement("Email"));
            el0_2_4.InnerText = "christiemack@gmail.com";

            contactsDoc = XDocument.Parse(doc.OuterXml);

            return;
            //            file = await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync("Contacts.xml");

//            var path = Windows.Storage.ApplicationData.Current.LocalFolder.Path + "\\Contacts.xml";
//
//            var fs = new FileStream(path, FileMode.Create);

//            doc.Save(fs);

//            fs.Dispose();

//            XmlReader xmlReader = XmlReader.Create(path);
//            while (xmlReader.Read())
//            {
//                if (xmlReader.Name.Equals("ID") && (xmlReader.NodeType == XmlNodeType.Element))
//                {
//                    lstd.Add(xmlReader.ReadElementContentAsString());
//                }
//            }
//            DataContext = this;
//            xmlReader.Dispose();

        }

        public async void loadContacts()
        {
//            var path = Windows.Storage.ApplicationData.Current.LocalFolder.Path + "\\Contacts.xml";

//            XDocument xdoc = XDocument.Load(path);

            var people = contactsDoc.Element("Contacts").Elements("Contact").ToArray();

            lstd.Clear();

            foreach(var person in people)
            {
                lstd.Add(person.Element("ID").Value);
            }
            DataContext = this;

            return;

//            XmlReader xmlReader = XmlReader.Create("path");

//            while (xmlReader.Read())
//            {
//                if (xmlReader.Name.Equals("ID") && (xmlReader.NodeType == XmlNodeType.Element))
//                {
//                    lstd.Add(xmlReader.ReadElementContentAsString());
//                }
//            }
//            DataContext = this;
//            xmlReader.Dispose();
        }

        private async void lstBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
//            StorageFile file = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync("Contacts.xml");
            try
            {
//                XDocument contactsDoc = XDocument.Load(file.Path);
                if (lstBox.SelectedIndex != -1)
                {
                    var nodes = (from n in contactsDoc.Descendants("Contact").
                Where(r => r.Element("ID").Value == lstBox.SelectedItem.ToString())
                                 select new
                                 {
                                     ID = (string)n.Element("ID").Value,
                                     FirstName = (string)n.Element("FirstName").Value,
                                     LastName = (string)n.Element("LastName").Value,
                                     Mobile = (string)n.Element("Mobile").Value,
                                     Email = (string)n.Element("Email").Value
                                 });
                    foreach (var n in nodes)
                    {
                        txtID.Text = n.ID;
                        txtFirstName.Text = n.FirstName;
                        txtLastName.Text = n.LastName;
                        txtMobile.Text = n.Mobile;
                        txtEmail.Text = n.Email;
                    };
                }
                else
                {
                    txtID.Text = "";
                    txtFirstName.Text = "";
                    txtLastName.Text = "";
                    txtMobile.Text = "";
                    txtEmail.Text = "";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private async void updateXMLFile(XDocument xdoc)
        {
            try
            {
                //StorageFile file = await installedLocation.CreateFileAsync("Contacts.xml", CreationCollisionOption.ReplaceExisting);
                StorageFile file = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync("Contacts.xml"); //This line was the replacement for the one above.
                await FileIO.WriteTextAsync(file, xdoc.ToString());
            }
            catch (Exception ex)
            {
                String s = ex.ToString();
            }
        }

        private async void btnUpdateContact_Click(object sender, RoutedEventArgs e)
        {
//            StorageFile file = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync("Contacts.xml");
//            XDocument contactsDoc = XDocument.Load(file.Path);
            if (lstBox.SelectedIndex != -1)
            {
                XElement upd = (from elements in contactsDoc.Descendants("Contact")
                                where elements.Element("ID").Value == lstBox.SelectedItem.ToString()
                                select elements).Single();
                upd.Element("ID").Value = txtFirstName.Text.Substring(0, 3).ToLower() + txtLastName.Text.Substring(0, 3).ToLower();
                upd.Element("FirstName").Value = txtFirstName.Text;
                upd.Element("LastName").Value = txtLastName.Text;
                upd.Element("Mobile").Value = txtMobile.Text;
                upd.Element("Email").Value = txtEmail.Text;
                updateXMLFile(contactsDoc);

                myList.Clear();

                loadContacts();
            }
        }


        private async void btnDeleteContact_Click(object sender, RoutedEventArgs e)
        {
//            StorageFile file = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync("Contacts.xml");
//            XDocument contactsDoc = XDocument.Load(file.Path);
            if (lstBox.SelectedIndex != -1)
            {
                contactsDoc.Element("Contacts")
                    .Elements("Contact")
                    .Where(x => (string)x.Element("ID") == lstBox.SelectedItem.ToString()).Remove();
                lstBox.SelectedIndex = -1; 
                updateXMLFile(contactsDoc);

                myList.Clear();
                
                loadContacts();
            }
        }
    }
}






