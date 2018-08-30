using System.Collections.Generic;
using System.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Sqlite;

namespace POC.CQRS.Tests.Infra
{
	internal class InMemoryDatabase
	{
		private readonly OrmLiteConnectionFactory dbFactory =
			new OrmLiteConnectionFactory(":memory:", SqliteOrmLiteDialectProvider.Instance);

		public IDbConnection OpenConnection() => this.dbFactory.OpenDbConnection();

		public void CreateTable<T>()
		{
			using (var db = this.OpenConnection())
			{
				Scripts.CreateTable.TryGetValue(typeof(T), out var createTable);
				Scripts.InsertData.TryGetValue(typeof(T), out var insertData);
				db.ExecuteSql(createTable);
				db.ExecuteSql(insertData);
			}
		}
	}
}