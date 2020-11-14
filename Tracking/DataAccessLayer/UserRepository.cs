using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tracking.DataAccessLayer
{
    class UserRepository : IRepository<User>
    {
        private readonly DataContext context;

        public UserRepository(DataContext context)
        {
            this.context = context;
        }

        public IQueryable<User> Read()
        {
            return context.User;
        }

        public void Add(User user)
        {
            context.User.Add(user);
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return context.User.Include(x => x.Tracking).Where(predicate).ToList();
        }
    }
}
