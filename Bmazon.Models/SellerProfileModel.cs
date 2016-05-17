using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bmazon.Models
{
    public class SellerProfileModel
    {
        [Key]
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Invalid character for Name")]
        [DisplayFormat(NullDisplayText = "Unknown")]
        [MinLength(2, ErrorMessage = "Name should be at least two characters")]
        [MaxLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Invalid character for Name")]
        [DisplayFormat(NullDisplayText = "Unknown")]
        [MinLength(2, ErrorMessage = "Name should be at least two characters")]
        [MaxLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DisplayFormat(NullDisplayText = "Unknown")]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        public string Company { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double Earning { get; set; }
    }
}
