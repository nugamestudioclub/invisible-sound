using Collections;
using System.Collections.Generic;

public interface IResourceService {
	IBlackboard LoadScene(string resourceId);
    IReadOnlyBlackboard Assets { get; }

    void Add<T>(IEnumerable<KeyValuePair<string, T>> data);

    
}