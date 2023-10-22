using System;

namespace Exceptions {
	public static class NotSupported {
		public static void ThrowIfReadOnly(bool isReadOnly) {
			if( isReadOnly )
				throw new NotSupportedException("The specified operation is not supported by this read-only collection.");
		}
	}
}