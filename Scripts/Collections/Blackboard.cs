using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Collections {
	public class Blackboard : IBlackboard {
		private readonly Dictionary<string, object> variables;

		public int Count => variables.Count;

		public IReadOnlyCollection<string> Names => variables.Keys;

		public IReadOnlyCollection<object> Values => variables.Values;

		bool ICollection<KeyValuePair<string, object>>.IsReadOnly { get; }

		public Blackboard() {
			variables = new Dictionary<string, object>();
		}

		public Blackboard(IList<string> names, IList<object> values) {
			Exceptions.ArgumentNull.ThrowIfNull(names, nameof(names));
			Exceptions.ArgumentNull.ThrowIfNull(values, nameof(values));
			Exceptions.Argument.ThrowIfCountNotEqual(names, values, nameof(names), nameof(values));
			int count = names.Count;
			variables = new Dictionary<string, object>(count);
			for( int i = 0; i < count; ++i ) {
				string name = names[i];
				object variable = values[i];
				ThrowIfNullName(name, nameof(names));
				ThrowIfDuplicateName(name, variables.Keys, nameof(names));
				ThrowIfNullValue(variable, nameof(values));
				variables.Add(name, variable);
			}
		}

		public Blackboard(IEnumerable<KeyValuePair<string, object>> pairs) {
			Exceptions.ArgumentNull.ThrowIfNull(pairs, nameof(pairs));
			variables = new Dictionary<string, object>();
			foreach( var pair in pairs ) {
				ThrowIfNullName(pair.Key, nameof(pairs));
				ThrowIfDuplicateName(pair.Key, variables.Keys, nameof(pairs));
				ThrowIfNullValue(pair.Value, nameof(pairs));
				variables.Add(pair.Key, pair.Value);
			}
		}

		void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item) {
			string name = item.Key;
			object value = item.Value;
			ThrowIfNullName(name, nameof(item));
			ThrowIfNullValue(value, nameof(item));
			ThrowIfIncompatibleType(variables, name, value);
			variables[name] = value;
		}

		public void Clear() {
			variables.Clear();
		}

		public bool Contains<T>(string name) {
			Exceptions.ArgumentNull.ThrowIfNull(name, nameof(name));
			return variables.TryGetValue(name, out var variable)
				&& variable is T;
		}

		public bool Contains(string name) {
			Exceptions.ArgumentNull.ThrowIfNull(name, nameof(name));
			return variables.ContainsKey(name);
		}

		bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item) {
			return (variables as ICollection<KeyValuePair<string, object>>).Contains(item);
		}

		void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex) {
			(variables as ICollection<KeyValuePair<string, object>>).CopyTo(array, arrayIndex);
		}

		public IEnumerator<KeyValuePair<string, object>> GetEnumerator() {
			return variables.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return variables.GetEnumerator();
		}

		public T GetValue<T>(string name) {
			Exceptions.ArgumentNull.ThrowIfNull(name, nameof(name));
			ThrowIfMissingValue(name, variables.Keys);
			var value = variables[name];
			Exceptions.InvalidOperation.ThrowIfNotOfType<T>(value);
			return (T)value;
		}

		public object GetValue(string name) {
			Exceptions.ArgumentNull.ThrowIfNull(name, nameof(name));
			ThrowIfMissingValue(name, variables.Keys);
			return variables[name];
		}

		public T GetValueOrDefault<T>(string name) {
			Exceptions.ArgumentNull.ThrowIfNull(name, nameof(name));
			return TryGetValue<T>(name, out var value)
				? value
				: default;
		}

		public T GetValueOrDefault<T>(string name, T defaultValue) {
			Exceptions.ArgumentNull.ThrowIfNull(name, nameof(name));
			return TryGetValue<T>(name, out var value)
				? value
				: defaultValue;
		}

        public object GetValueOrDefault(string name, object defaultValue) {
			Exceptions.ArgumentNull.ThrowIfNull(name, nameof(name));
			Exceptions.ArgumentNull.ThrowIfNull(defaultValue, nameof(defaultValue));
			return variables.TryGetValue(name, out var value)
				? value
				: defaultValue;
		}

		public object GetValueOrNull(string name) {
			Exceptions.ArgumentNull.ThrowIfNull(name, nameof(name));
			return variables.TryGetValue(name, out var value)
				? value
				: default;
		}

		public bool Remove<T>(string name) {
			Exceptions.ArgumentNull.ThrowIfNull(name, nameof(name));
			return variables.TryGetValue(name, out var value)
				&& value is T
				&& variables.Remove(name);
		}

		public bool Remove(string name) {
			Exceptions.ArgumentNull.ThrowIfNull(name, nameof(name));
			return variables.Remove(name);
		}

		bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item) {
			return (variables as ICollection<KeyValuePair<string, object>>).Remove(item);
		}

		public bool SetValue<T>(string name, T value) {
			Exceptions.ArgumentNull.ThrowIfNull(name, nameof(name));
			Exceptions.ArgumentNull.ThrowIfNull(value, nameof(value));
			if( variables.TryGetValue(name, out var currentValue)
				&& !(currentValue is T) ) {
				return false;
			}
			else {
				variables[name] = value;
				return true;
			}
		}

		public bool SetValue(string name, object value) {
			Exceptions.ArgumentNull.ThrowIfNull(name, nameof(name));
			Exceptions.ArgumentNull.ThrowIfNull(value, nameof(value));
			if( variables.TryGetValue(name, out var currentVariable)
				&& !currentVariable.GetType().IsAssignableFrom(value.GetType()) ) {
				return false;
			}
			else {
				variables[name] = value;
				return true;
			}
		}

		public bool TryGetValue<T>(string name, out T value) {
			Exceptions.ArgumentNull.ThrowIfNull(name, nameof(name));
			if( variables.TryGetValue(name, out var currentValue) && currentValue is T tValue ) {
				value = tValue;
				return true;
			}
			else {
				value = default;
				return false;
			}
		}

		public bool TryGetValue(string name, out object value) {
			Exceptions.ArgumentNull.ThrowIfNull(name, nameof(name));
			return variables.TryGetValue(name, out value);
		}


		private static void ThrowIfDuplicateName(string name, IReadOnlyCollection<string> names, string parameterName) {
			if( names.Contains(name) )
				throw new ArgumentException($"Duplicate variable names are not allowed. (Parameter '{parameterName}')");
		}

		private static void ThrowIfMissingValue(string name, IReadOnlyCollection<string> names) {
			if( !names.Contains(name) )
				throw new InvalidOperationException($"Specified variable was not found. (Name '{name}'");
		}

		private static void ThrowIfNullName(string name, string parameterName) {
			if( name == null )
				throw new ArgumentException($"Variable names cannot be null. (Parameter '{parameterName}')");
		}

		private static void ThrowIfNullValue(object value, string parameterName) {
			if( value == null )
				throw new ArgumentException($"Variable values cannot be null. (Parameter '{parameterName}')");
		}

		private static void ThrowIfIncompatibleType(IReadOnlyDictionary<string, object> variables, string name, object value) {
			if( variables.TryGetValue(name, out var currentValue)
				&& !currentValue.GetType().IsAssignableFrom(value.GetType()) )
				throw new InvalidOperationException($"Specified variable contains an incompatible type. (Name '{name}')");
		}
	}
}