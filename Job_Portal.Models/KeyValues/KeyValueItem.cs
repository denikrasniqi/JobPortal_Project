using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job_Portal.Models.KeyValues
{
    public class KeyValueItem
    {
        public int Key { get; set; }
        public string? SKey { get; set; }
        public bool BKey { get; set; }
        public string Value { get; set; } = null!;
    }
}
