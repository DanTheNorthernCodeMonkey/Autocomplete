using System.Collections.Generic;
using System.Threading.Tasks;
using Autocomplete.DataStructures;
using Autocomplete.DAL;
using Autocomplete.Search;
using Autocomplete.Validators;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Autocomplete.Test
{
	[TestFixture]
	public class ValidationTest
	{
		[Test]
		public async Task InvalidCharactersInSet_OnlyCitiesWithValidCharactersReturned()
		{
			//Given
			var mockDataReadModel = Substitute.For<IDataReadModel>();

			mockDataReadModel.GetData().Returns(new List<string>
			{
				"Valid", "In%Valid", "In3Valid", "Val*id"
			});

			var citySearch = new CitySearch(mockDataReadModel, new CityCharacterValidator(), new Trie());
			await citySearch.Initialise();

			var expectedResult = new CityResult
			{
				NextCities = new[] { "Valid" },
				NextLetters = new[] { "a" }
			};

			//When
			var results = citySearch.Search("V");

			//Then
			results.ShouldBeEquivalentTo(expectedResult);
		}

		[Test]
		public async Task InvalidCasing_MatchesAreNotCaseSensitive_ReturnsMatches()
		{
			// Given
			var mockDataReadModel = Substitute.For<IDataReadModel>();

			mockDataReadModel.GetData().Returns(new List<string>
			{
				"London", "Londonderry"
			});

			var citySearch = new CitySearch(mockDataReadModel, new CityCharacterValidator(), new Trie());
			await citySearch.Initialise();

			var expectedResult = new CityResult
			{
				NextCities = new[] { "London", "Londonderry" },
				NextLetters = new[] { "d" }
			};

			// When
			var results = citySearch.Search("lon");

			// Then
			results.ShouldBeEquivalentTo(expectedResult);
		}
	}
}