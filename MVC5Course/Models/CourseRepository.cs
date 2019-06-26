using System;
using System.Linq;
using System.Collections.Generic;

namespace MVC5Course.Models
{
    public class CourseRepository : EFRepository<Course>, ICourseRepository
    {
        //public Course Find(int id)
        //{
        //    return this.All().Where(p => p.CourseID == id).FirstOrDefault();
        //}

        public override IQueryable<Course> All()
        {
            return base.All().Where(p => !p.Title.Contains("Git")).OrderBy(p => p.CourseID);
        }
    }

    public interface ICourseRepository : IRepository<Course>
    {

    }
}