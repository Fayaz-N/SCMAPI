﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMModels.RFQModels
{
    public class MPRPAApproversModel
    {
        public MPRPAApproversModel()
        {
            MPRPADetail = new MPRPADetailsModel();
        }

        public string Approvername { get; set; }
        public string rolename { get; set; }
        public string Approver { get; set; }


        public int ApproverId { get; set; }
        public int PAId { get; set; }
        public byte ApproverLevel { get; set; }
        public string RoleName { get; set; }
        public string EmployeeNo { get; set; }
        public string ApproverName { get; set; }
        public string ApproversRemarks { get; set; }
        public string ApprovalStatus { get; set; }
        public string RoleId { get; set; }
        public int mprrevisionid { get; set; }
        public string parequested { get; set; }
        public Nullable<System.DateTime> PARequestedOn { get; set; }
        public string Pastatus { get; set; }
        public Nullable<System.DateTime> Pastatusupdate { get; set; }
        public Nullable<System.DateTime> ApprovedOn { get; set; }
        public  MPRPADetailsModel MPRPADetail { get; set; }
    }
}
