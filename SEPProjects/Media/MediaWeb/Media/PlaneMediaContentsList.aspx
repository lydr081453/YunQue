<%@ Page Language="C#"  MasterPageFile="~/MasterPage.master"  AutoEventWireup="true"  Inherits="Media_PlaneMediaContentsList" Title="平面媒体历史信息"  Codebehind="PlaneMediaContentsList.aspx.cs" %>
<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>   
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" Runat="Server">

        <table width="100%" border="0">
        <tr>
        <td>
            <table width="100%" border="0">
            <tr>
                <td colspan="4" class="headinglist">
                   平面媒体历史列表</td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                <cc4:MyGridView id="dgList" DataKeyNames="id" runat="server"  OnRowDataBound="dgList_RowDataBound" OnSorting="dgList_Sorting" ></cc4:MyGridView>
                </td>
            </tr>
            </table>
            </td>
            </tr>
            <tr>
		<td  colspan="3"  style="text-align:right">
	    	   <input type="button" value="关闭" onclick="javascipt:window.close();return false;" class="widebuttons"/>
	    	<asp:button id="btnBack" runat="server" Visible="false" CssClass="widebuttons" Text="返回"  OnClick="btnBack_Click" ></asp:button>
	   	</td>
	</tr>
       </table>
</asp:Content>
