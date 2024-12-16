<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalaryDetail.aspx.cs" Title="薪资" MasterPageFile="~/MasterPage.master" Inherits="FinanceWeb.Return.SalaryDetail" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .salary-area {
            font-size: 12px;
            color: #6e6e6e;
            padding: 0 0 0 10px;
            background-color: #FFFFFF;
            border-bottom: 1px solid #eaedf1;
            border-top: 1px solid #eaedf1;
            border-left: 1px solid #eaedf1;
            border-right: 1px solid #eaedf1;
            text-align: left;
            padding-right: 5px;
            padding-left: 5px;
            /* 改过的样式  */
            border-width: 1px;
            border-style: double;
            border-right-width: 1px;
            border-color: white;
        }

        .salary-title {
            background-color: #f7f7f7;
            font-size: 12px;
            color: #6e6e6e;
            font-weight: bold;
            padding: 0 0 0 10px;
            border-bottom: 1px solid #eaedf1;
            border-top: 1px solid #eaedf1;
            border-left: 1px solid #eaedf1;
            border-right: 1px solid #eaedf1;
            text-align: right;
            padding-left: 5px;
            padding-right: 5px;
            /* 改过的样式  */
            border-width: 1px;
            border-style: double;
            border-right-width: 1px;
        }
    </style>
    <script type="text/javascript" src="/public/js/jquery-1.2.6.js"></script>

    <table width="50%" class="tableForm" align="center">
        <tr>
            <td class="heading" colspan="4">个人信息
            </td>
        </tr>
        <tr>
            <td class="salary-title" style="width: 30%;">姓名:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblNameCN"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="salary-title">个人邮箱:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="lblEmail" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="salary-title">所属公司:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="lblBranch" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <table width="50%" class="tableForm" align="center">
        <tr>
            <td class="heading" colspan="4" style="font-weight: bolder;">
                <asp:Label runat="server" ID="lblDate"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="salary-title" style="width: 30%;">基本工资</td>
            <td class="salary-area" style="font-size: larger; font-weight: bolder;">
                <asp:Label runat="server" ID="lblSalaryBase"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="salary-title">绩效工资
            </td>
            <td class="salary-area" style="font-size: larger; font-weight: bolder;">
                <asp:Label runat="server" ID="lblSalaryPerformance"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="salary-title">事假扣款
            </td>
            <td class="salary-area">
                <asp:Label runat="server" ID="lblAffairCut"></asp:Label>
            </td>
        </tr>
         <tr>
            <td class="salary-title">病假扣款
            </td>
            <td class="salary-area">
                <asp:Label runat="server" ID="lblSickCut"></asp:Label>
            </td>
        </tr>
        
        <tr>
            <td class="salary-title">迟到扣款
            </td>
            <td class="salary-area">
                <asp:Label runat="server" ID="lblLateCut"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="salary-title">早退扣款
            </td>
            <td class="salary-area">
                <asp:Label runat="server" ID="lblAbsenceCut"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="salary-title">忘打卡扣款
            </td>
            <td class="salary-area">
                <asp:Label runat="server" ID="lblClockCut"></asp:Label>
            </td>
        </tr>
         <tr>
            <td class="salary-title">考勤扣款小计
            </td>
            <td class="salary-area" style="font-size: larger; font-weight: bolder;">
                <asp:Label runat="server" ID="lblKaoqinTotal"></asp:Label>
            </td>
        </tr>
         <tr>
            <td class="salary-title"> 其他减除费用
            </td>
            <td class="salary-area" >
                <asp:Label runat="server" ID="lblOtherCut"></asp:Label>
            </td>
        </tr>
         <tr>
            <td class="salary-title"> 其他增加费用
            </td>
            <td class="salary-area">
                <asp:Label runat="server" ID="lblOtherIncome"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="salary-title">当月收入
            </td>
            <td class="salary-area" style="font-size: larger; font-weight: bolder;">
                <asp:Label runat="server" ID="lblIncome"></asp:Label>
            </td>
        </tr>
          <tr>
            <td class="salary-title">住房
            </td>
            <td class="salary-area">
                <asp:Label runat="server" ID="lblHouse"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="salary-title">养老
            </td>
            <td class="salary-area">
                <asp:Label runat="server" ID="lblRetirement"></asp:Label>
            </td>
        </tr>
         <tr>
            <td class="salary-title">失业
            </td>
            <td class="salary-area">
                <asp:Label runat="server" ID="lblUnEmp"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="salary-title">医疗
            </td>
            <td class="salary-area">
                <asp:Label runat="server" ID="lblMedical"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="salary-title">社保(个人)小计
            </td>
            <td class="salary-area" style="font-size: larger; font-weight: bolder;">
                <asp:Label runat="server" ID="lblInsuranceTotal"></asp:Label>
            </td>
        </tr>
       
        <tr>
            <td class="salary-title">本期应预扣预缴税额
            </td>
            <td class="salary-area">
                <asp:Label runat="server" ID="lblTax3"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="salary-title">税前工资
            </td>
            <td class="salary-area">
                <asp:Label runat="server" ID="lblSalaryPreTax"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="salary-title">税后扣款
            </td>
            <td class="salary-area">
                <asp:Label runat="server" ID="lblTaxedCut"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="salary-title">实发金额
            </td>
            <td class="salary-area" style="font-size: larger; font-weight: bolder;">
                <asp:Label runat="server" ID="lblSalaryPaid"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="salary-title">说明
            </td>
            <td class="oddrow-l">
                <asp:Label runat="server" ID="lblRemark"></asp:Label>
            </td>
        </tr>
    </table>

    <table width="50%" class="tableForm" align="center">
        <tr>
            <td class="salary-title" style="width: 30%">个税计算器:
            </td>
            <td class="oddrow-l" colspan="3">

                <font style="color: gray;">您可使用个税模拟计算器（<a href="https://gerensuodeshui.cn/" style="color: blue; text-decoration: underline;" target="_blank">https://gerensuodeshui.cn/</a>）核算税金，结果仅供参考。使用前请详阅填写说明。</font>
            </td>
        </tr>
    </table>

</asp:Content>
