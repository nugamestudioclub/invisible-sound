using System;

namespace Exceptions {
	public static class ArgumentNull {
		public static void ThrowIfNull(object argument, string paramName = null) {
			if( argument == null )
				throw new ArgumentNullException(paramName);
		}
	}
}