using System;
using System.Data;

namespace POC.CQRS.Infra
{
	public interface IDatabaseContext : IDisposable
	{
		IDbConnection Connection { get; }
	}
}