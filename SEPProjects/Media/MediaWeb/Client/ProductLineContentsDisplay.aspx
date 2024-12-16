<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/MasterPage.master" Inherits="Client_ProductLineContentsDisplay" Codebehind="ProductLineContentsDisplay.aspx.cs" %>

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
                产品线历史数据详细信息
            </td>
        </tr>
        <tr>
            <td class="menusection-Packages">
                当前版本号：
            </td>
            <td class="menusection-Packages" colspan="3">
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
                <asp:Label ID="labLastModifyDate" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="4">
                <asp:Label ID="labName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                产品线名称：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="labProductLineName" Font-Bold="true" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                产品线图片：
            </td>
            <td class="oddrow-l" colspan="3">
                <div>
                    <a href="#" onclick="return hs.htmlExpand(this, { contentId: 'photo-div' } )"
                        class="highslide">
                        <asp:Image ID="imgTitle" runat="server" Visible="false" Width="50px" Height="50px" /></a>
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
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                描述：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="labDes" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <table style="width:100%" border="0">
        <tr>
            <td valign="bottom" colspan="4" align="right">
            <asp:Button ID="btnPrint" runat="server" OnClientClick="PrintPage();" CssClass="widebuttons" Text="打印" />&nbsp;
                <input type="button" value="返回" onclick="returnurl();" class="widebuttons" />&nbsp;
                <input type="button" value="关闭" onclick="window.close();" class="widebuttons" />
            </td>
        </tr>
    </table>
</asp:Content>
