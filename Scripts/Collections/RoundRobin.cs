using System;
using System.Collections.Generic;

namespace Collections {
	public class RoundRobin<T> {
		private readonly List<T> _items;

		private int _count;

		public RoundRobin(int count = 0) {
			_items = new List<T>(count);
		}

		public RoundRobin(IEnumerable<T> items) {
			_items = new List<T>(items);
		}

		public T Get() {
			if( _count >= _items.Count ) {
				_items.Shuffle();
				_count = 0;
			}
			return _items[_count++];
		}
	}
}