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
    public partial class WebForm1 : System.Web.UI.Page
    {
        private void clear()
        {
            fname.Text = lname.Text = gender.Text = phone.Text = dob.Text = jdate.Text = address.Text = position.Text = salary.Text = qualification.Text = string.Empty;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("EmployeeRecords", Class1.cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Action", "INSERT"));
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
            clear();
            cmd.ExecuteNonQuery();
            Class1.cn.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null)
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
}