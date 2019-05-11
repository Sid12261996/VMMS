using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using VMMAPI.Models;

namespace VMMAPI.Repository
{
    public class DataRepository
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        public string InsertIE(IE ie)
        {
            SqlCommand cmd = new SqlCommand("usp_IEInsert", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Question", ie.Question);
            cmd.Parameters.AddWithValue("@Answer", ie.Answer);
            cmd.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            string id = cmd.Parameters["@Id"].Value.ToString();
            connection.Close();
            return id;
        }

        public void UpdateIE(IE ie)
        {
            SqlCommand cmd = new SqlCommand("usp_IEUpdate", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", ie.Id);
            cmd.Parameters.AddWithValue("@Question", ie.Question);
            cmd.Parameters.AddWithValue("@Answer", ie.Answer);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
        }

        public DataSet GetIE()
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("usp_IEGet", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            connection.Open();
            da.Fill(ds);
            connection.Close();
            return ds;
            
        }

        public DataSet GetIEById(int Id)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("usp_IEGetById", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@Id", Id);
            connection.Open();
            da.Fill(ds);
            connection.Close();
            return ds;

        }

        public int GetUserAuthenticate(string username, string password)
        {
            int counter = 0;
            DataTable ds = new DataTable();
            SqlCommand cmd = new SqlCommand("[GetUserAuthenticate]", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", password);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            connection.Open();
            da.Fill(ds);
            counter = ds.Rows[0].Field<int>("UserCount");
            connection.Close();
            return counter;
        }
    }
}