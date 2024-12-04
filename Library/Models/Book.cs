using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace LibrarySystem.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        [DisplayName("Date of Publishing")]
        public DateTime PublishedDate { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        [ValidateNever]
        public List<Loan> Loans { get; set; }
    }
}
