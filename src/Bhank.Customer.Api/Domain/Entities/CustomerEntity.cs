
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bhank.Customer.Api.Domain.Entities
{
    [Table("Customer")]
    public class CustomerEntity
    {
        
        [Key]
        public Guid Id { get; set; }

        [StringLength(100)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(200)] 
        public string LastName { get; set; }

        [StringLength(100)]
        [Required]
        public string Email { get; set; }

        [StringLength(30)]
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }

        [StringLength(50)]
        public string Nationality { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid AddressId { get; set; }
        public AddressEntity Address { get; set; }
    }
}