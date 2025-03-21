using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Company.Session03.PL.Dtos
{
    public class CreateEmployeeDtos
    {
        [Required(ErrorMessage ="Name is Required")]
        public string Name { get; set; }

        [Range(20,60, ErrorMessage = "Age Must be btween 20 to 60")]
        public int? Age { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Email is not Valid")]
        public string Email { get; set; }
        public string Address { get; set; }

        [Phone]
        public string Phone { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        [DisplayName("Hiring Date")]
        public DateTime HiringDate { get; set; }

        [DisplayName("Date of Create")]

        public DateTime CreateAt { get; set; }

        public int? DepartmentId { get; set; }

        public string? ImgeName { get; set; }

        public IFormFile Image { get; set; }

    }
}
