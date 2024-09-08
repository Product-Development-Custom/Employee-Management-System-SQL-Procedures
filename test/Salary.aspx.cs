using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing.Printing;
using System.Security.Policy;
using System.Xml.Linq;

namespace test
{
    public partial class Salary : System.Web.UI.Page
    {
        int DP = 0, DL = 0, DB = 0, TT = 0, GTR = 0, BA = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["id"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!Page.IsPostBack)
            {
                showid();
                showbonus();
                showattendance();
                showsalarygrid();
            }

        }

        private void clear()
        {
            empid.Text = empsalary.Text = empname.Text = bamount.Text = bonus.Text = attendanceid.Text = present.Text = absence.Text = leave.Text = paydate.Text = string.Empty;
        }
        private void showsalarygrid()
        {
            SqlCommand cmd = new SqlCommand("SalaryRecords", Class1.cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "SELECT");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        private void showid()
        {
            SqlCommand cmd = new SqlCommand("CustomFetch", Class1.cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "SELECT_ID");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            empid.DataSource = dt;
            empid.DataTextField = "id";
            empid.DataValueField = "fullname";
            empid.DataBind();
            empid.Items.Insert(0, new ListItem("Select Id", ""));
        }

        private void showsalary()
        {
            SqlCommand cmd = new SqlCommand("CustomFetch", Class1.cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "SELECT_SALARY");
            cmd.Parameters.AddWithValue("@empid", empid.SelectedItem.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                empsalary.Text = dr["salary"].ToString();
            }
        }

        private void showbonus()
        {
            SqlCommand cmd = new SqlCommand("CustomFetch", Class1.cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "SELECT_NAME_AMOUNT");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            bonus.DataSource = dt;
            bonus.DataTextField = "bname";
            bonus.DataValueField = "bamount";
            bonus.DataBind();
            bonus.Items.Insert(0, new ListItem("Select Bonus", ""));

        }

        protected void submit_Click(object sender, EventArgs e)
        {
            Page.Validate("validationg");
            Page.Validate("myvalidationgroup");
            if (Page.IsValid)
            {
                Page.Validate("validationgroup");
                if (Page.IsValid)
                {
                    SqlCommand cmd = new SqlCommand("SalaryRecords", Class1.cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "INSERT");
                    cmd.Parameters.AddWithValue("@empid", empid.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@empname", empname.Text);
                    cmd.Parameters.AddWithValue("@salary", empsalary.Text);
                    cmd.Parameters.AddWithValue("@bonus", bamount.Text);
                    cmd.Parameters.AddWithValue("@totalsalary", totalsalary.Text);
                    cmd.Parameters.AddWithValue("@paydate", paydate.Text.ToString());
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    showsalarygrid();
                    clear();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                }
            }
        }

        private void showattendance()
        {
            SqlCommand cmd = new SqlCommand("CustomFetch", Class1.cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "SELECT_EMPNAME");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            attendanceid.DataSource = dt;
            attendanceid.DataValueField = "empname";
            attendanceid.DataBind();
            attendanceid.Items.Insert(0, new ListItem("Select Employee", ""));
        }

        private void showday()
        {
            SqlCommand cmd = new SqlCommand("CustomFetch", Class1.cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "SELECT_DAYS");
            cmd.Parameters.AddWithValue("@empname", attendanceid.SelectedItem.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                present.Text = dr["present"].ToString();
                absence.Text = dr["absence"].ToString();
                leave.Text = dr["leave"].ToString();
            }

        }
        protected void load_Click(object sender, EventArgs e)
        {

            Page.Validate("myvalidationgroup");
            if (Page.IsValid)
            {
                empname.Text = empid.Text;
                bamount.Text = bonus.Text;
                showsalary();
                showday();
                
                DP = Convert.ToInt32(present.Text);
                DL = Convert.ToInt32(leave.Text);
                DB = Convert.ToInt32(empsalary.Text) / 28;
                TT = ((DB) * DP) + ((DB / 2) * DL);
                BA = Convert.ToInt32(bamount.Text);
                GTR = TT + BA;
                totalsalary.Text = GTR.ToString();
            }

        }


    }
}