using System;
using System.Linq;

namespace POC.CQRS.Tests
{
	public abstract class UnitTestBase
	{
		protected string MockString(int length = 10)
		{
			var random = new Random();
			const string CHARS = "0123456789";
			return new string(Enumerable.Repeat(CHARS, length)
				.Select(s => s[random.Next(s.Length)]).ToArray());
		}
	}
}
