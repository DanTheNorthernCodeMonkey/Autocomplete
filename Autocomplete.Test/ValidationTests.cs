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
	public class ValidationTests
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

		[Test]
		public async Task NextLetter_SpaceReturned_AsValidNextLetter()
		{
			// Given
			var mockDataReadModel = Substitute.For<IDataReadModel>();

			mockDataReadModel.GetData().Returns(new List<string>
			{
				"Los Angeles"
			});

			var citySearch = new CitySearch(mockDataReadModel, new CityCharacterValidator(), new Trie());
			await citySearch.Initialise();

			var expectedResult = new CityResult
			{
				NextCities = new[] { "Los Angeles" },
				NextLetters = new[] { " " }
			};

			// When
			var results = citySearch.Search("Los");

			// Then
			results.ShouldBeEquivalentTo(expectedResult);
		}

		[Test]
		public async Task NextLetter_HyphenReturned_AsValidNextLetter()
		{
			// Given
			var mockDataReadModel = Substitute.For<IDataReadModel>();

			mockDataReadModel.GetData().Returns(new List<string>
			{
				"Stockton-on-Tees"
			});

			var citySearch = new CitySearch(mockDataReadModel, new CityCharacterValidator(), new Trie());
			await citySearch.Initialise();

			var expectedResult = new CityResult
			{
				NextCities = new[] { "Stockton-on-Tees" },
				NextLetters = new[] { "-" }
			};

			// When
			var results = citySearch.Search("Stockton");

			// Then 
			results.ShouldBeEquivalentTo(expectedResult);
		}
	}
}