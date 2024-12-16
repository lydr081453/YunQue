<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransferDone.aspx.cs" Inherits="SEPAdmin.HR.Transfer.TransferDone" MasterPageFile="~/MasterPage.master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <table width="100%" class="tableForm">
        <tr>
            <td class="oddrow" style="width: 15%">转出公司:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label runat="server" ID="lblTransOutCompany"></asp:Label>

            </td>
            <td class="oddrow" style="width: 15%">转出部门:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label runat="server" ID="lblTransOutDept"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">转出组别:
            </td>
            <td class="oddrow-l" style="width: 35%" colspan="3">
                <asp:HiddenField runat="server" ID="hidGroupId" />
                <asp:Label runat="server" ID="lblTransOutGroup"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">转入公司:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label runat="server" ID="lblTransInCompany"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">转入部门:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label runat="server" ID="lblTransInDept"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">转入组别:
            </td>
            <td class="oddrow-l" style="width: 35%" colspan="3">
                <asp:Label runat="server" ID="lblTransInGroup"></asp:Label>
            </td>

        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">转入职务:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label runat="server" ID="lblTransInPosition"></asp:Label>
            </td>
            <td class="oddrow" style="width: 10%">转入日期:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label runat="server" ID="lblTransInDate"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">转出员工:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:HiddenField ID="hidTransUserId" runat="server" />
                <asp:Label runat="server" ID="lblTransUser"></asp:Label>
            </td>
            <td class="oddrow" style="width: 10%">转出日期:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label runat="server" ID="lblTransOutDate"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">基本工资:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label runat="server" ID="lblSalaryBase"></asp:Label>
            </td>
            <td class="oddrow" style="width: 10%">绩效工资:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label runat="server" ID="lblSalaryPromotion"></asp:Label>

            </td>
        </tr>

        <tr>
            <td class="oddrow" style="width: 10%">备注:
            </td>
            <td class="oddrow-l" style="width: 80%" colspan="3">
                <asp:Label runat="server" ID="lblRemark"></asp:Label>
            </td>
        </tr>

    </table>


    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">业务交接
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvDetailList" runat="server" AutoGenerateColumns="False"
                    Width="100%" OnRowDataBound="gvDetailList_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="FormId" Visible="false" />
                        <asp:TemplateField HeaderText="单据编号">
                            <ItemTemplate>
                                <a href='http://<%# Eval("Website").ToString() + Eval("Url").ToString() %>' target="_blank"
                                    title="<%# Eval("FormCode") %>">
                                    <%# Eval("FormCode") %></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="FormType" HeaderText="单据类型" />
                        <asp:BoundField DataField="ProjectCode" HeaderText="项目号" />
                        <asp:BoundField DataField="Description" HeaderText="项目号描述" />
                        <asp:BoundField DataField="TotalPrice" HeaderText="总金额" />
                        <asp:TemplateField HeaderText="交接人">
                            <ItemTemplate>
                                <asp:Label ID="labReceiverName" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="是否转组" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkTransfer" runat="server" onclick="setTransfer(this);" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings Visible="false" />
                </asp:GridView>
            </td>
        </tr>

    </table>

    <table width="100%" class="tableForm" style="margin: 20px 0px 220px 0px; border-color: #CED4E7;">
        <tr>
            <td class="oddrow" style="width: 10%">日志:
            </td>
            <td class="oddrow-l" style="width: 80%" colspan="3">
                <asp:Label runat="server" ID="lblLog"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Button ID="btnAudit" runat="server" CssClass="widebuttons" OnClick="btnAudit_Click" Text=" 确 认 " />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnReturn" runat="server" CssClass="widebuttons" OnClick="btnReturn_Click" Text=" 返 回 " />
            </td>
        </tr>

    </table>
</asp:Content>
