using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCRUD.Infrastructure.Entities
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(35)")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(35)")]
        public string LastName { get; set; }

        [Required]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        [Column(TypeName = "varchar(60)")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        // 11 digit long phone nubmer only
        [Required]
        [Column(TypeName = "varchar(11)")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
    }
}
