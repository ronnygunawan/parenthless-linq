using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tests.Data {
	[Table("Continents")]
	public class Continent {
		[Key]
		public int Id { get; set; }

		[Required, StringLength(30)]
		public string Name { get; set; }

		public virtual ICollection<Country> Countries { get; set; }
	}
}
