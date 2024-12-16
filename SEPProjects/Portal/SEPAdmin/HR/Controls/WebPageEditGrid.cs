using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;
using ESP.Utilities;

namespace SEPAdmin.Controls
{
    public class WebPageEditGrid : System.Web.UI.WebControls.WebControl, System.Web.UI.IPostBackDataHandler, System.Web.UI.IPostBackEventHandler
    {
        private bool _isDataReady = false;
        private int _OriginalDefaultPageID = 0;
        private int _DefaultPageID = 0;
        private int _ModuleID = 0;
        private ModuleType _ModuleType = 0;
        private IList<WebPageInfo> _WebPages = null;
        private string _NewItemPathValue = string.Empty;

        private const string ID_NEW_PATH = "newpath";
        private const string ID_DEF_PAGEID = "defpageid";


        private string _DeleteButtonImage = string.Empty;
        private string _AddButtonText = string.Empty;
        private string _DefaultPageHeaderText = string.Empty;
        private string _PathHeaderText = string.Empty;
        private string _NoDefaultPageText = string.Empty;
        private TableStyle _GridStyle = null;
        private TableItemStyle _HeaderStyle = null;
        private TableItemStyle _ItemStyle = null;
        private TableItemStyle _SelectedItemStyle = null;
        private Style _LabelStyle = null;
        private Style _PathBoxStyle = null;
        private Style _ButtonStyle = null;

        #region Styles


        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TableStyle GridStyle
        {
            get
            {
                if (_GridStyle == null)
                    _GridStyle = new TableStyle();
                return _GridStyle;
            }
        }

        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TableItemStyle HeaderStyle
        {
            get
            {
                if (_HeaderStyle == null)
                    _HeaderStyle = new TableItemStyle();
                return _HeaderStyle;
            }
        }

        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TableItemStyle ItemStyle
        {
            get
            {
                if (_ItemStyle == null)
                    _ItemStyle = new TableItemStyle();
                return _ItemStyle;
            }
        }

        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TableItemStyle SelectedItemStyle
        {
            get { return _SelectedItemStyle; }
            set { _SelectedItemStyle = value; }
        }

        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Style LabelStyle
        {
            get
            {
                if (_LabelStyle == null)
                    _LabelStyle = new Style();
                return _LabelStyle;
            }
        }

        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Style PathBoxStyle
        {
            get
            {
                if (_PathBoxStyle == null)
                    _PathBoxStyle = new Style();
                return _PathBoxStyle;
            }
        }

        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Style ButtonStyle
        {
            get
            {
                if (_ButtonStyle == null)
                {
                    _ButtonStyle = new Style();
                }
                return _ButtonStyle;
            }
        }

        public string AddButtonText
        {
            get { return _AddButtonText == null ? string.Empty : _AddButtonText; }
            set { _AddButtonText = value; }
        }

        public string PathHeaderText
        {
            get { return _PathHeaderText; }
            set { _PathHeaderText = value; }
        }

        public string DefaultPageHeaderText
        {
            get { return _DefaultPageHeaderText; }
            set { _DefaultPageHeaderText = value; }
        }

        public string NoDefaultPageText
        {
            get { return _NoDefaultPageText; }
            set { _NoDefaultPageText = value; }
        }

        public string DeleteButtonImage
        {
            get { return _DeleteButtonImage; }
            set { _DeleteButtonImage = value; }
        }

        #endregion

        public void DataBind(ModuleInfo module)
        {
            if (module != null && module.NodeType == ModuleType.Module && module.ModuleID > 0)
            {
                _ModuleType = module.NodeType;
                _ModuleID = module.ModuleID;
                _OriginalDefaultPageID = module.DefaultPageID;
                _DefaultPageID = module.DefaultPageID;
                _WebPages = WebPageManager.GetByModule(module.ModuleID);
                _NewItemPathValue = string.Empty;
                _isDataReady = true;
            }
            else
            {
                _ModuleID = 0;
                _DefaultPageID = 0;
                _WebPages = null;
                _OriginalDefaultPageID = 0;
                _isDataReady = false;
                _NewItemPathValue = string.Empty;
            }
        }

