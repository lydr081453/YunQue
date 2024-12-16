<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" Inherits="Client_ClientContentsDetail"
    Title="客户详细信息" Codebehind="ClientContentsDetail.aspx.cs" %>

<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<%@ Register TagPrefix="cc2" Namespace="MyControls" Assembly="MyControls" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="contentHead" runat="server">

    <script type="text/javascript">
    function returnurl()
        {
                var hidurl=document.getElementById("<% = hidUrl.ClientID %>");
    window.location = hidurl.value;
        }
function PrintPage()
  {  
    window.print();
  }
    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">
    <input type="hidden" id="hidUrl" runat="server" />
    <table width="100%" border="1" class="tableForm">
        <tr>
            <td class="menusection-Packages" colspan="4">
                客户历史数据详细信息
            </td>
        </tr>
        <tr>
            <td class="menusection-Packages">
                当前版本号：
            </td>
            <td colspan="3" class="menusection-Packages">
                <asp:Label ID="labVersion" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="menusection-Packages" style="width: 20%">
                最后更新人：
            </td>
            <td class="menusection-Packages" style="width: 30%">
                <asp:Label ID="labLastModifyUser" runat="server"></asp:Label>
            </td>
            <td class="menusection-Packages" style="width: 20%">
                最后更新时间：
            </td>
            <td class="menusection-Packages" style="width: 30%">
                <asp:Label ID="labLastModifyDate" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td class="heading" colspan="4">
                <asp:Label ID="labClientName" runat="server">公司名称</asp:Label>
            </td>
        </tr>
        <tr>
            <td rowspan="7" style="width: 15%; text-align: center;" class="oddrow">
                <div>
                    <a href="#" onclick="return hs.htmlExpand(this, { contentId: 'photo-div' } )"
                        class="highslide">
                        <asp:Image runat="server" ID="imgLogo" /></a>
                    <div class="highslide-html-content" id="photo-div">
                        <div class="highslide-header">
                            <ul>
                                <li class="highslide-move"><a href="#" onclick="return false">移动</a> </li>
                                <li class="highslide-close"><a href="#" onclick="return hs.close(this)">关闭</a> </li>
                            </ul>
                        </div>
                        <div class="highslide-body">
                            <asp:Image ID="imgTitleFull" runat="server" />
                        </div>
                        <div class="highslide-footer">
                            <div>
                                <span class="highslide-resize" title="Resize"><span></span></span>
                            </div>
                        </div>
                    </div>
                </div>
            </td>
            <td style="width: 20%" class="oddrow">
                中文全称：
            </td>
            <td style="width: 50%" class="oddrow-l" colspan="2">
                <asp:Label ID="labChFullName" runat="server" Width="90%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 15%" class="oddrow">
                中文简称：
            </td>
            <td style="width: 70%" class="oddrow-l"  colspan="2">
                <asp:Label ID="labChShortName" runat="server" Width="90%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                英文全称：
            </td>
            <td class="oddrow-l" colspan="2">
                <asp:Label ID="labEnFullName" runat="server" Width="90%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                英文简称：
            </td>
            <td class="oddrow-l" colspan="2">
                <asp:Label ID="labEnShortName" runat="server" Width="90%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                省：
            </td>
            <td class="oddrow-l" colspan="2">
                <asp:Label ID="labProvince" runat="server" Width="90%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                市：
            </td>
            <td class="oddrow-l" colspan="2">
                <asp:Label ID="labCity" runat="server" Width="90%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                简介：
            </td>
            <td class="oddrow-l" colspan="2">
                <asp:Label ID="labBrief" runat="server" Width="90%"></asp:Label>
            </td>
        </tr>
        <%--	<tr>
		<td class="heading" colspan="3">产品线列表</td>
	</tr>
	<tr>
		<td align="center" colspan="3"><cc4:MyGridView id="dgList" runat="server" OnRowDataBound="dgList_RowDataBound"></cc4:MyGridView></td>
	</tr>--%>
    </table>
    <table style="width:100%" border="0">
        <tr>
            <td colspan="3" style="text-align: right">
            <asp:Button ID="btnPrint" runat="server" OnClientClick="PrintPage();" CssClass="widebuttons" Text="打印" />&nbsp;
                <input type="button" value="返回" onclick="returnurl();" class="widebuttons" />&nbsp;
                <input type="button" value="关闭" onclick="window.close();" class = "widebuttons" />
                <%--<asp:button id="btnAdd" runat="server" CssClass="widebuttons" Text="添加产品线" CausesValidation="true" OnClick="btnAdd_Click" ></asp:button>--%>
            </td>
        </tr>
    </table>
</asp:Content>
