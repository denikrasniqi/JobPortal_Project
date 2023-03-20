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

    public class JobTypeRepositosy : Repository<JobType>, IJobTypeRepository
    {
        protected readonly Job_PortalContext _job_portalDbContext;
        public JobTypeRepositosy(Job_PortalContext job_portalDbContext) : base(job_portalDbContext)
        {
            _job_portalDbContext = job_portalDbContext;
        }

    }
}
