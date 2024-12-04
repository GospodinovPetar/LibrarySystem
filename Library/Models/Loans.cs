using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace LibrarySystem.Models
{
    public class Loan
    {
        public int Id { get; set; }
        [DisplayName("Date of loan")]
        public DateTime LoanDate { get; set; }
        [DisplayName("Date of return")]
        public DateTime? ReturnDate { get; set; }
        [DisplayName("Book Name")]
        public int BookId { get; set; }
        [ValidateNever]
        public Book Book { get; set; }
        [DisplayName("Reader Name")]
        public int ReaderId { get; set; }
        [ValidateNever]
        public Reader Reader { get; set; }
    }
}
