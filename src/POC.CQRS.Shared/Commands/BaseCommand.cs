using System;
using Flunt.Notifications;

namespace POC.CQRS.Shared.Commands
{
	public abstract class BaseCommand : Notifiable
	{
		public abstract bool IsValid();
	}
}
