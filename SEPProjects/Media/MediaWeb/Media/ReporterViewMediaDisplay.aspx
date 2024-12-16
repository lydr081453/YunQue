<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Media_ReporterViewMediaDisplay" Title="媒体详细信息" Codebehind="ReporterViewMediaDisplay.aspx.cs" %>
<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<%@ Register TagPrefix="uc" TagName="DABMediaDisplay" Src="~/Media/skins/DABMediaDisplay.ascx" %>
<%@ Register TagPrefix="uc" TagName="PlaneMediaDisplay" Src="~/Media/skins/PlaneMediaDisplay.ascx" %>
<%@ Register TagPrefix="uc" TagName="TvMediaDisplay" Src="~/Media/skins/TvMediaDisplay.ascx" %>
<%@ Register TagPrefix="uc" TagName="WebMediaDisplay" Src="~/Media/skins/WebMediaDisplay.ascx" %>
<%@ Register TagPrefix="uc" TagName="Experience" Src="~/Media/skins/Experience.ascx" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="contentHead" runat="server">

 </asp:Content>
 
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" Runat="Server">
    <table style="width: 100%;" border="0" >
                        
            <tr>
               <td colspan="4">
                      <asp:Panel ID="panelMediaDisplay" runat="server" Height="0px"></asp:Panel>               
               </td>
           </tr>
            <tr>
	            <td class="heading" colspan="3">相关记者列表</td>
	        </tr>
	        <tr>
		        <td align="center" colspan="3"><cc4:MyGridView  id="dgList" runat="server" OnRowDataBound="dgList_RowDataBound"></cc4:MyGridView></td>
	        </tr>   
               
            <tr>
                <td style="text-align: right">
                     <input type="button" value="关闭" onclick="javascipt:window.close();return false;" class="widebuttons"/>
                  
                </td>
            </tr> 
	
  </table>
</asp:Content>
