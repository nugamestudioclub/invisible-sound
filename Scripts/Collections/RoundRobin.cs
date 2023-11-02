using System;
using System.Collections;
using System.Collections.Generic;

namespace Collections {
	public class RoundRobin<T> : IList<T>, IReadOnlyList<T> {
		private readonly List<T> _items;

		private int _count;

		public RoundRobin(int count = 0) {
			_items = new List<T>(count);
		}

		public RoundRobin(IEnumerable<T> items) {
			_items = new List<T>(items);
		}

		public T this[int index] {
			get => _items[index];
			set => _items[index] = value;
		}

		public int Count => _items.Count;

		bool ICollection<T>.IsReadOnly => false;

		void ICollection<T>.Add(T item) {
			_items.Add(item);
		}

		void ICollection<T>.Clear() {
			_items.Clear();
		}

		bool ICollection<T>.Contains(T item) {
			return _items.Contains(item);
		}

		void ICollection<T>.CopyTo(T[] array, int arrayIndex) {
			_items.CopyTo(array, arrayIndex);
		}

		public T Get() {
			if( _count >= _items.Count ) {
				_items.Shuffle();
				_count = 0;
			}
			return _items[_count++];
		}

		public IEnumerator<T> GetEnumerator() {
			return _items.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return _items.GetEnumerator();
		}

		int IList<T>.IndexOf(T item) {
			return _items.IndexOf(item);
		}

		void IList<T>.Insert(int index, T item) {
			_items.Insert(index, item);
		}

		bool ICollection<T>.Remove(T item) {
			return _items.Remove(item);
		}

		void IList<T>.RemoveAt(int index) {
			_items.RemoveAt(index);
		}
	}
}