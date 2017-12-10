using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Autocomplete.DataStructures;
using Autocomplete.DAL;
using Autocomplete.Search;
using Autocomplete.Validators;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Autocomplete.PerformanceTest
{
	[TestFixture, Explicit]
	[Category("Performance - Variable Input")]
	public class PerformanceTest
	{
		private IEnumerable<string> _cities;
		private ICitySearch _citySearch;

		[SetUp]
		public async Task Init()
		{
			_cities = TestHelper.GenerateOverOneMillionRandomCitiesSet();
			var mockDataReadModel = Substitute.For<IDataReadModel>();

			mockDataReadModel.GetData().Returns(_cities);

			_citySearch = new CitySearch(mockDataReadModel, new CityCharacterValidator(), new Trie());

			await _citySearch.Initialise();
		}

		[Test]
		public void CitySearch_OverOneMillionDataItems_ReturnsDataInLessThan200Milliseconds()
		{
			var testWord = "B";
			var stopwatch = new Stopwatch();
			stopwatch.Start();

			var results = _citySearch.Search(testWord);

			stopwatch.Stop();
			var milliseconds = stopwatch.ElapsedMilliseconds;

			results.NextCities.Count.Should().BeGreaterThan(0);
			results.NextLetters.Count.Should().BeGreaterThan(0);

			milliseconds.Should().BeLessThan(200);
		}
	}
}