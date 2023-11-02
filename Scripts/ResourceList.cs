using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public class ResourceList : Node, IList<Resource>, IReadOnlyList<Resource>
{
    [Export]
    private List<Resource> _items;

	public Resource this[int index] {
		get => _items[index];
		set => _items[index] = value;
	}

	public int Count => _items.Count;

	bool ICollection<Resource>.IsReadOnly => false;

	void ICollection<Resource>.Add(Resource item) {
		_items.Add(item);
	}

	void ICollection<Resource>.Clear() {
		_items.Clear();
	}

	bool ICollection<Resource>.Contains(Resource item) {
		return _items.Contains(item);
	}

	void ICollection<Resource>.CopyTo(Resource[] array, int arrayIndex) {
		((ICollection<Resource>)_items).CopyTo(array, arrayIndex);
	}

	public IEnumerator<Resource> GetEnumerator() {
		return _items.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator() {
		return _items.GetEnumerator();
	}

	int IList<Resource>.IndexOf(Resource item) {
		return _items.IndexOf(item);
	}

	void IList<Resource>.Insert(int index, Resource item) {
		_items.Insert(index, item);
	}

	bool ICollection<Resource>.Remove(Resource item) {
		return _items.Remove(item);
	}

	void IList<Resource>.RemoveAt(int index) {
		_items.RemoveAt(index);
	}
}
