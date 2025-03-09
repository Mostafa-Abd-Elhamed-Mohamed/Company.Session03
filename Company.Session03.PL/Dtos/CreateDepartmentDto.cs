using System.ComponentModel.DataAnnotations;

namespace Company.Session03.PL.Dtos
{
    public class CreateDepartmentDto
    {
        [Required(ErrorMessage ="The Code is Required")]
        public string Code { get; set; }

        [Required(ErrorMessage = "The Name is Required")]

        public string Name { get; set; }

        [Required(ErrorMessage = "The CreateAt is Required")]

        public DateTime CreateAt { get; set; }
    }
}
