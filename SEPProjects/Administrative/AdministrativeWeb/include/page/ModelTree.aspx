<%@ Page Language="C#" AutoEventWireup="True" Inherits="include_page_ModelTree" EnableViewState="false" Codebehind="ModelTree.aspx.cs" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server">
    <title>ModuleBrow</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../../css/syshomepage.css" rel="stylesheet" type="text/css" />
    <link href="../../css/treeStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../css/navBarstyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form runat="server" id="form">
    <asp:ScriptManager runat="server" ID="scriptManager"></asp:ScriptManager>
    
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td>
	        <!-- 系统模块节点数 -->
            <asp:UpdatePanel ID="upTreeView" runat="server" RenderMode="Block">
                <ContentTemplate>
                    <ComponentArt:NavBar
				        ID="navBarModel"
				        RunAt="server"
				        ExpandSinglePath="true"
				        ShowScrollBar="false"
				        CssClass="nb"
				        DefaultItemLookId="ItemLook"
				        DefaultGroupCssClass="grp">
				        <ItemLooks>
					        <ComponentArt:ItemLook LookId="ItemLook" CssClass="item" HoverCssClass="item-h" ExpandedCssClass="item-x" />
				        </ItemLooks>
				        <ClientTemplates>
					        <ComponentArt:ClientTemplate ID="TopItemTemplate">
						        <div class="top">
							        <div class="icon ## DataItem.getProperty('Value'); ##"></div>
							        <div class="txt">## DataItem.get_text(); ##</div>
						        </div>
					        </ComponentArt:ClientTemplate>

					        <ComponentArt:ClientTemplate ID="SubItemTemplate">
						        <div class="sub">
							        <div class="icon ## DataItem.getProperty('Value'); ##"></div>
							        <div class="txt">## DataItem.get_text(); ##</div>
						        </div>
					        </ComponentArt:ClientTemplate>

					        <ComponentArt:ClientTemplate ID="SubEndItemTemplate">
						        <div class="## DataItem.getProperty('Value'); ##"><span></span></div>
					        </ComponentArt:ClientTemplate>
				        </ClientTemplates>
		            </ComponentArt:NavBar>
                </ContentTemplate>
            </asp:UpdatePanel>
	    </td>
	  </tr>
	</table>
	</form>
</body>
</html>
