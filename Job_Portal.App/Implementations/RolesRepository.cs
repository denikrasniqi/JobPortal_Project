using Job_Portal.App.Interfaces;
using Job_Portal.Data.Context;
using Job_Portal.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job_Portal.App.Implementations
{
    public class RolesRepository : Repository<AspNetRole>, IRolesRepository
    {
        protected readonly Job_PortalContext _job_portalDbContext;

        public RolesRepository(Job_PortalContext job_portalDbContext) : base(job_portalDbContext)
        {
            _job_portalDbContext = job_portalDbContext;
        }

        public AspNetRole? GetByStringId(string id)
        {
            return _job_portalDbContext.AspNetRoles.FirstOrDefault(x => x.Id == id);
        }

        public AspNetRole? GetByUserId(string userId)
        {
            return _job_portalDbContext.AspNetUsers.Include(x => x.Roles).FirstOrDefault(x => x.Id == userId)?.Roles.FirstOrDefault();
        }
    }
}
