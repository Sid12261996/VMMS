using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VMMAPI.Models
{

    public class UserDetails
    {
        public string UserId { set; get; }
       
    }
        public class TransactionDetails
    {

        public string AssestTypeId { set; get; }
        public string TransactionNo { set; get; }
        public string ActualAssetsId { set; get; }
        public string AssetsId { set; get; }
        public string ActivityId { set; get; }
        public string ActivityName { set; get; }

        public string CustId { set; get; }
        public string CustName { set; get; }


        public string AssignToId { set; get; }
        public string AssignToName { set; get; }
        public string AssignmentDate { set; get; }

        public string HostName { set; get; }
        public string BImage { set; get; }
        public string Aimage { set; get; }
        public string completedby { set; get; }
        public string actiondate { set; get; }
        public string Description { set; get; }
        public string DND { set; get; }
        public string FaultyDesc { set; get; }

        }
}