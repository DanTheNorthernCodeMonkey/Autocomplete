using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autocomplete.DAL
{
	public interface IDataReadModel
	{
		Task<IEnumerable<string>> GetData();
	}
}