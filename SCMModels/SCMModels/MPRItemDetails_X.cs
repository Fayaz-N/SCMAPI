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
    
    public partial class MPRItemDetails_X
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MPRItemDetails_X()
        {
            this.MPRItemDetailsAttachments = new HashSet<MPRItemDetailsAttachment>();
            this.MPRPADocuments = new HashSet<MPRPADocument>();
        }
    
        public int ItemDetailsId { get; set; }
        public int RevisionId { get; set; }
        public string ItemDescription { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public string SaleOrderLineItemNo { get; set; }
        public string ReferenceDocNo { get; set; }
        public Nullable<int> PAId { get; set; }
        public string PONo { get; set; }
    
        public virtual MPRPADetail MPRPADetail { get; set; }
        public virtual MPRRevision MPRRevision { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MPRItemDetailsAttachment> MPRItemDetailsAttachments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MPRPADocument> MPRPADocuments { get; set; }
    }
}
