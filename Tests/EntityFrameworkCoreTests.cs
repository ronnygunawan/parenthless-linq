using FluentAssertions;
using System.Linq;
using System.Threading.Tasks;
using Tests.Data;
using Xunit;
using Parenthless.EntityFrameworkCore;
using static Parenthless.EntityFrameworkCore.Linq;
using System.Collections.Generic;
using System.Threading;

namespace Tests {
	public class EntityFrameworkCoreTests : IClassFixture<EntityFrameworkCoreTestServices> {
		private readonly EntityFrameworkCoreTestServices _services;

		public EntityFrameworkCoreTests(EntityFrameworkCoreTestServices services) {
			_services = services;
		}

		[Fact]
		public async Task CanEnumerateToListAsync() {
			using TestDbContext dbContext = _services.DbContext;
			List<string> continents = await from continent in dbContext.Continents
											group continent.Name by ToListAsync() into list
											select list;
			continents.GetType().Should().Be<List<string>>();
			continents.Should().ContainInOrder("Asia", "Africa", "North America", "South America", "Antarctica", "Europe", "Australia", "Oceania");
		}

		[Fact]
		public async Task CanEnumerateToArrayAsync() {
			CancellationToken cancellationToken = CancellationToken.None;
			using TestDbContext dbContext = _services.DbContext;
			string[] continents = await from continent in dbContext.Continents
										group continent.Name by ToArrayAsync(cancellationToken) into list
										select list;
			continents.GetType().Should().Be<string[]>();
			continents.Should().ContainInOrder("Asia", "Africa", "North America", "South America", "Antarctica", "Europe", "Australia", "Oceania");
		}

		[Fact]
		public async Task CanEnumerateToDictionaryAsync() {
			using TestDbContext dbContext = _services.DbContext;
			Dictionary<int, string> continents = await from continent in dbContext.Continents
													   group continent.Name by ToDictionaryAsync(continent.Id, CancellationToken.None) into dict
													   select dict;
			continents.GetType().Should().Be<Dictionary<int, string>>();
			continents.Should().ContainValues("Asia", "Africa", "North America", "South America", "Antarctica", "Europe", "Australia", "Oceania");
		}
	}
}
