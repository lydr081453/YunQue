<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="AuditMediaDisplay.aspx.cs" Inherits="MediaWeb.NewMedia.BaseData.AuditMediaDisplay" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register TagPrefix="uc" TagName="PlaneMediaAddAndEdit" Src="~/NewMedia/BaseData/skins/PlaneMediaAddAndEdit.ascx" %>
<%@ Register TagPrefix="uc" TagName="DABMediaAddAndEdit" Src="~/NewMedia/BaseData/skins/DABMediaAddAndEdit.ascx" %>
<%@ Register TagPrefix="uc" TagName="TvMediaAddAndEdit" Src="~/NewMedia/BaseData/skins/TvMediaAddAndEdit.ascx" %>
<%@ Register TagPrefix="uc" TagName="WebMediaAddAndEdit" Src="~/NewMedia/BaseData/skins/WebMediaAddAndEdit.ascx" %>
<%@ Register TagPrefix="uc" TagName="Experience" Src="~/NewMedia/BaseData/skins/Experience.ascx" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="contentHead" runat="server">
    <script type="text/javascript">
  function PrintPage()
  {  
    window.print();
  }
    </script>


</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">
 <input type="hidden" id="hidMediaId" runat="server" value="0" />
    <table style="width: 100%;" border="0">
        <tr>
            <td>
                <table style="width: 100%;" border="0">
                <%--    <tr>
                        <td>
                            <asp:Button ID="btnWatch" runat="server" CssClass="widebuttons" Text="历史察看" OnClick="btnWatch_Click" />
                        </td>
                    </tr>--%>
                    <tr>
                        <td align="right"><asp:Button ID="Button1"
                    runat="server" Text=" 编辑 " CssClass="widebuttons" OnClick="btnEdit_Click" />&nbsp;
                    <asp:Button ID="Button2" runat="server" OnClientClick="PrintPage();" CssClass="widebuttons" Text="打印" />&nbsp;
                            <asp:Button ID="Button3" runat="server" CssClass="widebuttons" Text="审核通过" CausesValidation="true" OnClick="btnAudit_Click"
                               ></asp:Button>
                            &nbsp;<asp:Button ID="Button4" runat="server" Text="返回" CssClass="widebuttons" OnClick="btnBack_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Panel ID="panelMediaDisplay" runat="server" Height="0px">
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td align="right"><asp:Button ID="btnEdit"
                    runat="server" Text=" 编辑 " CssClass="widebuttons" OnClick="btnEdit_Click" />&nbsp;
                    <asp:Button ID="btnPrint" runat="server" OnClientClick="PrintPage();" CssClass="widebuttons" Text="打印" />&nbsp;
                            <asp:Button ID="btnAudit" runat="server" CssClass="widebuttons" Text="审核通过" CausesValidation="true" OnClick="btnAudit_Click"
                               ></asp:Button>
                            &nbsp;<asp:Button ID="btnBack" runat="server" Text="返回" CssClass="widebuttons" OnClick="btnBack_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

        <%--<tr>
            <td>
                <table style="width: 100%;" border="0">
                    <tr>
                        <td class="headinglist" colspan="3">
                            相关记者列表</td>
                    </tr>
                    <tr>
                        <td align="center" colspan="3">
                            <cc4:MyGridView ID="dgList"  DataKeyNames="reporterid" runat="server" OnRowDataBound="dgList_RowDataBound">
                            </cc4:MyGridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>--%>
        
    </table>
</asp:Content>

