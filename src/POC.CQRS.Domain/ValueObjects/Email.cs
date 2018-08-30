using System;
using System.Text.RegularExpressions;

namespace POC.CQRS.Domain.ValueObjects
{
	public class Email
	{
		private const string EMAIL_PATTERN = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

		public Email(string address)
		{
			this.Address = address;
			
			if (Regex.IsMatch(this.Address ?? "", EMAIL_PATTERN) == false)
				throw new ArgumentException($"The email address is invalid");
		}

		public string Address { get; }

		public override string ToString()
			=> this.Address;
	}
}
