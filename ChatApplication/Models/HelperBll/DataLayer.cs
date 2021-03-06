﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;
using System.Configuration;
using System.Data;
using ChatApplication.Models.HelperBll;
using System.Data.SqlClient;

namespace ChatApplication.Models.HelperBll
{
    public class DataLayer
    {
        SqlConnection con;
        private SqlCommand cmd;



        public DataLayer()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString);
        }
        public UserModel login(string email,string password)
        {
            UserModel user = new UserModel();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string sql = "select * from tbluser where email='" + email + "' and password='" + password + "'";
            ds.Reset();

            using (cmd = new SqlCommand(sql, con))
            {
                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        user.userid = Convert.ToInt32(rdr["userid"].ToString());
                        user.email = rdr["email"].ToString();
                        user.mobile = rdr["mobile"].ToString();
                        user.password = rdr["password"].ToString();
                    }
                }
            }

            return user;
        }
        public List<UserModel> getusers(int id)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            List<UserModel> userlist = new List<UserModel>();
            string sql = "select * from tbluser where userid<>"+id;
            ds.Reset();

            using (cmd = new SqlCommand(sql, con))
            {
                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                    UserModel user = new UserModel();
                    user.userid = Convert.ToInt32(rdr["userid"].ToString());
                    user.email = rdr["email"].ToString();
                    user.mobile = rdr["mobile"].ToString();
                    user.password = rdr["password"].ToString();
                    user.dob = rdr["dob"].ToString();
                    userlist.Add(user);
                }

                }
            }

            return userlist;
        }

    }
}