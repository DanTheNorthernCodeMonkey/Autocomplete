using Autocomplete.DataStructures;
using Autocomplete.Search;
using Autocomplete.Validators;
using Autofac;

namespace Autocomplete
{
	public class AutocompleteModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<Trie>().As<ITrie>();
			builder.RegisterType<CitySearch>().As<ICitySearch>();
			builder.RegisterType<CityCharacterValidator>().As<ICharacterValidator>();
		}
	}
}