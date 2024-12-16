<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalaryImport.aspx.cs" EnableEventValidation="false"
    MasterPageFile="~/MasterPage.master" Inherits="FinanceWeb.Return.SalaryImport" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript" src="/public/js/jquery-1.2.6.js"></script>


    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">导入薪资数据
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">选择文件:
            </td>
            <td class="oddrow-l" align="left" colspan="3">
                <asp:FileUpload ID="fileId" runat="server" Width="500px" />
                <br />
                <font color="gray">薪资EXCEL格式模板下载</font>&nbsp;&nbsp;<a href="/tmp/Salary/SalaryCommonTemplate.xlsx" target="_blank"><img src="/images/ico_04.gif" /></a>
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Button ID="btnSalaryImport" runat="server" Text=" 导入薪资 " OnClick="btnSalaryImport_Click" CssClass="widebuttons" />
                
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">查询信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">年份:
            </td>
            <td class="oddrow-l" align="left" >
                <asp:DropDownList runat="server" ID="ddlYear"></asp:DropDownList>
            </td>
             <td class="oddrow" style="width: 15%">月份:
            </td>
            <td class="oddrow-l" align="left" >
                <asp:DropDownList runat="server" ID="ddlMonth"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " OnClick="btnSearch_Click" CssClass="widebuttons" />&nbsp;
                     <asp:Button ID="btnDelete" runat="server" Text=" 删除 " OnClick="btnDelete_Click" CssClass="widebuttons" />
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="4">已导入数据
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvG_RowDataBound"
                    EmptyDataText="暂时没有相关记录" AllowPaging="false" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="SalaryYear" HeaderText="年份" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="SalaryMonth" HeaderText="月份" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="namecn" HeaderText="姓名" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="usercode" HeaderText="员工编号" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="IDNumber" HeaderText="身份证号" ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
