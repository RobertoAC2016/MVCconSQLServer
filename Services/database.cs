using MVCconSQLServer.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MVCconSQLServer.Services
{
    public class database
    {
        private IConfiguration conf;
        private SqlConnection CON;
        public database(IConfiguration _conf)
        {
            conf = _conf;
        }
        private void getcon()
        {
            var constr = conf["constr"];
            CON = new SqlConnection(constr);
            CON.Open();
        }
        public List<user> get_user_list()
        {
            List<user> users = new List<user>();
            try
            {
                getcon();
                var SQL = "select id, name, lastname, phone, email, department, status from accounts where status = 'active' order by name;";
                SqlDataAdapter da = new SqlDataAdapter(SQL, CON);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        users.Add(new user {
                            id = int.Parse($"{item["id"]}"),
                            name = $"{item["name"]}",
                            lastname = $"{item["lastname"]}",
                            phone = $"{item["phone"]}",
                            email = $"{item["email"]}",
                            department = $"{item["department"]}",
                            status = $"{item["status"]}"
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return users;
        }

        public bool save_new_user(user? u)
        {
            bool status = true;
            try
            {
                getcon();
                var SQL = "insert into accounts (name, lastname, phone, email, department, status) values " +
                    $"('{u.name}','{u.lastname}','{u.phone}','{u.email}','{u.department}','active');select scope_identity()";
                SqlDataAdapter da = new SqlDataAdapter(SQL, CON);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count < 0)
                {
                    status = false;
                }
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public void delete_user(user u)
        {
            try
            {
                getcon();
                var SQL = $"delete from accounts where id = {u.id};select 1;";
                SqlDataAdapter da = new SqlDataAdapter(SQL, CON);
                DataTable dt = new DataTable();
                da.Fill(dt);
            }
            catch (Exception ex)
            {
            }
        }
        public user get_user_details(user u)
        {
            user usr = new user();
            try
            {
                getcon();
                var SQL = $"select * from accounts where id = {u.id};";
                SqlDataAdapter da = new SqlDataAdapter(SQL, CON);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        usr = new user
                        {
                            id = int.Parse($"{item["id"]}"),
                            name = $"{item["name"]}",
                            lastname = $"{item["lastname"]}",
                            phone = $"{item["phone"]}",
                            email = $"{item["email"]}",
                            department = $"{item["department"]}",
                            status = $"{item["status"]}"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return usr;
        }

        public void update_user(user u)
        {
            try
            {
                getcon();
                var SQL = $"Update accounts Set name = '{u.name}', lastname = '{u.lastname}', " +
                    $"phone = '{u.phone}', email = '{u.email}', department = '{u.department}' " +
                    $"where id = {u.id};select 1;";
                SqlDataAdapter da = new SqlDataAdapter(SQL, CON);
                DataTable dt = new DataTable();
                da.Fill(dt);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
