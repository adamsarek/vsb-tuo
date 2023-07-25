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
using System.Xml;

namespace SAR0083
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private XmlDocument xmlDocument = new XmlDocument();
        private FilmList filmList = new FilmList();
        private Film selectedFilm = new Film();
        private XmlNode selectedFilmXml = null;

        public MainWindow()
        {
            InitializeComponent();

            this.FilmListData.ItemsSource = this.filmList;

            this.Load();
        }

        public void Load()
        {
            this.filmList.Clear();
            try
            {
                xmlDocument.Load("filmList.xml");
            }
            catch (System.IO.FileNotFoundException)
            {
                xmlDocument.LoadXml("<?xml version=\"1.0\" ?>\n" +
                "<filmList>\n" +
/*                "\t<film name=\"MaDa is coming to your house!\" duration=\"98\" adultsOnly=\"true\" releaseDate=\"2020-01-01 1:23:45\" description=\"This is story about how MaDa came to your house for your wife!\"/>\n" +
                "\t<film name=\"MaDa is coming to your house!\" duration=\"98\" adultsOnly=\"true\" releaseDate=\"2020-01-01 1:23:45\" description=\"This is story about how MaDa came to your house for your wife!\"/>\n" +
                "\t<film name=\"MaDa is coming to your house!\" duration=\"98\" adultsOnly=\"true\" releaseDate=\"2020-01-01 1:23:45\" description=\"This is story about how MaDa came to your house for your wife!\"/>\n" +*/
                "</filmList>");
            }
            finally
            {
                foreach(XmlNode xmlDocumentFilmListItem in xmlDocument.SelectNodes("/filmList/film"))
                {
                    Film film = new Film();
                    foreach (XmlAttribute xmlDocumentFilmListItemAttribute in xmlDocumentFilmListItem.Attributes)
                    {
                        switch (xmlDocumentFilmListItemAttribute.LocalName)
                        {
                            case "id":
                                film.Id = int.Parse(xmlDocumentFilmListItemAttribute.Value);
                                break;
                            case "name":
                                film.Name = xmlDocumentFilmListItemAttribute.Value;
                                break;
                            case "duration":
                                if (int.TryParse(xmlDocumentFilmListItemAttribute.Value, out int parsedDuration))
                                {
                                    film.Duration = parsedDuration;
                                }
                                break;
                            case "adultsOnly":
                                film.AdultsOnly = bool.Parse(xmlDocumentFilmListItemAttribute.Value);
                                break;
                            case "releaseDate":
                                if (DateTime.TryParse(xmlDocumentFilmListItemAttribute.Value, out DateTime parsedReleaseDate))
                                {
                                    film.ReleaseDate = parsedReleaseDate;
                                }
                                break;
                            case "description":
                                film.Description = xmlDocumentFilmListItemAttribute.Value;
                                break;
                        }
                    }
                    this.filmList.Add(film);
                }
                this.Update();
            }
        }

        public void Save()
        {
            xmlDocument.Save("filmList.xml");
        }

        public void Update()
        {
            this.filmList.SortByReleaseDate(true);
            FilmListData.Items.Refresh();
        }

        private void AddFilmButton_Click(object sender, RoutedEventArgs e)
        {
            AddFilmName.Text = "";
            AddFilmDuration.Text = "0";
            AddFilmAdultsOnly.IsChecked = false;
            AddFilmReleaseDate.Text = "";
            AddFilmDescription.Text = "";
            AddFilmGrid.Visibility = Visibility.Visible;
        }

        private void AddFilmBackButton_Click(object sender, RoutedEventArgs e)
        {
            AddFilmGrid.Visibility = Visibility.Hidden;
        }

        private void EditFilmButton_Click(object sender, RoutedEventArgs e)
        {
            EditFilmName.Text = this.selectedFilm.Name;
            EditFilmDuration.Text = this.selectedFilm.Duration.ToString();
            EditFilmAdultsOnly.IsChecked = this.selectedFilm.AdultsOnly;
            EditFilmReleaseDate.Text = this.selectedFilm.ReleaseDate.ToString();
            EditFilmDescription.Text = this.selectedFilm.Description;
            EditFilmGrid.Visibility = Visibility.Visible;
        }

        private void EditFilmBackButton_Click(object sender, RoutedEventArgs e)
        {
            EditFilmGrid.Visibility = Visibility.Hidden;
        }

        private void DeleteFilmButton_Click(object sender, RoutedEventArgs e)
        {
            this.filmList.Remove(this.selectedFilm);
            
            this.selectedFilmXml.ParentNode.RemoveChild(this.selectedFilmXml);
            this.Save();

            this.Update();
        }

        private void FilmListData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FilmListData.SelectedIndex > -1)
            {
                this.selectedFilm = this.filmList.Get(FilmListData.SelectedIndex);
                foreach(XmlNode filmNode in xmlDocument.SelectNodes("/filmList/film"))
                {
                    foreach(XmlAttribute filmNodeAttribute in filmNode.Attributes)
                    {
                        if(filmNodeAttribute.LocalName == "id" && int.Parse(filmNodeAttribute.Value) == this.selectedFilm.Id)
                        {
                            this.selectedFilmXml = filmNode;
                        }
                    }
                }

                EditFilmButton.IsEnabled = true;
                DeleteFilmButton.IsEnabled = true;
            }
            else
            {
                this.selectedFilm = new Film();
                this.selectedFilmXml = null;

                EditFilmButton.IsEnabled = false;
                DeleteFilmButton.IsEnabled = false;
            }
        }

        private void AddFilmAddButton_Click(object sender, RoutedEventArgs e)
        {
            Film film = new Film();
            film.Name = AddFilmName.Text;
            if (int.TryParse(AddFilmDuration.Text, out int parsedDuration))
            {
                film.Duration = parsedDuration;
            }
            film.AdultsOnly = bool.Parse(AddFilmAdultsOnly.IsChecked.ToString());
            if (DateTime.TryParse(AddFilmReleaseDate.Text, out DateTime parsedReleaseDate))
            {
                film.ReleaseDate = parsedReleaseDate;
            }
            film.Description = AddFilmDescription.Text;
            this.filmList.Add(film);

            XmlElement filmXml = xmlDocument.CreateElement("film");
            filmXml.SetAttribute("id", this.filmList.Get(this.filmList.Count - 1).Id.ToString());
            filmXml.SetAttribute("name",AddFilmName.Text);
            filmXml.SetAttribute("duration", AddFilmDuration.Text);
            filmXml.SetAttribute("adultsOnly", AddFilmAdultsOnly.IsChecked.ToString());
            filmXml.SetAttribute("releaseDate", AddFilmReleaseDate.Text);
            filmXml.SetAttribute("description", AddFilmDescription.Text);
            xmlDocument.SelectSingleNode("filmList").AppendChild(filmXml);
            this.Save();

            this.Update();
            AddFilmGrid.Visibility = Visibility.Hidden;
        }

        private void EditFilmEditButton_Click(object sender, RoutedEventArgs e)
        {
            Film film = new Film();
            film.Id = this.selectedFilm.Id;
            film.Name = EditFilmName.Text;
            if (int.TryParse(EditFilmDuration.Text, out int parsedDuration))
            {
                film.Duration = parsedDuration;
            }
            film.AdultsOnly = bool.Parse(EditFilmAdultsOnly.IsChecked.ToString());
            if (DateTime.TryParse(EditFilmReleaseDate.Text, out DateTime parsedReleaseDate))
            {
                film.ReleaseDate = parsedReleaseDate;
            }
            film.Description = EditFilmDescription.Text;
            this.filmList.Set(FilmListData.SelectedIndex, film);

            int i = 0;
            foreach (XmlAttribute filmXmlAttribute in this.selectedFilmXml.Attributes)
            {
                switch (filmXmlAttribute.LocalName)
                {
                    case "name":
                        this.selectedFilmXml.Attributes[i].Value = film.Name;
                        break;
                    case "duration":
                        this.selectedFilmXml.Attributes[i].Value = film.Duration.ToString();
                        break;
                    case "adultsOnly":
                        this.selectedFilmXml.Attributes[i].Value = film.AdultsOnly.ToString();
                        break;
                    case "releaseDate":
                        this.selectedFilmXml.Attributes[i].Value = film.ReleaseDate.ToString();
                        break;
                    case "description":
                        this.selectedFilmXml.Attributes[i].Value = film.Description;
                        break;
                }
                i++;
            }
            this.Save();

            this.Update();
            EditFilmGrid.Visibility = Visibility.Hidden;
        }
    }
}
