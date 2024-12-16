<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="BatchPurchaseDisplay.aspx.cs" Inherits="Purchase_BatchPurchaseDisplay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" border="0" background="../images/allinfo_bg.gif">
        <tr style="height: 30px">
            <td width="50%">
                <font style="font-weight: bold; font-size: 15px"> 批次流水:
                <asp:Label runat="server" ID="lblBatchId"></asp:Label></font>
            </td>
            <td align="right">
                <font style="font-weight: bold; font-size: 15px">批次号:<asp:Label ID="labPurchaeBatchCode"
                    runat="server" /></font>
            </td>
        </tr>
        <tr>
             <td width="50%">
               <font style="font-weight: bold; font-size: 15px"> 创建人:<asp:Label ID="labCreateUser"
                    runat="server" /></font>
            </td>
              <td align="right">
                <font style="font-weight: bold; font-size: 15px">银行凭证号:
            <asp:Label ID="lblBatchCode" runat="server"></asp:Label></font>
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                批次付款
            </td>
        </tr>
        <tr>
            <td class="oddrow" width="15%">
                公司选择:
            </td>
            <td class="oddrow-l" width="35%">
                <asp:DropDownList ID="ddlCompany" runat="server" />
            </td>
            <td class="oddrow" width="15%">
                付款方式:
            </td>
            <td class="oddrow-l" width="35%">
                <asp:DropDownList ID="ddlPaymentType" runat="server" />
        </tr>
    </table>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading">
                付款列表
            </td>
        </tr>
        <tr>
            <td class="oddrow-l">
                <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="ReturnID"
                    OnRowDataBound="gvG_RowDataBound" EmptyDataText="暂时没有相关记录" AllowPaging="False"
                    Width="100%">
                    <Columns>
                        <asp:BoundField DataField="ReturnID" HeaderText="ReturnID" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="PurchasePayID" HeaderText="帐期流水" ItemStyle-HorizontalAlign="Center"
                            Visible="false" />
                        <asp:BoundField DataField="PRID" HeaderText="pr流水" ItemStyle-HorizontalAlign="Center"
                            Visible="false" />
                        <asp:BoundField DataField="ProjectID" HeaderText="项目流水" ItemStyle-HorizontalAlign="Center"
                            Visible="false" />
                        <asp:TemplateField ItemStyle-Width="8%" HeaderText="PR号" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPR"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PN号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <a href="/Purchase/ReturnDisplay.aspx?<% =ESP.Finance.Utility.RequestName.ReturnID %>=<%#Eval("ReturnID") %>"
                                    target="_blank">
                                    <%#Eval("ReturnCode")%></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="GR号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <asp:Literal ID="litGRNo" runat="server" />
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
                        <asp:TemplateField HeaderText="付款状态" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblStatusName" />
                                <asp:HiddenField ID="hidStatusNameID" Value='<%# Eval("ReturnStatus") %>' runat="server" />
                                <asp:HiddenField ID="hidReturnID" Value='<%# Eval("ReturnID") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="帐期类型" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPaymentType" Text='<%# Eval("PaymentTypeName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="供应商名称" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblSupplier" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="打印预览" ItemStyle-Width="5%">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:HyperLink ID="hylPrint" Target="_blank" NavigateUrl='<%# "Print/PrintPRGR.aspx?" + ESP.Finance.Utility.RequestName.ReturnID+"="+Eval("ReturnID").ToString()  %>'
                                    runat="server" ImageUrl="/images/Icon_Output.gif" ToolTip="打印预览" Width="4%"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td align="right">
                付款总额:<asp:Label ID="labTotal" runat="server" />
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="2">
                审批信息
            </td>
        </tr>
        <tr>
            <td width="15%" class="oddrow">
                审批历史:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labAuditLog" runat="server" />
            </td>
        </tr>
    </table>
    <table width="100%" class="XTable">
        <tr>
            <td align="center">
                <asp:Button ID="btnBack" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnBack_Click"
                    CausesValidation="false" />
            </td>
        </tr>
    </table>
</asp:Content>
