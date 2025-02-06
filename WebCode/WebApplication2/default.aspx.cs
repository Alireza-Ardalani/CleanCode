using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (this.FileUpload1.HasFile)
            {
                String FileName = FileUpload1.FileName;
                String ServerPath = Server.MapPath("inputcode");

                FileUpload1.PostedFile.SaveAs(ServerPath + "\\" + FileName);

                this.Session.Add("id", FileName);

                Response.Redirect("result.aspx");

            }
            else
            {
                Response.Write("<script>alert(\"فایلی انتخاب نشده است\");</script>");

            }
        }
    }
}