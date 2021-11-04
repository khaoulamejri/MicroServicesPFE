using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Data.Infrastructure;
using UserApp.Domain.Entities;

namespace UserApp.Data.Repositories
{
   public class UserProfileRepository : RepositoryBase<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(IDatabaseFactory dbFactory) : base(dbFactory) { }
    }
    public interface IUserProfileRepository : IRepository<UserProfile> { }
}
