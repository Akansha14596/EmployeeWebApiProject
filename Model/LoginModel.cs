using System.ComponentModel.DataAnnotations;

namespace EmployeeWebApi.Model
{
    public class LoginModel
    {
        [Required]
        public string EmpId { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
