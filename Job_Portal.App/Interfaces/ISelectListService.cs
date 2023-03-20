using Job_Portal.Models.KeyValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job_Portal.App.Interfaces
{
    public interface ISelectListService
    {
        IEnumerable<KeyValueItem> GetRolesKeysValues();
        IEnumerable<KeyValueItem> GetTypesKeysValues();
    }
}
