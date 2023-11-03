using Collections;
using System.Collections.Generic;

public interface IResourceService {
	IReadOnlyBlackboard Assets { get; }

	void AddRange<T>(IEnumerable<KeyValuePair<string, T>> data);
	IBlackboard LoadScene(string resourceId);
	IBlackboard LoadResource(string path);
}