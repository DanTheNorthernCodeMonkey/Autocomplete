using System;
using System.Collections.Generic;

namespace Autocomplete.PerformanceTest
{
	public static class TestHelper
	{
		public static string[] DefaultCitiesSet = {
			"Anchorage", "Cordova", "Fairbanks", "Haines", "Homer", "Juneau", "Ketchikan", "Kodiak", "Kotzebue", "Nome", "Palmer", "Seward", "Sitka",
			"Skagway","Valdez", "Arkadelphia", "Arkansas Post", "Batesville", "Benton", "Blytheville", "Camden", "Conway", "Crossett", "El Dorado",
			"Fayetteville","Forrest City", "Fort Smith", "Harrison", "Helena", "Hope", "Hot Springs", "Jacksonville", "Jonesboro", "Little Rock",
			"Magnolia","Morrilton","Newport", "North Little Rock", "Osceola", "Pine Bluff", "Rogers", "Searcy", "Stuttgart", "Van Buren",
			"West Memphis", "Newcastle-On-Tyne", "London", "New York", "Paris", "Prague", "Portsmouth", "Bangor", "Bath", "Belfast", "Birmingham",
			"Brighton", "Bristol", "Newcastle-On-Tyne", "Newport"
		};

		public static char[] AllowedChars = 
		{
			'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w',
			'x', 'y', 'z', '-', ' '
		};

		public static IEnumerable<string> GenerateOverOneMillionRandomCitiesSet()
		{
			var resultSet = new List<string>(DefaultCitiesSet);

			var rand = new Random();
			var loopsPerDefaultCity = 1000000 / DefaultCitiesSet.Length;

			var allowedCharactersLastIndex = AllowedChars.Length -1;

			foreach (var defaultCity in DefaultCitiesSet)
			{
				var defaultCityLastIndex = defaultCity.Length -1;
				var defaultCityCharArray = defaultCity.ToCharArray();

				for (var i = 0; i < loopsPerDefaultCity; i++)
				{
					var defaultCityCharToChangeIndex = rand.Next(0, defaultCityLastIndex);
					var characterToSwapIndex = rand.Next(0, allowedCharactersLastIndex);

					defaultCityCharArray[defaultCityCharToChangeIndex] = AllowedChars[characterToSwapIndex];

					resultSet.Add(new string(defaultCityCharArray));
				}
			}

			return resultSet;
		}
	}
}