using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace test
{
    public partial class Attendance1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {

                showid();
            }

            if (Session["id"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            showAttendance();
        }

        private void clear()
        {
            TextBox1.Text = empid.Text = empname.Text = present.Text = absence.Text = leave.Text = Period.Text = string.Empty;
        }
        private void showid()
        {
            string sql = "select id,fname + ' ' + lname as fullname from Employee";
            SqlDataAdapter da = new SqlDataAdapter(sql, Class1.cn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            empid.DataSource = dt;
            empid.DataTextField = "id";
            empid.DataValueField = "fullname";
            empid.DataBind();
            empid.Items.Insert(0, new ListItem("--Select Id--", ""));
        }
        private void showAttendance()
        {
            SqlCommand cmd = new SqlCommand("AttendanceRecords", Class1.cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "SELECT");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        protected void delete0_Click(object sender, EventArgs e)
        {
            Page.Validate("validationgroup");
            if (Page.IsValid)
            {
                empname.Text = empid.Text;
            }
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            Page.Validate("myvalidationgroup");
            if (Page.IsValid)
            {
                SqlCommand cmd = new SqlCommand("AttendanceRecords", Class1.cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "INSERT");
                cmd.Parameters.AddWithValue("@empid", empid.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@empname", empname.Text);
                cmd.Parameters.AddWithValue("@present", present.Text);
                cmd.Parameters.AddWithValue("@absence", absence.Text);
                cmd.Parameters.AddWithValue("@leave", leave.Text);
                cmd.Parameters.AddWithValue("@period", Period.Text.ToString());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                clear();
                showAttendance();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);

            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Page.Validate("validationgg");
            if (Page.IsValid)
            {
                SqlCommand cmd = new SqlCommand("AttendanceRecords", Class1.cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "SELECT");
                cmd.Parameters.AddWithValue("@Id", TextBox1.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {

                    empid.SelectedItem.Text = dr["empid"].ToString();
                    empname.Text = dr["empname"].ToString();
                    present.Text = dr["present"].ToString();
                    absence.Text = dr["absence"].ToString();
                    leave.Text = dr["leave"].ToString();
                    Period.Text = dr["period"].ToString();
                }
            }
        }

        protected void delete_Click(object sender, EventArgs e)
        {
            Page.Validate("validationgg");
            if (Page.IsValid)
            {
                SqlCommand cmd = new SqlCommand("AttendanceRecords", Class1.cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@Id", TextBox1.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                clear();
                showAttendance();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Successfully')", true);

            }
        }

        protected void update_Click(object sender, EventArgs e)
        {

            Page.Validate("validationgg");
            if (Page.IsValid)
            {
                Page.Validate("myvalidationgroup");
                if (Page.IsValid)
                {
                    SqlCommand cmd = new SqlCommand("AttendanceRecords", Class1.cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "UPDATE");
                    cmd.Parameters.AddWithValue("@Id", TextBox1.Text);
                    cmd.Parameters.AddWithValue("@empid", empid.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@empname", empname.Text);
                    cmd.Parameters.AddWithValue("@present", present.Text);
                    cmd.Parameters.AddWithValue("@absence", absence.Text);
                    cmd.Parameters.AddWithValue("@leave", leave.Text);
                    cmd.Parameters.AddWithValue("@period", Period.Text.ToString());
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    showAttendance();
                    clear();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully')", true);

                }
            }
        }
    }
}