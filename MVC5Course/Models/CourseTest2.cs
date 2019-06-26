using MVC5Course.DataTypeAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models
{
    [MetadataType(typeof(CourseTest2))]
    public partial class CourseMetaData
    {
        ////模型驗證
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (this.Credits <= 3 && this.Title.Length > 10)
        //    {
        //        yield return new ValidationResult("學分數小於等於3時，課程名稱不可超過10", new string[] { "Credits", "Title" });
        //    }
        //}
    }
    public partial class CourseTest2
    {
        [Required]
        public int CourseID { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        [Range(1, 5, ErrorMessage = "學分數不可超過5學分")]
        public int Credits { get; set; }
        [Required]
        public int DepartmentID { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        public string Location { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<Enrollment> Enrollment { get; set; }
        public virtual ICollection<Person> Person { get; set; }
    }
}