<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportersContentsList.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="MediaWeb.NewMedia.BaseData.ReportersContentsList" %>

<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<%@ Register TagPrefix="uc" TagName="Experience" Src="~/Media/skins/Experience.ascx" %>
 
 <asp:Content ID="cntHead" ContentPlaceHolderID="contentHead" runat="server">

 </asp:Content>
 
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" Runat="Server">
        <table  width="100%" border="0">
         <tr>
		    <td  colspan="4"  style="text-align:right">
		        
	    	    <input type="button" value="关闭" onclick="javascipt:window.close();return false;" class="widebuttons"/>&nbsp;
	    	    <asp:button id="Button3" runat="server" CssClass="widebuttons" Text="返回" Visible="false" OnClick="btnBack_Click" ></asp:button>
	   	    </td>
	       </tr> 
            <tr>
                <td colspan="4"  class="headinglist">
                    记者历史信息列表</td>
            </tr>
            <tr>
                <td colspan="4" style=" text-align:center;">
                <cc4:MyGridView id="dgList" runat="server" OnRowDataBound="dgList_RowDataBound" OnSorting="dgList_Sorting" ></cc4:MyGridView>
                </td>
            </tr>
           <tr>
		    <td  colspan="4"  style="text-align:right">
		        
	    	    <input type="button" value="关闭" onclick="javascipt:window.close();return false;" class="widebuttons"/>&nbsp;
	    	    <asp:Button id="btnBack" runat="server" CssClass="widebuttons" Text="返回" Visible="false" OnClick="btnBack_Click" ></asp:Button>
	   	    </td>
	       </tr> 
        </table>
        
</asp:Content>