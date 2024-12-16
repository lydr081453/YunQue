<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Media_MediaAddAndEdit" EnableEventValidation="false" Codebehind="MediaAddAndEdit.aspx.cs" %>

<%@ Register TagPrefix="uc" TagName="PlaneMediaAddAndEdit" Src="~/Media/skins/PlaneMediaAddAndEdit.ascx" %>
<%@ Register TagPrefix="uc" TagName="DABMediaAddAndEdit" Src="~/Media/skins/DABMediaAddAndEdit.ascx" %>
<%@ Register TagPrefix="uc" TagName="TvMediaAddAndEdit" Src="~/Media/skins/TvMediaAddAndEdit.ascx" %>
<%@ Register TagPrefix="uc" TagName="WebMediaAddAndEdit" Src="~/Media/skins/WebMediaAddAndEdit.ascx" %>
<%@ Register TagPrefix="uc" TagName="Experience" Src="~/Media/skins/Experience.ascx" %>
<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="contentHead" runat="server">

    <script type="text/javascript">
  function   WinOpen()   
      {   
       Mid = document.getElementById("<% =hidMediaId.ClientID%>");
       //window.open("MediaLinkReporterList.aspx?alert=1&Mid="+Mid.value,"选择记者","<%= ESP.Media.Access.Utilities.Global.OpenClass.Common %>");
       window.open("ReporterAddAndEdit.aspx?Operate=ADD&alert=1&Mid=" + Mid.value, "选择记者", "<%= ESP.Media.Access.Utilities.Global.OpenClass.Common %>");    
      }
         function returnurl()
        {
                var hidurl=document.getElementById("<% = hidUrl.ClientID %>");
    window.location = hidurl.value;
}


    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">
    <input type="hidden" id="hidUrl" runat="server" />
    <input type="hidden" id="hidMediaId" runat="server" value="0" />
    <table style="width: 100%;">
        <tr>
            <td>
                <table style="width: 100%;" border="0">
                    <tr>
                        <td colspan="4">
                            <asp:Panel ID="panelMediaAddAndEdit" runat="server" Height="0px">
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="6" style="text-align: right">
                <asp:Button ID="btnOk" runat="server" CssClass="widebuttons" Text="保存" OnClick="btnOk_Click"
                    OnClientClick="return check();" />
                <asp:Button ID="btnSubmit" runat="server" CssClass="widebuttons" Text="保存" OnClick="btnSubmit_Click"
                    OnClientClick="return check();" Visible="false" /> <%--原提交按钮--%>
                <input type="reset" class="widebuttons" value="重置" />
                <%--<input type="button" value="返回" onclick="returnurl();" class="widebuttons" />--%>
                <asp:Button ID="btnBack" runat="server" Text="返回" CssClass="widebuttons" OnClick="btnBack_Click" />
            </td>
            <asp:HiddenField ID="hidBackUrl" runat="server" />
            <asp:HiddenField ID="hidMid" runat="server" />
        </tr>
        <tr>
            <td style="height:25px" />
        </tr>
        <tr>
            <td>
                <asp:Panel runat="server" ID="pReaporter">
                    <table style="width: 100%;" border="0">
                        <tr>
                            <td class="headinglist" colspan="3">
                                相关记者列表
                            </td>
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
                                <cc4:MyGridView ID="dgList" DataKeyNames="ReporterID" runat="server" OnRowDataBound="dgList_RowDataBound">
                                </cc4:MyGridView>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
