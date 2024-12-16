<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Media_MediaContentsDisplay"
    Title="媒体详细信息" Codebehind="MediaContentsDisplay.aspx.cs" %>

<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<%@ Register TagPrefix="uc" TagName="DABMediaDisplay" Src="~/Media/skins/DABMediaContentsDisplay.ascx" %>
<%@ Register TagPrefix="uc" TagName="PlaneMediaDisplay" Src="~/Media/skins/PlaneMediaContentsDisplay.ascx" %>
<%@ Register TagPrefix="uc" TagName="TvMediaDisplay" Src="~/Media/skins/TvMediaContentsDisplay.ascx" %>
<%@ Register TagPrefix="uc" TagName="WebMediaDisplay" Src="~/Media/skins/WebMediaContentsDisplay.ascx" %>
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
    <table style="width: 100%;" border="0">
         <tr>
            <td style="text-align: right">
            <asp:Button ID="Button1" runat="server" OnClientClick="PrintPage();" CssClass="widebuttons" Text="打印" />&nbsp;
                <input type="button" value="关闭" onclick="javascipt:window.close();return false;"
                    class="widebuttons" />&nbsp;<asp:Button ID="Button2" runat="server" Text="返回" CssClass="widebuttons" OnClick="btnBack_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Panel ID="panelMediaDisplay" runat="server" Height="0px">
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td style="text-align: right">
            <asp:Button ID="btnPrint" runat="server" OnClientClick="PrintPage();" CssClass="widebuttons" Text="打印" />&nbsp;
                <input type="button" value="关闭" onclick="javascipt:window.close();return false;"
                    class="widebuttons" />&nbsp;<asp:Button ID="btnBack" runat="server" Text="返回" CssClass="widebuttons" OnClick="btnBack_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
