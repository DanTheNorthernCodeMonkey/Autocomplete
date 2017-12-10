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
	public class AutoCompleteSpecTests
	{
		[Test]
		public async Task GivenBang_ResultsShouldMatchExpectations()
		{
			var mockDataReadModel = Substitute.For<IDataReadModel>();

			mockDataReadModel.GetData().Returns(new List<string>
			{
				"BANDUNG", "BANGUI", "BANGKOK", "BANGALORE"
			});

			var citySearch = new CitySearch(mockDataReadModel, new CityCharacterValidator(), new Trie());
			await citySearch.Initialise();

			var expectedResult = new CityResult
			{
				NextCities = new[] {"BANGUI", "BANGKOK", "BANGALORE"},
				NextLetters = new[] { "U", "K", "A"}
			};

			var results = citySearch.Search("BANG");

			results.ShouldBeEquivalentTo(expectedResult);
		}

		[Test]
		public async Task GivenLA_ResultsShouldMatchExpectations()
		{
			//Given
			var mockDataReadModel = Substitute.For<IDataReadModel>();

			mockDataReadModel.GetData().Returns(new List<string>
			{
				"LA PAZ", "LA PLATA", "LAGOS", "LEEDS"
			});

			var citySearch = new CitySearch(mockDataReadModel, new CityCharacterValidator(), new Trie());
			await citySearch.Initialise();

			var expectedResult = new CityResult
			{
				NextCities = new[] { "LA PAZ", "LA PLATA", "LAGOS" },
				NextLetters = new[] { " ", "G" }
			};

			//When
			var results = citySearch.Search("LA");

			//Then
			results.ShouldBeEquivalentTo(expectedResult);
		}

		[Test]
		public async Task GivenZE_ResultsShouldMatchExpectations()
		{
			//Given
			var mockDataReadModel = Substitute.For<IDataReadModel>();

			mockDataReadModel.GetData().Returns(new List<string>
			{
				"ZARIA", "ZHUGHAI", "ZIBO"
			});

			var citySearch = new CitySearch(mockDataReadModel, new CityCharacterValidator(), new Trie());
			await citySearch.Initialise();

			var expectedResult = new CityResult
			{
				NextCities = new string[0],
				NextLetters = new string[0]
			};

			//When
			var results = citySearch.Search("ZE");

			//Then
			results.ShouldBeEquivalentTo(expectedResult);
		}
	}
}
