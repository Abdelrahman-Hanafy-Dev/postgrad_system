using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PostGrad
{
    public partial class Student : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["user"] == null || (Session["userType"]!="gucianstudent" && Session["userType"]!="nongucianstudent"))
            //   Response.Redirect("Login.aspx");

            if ((string)Session["userType"] == "gucianstudent")
            {
                ViewCoursesNon.Enabled = false;
            }
            gucianProfileTable.Visible = false;
            stdListThesis.Visible = false;
            coursesTable.Visible = false;
            Label4.Visible = false;
            Label5.Visible = false;
            Label6.Visible = false;
            Label7.Visible = false;
            thesNumFillPrg.Visible = false;
            prgNumFillPrg.Visible = false;
            state.Visible = false;
            descTextArea.Visible = false;
            TablePrgReports.Visible = false;
            FillPR.Visible = false;


        }

        protected void viewPrfBtn(object sender, EventArgs e)
        {
            Label1.Visible = false;
            Label2.Visible = false;
            Label3.Visible = false;
            thesisNum.Visible = false;
            progDate.Visible = false;
            progNumber.Visible = false;
            AddRep.Visible = false;

            string connStr = WebConfigurationManager.ConnectionStrings["PostGrad"].ToString();

            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand viewMyProfilePROC = new SqlCommand("viewMyProfile", conn);
            viewMyProfilePROC.CommandType = CommandType.StoredProcedure;

            viewMyProfilePROC.Parameters.Add(new SqlParameter("@studentId", Session["user"]));


            conn.Open();

            SqlDataReader rdr = viewMyProfilePROC.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                Int32 stdID = rdr.GetInt32(rdr.GetOrdinal("id"));
                String stdFirstName = rdr.GetString(rdr.GetOrdinal("firstName"));
                String stdLastName = rdr.GetString(rdr.GetOrdinal("lastName"));
                String stdType = "";
                try
                {
                    stdType = rdr.GetString(rdr.GetOrdinal("type"));
                }
                catch (Exception e1)
                {
                }
                String stdFaculty = rdr.GetString(rdr.GetOrdinal("faculty"));
                String stdAddress = rdr.GetString(rdr.GetOrdinal("address"));
                Decimal stdGPA = 0;
                try
                {
                    stdGPA = rdr.GetDecimal(rdr.GetOrdinal("GPA"));
                }
                catch (Exception e1)
                {
                }
                Int32 stdUnderID = 0;
                try
                {
                    stdUnderID = rdr.GetInt32(rdr.GetOrdinal("undergradID"));
                }
                catch (Exception e1)
                {
                }

                HtmlTableRow tRow = new HtmlTableRow();

                HtmlTableCell tb = new HtmlTableCell();
                tb.InnerText = "" + stdID;
                HtmlTableCell tb1 = new HtmlTableCell();
                tb1.InnerText = stdFirstName;
                HtmlTableCell tb2 = new HtmlTableCell();
                tb2.InnerText = stdLastName;
                HtmlTableCell tb3 = new HtmlTableCell();
                tb3.InnerText = stdType;
                HtmlTableCell tb4 = new HtmlTableCell();
                tb4.InnerText = stdFaculty;
                HtmlTableCell tb5 = new HtmlTableCell();
                tb5.InnerText = stdAddress;
                HtmlTableCell tb6 = new HtmlTableCell();
                tb6.InnerText = "" + stdGPA;
                HtmlTableCell tb7 = new HtmlTableCell();
                tb7.InnerText = "" + stdUnderID;

                tRow.Controls.Add(tb);
                tRow.Controls.Add(tb1);
                tRow.Controls.Add(tb2);
                tRow.Controls.Add(tb3);
                tRow.Controls.Add(tb4);
                tRow.Controls.Add(tb5);
                tRow.Controls.Add(tb6);
                tRow.Controls.Add(tb7);

                gucianProfileTable.Rows.Add(tRow);
            }

            conn.Close();


            gucianProfileTable.Visible = true;
        }


        protected void listThesBtn(object sender, EventArgs e)
        {
            Label1.Visible = false;
            Label2.Visible = false;
            Label3.Visible = false;
            thesisNum.Visible = false;
            progDate.Visible = false;
            progNumber.Visible = false;
            AddRep.Visible = false;

            string connStr = WebConfigurationManager.ConnectionStrings["PostGrad"].ToString();

            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand listMyThesisPROC = new SqlCommand("listMyThesis", conn);
            listMyThesisPROC.CommandType = CommandType.StoredProcedure;

            listMyThesisPROC.Parameters.Add(new SqlParameter("@studentId", Session["user"]));

            conn.Open();

            SqlDataReader rdr = listMyThesisPROC.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                Int32 ThesisSer = rdr.GetInt32(rdr.GetOrdinal("serialNumber"));

                String field = "";
                try
                {
                    field = rdr.GetString(rdr.GetOrdinal("field"));
                }
                catch (Exception e1)
                {

                }

                String type = rdr.GetString(rdr.GetOrdinal("type"));
                String title = rdr.GetString(rdr.GetOrdinal("title"));
                DateTime startDate = rdr.GetDateTime(rdr.GetOrdinal("startDate"));
                DateTime endDate = rdr.GetDateTime(rdr.GetOrdinal("endDate"));

                DateTime defenseDate = new DateTime();
                try
                {
                    defenseDate = rdr.GetDateTime(rdr.GetOrdinal("defenseDate"));
                }
                catch (Exception e1)
                {

                }

                Int32 years = 0;
                try
                {
                    years = rdr.GetInt32(rdr.GetOrdinal("years"));
                }
                catch (Exception e1)
                {

                }

                Decimal grade = 0;
                try
                {
                    grade = rdr.GetDecimal(rdr.GetOrdinal("grade"));
                }
                catch (Exception e1)
                {

                }

                Int32 paymentID = 0;
                try
                {
                    paymentID = rdr.GetInt32(rdr.GetOrdinal("payment_id"));
                }
                catch (Exception e1)
                {

                }

                Int32 numOfExt = 0;
                try
                {
                    numOfExt = rdr.GetInt32(rdr.GetOrdinal("noOfExtensions"));
                }
                catch (Exception e1)
                {

                }

                HtmlTableRow tRow = new HtmlTableRow();

                HtmlTableCell tb = new HtmlTableCell();
                tb.InnerText = "" + ThesisSer;
                HtmlTableCell tb1 = new HtmlTableCell();
                tb1.InnerText = field;
                HtmlTableCell tb2 = new HtmlTableCell();
                tb2.InnerText = type;
                HtmlTableCell tb10 = new HtmlTableCell();
                tb10.InnerText = title;
                HtmlTableCell tb3 = new HtmlTableCell();
                tb3.InnerText = "" + startDate;
                HtmlTableCell tb4 = new HtmlTableCell();
                tb4.InnerText = "" + endDate;
                HtmlTableCell tb5 = new HtmlTableCell();
                tb5.InnerText = "" + defenseDate;
                HtmlTableCell tb6 = new HtmlTableCell();
                tb6.InnerText = "" + years;
                HtmlTableCell tb7 = new HtmlTableCell();
                tb7.InnerText = "" + grade;
                HtmlTableCell tb8 = new HtmlTableCell();
                tb7.InnerText = "" + paymentID;
                HtmlTableCell tb9 = new HtmlTableCell();
                tb7.InnerText = "" + numOfExt;

                tRow.Controls.Add(tb);
                tRow.Controls.Add(tb1);
                tRow.Controls.Add(tb2);
                tRow.Controls.Add(tb10);
                tRow.Controls.Add(tb3);
                tRow.Controls.Add(tb4);
                tRow.Controls.Add(tb5);
                tRow.Controls.Add(tb6);
                tRow.Controls.Add(tb7);
                tRow.Controls.Add(tb8);
                tRow.Controls.Add(tb9);

                stdListThesis.Rows.Add(tRow);
            }

            conn.Close();
            stdListThesis.Visible = true;
        }


        protected void ViewCrsBtn(object sender, EventArgs e)
        {
            Label1.Visible = false;
            Label2.Visible = false;
            Label3.Visible = false;
            thesisNum.Visible = false;
            progDate.Visible = false;
            progNumber.Visible = false;
            AddRep.Visible = false;

            string connStr = WebConfigurationManager.ConnectionStrings["PostGrad"].ToString();

            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand ListCoursesAndGradePROC = new SqlCommand("ListCoursesAndGrade", conn);
            ListCoursesAndGradePROC.CommandType = CommandType.StoredProcedure;

            ListCoursesAndGradePROC.Parameters.Add(new SqlParameter("@studentId", Session["user"]));

            conn.Open();

            SqlDataReader rdr = ListCoursesAndGradePROC.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                Int32 cid = rdr.GetInt32(rdr.GetOrdinal("id"));

                Int32 fees = 0;
                try
                {
                    fees = rdr.GetInt32(rdr.GetOrdinal("fees"));
                }
                catch (Exception e1)
                {

                }

                Int32 crdtHrs = 0;
                try
                {
                    crdtHrs = rdr.GetInt32(rdr.GetOrdinal("creditHours"));
                }
                catch (Exception e1)
                {

                }

                String code = "";
                try
                {
                    code = rdr.GetString(rdr.GetOrdinal("code"));
                }
                catch (Exception e1)
                {

                }

                decimal grade = 0;
                try
                {
                    grade = rdr.GetDecimal(rdr.GetOrdinal("grade"));
                }
                catch (Exception e1)
                {

                }

                HtmlTableRow tRow = new HtmlTableRow();

                HtmlTableCell tb = new HtmlTableCell();
                tb.InnerText = "" + cid;
                HtmlTableCell tb1 = new HtmlTableCell();
                tb1.InnerText = "" + fees;
                HtmlTableCell tb2 = new HtmlTableCell();
                tb2.InnerText = "" + crdtHrs;
                HtmlTableCell tb3 = new HtmlTableCell();
                tb3.InnerText = code;
                HtmlTableCell tb4 = new HtmlTableCell();
                tb4.InnerText = "" + grade;

                tRow.Controls.Add(tb);
                tRow.Controls.Add(tb1);
                tRow.Controls.Add(tb2);
                tRow.Controls.Add(tb3);
                tRow.Controls.Add(tb4);

                coursesTable.Rows.Add(tRow);
            }

            conn.Close();
            coursesTable.Visible = true;

        }

        protected void addPrg(object sender, EventArgs e)
        {

            Label1.Visible = true;
            Label2.Visible = true;
            Label3.Visible = true;
            thesisNum.Visible = true;
            progDate.Visible = true;
            progNumber.Visible = true;
            AddRep.Visible = true;
            thesisNum.Enabled = true;
            progDate.Enabled = true;

        }
        bool IsAllDigits(string s)
        {
            foreach (char c in s)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }
        protected void addProgress(object sender, EventArgs e)
        {

            string connStr = WebConfigurationManager.ConnectionStrings["PostGrad"].ToString();

            SqlConnection conn = new SqlConnection(connStr);

            string thesisNumber = thesisNum.Text;
            DateTime progCalender = progDate.SelectedDate;
            string prgNum = progNumber.Text;

            if (thesisNumber.Replace(" ", "") == "" || prgNum.Replace(" ", "") == "")
            {
                int x = 800;
                int y = 760;
                Response.Write("<div style='position:absolute;top:" + y.ToString() + "px;left:" + x.ToString() + "px'>Please fill all the fields!</div>");
            }
            else
            {
                if (!IsAllDigits(thesisNumber))
                {
                    int x = 820;
                    int y = 760;
                    Response.Write("<div style='position:absolute;top:" + y.ToString() + "px;left:" + x.ToString() + "px'>Thesis number has to be all digit numbers!</div>");
                }
                else if (!IsAllDigits(prgNum))
                {
                    int x = 820;
                    int y = 760;
                    Response.Write("<div style='position:absolute;top:" + y.ToString() + "px;left:" + x.ToString() + "px'>Progress number has to be all digit numbers!</div>");
                }
                else
                {
                    int thesNumInt = Int16.Parse(thesisNumber);

                    SqlCommand thesisBelongsToMePROC = new SqlCommand("thesisBelongsToMe", conn);
                    thesisBelongsToMePROC.CommandType = CommandType.StoredProcedure;

                    thesisBelongsToMePROC.Parameters.Add(new SqlParameter("@stdID", Session["user"]));
                    thesisBelongsToMePROC.Parameters.Add(new SqlParameter("@thesisSerial", thesNumInt));

                    SqlParameter success = thesisBelongsToMePROC.Parameters.Add("@success", SqlDbType.Int);

                    success.Direction = ParameterDirection.Output;

                    conn.Open();
                    thesisBelongsToMePROC.ExecuteNonQuery();
                    conn.Close();

                    int progNumInt = Int16.Parse(prgNum);

                    SqlCommand progressNumUsedPROC = new SqlCommand("progressNumUsed", conn);
                    progressNumUsedPROC.CommandType = CommandType.StoredProcedure;

                    progressNumUsedPROC.Parameters.Add(new SqlParameter("@stdID", Session["user"]));
                    progressNumUsedPROC.Parameters.Add(new SqlParameter("@progressNum", progNumInt));

                    SqlParameter success2 = progressNumUsedPROC.Parameters.Add("@success", SqlDbType.Int);

                    success2.Direction = ParameterDirection.Output;

                    conn.Open();
                    progressNumUsedPROC.ExecuteNonQuery();
                    conn.Close();

                    if (success.Value.ToString() == "0")
                    {
                        int x = 840;
                        int y = 760;
                        Response.Write("<div style='position:absolute;top:" + y.ToString() + "px;left:" + x.ToString() + "px'>Please choose a thesis you registered!</div>");
                    }
                    else if (success2.Value.ToString() == "1")
                    {
                        int x = 800;
                        int y = 760;
                        Response.Write("<div style='position:absolute;top:" + y.ToString() + "px;left:" + x.ToString() + "px'>Progress number already used, please choose another one.</div>");
                    }
                    else
                    {

                        SqlCommand AddProgressReportPROC = new SqlCommand("AddProgressReport", conn);
                        AddProgressReportPROC.CommandType = CommandType.StoredProcedure;

                        AddProgressReportPROC.Parameters.Add(new SqlParameter("@thesisSerialNo", thesNumInt));
                        AddProgressReportPROC.Parameters.Add(new SqlParameter("@progressReportDate", progCalender));
                        AddProgressReportPROC.Parameters.Add(new SqlParameter("@studentID", Session["user"]));
                        AddProgressReportPROC.Parameters.Add(new SqlParameter("@progressReportNo", progNumInt));


                        conn.Open();
                        AddProgressReportPROC.ExecuteNonQuery();
                        conn.Close();

                        int x = 840;
                        int y = 760;
                        Response.Write("<div style='position:absolute;top:" + y.ToString() + "px;left:" + x.ToString() + "px'>Progress Report added successfully.</div>");


                    }

                }


            }


        }

        protected void fillProgress(object sender, EventArgs e)
        {
            Label1.Visible = false;
            Label2.Visible = false;
            Label3.Visible = false;
            thesisNum.Visible = false;
            progDate.Visible = false;
            progNumber.Visible = false;
            AddRep.Visible = false;

            Label4.Visible = true;
            Label5.Visible = true;
            Label6.Visible = true;
            Label7.Visible = true;
            thesNumFillPrg.Visible = true;
            prgNumFillPrg.Visible = true;
            state.Visible = true;
            descTextArea.Visible = true;
            TablePrgReports.Visible = true;
            FillPR.Visible = true;


            string connStr = WebConfigurationManager.ConnectionStrings["PostGrad"].ToString();

            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand showProgressReportsPROC = new SqlCommand("showProgressReports", conn);
            showProgressReportsPROC.CommandType = CommandType.StoredProcedure;

            showProgressReportsPROC.Parameters.Add(new SqlParameter("@stdID", Session["user"]));


            conn.Open();

            SqlDataReader rdr = showProgressReportsPROC.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                Int32 progNum = rdr.GetInt32(rdr.GetOrdinal("no"));

                DateTime progDate = new DateTime();
                try
                {
                    progDate = rdr.GetDateTime(rdr.GetOrdinal("date"));
                }
                catch (Exception e1)
                {

                }

                Int32 eval = 0;
                try
                {
                    eval = rdr.GetInt32(rdr.GetOrdinal("eval"));
                }
                catch (Exception e1)
                {

                }

                Int32 stateCell = 0;
                try
                {
                    stateCell = rdr.GetInt32(rdr.GetOrdinal("state"));
                }
                catch (Exception e1)
                {

                }

                string desc = "";
                try
                {
                    desc = rdr.GetString(rdr.GetOrdinal("description"));
                }
                catch (Exception e1)
                {

                }

                Int32 thesisCell = rdr.GetInt32(rdr.GetOrdinal("thesisSerialNumber"));

                Int32 superID = 0;
                try
                {
                    superID = rdr.GetInt32(rdr.GetOrdinal("supid"));
                }
                catch (Exception e1)
                {

                }

                HtmlTableRow tRow = new HtmlTableRow();

                HtmlTableCell tb = new HtmlTableCell();
                tb.InnerText = "" + progNum;
                HtmlTableCell tb1 = new HtmlTableCell();
                tb1.InnerText = "" + progDate;
                HtmlTableCell tb2 = new HtmlTableCell();
                tb2.InnerText = "" + eval;
                HtmlTableCell tb10 = new HtmlTableCell();
                tb10.InnerText = "" + stateCell;
                HtmlTableCell tb3 = new HtmlTableCell();
                tb3.InnerText = desc;
                HtmlTableCell tb4 = new HtmlTableCell();
                tb4.InnerText = "" + thesisCell;
                HtmlTableCell tb5 = new HtmlTableCell();
                tb5.InnerText = "" + superID;

                tRow.Controls.Add(tb);
                tRow.Controls.Add(tb1);
                tRow.Controls.Add(tb2);
                tRow.Controls.Add(tb10);
                tRow.Controls.Add(tb3);
                tRow.Controls.Add(tb4);
                tRow.Controls.Add(tb5);


                TablePrgReports.Rows.Add(tRow);


            }

            conn.Close();


            TablePrgReports.Visible = true;
        }

        protected void fill(object sender, EventArgs e)
        {


            string connStr = WebConfigurationManager.ConnectionStrings["PostGrad"].ToString();

            SqlConnection conn = new SqlConnection(connStr);

            Label1.Visible = false;
            Label2.Visible = false;
            Label3.Visible = false;
            thesisNum.Visible = false;
            progDate.Visible = false;
            progNumber.Visible = false;
            AddRep.Visible = false;

            Label4.Visible = true;
            Label5.Visible = true;
            Label6.Visible = true;
            Label7.Visible = true;
            thesNumFillPrg.Visible = true;
            prgNumFillPrg.Visible = true;
            state.Visible = true;
            descTextArea.Visible = true;
            TablePrgReports.Visible = true;
            FillPR.Visible = true;

            string thesisNF = thesNumFillPrg.Text;
            string prgNumF = prgNumFillPrg.Text;
            string stateF = state.Text;
            string descripF = descTextArea.InnerText;

            if (thesisNF.Replace(" ", "") == "" || prgNumF.Replace(" ", "") == "")
            {
                int x = 1410;
                int y = 740;
                Response.Write("<div style='position:absolute;top:" + y.ToString() + "px;left:" + x.ToString() + "px'>Please enter thesis and report numbers!</div>");
            }
            else
            {
                if (!IsAllDigits(thesisNF))
                {
                    int x = 1410;
                    int y = 740;
                    Response.Write("<div style='position:absolute;top:" + y.ToString() + "px;left:" + x.ToString() + "px'>Thesis number has to be all digit numbers!</div>");
                }
                else if (!IsAllDigits(prgNumF))
                {
                    int x = 1410;
                    int y = 740;
                    Response.Write("<div style='position:absolute;top:" + y.ToString() + "px;left:" + x.ToString() + "px'>Report number has to be all digit numbers!</div>");
                }
                else
                {
                    int thesNumInt = Int16.Parse(thesisNF);
                    int prgNumInt = Int16.Parse(prgNumF);

                    SqlCommand validProgressUpdatePROC = new SqlCommand("validProgressUpdate", conn);
                    validProgressUpdatePROC.CommandType = CommandType.StoredProcedure;

                    validProgressUpdatePROC.Parameters.Add(new SqlParameter("@stdID", Session["user"]));
                    validProgressUpdatePROC.Parameters.Add(new SqlParameter("@thesisSer", thesNumInt));
                    validProgressUpdatePROC.Parameters.Add(new SqlParameter("@prgNum", prgNumInt));

                    SqlParameter success = validProgressUpdatePROC.Parameters.Add("@success", SqlDbType.Int);

                    success.Direction = ParameterDirection.Output;

                    conn.Open();
                    validProgressUpdatePROC.ExecuteNonQuery();
                    conn.Close();

                    if (success.Value.ToString() == "0")
                    {
                        int x = 1410;
                        int y = 700;
                        Response.Write("<div style='position:absolute;top:" + y.ToString() + "px;left:" + x.ToString() + "px'>Please enter a valid thesis and report numbers from the table!</div>");
                    }
                    else
                    {
                        SqlCommand FillProgressReportPROC = new SqlCommand("FillProgressReport", conn);
                        FillProgressReportPROC.CommandType = CommandType.StoredProcedure;

                        FillProgressReportPROC.Parameters.Add(new SqlParameter("@thesisSerialNo", thesNumInt));
                        FillProgressReportPROC.Parameters.Add(new SqlParameter("@progressReportNo", prgNumInt));
                        FillProgressReportPROC.Parameters.Add(new SqlParameter("@state", stateF));
                        FillProgressReportPROC.Parameters.Add(new SqlParameter("@description", descripF));
                        FillProgressReportPROC.Parameters.Add(new SqlParameter("@studentID", Session["user"]));

                        conn.Open();
                        FillProgressReportPROC.ExecuteNonQuery();
                        conn.Close();

                        int x = 1410;
                        int y = 700;
                        Response.Write("<div style='position:absolute;top:" + y.ToString() + "px;left:" + x.ToString() + "px'>Added successfully!</div>");

                        fillProgress(null, EventArgs.Empty);


                    }





                }
            }
        }



    }



}