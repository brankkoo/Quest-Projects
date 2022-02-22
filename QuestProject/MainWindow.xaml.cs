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
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using Microsoft.Win32;

namespace QuestProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        //allows only numbers in results textbox
        private void results_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(results.Text, "[^0-9]"))
            {
                results.Text = results.Text.Remove(results.Text.Length - 1);
            }
        }

        private void savebtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //assigning UI values to object Properties
                ProjectInfo info = new ProjectInfo();
                info.projectName = ProjectBox.Text;
                info.description = DescriptionBox.Text;
                info.startDate = DateTime.Parse(StartDate.Text);
                info.endDate = DateTime.Parse(EndDate.Text);
                info.maxResults = float.Parse(results.Text);
                info.notification = Notify.IsChecked;
                info.projectType = ProjectType.SelectedIndex;
                //Callin the method Saveinfo and passing in a object as the first parametar and a string as the filename parametar
                SaveInfo.saveinfo(info, info.projectName + ".xml");

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void newbtn_Click(object sender, RoutedEventArgs e)
        {
            //Clearing The UI
            ProjectBox.Text = "";
            DescriptionBox.Text = "";
            StartDate.Text = "";
            EndDate.Text = "";
            results.Text = "";
            Notify.IsChecked = false;
            FolderBox.Text = "";
            ProjectType.SelectedItem = firstItem;
        }

        private void Openbtn_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();



            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML Files (*.xml)|*.xml";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                FolderBox.Text = filename;
                using (StreamWriter objWriter = new StreamWriter("temp.txt", true /*append to file*/))
                {
                    objWriter.WriteLine(FolderBox.Text);
                }

                    //Parsing the XML data
                    XmlDocument document = new XmlDocument();
                document.Load(filename);
             
                //making a list of nodes in between project info tags in the xml file
                XmlNodeList list = document.DocumentElement.SelectNodes("/ProjectInfo");
                string projectName = "", description = "", startDate = "", endDate = "", MaxResult= "", Notification = "", projectType = "";
                //going through each node and giving the approriate value to the variables
                foreach(XmlNode node in list)
                {
                    projectName = node.SelectSingleNode("projectName").InnerText;
                    description = node.SelectSingleNode("description").InnerText;
                    startDate = node.SelectSingleNode("startDate").InnerText;
                    endDate = node.SelectSingleNode("endDate").InnerText;
                    MaxResult = node.SelectSingleNode("maxResults").InnerText;
                    Notification = node.SelectSingleNode("notification").InnerText;
                    projectType = node.SelectSingleNode("projectType").InnerText;
                    //filling the WPF app with parsed Data
                    ProjectBox.Text = projectName;
                    DescriptionBox.Text = description;
                    StartDate.SelectedDate = DateTime.Parse(startDate);
                    EndDate.SelectedDate = DateTime.Parse(endDate);
                    results.Text = MaxResult;
                    Notify.IsChecked = bool.Parse(Notification);
                    ProjectType.SelectedIndex = int.Parse(projectType);
                }





            }

           
        }

        //Loads Default settings
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var line in File.ReadAllLines("temp.txt").Reverse().Take(5))
            {
                this.Recent.Items.Add(line);
                
            }
            


            ProjectInfo values = new ProjectInfo();
            values.projectName = Properties.Settings.Default.ProjName;
            values.description = Properties.Settings.Default.Desc;
            Properties.Settings.Default.DateStart = DateTime.Today;
            values.startDate = Properties.Settings.Default.DateStart;
            Properties.Settings.Default.DateEnd = values.startDate.AddDays(7);
            values.endDate = Properties.Settings.Default.DateEnd;
            values.maxResults = Properties.Settings.Default.ResultMax;
            values.notification = Properties.Settings.Default.Notifyy;
            values.projectType = Properties.Settings.Default.combobox;

            ProjectBox.Text = values.projectName;
            DescriptionBox.Text = values.description;
            StartDate.SelectedDate = values.startDate;
            EndDate.SelectedDate = values.endDate;
            results.Text = values.maxResults.ToString();
            Notify.IsChecked = values.notification;
            ProjectType.SelectedIndex = values.projectType;

        }

       

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".xml";
            saveFileDialog.Filter = "XML Files (*.xml)|*.xml";
            if (saveFileDialog.ShowDialog() == true)
            {
                string filename = saveFileDialog.FileName;

                ProjectInfo info = new ProjectInfo();
                info.projectName = ProjectBox.Text;
                info.description = DescriptionBox.Text;
                info.startDate = DateTime.Parse(StartDate.Text);
                info.endDate = DateTime.Parse(EndDate.Text);
                info.maxResults = float.Parse(results.Text);
                info.notification = Notify.IsChecked;
                info.projectType = ProjectType.SelectedIndex;
                //Callin the method Saveinfo and passing in a object as the first parametar and a string as the filename parametar
                SaveInfo.saveinfo(info, filename);

            }
        }

        
    }
}
