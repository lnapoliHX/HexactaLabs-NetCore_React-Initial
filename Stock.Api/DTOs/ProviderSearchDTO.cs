using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stock.Api.DTOs
{
    public class ProviderSearchDTO
    {

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public ActionDto Condition { get; set; }

    }
}
