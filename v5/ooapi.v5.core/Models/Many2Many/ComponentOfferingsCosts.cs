using ooapi.v5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ooapi.v5.core.Models.Many2Many
{
    public class ComponentOfferingsCosts
    {
        public Guid ComponentOfferingId { get; set; }
        public ComponentOffering ComponentOffering { get; set; }

        public Guid CostId { get; set; }
        public Cost Cost { get; set; }

    }
}
