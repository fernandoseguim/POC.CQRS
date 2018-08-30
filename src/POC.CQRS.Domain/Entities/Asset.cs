using System;
using POC.CQRS.Shared;
using POC.CQRS.Shared.Entities;
using Flunt.Validations;

namespace POC.CQRS.Domain.Entities
{
	public sealed class Asset : Entity
	{
		public const int MINIMUM_NAME_SIZE = 10;
		public const int MAXIMUM_NAME_SIZE = 50;
		public const int MAXIMUM_DESCRIPTION_SIZE = 50;

		public Asset(string name, Brand brand)
		{
			this.Name = name;

			if (string.IsNullOrWhiteSpace(this.Name)) 
				throw new ArgumentNullException($"The asset name can not be null or whitespace");
			this.Brand = brand ?? throw new ArgumentNullException($"The asset brand can not be null");

			this.GenerateassetRegisterNumber();
		}

		public string Name { get; }
		public Brand Brand { get; }
		public string Description { get; private set; }
		public string Registry { get; private set; }

		public void AddDescription(string description)
		{
			if(string.IsNullOrWhiteSpace(description) || description.Length > MAXIMUM_DESCRIPTION_SIZE)
			{
				this.AddNotification(nameof(this.Description),
					$"The asset description should contain a maximum of {MAXIMUM_NAME_SIZE} characters");
				return;
			}

			this.Description = description;
		}
		
		private void GenerateassetRegisterNumber() 
			=> this.Registry = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8);
	}
}
