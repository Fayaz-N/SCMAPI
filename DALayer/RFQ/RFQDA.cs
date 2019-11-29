﻿using SCMModels.RemoteModel;
using SCMModels.RFQModels;
using SCMModels.SCMModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALayer.RFQ
{
	public class RFQDA:IRFQDA
	{
        VSCMEntities vscm = new VSCMEntities();
        YSCMEntities obj = new YSCMEntities();
        public List<RFQItemsView> getRFQItems(int RevisionId)
		{
			using (YSCMEntities Context = new YSCMEntities())
			{
				return Context.RFQItemsViews.Where(li => li.MPRRevisionId == RevisionId).ToList();
			}
		}

        public async Task<statuscheckmodel> CreateRfQ(RfqRevisionModel model)
        {
            statuscheckmodel status = new statuscheckmodel();
            try
            {
                //server data
                if (model != null)
                {
                    var rfqremote = new RemoteRFQMaster();
                    var remote = vscm.Database.Connection.ConnectionString;
                    vscm.Database.Connection.Open();
                    if (model.rfqmaster.RfqMasterId == 0)
                    {
                        //string unique = obj.RFQMasters.Select(x => x.RFQNo).FirstOrDefault();
                        rfqremote.RFQNo = "rfq/" + DateTime.Now.ToString("MMyy") + "/";
                        rfqremote.RFQUniqueNo = model.rfqmaster.RfqUniqueNo;
                        rfqremote.CreatedBy = model.rfqmaster.CreatedBy;
                        rfqremote.CreatedDate = model.rfqmaster.Created;
                        rfqremote.VendorId = model.rfqmaster.VendorId;
                        vscm.RemoteRFQMasters.Add(rfqremote);
                        vscm.SaveChanges();
                    }
                    else
                    {
                        rfqremote.RFQUniqueNo = model.rfqmaster.RfqUniqueNo;
                        //rfqdomain.RfqMasterId = model.RfqMasterId;
                        rfqremote.VendorId = model.rfqmaster.VendorId;
                        rfqremote.CreatedBy = model.rfqmaster.CreatedBy;
                        rfqremote.CreatedDate = model.rfqmaster.Created;
                        vscm.RemoteRFQMasters.Add(rfqremote);
                        vscm.SaveChanges();
                    }

                    int id = rfqremote.RfqMasterId;

                    RemoteRFQRevision revision = new RemoteRFQRevision();
                    if (model.RfqRevisionId == 0)
                    {
                        revision.rfqMasterId = id;
                        revision.RevisionNo = model.RfqRevisionNo;
                        revision.CreatedBy = model.CreatedBy;
                        revision.CreatedDate = model.CreatedDate;
                        revision.RFQValidDate = model.RfqValidDate;
                        revision.SalesTax = model.salesTax;
                        revision.PaymentTermRemarks = model.PaymentTermRemarks;
                        revision.PackingForwarding = model.PackingForwading;
                        revision.PaymentTermDays = model.PaymentTermDays;
                        revision.Insurance = model.Insurance;
                        revision.ExciseDuty = model.ExciseDuty;
                        revision.ShipmentModeid = model.ShipmentModeId;
                        revision.BankGuarantee = model.BankGuarantee;
                        revision.DeliveryMaxWeeks = model.DeliveryMaxWeeks;
                        revision.DeliveryMinWeeks = model.DeliveryMinWeeks;
                        //revision.RemoteRFQStatus.Select(x => new Remotedata.RFQStatu()
                        //{
                        //    StatusId = Convert.ToInt32(Enum.GetName(typeof(RFQStatusType), RFQStatusType.requested))
                        //});

                        vscm.RemoteRFQRevisions.Add(revision);
                        vscm.SaveChanges();
                    }
                    else
                    {
                        revision.RevisionNo = model.RfqRevisionNo;
                        revision.CreatedBy = model.CreatedBy;
                        revision.CreatedDate = model.CreatedDate;
                        revision.RFQValidDate = model.RfqValidDate;
                        revision.SalesTax = model.salesTax;
                        revision.PaymentTermRemarks = model.PaymentTermRemarks;
                        revision.PackingForwarding = model.PackingForwading;
                        revision.PaymentTermDays = model.PaymentTermDays;
                        revision.Insurance = model.Insurance;
                        revision.ExciseDuty = model.ExciseDuty;
                        revision.ShipmentModeid = model.ShipmentModeId;
                        revision.BankGuarantee = model.BankGuarantee;
                        revision.DeliveryMaxWeeks = model.DeliveryMaxWeeks;
                        revision.DeliveryMinWeeks = model.DeliveryMinWeeks;

                        vscm.RemoteRFQRevisions.Add(revision);
                        vscm.SaveChanges();
                    }


                    int revisionid = revision.rfqRevisionId;

                    foreach (var data in model.rfqitem)
                    {

                        var rfitems = new RemoteRFQItem()
                        {
                            RFQRevisionId = revisionid,
                            MPRItemDetailsid = data.MRPItemsDetailsID,
                            QuotationQty = data.QuotationQty,
                            VendorModelNo = data.VendorModelNo,
                            HSNCode = data.HSNCode,
                            RequestRemarks = data.RequsetRemarks,
                            DeleteFlag = false
                        };
                        vscm.RemoteRFQItems.Add(rfitems);
                        obj.SaveChanges();
                    }
                }
                vscm.Database.Connection.Close();
                if (model != null)
                {
                    obj.Database.Connection.Open();
                    var rfqlocal = new RFQMaster();
                    if (model.rfqmaster.RfqMasterId == 0)
                    {
                        //string unique = obj.RFQMasters.Select(x => x.RFQNo).FirstOrDefault();
                        rfqlocal.RFQNo = "rfq/" + DateTime.Now.ToString("MMyy") + "/";

                        rfqlocal.RFQUniqueNo = model.rfqmaster.RfqUniqueNo;
                        rfqlocal.CreatedBy = model.rfqmaster.CreatedBy;
                        rfqlocal.CreatedDate = model.rfqmaster.Created;
                        rfqlocal.VendorId = model.rfqmaster.VendorId;
                        obj.RFQMasters.Add(rfqlocal);
                        obj.SaveChanges();
                    }
                    else
                    {
                        rfqlocal.RFQUniqueNo = model.rfqmaster.RfqUniqueNo;
                        //rfqdomain.RfqMasterId = model.RfqMasterId;
                        rfqlocal.VendorId = model.rfqmaster.VendorId;
                        rfqlocal.CreatedBy = model.rfqmaster.CreatedBy;
                        rfqlocal.CreatedDate = model.rfqmaster.Created;
                        obj.RFQMasters.Add(rfqlocal);
                        obj.SaveChanges();
                    }

                    int id = rfqlocal.RfqMasterId;
                    RFQRevision revision = new RFQRevision();
                    if (model.RfqRevisionId == 0)
                    {
                        revision.rfqMasterId = id;
                        revision.RevisionNo = model.RfqRevisionNo;
                        revision.CreatedBy = model.CreatedBy;
                        revision.CreatedDate = model.CreatedDate;
                        revision.RFQValidDate = model.RfqValidDate;
                        revision.SalesTax = model.salesTax;
                        revision.PaymentTermRemarks = model.PaymentTermRemarks;
                        revision.PackingForwarding = model.PackingForwading;
                        revision.PaymentTermDays = model.PaymentTermDays;
                        revision.Insurance = model.Insurance;
                        revision.ExciseDuty = model.ExciseDuty;
                        revision.ShipmentModeid = model.ShipmentModeId;
                        revision.BankGuarantee = model.BankGuarantee;
                        revision.DeliveryMaxWeeks = model.DeliveryMaxWeeks;
                        revision.DeliveryMinWeeks = model.DeliveryMinWeeks;
                        //revision.RFQStatus.Select(x => new RFQStatu()
                        //{
                        //    StatusId = Convert.ToInt32(Enum.GetName(typeof(RFQStatusType), RFQStatusType.requested))
                        //});

                        obj.RFQRevisions.Add(revision);
                        obj.SaveChanges();
                    }
                    else
                    {
                        revision.RevisionNo = model.RfqRevisionNo;
                        revision.CreatedBy = model.CreatedBy;
                        revision.CreatedDate = model.CreatedDate;
                        revision.RFQValidDate = model.RfqValidDate;
                        revision.SalesTax = model.salesTax;
                        revision.PaymentTermRemarks = model.PaymentTermRemarks;
                        revision.PackingForwarding = model.PackingForwading;
                        revision.PaymentTermDays = model.PaymentTermDays;
                        revision.Insurance = model.Insurance;
                        revision.ExciseDuty = model.ExciseDuty;
                        revision.ShipmentModeid = model.ShipmentModeId;
                        revision.BankGuarantee = model.BankGuarantee;
                        revision.DeliveryMaxWeeks = model.DeliveryMaxWeeks;
                        revision.DeliveryMinWeeks = model.DeliveryMinWeeks;

                        obj.RFQRevisions.Add(revision);
                        obj.SaveChanges();
                    }


                    int revisionid = revision.rfqRevisionId;

                    foreach (var data in model.rfqitem)
                    {

                        var rfitems = new RFQItem()
                        {
                            RFQRevisionId = revisionid,
                            MPRItemDetailsid = data.MRPItemsDetailsID,
                            QuotationQty = data.QuotationQty,
                            VendorModelNo = data.VendorModelNo,
                            HSNCode = data.HSNCode,
                            RequestRemarks = data.RequsetRemarks,
                            DeleteFlag = false
                        };
                        revision.RFQItems.Add(rfitems);
                        obj.SaveChanges();
                    }
                    status.Sid = rfqlocal.RfqMasterId;
                }
                return status;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                           eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                               ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

        }
        public async Task<statuscheckmodel> CreateNewRfq(RFQMasterModel model)
        {
            statuscheckmodel status = new statuscheckmodel();
            try
            {
                var rfqremote = new RemoteRFQMaster();
                if (model != null)
                {
                    //var remoteconnection = obj.Database.Connection.ConnectionString;
                    var remote = vscm.Database.Connection.ConnectionString;
                    vscm.Database.Connection.Open();
                    //vscm.Database.Connection.Close();
                    var result = obj.Database.Connection.ConnectionString;

                    rfqremote.RFQNo = model.RfqNo;
                    rfqremote.RFQUniqueNo = model.RfqUniqueNo;
                    rfqremote.MPRRevisionId = model.MPRRevisionId;
                    rfqremote.VendorId = model.VendorId;
                    rfqremote.CreatedBy = model.CreatedBy;
                    rfqremote.CreatedDate = model.Created;
                    vscm.RemoteRFQMasters.Add(rfqremote);
                    vscm.SaveChanges();
                }
                int masterid = rfqremote.RfqMasterId;
                vscm.Database.Connection.Close();
                var rfqlocal = new RFQMaster();
                if (model != null)
                {
                    var localconnection = vscm.Database.Connection.ConnectionString;
                    obj.Database.Connection.Open();
                    rfqlocal.RfqMasterId = masterid;
                    rfqlocal.RFQNo = model.RfqNo;
                    rfqlocal.RFQUniqueNo = model.RfqUniqueNo;
                    rfqlocal.MPRRevisionId = model.MPRRevisionId;
                    rfqlocal.VendorId = model.VendorId;
                    rfqlocal.CreatedBy = model.CreatedBy;
                    rfqlocal.CreatedDate = model.Created;
                    obj.RFQMasters.Add(rfqlocal);
                    obj.SaveChanges();
                    obj.Database.Connection.Close();
                }

                return status;
            }

            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task<List<RFQMasterModel>> getallrfqlist()
        {
            List<RFQMasterModel> master = new List<RFQMasterModel>();
            try
            {
                var result = obj.RFQMasters.Where(x => x.DeleteFlag == false).Include(x => x.RFQRevisions).ToList();
                if (result != null)
                {
                    foreach (var item in result)
                    {
                        master = result.ConvertAll(x => new RFQMasterModel()
                        {
                            RfqMasterId = x.RfqMasterId,
                            VendorId = x.VendorId,
                            RfqNo = x.RFQNo,
                            RfqUniqueNo = x.RFQUniqueNo,
                            Created = x.CreatedDate,
                            CreatedBy = x.CreatedBy,
                            Revision = x.RFQRevisions.Where(y => y.rfqMasterId == x.RfqMasterId).Select(y => new RfqRevisionModel()
                            {
                                RfqRevisionId = y.rfqRevisionId,
                                RfqValidDate = y.RFQValidDate,
                                RfqMasterId = y.rfqMasterId,
                                RfqRevisionNo = y.RevisionNo
                            }).ToList()
                        });

                    }

                }
                //remote data
                //var remotedata = vscm.RemoteRFQMasters.Where(x => x.DeleteFlag == false).Include(x => x.RemoteRFQRevisions).ToList();
                //if (remotedata != null)
                //{
                //    foreach (var item in remotedata)
                //    {
                //        master = result.ConvertAll(x => new RFQMasterModel()
                //        {
                //            RfqMasterId = x.RfqMasterId,
                //            VendorId = x.VendorId,
                //            RfqNo = x.RFQNo,
                //            RfqUniqueNo = x.RFQUniqueNo,
                //            Created = x.CreatedDate,
                //            CreatedBy = x.CreatedBy,
                //            Revision = x.RFQRevisions.Where(y => y.rfqMasterId == x.RfqMasterId).Select(y => new RfqRevisionModel()
                //            {
                //                RfqRevisionId = y.rfqRevisionId,
                //                RfqValidDate = y.RFQValidDate,
                //                RfqMasterId = y.rfqMasterId,
                //                RfqRevisionNo = y.RevisionNo
                //            }).ToList()
                //        });

                //    }

                //}
                return master;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //public async Task<List<RfqRevisionModel>> GetAllRFQs()
        //{
        //    List<RfqRevisionModel> model;

        //    try
        //    {
        //        obj.Configuration.ProxyCreationEnabled = false;
        //        model = obj.RFQRevisions.Where(x => x.DeleteFlag == false).Include(x => x.RFQMaster).ToList();
        //        //foreach (var item in revision)
        //        //{
        //        //    model = revision.Select(x => new RfqRevisionModel()
        //        //    {
        //        //        RfqRevisionId = x.rfqRevisionId,
        //        //        RfqRevisionNo = x.RevisionNo,
        //        //        RfqValidDate = x.RFQValidDate,
        //        //        CreatedBy = x.CreatedBy,
        //        //        CreatedDate = x.CreatedDate,
        //        //    }).ToList();
        //        //    foreach (var items in revision)
        //        //    {
        //        //        foreach (var masters in revision)
        //        //        {

        //        //        }
        //        //    }

        //        //}   




        //        //foreach (var item in revision)
        //        //{
        //        //    revision.Select(x => new RfqRevisionModel()
        //        //    {
        //        //        RfqRevisionId = item.rfqRevisionId,
        //        //        CreatedDate = item.CreatedDate,
        //        //        RfqValidDate = item.RFQValidDate,
        //        //    }).ToList();
        //        //    revision.Select(x => new RFQMasterModel()
        //        //    {
        //        //        RfqMasterId=item.RFQMaster.RfqMasterId,
        //        //        RfqUniqueNo=item.RFQMaster.RFQUniqueNo,
        //        //        RfqNo=item.RFQMaster.RFQNo
        //        //    }).ToList();

        //        //}
        //        return model;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        //public async Task<RFQMasterDataModel> GetAllRFQs()
        //{
        //    RFQMasterDataModel rfqitems = new RFQMasterDataModel();
        //    //var rfqs = from x in obj.RFQMasters where x.DeleteFlag=="false"  select x;
        //    try
        //    {
        //        var rfqs = obj.RFQMasters.Where(x => x.DeleteFlag == false).Select(x => x).ToList();
        //        //var rfqs = obj.RFQMasters.SqlQuery("select * from RFQMaster where DeleteFlag=0");
        //        // var rfqs = obj.RFQMasters.Where(x => x.DeleteFlag = false).ToList();
        //        //List<RFQMasterModel> rfqs = (from x in obj.RFQMasters where x.DeleteFlag == false select x)
        //        RFQMasterModel model = new RFQMasterModel();
        //        foreach (var item in rfqs)
        //        {
        //            model.Created = item.CreatedDate;
        //            model.CreatedBy = item.CreatedBy;
        //            model.RfqMasterId = item.RfqMasterId;
        //            model.RfqNo = item.RFQNo;
        //            model.RfqUniqueNo = item.RFQUniqueNo;
        //            model.VendorId = item.VendorId;
        //            rfqitems.RFQlist.Add(model);
        //        }
        //        return rfqitems;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }

        //}
        ////public async Task<RFQMasterDataModel> GetAllRFQs()
        //{
        //    RFQMasterDataModel rfqitems = new RFQMasterDataModel();
        //    List< RFQMasterModel> rfqs = new List< RFQMasterModel>();
        //    //var rfqs = from x in obj.RFQMasters where x.DeleteFlag=="false"  select x;
        //    try
        //    {
        //         rfqs = (from x in obj.RFQMasters
        //                    where x.DeleteFlag == false
        //                    select new RFQMasterModel
        //                    {
        //                       RfqMasterId=x.RfqMasterId,
        //                       RfqUniqueNo=x.RFQUniqueNo,
        //                       RfqNo=x.RFQNo,
        //                       VendorId=x.VendorId,
        //                       CreatedBy=x.CreatedBy,
        //                       Created=x.CreatedDate
        //                    }).ToList();
        //        rfqitems.RFQlist.Add(rfqs);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }

        //}
        public async Task<List<RfqRevisionModel>> GetAllrevisionRFQs()
        {
            List<RfqRevisionModel> rfq = new List<RfqRevisionModel>();
            try
            {
                return (from x in obj.RFQRevisions
                        where x.DeleteFlag == false
                        select new RfqRevisionModel()
                        {
                            RfqRevisionId = x.rfqRevisionId,
                            RfqMasterId = x.rfqMasterId,
                            CreatedBy = x.CreatedBy,
                            CreatedDate = x.CreatedDate,
                            RfqRevisionNo = x.RevisionNo,
                            RfqValidDate = x.RFQValidDate
                        }).ToList();

            }

            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task<RFQMasterModel> GetRFQById(int id)
        {
            RFQMasterModel model = new RFQMasterModel();
            RFQMaster rfq = new RFQMaster();
            RFQItem rfqitem = new RFQItem();
            try
            {
                rfq = obj.RFQMasters.Where(x => x.RfqMasterId == id).Include(x => x.RFQRevisions).SingleOrDefault<RFQMaster>();
                //rfq.RFQRevisions = obj.RFQRevisions.Where(x => x.rfqMasterId == id).Include(x=>x.RFQItems).ToList<RFQRevision>();
                if (rfq != null)
                {
                    model.RfqMasterId = rfq.RfqMasterId;
                    model.RfqNo = rfq.RFQNo;
                    model.RfqUniqueNo = rfq.RFQUniqueNo;
                    model.VendorId = rfq.VendorId;
                    model.CreatedBy = rfq.CreatedBy;


                    foreach (var item in rfq.RFQRevisions)
                    {
                        RfqRevisionModel RfqRevisionModel = new RfqRevisionModel();
                        RfqRevisionModel.RfqMasterId = item.rfqMasterId;
                        RfqRevisionModel.RfqRevisionId = item.rfqRevisionId;
                        RfqRevisionModel.RfqValidDate = item.RFQValidDate;
                        RfqRevisionModel.CreatedBy = item.CreatedBy;
                        RfqRevisionModel.CreatedDate = item.CreatedDate;
                        RfqRevisionModel.salesTax = item.SalesTax;
                        RfqRevisionModel.PaymentTermDays = item.PaymentTermDays;
                        RfqRevisionModel.freight = item.Freight;
                        RfqRevisionModel.Insurance = item.Insurance;
                        RfqRevisionModel.IsDeleted = item.DeleteFlag;
                        RfqRevisionModel.DeliveryMaxWeeks = item.DeliveryMaxWeeks;
                        RfqRevisionModel.DeliveryMinWeeks = item.DeliveryMinWeeks;
                        RfqRevisionModel.CustomsDuty = item.CustomsDuty;
                        RfqRevisionModel.BankGuarantee = item.BankGuarantee;
                        RfqRevisionModel.ExciseDuty = item.ExciseDuty;

                        model.Revision.Add(RfqRevisionModel);


                        //var result = obj.RFQItems.Where(x => x.RFQRevisionId == RfqRevisionModel.RfqRevisionId).ToList<RFQItem>();
                        //if (result != null)
                        //{
                        //    RfqItemModel rfqs = new RfqItemModel();
                        //    foreach (RFQItem items in item.RFQItems)
                        //    {
                        //        rfqs.RFQItemID = items.RFQItemsId;
                        //        rfqs.HSNCode = items.HSNCode;
                        //        rfqs.RFQRevisionId = items.RFQRevisionId;
                        //        rfqs.MRPItemsDetailsID = items.MPRItemDetailsid;
                        //        rfqs.QuotationQty = items.QuotationQty;
                        //        rfqs.VendorModelNo = items.VendorModelNo;
                        //        rfqs.RequsetRemarks = items.RequestRemarks;

                        //        RfqRevisionModel.rfqitem.Add(rfqs);
                        //    }

                        //}

                    }
                }
                //var gets = from x in obj.RFQMasters where x.RfqMasterId == id select x;

                return model;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                           eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                               ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

        }
        public statuscheckmodel DeleteRfqById(int id)
        {
            statuscheckmodel status = new statuscheckmodel();
            var domainentity = obj.Set<RFQMaster>().Where(x => x.RfqMasterId == id && x.DeleteFlag == false).Include(x => x.RFQRevisions).SingleOrDefault();


            if (domainentity != null)
            {
                domainentity.DeleteFlag = true;
                obj.SaveChanges();
                var revision = obj.RFQRevisions.Where(l => l.rfqMasterId == domainentity.RfqMasterId && l.DeleteFlag == false).ToList();
                if (revision != null)
                {
                    foreach (var item in revision)
                    {
                        item.DeleteFlag = true;
                        obj.SaveChanges();
                    }

                    var ids = revision.Select(x => x.rfqRevisionId).ToList();
                    var itemlist = obj.RFQItems.Where(x => ids.Contains(x.RFQRevisionId) && x.DeleteFlag == false).ToList();
                    if (itemlist != null)
                    {
                        foreach (var item in itemlist)
                        {
                            item.DeleteFlag = true;
                            obj.SaveChanges();
                        }
                    }
                }
            }
            return status;
        }

        //public async Task<statusmodel> UpdateRfqRevision(RFQMasterModel model)
        //{
        //    statusmodel status = new statusmodel();
        //    var result = obj.RFQMasters.Where(x => x.RfqMasterId == model.RfqMasterId).Include(x=>x.RFQRevisions).FirstOrDefault<RFQMaster>();
        //    try
        //    {
        //        if (result!=null)
        //        {
        //            foreach (var item in model.Revision)
        //            {
        //                // var revision = new RFQRevision();
        //                var existing = result.RFQRevisions.Where(x => x.rfqMasterId == item.RfqMasterId).ToList<RFQRevision>();
        //                if (existing!=null)
        //                {
        //                    obj.Entry(existing).CurrentValues.SetValues(item);
        //                }

        //                //revision.SalesTax = item.salesTax;
        //                //revision.RFQValidDate = item.RfqValidDate;
        //                //revision.CreatedBy = item.CreatedBy;
        //                //revision.PaymentTermDays = item.PaymentTermDays;
        //                //revision.DeliveryMaxWeeks = item.DeliveryMaxWeeks;
        //                //revision.DeliveryMinWeeks = item.DeliveryMinWeeks;
        //                //revision.BankGuarantee = item.BankGuarantee;
        //                //revision.ExciseDuty = item.ExciseDuty;
        //                //revision.CustomsDuty = item.CustomsDuty;
        //                //revision.Insurance = item.Insurance;
        //                //revision.Freight = item.freight;
        //                //revision.PackingForwarding = item.PackingForwading;
        //                //revision.CreatedDate = item.CreatedDate;
        //            }

        //            await obj.SaveChangesAsync();
        //        }
        //        status.Sid = model.RfqMasterId;
        //        return  status;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}
        public async Task<statuscheckmodel> UpdateRfqRevision(RfqRevisionModel model)
        {
            statuscheckmodel status = new statuscheckmodel();
            var result = obj.RFQRevisions.Where(x => x.rfqRevisionId == model.RfqRevisionId).Include(x => x.RFQMaster).FirstOrDefault<RFQRevision>();
            try
            {

                if (result != null)
                {
                    RFQRevision revision = new RFQRevision();

                    result.SalesTax = model.salesTax;
                    result.RFQValidDate = model.RfqValidDate;
                    result.CreatedBy = model.CreatedBy;
                    result.PaymentTermDays = model.PaymentTermDays;
                    result.DeliveryMaxWeeks = model.DeliveryMaxWeeks;
                    result.DeliveryMinWeeks = model.DeliveryMinWeeks;
                    result.BankGuarantee = model.BankGuarantee;
                    result.ExciseDuty = model.ExciseDuty;
                    result.CustomsDuty = model.CustomsDuty;
                    result.Insurance = model.Insurance;
                    result.Freight = model.freight;
                    result.PackingForwarding = model.PackingForwading;
                    result.CreatedDate = model.CreatedDate;

                    obj.SaveChanges();
                    obj.Database.Connection.Close();
                }
                vscm.Database.Connection.Open();
                var remotedata=vscm.RemoteRFQRevisions.Where(x=>x.rfqRevisionId==model.RfqRevisionId).Include(x => x.RemoteRFQMaster).FirstOrDefault<RemoteRFQRevision>();
                if (remotedata!=null)
                {
                    RemoteRFQRevision revision = new RemoteRFQRevision();
                    remotedata.SalesTax = model.salesTax;
                    remotedata.RFQValidDate = model.RfqValidDate;
                    remotedata.CreatedBy = model.CreatedBy;
                    remotedata.PaymentTermDays = model.PaymentTermDays;
                    remotedata.DeliveryMaxWeeks = model.DeliveryMaxWeeks;
                    remotedata.DeliveryMinWeeks = model.DeliveryMinWeeks;
                    remotedata.BankGuarantee = model.BankGuarantee;
                    remotedata.ExciseDuty = model.ExciseDuty;
                    remotedata.CustomsDuty = model.CustomsDuty;
                    remotedata.Insurance = model.Insurance;
                    remotedata.Freight = model.freight;
                    remotedata.PackingForwarding = model.PackingForwading;
                    remotedata.CreatedDate = model.CreatedDate;
                    vscm.SaveChanges();
                }
                status.Sid = model.RfqMasterId;
                return status;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<statuscheckmodel> UpdateBulkRfqRevision(RfqRevisionModel model)
        {
            statuscheckmodel status = new statuscheckmodel();
            var result = obj.RFQRevisions.Where(x => x.rfqMasterId == model.RfqMasterId).Include(x => x.RFQMaster).ToList<RFQRevision>();
            try
            {

                if (result != null)
                {
                    foreach (var item in result)
                    {
                        item.SalesTax = model.salesTax;
                        item.RFQValidDate = model.RfqValidDate;
                        item.CreatedBy = model.CreatedBy;
                        item.PaymentTermDays = model.PaymentTermDays;
                        item.DeliveryMaxWeeks = model.DeliveryMaxWeeks;
                        item.DeliveryMinWeeks = model.DeliveryMinWeeks;
                        item.BankGuarantee = model.BankGuarantee;
                        item.ExciseDuty = model.ExciseDuty;
                        item.CustomsDuty = model.CustomsDuty;
                        item.Insurance = model.Insurance;
                        item.Freight = model.freight;
                        item.PackingForwarding = model.PackingForwading;
                        item.CreatedDate = model.CreatedDate;

                        obj.SaveChanges();
                    }

                }
                status.Sid = model.RfqMasterId;
                return status;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<statuscheckmodel> UpdateRfqItemByBulk(RfqItemModel model)
        {
            statuscheckmodel status = new statuscheckmodel();
            RFQRevision revision = new RFQRevision();
            try
            {
                var result = obj.RFQItems.Where(x => x.RFQRevisionId == model.RFQRevisionId).Include(x => x.RFQRevision).ToList<RFQItem>();

                if (result != null)
                {
                    foreach (var item in result)
                    {
                        item.HSNCode = model.HSNCode;
                        item.QuotationQty = model.QuotationQty;
                        item.VendorModelNo = model.VendorModelNo;
                        item.RequestRemarks = model.RequsetRemarks;
                        obj.SaveChanges();
                    }
                }

                return status;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<RfqItemModel>> GetItemsByRevisionId(int id)
        {
            List<RfqItemModel> rfq = new List<RfqItemModel>();

            try
            {
                return (from x in obj.RFQItems
                        where x.RFQRevisionId == id
                        select new RfqItemModel
                        {
                            HSNCode = x.HSNCode,
                            QuotationQty = x.QuotationQty,
                            RFQRevisionId = x.RFQRevisionId,
                            VendorModelNo = x.VendorModelNo,
                            RequsetRemarks = x.RequestRemarks,
                            RFQItemID = x.RFQItemsId
                        }).ToList();

                //foreach (var item in revision.RFQVendorTerms)
                //{
                //    item.
                //} 

            }

            catch (Exception ex)
            {

                throw;
            }
        }

        //public RFQMasterModel getItemsbyRevisionno(string id)
        // {
        //     RFQMasterModel master = new RFQMasterModel();
        //     try
        //     {
        //         var masters = obj.RFQMasters.Where(x => x.RFQNo == id && x.DeleteFlag == false).Include(x => x.RFQRevisions).Select(x => x).SingleOrDefault();
        //         if (masters!=null)
        //         {

        //         }
        //     }
        //     catch (Exception)
        //     {

        //         throw;
        //     }
        // }
        public async Task<statuscheckmodel> UpdateSingleRfqItem(RfqItemModel model)
        {
            statuscheckmodel status = new statuscheckmodel();
            var data = obj.RFQItems.Where(x => x.RFQRevisionId == model.RFQRevisionId).Include(x => x.RFQRevision).FirstOrDefault<RFQItem>();
            try
            {
                if (data != null)
                {
                    data.HSNCode = model.HSNCode;
                    data.QuotationQty = model.QuotationQty;
                    data.VendorModelNo = model.VendorModelNo;
                    data.RequestRemarks = model.RequsetRemarks;
                    obj.SaveChanges();
                }
                return status;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public statuscheckmodel DeleteRfqRevisionbyId(int id)
        {
            statuscheckmodel staus = new statuscheckmodel();
            try
            {
                var data = obj.RFQRevisions.Where(x => x.rfqRevisionId == id && x.DeleteFlag == false).Select(x => x).SingleOrDefault();
                if (data != null)
                {
                    data.DeleteFlag = true;
                    obj.SaveChanges();
                }
                return staus;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public statuscheckmodel DeleteRfqItemById(int id)
        {
            statuscheckmodel status = new statuscheckmodel();
            try
            {
                var data = obj.RFQItems.Where(x => x.RFQRevisionId == id && x.DeleteFlag == false).Select(x => x).SingleOrDefault();
                if (data != null)
                {
                    data.DeleteFlag = true;
                    obj.SaveChanges();
                }
                return status;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public statuscheckmodel DeleteBulkItemsByItemId(List<int> id)
        {
            statuscheckmodel status = new statuscheckmodel();
            try
            {
                var data = obj.RFQItems.Where(x => id.Contains(x.RFQItemsId)).ToList();
                if (data != null)
                {
                    data.Select(x => x.DeleteFlag == true);
                    obj.SaveChanges();
                }
                return status;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //public statusmodel InsertDocument(RfqDocumentsModel model)
        //{
        //    if (true)
        //    {
        //        try
        //        {
        //            string PhotoPath = Convert.ToString(ConfigurationManager.AppSettings["ImagePath"]);
        //            RFQDocument newObj = new RFQDocument();
        //            newObj.DocumentName = model.DocumentName;
        //            newObj.DocumentType = model.DocumentType;
        //            newObj.Path = model.Path;
        //            newObj.UploadedBy = model.UploadedBy;
        //            newObj.uploadedDate = model.UploadedDate;
        //            newObj.Status = model.Status;
        //            newObj.StatusBy = model.StatusBy;
        //            newObj.StatusDate = model.Statusdate;

        //            if (String.IsNullOrEmpty(newObj.Path))
        //            {

        //            }
        //            else
        //            {
        //               // string startingFilePath = PhotoPath;

        //                string FilePath = SaveImage(newObj.Path, startingFilePath, newObj.DocumentName);

        //                FileInfo fInfo = new FileInfo(FilePath);

        //                newObj.Content = fInfo.Name;
        //            }



        //            return Request.CreateResponse(HttpStatusCode.Created, newArticle);
        //        }
        //        catch (Exception ex)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
        //        }
        //    }
        //    else
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
        //    }
        //}
        private string SaveImage(string base64, string FilePath, string ImageName)
        {
            //Get the file type to save in
            var FilePathWithExtension = "";
            string localBase64 = "";

            if (base64.Contains("data:image/jpeg;base64,"))
            {
                FilePathWithExtension = FilePath + ImageName + ".jpg";
                localBase64 = base64.Replace("data:image/jpeg;base64,", "");
            }
            else if (base64.Contains("data:image/png;base64,"))
            {
                FilePathWithExtension = FilePath + ImageName + ".png";
                localBase64 = base64.Replace("data:image/png;base64,", "");
            }
            else if (base64.Contains("data:image/bmp;base64"))
            {
                FilePathWithExtension = FilePath + ImageName + ".bmp";
                localBase64 = base64.Replace("data:image/bmp;base64", "");
            }
            else if (base64.Contains("data:application/msword;base64,"))
            {
                FilePathWithExtension = FilePath + ImageName + ".doc";
                localBase64 = base64.Replace("data:application/msword;base64,", "");
            }
            else if (base64.Contains("data:application/vnd.openxmlformats-officedocument.wordprocessingml.document;base64,"))
            {
                FilePathWithExtension = FilePath + ImageName + ".docx";
                localBase64 = base64.Replace("data:application/vnd.openxmlformats-officedocument.wordprocessingml.document;base64,", "");
            }
            else if (base64.Contains("data:application/pdf;base64,"))
            {
                FilePathWithExtension = FilePath + ImageName + ".pdf";
                localBase64 = base64.Replace("data:application/pdf;base64,", "");
            }

            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(localBase64)))
            {
                using (FileStream fs = new FileStream(FilePathWithExtension, FileMode.Create, FileAccess.Write))
                {
                    //Create the specified directory if it does not exist
                    var photofolder = System.IO.Path.GetDirectoryName(FilePathWithExtension);
                    if (!Directory.Exists(photofolder))
                    {
                        Directory.CreateDirectory(photofolder);
                    }

                    ms.WriteTo(fs);
                    fs.Close();
                    ms.Close();
                }
            }

            return FilePathWithExtension;
        }
        public statuscheckmodel CommunicationAdd(RfqCommunicationModel model)
        {
            statuscheckmodel status = new statuscheckmodel();
            var revision = obj.RFQRevisions.Where(x => x.rfqRevisionId == model.RfqRevision.RfqRevisionId && x.DeleteFlag == false).Include(x => x.RFQItems).SingleOrDefault();
            var item = revision.RFQItems.Where(x => x.RFQItemsId == model.RfqItem.RFQItemID && x.DeleteFlag == false).Select(x => x);
            // var result=from x in obj.MPRRevisions where x.RevisionId==model.re
            if (item != null)
            {
                RFQCommunication communication = new RFQCommunication();
                communication.RemarksFrom = model.RemarksFrom;
                communication.RemarksTo = model.RemarksTo;
                communication.ReminderDate = model.ReminderDate;
                communication.SendEmail = model.SendEmail;
                communication.SetReminder = model.SetReminder;
                communication.RemarksDate = model.RemarksDate;
                communication.Remarks = model.Remarks;
                communication.RfqItemsId = model.RfqItem.RFQItemID;
                communication.RfqRevisionId = model.RfqRevision.RfqRevisionId;

                obj.RFQCommunications.Add(communication);
                obj.SaveChanges();
            }
            status.Sid = model.Rfqccid;
            //status.StatusMesssage = model.StatusMesssage;
            return status;
        }
        public statuscheckmodel InsertDocument(RfqDocumentsModel model)
        {
            throw new NotImplementedException();
        }
        public async Task<RfqItemModel> GetItemsByItemId(int id)
        {
            RfqItemModel model = new RfqItemModel();
            try
            {
                var items = from x in obj.RFQItems where x.RFQItemsId == id && x.DeleteFlag == false select x;
                foreach (var item in items)
                {
                    model.HSNCode = item.HSNCode;
                    model.QuotationQty = item.QuotationQty;
                    model.VendorModelNo = item.VendorModelNo;
                    model.RequsetRemarks = item.RequestRemarks;
                }
                return model;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<VendormasterModel> GetAllvendorList()
        {
            List<VendormasterModel> vendor = new List<VendormasterModel>();
            try
            {
                var data = obj.VendorMasters.Where(x => x.Deleteflag == false).ToList();
                vendor = data.Select(x => new VendormasterModel()
                {
                    ContactNo = x.ContactNo,
                    Vendorid = x.Vendorid,
                    VendorCode = x.VendorCode,
                    VendorName = x.VendorName,

                }).ToList();
                return vendor;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<VendormasterModel> GetvendorById(int id)
        {
            VendormasterModel vendor = new VendormasterModel();
            try
            {
                var data = obj.VendorMasters.Where(x => x.Vendorid == id && x.Deleteflag == false).SingleOrDefault();
                if (data != null)
                {
                    vendor.ContactNo = data.ContactNo;
                    vendor.Emailid = data.Emailid;
                    vendor.VendorCode = data.VendorCode;
                    vendor.VendorName = data.VendorName;
                }
                return vendor;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<statuscheckmodel> InsertVendorterms(VendorRfqtermModel vendor)
        {
            statuscheckmodel status = new statuscheckmodel();
            try
            {
                if (vendor != null)
                {
                    var data = new VendorRFQTerm();
                    data.VendorTermsid = vendor.VendorTermsid;
                    data.VendorID = vendor.VendorID;
                    data.Terms = vendor.Terms;
                    data.Indexno = vendor.Indexno;
                    obj.VendorRFQTerms.Add(data);
                    obj.SaveChanges();
                }
                return status;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<statuscheckmodel> UpdateRfqStatus(int id)
        {
            statuscheckmodel status = new statuscheckmodel();
            try
            {
                var data = obj.RFQStatus.Select(x => x.RfqStatusId).ToList();

                return status;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public Task<List<RfqRevisionModel>> GetAllRFQs()
        {
            throw new NotImplementedException();
        }
        public async Task<statuscheckmodel> InsertRfqItemInfo(RfqItemModel model)
        {
            statuscheckmodel status = new statuscheckmodel();
            try
            {
                //for remoteserver
                if (model != null)
                {
                    vscm.Database.Connection.Open();
                    var Remotedata = new RemoteRFQItem();
                    var rfqRemoteitem = from x in vscm.RemoteRFQItems where x.RFQItemsId == model.RFQItemID select x;
                    Remotedata.HSNCode = model.HSNCode;
                    Remotedata.QuotationQty = model.QuotationQty;
                    Remotedata.VendorModelNo = model.VendorModelNo;
                    vscm.RemoteRFQItems.Add(Remotedata);
                    vscm.SaveChanges();
                    foreach (var item in model.iteminfo)
                    {
                        var remoteinfo = new RemoteRFQItemsInfo()
                        {
                            RFQItemsId = item.RFQItemsId,
                            Qty = item.Qunatity,
                            UOM = item.UOM,
                            UnitPrice = item.UnitPrice,
                            DiscountPercentage = item.DiscountPercentage,
                            Discount = item.Discount,
                            CurrencyId = item.CurrencyID,
                            CurrencyValue = item.CurrencyValue,
                            Remarks = item.Remarks,
                            DeliveryDate = item.DeliveryDate,
                            GSTPercentage=item.GSTPercentage,
                            SyncDate = System.DateTime.Now
                        };
                        Remotedata.RemoteRFQItemsInfoes.Add(remoteinfo);
                        obj.SaveChanges();
                    }
                }
                vscm.Database.Connection.Close();
                ///for local datbase
                var data = new RFQItem();
                obj.Database.Connection.Open();
                var rfqitem = from x in obj.RFQItems where x.RFQItemsId == model.RFQItemID select x;
                data.HSNCode = model.HSNCode;
                data.QuotationQty = model.QuotationQty;
                data.VendorModelNo = model.VendorModelNo;
                obj.RFQItems.Add(data);
                obj.SaveChanges();
                foreach (var item in model.iteminfo)
                {
                    var info = new RFQItemsInfo()
                    {
                        RFQItemsId = item.RFQItemsId,
                        Qty = item.Qunatity,
                        UOM = item.UOM,
                        UnitPrice = item.UnitPrice,
                        DiscountPercentage = item.DiscountPercentage,
                        GSTPercentage=item.GSTPercentage,
                        Discount = item.Discount,
                        CurrencyId = item.CurrencyID,
                        CurrencyValue = item.CurrencyValue,
                        Remarks = item.Remarks,
                        DeliveryDate = item.DeliveryDate
                    };
                    data.RFQItemsInfoes.Add(info);
                    obj.SaveChanges();
                }
                obj.Database.Connection.Close();

                return status;
            } 
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<statuscheckmodel> DeleteRfqIteminfoByid(List<int> id)
        {
            statuscheckmodel status = new statuscheckmodel();
            try
            {
                vscm.Database.Connection.Open();
                var Remotedata = vscm.RemoteRFQItemsInfoes.Where(x => id.Contains(x.RFQSplitItemId) && x.DeleteFlag == false).FirstOrDefault();
                if (Remotedata != null)
                {
                    Remotedata.DeleteFlag = true;
                    vscm.SaveChanges();
                }
                vscm.Database.Connection.Close();

                obj.Database.Connection.Open();
                var Localdata = obj.RFQItemsInfoes.Where(x => id.Contains(x.RFQSplitItemId) && x.DeleteFlag == false).FirstOrDefault();
                if (Localdata != null)
                {
                    Localdata.DeleteFlag = true;
                    obj.SaveChanges();
                }
                status.Sid = Localdata.RFQItemsId;
                return status;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<statuscheckmodel> DeleteRfqitemandinfosById(int id)
        {
            statuscheckmodel status = new statuscheckmodel();
            try
            {
                //remotedata
                vscm.Database.Connection.Open();
                var Remotedata = vscm.RemoteRFQItems.Where(x => x.RFQItemsId == id && x.DeleteFlag == false).FirstOrDefault();
                if (Remotedata != null)
                {
                    Remotedata.DeleteFlag = true;
                    vscm.SaveChanges();

                    var remoteitemsdata = vscm.RemoteRFQItemsInfoes.Where(x => x.RFQItemsId == Remotedata.RFQItemsId && x.DeleteFlag == false).ToList();
                    if (remoteitemsdata != null)
                    {
                        foreach (var item in remoteitemsdata)
                        {
                            item.DeleteFlag = true;
                            vscm.SaveChanges();
                        }
                    }
                    //int Itemid = Remotedata.RFQItemsId;
                }
                vscm.Database.Connection.Close();

                //localdata
                obj.Database.Connection.Open();
                var Localdata = obj.RFQItems.Where(x => x.RFQItemsId == id && x.DeleteFlag == false).FirstOrDefault();
                if (Localdata != null)
                {
                    Localdata.DeleteFlag = true;
                    obj.SaveChanges();

                    var localitemsdata = obj.RFQItemsInfoes.Where(x => x.RFQItemsId == Remotedata.RFQItemsId && x.DeleteFlag == false).ToList();
                    if (localitemsdata != null)
                    {
                        foreach (var item in localitemsdata)
                        {
                            item.DeleteFlag = true;
                            obj.SaveChanges();
                        }
                    }
                }
                int Itemid = Localdata.RFQItemsId;
                obj.Database.Connection.Close();
                status.Sid = Itemid;
                return status;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<statuscheckmodel> UpdateRfqItemInfoById(RfqItemInfoModel model)
        {
            statuscheckmodel status = new statuscheckmodel();
            try
            {
                //remote data
                var remoteitem = vscm.RemoteRFQItemsInfoes.Where(x => x.RFQItemsId == model.RFQItemsId).FirstOrDefault();
                remoteitem.Discount = model.Discount;
                remoteitem.DiscountPercentage = model.DiscountPercentage;
                remoteitem.Qty = model.Qunatity;
                remoteitem.UnitPrice = model.UnitPrice;
                remoteitem.UOM = model.UOM;
                remoteitem.CurrencyValue = model.CurrencyValue;
                remoteitem.CurrencyId = model.CurrencyID;
                remoteitem.Remarks = model.Remarks;
                remoteitem.DeliveryDate = model.DeliveryDate;
                remoteitem.GSTPercentage = model.GSTPercentage;

                vscm.RemoteRFQItemsInfoes.Add(remoteitem);
                vscm.SaveChanges();
                int remoteitemid = remoteitem.RFQItemsId;
                vscm.Database.Connection.Close();

                obj.Database.Connection.Open();
                var localitem = obj.RFQItemsInfoes.Where(x => x.RFQItemsId == model.RFQItemsId).FirstOrDefault();
                localitem.Discount = model.Discount;
                localitem.DiscountPercentage = model.DiscountPercentage;
                localitem.Qty = model.Qunatity;
                localitem.UnitPrice = model.UnitPrice;
                localitem.UOM = model.UOM;
                localitem.CurrencyValue = model.CurrencyValue;
                localitem.GSTPercentage = model.GSTPercentage;
                localitem.CurrencyId = model.CurrencyID;
                localitem.Remarks = model.Remarks;
                localitem.DeliveryDate = model.DeliveryDate;
                obj.RFQItemsInfoes.Add(localitem);
                obj.SaveChanges();
                return status;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<RfqItemModel> GetRfqItemByMPrId(int id)
        {
            RfqItemModel itemmodel = new RfqItemModel();
            try
            {
                vscm.Database.Connection.Open();
                RfqItemInfoModel iteminfo = new RfqItemInfoModel();
                var itemdetails = vscm.RemoteRFQItems.Where(x => x.MPRItemDetailsid == id).Include(x => x.RemoteRFQItemsInfoes).FirstOrDefault();
                itemmodel.HSNCode = itemdetails.HSNCode;
                itemmodel.RFQItemID = itemdetails.RFQItemsId;
                itemmodel.QuotationQty = itemdetails.QuotationQty;
                itemmodel.VendorModelNo = itemdetails.VendorModelNo;
                itemmodel.RequsetRemarks = itemdetails.RequestRemarks;
                foreach (var item in itemdetails.RemoteRFQItemsInfoes)
                {
                    iteminfo.RFQItemsId = item.RFQItemsId;
                    iteminfo.RFQSplitItemId = item.RFQSplitItemId;
                    iteminfo.UnitPrice = item.UnitPrice;
                    iteminfo.UOM = item.UOM;
                    iteminfo.Remarks = item.Remarks;
                    iteminfo.DiscountPercentage = item.DiscountPercentage;
                    iteminfo.Discount = item.Discount;
                    iteminfo.DeliveryDate = item.DeliveryDate;
                    iteminfo.CurrencyID = item.CurrencyId;
                    iteminfo.CurrencyValue = item.CurrencyValue;
                    itemmodel.iteminfo.Add(iteminfo);
                }
                return itemmodel;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<RfqRevisionModel> GetRfqDetailsById(int revisionId)
        {
            RfqRevisionModel revision = new RfqRevisionModel();
            try
            {
                var localrevision = obj.RFQRevisions.Where(x => x.rfqRevisionId == revisionId && x.DeleteFlag == false).Include(x => x.RFQMaster).Include(x => x.RFQItems).FirstOrDefault();
                if (localrevision != null)
                {
                    revision.RfqMasterId = localrevision.rfqMasterId;
                    revision.RfqRevisionNo = localrevision.RevisionNo;
                    revision.CreatedBy = localrevision.CreatedBy;
                    revision.CreatedDate = localrevision.CreatedDate;
                    revision.PackingForwading = localrevision.PackingForwarding;
                    revision.salesTax = localrevision.SalesTax;
                    revision.Insurance = localrevision.Insurance;
                    revision.CustomsDuty = localrevision.CustomsDuty;
                    revision.PaymentTermDays = localrevision.PaymentTermDays;
                    revision.PaymentTermRemarks = localrevision.PaymentTermRemarks;
                    revision.BankGuarantee = localrevision.BankGuarantee;
                    revision.DeliveryMaxWeeks = localrevision.DeliveryMaxWeeks;
                    revision.DeliveryMinWeeks = localrevision.DeliveryMinWeeks;
                }

                //var rfqitemss = obj.RFQItems.Where(x => x.RFQRevisionId == revisionId).GroupBy(x => x.RFQRevisionId).Select(x => x.Last());
                var rfqitemss = localrevision.RFQItems.GroupBy(x => x.RFQRevisionId).Select(x => x.Last());
                foreach (var item in rfqitemss)
                {
                    //revision.rfqitem= localrevision.RFQItems.Select(x => new RfqItemModel()
                    //  {
                    //      HSNCode = item.HSNCode,
                    //      RFQItemID = item.RFQItemID,
                    //      RFQRevisionId = item.RFQRevisionId,
                    //      QuotationQty = item.QuotationQty,
                    //      VendorModelNo = item.VendorModelNo,
                    //      RequsetRemarks = item.RequsetRemarks
                    //  }).ToList();
                    RfqItemModel rfqitems = new RfqItemModel();
                    rfqitems.HSNCode = item.HSNCode;
                    rfqitems.QuotationQty = item.QuotationQty;
                    rfqitems.RFQRevisionId = item.RFQRevisionId;
                    rfqitems.RFQItemID = item.RFQItemsId;
                    revision.rfqitem.Add(rfqitems);
                }

                return revision;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<statuscheckmodel> InsertSingleIteminfos(RfqItemInfoModel model)
        {
            statuscheckmodel status = new statuscheckmodel();
            try
            {
                vscm.Database.Connection.Open();
                var remoteiteminfo = new RemoteRFQItemsInfo();
                remoteiteminfo.UOM = model.UOM;
                remoteiteminfo.UnitPrice = model.UnitPrice;
                remoteiteminfo.RFQItemsId = model.RFQItemsId;
                remoteiteminfo.GSTPercentage = model.GSTPercentage;
                remoteiteminfo.DiscountPercentage = model.DiscountPercentage;
                remoteiteminfo.Qty = model.Qunatity;
                remoteiteminfo.DeliveryDate = model.DeliveryDate;
                remoteiteminfo.CurrencyValue = model.CurrencyValue;
                remoteiteminfo.CurrencyId = model.CurrencyID;

                vscm.RemoteRFQItemsInfoes.Add(remoteiteminfo);
                vscm.SaveChanges();
                int Remotesplitid = remoteiteminfo.RFQSplitItemId;
                vscm.Database.Connection.Close();

                obj.Database.Connection.Open();
                var localiteminfo = new RFQItemsInfo();
                localiteminfo.UOM = model.UOM;
                localiteminfo.UnitPrice = model.UnitPrice;
                localiteminfo.RFQItemsId = model.RFQItemsId;
                localiteminfo.GSTPercentage = model.GSTPercentage;
                localiteminfo.DiscountPercentage = model.DiscountPercentage;
                localiteminfo.Qty = model.Qunatity;
                localiteminfo.DeliveryDate = model.DeliveryDate;
                localiteminfo.CurrencyValue = model.CurrencyValue;
                localiteminfo.CurrencyId = model.CurrencyID;
                obj.RFQItemsInfoes.Add(localiteminfo);
                obj.SaveChanges();
                int localsplitid = localiteminfo.RFQSplitItemId;
                status.Sid = localsplitid;
                return status;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<statuscheckmodel> InsertBulkItemInfos(List<RfqItemInfoModel> model)
        {
            statuscheckmodel status = new statuscheckmodel();
            try
            {
                if (model!=null)
                {
                    vscm.Database.Connection.Open();
                    var remoteiteminfo = new RemoteRFQItemsInfo();
                    foreach (var item in model)
                    {
                        remoteiteminfo.UOM = item.UOM;
                        remoteiteminfo.UnitPrice = item.UnitPrice;
                        remoteiteminfo.RFQItemsId = item.RFQItemsId;
                        remoteiteminfo.GSTPercentage = item.GSTPercentage;
                        remoteiteminfo.DiscountPercentage = item.DiscountPercentage;
                        remoteiteminfo.Qty = item.Qunatity;
                        remoteiteminfo.DeliveryDate = item.DeliveryDate;
                        remoteiteminfo.CurrencyValue = item.CurrencyValue;
                        remoteiteminfo.CurrencyId = item.CurrencyID;
                        vscm.RemoteRFQItemsInfoes.Add(remoteiteminfo);
                        vscm.SaveChanges();
                    }
                   // vscm.SaveChanges();
                }
                vscm.Database.Connection.Close();
                if (model!=null)
                {
                    obj.Database.Connection.Open();
                    var localiteminfo = new RFQItemsInfo();
                    foreach (var item in model)
                    {
                        localiteminfo.UOM = item.UOM;
                        localiteminfo.UnitPrice = item.UnitPrice;
                        localiteminfo.RFQItemsId = item.RFQItemsId;
                        localiteminfo.GSTPercentage = item.GSTPercentage;
                        localiteminfo.DiscountPercentage = item.DiscountPercentage;
                        localiteminfo.Qty = item.Qunatity;
                        localiteminfo.DeliveryDate = item.DeliveryDate;
                        localiteminfo.CurrencyValue = item.CurrencyValue;
                        localiteminfo.CurrencyId = item.CurrencyID;
                        obj.RFQItemsInfoes.Add(localiteminfo);
                        obj.SaveChanges();
                    }
                }
                return status;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

