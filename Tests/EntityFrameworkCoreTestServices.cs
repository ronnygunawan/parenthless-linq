using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Tests.Data;

namespace Tests {
	public class EntityFrameworkCoreTestServices : IDisposable {
		private readonly SqliteConnection _sqliteConnection;
		private readonly IServiceProvider _serviceProvider;

		public TestDbContext DbContext => _serviceProvider.GetRequiredService<TestDbContext>();

		public EntityFrameworkCoreTestServices() {
			_sqliteConnection = new SqliteConnection("DataSource=:memory:");
			_sqliteConnection.Open();
			ServiceCollection serviceCollection = new ServiceCollection();
			serviceCollection.AddDbContext<TestDbContext>(options => {
				options.UseSqlite(_sqliteConnection);
			}, ServiceLifetime.Transient);
			_serviceProvider = serviceCollection.BuildServiceProvider();

			using TestDbContext dbContext = _serviceProvider.GetRequiredService<TestDbContext>();
			dbContext.Database.Migrate();
		}

		public void Dispose() {
			_sqliteConnection.Dispose();
		}
	}
}
