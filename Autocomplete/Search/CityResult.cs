using System.Collections.Generic;
using ThirdParty;

namespace Autocomplete.Search
{
	public class CityResult : ICityResult
	{
		public ICollection<string> NextLetters { get; set; }
		public ICollection<string> NextCities { get; set; }

		public CityResult()
		{
			NextCities = new List<string>();
			NextLetters = new List<string>();
		}
	}
}