using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autocomplete.DataStructures;
using Autocomplete.DAL;
using Autocomplete.Validators;
using ThirdParty;

namespace Autocomplete.Search
{
	public sealed class CitySearch : ICitySearch
	{
		private readonly ICharacterValidator _characterValidator;
		private readonly IDataReadModel _dataReadModel;
		private readonly ITrie _structuredData;

		public CitySearch(IDataReadModel dataReadModel, ICharacterValidator characterValidator, ITrie trie)
		{
			_structuredData = trie;
			_dataReadModel = dataReadModel;
			_characterValidator = characterValidator;
		}

		public async Task Initialise()
		{
			var unstructuredData = await _dataReadModel.GetData();

			foreach (var dataItem in unstructuredData)
			{
				if (!_characterValidator.IsValid(dataItem))
					continue; 

				_structuredData.Insert(dataItem);
			}
		}

		public ICityResult Search(string searchString)
		{
			if (string.IsNullOrEmpty(searchString))
				return new CityResult();

			var result = _structuredData.Autocomplete(searchString).ToArray();

			return new CityResult
			{
				NextCities = result.ToArray(),
				NextLetters = GetNextLetters(searchString, result).ToArray()
			};
		}

		private static IEnumerable<string> GetNextLetters(string searchString, IEnumerable<string> matchingWords)
		{
			if (matchingWords == null || !matchingWords.Any())
				return new string[0];

			var nextLetterIndex = searchString.Length;
			var nextLetters = new List<string>();
			
			foreach (var word in matchingWords)
			{
				if (word.Length -1 < nextLetterIndex)
					continue;

				nextLetters.Add(word.ToCharArray()[nextLetterIndex].ToString());
			}

			return nextLetters.Distinct();
		}
	}
}