﻿using SCMModels.MPRMasterModels;
using SCMModels.RFQModels;
using SCMModels.SCMModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SCMModels;

namespace DALayer.RFQ
{
    public interface IRFQDA
    {
        DataTable getRFQItems(int RevisionId);
        bool updateVendorQuotes(List<RFQQuoteView> RFQQuoteViewList, List<YILTermsandCondition> termsList, List<MPRRFQDocument> mprfqDocs);
        DataSet getRFQCompareItems(int RevisionId);
        bool rfqStatusUpdate(List<RFQQuoteView> RFQQuoteViewList);

        //rfq module

        Task<RFQMasterModel> GetRFQById(int masterID);
        Task<RfqRevisionModel> CreateRfQ(RfqRevisionModel model,bool addMPRRfq);
        //Task<List<RfqRevisionModel>> GetAllRFQs();
        Task<List<RFQMasterModel>> getallrfqlist();
        Task<RfqRevisionModel> GetRfqDetailsById(int revisionId);
        bool updateRfqDocStatus(List<RFQDocument> rfqDocs);
        Task<statuscheckmodel> UpdateRfqRevision(RfqRevisionModel model);
        Task<statuscheckmodel> UpdateRfqItemByBulk(RfqItemModel model);
        Task<statuscheckmodel> UpdateSingleRfqItem(RfqItemModel model);
        Task<statuscheckmodel> UpdateBulkRfqRevision(RfqRevisionModel model);
        statuscheckmodel DeleteRfqById(int rfqmasterid);
        statuscheckmodel DeleteRfqRevisionbyId(int id);
        statuscheckmodel DeleteRfqItemById(int id);
        statuscheckmodel DeleteBulkItemsByItemId(List<int> id);
        Task<statuscheckmodel> InsertDocument(RfqDocumentsModel model);
        statuscheckmodel CommunicationAdd(RfqCommunicationModel model);
        string UpdateVendorCommunication(RfqCommunicationModel model);
        int addNewRfqRevision(int rfqrevisionId);
        Task<List<RfqItemModel>> GetItemsByRevisionId(int revisionid);
        Task<List<RfqRevisionModel>> GetAllrevisionRFQs();
        Task<RfqItemModel> GetItemsByItemId(int id);
        List<VendormasterModel> GetAllvendorList();
        Task<statuscheckmodel> CreateNewRfq(RFQMasterModel model);
        Task<VendormasterModel> GetvendorById(int id);
        Task<statuscheckmodel> InsertVendorterms(VendorRfqtermModel vendor);
        Task<RfqRevisionModel> InsertRfqItemInfo(RFQItemsInfo_N model);
        Task<statuscheckmodel> DeleteRfqIteminfoByid(int id);
        Task<statuscheckmodel> DeleteRfqItemByid(int id);
        Task<statuscheckmodel> DeleteRfqitemandinfosById(int id);
        Task<statuscheckmodel> UpdateRfqItemInfoById(RfqItemInfoModel model);
        Task<RfqItemModel> GetRfqItemByMPrId(int id);
        Task<statuscheckmodel> InsertSingleIteminfos(RfqItemInfoModel model);
        Task<statuscheckmodel> InsertBulkItemInfos(List<RfqItemInfoModel> model);
        Task<List<UnitMasterModel>> GetUnitMasterList();
        Task<statuscheckmodel> InsertRfqRemainder(RfqRemainderTrackingModel model);
        Task<RfqRemainderTrackingModel> getrfqremaindersById(int id);
        Task<statuscheckmodel> Insertrfqvendorterms(RfqVendorTermModel model);
        Task<RfqVendorTermModel> getRfqVendorById(int id);
        Task<statuscheckmodel> RemoveRfqVendorTermsById(int id);
        Task<statuscheckmodel> RemoveVendorRfqByid(int id);
       	List<CurrencyMaster> UpdateNewCurrencyMaster(CurrencyMaster model);
        Task<statuscheckmodel> InsertCurrentCurrencyHistory(CurrencyHistoryModel model);
        Task<statuscheckmodel> UpdateCurrentCurrencyHistory(CurrencyHistoryModel model);
		List<CurrencyMaster> GetAllMasterCurrency();
        Task<CurrencyMasterModel> GetMasterCurrencyById(int currencyId);
		List<CurrencyMaster> RemoveMasterCurrencyById(int currencyId,string DeletedBy);
        Task<CurrencyHistoryModel> GetcurrencyHistoryById(int currencyId);
        Task<MPRBuyerGroupModel> GetMPRBuyerGroupsById(int id);
        Task<List<MPRBuyerGroupModel>> GetAllMPRBuyerGroups();
        Task<List<MPRApproversViewModel>> GetAllMPRApprovers();
        Task<statuscheckmodel> InsertMprBuyerGroups(MPRBuyerGroupModel model);
        Task<statuscheckmodel> UpdateMprBuyerGroups(MPRBuyerGroupModel model);
        Task<statuscheckmodel> InsertMPRApprover(MPRApproverModel model);
        Task<MPRApproverModel> GetMPRApprovalsById(int id);
        Task<List<MPRApproverModel>> GetAllMPRApprovals();
        Task<List<MPRDepartmentModel>> GetAllMPRDepartments();
        Task<MPRDepartmentModel> GetMPRDepartmentById(int id);
        Task<List<MPRDispatchLocationModel>> GetAllMPRDispatchLocations();
        Task<MPRDispatchLocationModel> GetMPRDispatchLocationById(int id);
        Task<List<MPRCustomsDutyModel>> GetAllCustomDuty();
        Task<statuscheckmodel> InsertYILTerms(YILTermsandConditionModel model);
        Task<statuscheckmodel> InsertYILTermsGroup(YILTermsGroupModel model);
        Task<statuscheckmodel> InsertRFQTerms(RFQTermsModel model);
        Task<statuscheckmodel> UpdateRFQTerms(RFQTermsModel model);
        Task<YILTermsandConditionModel> GetYILTermsByBuyerGroupID(int id);
        Task<YILTermsGroupModel> GetYILTermsGroupById(int id);
        Task<RFQTermsModel> GetRfqTermsById(int termsid);
        Task<RfqItemModel> GetItemByItemId(int id);
        Task<List<RFQMasterModel>> GetRfqByVendorId(int vendorid);
        List<RFQListView> getRFQList(rfqFilterParams Rfqfilterparams);

