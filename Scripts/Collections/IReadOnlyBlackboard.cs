using System.Collections.Generic;

namespace Collections
{
    public interface IReadOnlyBlackboard : IReadOnlyCollection<KeyValuePair<string, object>>
    {
        IReadOnlyCollection<string> Names { get; }
        IReadOnlyCollection<object> Values { get; }
        bool Contains<T>(string name);
        bool Contains(string name);
        T GetValue<T>(string name);
        object GetValue(string name);
        T GetValueOrDefault<T>(string name);

        T GetValueOrDefault<T>(string name, T defaultValue);

        object GetValueOrDefault(string name, object defaultValue);
        object GetValueOrNull(string name);
        bool TryGetValue<T>(string name, out T value);
        bool TryGetValue(string name, out object value);
    }
}