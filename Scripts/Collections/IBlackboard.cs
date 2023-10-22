using System.Collections.Generic;

namespace Collections {
	public interface IBlackboard : IReadOnlyBlackboard, ICollection<KeyValuePair<string, object>> {
		bool Remove<T>(string name);
		bool Remove(string name);
		bool SetValue<T>(string name, T value);
		bool SetValue(string name, object value);
	}
}