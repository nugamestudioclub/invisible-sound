using Collections;
using System.Collections.Generic;

public interface IResourceService {
	IBlackboard LoadScene(string resourceId);
    IBlackboard LoadResource(string path);
    IReadOnlyBlackboard Assets { get; }

    void AddRange<T>(IEnumerable<KeyValuePair<string, T>> data);

    
}