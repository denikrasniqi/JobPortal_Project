using Job_Portal.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job_Portal.App.Interfaces
{
    public interface IRolesRepository : IRepository<AspNetRole>
    {
        AspNetRole? GetByUserId(string userId);
        AspNetRole? GetByStringId(string id);
    }
}
