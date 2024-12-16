using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
namespace AdministrativeWeb
{
    public partial class MySqlTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetShunyaDateInfoBJ();
            }
        }

        private void GetShunyaDateInfoBJ()
        {
            string M_str_sqlcon = "server=172.16.4.200;user id=admin;password=admin;database=nav";
            DataSet myds = new DataSet();
            MySqlConnection mycon = new MySqlConnection();
            mycon.ConnectionString = M_str_sqlcon;
            
            try
            {
                string sql = "select * from select_test_table";
                // 连接打卡服务器
                mycon.Open();
                MySqlDataAdapter mda = new MySqlDataAdapter(sql, mycon);

                mda.Fill(myds, "table1");
                
              

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                mycon.Close();
            }

            this.gv.DataSource = myds.Tables[0];
            this.gv.DataBind();
        }

    }
}