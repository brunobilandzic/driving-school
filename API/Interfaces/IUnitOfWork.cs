using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository {get;}

        IProfessorRepository ProfessorRepository {get;}

        IDrivingRepository DrivingRepository {get;}

        Task<int> SaveAllChanges();
    }
}