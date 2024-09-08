using System;
using System.Data;
using System.Data.SqlClient;
using test;

namespace mufizproject
{
    public partial class WebForm1 : System.Web.UI.Page
    {


        private void clear()
        {
            txt_Username.Text = txt_password.Text = string.Empty;
        }
        protected void btn_Login_Click(object sender, EventArgs e)
        {
            int userCount = 0;
            string UserName = txt_Username.Text;
            string Password = txt_password.Text;

            SqlCommand cmd = new SqlCommand("CheckUserLogin", Class1.cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Username", UserName));
            cmd.Parameters.Add(new SqlParameter("@Password", Password));
            Class1.cn.Open();
            userCount = (int)cmd.ExecuteScalar();
            Class1.cn.Close();
            if (userCount > 0)
            {
                Session["id"] = txt_Username.Text;
                Response.Redirect("Home.aspx");
                clear();
            }
            else
            {
                lbl3.Text = "Invalid Username Or Password";
                clear();
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            clear();
        }
    }
}