<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    EnableEventValidation="false" Inherits="project_ProjectStep1" CodeBehind="ProjectStep1.aspx.cs" %>

<%@ Register Src="/UserControls/Project/PrepareInfo.ascx" TagName="Prepare" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript" type="text/javascript">

        function checkValid(btnobj) {
            var msg = "";
            if (document.getElementById("<%= PrepareInfo.FindControl("hidContactStatus").ClientID%>").value == "") {
                msg += "请选择合同状态。" + "\n";
            }

            var projectType = document.getElementById("<%=PrepareInfo.FindControl("ddlProjectType").ClientID %>");
            var projectTypeValue = projectType.options[projectType.selectedIndex].value;

            var projectTypeLevel2 = document.getElementById("<%=PrepareInfo.FindControl("ddlTypeLevel2").ClientID %>");
            var projectTypeLevel2Value = projectTypeLevel2.options[projectTypeLevel2.selectedIndex].value;

            var projectTypeLevel3 = document.getElementById("<%=PrepareInfo.FindControl("ddlTypeLevel3").ClientID %>");
            var projectTypeLevel3Value = projectTypeLevel2.options[projectTypeLevel3.selectedIndex].value;
            
            var bizType = document.getElementById("<%=PrepareInfo.FindControl("ddlBizType").ClientID %>");
            var bizTypeValue=bizType.options[bizType.selectedIndex].value;
            
            if (projectTypeValue == "0" || projectTypeLevel2Value == '0' || projectTypeLevel3Value == '0') {
                msg += "请选择项目类型。" + "\n";
            }
              
            if (bizTypeValue == "0") {
                msg += "请选择业务类型。" + "\n";
            }
            
            if (document.getElementById("<%= PrepareInfo.FindControl("hidGroupID").ClientID%>").value == "" || document.getElementById("<%= PrepareInfo.FindControl("hidGroupID").ClientID%>").value =="-1" || document.getElementById("<%= PrepareInfo.FindControl("txtGroup").ClientID%>").value =="") {
                msg += "请选择项目组。" + "\n";
            }
            if (document.getElementById("<%= PrepareInfo.FindControl("hidApplicantID").ClientID%>").value == "") {
                msg += "请选择项目负责人。" + "\n";
            }
            if (document.getElementById("<%= PrepareInfo.FindControl("hidBusinessPersonId").ClientID%>").value == "") {
                msg += "请选择商务。" + "\n";
            }
            if (document.getElementById("<%= PrepareInfo.FindControl("txtBizDesc").ClientID%>").value == "") {
                msg += "请填写项目名称。" + "\n";
            }
            if (document.getElementById("<%= PrepareInfo.FindControl("txtBrands").ClientID%>").value == "") {
                    msg += "请填写品牌信息。" + "\n";
            }
            if (msg == "") {
                btnobj.disabled = true;
                return true;
            }
            else {
                alert(msg);
                return false;
            }
        }
    </script>

    <table width="100%" border="0" cellpadding="0" runat="server" visible="true" id="tabTitle">
        <tr>
            <td width="100%" align="center">
                <img id="imgtitle" src="/images/l_01.jpg" />
            </td>
        </tr>
        <tr>
            <td style="height: 15px">
            </td>
        </tr>
    </table>
    <table style="width: 100%">
        <tr>
            <td colspan="2">
                <li>
                    <asp:HyperLink ID="linkBack" runat="server" NavigateUrl="ProjectList.aspx">返回项目号申请单列表</asp:HyperLink></li>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:Prepare ID="PrepareInfo" runat="server" />
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnSave" runat="server" Text="  保存 " usesubmitbehavior="false" OnClientClick="if(!checkValid(this)){return false;}"
                    OnClick="btnSave_Click" CssClass="widebuttons" />&nbsp;
                <asp:Button ID="btnNext" Text="下一步" CssClass="widebuttons" usesubmitbehavior="false" OnClientClick="if(!checkValid(this)){return false;}"
                    OnClick="btnNext_Click" runat="server" />
                &nbsp;
                <asp:Button ID="btnReturn" Text=" 返回 " CssClass="widebuttons" OnClick="btnReturn_Click"
                    runat="server" />
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="false"
        ShowMessageBox="true" />
</asp:Content>
