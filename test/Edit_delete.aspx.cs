using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace test
{
    public partial class Edit_delete : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            showemployee();
        }

        private void clear()
        {
            fname.Text = lname.Text = gender.Text = phone.Text = dob.Text = jdate.Text = address.Text = position.Text = salary.Text = qualification.Text = string.Empty;
        }
        private void showemployee()
        {
            SqlCommand cmd = new SqlCommand("EmployeeRecords", Class1.cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "SELECT");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            Page.Validate("validationgroup");
            if (Page.IsValid)
            {
                SqlCommand cmd = new SqlCommand("EmployeeRecords", Class1.cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "SELECT");
                cmd.Parameters.AddWithValue("@EmployeeID", TextBox1.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    fname.Text = dr["fname"].ToString();
                    lname.Text = dr["lname"].ToString();
                    gender.Text = dr["gender"].ToString();
                    phone.Text = dr["phone"].ToString();
                    dob.Text = dr["dob"].ToString();
                    jdate.Text = dr["jdate"].ToString();
                    address.Text = dr["address"].ToString();
                    position.Text = dr["position"].ToString();
                    salary.Text = dr["salary"].ToString();
                    qualification.Text = dr["qualification"].ToString();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Page.Validate("validationgroup");
            if (Page.IsValid)
            {
                SqlCommand cmd = new SqlCommand("EmployeeRecords", Class1.cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@EmployeeID", TextBox1.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                clear();
                showemployee();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Successfully')", true);

            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Page.Validate("validationgroup");
            if (Page.IsValid)
            {
                Page.Validate("myvalidationgroup");
                if (Page.IsValid)
                {
                    SqlCommand cmd = new SqlCommand("EmployeeRecords", Class1.cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "UPDATE");
                    cmd.Parameters.AddWithValue("@EmployeeID", TextBox1.Text);
                    cmd.Parameters.Add(new SqlParameter("@FirstName", fname.Text));
                    cmd.Parameters.Add(new SqlParameter("@LastName", lname.Text));
                    cmd.Parameters.Add(new SqlParameter("@Gender", gender.Text));
                    cmd.Parameters.Add(new SqlParameter("@Phone", phone.Text));
                    cmd.Parameters.Add(new SqlParameter("@DOB", dob.Text.ToString()));
                    cmd.Parameters.Add(new SqlParameter("@JDate", jdate.Text.ToString()));
                    cmd.Parameters.Add(new SqlParameter("@Address", address.Text));
                    cmd.Parameters.Add(new SqlParameter("@Position", position.Text));
                    cmd.Parameters.Add(new SqlParameter("@Salary", salary.Text));
                    cmd.Parameters.Add(new SqlParameter("@Qualification", qualification.Text));
                    Class1.cn.Open();
                    cmd.ExecuteNonQuery();
                    Class1.cn.Close();
                    clear();
                    showemployee();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully')", true);

                }
            }
        }
    }
}