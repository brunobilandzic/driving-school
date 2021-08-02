using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        public UnitOfWork(DataContext context, IMapper mapper, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
        }

        public IUserRepository UserRepository => new UserRepository(_mapper, _userManager, _roleManager);

        public IProfessorRepository ProfessorRepository => new ProfessorRepository(_context, _mapper, _userManager, _roleManager);

        public IDrivingRepository DrivingRepository => new DrivingRepository(_context, _userManager, _mapper);

        public IStudentRepository StudentRepository => new StudentRepository(_context, _mapper, _userManager);

        public async Task<int> SaveAllChanges()
        {
            return await _context.SaveChangesAsync();
        }
    }
}