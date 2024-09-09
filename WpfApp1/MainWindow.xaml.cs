using System.Security.AccessControl;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using System;
using Microsoft.Win32;
using static WpfApp1.Part;
using System.Reflection;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static String[] propetrties = typeof(Part).GetProperties().Select(G => G.Name).ToArray();

        public MainWindow()
        {
            InitializeComponent();

            cb1.ItemsSource = propetrties;
            cb2.ItemsSource = propetrties;
            cb3.ItemsSource = propetrties;
        }

        private void betoltesBTN_Click(object sender, RoutedEventArgs e)
        {
            string filename = "";

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".bsx";
            ofd.Filter = "BSX Files (*.bsx)|*.bsx";
            if (ofd.ShowDialog() == true)
            {
                // Open document 
                filename = ofd.FileName;
            }

            if (filename.Length > 0)
            {
                XDocument.Load(filename)
                    .Descendants("Item")
                    .ToList()
                    .ForEach(G => new Part(G));

                mainDG.ItemsSource = PartList;
                lblTunjEl.Visibility = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show("Hiba a fájl kiválasztása közben!");
            }

            
            
        }

        private void betoltesTobbBTN_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Multiselect = true,
            };
            ofd.DefaultExt = ".bsx";
            ofd.Filter = "BSX Files (*.bsx)|*.bsx";
            if (ofd.ShowDialog() == true)
            {
                foreach (string file in ofd.FileNames)
                {
                    XDocument.Load(file)
                        .Descendants("Item")
                        .ToList()
                        .ForEach(G => new Part(G));
                }

                mainDG.ItemsSource = PartList;
                lblTunjEl.Visibility = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show("Hiba a fájl kiválasztása közben!");
            }



        }

        private void txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Part> temp = PartList;

            if (cb1.SelectedIndex != -1 && !string.IsNullOrWhiteSpace(txt1.Text))
                temp = temp.Where(G => GetPropValue(G, cb1.SelectedValue.ToString()).ToLower().Contains(txt1.Text.ToLower())).ToList();

            if (cb2.SelectedIndex != -1 && !string.IsNullOrWhiteSpace(txt2.Text))
                temp = temp.Where(G => GetPropValue(G, cb2.SelectedValue.ToString()).ToLower().Contains(txt2.Text.ToLower())).ToList();

            if (cb3.SelectedIndex != -1 && !string.IsNullOrWhiteSpace(txt3.Text))
                temp = temp.Where(G => GetPropValue(G, cb3.SelectedValue.ToString()).ToLower().Contains(txt3.Text.ToLower())).ToList();

            mainDG.ItemsSource = temp;
        }

        public static string GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null).ToString();
        }
    }
}