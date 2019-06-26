namespace MVC5Course.Models
{
    using MVC5Course.DataTypeAttributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(CourseMetaData))]
    public partial class Course : IValidatableObject, IEditCourse
    {
        //模型驗證
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new ArgumentException("驗證失敗");
            if (this.Credits <= 3 && this.Title.Length > 10)
            {
                yield return new ValidationResult("學分數小於等於3時，課程名稱不可超過10", new string[] { "Credits", "Title" });
            }
        }
    }

    internal interface IEditCourse
    {
        int CourseID { get; set; }
        string Title { get; set; }
        int Credits { get; set; }
    }

    public partial class CourseMetaData
    {
        [Required]
        public int CourseID { get; set; }

        [StringLength(15, ErrorMessage = "欄位長度不得大於 15 個字元")]
        [NoAllowSpecialWords]
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
