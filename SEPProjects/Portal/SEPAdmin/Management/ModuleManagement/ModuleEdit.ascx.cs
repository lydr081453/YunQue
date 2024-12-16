using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using ESP.Web.UI;
using System.Text;
using System.Web.Script.Serialization;
using SEPAdmin.Controls;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;

namespace SEPAdmin.Management.ModuleManagement
{
    public partial class ModuleEdit : System.Web.UI.UserControl
    {
        ModuleInfo _Module;

        private void ParseKey(ulong moduleKey, out int moduleId, out ModuleType type)
        {
            moduleId = (int)(moduleKey & 0xffffffff);
            type = (ModuleType)(moduleKey >> 32);
        }

        private ulong MakeKey(int moduleId, ModuleType type)
        {
            ulong t = (ulong)type << 32;
            ulong id = (ulong)moduleId;
            return t | id;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Page.RegisterRequiresControlState(this);
        }

        private string GetModuleTypeString(ModuleType type)
        {
            switch (type)
            {
                case ModuleType.Module:
                    return "模块";
                case ModuleType.Folder:
                    return "文件夹";
                case ModuleType.WebSite:
                    return "站点";
                default:
                    return "未知";
            }
        }

        protected override object SaveControlState()
        {
            object obj = base.SaveControlState();
            return new object[] { obj, _Module };
        }

        protected override void LoadControlState(object savedState)
        {
            object[] states = (object[])savedState;
            base.LoadControlState(states[0]);
            _Module = (ModuleInfo)states[1];
        }


        public bool BindData(ulong key)
        {
            int mid;
            ModuleType type;
            ParseKey(key, out mid, out type);
            if (mid <= 0 && type == ModuleType.WebSite)
                return false;

            if (type == ModuleType.WebSite)
            {
                WebSiteInfo webSite = WebSiteManager.Get(mid);
                if (webSite == null)
                    return false;

                _Module = new ModuleInfo();
                _Module.NodeType = type;
                _Module.ModuleID = 0;
                _Module.WebSiteID = mid;

                this.txtModuleName.Text = webSite.WebSiteName;
                this.txtDescription.Text = webSite.Description;
                this.txtModuleType.Text = GetModuleTypeString(type);
                this.txtModuleID.Text = mid.ToString();
                this.txtOrdinal.Text = webSite.Ordinal.ToString();
            }
            else if (type == ModuleType.Module || type == ModuleType.Folder)
            {
                if (mid <= 0)
                {
                    _Module = new ModuleInfo();
                    _Module.ModuleID = 0;
                    _Module.NodeType = type;
                }
                else
                {
                    _Module = ModuleManager.Get(mid);
                    if (_Module == null)
                        return false;
                }

                this.txtModuleName.Text = _Module.ModuleName;
                this.txtDescription.Text = _Module.Description;
                this.txtModuleType.Text = GetModuleTypeString(_Module.NodeType);
                this.txtModuleID.Text = _Module.ModuleID.ToString();
                this.txtOrdinal.Text = _Module.Ordinal.ToString();
            }

            UpdateControlStatus();

            return true;
        }

        private void UpdateControlStatus()
        {

            WebPageEditGrid1.DataBind(_Module);

            if (_Module != null && _Module.NodeType == ModuleType.Module)
                PermissionDefinitionEditGrid1.DataBind(EntityType.Module, _Module.ModuleID);
            else
                PermissionDefinitionEditGrid1.DataBind(EntityType.Module, 0);

            if (_Module.NodeType == ModuleType.WebSite)
            {
                txtModuleName.ReadOnly = true;
                txtDescription.ReadOnly = true;
                txtOrdinal.ReadOnly = true;

                btnAddFolder.Visible = true;
                btnAddModule.Visible = true;
                btnDelete.Visible = false;
                btnSave.Visible = false;
            }
            else if (_Module.NodeType == ModuleType.Folder)
            {
                txtModuleName.ReadOnly = false;
                txtDescription.ReadOnly = false;
                txtOrdinal.ReadOnly = false;

                btnAddFolder.Visible = _Module.ModuleID != 0;
                btnAddModule.Visible = _Module.ModuleID != 0;
                btnDelete.Visible = _Module.ModuleID != 0;
                btnSave.Visible = true;
            }
            else if (_Module.NodeType == ModuleType.Module)
            {
                txtModuleName.ReadOnly = false;
                txtDescription.ReadOnly = false;
                txtOrdinal.ReadOnly = false;

                btnAddFolder.Visible = false;
                btnAddModule.Visible = false;
                btnDelete.Visible = _Module.ModuleID != 0;
                btnSave.Visible = true;
            }
            else
            {
                txtModuleName.ReadOnly = true;
                txtDescription.ReadOnly = true;
                txtOrdinal.ReadOnly = true;

                btnAddFolder.Visible = false;
                btnAddModule.Visible = false;
                btnDelete.Visible = false;
                btnSave.Visible = false;
            }
        }

