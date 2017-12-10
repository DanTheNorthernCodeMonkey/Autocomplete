using System.Collections.Generic;

namespace ThirdParty
{
	public interface ICityResult
	{
		ICollection<string> NextLetters { get; set; }
		ICollection<string> NextCities { get; set; }
	}
}