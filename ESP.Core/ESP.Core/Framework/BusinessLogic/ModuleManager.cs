using System;
using System.Collections.Generic;
using System.Text;
using ESP.Framework.DataAccess;
using ESP.Framework.Entity;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ESP.Framework;
using ESP.Framework.DataAccess.Utilities;

namespace ESP.Framework.BusinessLogic
{
    /// <summary>
    /// 模块控制类
    /// </summary>
    public static class ModuleManager
    {
        #region Private Members
        private static IModuleDataProvider GetProvider()
        {
            return ESP.Configuration.ProviderHelper<IModuleDataProvider>.Instance;
        }

        private static void AppendChild(Tree<ModuleInfo> parent, int parentID, int webSiteId, IList<ModuleInfo> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                ModuleInfo mi = list[i];
                if (mi.ParentID == parentID && mi.WebSiteID == webSiteId)
                {
                    long key = MakeKey(mi);
                    Tree<ModuleInfo> t = new Tree<ModuleInfo>(key, mi.ModuleName, mi);
                    parent.AddChild(t);
                    AppendChild(t, mi.ModuleID, webSiteId, list);
                }
            }
        }
        private static Tree<ModuleInfo> GetTree(int webSiteId, IList<ModuleInfo> modules)
        {
            WebSiteInfo webSite = WebSiteManager.Get(webSiteId);
            return GetTree(webSite, modules);
        }

        private static Tree<ModuleInfo> GetTree(WebSiteInfo webSite, IList<ModuleInfo> modules)
        {
            if (webSite == null)
                return null;

            ModuleInfo webSiteModule = new ModuleInfo();
            webSiteModule.WebSiteID = webSite.WebSiteID;
            webSiteModule.ParentID = 0;
            webSiteModule.NodeType = ModuleType.WebSite;
            webSiteModule.NodePath = string.Empty;
            webSiteModule.NodeLevel = 0;
            webSiteModule.Node = string.Empty;
            webSiteModule.ModuleName = webSite.WebSiteName;
            webSiteModule.ModuleID = 0;
            webSiteModule.LastModifierName = webSite.LastModifierName;
            webSiteModule.LastModifier = webSite.LastModifier;
            webSiteModule.LastModifiedTime = webSite.LastModifiedTime;
            webSiteModule.Description = webSite.Description;
            webSiteModule.DefaultPageID = 0;
            webSiteModule.CreatorName = webSite.CreatorName;
            webSiteModule.Creator = webSite.Creator;
            webSiteModule.CreatedTime = webSite.CreatedTime;
            webSiteModule.Ordinal = webSite.Ordinal;

            long key = MakeKey(webSiteModule);

            Tree<ModuleInfo> root = new Tree<ModuleInfo>(key, webSiteModule.ModuleName, webSiteModule);

            if (modules == null || modules.Count == 0)
                return root;

            AppendChild(root, webSiteModule.ModuleID, webSite.WebSiteID, modules);

            return root;
        }


        /// <summary>
        /// 生成模块定义的唯一标识
        /// </summary>
        /// <param name="type">模块定义的类型</param>
        /// <param name="id">模块的ID</param>
        /// <returns>唯一标识</returns>
        public static long MakeKey(ModuleType type, int id)
        {
            ulong t = (ulong)type << 32;
            ulong ulid = (ulong)id;
            return (long) (t | ulid);

        }

        /// <summary>
        /// 生成模块定义的唯一标识
        /// </summary>
        /// <param name="m">模块定义</param>
        /// <returns>唯一标识</returns>
        public static long MakeKey(ModuleInfo m)
        {
            if (m.NodeType == ModuleType.WebSite)
            {
                return MakeKey(m.NodeType, m.WebSiteID);
            }
            else if (m.NodeType == ModuleType.Folder || m.NodeType == ModuleType.Module)
            {
                return MakeKey(m.NodeType, m.ModuleID);
            }

            return 0;
        }

        #endregion



        /// <summary>
        /// 获取指定ID的模块
        /// </summary>
        /// <param name="id">模块ID</param>
        /// <returns>模块信息对象</returns>
        public static ModuleInfo Get(int id)
        {
            return GetProvider().Get(id);
        }

        /// <summary>
        /// 获取指定站点ID的所有模块的列表
        /// </summary>
        /// <param name="webSiteId">站点ID</param>
        /// <returns>模块列表</returns>
        public static IList<ModuleInfo> GetByWebSite(int webSiteId)
        {
            return GetProvider().GetByWebSite(webSiteId);
        }

        /// <summary>
        /// 获取所有模块的列表
        /// </summary>
        /// <returns>模块列表</returns>
        public static IList<ModuleInfo> GetAll()
        {
            return GetProvider().GetAll();
        }

        /// <summary>
        /// 更新模块信息
        /// </summary>
        /// <param name="module">要更新的模块信息</param>
        public static void Update( ModuleInfo module)
        {
            if (module.LastModifiedTime <= NullValues.DateTime)
                module.LastModifiedTime = DateTime.Now;

            if (module.LastModifier <= 0)
                module.LastModifier = UserManager.GetCurrentUserID();


            GetProvider().Update( module);
        }

