using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace test
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Empshow();
            Managerc();
            Salarysum();
            Bonussum();

            if (Session["id"] == null)
            {
                Response.Redirect("Login.aspx");
            }
        }

        private void Empshow()
        {
            SqlCommand cmd = new SqlCommand("EmployeeRecords", Class1.cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "SELECT");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            emplb.Text = dt.Rows.Count.ToString();
        }

        private void Managerc()
        {
            SqlCommand cmd = new SqlCommand("CustomFetch", Class1.cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "SELECT_MANAGER");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            manalb.Text = dt.Rows[0][0].ToString();
        }
        private void Salarysum()
        {
            SqlCommand cmd = new SqlCommand("EmployeeRecords", Class1.cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "SUM_SALARY");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            sallb.Text = dt.Rows[0][0].ToString();
        }


        private void Bonussum()
        {
            SqlCommand cmd = new SqlCommand("SalaryRecords", Class1.cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "SUM_SALARY");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            bonlb.Text = dt.Rows[0][0].ToString();
        }

        protected void Unnamed1_Click(object sender, ImageClickEventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            Response.Redirect("Login.aspx");
        }
    }
}