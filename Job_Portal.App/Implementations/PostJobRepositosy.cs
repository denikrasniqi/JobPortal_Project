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
    public class PostJobRepositosy : Repository<PostJob>, IPostJobRepository
    {
        protected readonly Job_PortalContext _job_portalDbContext;
        public PostJobRepositosy(Job_PortalContext job_portalDbContext) : base(job_portalDbContext)
        {
            _job_portalDbContext = job_portalDbContext;
        }
        public List<PostJob> GetAllJobTypes()
        {
            return _job_portalDbContext.PostJobs.Include(x => x.JobType).ToList();
        }

    }
}


