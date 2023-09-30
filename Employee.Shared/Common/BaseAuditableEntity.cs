using Employee.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Shared.Common
{
    public abstract class BaseAuditableEntity

    {

        ///<summary>
        /// Created
        ///</summary>
        
        public DateTimeOffset Created { get; set; } = DateTimeOffset.Now;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTimeOffset LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
        public EntityStatus Status { get; set; }



    }
}
