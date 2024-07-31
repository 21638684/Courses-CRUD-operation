using Microsoft.EntityFrameworkCore;
using System;

namespace Architecture_API.Models
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _appDbContext;

        public CourseRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Course[]> GetAllCourseAsync()
        {
            IQueryable<Course> query = _appDbContext.Courses;
            return await query.ToArrayAsync();
        }

        public async Task<Course> GetCourseByIdAsync(int id)
        {
            return await _appDbContext.Courses.FindAsync(id);
        }

        public async Task AddCourseAsync(Course course)
        {
            _appDbContext.Courses.Add(course);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task DeleteCourseAsync(int id)
        {
            var course = await _appDbContext.Courses.FindAsync(id);
            if (course != null)
            {
                _appDbContext.Courses.Remove(course);
                await _appDbContext.SaveChangesAsync();
            }
        }
        public async Task UpdateCourseAsync(Course course)
        {
            _appDbContext.Entry(course).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
        }
        
    }
}