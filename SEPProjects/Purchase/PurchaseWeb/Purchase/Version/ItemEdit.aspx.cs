using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Supplier.BusinessLogic;
using ESP.Supplier.Common;
using ESP.Supplier.Entity;
using System.Text.RegularExpressions;
using System.Data;
using System.Collections;

namespace PurchaseWeb.Purchase.Version
{
    public partial class ItemEdit : System.Web.UI.Page
    {
        public string mName = string.Empty;
        int mSite = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (null != Request["name"] && !string.IsNullOrEmpty(Request["name"]))
            {
                mName = Request["name"].ToString();
            }

            if (null != Request["site"] && !string.IsNullOrEmpty(Request["site"]))
            {
                mSite = int.Parse(Request["site"].ToString());
            }

            if (!IsPostBack)
            {
                LoadPage();
            }
        }

        private void LoadPage()
        {
            if (!string.IsNullOrEmpty(mName))
            {
                List<DataLib.Model.TableModel> modellist = (List<DataLib.Model.TableModel>)Session["TableModel"];
                //DataLib.Model.TableModel model = modellist.FirstOrDefault(x=>x.ID==mName);
                DataLib.Model.TableModel model = modellist.Find(new Predicate<DataLib.Model.TableModel>(delegate(DataLib.Model.TableModel x)
                {
                    return x.ID == mName;
                }));

                t_ID.Text = model.ID;
                t_cnDescription.Text = model.cnDescription;
                t_enDescription.Text = model.enDescription;

                if (model.Type == "文本")
                {
                    if (model.Length < 5000)
                    {
                        t_Type.SelectedValue = "0";
                    }
                    else
                    {
                        t_Type.SelectedValue = "1";
                    }
                }
                else
                {
                    t_Type.SelectedValue = "2";
                }
                t_Control.SelectedValue = model.Control;
                t_Option.SelectedValue = model.Option;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<DataLib.Model.TableModel> dt = (List<DataLib.Model.TableModel>)Session["TableModel"];

            if (!string.IsNullOrEmpty(mName))
            {
                DataLib.Model.TableModel searModel = dt.FirstOrDefault(x => x.ID == t_ID.Text.Trim() && x.ID != mName);
                if (searModel!=null)
                {
                    Response.Write("<script>alert('此属性已经存在。请更换一个名称。');history.back();</script>");
                }
                else
                {
                    DataLib.Model.TableModel model = dt.Find(new Predicate<DataLib.Model.TableModel>(delegate(DataLib.Model.TableModel x)
                    {
                        return x.ID == mName;
                    }));
                    model.ID = t_ID.Text;
                    model.cnDescription = t_cnDescription.Text;
                    model.enDescription = t_enDescription.Text;
                    switch (int.Parse(t_Type.SelectedValue))
                    {
                        case 0:
                            {
                                model.Type = "文本";
                                model.Length = 2000;
                            }
                            break;
                        case 1:
                            {
                                model.Type = "文本";
                                model.Length = 5000;
                            }
                            break;
                        case 2:
                            {
                                model.Type = "数字";
                                model.Length = 2000;
                            }
                            break;
                    }

                    model.Control = t_Control.SelectedValue;
                    model.Option = t_Option.SelectedValue;
                }
            }
            else
            {
                DataLib.Model.TableModel searModel = dt.FirstOrDefault(x => x.ID == t_ID.Text.Trim() && x.ID != mName);
                if (searModel!=null)
                {
                    Response.Write("<script>alert('此项目已经存在。请更换一个属性名称。');history.back();</script>");
                }
                else
                {
                    DataLib.Model.TableModel model = new DataLib.Model.TableModel();
                    model.ID = t_ID.Text;
                    model.cnDescription = t_cnDescription.Text;
                    model.enDescription = t_enDescription.Text;
                    switch (int.Parse(t_Type.SelectedValue))
                    {
                        case 0:
                            {
                                model.Type = "文本";
                                model.Length = 2000;
                            }
                            break;
                        case 1:
                            {
                                model.Type = "文本";
                                model.Length = 5000;
                            }
                            break;
                        case 2:
                            {
                                model.Type = "数字";
                                model.Length = 2000;
                            }
                            break;
                    }

                    model.Control = t_Control.SelectedValue;
                    model.Option = t_Option.SelectedValue;

                    //数据插入位置
                    int tSite = mSite;
                    if (ddlSite.SelectedValue == "0")
                    {
                        tSite = mSite + 1;
                    }

                    if (mSite < 0) mSite = 0;
                    if (mSite >= dt.Count) mSite = dt.Count;

                    dt.Insert(tSite,model);

                }
            }
            Session["TableModel"] = dt;


            string script = " parent.$('#floatBoxBg').hide();parent.$('#floatBox').hide();parent.document.location.reload();";
            ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), script, true);

        }
    }
}
