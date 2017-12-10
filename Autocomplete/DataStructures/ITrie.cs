using System.Collections.Generic;

namespace Autocomplete.DataStructures
{
	public interface ITrie
	{
		void Insert(string word);
		IEnumerable<string> Autocomplete(string searchString);
	}
}