<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" MasterPageFile="~/MasterPage.master"
    CodeBehind="AddJob.aspx.cs" Inherits="SEPAdmin.HR.Recruitment.AddJob" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript" type="text/javascript" src="../../public/js/DatePicker.js"></script>

    <script language="javascript" type="text/javascript" src="../../public/js/DatePicker1.js"></script>

    <script language="javascript" type="text/javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script type="text/javascript" src="/publicjs/jquery.jcarousellite.min.js"></script>

    <script type="text/javascript" src="/publicjs/script_jcarousellite.js"></script>

    <script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>

    <script src="/public/js/dimensions.js" type="text/javascript"></script>

    <script type="text/javascript" src="/public/js/jquery.blockUI.js"></script>

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                职位名称:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtName" runat="server" MaxLength="50"></asp:TextBox>
            </td>
            <td class="oddrow" style="width: 15%">
                工作地点:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:DropDownList runat="server" ID="ddlWorkingPlace">
                    <asp:ListItem Text="北京" Selected="True" Value="北京"></asp:ListItem>
                    <asp:ListItem Text="重庆" Value="重庆"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                服务客户:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtSerToCustomer" runat="server" MaxLength="50"></asp:TextBox>
            </td>
            <td class="oddrow" style="width: 15%">
                是否紧急？
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:RadioButton ID="radioIsEmergency" runat="server" Text="是" GroupName="0" Checked="false" />
                <asp:RadioButton ID="radioIsNotEmergency" runat="server" Text="否" GroupName="0" Checked="true" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                工作职责:
            </td>
            <td class="oddrow-l" colspan="3" style="width: 85%">
                <asp:TextBox ID="txtResponsibilities" runat="server" MaxLength="4000" Height="84px"
                    Width="842px" Columns="76" Rows="5" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                职位要求:
            </td>
            <td class="oddrow-l" colspan="3" style="width: 85%">
                <asp:TextBox ID="txtRequirements" runat="server" MaxLength="4000" Height="84px" Width="842px"
                    Columns="76" Rows="5" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" align="center">
            </td>
            <td class="oddrow-l" align="center">
                <asp:Button ID="btnSave" runat="server" Text=" 保存 " CssClass="widebuttons" OnClick="btnSave_Click" />
                <asp:Label runat="server" ID="lblEmpty" Visible="false" />
                <asp:Button ID="btnCancle" runat="server" Text="返回" OnClick="btnCancle_Click" CssClass="widebuttons" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtCreateTime" runat="server" Visible="false" />
                <asp:TextBox ID="txtCreater" runat="server" Visible="false" />
                <asp:TextBox ID="txtId" runat="server" Visible="false"></asp:TextBox>
            </td>
        </tr>
    </table>
</asp:Content>
