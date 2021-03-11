using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tests.Data {
	[Table("Countries")]
	public class Country {
		[Key]
		public int Id { get; set; }

		[ForeignKey(nameof(Continent))]
		public int ContinentId { get; set; }

		[Required, StringLength(50)]
		public string Name { get; set; }

		public virtual Continent Continent { get; set; }
	}
}
