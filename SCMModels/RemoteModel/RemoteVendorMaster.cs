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
    
    public partial class RemoteVendorMaster
    {
        public int Vendorid { get; set; }
        public string VendorCode { get; set; }
        public string VendorName { get; set; }
        public string OldVendorCode { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public Nullable<byte> RegionCode { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNo { get; set; }
        public string FaxNo { get; set; }
        public string AuGr { get; set; }
        public string PaymentTermCode { get; set; }
        public string Blocked { get; set; }
        public string Emailid { get; set; }
        public string ContactNo { get; set; }
        public bool AutoAssignmentofRFQ { get; set; }
        public Nullable<bool> Deleteflag { get; set; }
    }
}
