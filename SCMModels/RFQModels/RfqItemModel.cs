﻿using SCMModels.SCMModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMModels.RFQModels
{
	public class RfqItemModel
	{

		public RfqItemModel()
		{
			communication = new List<RfqCommunicationModel>();
			iteminfo = new List<RfqItemInfoModel>();
			RFQDocuments = new List<RfqDocumentsModel>();
			rfqterms = new List<RFQTermsModel>();
			mappingrfq = new List<RFQMPRMappingModel>();
			RfqVendorBOM = new List<RfqVendorBOM>();
		}
		public int RFQItemID { get; set; }
		public int RFQRevisionId { get; set; }
		public Nullable<int> MRPItemsDetailsID { get; set; }
		public int MPRRevisionId { get; set; }
		public string ItemId { get; set; }
		public string materialid { get; set; }
		public string Materialdescription { get; set; }
		public double QuotationQty { get; set; }
		public string ItemName { get; set; }
		public string ItemDescription { get; set; }
		public string VendorModelNo { get; set; }
		public string HSNCode { get; set; }
		public string VendorName { get; set; }
        public string VendorCode { get; set; }
        public string DocumentyNo { get; set; }
		public decimal TargetSpend { get; set; }
		public string SaleOrderNo { get; set; }
		public decimal UnitPrice { get; set; }
		public string PaymentTermCode { get; set; }
		public string DocumentNo { get; set; }
		public int RFQItemsId { get; set; }
		public string Department { get; set; }
		public Nullable<byte> DepartmentId { get; set; }
		public Nullable<decimal> CustomDuty { get; set; }
		public Nullable<decimal> CustomDutyAmount { get; set; }
		public Nullable<decimal> FreightPercentage { get; set; }
		public Nullable<decimal> FreightAmount { get; set; }
		public Nullable<decimal> PFPercentage { get; set; }
		public Nullable<decimal> PFAmount { get; set; }
        public Nullable<double> TotalPFAmount { get; set; }
        public Nullable<double> TotalFreightAmount { get; set; }
        public Nullable<decimal> TotalTaxAmount { get; set; }
		public Nullable<decimal> IGSTPercentage { get; set; }
		public Nullable<decimal> CGSTPercentage { get; set; }
		public string MfgPartNo { get; set; }
		public string MfgModelNo { get; set; }
		public string ManufacturerName { get; set; }
		public string RFQNo { get; set; }
		public Nullable<decimal> SGSTPercentage { get; set; }
		public Nullable<decimal> ItemUnitPrice { get; set; }
		public Nullable<decimal> IGSTAmount { get; set; }
		public Nullable<decimal> CGSTAmount { get; set; }
		public Nullable<decimal> SGSTAmount { get; set; }
		public Nullable<bool> taxInclusiveOfDiscount { get; set; }
		public Nullable<decimal> DiscountAmount { get; set; }
		public Nullable<decimal> Discountpercentage { get; set; }
		public Nullable<decimal> NetAmount { get; set; }
		public Nullable<decimal> TotalAmount { get; set; }
		public Nullable<decimal> FinalNetAmount { get; set; }
		public string RequestRemarks { get; set; }
		public Nullable<decimal> HandlingPercentage { get; set; }
		public Nullable<decimal> ImportFreightPercentage { get; set; }
		public Nullable<decimal> InsurancePercentage { get; set; }
		public Nullable<decimal> DutyPercentage { get; set; }
        public Nullable<double> InsuranceAmount { get; set; }
        public Nullable<double> ImportFreightAmount { get; set; }
        public Nullable<double> DutyAmount { get; set; }
        public Nullable<double> HandlingAmount { get; set; }
        public bool IsDeleted { get; set; }
		public int paid { get; set; }
		public int paitemid { get; set; }
		public string PONO { get; set; }
		public string POItemNo { get; set; }
		public string PODate { get; set; }
        public Nullable<System.DateTime> POStatusDate { get; set; }
        public string Remarks { get; set; }
		public int MPRItemDetailsid { get; set; }
		public int Mprrfqsplititemid { get; set; }
		public RfqRevisionModel RFQRevision { get; set; }
		public List<RFQTermsModel> rfqterms { get; set; }
		public List<RfqItemInfoModel> iteminfo { get; set; }
		public List<RfqCommunicationModel> communication { get; set; }
		public List<RfqDocumentsModel> RFQDocuments { get; set; }
		public List<RFQMPRMappingModel> mappingrfq { get; set; }
		public List<RfqVendorBOM> RfqVendorBOM { get; set; }
		public MPRPADetailsModel mprpa { get; set; }
		public Nullable<int> Tklineitemid { get; set; }
        public string TokuchuNo { get; set; }
        public Nullable<int> StandardLeadtime { get; set; }
		public Nullable<int> ProductCategorylevel2id { get; set; }
		public TokuchuLIneItem TokuchuLIneItems { get; set; }
        public string ShipToParty { get; set; }
        public string SoldToParty { get; set; }
        public string EndUser { get; set; }
    }
	public class PADetailsModel
	{
		public string rfqnumber { get; set; }
		public int VendorId { get; set; }
		public int venderid { get; set; }
		public int RequisitionIds { get; set; }
		public int RevisionId { get; set; }
		public string DocumentNumber { get; set; }
		public string DepartmentName { get; set; }
		public string RFQNo { get; set; }
		public int BuyerGroupId { get; set; }
		public string SaleOrderNo { get; set; }
		public int DeptID { get; set; }
        public string vendorProjectManager { get; set; }
		public string POItemNo { get; set; }
		public string POdate { get; set; }
		public string PONO { get; set; }
		public int DepartmentId { get; set; }
		public object EmployeeNo { get; set; }
		public string PAStatus { get; set; }
		public string POStatus { get; set; }
		public string fromDate { get; set; }
		public string toDate { get; set; }
		public int Paid { get; set; }
        public int OrgDepartmentId { get; set; }
        public string Approvername { get; set; }
        public string Approverstatus { get; set; }
    }

	public class ItemsViewModel
	{
		public string POItemNo { get; set; }
		public Nullable<System.DateTime> PODate { get; set; }
        public Nullable<System.DateTime> POStatusDate { get; set; }
        public string PONO { get; set; }
		public string Remarks { get; set; }
		public int paid { get; set; }
		public int paitemid { get; set; }
		public int MRPItemsDetailsID { get; set; }
		public string EmployeeNo { get; set; }
	}
	public class EmployeeFilterModel
	{
		public int DeptId { get; set; }
		public string Employeeid { get; set; }
		public bool LessBudget { get; set; }
		public bool MoreBudget { get; set; }
		public int MinPAValue { get; set; }
		public int MaxPAValue { get; set; }
		public int Authid { get; set; }
	}
}
