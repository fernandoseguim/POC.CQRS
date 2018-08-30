using System.Data;

namespace POC.CQRS.Infra
{
	public class POCCQRSDatabaseContext : IDatabaseContext
	{
		public POCCQRSDatabaseContext(IDbConnection connection)
		{
			this.Connection = connection;
			this.Connection.Open();
		}

		public IDbConnection Connection { get; }

		public void Dispose()
		{
			if (this.Connection.State != ConnectionState.Closed)
			{
				this.Connection.Close();
			}
		}
	}
}
