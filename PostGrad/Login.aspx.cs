using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PostGrad
{

    public partial class Login : System.Web.UI.Page
    {
        bool IsAllDigits(string s)
        {
            foreach (char c in s)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginBtn(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["PostGrad"].ToString();

            SqlConnection conn = new SqlConnection(connStr);

            string userIDTxt = userID.Text;
            string pass = password.Text;

            if (userIDTxt.Replace(" ", "") == "" || pass.Replace(" ", "") == "")
            {
                int x = 705;
                int y = 400;
                Response.Write("<div style='position:absolute;top:" + y.ToString() + "px;left:" + x.ToString() + "px'>Please enter a valid id and password!</div>");
            }
            else
            {
                if (IsAllDigits(userIDTxt))
                {
                    int idUser = Int16.Parse(userIDTxt);
                    SqlCommand loginPROC = new SqlCommand("userLogin", conn);
                    loginPROC.CommandType = CommandType.StoredProcedure;

                    loginPROC.Parameters.Add(new SqlParameter("@id", idUser));
                    loginPROC.Parameters.Add(new SqlParameter("@password", pass));

                    SqlParameter success = loginPROC.Parameters.Add("@success", SqlDbType.Int);

                    success.Direction = ParameterDirection.Output;

                    conn.Open();
                    loginPROC.ExecuteNonQuery();
                    conn.Close();

                    if (success.Value.ToString() == "1")
                    {
                        int x = 735;
                        int y = 400;
                        Response.Write("<div style='position:absolute;top:" + y.ToString() + "px;left:" + x.ToString() + "px'>You logged in successfully!</div>");

                        Session["user"] = idUser;

                        SqlCommand userTypePROC = new SqlCommand("userType", conn);
                        userTypePROC.CommandType = CommandType.StoredProcedure;

                        userTypePROC.Parameters.Add(new SqlParameter("@id", idUser));

                        SqlParameter userType = userTypePROC.Parameters.Add("@type", SqlDbType.VarChar, 20);

                        userType.Direction = ParameterDirection.Output;

                        conn.Open();
                        userTypePROC.ExecuteNonQuery();
                        conn.Close();

                        Session["userType"] = userType.Value;

                        if ((string)Session["userType"] == "gucianstudent" || (string)Session["userType"] == "nongucianstudent")
                        {
                            Response.Redirect("Student.aspx");
                        }else if((string)Session["userType"] == "supervisor")
                        {
                            Response.Redirect("Supervisor.aspx");
                        }
                        else if ((string)Session["userType"] == "admin")
                        {
                            Response.Redirect("Admin.aspx");
                        }
                        //redirect to homepage

                    }
                    else
                    {
                        int x = 705;
                        int y = 400;
                        Response.Write("<div style='position:absolute;top:" + y.ToString() + "px;left:" + x.ToString() + "px'>Please enter a valid id and password!</div>");
                    }
                }
                else
                {
                    int x = 735;
                    int y = 400;
                    Response.Write("<div style='position:absolute;top:" + y.ToString() + "px;left:" + x.ToString() + "px'>ID has to be all digit numbers!</div>");
                }

            }


        }
    }
}