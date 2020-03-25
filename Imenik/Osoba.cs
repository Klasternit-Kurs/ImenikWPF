using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imenik
{
	public class Osoba
	{
		public string Ime { get; set; }
		public string Prezime { get; set; }
		public string Telefon { get; set; }

		public override string ToString()
		{
			return $"{Ime} {Prezime}";
		}
	}
}
