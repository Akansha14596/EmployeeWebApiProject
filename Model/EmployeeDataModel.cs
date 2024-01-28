using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeWebApi.Model
{
    public class EmployeeDataModel
    {
        [Key]
        public string EmpId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string JobTitle { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Gender { get; set; }

        [ForeignKey("Manager")]
        public string? ManagerId { get; set; }

        public EmployeeDataModel? Manager { get; set; }

    }
}
