//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SCMModels.RemoteModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class RemoteRFQMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RemoteRFQMaster()
        {
            this.RemoteRFQRevisions = new HashSet<RemoteRFQRevision>();
        }
    
        public int RfqMasterId { get; set; }
        public Nullable<int> MPRRevisionId { get; set; }
        public string RFQNo { get; set; }
        public Nullable<int> RFQUniqueNo { get; set; }
        public int VendorId { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public bool DeleteFlag { get; set; }
        public Nullable<System.DateTime> SyncDate { get; set; }
        public Nullable<bool> SyncStatus { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RemoteRFQRevision> RemoteRFQRevisions { get; set; }
    }
}
