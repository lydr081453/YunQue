<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" EnableEventValidation="false" Inherits="project_ProjectStep2" Codebehind="ProjectStep2.aspx.cs" %>
    <%@ register src="/UserControls/Project/CustomerInfo.ascx" tagname="CustomerInfo"
        tagprefix="uc1" %>
            <%@ register src="/UserControls/Project/PrepareDisplay.ascx" tagname="PrepareDisplay"
        tagprefix="uc1" %>
            <%@ register src="/UserControls/Project/ProjectMemberDisplay.ascx" tagname="ProjectMemberDisplay"
        tagprefix="uc1" %>
 <asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
     <script>
         $(document).ready(function () {
             $(window).scrollTop($(document).height());
         });
     </script>


    <table style="width: 100%">
        <tr>
        <td  width="100%" align="center">
        <img id="imgtitle" src="/images/l_02.jpg"/>
        </td>
    </tr>    
    <tr>
        <td style="height: 15px">
        </td>
    </tr>
        <tr>
            <td colspan="2">
                <li>
                    <asp:HyperLink ID="linkBack" runat="server" NavigateUrl="ProjectList.aspx">返回项目号申请单列表</asp:HyperLink></li>
            </td>
        </tr>
        <tr>
          <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:PrepareDisplay ID="PrepareDisplay" runat="server" />
            </td>
        </tr>
                <tr>
          <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:ProjectMemberDisplay ID="ProjectMemberDisplay" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:CustomerInfo ID="CustomerInfo" runat="server" />
            </td>
        </tr>
        
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnSave" runat="server" Text=" 保存 " OnClick="btnSave_Click" CssClass="widebuttons" />&nbsp;
                <asp:Button ID="btnPre" Text="上一步" CssClass="widebuttons" OnClick="btnPre_Click" runat="server" CausesValidation="false" />&nbsp;
                <asp:Button ID="btnNext" Text="下一步" CssClass="widebuttons" OnClick="btnNext_Click" runat="server" />
                &nbsp;
                     <asp:Button ID="btnReturn" Text=" 返回 " CssClass="widebuttons" CausesValidation="false" OnClick="btnReturn_Click" runat="server" />
            </td>
        </tr>
    </table>
 <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="false" ShowMessageBox="true" />
</asp:Content>