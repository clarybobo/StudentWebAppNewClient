using StudentWebAppNewClient.Data.Models;

namespace StudentWebAppNewClient.Data.Interfaces
{
    public interface IStudentService
    {
        public Task<List<Student>> GetStudentsAsync();
    }
}