        //pa authorization
        Task<statuscheckmodel> InsertPAAuthorizationLimits(PAAuthorizationLimitModel model);
        Task<PAAuthorizationLimitModel> GetPAAuthorizationLimitById(int deptid);
        Task<statuscheckmodel> CreatePAAuthirizationEmployeeMapping(PAAuthorizationEmployeeMappingModel model);
        Task<PAAuthorizationEmployeeMappingModel> GetMappingEmployee(PAAuthorizationLimitModel limit);
        Task<statuscheckmodel> CreatePACreditDaysmaster(PACreditDaysMasterModel model);
        Task<PACreditDaysMasterModel> GetCreditdaysMasterByID(int creditdaysid);
        Task<statuscheckmodel> AssignCreditdaysToEmployee(PACreditDaysApproverModel model);
        Task<statuscheckmodel> RemovePAAuthorizationLimitsByID(int authid);
        Task<statuscheckmodel> RemovePACreditDaysMaster(int creditid);
        Task<List<PAAuthorizationLimitModel>> GetPAAuthorizationLimitsByDeptId(int departmentid);
        Task<statuscheckmodel> RemovePACreditDaysApprover(EmployeemappingtocreditModel model);
        Task<statuscheckmodel> RemovePurchaseApprover(EmployeemappingtopurchaseModel model);
        Task<PACreditDaysApproverModel> GetPACreditDaysApproverById(int ApprovalId);
        Task<EmployeModel> GetEmployeeMappings(PAConfigurationModel model);
        Task<List<RfqItemModel>> GetRfqItemsByRevisionId(int revisionid);
        //Task<List<LoadItemsByID>> GetItemsByMasterIDs(PADetailsModel masters);
        List<LoadItemsByID> GetItemsByMasterIDs(PADetailsModel masters);
        Task<List<DepartmentModel>> GetAllDepartments();
        Task<List<PAAuthorizationLimitModel>> GetSlabsByDepartmentID(int DeptID);
        Task<List<EmployeModel>> GetAllEmployee();
        Task<List<PAAuthorizationLimitModel>> GetAllCredits();
        Task<List<PACreditDaysMasterModel>> GetAllCreditDays();
        Task<List<MPRPAPurchaseModesModel>> GetAllMprPAPurchaseModes();
        Task<List<MPRPAPurchaseTypesModel>> GetAllMprPAPurchaseTypes();
        Task<statuscheckmodel> InsertPurchaseAuthorization(MPRPADetailsModel model);
        Task<MPRPADetailsModel> GetMPRPADeatilsByPAID(int PID);
        Task<List<MPRPADetailsModel>> GetAllMPRPAList();
        Task<List<PAFunctionalRolesModel>> GetAllPAFunctionalRoles();
        Task<List<EmployeemappingtocreditModel>> GetCreditSlabsandemployees();
        Task<List<EmployeemappingtopurchaseModel>> GetPurchaseSlabsandMappedemployees();
        Task<List<ProjectManagerModel>> LoadAllProjectManagers();
        Task<List<VendormasterModel>> LoadVendorByMprDetailsId(List<int?> MPRItemDetailsid);
        Task<List<MPRPAApproversModel>> GetAllApproversList();
        Task<List<GetmprApproverdeatil>> GetMprApproverDetailsBySearch(PAApproverDetailsInputModel model);
        Task<statuscheckmodel> UpdateMprpaApproverStatus(MPRPAApproversModel model);
        Task<List<DisplayRfqTermsByRevisionId>> getrfqtermsbyrevisionid(List<int> RevisionId);
        Task<List<EmployeemappingtopurchaseModel>> GetPurchaseSlabsandMappedemployeesByDeptId(int deptid);
        Task<statuscheckmodel> InsertPaitems(ItemsViewModel paitem);
        Task<List<GetMappedSlab>> GetAllMappedSlabs();
        Task<statuscheckmodel> RemoveMappedSlab(PAAuthorizationLimitModel model);
        Task<List<GetMprPaDetailsByFilter>> getMprPaDetailsBySearch(PADetailsModel model);
        bool PreviouPriceUpdate(MPRItemInfo previousprice);
		bool updateHandlingCharges(List<RFQItems_N> rfqItems);
	}
}
