<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="CashPNLink.aspx.cs" Inherits="Purchase_CashPNLink" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%@ register src="../UserControls/Purchase/TopMessage.ascx" tagname="TopMessage"
        tagprefix="uc1" %>
    <uc1:TopMessage ID="TopMessage" runat="server" />
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                采购付款信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                PR单号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblPRNo" runat="server"></asp:Label>
                <input type="hidden" id="hidPrID" runat="server" />
                <input type="hidden" id="hidProjectID" runat="server" />
                <input type="hidden" id="hidPaymentID" runat="server" />
            </td>
            <td class="oddrow" style="width: 15%">
                项目号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblProjectCode" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                申请人姓名:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblApplicant" runat="server" CssClass="userLabel"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                付款申请流水:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblReturnCode" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                预计付款时间:
            </td>
            <td class="oddrow-l"  colspan="3">
                <asp:Label ID="lblBeginDate" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                申请付款金额:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblInceptPrice" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 20%">
                申请付款时间:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="lblInceptDate" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                帐期类型:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblPeriodType" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 20%">
                付款状态:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="lblStatus" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                付款申请描述:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="txtPayRemark" runat="server" Width="80%" Height="80px"></asp:Label>
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                供应商信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                供应商名称:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblSupplierName"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                开户行名称:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="txtSupplierBank"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                开户行帐号:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="txtSupplierAccount" Width="60%"></asp:Label>
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                实际付款信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                实际付款金额:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="txtFactFee" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                是否有发票:
            </td>
            <td class="oddrow-l" colspan="3">
                <%--                <asp:RadioButtonList runat="server" ID="radioInvoice" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0">未开</asp:ListItem>
                    <asp:ListItem Value="1">已开</asp:ListItem>
                    <asp:ListItem Value="2">无需发票</asp:ListItem>
                </asp:RadioButtonList>
                <font color="red">*</font>--%>
                <asp:Label ID="labInvoice" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                预计付款日期:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="txtReturnPreDate" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                选择开户行:<%--<input type="hidden" runat="server" id="hidBankID" />--%>
            </td>
            <td class="oddrow-l" style="width: 35%">
                <%--                <asp:DropDownList runat="server" ID="ddlBank">
                </asp:DropDownList>--%>
                <asp:Label ID="labBank" runat="server" />
            </td>
            <td class="oddrow" style="width: 15%">
                帐号名称:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblAccountName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                银行帐号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblAccount" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                银行地址:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblBankAddress" runat="server" Width="60%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                付款方式:<%--<input type="hidden" runat="server" id="hidPaymentTypeID" /><input type="hidden"
                    runat="server" id="hidIsNeedCode" /><input type="hidden"
                    runat="server" id="hidIsNeedBank" /><input type="hidden"
                    runat="server" id="hidTag" />--%>
            </td>
            <td class="oddrow-l">
                <%--                <asp:DropDownList runat="server" ID="ddlPaymentType">
                </asp:DropDownList>
                <font color="red">*</font>--%>
            </td>
            <td class="oddrow" style="width: 15%">
                <asp:Label runat="server" ID="lblPayCode" Text="网银号/支票号:"></asp:Label>
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label runat="server" ID="txtPayCode"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" width="15%">
                审批历史:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="lblLog" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading">
                抵消现金借款
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="2">
                <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="ReturnID"
                    PageSize="10" EmptyDataText="暂时没有相关记录" PagerSettings-Mode="NumericFirstLast"
                    AllowPaging="true" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <input name="chkItem" id="chkItem" type="checkbox" value='<%# Eval("returnid").ToString() %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PurchasePayID" HeaderText="帐期流水" ItemStyle-HorizontalAlign="Center"
                            Visible="false" />
                        <asp:BoundField DataField="PRID" HeaderText="pr流水" ItemStyle-HorizontalAlign="Center"
                            Visible="false" />
                        <asp:BoundField DataField="ProjectID" HeaderText="项目流水" ItemStyle-HorizontalAlign="Center"
                            Visible="false" />
                        <asp:TemplateField ItemStyle-Width="8%" HeaderText="PR号" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# Eval("Prno")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PN号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <a href="ForegiftDetail.aspx?<% =ESP.Finance.Utility.RequestName.ReturnID %>=<%#Eval("ReturnID") %>">
                                    <%#Eval("ReturnCode")%></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="负责人" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblName" runat="server" CssClass="userLabel" Text='<%# Eval("RequestEmployeeName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ProjectCode" HeaderText="项目号" ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="预付金额" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="labExpectPaymentPrice" Text='<%# Eval("PreFee") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="起始时间" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblBeginDate" Text='<%# Eval("PReBeginDate") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="结束时间" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblEndDate" Text='<%# Eval("PReEndDate") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="帐期类型" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPaymentType" Text='<%# Eval("PaymentTypeName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="oddrow" width="15%">
                抵消金额:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtPrice" runat="server" /><font color="red"> * </font><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                    runat="server" ErrorMessage="抵消金额为必填" ControlToValidate="txtPrice" Display="None" ></asp:RequiredFieldValidator><asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="抵消金额必须大于0" ValueToCompare="0" Operator="GreaterThan" ControlToValidate="txtPrice" Display="None"></asp:CompareValidator>
            </td>
            
        </tr>
        <tr>
            <td class="oddrow">
                描述:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="80%" Height="40px" />
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                <asp:Button ID="btnCommit" runat="server" Text=" 通过 " CssClass="widebuttons" OnClick="btnCommit_Click" />
                <asp:Button ID="btnBack" runat="server" Text=" 返回 " CausesValidation="false" CssClass="widebuttons" OnClick="btnBack_Click" />
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" runat="server" />
</asp:Content>
