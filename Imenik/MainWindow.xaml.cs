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
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Imenik
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	/// 
	public partial class MainWindow : Window
	{
		public ObservableCollection<Osoba> listaO = new ObservableCollection<Osoba>();

		public MainWindow()
		{
			InitializeComponent();
			if (File.Exists("osobe.dat"))
			{
				BinaryFormatter bf = new BinaryFormatter();
				using (FileStream fs = new FileStream("osobe.dat", FileMode.Open, FileAccess.Read))
				{
					listaO = bf.Deserialize(fs) as ObservableCollection<Osoba>;
				}
			}

			dg.ItemsSource = listaO;
		}

		private void Dodavanje(object sender, RoutedEventArgs e)
		{
			OsobeEditor oE = new OsobeEditor();
			oE.Owner = this;
			if (oE.ShowDialog() == true)
			{
				listaO.Add(oE.DataContext as Osoba);
			}
		}

		private void Brisanje(object sender, RoutedEventArgs e)
		{
			if (dg.SelectedItem != null)
			{
				List<Osoba> zaBrisanje = new List<Osoba>();
				foreach (Osoba selektovano in dg.SelectedItems)
				{ 
					zaBrisanje.Add(selektovano);
				}

				foreach (Osoba o in zaBrisanje)
				{
					listaO.Remove(o);
				}
			}
		}

		private void ZatvaramSe(object sender, System.ComponentModel.CancelEventArgs e)
		{
			BinaryFormatter bf = new BinaryFormatter();
			using (FileStream kaFajlu = new FileStream("osobe.dat", FileMode.Create, FileAccess.Write))
			{
				bf.Serialize(kaFajlu, listaO);
			}
			File.Encrypt("osobe.dat");
		}
	}
}
