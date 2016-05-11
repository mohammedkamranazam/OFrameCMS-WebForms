using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectJKL
{
    public partial class UserDetails : System.Web.UI.Page
    {

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.Util.Utilities.GetMainThemeFile();
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}