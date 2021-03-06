//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SCMModels.SCMModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class AccessGroupMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AccessGroupMaster()
        {
            this.AccessNames = new HashSet<AccessName>();
        }
    
        public int AccessGroupId { get; set; }
        public string GroupName { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
        public string DeletedRemarks { get; set; }
        public bool DeleteFlag { get; set; }
    
        public virtual UserDetail UserDetail { get; set; }
        public virtual UserDetail UserDetail1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccessName> AccessNames { get; set; }
    }
}
