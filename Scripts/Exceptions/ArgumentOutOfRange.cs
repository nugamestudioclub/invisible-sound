using System;

namespace Exceptions {
	public static class ArgumentOutOfRange {
		public static void ThrowIfIndexOutOfRange(int value, int length, string paramName) {
			if( value < 0 || value >= length )
				throw new ArgumentOutOfRangeException(paramName, $"Index must be in the range [0,{length}).");
		}

		public static void ThrowIfIndexOutOfRange(int value, int length, string rangeName, string paramName) {
			if( value < 0 || value >= length )
				throw new ArgumentOutOfRangeException(paramName, value, $"{rangeName} index must be in the range [0,{length}).");
		}

		public static void ThrowIfLengthNegative(int value, string paramName) {
			if( value < 0 )
				throw new ArgumentOutOfRangeException(paramName, "Length cannot be negative.");
		}

		public static void ThrowIfLengthNotEqual(int value, int expectedValue, string paramName) {
			if( value != expectedValue )
				throw new ArgumentOutOfRangeException(paramName, value, $"Length must be {expectedValue}.");
		}

		public static void ThrowIfLengthNotEqual(int value, int expectedValue, string rangeName, string paramName) {
			if( value != expectedValue )
				throw new ArgumentOutOfRangeException(paramName, value, $"{rangeName} length must be {expectedValue}.");
		}
	}
}