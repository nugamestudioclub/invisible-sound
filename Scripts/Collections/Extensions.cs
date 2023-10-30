using System;
using System.Collections.Generic;

public static class Extensions {
	private static readonly Random _random = new Random();

	public static void Shuffle<T>(this IList<T> items, Random random = null) {
		if( random == null )
			random = _random;
		for( int i = items.Count - 1; i  > 0; --i ) {
			int j = random.Next(0, i + 1);
			(items[i], items[j]) = (items[j], items[i]);
		}
	}
}
