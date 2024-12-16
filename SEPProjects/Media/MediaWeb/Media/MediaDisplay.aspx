<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Media_MediaDisplay" Title="媒体详细信息" Codebehind="MediaDisplay.aspx.cs" %>

<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<%@ Register TagPrefix="uc" TagName="DABMediaDisplay" Src="~/Media/skins/DABMediaDisplay.ascx" %>
<%@ Register TagPrefix="uc" TagName="PlaneMediaDisplay" Src="~/Media/skins/PlaneMediaDisplay.ascx" %>
<%@ Register TagPrefix="uc" TagName="TvMediaDisplay" Src="~/Media/skins/TvMediaDisplay.ascx" %>
<%@ Register TagPrefix="uc" TagName="WebMediaDisplay" Src="~/Media/skins/WebMediaDisplay.ascx" %>
<%@ Register TagPrefix="uc" TagName="Experience" Src="~/Media/skins/Experience.ascx" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="contentHead" runat="server">

    <script type="text/javascript">
    function returnurl()
        {
                var hidurl=document.getElementById("<% = hidUrl.ClientID %>");
    window.location = hidurl.value;
        }

        function WinOpen() {
            Mid = document.getElementById("<% =hidMediaId.ClientID%>");
            //window.open("MediaLinkReporterList.aspx?alert=1&Mid="+Mid.value,"选择记者","<%= ESP.Media.Access.Utilities.Global.OpenClass.Common %>");
            window.open("ReporterAddAndEdit.aspx?Operate=ADD&alert=1&Mid=" + Mid.value, "选择记者", "<%= ESP.Media.Access.Utilities.Global.OpenClass.Common %>");
        }
  function PrintPage()
  {  
    window.print();
  }
  function btnReporterSign_ClientClick(fn) {
            
            hidMediaID = document.getElementById("<% = hidMediaId.ClientID %>");
            window.open('/DownLoad/MediaDisplay.aspx?ExportType=sign&FileName=' + fn + '&Term=' + hidMediaID.value, "");
                return false;
            
        }
        function btnReporterContact_ClientClick(fn) {
            
                window.open('/DownLoad/MediaDisplay.aspx?ExportType=contact&FileName=' + fn + '&Term=' + hidMediaID.value, "");
                return false;
            
        }
    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">
 <input type="hidden" id="hidMediaId" runat="server" value="0" /><input type="hidden" id="hidUrl" runat="server" />
    <table style="width: 100%;" >
        <tr>
            <td>
                <table style="width: 100%;" border="0">
                    <tr>
                        <td align="right">
                           <%-- <asp:Button ID="btnWatch" runat="server" CssClass="widebuttons" Text="历史查看" OnClick="btnWatch_Click" />--%>
                              <asp:Literal runat="server" ID="ltOperate"></asp:Literal>&nbsp;<asp:Button ID="btnEdit1"
                    runat="server" Text=" 编辑 " CssClass="widebuttons" OnClick="btnEdit_Click" />&nbsp;<asp:Button ID="btnPrint" runat="server" OnClientClick="PrintPage();" CssClass="widebuttons" Text="打印" />&nbsp;<asp:Button ID="btnBack1" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnBack_Click" />&nbsp;<asp:Button ID="btnClose1" runat="server" Text=" 关闭 " CssClass="widebuttons" OnClientClick="window.close();return false;" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Panel ID="panelMediaDisplay" runat="server" Height="0px">
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right">
                            <asp:Button ID="btnAdd" runat="server" CssClass="widebuttons" Text="添加记者" CausesValidation="true"
                                  OnClientClick="  WinOpen() ;" Visible="false"></asp:Button><asp:Button ID="btnEdit"
                    runat="server" Text=" 编辑 " CssClass="widebuttons" OnClick="btnEdit_Click" />&nbsp;
                    <asp:Button ID="Button1" runat="server" OnClientClick="PrintPage();" CssClass="widebuttons" Text="打印" />&nbsp;
                            <asp:Button ID="btnBack" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnBack_Click" />&nbsp;
                            <asp:Button ID="btnClose" runat="server" Text=" 关闭 " CssClass="widebuttons" OnClientClick="window.close();return false;" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;" border="0" runat="server" id="tbReporter">
                    <tr>
                        <td class="headinglist" colspan="3">
                            相关记者列表</td>
                    </tr>
                    <tr>
                            <td style="padding-left:20px">
                            <table class="tablehead">
                                <tr>
                                    <td>
                                <img src="/images/add.gif" border="0" style=" vertical-align:bottom" />&nbsp;<asp:LinkButton ID="btnLink" runat="server" class="bigfont" Text="添加记者" OnClientClick="WinOpen();return false;" />
                                </td>
                            </tr>
                            </table>
                            </td>
                        </tr>
                    <tr>
                        <td align="center" colspan="3">
                            <cc4:MyGridView ID="dgList" DataKeyNames="reporterid" runat="server" OnRowDataBound="dgList_RowDataBound">
                            </cc4:MyGridView>
                        </td>
                    </tr>                    
                </table>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Button ID="btnReporterSign" runat="server" CssClass="widebuttons" Text="生成签到表" OnClick="btnReporterSign_Click" />
                <asp:Button ID="btnReporterContact" runat="server" CssClass="widebuttons" Text="生成通联表" OnClick="btnReporterContact_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
