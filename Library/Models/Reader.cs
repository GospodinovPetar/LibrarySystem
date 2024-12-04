using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace LibrarySystem.Models
{
    public class Reader
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        [DisplayName("Membership creation date")]
        public DateTime MembershipDate { get; set; }
        [ValidateNever]
        public List<Loan> Loans { get; set; }
    }
}
