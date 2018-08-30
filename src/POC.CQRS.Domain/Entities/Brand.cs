using POC.CQRS.Shared;
using System;
using POC.CQRS.Shared.Entities;

namespace POC.CQRS.Domain.Entities
{
	public sealed class Brand : Entity
	{
		public const int MINIMUM_NAME_SIZE = 10;
		public const int MAXIMUM_NAME_SIZE = 20;

		public Brand(Guid id, string name)
		{
			this.Name = name;
			if (id != default(Guid)) { this.Id = id; }

			if (string.IsNullOrWhiteSpace(this.Name))
			{
				throw new ArgumentNullException($"The brand name can not be null or whitespace");
			}
		}

		public Brand(string name) : this(default(Guid), name) { }

		public string Name { get; }
	}
}
