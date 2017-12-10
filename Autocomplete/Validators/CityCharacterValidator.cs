using System.Text.RegularExpressions;

namespace Autocomplete.Validators
{
	public class CityCharacterValidator : ICharacterValidator
	{
		private readonly Regex _regex = new Regex("^[a-zA-Z\\-\\s]+$");

		public bool IsValid(string cityName)
		{
			return _regex.IsMatch(cityName);
		}
	}
}