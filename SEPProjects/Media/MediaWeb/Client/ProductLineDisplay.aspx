<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/MasterPage.master" Inherits="Client_ProductLineDisplay" Codebehind="ProductLineDisplay.aspx.cs" %>

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
  function openClient() {
      var plid = '<%=Request["Plid"] %>';
      window.open("ProductSelectClientList.aspx?alert=1&Plid="+plid, "关联客户", "<%=ESP.Media.Access.Utilities.Global.OpenClass.Common %>");
  }
    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">
    <input type="hidden" id="hidUrl" runat="server" />
    <table width="100%" border="0">
        <tr>
            <td colspan="4" align="right">
                <%-- <asp:Button ID="btnWatch" runat="server"   CssClass="widebuttons" Text="历史察看" OnClick="btnWatch_Click" />--%>
                
                <asp:Literal runat="server" ID="ltOperate"></asp:Literal>&nbsp;
                <asp:Button ID="benEdit1"
                    runat="server" Text=" 编辑 " CssClass="widebuttons" OnClick="btnEdit_Click" />&nbsp;
                    <asp:Button ID="btnPrint" runat="server" OnClientClick="PrintPage();" CssClass="widebuttons" Text="打印" />&nbsp;
            <asp:Literal ID="lClose1" runat="server" ></asp:Literal>
            </td>
        </tr>
    </table>
    <table width="100%" border="1" class="tableForm">
        <tr>
            <td class="menusection-Packages" colspan="4">
                显示产品线
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
            <td colspan="4" class="heading">
                产品线信息
            </td>
        </tr>
         <tr>
            <td style="width: 20%" class="oddrow">
               所属客户名称：
            </td>
            <td colspan="3" class="oddrow-l">
                <asp:LinkButton ID="lnkClientName" runat="server" Font-Bold="True" />
                <asp:Button ID="btnChangeClient" runat="server" Text="变更所属客户" CssClass="bigwidebuttons"
                    OnClientClick="openClient();return false;" />
            </td>            
        </tr>
        <tr>
            <td style="width: 20%" class="oddrow">
                产品线名称：
            </td>
            <td colspan="3" class="oddrow-l">
                <asp:Label ID="labProductLineName" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 20%" class="oddrow">
                产品线图片：
            </td>
            <td colspan="3" class="oddrow-l">
                <div>
                    <a href="#" onclick="return hs.htmlExpand(this, { contentId: 'photo-div' } )"
                        class="highslide">
                        <asp:Image ID="imgTitle" runat="server" Visible="false" CssClass="ThumbnailPhoto" /></a>
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
            <td style="width: 20%" class="oddrow">
                描述：
            </td>
            <td colspan="3" class="oddrow-l">
                <asp:Label ID="labDes" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <table width="100%" border="0">
        <tr>
            <td valign="bottom" colspan="4" align="right"><asp:Button ID="Button1"
                    runat="server" Text=" 编辑 " CssClass="widebuttons" OnClick="btnEdit_Click" />&nbsp;
                    <asp:Button ID="Button2" runat="server" OnClientClick="PrintPage();" CssClass="widebuttons" Text="打印" />&nbsp;
            <asp:Literal ID="lClose" runat="server" ></asp:Literal>
                <%--<input type="button" value="返回" onclick="returnurl();" class="widebuttons"/>--%>
 <%--               <input type="button" value="关闭" onclick="javascipt:window.close();return false;"
                    class="widebuttons" />--%>
            </td>
        </tr>
    </table>
</asp:Content>
