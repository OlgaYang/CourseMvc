using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.ViewModels
{
    public class DepartmentCreationVM
    {
        public DepartmentCreationVM()
        {

        }

        [Required]
        public int DepartmentId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Budget { get; set; }

        public System.DateTime StartDate { get; set; }
    }

    public class DepartmentBatchUpdateVM
    {
        [Required]
        public int DepartmentId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Budget { get; set; }
    }
}