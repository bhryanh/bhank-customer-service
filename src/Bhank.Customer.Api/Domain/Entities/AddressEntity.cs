
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bhank.Customer.Api.Domain.Entities
{
    [Table("Address")]
    public class AddressEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Street { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(50)]
        public string State { get; set; }

        [Required]
        [StringLength(50)]
        public string PostalCode { get; set; }

        [Required]
        [StringLength(50)]
        public string Country { get; set; }

        public CustomerEntity Customer { get; set; }
    }
}