        public void UpdateModule()
        {
            ESP.Web.UI.PageBase sepPage = Page as ESP.Web.UI.PageBase;

            _Module.ModuleName = txtModuleName.Text;
            _Module.DefaultPageID = WebPageEditGrid1.DefaultPageID;
            _Module.Description = txtDescription.Text;
            _Module.Ordinal = int.Parse(txtOrdinal.Text.Trim());
            if (_Module.ModuleID > 0)
            {
                _Module.LastModifiedTime = DateTime.Now;
                _Module.LastModifier = (sepPage == null) ? 0 : sepPage.UserID;
                //////
                ModuleManager.Update( _Module);

                if (ModuleTreeChanged != null)
                    ModuleTreeChanged(this, new ModuleTreeChangedEventArgs(ModuleTreeChangedEventType.Updated,
                        _Module.ModuleID, _Module.ParentID, _Module.WebSiteID));

            }
            else
            {
                _Module.CreatedTime = DateTime.Now;
                _Module.Creator = (sepPage == null) ? 0 : sepPage.UserID;
                //////
                ModuleManager.Create(_Module);

                if (ModuleTreeChanged != null)
                    ModuleTreeChanged(this, new ModuleTreeChangedEventArgs(ModuleTreeChangedEventType.Created,
                        _Module.ModuleID, _Module.ParentID, _Module.WebSiteID));
            }

            UpdateControlStatus();
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (_Module == null || _Module.NodeType == ModuleType.WebSite)
                return;

            UpdateModule();
        }

        protected void btnAddFolder_Click(object sender, EventArgs e)
        {
            int moduleId = _Module.ModuleID;
            int webSiteId = _Module.WebSiteID;
            BindData(MakeKey(0, ModuleType.Folder));
            _Module.ParentID = moduleId;
            _Module.WebSiteID = webSiteId;
        }

        protected void btnAddModule_Click(object sender, EventArgs e)
        {
            int moduleId = _Module.ModuleID;
            int webSiteId = _Module.WebSiteID;
            BindData(MakeKey(0, ModuleType.Module));
            _Module.ParentID = moduleId;
            _Module.WebSiteID = webSiteId;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int moduleId = _Module.ModuleID;
            int webSiteId = _Module.WebSiteID;
            int parentId = _Module.ParentID;

            ModuleManager.Delete(moduleId);

            if(ModuleTreeChanged != null)
                ModuleTreeChanged(this, new ModuleTreeChangedEventArgs(ModuleTreeChangedEventType.Deleted, moduleId, parentId, webSiteId));
        }

        public event ModuleTreeChangedEventHandler ModuleTreeChanged;
    }

    public delegate void ModuleTreeChangedEventHandler(object sender, ModuleTreeChangedEventArgs args);
    public class ModuleTreeChangedEventArgs : EventArgs
    {
        ModuleTreeChangedEventType _EventType;
        int _ModuleId;
        int _ParentId;
        int _WebSiteID;

        public int WebSiteID
        {
            get { return _WebSiteID; }
            set { _WebSiteID = value; }
        }

        public ModuleTreeChangedEventType EventType
        {
            get { return _EventType; }
            set { _EventType = value; }
        }

        public int ModuleID
        {
            get { return _ModuleId; }
            set { _ModuleId = value; }
        }

        public int ParentID
        {
            get { return _ParentId; }
            set { _ParentId = value; }
        }

        public ModuleTreeChangedEventArgs(ModuleTreeChangedEventType eventType, int moduleId, int parentId, int webSiteID)
        {
            _EventType = eventType;
            _ModuleId  = moduleId;
            _ParentId = parentId;
            _WebSiteID = webSiteID;
        }
    }

    public enum ModuleTreeChangedEventType
    {
        Created,
        Updated,
        Deleted
    }
}