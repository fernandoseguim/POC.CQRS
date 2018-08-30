using System;
using System.Text.RegularExpressions;
using System.Xml.Schema;

namespace POC.CQRS.Domain.ValueObjects
{
	public class Name
	{
		public const string FIRST_NAME_PATTERN = @"^\w{3,}$";
		public const string LAST_NAME_PATTERN = @"^\w{2,}$";
		public const int MINIMUM_FIRST_NAME_SIZE = 3;
		public const int MINIMUM_LAST_NAME_SIZE = 2;

		public Name(string firstName, string lastName)
		{
			this.FirstName = firstName;
			this.LastName = lastName;

			this.Validate();
		}

		public string FirstName { get; }
		public string LastName { get; }

		public override string ToString() 
			=> $"{this.FirstName} {this.LastName}";

		private void Validate()
		{
			if (Regex.IsMatch(this.FirstName ?? "", FIRST_NAME_PATTERN) == false)
				throw new ArgumentException($"The firstname does not be lower than {MINIMUM_FIRST_NAME_SIZE} characters");

			if (Regex.IsMatch(this.LastName ?? "", LAST_NAME_PATTERN) == false)
				throw new ArgumentException($"The lastname does not be lower than {MINIMUM_LAST_NAME_SIZE} characters");
		}
	}
}
