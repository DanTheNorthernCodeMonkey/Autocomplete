using System.Collections.Generic;

namespace Autocomplete.DataStructures
{
	public sealed class Trie : ITrie
	{
		private readonly TrieNode _root;

		public Trie()
		{
			_root = new TrieNode(' ');
		}

		public void Insert(string word)
		{
			if (Search(word))
				return;

			var current = _root;
			var charArray = word.ToCharArray();
			foreach (var character in charArray)
			{
				var pre = current;
				var child = current.GetChild(character);
				if (child != null)
				{
					current = child;
					child.Parent = pre;
				}
				else
				{
					current.Children.Add(new TrieNode(character));
					current = current.GetChild(character);
					current.Parent = pre;
				}
			}
			current.IsEnd = true;
		}

		public IEnumerable<string> Autocomplete(string searchString)
		{
			var lastNode = _root;

			foreach (var character in searchString)
			{
				lastNode = lastNode.GetChild(character);
				if (lastNode == null)
					return new List<string>();
			}

			return lastNode.GetWords();
		}

		private bool Search(string word)
		{
			var current = _root;
			var charArray = word.ToCharArray();

			foreach (var character in charArray)
			{
				if (current.GetChild(character) == null)
					return false;

				current = current.GetChild(character);
			}

			return current.IsEnd;
		}
	}
}
