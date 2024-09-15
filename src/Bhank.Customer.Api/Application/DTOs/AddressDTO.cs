using System.ComponentModel.DataAnnotations;

namespace Bhank.Customer.Api.Application.DTOs
{
    public class AddressDTO
    {
        [StringLength(100)]
        public string Street { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string State { get; set; }

        [StringLength(50)]
        public string PostalCode { get; set; }

        [StringLength(50)]
        public string Country { get; set; }
    }
}