namespace Intership.Core.Utils
{
	public static class StringUtils
	{
		public static int GetDecimalStringLength(int value)
		{
			int result = value < 0 ? 2 : 1;
			while ((value /= 10) != 0) result++;
			return result;
		}
	}
}