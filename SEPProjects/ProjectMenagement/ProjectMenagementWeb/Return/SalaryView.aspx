<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalaryView.aspx.cs" Title="薪资" MasterPageFile="~/MasterPage.master" Inherits="FinanceWeb.Return.SalaryView" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="/public/js/jquery-1.2.6.js"></script>

    <table width="80%" class="tableForm" align="center">
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
    <table width="80%" class="tableForm" align="center">
        <tr>
            <td class="heading" colspan="4">薪资信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">当前发放年度:
            </td>
            <td class="oddrow-l">
                <asp:DropDownList runat="server" ID="ddlYear"></asp:DropDownList>
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
            <td colspan="4">
                <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvG_RowDataBound"
                    EmptyDataText="暂时没有相关记录" AllowPaging="false" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="发放月" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDate"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="namecn" HeaderText="姓名" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%" />
                        <asp:BoundField DataField="usercode" HeaderText="员工编号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%" />
                        <asp:TemplateField HeaderText="基本工资" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblSalaryBased"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="绩效工资" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblSalaryPerformance"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="考勤扣款" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblKaoqinTotal"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="当月收入" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblIncome"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="社保小计" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblInsuranceTotal"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="税前工资" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblSalaryPretax"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="本期应预扣预缴税额" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblTax3"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="税后扣款" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblTaxedCut"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="实发金额" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblSalaryPaid"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="明细" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%">
                            <ItemTemplate>
                                <a href='SalaryDetail.aspx?year=<%#Eval("SalaryYear")%>&month=<%#Eval("SalaryMonth")%>&pwd=<%#Eval("EmailPassword")%>' target="_blank">
                                    <img src="../images/dc.gif" border="0px;" title="明细"></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>

    <table width="80%" class="tableForm" align="center">
        <tr>
            <td class="oddrow" style="width: 15%">个税计算器:
            </td>
            <td class="oddrow-l" colspan="3">

                <font style="color: gray;">您可使用个税模拟计算器（<a href="https://gerensuodeshui.cn/" style="color: blue; text-decoration: underline;" target="_blank">https://gerensuodeshui.cn/</a>）核算税金，结果仅供参考。使用前请详阅填写说明。</font>
            </td>
        </tr>
    </table>

</asp:Content>