        public int DefaultPageID
        {
            get { return _DefaultPageID; }
            set
            {
                _OriginalDefaultPageID = value;
                _DefaultPageID = value;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            #region [script] function sep_WebPageEditGrid_Delete(pageId, pageUrl)

            string script = @"
    function sep_WebPageEditGrid_Delete(pageId, pageUrl){
        if(!confirm('确定要删除该页面吗？')) return;

        var txtNewPathBoxID = '" + GetID(ID_NEW_PATH) + @"';
        document.getElementById(txtNewPathBoxID).value = pageUrl;

        __doPostBack('" + this.UniqueID + @"', 'del' + pageId);
    }
";
            #endregion

            Page.ClientScript.RegisterClientScriptBlock(typeof(WebPageEditGrid), "sep_WebPageEditGrid_Delete", script, true);
            Page.RegisterRequiresControlState(this);
            Page.RegisterRequiresPostBack(this);
        }

        protected override void LoadControlState(object savedState)
        {
            object[] states = (object[])savedState;
            base.LoadControlState(states[0]);
            _isDataReady = states[1] is bool ? (bool)states[1] : false;
            _ModuleID = states[2] is int ? (int)states[2] : 0;
            _ModuleType = states[3] is ModuleType ? (ModuleType)states[3] : ModuleType.Module;
            _WebPages = states[4] as IList<WebPageInfo>;
            _OriginalDefaultPageID = (states[5] is int) ? (int)states[5] : 0;
        }

        protected override object SaveControlState()
        {
            object baseState = base.SaveControlState();
            return new object[]
            {
                baseState,
                _isDataReady,
                _ModuleID,
                _ModuleType,
                _WebPages,
                _OriginalDefaultPageID
            };
        }

        private void WriteStyle(Style style, HtmlTextWriter writer)
        {
            if (style != null)
                style.AddAttributesToRender(writer);
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            if (!_isDataReady)
                return;

            if (_DeleteButtonImage != null && _DeleteButtonImage.Length > 0 && _DeleteButtonImage[0] == '~')
                _DeleteButtonImage = VirtualPathUtility.ToAbsolute(_DeleteButtonImage);

            writer.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.Div);

            WriteStyle(_GridStyle, writer);
            writer.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.Table);

            RenderHeader(writer);

            RenderEmptyItem(writer);

            if (_WebPages != null)
            {
                foreach (WebPageInfo p in _WebPages)
                {
                    RenderItem(p, writer);
                }
            }

            writer.RenderEndTag(); // End "Table"

            RenderAddItem(writer);

            writer.RenderEndTag();
        }

        private void RenderEmptyItem(HtmlTextWriter writer)
        {
            Style itemStyle = (0 == _OriginalDefaultPageID) ? _SelectedItemStyle : _ItemStyle;
            WriteStyle(itemStyle, writer);
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            
            WriteStyle(itemStyle, writer);
            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            writer.Write("&nbsp;");

            writer.RenderEndTag(); // End "TD"

            WriteStyle(itemStyle, writer);
            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            writer.AddAttribute(System.Web.UI.HtmlTextWriterAttribute.Name, GetUniqueID(ID_DEF_PAGEID));
            writer.AddAttribute(System.Web.UI.HtmlTextWriterAttribute.Type, "radio");
            writer.AddAttribute(System.Web.UI.HtmlTextWriterAttribute.Value, "0");
            if (0 == _DefaultPageID)
                writer.AddAttribute(System.Web.UI.HtmlTextWriterAttribute.Checked, "checked");
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();

            writer.RenderEndTag(); // End "TD"

            WriteStyle(itemStyle, writer);
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.Write(_NoDefaultPageText == null ? "" : _NoDefaultPageText);
            writer.RenderEndTag(); // End "TD"

            writer.RenderEndTag(); // End "TR"
        }

        private void RenderItem(WebPageInfo p, System.Web.UI.HtmlTextWriter writer)
        {
            Style itemStyle = (p.WebPageID == _OriginalDefaultPageID) ? _SelectedItemStyle : _ItemStyle;

            WriteStyle(itemStyle, writer);
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);

