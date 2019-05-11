﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DataUtilityLayer;
using System.Configuration;
using System.Net.Mail;

namespace VMMAPI.Models
{

    public class Transaction : DataUtilityLayer.DataUtility
    {
        public string conn = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        SqlCommand cmdObj;

        public bool Insert_TaskAssignment(string cityId, string VendorId, string CustId, string ActivityId, string Type, string AssetsNO,string AssetType,string Address,string TaskType,string ContactNo,string SpocName,string State)
        {
            cmdObj = new SqlCommand();
            cmdObj.CommandText = "[TransactionAssignVendor]";

            cmdObj.Parameters
                .Add(new SqlParameter("@CustName", SqlDbType.NVarChar))
                .Value = CustId;

            cmdObj.Parameters
                .Add(new SqlParameter("@AssestCountNO", SqlDbType.NVarChar))
                .Value = AssetsNO;

            cmdObj.Parameters
                    .Add(new SqlParameter("@City", SqlDbType.NVarChar))
                    .Value = cityId;



            cmdObj.Parameters
                 .Add(new SqlParameter("@ActCount", SqlDbType.NVarChar))
                 .Value = "";
            cmdObj.Parameters
                .Add(new SqlParameter("@NewVenId", SqlDbType.NVarChar))
                .Value = VendorId;

            cmdObj.Parameters
                  .Add(new SqlParameter("@OldVenid", SqlDbType.NVarChar))
                  .Value = "";
            cmdObj.Parameters
                 .Add(new SqlParameter("@ActType", SqlDbType.NVarChar))
                 .Value = Type;
            cmdObj.Parameters
                .Add(new SqlParameter("@OldDate", SqlDbType.NVarChar))
                .Value = "";
            cmdObj.Parameters
                 .Add(new SqlParameter("@TaskId", SqlDbType.NVarChar))
                 .Value = ActivityId;
            cmdObj.Parameters
               .Add(new SqlParameter("@AssestType", SqlDbType.NVarChar))
               .Value = AssetType;

            cmdObj.Parameters
               .Add(new SqlParameter("@Address", SqlDbType.NVarChar))
               .Value = Address;
            cmdObj.Parameters
               .Add(new SqlParameter("@TaskType", SqlDbType.NVarChar))
               .Value = TaskType;
            cmdObj.Parameters
               .Add(new SqlParameter("@ContactNo", SqlDbType.NVarChar))
               .Value = ContactNo;
            cmdObj.Parameters
               .Add(new SqlParameter("@SpocName", SqlDbType.NVarChar))
               .Value = SpocName;
            cmdObj.Parameters
               .Add(new SqlParameter("@State", SqlDbType.NVarChar))
               .Value = State;
            SqlParameter outputParam = cmdObj.Parameters.Add("@ERROR", SqlDbType.Int);
            outputParam.Direction = ParameterDirection.Output;

            if (ExecuteSqlProcedure(cmdObj))
            {
                string Success = outputParam.Value.ToString();
                if (Success == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else { return false; }
        }



        public bool Insert_Remarks(string Transaction, string Activityid, string Remarks, string Status,string AssetsId)
        {
            cmdObj = new SqlCommand();
            cmdObj.CommandText = "[InsertTransactionRemarks]";

            cmdObj.Parameters
                .Add(new SqlParameter("@Transaction", SqlDbType.NVarChar))
                .Value = Transaction;
            this.cmdObj.Parameters.Add(new SqlParameter("@AssetsId", SqlDbType.NVarChar)).Value = (object)AssetsId;

            this.cmdObj.Parameters.Add(new SqlParameter("@Activityid", SqlDbType.NVarChar)).Value = (object)Activityid;
            
            this.cmdObj.Parameters.Add(new SqlParameter("@Remarks", SqlDbType.NVarChar)).Value = (object)Remarks;
            this.cmdObj.Parameters.Add(new SqlParameter("@Status", SqlDbType.NVarChar)).Value = (object)Status;


            SqlParameter sqlParameter = this.cmdObj.Parameters.Add("@ERROR", SqlDbType.Int);//@FeedBackType
            sqlParameter.Direction = ParameterDirection.Output;
            return this.ExecuteSqlProcedure(this.cmdObj) && sqlParameter.Value.ToString() == "1";


        }


        public DataTable Approval_Transation_Data(string Uid, string _Dealer, string _month, string _LOB, String _Out, string _sheetflag, string year)
        {
            cmdObj = new SqlCommand();
            cmdObj.CommandText = "Transation_Data";

            cmdObj.Parameters
                .Add(new SqlParameter("@d", SqlDbType.NVarChar))
                .Value = _Dealer;
            cmdObj.Parameters
                .Add(new SqlParameter("@m", SqlDbType.NVarChar))
                .Value = _month;
            cmdObj.Parameters
              .Add(new SqlParameter("@y", SqlDbType.NVarChar))
              .Value = year;
            cmdObj.Parameters
                .Add(new SqlParameter("@o", SqlDbType.NVarChar))
                .Value = _Out;
            cmdObj.Parameters
                  .Add(new SqlParameter("@l", SqlDbType.NVarChar))
                  .Value = _LOB;
            cmdObj.Parameters
                 .Add(new SqlParameter("@User_Id", SqlDbType.NVarChar))
                 .Value = Uid;

            cmdObj.Parameters
                  .Add(new SqlParameter("@SheetFlag", SqlDbType.NVarChar))
                  .Value = _sheetflag;

            DataTable dt = new DataTable();
            dt = GetDataTableWithProc(cmdObj);
            return dt;
        }

        public DataTable Approved_Data(string _Unique_Id)
        {
            cmdObj = new SqlCommand();
            cmdObj.CommandText = "Select * from tblTransaction where [Unique_Id]='" + _Unique_Id + "'";


            DataTable dt = new DataTable();
            dt = GetDataTableWithQuery(cmdObj);
            return dt;
        }



        public bool Update_Data(string _Dealer, string _month, string _LOB, string _KPI, String _TValue, String _Out)
        {
            cmdObj = new SqlCommand();
            cmdObj.CommandText = "Update_Target";

            cmdObj.Parameters
                .Add(new SqlParameter("@d", SqlDbType.NVarChar))
                .Value = _Dealer;
            cmdObj.Parameters
                .Add(new SqlParameter("@m", SqlDbType.NVarChar))
                .Value = _month;
            cmdObj.Parameters
                .Add(new SqlParameter("@o", SqlDbType.NVarChar))
                .Value = _Out;
            cmdObj.Parameters
                  .Add(new SqlParameter("@l", SqlDbType.NVarChar))
                  .Value = _LOB;

            cmdObj.Parameters
                 .Add(new SqlParameter("@t", SqlDbType.NVarChar))
                 .Value = _TValue;
            cmdObj.Parameters
                 .Add(new SqlParameter("@k", SqlDbType.NVarChar))
                 .Value = _KPI;
            cmdObj.Parameters
                 .Add(new SqlParameter("@Uid", SqlDbType.NVarChar))
                 .Value = Session["Uid"];

            if (ExecuteSqlProcedure(cmdObj))
            {

                return true;
            }
            else { return false; }

        }

        public bool Update_BULKData(string _Dealer, string _month, string _LOB, string _KPI, String _TValue, String _Out, string notification_id, string year)
        {
            cmdObj = new SqlCommand();
            cmdObj.CommandText = "Update_BULKTarget";

            cmdObj.Parameters
                .Add(new SqlParameter("@d", SqlDbType.NVarChar))
                .Value = _Dealer;
            cmdObj.Parameters
                .Add(new SqlParameter("@m", SqlDbType.NVarChar))
                .Value = _month;
            cmdObj.Parameters
                .Add(new SqlParameter("@y", SqlDbType.NVarChar))
                .Value = year;
            cmdObj.Parameters
                .Add(new SqlParameter("@o", SqlDbType.NVarChar))
                .Value = _Out;
            cmdObj.Parameters
                  .Add(new SqlParameter("@l", SqlDbType.NVarChar))
                  .Value = _LOB;

            cmdObj.Parameters
                 .Add(new SqlParameter("@t", SqlDbType.NVarChar))
                 .Value = _TValue;
            cmdObj.Parameters
                 .Add(new SqlParameter("@k", SqlDbType.NVarChar))
                 .Value = _KPI;
            cmdObj.Parameters
               .Add(new SqlParameter("@Notific_id", SqlDbType.NVarChar))
               .Value = notification_id;
            cmdObj.Parameters
                 .Add(new SqlParameter("@Uid", SqlDbType.NVarChar))
                 .Value = Session["Uid"];

            if (ExecuteSqlProcedure(cmdObj))
            {
                Session["result"] = "succ";
                return true;
            }
            else { return false; }

        }

        public bool updateActual_UserId(string _month, String _LOB, string _Dealer, String _Out)
        {
            cmdObj = new SqlCommand();
            cmdObj.CommandText = "Update_ActualUser";

            cmdObj.Parameters
                .Add(new SqlParameter("@d", SqlDbType.NVarChar))
                .Value = _Dealer;
            cmdObj.Parameters
                .Add(new SqlParameter("@m", SqlDbType.NVarChar))
                .Value = _month;
            cmdObj.Parameters
                  .Add(new SqlParameter("@l", SqlDbType.NVarChar))
                  .Value = _LOB;
            cmdObj.Parameters
                  .Add(new SqlParameter("@o", SqlDbType.NVarChar))
                  .Value = _Out;


            cmdObj.Parameters
                 .Add(new SqlParameter("@au", SqlDbType.NVarChar))
                 .Value = Session["Uid"].ToString();

            if (ExecuteSqlProcedure(cmdObj))
            { return true; }
            else { return false; }

        }

        public bool updateActualBulk_UserId(string _month, string _Dealer, string year)
        {
            cmdObj = new SqlCommand();
            cmdObj.CommandText = "Update_BulkActualUser";

            cmdObj.Parameters
                .Add(new SqlParameter("@d", SqlDbType.NVarChar))
                .Value = _Dealer;
            cmdObj.Parameters
                .Add(new SqlParameter("@m", SqlDbType.NVarChar))
                .Value = _month;
            cmdObj.Parameters
                   .Add(new SqlParameter("@y", SqlDbType.NVarChar))
                   .Value = year;

            cmdObj.Parameters
                 .Add(new SqlParameter("@au", SqlDbType.NVarChar))
                 .Value = Session["Uid"].ToString();

            if (ExecuteSqlProcedure(cmdObj))
            { return true; }
            else { return false; }

        }

        public bool updatehyg(string _no, String _desc, string _sel)
        {
            cmdObj = new SqlCommand();
            cmdObj.CommandText = "UpdateHyg";

            cmdObj.Parameters
                .Add(new SqlParameter("@no", SqlDbType.NVarChar))
                .Value = _no;
            cmdObj.Parameters
                .Add(new SqlParameter("@desc", SqlDbType.NVarChar))
                .Value = _desc;
            cmdObj.Parameters
                  .Add(new SqlParameter("@Selection", SqlDbType.NVarChar))
                  .Value = _sel;




            if (ExecuteSqlProcedure(cmdObj))
            { return true; }
            else { return false; }

        }
        public bool updateTarget_UserId(string _month, String _LOB, string _Dealer, String _Out)
        {
            cmdObj = new SqlCommand();
            cmdObj.CommandText = "Update_TargetUser";

            cmdObj.Parameters
                .Add(new SqlParameter("@d", SqlDbType.NVarChar))
                .Value = _Dealer;
            cmdObj.Parameters
                .Add(new SqlParameter("@m", SqlDbType.NVarChar))
                .Value = _month;
            cmdObj.Parameters
                  .Add(new SqlParameter("@l", SqlDbType.NVarChar))
                  .Value = _LOB;
            cmdObj.Parameters
                  .Add(new SqlParameter("@o", SqlDbType.NVarChar))
                  .Value = _Out;


            cmdObj.Parameters
                 .Add(new SqlParameter("@au", SqlDbType.NVarChar))
                 .Value = Session["Uid"].ToString();

            if (ExecuteSqlProcedure(cmdObj))
            { return true; }
            else { return false; }

        }

        public bool updateTargetBulk_UserId(string _month, string _Dealer, string year)
        {
            cmdObj = new SqlCommand();
            cmdObj.CommandText = "Update_BulkTargetUser";

            cmdObj.Parameters
                .Add(new SqlParameter("@d", SqlDbType.NVarChar))
                .Value = _Dealer;
            cmdObj.Parameters
                .Add(new SqlParameter("@m", SqlDbType.NVarChar))
                .Value = _month;
            cmdObj.Parameters
                .Add(new SqlParameter("@y", SqlDbType.NVarChar))
                .Value = year;


            cmdObj.Parameters
                 .Add(new SqlParameter("@au", SqlDbType.NVarChar))
                 .Value = Session["Uid"].ToString();

            if (ExecuteSqlProcedure(cmdObj))
            { return true; }
            else { return false; }

        }


        /* For update Approval status in Target_Actual_Approvels */
        public bool Update_Flag(string _Unique_id, string _Flag, string _Division_Id, string _LOB_Id, string flag, string fromwhom, string remarks, string notif_Id)
        {
            SqlCommand cmdObj2 = new SqlCommand();
            cmdObj = new SqlCommand();

            if (flag == "reject")
            {
                cmdObj.CommandText = "Update ttaa set [Approved_Status]='0',[App/rejected]='reject',Remarks='" + remarks + "' from tblTarget_Actual_Approvels ttaa inner join tblNotifications tn on (ttaa.[Notification_id]=tn.[Notification_id]) where ttaa.[Unique_Id]='" + _Unique_id + "' and ttaa.[Type]='" + _Flag + "' and ttaa.Division_Id='" + _Division_Id + "' and ttaa.LOB_Id ='" + _LOB_Id + "'and tn.FromWhom ='" + fromwhom + "' and tn.[Notification_id]='" + notif_Id + "'";
                cmdObj2.CommandText = "Update tn set tn.Hasbeenseen=0 from tblTarget_Actual_Approvels ttaa inner join tblNotifications tn on (ttaa.[Notification_id]=tn.[Notification_id]) where ttaa.[Unique_Id]='" + _Unique_id + "' and ttaa.[Type]='" + _Flag + "' and ttaa.Division_Id='" + _Division_Id + "' and ttaa.LOB_Id ='" + _LOB_Id + "'and tn.FromWhom ='" + fromwhom + "'and tn.[Notification_id]='" + notif_Id + "'";

            }
            else if (flag == "Rejectaccept")
            {
                cmdObj.CommandText = "Update ttaa set [Approved_Status]='0',[App/rejected]='accept',Remarks='" + remarks + "' from tblTarget_Actual_Approvels ttaa inner join tblNotifications tn on (ttaa.[Notification_id]=tn.[Notification_id]) where ttaa.[Unique_Id]='" + _Unique_id + "' and ttaa.[Type]='" + _Flag + "' and ttaa.Division_Id='" + _Division_Id + "' and ttaa.LOB_Id ='" + _LOB_Id + "' and tn.FromWhom ='" + fromwhom + "'and tn.[Notification_id]='" + notif_Id + "'";
                cmdObj2.CommandText = "Update tn set tn.Hasbeenseen=0 from tblTarget_Actual_Approvels ttaa inner join tblNotifications tn on (ttaa.[Notification_id]=tn.[Notification_id]) where ttaa.[Unique_Id]='" + _Unique_id + "' and ttaa.[Type]='" + _Flag + "' and ttaa.Division_Id='" + _Division_Id + "' and ttaa.LOB_Id ='" + _LOB_Id + "'and tn.FromWhom ='" + fromwhom + "'and tn.[Notification_id]='" + notif_Id + "'";
            }
            else
            {
                cmdObj.CommandText = "Update ttaa set ttaa.[Approved_Status]='1',ttaa.[App/rejected]='accept',Remarks='" + remarks + "' from tblTarget_Actual_Approvels ttaa inner join tblNotifications tn on (ttaa.[Notification_id]=tn.[Notification_id]) where ttaa.[Unique_Id]='" + _Unique_id + "' and ttaa.[Type]='" + _Flag + "' and ttaa.Division_Id='" + _Division_Id + "' and ttaa.LOB_Id ='" + _LOB_Id + "' and tn.FromWhom ='" + fromwhom + "'and tn.[Notification_id]='" + notif_Id + "'";
                cmdObj2.CommandText = "Update tn set tn.Hasbeenseen=1 from tblTarget_Actual_Approvels ttaa inner join tblNotifications tn on (ttaa.[Notification_id]=tn.[Notification_id]) where ttaa.[Unique_Id]='" + _Unique_id + "' and ttaa.[Type]='" + _Flag + "' and ttaa.Division_Id='" + _Division_Id + "' and ttaa.LOB_Id ='" + _LOB_Id + "'and tn.FromWhom ='" + fromwhom + "' and tn.[Notification_id]='" + notif_Id + "'";
            }




            if (ExecuteSqlCommand(cmdObj))
            {
                //Trans_Flag(_Unique_id, _Flag);
                ExecuteSqlCommand(cmdObj2);
                return true;
            }
            else { return false; }

        }

        /* For update KPI Wies Approval in Transaction table */
        public void Trans_Flag(String _Unique_id, String _Flag)
        {
            cmdObj = new SqlCommand();

            if (_Flag == "Target")
            {
                cmdObj.CommandText = "Update tblTransaction set [Approved]=1 where [Unique_Id]='" + _Unique_id + "'";

            }
            else
            {
                cmdObj.CommandText = "Update tblTransaction set [Approved]=1 where [Unique_Id]='" + _Unique_id + "'";

            }



            ExecuteSqlCommand(cmdObj);

        }

        /* For update Notification into table as well as mapped data into Target_Actual_Approvels */
        public bool notification_Insert(string _month, string _LOB, string _Dealer, string _Out, string Flag)
        {
            cmdObj = new SqlCommand();
            cmdObj.CommandText = "Insert_Notification";

            cmdObj.Parameters
                .Add(new SqlParameter("@d", SqlDbType.NVarChar))
                .Value = _Dealer;
            cmdObj.Parameters
                .Add(new SqlParameter("@m", SqlDbType.NVarChar))
                .Value = _month;
            cmdObj.Parameters
                  .Add(new SqlParameter("@l", SqlDbType.NVarChar))
                  .Value = _LOB;
            cmdObj.Parameters
                  .Add(new SqlParameter("@o", SqlDbType.NVarChar))
                  .Value = _Out;
            cmdObj.Parameters
                  .Add(new SqlParameter("@flag", SqlDbType.NVarChar))
                  .Value = Flag;

            cmdObj.Parameters
                 .Add(new SqlParameter("@au", SqlDbType.NVarChar))
                 .Value = Session["Uid"].ToString();
            cmdObj.Parameters
                 .Add(new SqlParameter("@type", SqlDbType.NVarChar))
                 .Value = Session["type"].ToString();


            if (ExecuteSqlProcedure(cmdObj))
            { return true; }
            else { return false; }

        }

        public bool Bulknotification_Insert(string _month, string _LOB, string _Dealer, string _Out, string Flag, string notification_id, string year)
        {
            cmdObj = new SqlCommand();
            cmdObj.CommandText = "Insert_BULKNotification";

            cmdObj.Parameters
                .Add(new SqlParameter("@d", SqlDbType.NVarChar))
                .Value = _Dealer;
            cmdObj.Parameters
                .Add(new SqlParameter("@m", SqlDbType.NVarChar))
                .Value = _month;
            cmdObj.Parameters
                .Add(new SqlParameter("@y", SqlDbType.NVarChar))
                .Value = year;
            cmdObj.Parameters
                  .Add(new SqlParameter("@l", SqlDbType.NVarChar))
                  .Value = _LOB;
            cmdObj.Parameters
                  .Add(new SqlParameter("@o", SqlDbType.NVarChar))
                  .Value = _Out.ToString();
            cmdObj.Parameters
                  .Add(new SqlParameter("@flag", SqlDbType.NVarChar))
                  .Value = Flag;
            cmdObj.Parameters
                 .Add(new SqlParameter("@notification_id", SqlDbType.NVarChar))
                 .Value = notification_id;

            cmdObj.Parameters
                 .Add(new SqlParameter("@au", SqlDbType.NVarChar))
                 .Value = Session["Uid"].ToString();
            cmdObj.Parameters
                 .Add(new SqlParameter("@type", SqlDbType.NVarChar))
                 .Value = Session["type"].ToString();


            if (ExecuteSqlProcedure(cmdObj))
            { return true; }
            else { return false; }

        }

        public bool Email(string dealer, string flag, string Notification_id)
        {
            string tomail_id = string.Empty, frommail_id = string.Empty, forwhom = string.Empty, fromwhom = string.Empty;
            try
            {
                using (var cn = new SqlConnection(conn))
                {
                    string _sql = @"getMailDetails";
                    var cmd = new SqlCommand(_sql, cn);
                    cmd.Parameters
                        .Add(new SqlParameter("@d", SqlDbType.NVarChar))
                        .Value = dealer;
                    cmd.Parameters
                        .Add(new SqlParameter("@noti_id", SqlDbType.NVarChar))
                        .Value = Notification_id;
                    cmd.Parameters
                        .Add(new SqlParameter("@type", SqlDbType.NVarChar))
                        .Value = Session["Type"].ToString();
                    cmd.Parameters
                        .Add(new SqlParameter("@Uid", SqlDbType.NVarChar))
                        .Value = Session["Uid"].ToString();
                    cn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            tomail_id = reader["ToEmail"].ToString();
                            frommail_id = reader["FromEmail"].ToString();
                            forwhom = reader["For_Name"].ToString();
                            fromwhom = reader["From_Name"].ToString();

                        }
                        reader.Dispose();
                        cmd.Dispose();
                        SendEmail(tomail_id, frommail_id, forwhom, fromwhom, flag);
                        return true;
                    }
                    else
                    {
                        reader.Dispose();
                        cmd.Dispose();
                        return false;
                    }
                }
            }
            catch
            { }
            return true;
        }
        public bool SendEmail(string tomail_id, string frommail_id, string forwhom, string fromwhom, string flag)
        {
            try
            {
                MailMessage objeto_mail = new MailMessage();
                SmtpClient client = new SmtpClient();
                client.Port = 25;
                //client.Port = 587;
                client.Host = "172.31.70.147";
                client.Timeout = 20000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.EnableSsl = false;
                client.Credentials = new System.Net.NetworkCredential("cpsc@tatamotors.com", "Welcome@123");
                objeto_mail.From = new MailAddress(frommail_id, "Alert-CPSC");
                objeto_mail.To.Add(new MailAddress(tomail_id));
                objeto_mail.Subject = flag + "Sheet";

                objeto_mail.Body = "Dear Concerned, \r\n" + fromwhom + " has been Filled the BulkSheet" + flag + ",Approval is pending.\r\n" + "Thank you,\r\n" + "Tata Support Team";
                client.Send(objeto_mail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Update_ActualData(string _Dealer, string _month, string _LOB, string _KPI, String _AValue, String _Out)
        {
            cmdObj = new SqlCommand();
            cmdObj.CommandText = "Update_Actual";

            cmdObj.Parameters
                .Add(new SqlParameter("@d", SqlDbType.NVarChar))
                .Value = _Dealer;
            cmdObj.Parameters
                .Add(new SqlParameter("@m", SqlDbType.NVarChar))
                .Value = _month;
            cmdObj.Parameters
                  .Add(new SqlParameter("@l", SqlDbType.NVarChar))
                  .Value = _LOB;

            cmdObj.Parameters
                  .Add(new SqlParameter("@o", SqlDbType.NVarChar))
                  .Value = _Out;

            cmdObj.Parameters
                 .Add(new SqlParameter("@a", SqlDbType.NVarChar))
                 .Value = _AValue;
            cmdObj.Parameters
                 .Add(new SqlParameter("@k", SqlDbType.NVarChar))
                 .Value = _KPI;
            cmdObj.Parameters
                .Add(new SqlParameter("@Uid", SqlDbType.NVarChar))
                .Value = Session["Uid"];
            if (ExecuteSqlProcedure(cmdObj))
            {
                return true;
            }
            else { return false; }

        }


        public bool Update_BULKActualData(string _Dealer, string year, string _month, string _LOB, string _KPI, String _AValue, String _Out, string Notification_id)
        {
            cmdObj = new SqlCommand();
            cmdObj.CommandText = "Update_BULKActual";

            cmdObj.Parameters
                .Add(new SqlParameter("@d", SqlDbType.NVarChar))
                .Value = _Dealer;

            cmdObj.Parameters
                .Add(new SqlParameter("@y", SqlDbType.NVarChar))
                .Value = year;
            cmdObj.Parameters
                .Add(new SqlParameter("@m", SqlDbType.NVarChar))
                .Value = _month;
            cmdObj.Parameters
                  .Add(new SqlParameter("@l", SqlDbType.NVarChar))
                  .Value = _LOB;

            cmdObj.Parameters
                  .Add(new SqlParameter("@o", SqlDbType.NVarChar))
                  .Value = _Out;

            cmdObj.Parameters
                 .Add(new SqlParameter("@a", SqlDbType.NVarChar))
                 .Value = _AValue;
            cmdObj.Parameters
                 .Add(new SqlParameter("@k", SqlDbType.NVarChar))
                 .Value = _KPI;
            cmdObj.Parameters
                .Add(new SqlParameter("@Notific_id", SqlDbType.NVarChar))
                .Value = Notification_id;
            cmdObj.Parameters
                .Add(new SqlParameter("@Uid", SqlDbType.NVarChar))
                .Value = Session["Uid"];
            if (ExecuteSqlProcedure(cmdObj))
            {
                Session["result"] = "succ";
                return true;
            }
            else { return false; }

        }



        public bool Update_AllCheckedNotification(string Notification_id, string flag)
        {
            cmdObj = new SqlCommand();
            cmdObj.CommandText = "Update_Notification_All";

            cmdObj.Parameters
                .Add(new SqlParameter("@Notification_id", SqlDbType.NVarChar))
                .Value = Notification_id;

            cmdObj.Parameters
                .Add(new SqlParameter("@flag", SqlDbType.NVarChar))
                .Value = flag;
            if (ExecuteSqlProcedure(cmdObj))
            {
                Session["result"] = "succ";
                return true;
            }
            else { return false; }

        }



    }
    public class TransactionViewModel
    {
        public TransactionViewModel()
        {
            Trans_list = new List<Transaction>();
            Trans = new Transaction();
        }
        public List<Transaction> Trans_list { get; set; }
        public Transaction Trans { get; set; }
    }
}