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
    public class PermissionDefinitionEditGrid : System.Web.UI.WebControls.WebControl, System.Web.UI.IPostBackDataHandler, System.Web.UI.IPostBackEventHandler
    {
        #region Constants

        private const string ID_NEW_NAME = "newname";
        private const string ID_NEW_DESC = "newdesc";

        #endregion

        #region private fields

        private bool _isDataReady = false;
        private int _EntityID;
        private EntityType _EntityType;
        private IList<PermissionDefinitionInfo> _Definitions = null;
        private string _NewNameValue = string.Empty;
        private string _NewDescriptionValue = string.Empty;

        private string _DeleteButtonImage = string.Empty;
        private string _AddButtonText = string.Empty;
        private string _NameHeaderText = string.Empty;
        private string _DescriptionHeaderText = string.Empty;
        private TableStyle _GridStyle = null;
        private TableItemStyle _HeaderStyle = null;
        private TableItemStyle _ItemStyle = null;
        private Style _LabelStyle = null;
        private Style _NameBoxStyle = null;
        private Style _DescriptionBoxStyle = null;
        private Style _ButtonStyle = null;

        #endregion


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
        public Style NameBoxStyle
        {
            get
            {
                if (_NameBoxStyle == null)
                    _NameBoxStyle = new Style();
                return _NameBoxStyle;
            }
        }

        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Style DescriptionBoxStyle
        {
            get
            {
                if (_DescriptionBoxStyle == null)
                    _DescriptionBoxStyle = new Style();
                return _DescriptionBoxStyle;
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

        public string NameHeaderText
        {
            get { return _NameHeaderText; }
            set { _NameHeaderText = value; }
        }

        public string DescriptionHeaderText
        {
            get { return _DescriptionHeaderText; }
            set { _DescriptionHeaderText = value; }
        }

        public string DeleteButtonImage
        {
            get { return _DeleteButtonImage; }
            set { _DeleteButtonImage = value; }
        }

        #endregion

        public void DataBind(EntityType entityType, int entityId)
        {
            if (entityId > 0)
            {
                _EntityType = entityType;
                _EntityID = entityId;
                _Definitions = PermissionManager.GetDefinitions(entityType, entityId);
                _NewNameValue = string.Empty;
                _NewDescriptionValue = string.Empty;
                _isDataReady = true;
            }
            else
            {
                _EntityType = 0;
                _EntityID = 0;
                _Definitions = null;
                _NewNameValue = string.Empty;
                _NewDescriptionValue = string.Empty;
                _isDataReady = false;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            #region [script] function sep_PermissionDefinitionEditGrid_Delete (permissionDefinitionId, name, description)
            string script = @"
    function sep_PermissionDefinitionEditGrid_Delete(permissionDefinitionId, name, description){
        if(!confirm('确定要删除该权限定义吗？')) return;

        var txtNewNameTexBoxID = '" + GetID(ID_NEW_NAME) + @"';
        var txtNewDescTexBoxID = '" + GetID(ID_NEW_DESC) + @"';

        document.getElementById(txtNewNameTexBoxID).value = name;
        document.getElementById(txtNewDescTexBoxID).value = description;
        __doPostBack('" + this.UniqueID + @"', 'del' + permissionDefinitionId);
    }
";
            #endregion

            Page.ClientScript.RegisterClientScriptBlock(typeof(WebPageEditGrid), "sep_PermissionDefinitionEditGrid_Delete", script, true);
            Page.RegisterRequiresControlState(this);
            Page.RegisterRequiresPostBack(this);
        }

        protected override void LoadControlState(object savedState)
        {
            object[] states = (object[])savedState;
            base.LoadControlState(states[0]);
            _isDataReady = states[1] is bool ? (bool)states[1] : false;
            _EntityID = states[2] is int ? (int)states[2] : 0;
            _EntityType = states[3] is EntityType ? (EntityType)states[3] : 0;
            _Definitions = states[4] as IList<PermissionDefinitionInfo>;
        }

        protected override object SaveControlState()
        {
            object baseState = base.SaveControlState();
            return new object[]
            {
                baseState,
                _isDataReady,
                _EntityID,
                _EntityType,
                _Definitions
            };
        }

        private string GetID(string suffix)
        {
            return this.ClientID + "_" + suffix;
        }

        private string GetUniqueID(string suffix)
        {
            return this.UniqueID + this.IdSeparator + suffix;
        }

        protected override System.Web.UI.Control FindControl(string id, int pathOffset)
        {
            return this;
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

            if (_Definitions != null)
            {
                foreach (PermissionDefinitionInfo pd in _Definitions)
                {
                    RenderItem(pd, writer);
                }
            }

            writer.RenderEndTag(); // End "Table"

            RenderAddItem(writer);

            writer.RenderEndTag();
        }

        private void RenderItem(PermissionDefinitionInfo pd, System.Web.UI.HtmlTextWriter writer)
        {
            WriteStyle(_ItemStyle, writer);
            writer.RenderBeginTag("tr");

            WriteStyle(_ItemStyle, writer);
            writer.RenderBeginTag("td");

            // javascript:sep_PermissionDefinitionEditGrid_Delete(666, "Name", "描述(名字)");
            writer.AddAttribute("href", "javascript:sep_PermissionDefinitionEditGrid_Delete("
                + pd.PermissionDefinitionID + ","
                + ESP.Utilities.JavascriptUtility.QuoteJScriptString(pd.PermissionName, true, true) + ","
                + JavascriptUtility.QuoteJScriptString(pd.Description, true, true) + ");");

            writer.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.A);
            writer.AddAttribute("border", "0");
            writer.AddAttribute("src", _DeleteButtonImage);
            writer.AddAttribute("alt", "");
            writer.RenderBeginTag("img");
            writer.RenderEndTag();
            writer.RenderEndTag(); // End "A"

            writer.RenderEndTag(); // End "TD"


            WriteStyle(_ItemStyle, writer);
            writer.RenderBeginTag("td");
            writer.Write(pd.PermissionName);
            writer.RenderEndTag(); // End "TD"


            WriteStyle(_ItemStyle, writer);
            writer.RenderBeginTag("td");
            writer.Write(pd.Description);
            writer.RenderEndTag(); // End "TD"

            writer.RenderEndTag(); // End "TR"
        }

        private void RenderAddItem(System.Web.UI.HtmlTextWriter writer)
        {
            string nameCtrlID = GetID(ID_NEW_NAME);
            string descCtrlID = GetID(ID_NEW_DESC);

            writer.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.Div);


            WriteStyle(_LabelStyle, writer);
            writer.AddAttribute(System.Web.UI.HtmlTextWriterAttribute.For, nameCtrlID);
            writer.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.Label);
            writer.Write(_NameHeaderText + ":");
            writer.RenderEndTag();

            WriteStyle(_NameBoxStyle, writer);
            writer.AddAttribute("id", nameCtrlID);
            writer.AddAttribute("name", GetUniqueID(ID_NEW_NAME));
            writer.AddAttribute("type", "text");
            writer.AddAttribute("value", _NewNameValue);
            writer.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.Input);
            writer.RenderEndTag();

            writer.Write("&nbsp;");

            WriteStyle(_LabelStyle, writer);
            writer.AddAttribute(System.Web.UI.HtmlTextWriterAttribute.For, nameCtrlID);
            writer.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.Label);
            writer.Write(_DescriptionHeaderText + ":");
            writer.RenderEndTag();

            writer.AddAttribute(System.Web.UI.HtmlTextWriterAttribute.Class, "sep_grid_footer_textbox_2");
            writer.AddAttribute("id", descCtrlID);
            writer.AddAttribute("name", GetUniqueID(ID_NEW_DESC));
            writer.AddAttribute("type", "text");
            writer.AddAttribute("value", _NewDescriptionValue);
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
            writer.Write(_NameHeaderText);
            writer.RenderEndTag();

            WriteStyle(_HeaderStyle, writer);
            writer.RenderBeginTag("td");
            writer.Write(_DescriptionHeaderText);
            writer.RenderEndTag();

            writer.RenderEndTag();
        }

        #region IPostBackDataHandler 成员

        public bool LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection postCollection)
        {
            if (!_isDataReady)
                return false;

            string value = postCollection[postDataKey];
            if (value == null)
                value = string.Empty;
            else
                value = value.Trim();

            if (postDataKey.EndsWith(ID_NEW_NAME))
                _NewNameValue = value;
            else if (postDataKey.EndsWith(ID_NEW_DESC))
                _NewDescriptionValue = value;

            return false;
        }

        public void RaisePostDataChangedEvent()
        {
        }

        #endregion

        private void OnAddItem(string cmd, string arg)
        {
            if (_NewNameValue.Length != 0 && _NewDescriptionValue.Length != 0)
            {
                ESP.Web.UI.PageBase sepPage = Page as ESP.Web.UI.PageBase;

                PermissionDefinitionInfo pd = new PermissionDefinitionInfo();
                pd.CreatedTime = DateTime.Now;
                pd.Creator = UserManager.GetCurrentUserID();
                pd.Description = _NewDescriptionValue;
                pd.PermissionName = _NewNameValue;
                pd.ReferredEntityID = _EntityID;
                pd.ReferredEntityType = _EntityType;
                //////
                PermissionManager.AddDefinition( pd);

                //PermissionInfo p = new PermissionInfo();
                //p.OwnerID = 1;
                //p.OwnerType = PermissionOwnerTypes.Role;
                //p.PermissionDefinitionID = pd.PermissionDefinitionID;
                //p.Creator = UserController.GetCurrentUserID();
                //p.CreatedTime = DateTime.Now;
                //PermissionController.Add(ref p);

                _NewNameValue = string.Empty;
                _NewDescriptionValue = string.Empty;

                this._Definitions = PermissionManager.GetDefinitions(_EntityType, _EntityID);
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
                PermissionManager.RemoveDefinition(id);
                this._Definitions = PermissionManager.GetDefinitions(_EntityType, _EntityID);
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
