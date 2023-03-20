using Job_Portal.App.Interfaces;
using Job_Portal.Models.KeyValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job_Portal.App.Implementations
{
    public class SelectListService : ISelectListService
    {
        private readonly IRolesRepository rolesRepository;
        private readonly IJobTypeRepository jobTypeRepository;

        public SelectListService(IRolesRepository rolesRepository, IJobTypeRepository jobTypeRepository)
        {
            this.rolesRepository = rolesRepository;
            this.jobTypeRepository = jobTypeRepository;
        }
 

        public IEnumerable<KeyValueItem> GetRolesKeysValues()
        {
            try
            {
                var roles = rolesRepository.GetAll().ToList();
                var result = roles.Select(role => new KeyValueItem()
                {
                    SKey = role.Id,
                    Value = role.Name ?? ""
                });

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        } 

        public IEnumerable<KeyValueItem> GetTypesKeysValues()
        {
            try
            {
                var types = jobTypeRepository.GetAll().ToList();
                var result = types.Select(type => new KeyValueItem()
                {
                    Key = type.Id,
                    Value = type.Type ?? ""
                });

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }

}