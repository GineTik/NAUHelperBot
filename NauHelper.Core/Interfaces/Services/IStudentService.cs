using NauHelper.Core.Models;

namespace NauHelper.Core.Interfaces.Services
{
    public interface IStudentService
    {
        Task<StudentInfo> GetStudentInfoAsync(long userId);
        Task ApplyRegistrationAsync(long userId, int groupId);
    }
}
