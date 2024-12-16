<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" Inherits="project_SetAuditor" Codebehind="SetAuditor.aspx.cs" %>
 <%@ register src="/UserControls/Project/SetAuditor.ascx" tagname="SetAuditor"
        tagprefix="uc1" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
         <table style="width: 100%">
          <tr>
            <td width="100%" align="center">
                <img id="imgtitle" src="/images/l_05.jpg" />
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
                <uc1:SetAuditor ID="SetAuditor" runat="server" />
            </td>
        </tr>
    </table>

</asp:Content>
