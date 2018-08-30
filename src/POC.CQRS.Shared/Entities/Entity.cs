using System;
using Flunt.Notifications;

namespace POC.CQRS.Shared.Entities
{
	public abstract class Entity : Notifiable
	{
		protected Entity() => this.Id = Guid.NewGuid();

		public Guid Id { get; protected set; }
	}
}