            WriteStyle(itemStyle, writer);
            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            // javascript:sep_WebPageEditGrid_Delete(666, "/path/path/file.ext");
            writer.AddAttribute("href", "javascript:sep_WebPageEditGrid_Delete(" + p.WebPageID + "," 
                + ESP.Utilities.JavascriptUtility.QuoteJScriptString(p.AppRelativePath, true, true) + ");");
            writer.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.A);
            writer.AddAttribute("border", "0");
            writer.AddAttribute("src", _DeleteButtonImage);
            writer.AddAttribute("alt", "");
            writer.RenderBeginTag(HtmlTextWriterTag.Img);
            writer.RenderEndTag();
            writer.RenderEndTag(); // End "A"

            writer.RenderEndTag(); // End "TD"

            WriteStyle(itemStyle, writer);
            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            writer.AddAttribute(System.Web.UI.HtmlTextWriterAttribute.Name, GetUniqueID(ID_DEF_PAGEID));
            writer.AddAttribute(System.Web.UI.HtmlTextWriterAttribute.Type, "radio");
            writer.AddAttribute(System.Web.UI.HtmlTextWriterAttribute.Value, p.WebPageID.ToString());
            if (p.WebPageID == _DefaultPageID)
                writer.AddAttribute(System.Web.UI.HtmlTextWriterAttribute.Checked, "checked");
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();

            writer.RenderEndTag(); // End "TD"

            WriteStyle(itemStyle, writer);
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.Write(p.AppRelativePath);
            writer.RenderEndTag(); // End "TD"

            writer.RenderEndTag(); // End "TR"
        }


        private string GetID(string suffix)
        {
            return this.ClientID + "_" + suffix;
        }

        private string GetUniqueID(string suffix)
        {
            return this.UniqueID + this.IdSeparator + suffix;
        }


        private void RenderAddItem(System.Web.UI.HtmlTextWriter writer)
        {
            string pathCtrlID = GetID(ID_NEW_PATH);

            writer.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.Div);

            WriteStyle(_LabelStyle, writer);
            writer.AddAttribute(System.Web.UI.HtmlTextWriterAttribute.For, pathCtrlID);
            writer.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.Label);
            writer.Write(_PathHeaderText + ":");
            writer.RenderEndTag();


            WriteStyle(_PathBoxStyle, writer);
            writer.AddAttribute("id", pathCtrlID);
            writer.AddAttribute("name", GetUniqueID(ID_NEW_PATH));
            writer.AddAttribute("type", "text");
            writer.AddAttribute("value", _NewItemPathValue);
            writer.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.Input);
            writer.RenderEndTag();

            writer.Write("&nbsp;");

            WriteStyle(_ButtonStyle, writer);
            writer.AddAttribute("href", "javascript:__doPostBack('" + this.UniqueID + "','add');");
            writer.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.A);
            writer.Write(_AddButtonText);
            writer.RenderEndTag();

            writer.RenderEndTag(); /* End "Div" */
        }

        private void RenderHeader(System.Web.UI.HtmlTextWriter writer)
        {
            WriteStyle(_HeaderStyle, writer);
            writer.RenderBeginTag("tr");

            WriteStyle(_HeaderStyle, writer);
            writer.RenderBeginTag("td");
            writer.Write("&nbsp;");
            writer.RenderEndTag();


            WriteStyle(_HeaderStyle, writer);
            writer.RenderBeginTag("td");
            writer.Write(_DefaultPageHeaderText);
            writer.RenderEndTag();

            WriteStyle(_HeaderStyle, writer);
            writer.RenderBeginTag("td");
            writer.Write(_PathHeaderText);
            writer.RenderEndTag();

            writer.RenderEndTag();
        }

        protected override System.Web.UI.Control FindControl(string id, int pathOffset)
        {
            return this;
        }

        #region IPostBackDataHandler 成员

        public bool LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection postCollection)
        {
            if (!_isDataReady)
                return false;

            string value = postCollection[postDataKey];
            if(value == null)
                value = string.Empty;
            else
                value = value.Trim();

            if (postDataKey.EndsWith(ID_NEW_PATH))
            {
                _NewItemPathValue = value;
            }
            else if(postDataKey.EndsWith(ID_DEF_PAGEID) && this._WebPages != null)
            {
                int defPageID = 0;
                if (!int.TryParse(value, out defPageID))
                    defPageID = 0;

                _DefaultPageID = 0;
                foreach (WebPageInfo wp in this._WebPages)
                {
                    if (wp.WebPageID == defPageID)
                    {
                        _DefaultPageID = defPageID;
                        break;
                    }
                }
            }

            return false;
        }

        public void RaisePostDataChangedEvent()
        {
        }

        #endregion

        private void OnAddItem(string cmd, string arg)
        {
            if (_NewItemPathValue.Length != 0)
            {
                ESP.Web.UI.PageBase sepPage = Page as ESP.Web.UI.PageBase;
                WebPageInfo page = new WebPageInfo();
                page.AppRelativePath = _NewItemPathValue;
                page.CreatedTime = DateTime.Now;
                page.Creator = UserManager.GetCurrentUserID();
                page.Description = "";
                page.ModuleID = _ModuleID;
                //////
                WebPageManager.Create( page);

                this._WebPages = WebPageManager.GetByModule(_ModuleID);
                _NewItemPathValue = string.Empty;

            }
            else
            {
            }
        }

        private void OnDeleteItem(string cmd, string arg)
        {
            int id;
            if (int.TryParse(arg, out id) && id > 0)
            {
                WebPageManager.Delete(id);
                this._WebPages = WebPageManager.GetByModule(_ModuleID);
            }
        }

        #region IPostBackEventHandler 成员

        public void RaisePostBackEvent(string eventArgument)
        {
            if (!_isDataReady || eventArgument == null || eventArgument.Length < 3)
                return;

            string cmd = eventArgument.Substring(0, 3);
            string arg = eventArgument.Substring(3);
            switch (cmd)
            {
                case "add":
                    OnAddItem(cmd, arg);
                    break;
                case "del":
                    OnDeleteItem(cmd, arg);
                    break;
            }

        }

        #endregion

    }

}
