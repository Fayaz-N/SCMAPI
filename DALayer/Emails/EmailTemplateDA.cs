﻿using SCMModels.SCMModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using SCMModels;
using System.Text.RegularExpressions;
using SCMModels.RemoteModel;

namespace DALayer.Emails
{
	public class EmailTemplateDA : IEmailTemplateDA
	{
		public bool prepareMPREmailTemplate(string typeOfUser, int revisionId, string FrmEmailId, string ToEmailId, string Remarks)
		{

			try
			{
				using (var db = new YSCMEntities()) //ok
				{

					var ipaddress = ConfigurationManager.AppSettings["UI_IpAddress"];
					MPRRevisionDetail mprrevisionDetails = db.MPRRevisionDetails.Where(li => li.RevisionId == revisionId).FirstOrDefault<MPRRevisionDetail>();
					ipaddress = ipaddress + "SCM/MPRForm/" + mprrevisionDetails.RevisionId + "";
					var issueOfPurpose = mprrevisionDetails.IssuePurposeId == 1 ? "For Enquiry" : "For Issuing PO";
					EmailSend emlSndngList = new EmailSend();
					if (typeOfUser == "")
					{
						emlSndngList.Body = "<html><meta charset=\"ISO-8859-1\"><head><link rel = 'stylesheet' href = 'https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css' ></head><body><div class='container'><table border='1' class='table table-bordered table-sm'><tr><td><b>MPR Number</b></td><td>" + mprrevisionDetails.DocumentNo + "</td><td><b>Document Description</b></td><td>" + mprrevisionDetails.DocumentDescription + "</td><td><b>Purpose of Iussing MPR</b></td><td>" + issueOfPurpose + "</td></tr><tr><td><b>Department</b></td><td>" + mprrevisionDetails.DepartmentName + "</td><td><b>Project Manager</td></td><td>" + mprrevisionDetails.ProjectManagerName + "</td><td><b>Job Code</b></td><td>" + mprrevisionDetails.JobCode + "</td></tr><tr><td><b>Client Name</b></td><td>" + mprrevisionDetails.ClientName + "</td><td><b>Job Name</b></td><td>" + mprrevisionDetails.JobName + "</td><td><b>Buyer Group</b></td><td>" + mprrevisionDetails.BuyerGroupName + "</td></tr><tr><td><b>Checker Name</b></td><td>" + mprrevisionDetails.CheckedName + "</td><td><b>Checker Status</b></td><td>" + mprrevisionDetails.CheckStatus + "</td><td><b>Checker Remarks</b></td><td>" + mprrevisionDetails.CheckerRemarks + "</td></tr><tr><td><b>Approver Name</b></td><td>" + mprrevisionDetails.ApproverName + "</td><td><b>Approver Status</b></td><td>" + mprrevisionDetails.ApprovalStatus + "</td><td><b>Approver Remarks</b></td><td>" + mprrevisionDetails.ApproverRemarks + "</td></tr></table><br/><br/><span><b>Remarks : <b/>" + Remarks + "</span><br/><br/><b>Click here to redirect : </b>&nbsp<a href='" + ipaddress + "'>" + ipaddress + " </a></div></body></html>";
						Employee frmEmail = db.Employees.Where(li => li.EmployeeNo == FrmEmailId).FirstOrDefault<Employee>();
						emlSndngList.FrmEmailId = frmEmail.EMail;
						emlSndngList.Subject = "Comments From " + frmEmail.Name;
						//emlSndngList.ToEmailId = "Developer@in.yokogawa.com";
						emlSndngList.ToEmailId = (db.Employees.Where(li => li.EmployeeNo == ToEmailId).FirstOrDefault<Employee>()).EMail;
						if (emlSndngList.FrmEmailId != "NULL" && emlSndngList.ToEmailId != "NULL")
							this.sendEmail(emlSndngList);
					}
					else
					{
						emlSndngList.Body = "<html><meta charset=\"ISO-8859-1\"><head><link rel = 'stylesheet' href = 'https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css' ></head><body><div class='container'><table border='1' class='table table-bordered table-sm'><tr><td><b>MPR Number</b></td><td>" + mprrevisionDetails.DocumentNo + "</td><td><b>Document Description</b></td><td>" + mprrevisionDetails.DocumentDescription + "</td><td><b>Purpose of Iussing MPR</b></td><td>" + issueOfPurpose + "</td></tr><tr><td><b>Department</b></td><td>" + mprrevisionDetails.DepartmentName + "</td><td><b>Project Manager</td></td><td>" + mprrevisionDetails.ProjectManagerName + "</td><td><b>Job Code</b></td><td>" + mprrevisionDetails.JobCode + "</td></tr><tr><td><b>Client Name</b></td><td>" + mprrevisionDetails.ClientName + "</td><td><b>Job Name</b></td><td>" + mprrevisionDetails.JobName + "</td><td><b>Buyer Group</b></td><td>" + mprrevisionDetails.BuyerGroupName + "</td></tr><tr><td><b>Checker Name</b></td><td>" + mprrevisionDetails.CheckedName + "</td><td><b>Checker Status</b></td><td>" + mprrevisionDetails.CheckStatus + "</td><td><b>Checker Remarks</b></td><td>" + mprrevisionDetails.CheckerRemarks + "</td></tr><tr><td><b>Approver Name</b></td><td>" + mprrevisionDetails.ApproverName + "</td><td><b>Approver Status</b></td><td>" + mprrevisionDetails.ApprovalStatus + "</td><td><b>Approver Remarks</b></td><td>" + mprrevisionDetails.ApproverRemarks + "</td></tr></table><br/><br/><b>Click here to redirect : </b>&nbsp<a href='" + ipaddress + "'>" + ipaddress + "</a></div></body></html>";
						if (typeOfUser == "Requestor")
						{
							var res = db.Employees.Where(li => li.EmployeeNo == mprrevisionDetails.PreparedBy).FirstOrDefault<Employee>();
							emlSndngList.FrmEmailId = (db.Employees.Where(li => li.EmployeeNo == mprrevisionDetails.PreparedBy).FirstOrDefault<Employee>()).EMail;
							emlSndngList.Subject = "MPR Information: " + mprrevisionDetails.DocumentNo + " ; " + "Approver Status: " + mprrevisionDetails.ApprovalStatus;
							if (mprrevisionDetails.CheckedBy != "-" && mprrevisionDetails.CheckedBy != "")
							{
								emlSndngList.ToEmailId = (db.Employees.Where(li => li.EmployeeNo == mprrevisionDetails.CheckedBy).FirstOrDefault<Employee>()).EMail;
								if ((!string.IsNullOrEmpty(emlSndngList.FrmEmailId) && !string.IsNullOrEmpty(emlSndngList.FrmEmailId)) && (emlSndngList.FrmEmailId != "NULL" && emlSndngList.ToEmailId != "NULL"))
									this.sendEmail(emlSndngList);
							}
							//emlSndngList.Subject = "MPR Information: " + mprrevisionDetails.DocumentNo + " ; " + "Checker Status: " + mprrevisionDetails.CheckStatus;
							//if (mprrevisionDetails.ApprovedBy != "-" && mprrevisionDetails.ApprovedBy != "")
							//{
							//    emlSndngList.ToEmailId = (db.Employees.Where(li => li.EmployeeNo == mprrevisionDetails.ApprovedBy).FirstOrDefault<Employee>()).EMail;
							//    if (emlSndngList.FrmEmailId != "NULL" && emlSndngList.ToEmailId != "NULL")
							//        this.sendEmail(emlSndngList);
							//}
						}
						if (typeOfUser == "Checker")
						{
							//emlSndngList.FrmEmailId = "Developer@in.yokogawa.com";
							//emlSndngList.ToEmailId = "Developer@in.yokogawa.com";
							emlSndngList.FrmEmailId = (db.Employees.Where(li => li.EmployeeNo == mprrevisionDetails.CheckedBy).FirstOrDefault<Employee>()).EMail;
							emlSndngList.Subject = "MPR Information: " + mprrevisionDetails.DocumentNo + " ; " + "Checker Status: " + mprrevisionDetails.CheckStatus + " ; " + "Approver Status: " + mprrevisionDetails.ApprovalStatus;
							emlSndngList.ToEmailId = (db.Employees.Where(li => li.EmployeeNo == mprrevisionDetails.PreparedBy).FirstOrDefault<Employee>()).EMail;
							if ((!string.IsNullOrEmpty(emlSndngList.FrmEmailId) && !string.IsNullOrEmpty(emlSndngList.FrmEmailId)) && (emlSndngList.FrmEmailId != "NULL" && emlSndngList.ToEmailId != "NULL"))
								this.sendEmail(emlSndngList);
							if (mprrevisionDetails.CheckStatus == "Approved")
							{
								emlSndngList.Subject = "MPR Information: " + mprrevisionDetails.DocumentNo + " ; " + "Checker Status: " + mprrevisionDetails.CheckStatus;
								if (mprrevisionDetails.ApprovedBy != "-" && mprrevisionDetails.ApprovedBy != "")
								{
									emlSndngList.ToEmailId = (db.Employees.Where(li => li.EmployeeNo == mprrevisionDetails.ApprovedBy).FirstOrDefault<Employee>()).EMail;
									if ((!string.IsNullOrEmpty(emlSndngList.FrmEmailId) && !string.IsNullOrEmpty(emlSndngList.FrmEmailId)) && (emlSndngList.FrmEmailId != "NULL" && emlSndngList.ToEmailId != "NULL"))
										this.sendEmail(emlSndngList);
								}
							}
						}
						if (typeOfUser == "Approver")
						{
							emlSndngList.FrmEmailId = (db.Employees.Where(li => li.EmployeeNo == mprrevisionDetails.ApprovedBy).FirstOrDefault<Employee>()).EMail;
							emlSndngList.Subject = "MPR Information: " + mprrevisionDetails.DocumentNo + " ; " + "Checker Status: " + mprrevisionDetails.CheckStatus + " ; " + "Approver Status: " + mprrevisionDetails.ApprovalStatus;
							emlSndngList.ToEmailId = (db.Employees.Where(li => li.EmployeeNo == mprrevisionDetails.PreparedBy).FirstOrDefault<Employee>()).EMail;
							if ((!string.IsNullOrEmpty(emlSndngList.FrmEmailId) && !string.IsNullOrEmpty(emlSndngList.FrmEmailId)) && (emlSndngList.FrmEmailId != "NULL" && emlSndngList.ToEmailId != "NULL"))
								this.sendEmail(emlSndngList);
							emlSndngList.Subject = "MPR Information: " + mprrevisionDetails.DocumentNo + " ; " + "Approver Status: " + mprrevisionDetails.ApprovalStatus;
							emlSndngList.ToEmailId = (db.Employees.Where(li => li.EmployeeNo == mprrevisionDetails.CheckedBy).FirstOrDefault<Employee>()).EMail;
							if ((!string.IsNullOrEmpty(emlSndngList.FrmEmailId) && !string.IsNullOrEmpty(emlSndngList.FrmEmailId)) && (emlSndngList.FrmEmailId != "NULL" && emlSndngList.ToEmailId != "NULL"))
								this.sendEmail(emlSndngList);
							if (mprrevisionDetails.ApprovalStatus == "Approved")
							{
								emlSndngList.Subject = "MPR Information: " + mprrevisionDetails.DocumentNo + " ; " + "Checker Status: " + mprrevisionDetails.CheckStatus + " ; " + "Approver Status: " + mprrevisionDetails.ApprovalStatus;
								if (mprrevisionDetails.SecondApprover != "-" && mprrevisionDetails.SecondApprover != "")
								{
									emlSndngList.ToEmailId = (db.Employees.Where(li => li.EmployeeNo == mprrevisionDetails.SecondApprover).FirstOrDefault<Employee>()).EMail;
									if ((!string.IsNullOrEmpty(emlSndngList.FrmEmailId) && !string.IsNullOrEmpty(emlSndngList.FrmEmailId)) && (emlSndngList.FrmEmailId != "NULL" && emlSndngList.ToEmailId != "NULL"))
										this.sendEmail(emlSndngList);
								}
								else
								{
									emlSndngList.Subject = "New MPR is submitted for acknowledgement - " + mprrevisionDetails.DocumentNo + "";
									var TooEmailId = db.MPRBuyerGroups.Where(li => li.BuyerGroupId == mprrevisionDetails.BuyerGroupId).FirstOrDefault().BuyerManager;
									emlSndngList.ToEmailId = (db.Employees.Where(li => li.EmployeeNo == TooEmailId).FirstOrDefault<Employee>()).EMail;
									if ((!string.IsNullOrEmpty(emlSndngList.FrmEmailId) && !string.IsNullOrEmpty(emlSndngList.FrmEmailId)) && (emlSndngList.FrmEmailId != "NULL" && emlSndngList.ToEmailId != "NULL"))
										this.sendEmail(emlSndngList);
								}
							}
						}
						if (typeOfUser == "SecondApprover")
						{
							emlSndngList.FrmEmailId = (db.Employees.Where(li => li.EmployeeNo == mprrevisionDetails.SecondApprover).FirstOrDefault<Employee>()).EMail;
							emlSndngList.Subject = "MPR Information: " + mprrevisionDetails.DocumentNo + " ; " + "Checker Status: " + mprrevisionDetails.CheckStatus + " ; " + "Approver Status: " + mprrevisionDetails.ApprovalStatus;

							emlSndngList.ToEmailId = (db.Employees.Where(li => li.EmployeeNo == mprrevisionDetails.PreparedBy).FirstOrDefault<Employee>()).EMail;
							if ((!string.IsNullOrEmpty(emlSndngList.FrmEmailId) && !string.IsNullOrEmpty(emlSndngList.FrmEmailId)) && (emlSndngList.FrmEmailId != "NULL" && emlSndngList.ToEmailId != "NULL"))
								this.sendEmail(emlSndngList);

							emlSndngList.Subject = "MPR Information: " + mprrevisionDetails.DocumentNo + " ; " + "Approver Status: " + mprrevisionDetails.ApprovalStatus;
							emlSndngList.ToEmailId = (db.Employees.Where(li => li.EmployeeNo == mprrevisionDetails.CheckedBy).FirstOrDefault<Employee>()).EMail;
							if ((!string.IsNullOrEmpty(emlSndngList.FrmEmailId) && !string.IsNullOrEmpty(emlSndngList.FrmEmailId)) && (emlSndngList.FrmEmailId != "NULL" && emlSndngList.ToEmailId != "NULL"))
								this.sendEmail(emlSndngList);

							emlSndngList.Subject = "MPR Information: " + mprrevisionDetails.DocumentNo + " ; " + "Checker Status: " + mprrevisionDetails.CheckStatus + " ; " + "Approver Status: " + mprrevisionDetails.ApprovalStatus;
							emlSndngList.ToEmailId = (db.Employees.Where(li => li.EmployeeNo == mprrevisionDetails.ApprovedBy).FirstOrDefault<Employee>()).EMail;
							if ((!string.IsNullOrEmpty(emlSndngList.FrmEmailId) && !string.IsNullOrEmpty(emlSndngList.FrmEmailId)) && (emlSndngList.FrmEmailId != "NULL" && emlSndngList.ToEmailId != "NULL")) if (emlSndngList.FrmEmailId != "NULL" && emlSndngList.ToEmailId != "NULL")
									this.sendEmail(emlSndngList);

							if (mprrevisionDetails.SecondApproversStatus == "Approved")
							{
								emlSndngList.Subject = "MPR Information: " + mprrevisionDetails.DocumentNo + " ; " + "Checker Status: " + mprrevisionDetails.CheckStatus + " ; " + "Approver Status: " + mprrevisionDetails.ApprovalStatus + " ; " + "Second Approval Status: " + mprrevisionDetails.SecondApproversStatus;
								if (mprrevisionDetails.ThirdApprover != "-" && mprrevisionDetails.ThirdApprover != "")
								{
									emlSndngList.ToEmailId = (db.Employees.Where(li => li.EmployeeNo == mprrevisionDetails.ThirdApprover).FirstOrDefault<Employee>()).EMail;
									if ((!string.IsNullOrEmpty(emlSndngList.FrmEmailId) && !string.IsNullOrEmpty(emlSndngList.FrmEmailId)) && (emlSndngList.FrmEmailId != "NULL" && emlSndngList.ToEmailId != "NULL"))
										this.sendEmail(emlSndngList);
								}
								else
								{
									emlSndngList.Subject = "New MPR is submitted for acknowledgement - " + mprrevisionDetails.DocumentNo + "";
									emlSndngList.ToEmailId = db.MPRBuyerGroups.Where(li => li.BuyerGroupId == mprrevisionDetails.BuyerGroupId).FirstOrDefault().BuyerManager;
									if ((!string.IsNullOrEmpty(emlSndngList.FrmEmailId) && !string.IsNullOrEmpty(emlSndngList.FrmEmailId)) && (emlSndngList.FrmEmailId != "NULL" && emlSndngList.ToEmailId != "NULL"))
										this.sendEmail(emlSndngList);
								}
							}
						}
						if (typeOfUser == "ThirdApprover")
						{
							emlSndngList.FrmEmailId = (db.Employees.Where(li => li.EmployeeNo == mprrevisionDetails.ThirdApprover).FirstOrDefault<Employee>()).EMail;
							emlSndngList.Subject = "MPR Information: " + mprrevisionDetails.DocumentNo + " ; " + "Checker Status: " + mprrevisionDetails.CheckStatus + " ; " + "Approver Status: " + mprrevisionDetails.ApprovalStatus;
							emlSndngList.ToEmailId = (db.Employees.Where(li => li.EmployeeNo == mprrevisionDetails.PreparedBy).FirstOrDefault<Employee>()).EMail;
							if ((!string.IsNullOrEmpty(emlSndngList.FrmEmailId) && !string.IsNullOrEmpty(emlSndngList.FrmEmailId)) && (emlSndngList.FrmEmailId != "NULL" && emlSndngList.ToEmailId != "NULL"))
								this.sendEmail(emlSndngList);

							emlSndngList.Subject = "MPR Information: " + mprrevisionDetails.DocumentNo + " ; " + "Approver Status: " + mprrevisionDetails.ApprovalStatus;
							emlSndngList.ToEmailId = (db.Employees.Where(li => li.EmployeeNo == mprrevisionDetails.CheckedBy).FirstOrDefault<Employee>()).EMail;
							if ((!string.IsNullOrEmpty(emlSndngList.FrmEmailId) && !string.IsNullOrEmpty(emlSndngList.FrmEmailId)) && (emlSndngList.FrmEmailId != "NULL" && emlSndngList.ToEmailId != "NULL"))
								this.sendEmail(emlSndngList);

							emlSndngList.Subject = "MPR Information: " + mprrevisionDetails.DocumentNo + " ; " + "Checker Status: " + mprrevisionDetails.CheckStatus + " ; " + "Approver Status: " + mprrevisionDetails.ApprovalStatus;
							emlSndngList.ToEmailId = (db.Employees.Where(li => li.EmployeeNo == mprrevisionDetails.ApprovedBy).FirstOrDefault<Employee>()).EMail;
							if ((!string.IsNullOrEmpty(emlSndngList.FrmEmailId) && !string.IsNullOrEmpty(emlSndngList.FrmEmailId)) && (emlSndngList.FrmEmailId != "NULL" && emlSndngList.ToEmailId != "NULL"))
								this.sendEmail(emlSndngList);


							emlSndngList.Subject = "MPR Information: " + mprrevisionDetails.DocumentNo + " ; " + "Checker Status: " + mprrevisionDetails.CheckStatus + " ; " + "Approver Status: " + mprrevisionDetails.ApprovalStatus + " ; " + "Second Approval Status: " + mprrevisionDetails.SecondApproversStatus;
							emlSndngList.ToEmailId = (db.Employees.Where(li => li.EmployeeNo == mprrevisionDetails.SecondApprover).FirstOrDefault<Employee>()).EMail;
							if ((!string.IsNullOrEmpty(emlSndngList.FrmEmailId) && !string.IsNullOrEmpty(emlSndngList.FrmEmailId)) && (emlSndngList.FrmEmailId != "NULL" && emlSndngList.ToEmailId != "NULL"))
								this.sendEmail(emlSndngList);
							if (mprrevisionDetails.ThirdApproverStatus == "Approved")
							{
								emlSndngList.Subject = "New MPR is submitted for acknowledgement - " + mprrevisionDetails.DocumentNo + "";
								emlSndngList.ToEmailId = db.MPRBuyerGroups.Where(li => li.BuyerGroupId == mprrevisionDetails.BuyerGroupId).FirstOrDefault().BuyerManager;
								if ((!string.IsNullOrEmpty(emlSndngList.FrmEmailId) && !string.IsNullOrEmpty(emlSndngList.FrmEmailId)) && (emlSndngList.FrmEmailId != "NULL" && emlSndngList.ToEmailId != "NULL"))
									this.sendEmail(emlSndngList);
							}
						}
					}

				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return true;

		}

		public bool prepareRFQGeneratedEmail(string FrmEmailId, int VendorId)
		{
			try
			{
				using (var db = new YSCMEntities()) //ok
				{
					var vscm = new VSCMEntities();
					var vendor = vscm.RemoteVendorUserMasters.Where(li => li.VendorId == VendorId).FirstOrDefault();
					var ipaddress = ConfigurationManager.AppSettings["UI_vendor_IpAddress"];
					EmailSend emlSndngList = new EmailSend();
					emlSndngList.Subject = "New RFQ Generated From YOKOGAWA";
					emlSndngList.Body = "<html><meta charset=\"ISO-8859-1\"><head><link rel = 'stylesheet' href = 'https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css' ></head><body><div class='container'><div>Dear Vendor, </div><br/><div>You have received new RFQ from Yokogawa</div><br/>The required portal details and the password is given below : <br /><br /> <b  style='color:#40bfbf;'>Click Here to Redirect : <a href='" + ipaddress + "'>" + ipaddress + "</a></b><br /> <br /> <b style='color:#40bfbf;'>User Name:</b> " + vendor.Vuserid + " <br /><b style='color:#40bfbf;'>Pass word:</b> " + vendor.pwd + "<br /><br/><div>Regards,<br/><div>CMM Department</div></body></html>";
					emlSndngList.FrmEmailId = (db.Employees.Where(li => li.EmployeeNo == FrmEmailId).FirstOrDefault<Employee>()).EMail;
					//emlSndngList.ToEmailId = "Developer@in.yokogawa.com";
					string emails = (db.VendorMasters.Where(li => li.Vendorid == VendorId).FirstOrDefault<VendorMaster>()).Emailid;
					List<string> emailList = emails.Split(',').ToList();
					foreach (var item in emailList)
					{
						emlSndngList.ToEmailId = item;
						if ((!string.IsNullOrEmpty(emlSndngList.FrmEmailId) && !string.IsNullOrEmpty(emlSndngList.FrmEmailId)) && (emlSndngList.FrmEmailId != "NULL" && emlSndngList.ToEmailId != "NULL"))
							this.sendEmail(emlSndngList);
					}

				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return true;
		}
		public bool prepareMPRStatusEmail(string FrmEmailId, string ToEmailId, string type, int revisionid)
		{
			try
			{
				using (var db = new YSCMEntities()) //ok
				{
					var ipaddress = ConfigurationManager.AppSettings["UI_IpAddress"];
					ipaddress = ipaddress + "SCM/MPRForm/" + revisionid + "";
					MPRRevisionDetail mprrevisionDetails = db.MPRRevisionDetails.Where(li => li.RevisionId == revisionid).FirstOrDefault<MPRRevisionDetail>();
					EmailSend emlSndngList = new EmailSend();
					string bodyTxt = "";
					if (type == "mprAssign")
					{
						emlSndngList.Subject = "MPR Assigned";
						bodyTxt = "MPR No:" + mprrevisionDetails.DocumentNo + " is assigned to you";
					}
					else
					{
						emlSndngList.Subject = "Buyer Group Changed";
						bodyTxt = "<b>MPR No: </b>" + mprrevisionDetails.DocumentNo + " buyer group is changed to " + mprrevisionDetails.BuyerGroupName + "";
					}

					emlSndngList.Body = "<html><meta charset=\"ISO-8859-1\"><head><link rel = 'stylesheet' href = 'https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css' ></head><body><div class='container'><span>" + bodyTxt + "</span><br/><br/><b>Click here to redirect : </b>&nbsp<a href='" + ipaddress + "'>" + ipaddress + "</a></div></body></html>";
					emlSndngList.FrmEmailId = (db.Employees.Where(li => li.EmployeeNo == FrmEmailId).FirstOrDefault<Employee>()).EMail;
					//emlSndngList.ToEmailId = "Developer@in.yokogawa.com";
					emlSndngList.ToEmailId = (db.Employees.Where(li => li.EmployeeNo == ToEmailId).FirstOrDefault<Employee>()).EMail;
					if ((!string.IsNullOrEmpty(emlSndngList.FrmEmailId) && !string.IsNullOrEmpty(emlSndngList.FrmEmailId)) && (emlSndngList.FrmEmailId != "NULL" && emlSndngList.ToEmailId != "NULL"))
						this.sendEmail(emlSndngList);

				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return true;
		}

		public bool sendMailtoVendor(sendMailObj mailObj)
		{
			try
			{
				using (var db = new YSCMEntities()) //ok
				{
					var vscm = new VSCMEntities();
					var ipaddress = ConfigurationManager.AppSettings["UI_vendor_IpAddress"];
					var fromEmail = ConfigurationManager.AppSettings["fromEmailTovendor"];
					EmailSend emlSndngList = new EmailSend();
					emlSndngList.Subject = "Message From Yokogawa";
					emlSndngList.FrmEmailId = fromEmail;
					//emlSndngList.ToEmailId = "Developer@in.yokogawa.com;";
					List<RemoteVendorUserMaster> userlist = vscm.RemoteVendorUserMasters.ToList();
					foreach (var item in userlist)
					{

						string mailbody = mailObj.Message;
						emlSndngList.ToEmailId = item.Vuserid;
						if (mailObj.IncludeUrl)
						{
							string url = "The required portal details and the password is given below : <br /><br /> <b  style='color:#40bfbf;'>Click Here to Redirect: <a href='" + ipaddress + "'>" + ipaddress + "</a></b><br />";
							mailbody = mailbody.Replace("The required portal details and the password is given below : <br />", url);
						}
						if (mailObj.IncludeCredentials)
						{
							string credentials = "The required portal details and the password is given below : <br /> <br /> <b style='color:#40bfbf;'>User Name:</b> " + item.Vuserid + " <br /><b style='color:#40bfbf;'>Pass word:</b> " + item.pwd + " <br />";
							mailbody = mailbody.Replace("The required portal details and the password is given below : <br />", credentials);
						}

						emlSndngList.Body = "<html><head></head><body><div>" + mailbody + "</div></body></html>";
						if ((!string.IsNullOrEmpty(emlSndngList.FrmEmailId) && !string.IsNullOrEmpty(emlSndngList.FrmEmailId)) && (emlSndngList.FrmEmailId != "NULL" && emlSndngList.ToEmailId != "NULL"))
							this.sendEmail(emlSndngList);

					}

				}
			}
			catch (Exception ex)
			{
				//throw ex;
			}
			return true;

		}
		public bool sendEmail(EmailSend emlSndngList)
		{
			bool validEmail = IsValidEmail(emlSndngList.ToEmailId);
			if (!string.IsNullOrEmpty(emlSndngList.ToEmailId) && !string.IsNullOrEmpty(emlSndngList.FrmEmailId) && validEmail)
			{
				var BCC = ConfigurationManager.AppSettings["BCC"];
				var SMTPServer = ConfigurationManager.AppSettings["SMTPServer"];
				MailMessage mailMessage = new MailMessage(emlSndngList.FrmEmailId, emlSndngList.ToEmailId);
				SmtpClient client = new SmtpClient();
				if (!string.IsNullOrEmpty(emlSndngList.Subject))
					mailMessage.Subject = emlSndngList.Subject;
				if (!string.IsNullOrEmpty(emlSndngList.CC))
					mailMessage.CC.Add(emlSndngList.CC);
				if (!string.IsNullOrEmpty(BCC))
					mailMessage.Bcc.Add(BCC);
				mailMessage.Body = emlSndngList.Body;
				mailMessage.IsBodyHtml = true;
				mailMessage.BodyEncoding = Encoding.UTF8;
				SmtpClient mailClient = new SmtpClient(SMTPServer, 25);
				//SmtpClient mailClient = new SmtpClient("10.29.15.9", 25);
				//mailClient.EnableSsl = true;
				mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
				mailClient.Send(mailMessage);
			}
			return true;
		}

		bool IsValidEmail(string email)
		{
			try
			{
				var addr = new System.Net.Mail.MailAddress(email);
				return addr.Address == email;
			}
			catch
			{
				return false;
			}
		}
	}

	public class EmailSend
	{
		public string FrmEmailId { get; set; }
		public string ToEmailId { get; set; }
		public string CC { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
	}
}
