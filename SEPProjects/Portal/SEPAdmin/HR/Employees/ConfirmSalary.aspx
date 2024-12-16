<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" EnableEventValidation="false"
    CodeBehind="ConfirmSalary.aspx.cs" Inherits="SEPAdmin.HR.Employees.ConfirmSalary" Title="13薪发放" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" src="/public/js/jquery.js"></script>

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">个人信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">姓名:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblNameCN"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow-l">个人邮箱:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="lblEmail" runat="server"></asp:Label>
            </td>
        </tr>

    </table>
    <br />
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">薪资信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">发放月:
            </td>
            <td class="oddrow-l">
                <asp:DropDownList runat="server" ID="ddlYear"></asp:DropDownList>&nbsp;-&nbsp;<asp:DropDownList runat="server" ID="ddlMonth"></asp:DropDownList>
                &nbsp;&nbsp;<asp:Button runat="server" ID="btnSend" Text=" 发送验证码到公司邮箱 " OnClick="btnSend_Click" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">请输入验证码:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtPwd" runat="server" />&nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click" />&nbsp;&nbsp;<font style="font-size: smaller; color: gray;">验证码5分钟内有效，请及时操作。</font>

            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">13薪金额:
            </td>
            <td class="oddrow-l" colspan="3" style="font-weight: bolder; color: red; font-size: larger;">
                <asp:Label runat="server" ID="lblSalary"></asp:Label>
            </td>

        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">社保公积金:
            </td>
            <td class="oddrow-l" colspan="3" style="font-weight: bolder; color: red; font-size: larger;">
                <asp:Label runat="server" ID="lblInsurance"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%"></td>
            <td class="oddrow-l" colspan="3">
                <p style="font-weight: bolder; color: red; font-size: larger;">
                    <asp:Label runat="server" ID="lblError"></asp:Label><asp:HyperLink runat="server" ID="lblHome" NavigateUrl="http://xy.shunyagroup.com" Text="返回首页" Font-Underline="true" Visible="false"></asp:HyperLink>
                </p>
            </td>

        </tr>
    </table>
    <table width="100%" class="tableForm">
        <tr>
            <td class="oddrow" style="width: 15%">请确认13薪发放方式:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:DropDownList runat="server" ID="ddlType">
                    <asp:ListItem Selected="True" Text="单独发放" Value="1"></asp:ListItem>
                    <asp:ListItem Text="与发放月工资合并发放" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="4" style="padding-left: 15%;">
                <asp:Button runat="server" ID="btnConfirm" Text=" 确认提交13薪发放方式，财务部将按照您的选择进行发放 " OnClick="btnConfirm_Click" />
            </td>
        </tr>
    </table>

    <table width="100%" class="tableForm">
        <tr>
            <td class="oddrow" style="width: 15%">个税计算器:
            </td>
            <td class="oddrow-l" colspan="3">

                <font style="color: gray;">您可使用个税模拟计算器（<a href="https://gerensuodeshui.cn/" style="color: blue; text-decoration: underline;" target="_blank">https://gerensuodeshui.cn/</a>）测算两种计税方式的税额差异，结果仅供参考。使用前请详阅填写说明。</font>
            </td>
        </tr>
    </table>

</asp:Content>