        /// <summary>
        /// 创建新的模块记录
        /// </summary>
        /// <param name="module">要创建的模块信息对象</param>
        public static void Create( ModuleInfo module)
        {
            if (module.CreatedTime <= NullValues.DateTime)
                module.CreatedTime = DateTime.Now;

            if (module.Creator <= 0)
                module.Creator = UserManager.GetCurrentUserID();

            GetProvider().Create( module);
        }

        /// <summary>
        /// 删除指定ID的模块
        /// </summary>
        /// <param name="id">要删除的模块的ID</param>
        public static void Delete(int id)
        {
            GetProvider().Delete(id);
        }

        /// <summary>
        /// 获取指定ID的站点的所有模块的列表，并以树形结构返回
        /// </summary>
        /// <param name="webSiteId">站点ID</param>
        /// <returns>模块的树形结构对象</returns>
        public static Tree<ModuleInfo> GetWebSiteTree(int webSiteId)
        {
            IList<ModuleInfo> modules = GetByWebSite(webSiteId);

            return GetTree(webSiteId, modules);
        }


        /// <summary>
        /// 获取指定ID的用户拥有访问权的模块的列表，并以树形结构返回
        /// </summary>
        /// <param name="webSiteId">站点的ID</param>
        /// <param name="userId">用户的ID</param>
        /// <returns>模块的树形结构对象</returns>
        public static Tree<ModuleInfo> GetUserTree(int webSiteId, int userId)
        {
            IList<ModuleInfo> modules = GetByUser(webSiteId, userId);
            return GetTree(webSiteId, modules);
        }

        /// <summary>
        /// 获取指定ID的用户拥有访问权的模块的列表
        /// </summary>
        /// <param name="webSiteId">站点的ID</param>
        /// <param name="userId">用户的ID</param>
        /// <returns>模块的列表</returns>
        public static IList<ModuleInfo> GetByUser(int webSiteId, int userId)
        {
            return GetProvider().GetByUser(webSiteId, userId);
        }

        /// <summary>
        /// 获取所有模块的树状列表
        /// </summary>
        /// <returns>所有模块的树形列表</returns>
        /// <remarks>
        /// 树节点对象的 Key 字段为模块类型与模块 ID 的组合
        /// 其中高 32 位为模块类型，低 32 位为模块 ID
        /// </remarks>
        public static Tree<ModuleInfo> GetEntireTree()
        {
            Tree<ModuleInfo> root = new Tree<ModuleInfo>(null, null, null);
            IList<ModuleInfo> list = GetAll();

            IList<WebSiteInfo> sites = WebSiteManager.GetAll();
            foreach (WebSiteInfo site in sites)
            {
                Tree<ModuleInfo> wsNode = GetTree(site, list);

                root.AddChild(wsNode);
            }

            return root;
        }


        /// <summary>
        /// 获取当前用户树型菜单
        /// </summary>
        /// <returns></returns>
        public static List<UserMenuTreeInfo> GetUserMenusTree(int websiteId)
        {
            List<ModuleInfo> list = ModuleManager.GetByUser(websiteId, UserManager.GetCurrentUserID()) as List<ModuleInfo>;
            List<UserMenuTreeInfo> treeList = new List<UserMenuTreeInfo>();
            foreach (var l in list.FindAll(x => x.ParentID == 0))
            {
                //var item = new UserMenuTreeInfo();

                if (l.NodeType == ModuleType.Folder)
                {
                    treeList.Add(new UserMenuTreeInfo()
                    {
                        text = l.ModuleName,
                        isexpand = "false",
                        children = GetChildren2(l.ModuleID, list)
                    });
                }
                else
                {
                    treeList.Add(new UserMenuTreeInfo()
                    {
                        text = l.ModuleName,
                        url = l.DefaultPageUrl
                    });
                }
            }

            return treeList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<UserMenuTreeInfo> GetChildren2(int parentId, List<ModuleInfo> list)
        {
            List<UserMenuTreeInfo> childs = new List<UserMenuTreeInfo>();
            foreach (var item in list.FindAll(x => x.ParentID == parentId))
            {
                if (item.NodeType == ModuleType.Folder)
                {
                    childs.Add(new UserMenuTreeInfo()
                    {
                        text = item.ModuleName,
                        isexpand = "false",
                        children = GetChildren3(item.ModuleID, list)
                    });
                }
                else
                {
                    childs.Add(new UserMenuTreeInfo()
                    {
                        text = item.ModuleName,
                        url = item.DefaultPageUrl,
                    });
                }
            }
            return childs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<UserMenuTreeInfo> GetChildren3(int parentId, List<ModuleInfo> list)
        {
            List<UserMenuTreeInfo> childs = new List<UserMenuTreeInfo>();
            foreach (var item in list.FindAll(x => x.ParentID == parentId))
            {
                childs.Add(new UserMenuTreeInfo()
                {
                    text = item.ModuleName,
                    url = item.DefaultPageUrl
                });
            }
            return childs;
        }

    }
}
