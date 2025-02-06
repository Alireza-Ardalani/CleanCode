using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace WebApplication2
{
    public partial class test : System.Web.UI.Page
    {
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!this.IsPostBack)
            {
                BoundField bfield = new BoundField();
                bfield.HeaderText = "CleanCodePrinciple";
                bfield.DataField = "Name";
                GridView1.Columns.Add(bfield);

                BoundField tfield = new BoundField();
                tfield.HeaderText = "count";
                GridView1.Columns.Add(tfield);

                tfield = new BoundField();
                tfield.HeaderText = "complete information";
                GridView1.Columns.Add(tfield);
            }
            dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[3] { new DataColumn("more", typeof(string)),
                        new DataColumn("Name", typeof(string)),
                        new DataColumn("count", typeof(string)) });

            dt.Rows.Add("line 44 .....................", "abstraction smell", "5");
            dt.Rows.Add("line 45 .....................", "modularization smell", "77");

            GridView1.DataSource = dt;
            GridView1.DataBind();

        }

        //private void BindGrid()
        //{
        //    dt = new DataTable();
        //    dt.Columns.AddRange(new DataColumn[3] { new DataColumn("more", typeof(string)),
        //                new DataColumn("Name", typeof(string)),
        //                new DataColumn("count", typeof(string)) });

        //    dt.Rows.Add("line 44 .....................", "abstraction smell", "5");
        //    dt.Rows.Add("line 45 .....................", "modularization smell", "77");

        //    GridView1.DataSource = dt;
        //    GridView1.DataBind();
        //}
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label txtmore = new Label();
                txtmore.ID = "txtmore";
                txtmore.Text = (e.Row.DataItem as DataRowView).Row["count"].ToString();
                e.Row.Cells[1].Controls.Add(txtmore);

                LinkButton lnkView = new LinkButton();
                lnkView.ID = "lnkView";
                lnkView.Text = "View";

                lnkView.Click += ViewDetails;
                lnkView.CommandArgument = (e.Row.DataItem as DataRowView).Row["more"].ToString();
                e.Row.Cells[2].Controls.Add(lnkView);
            }



        }
        protected void ViewDetails(object sender, EventArgs e)
        {
            LinkButton lnkView = (sender as LinkButton);
            GridViewRow row = (lnkView.NamingContainer as GridViewRow);
            string m = lnkView.CommandArgument;



            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('more information : " + m + "')", true);
        }

    }
}
