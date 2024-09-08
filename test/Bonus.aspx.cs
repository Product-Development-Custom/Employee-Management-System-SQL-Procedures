using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace test
{
    public partial class Bonus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            showbonus();
        }
        private void clear()
        {
            bname.Text = bamount.Text = TextBox1.Text = string.Empty;
        }

        private void showbonus()
        {
            SqlCommand cmd = new SqlCommand("BonusRecords", Class1.cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "SELECT");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            Page.Validate("myvalidationgroup");
            if (Page.IsValid)
            {
                SqlCommand cmd = new SqlCommand("BonusRecords", Class1.cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "INSERT");
                cmd.Parameters.AddWithValue("@bname", bname.Text);
                cmd.Parameters.AddWithValue("@bamount", bamount.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                showbonus();
                clear();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);

            }
            else
            {
                clear();
            }
        }

        protected void delete_Click(object sender, EventArgs e)
        {
            Page.Validate("validationgroup");
            if (Page.IsValid)
            {
               
                SqlCommand cmd = new SqlCommand("BonusRecords", Class1.cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@Id", TextBox1.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                clear();
                showbonus();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Successfully')", true);
            }
        }

        protected void update_Click(object sender, EventArgs e)
        {
            Page.Validate("validationgroup");
            if (Page.IsValid)
            {
                Page.Validate("myvalidationgroup");
                if (Page.IsValid)
                {
                    SqlCommand cmd = new SqlCommand("BonusRecords", Class1.cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "UPDATE");
                    cmd.Parameters.AddWithValue("@Id", TextBox1.Text);
                    cmd.Parameters.AddWithValue("@bname", bname.Text);
                    cmd.Parameters.AddWithValue("@bamount", bamount.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    clear();
                    showbonus();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully')", true);

                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Page.Validate("validationgroup");
            if (Page.IsValid)
            {
                SqlCommand cmd = new SqlCommand("BonusRecords", Class1.cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "SELECT");
                cmd.Parameters.AddWithValue("@Id", TextBox1.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    bname.Text = dr["bname"].ToString();
                    bamount.Text = dr["bamount"].ToString();

                }
            }
        }
    }
}