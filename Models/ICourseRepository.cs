namespace Architecture_API.Models
{
    public interface ICourseRepository
    {
        // Course
        Task<Course[]> GetAllCourseAsync();
        Task<Course> GetCourseByIdAsync(int id);
        Task AddCourseAsync(Course course);
        Task DeleteCourseAsync(int id);
        Task UpdateCourseAsync(Course course);

    }
}
