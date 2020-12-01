using Apsy.Elemental.Example.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apsy.Elemental.Example.Web.Models
{
    public class BranchOperationHours
    {
        public int BranchOperationHoursId { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
        public int OperationHoursId { get; set; }
        public OperationHours OperationHours { get; set; } 
    }
}
