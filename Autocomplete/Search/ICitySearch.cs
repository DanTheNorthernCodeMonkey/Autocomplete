using System.Threading.Tasks;
using ThirdParty;

namespace Autocomplete.Search
{
	public interface ICitySearch : ICityFinder
	{
		Task Initialise();
	}
}