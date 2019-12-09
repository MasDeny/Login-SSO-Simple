using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApps.Data.Context;

namespace WebApps.Data.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly WebAppDbContext _context;

        public BaseRepository(WebAppDbContext context)
        {
            _context = context;
        }
    }
}
