using System;
using Flunt.Validations;

namespace POC.CQRS.Shared
{
	public static class ContractExtensions
	{
		/// <summary>
		/// This method is a workaround beacuse exists a problem in oginal method HasMinLen from Flunt package in version 1.0.2
		/// see more in https://github.com/andrebaltieri/flunt/issues/19
		/// </summary>
		/// <returns></returns>
		public static Contract HasMinLength(this Contract contract, string val, int min, string property, string message)
		{
			if (string.IsNullOrEmpty(val) || val.Length < min)
			{
				contract.AddNotification(property, message);
			}

			return contract;
		}

		/// <summary>
		/// This method is a workaround beacuse exists a problem in oginal method HasMinLen from Flunt package in version 1.0.2
		/// see more in https://github.com/andrebaltieri/flunt/issues/19
		/// </summary>
		/// <returns></returns>
		public static Contract HasMaxLength(this Contract contract, string val, int max, string property, string message)
		{
			if (string.IsNullOrEmpty(val) || val.Length > max)
			{
				contract.AddNotification(property, message);
			}

			return contract;
		}

		public static Contract IsValidGuid(this Contract contract, string val, string property, string message)
		{
			if(!Guid.TryParse(val, out _))
			{
				contract.AddNotification(property, message);
			}

			return contract;
		}
	}
}
