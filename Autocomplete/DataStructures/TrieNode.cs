using System.Collections.Generic;
using System.Linq;

namespace Autocomplete.DataStructures
{
	internal sealed class TrieNode
	{
		private readonly char _data;
		public List<TrieNode> Children { get; set; }
		public bool IsEnd { get; set; }
		public TrieNode Parent { get; set; }


		public TrieNode(char data)
		{
			_data = data;
			Children = new List<TrieNode>();
			IsEnd = false;
		}


		public TrieNode GetChild(char character)
		{
			return Children?.FirstOrDefault(trieNode => char.ToUpperInvariant(trieNode._data) == char.ToUpperInvariant(character));
		}

		public List<string> GetWords()
		{
			var words  = new List<string>();

			if (IsEnd)
				words.Add(ConvertToString());

			if (Children == null)
				return words;

			foreach (var trieNode in Children)
			{
				if (trieNode != null)
					words.AddRange(trieNode.GetWords());
			}

			return words;
		}

		private string ConvertToString()
		{
			return Parent == null ? string.Empty : string.Concat(Parent.ConvertToString(), new string(new[] {_data}));
		}
	}
}