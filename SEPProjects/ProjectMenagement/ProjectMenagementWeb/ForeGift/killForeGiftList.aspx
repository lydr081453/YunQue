<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="killForeGiftList.aspx.cs" Inherits="ForeGift_killForeGiftList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" class="tableForm">
        <tr>
            <td colspan="4" class="heading">
                检索
            </td>
        </tr>
        <tr>
            <td class="oddrow">关键字:</td>
            <td class="oddrow-l" colspan="3"><asp:TextBox ID="txtKey" runat="server" /></td>
        </tr>
        <tr>
            <td colspan="4" class="oddrow-l">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading">
                待抵消押金
            </td>
        </tr>
        <tr>
            <td class="oddrow-l">
                <asp:GridView ID="gvList" runat="server" OnRowDataBound="gvG_RowDataBound" PageSize="10"
                    DataKeyNames="returnId" AutoGenerateColumns="False" EmptyDataText="暂时没有相关记录"
                    PagerSettings-Mode="NumericFirstLast" OnPageIndexChanging="gvG_PageIndexChanging"
                    AllowPaging="true" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="returnid" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="PurchasePayID" HeaderText="帐期流水" ItemStyle-HorizontalAlign="Center"
                            Visible="false" />
                        <asp:BoundField DataField="PRID" HeaderText="pr流水" ItemStyle-HorizontalAlign="Center"
                            Visible="false" />
                        <asp:BoundField DataField="ProjectID" HeaderText="项目流水" ItemStyle-HorizontalAlign="Center"
                            Visible="false" />
                        <asp:BoundField HeaderText="PR号" DataField="prno" />
                        <asp:TemplateField HeaderText="PN号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <%#Eval("ReturnCode")%>
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
                        <asp:TemplateField HeaderText="编辑">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:HyperLink ID="hylEdit" runat="server" ImageUrl="/images/edit.gif" ToolTip="编辑"
                                    NavigateUrl='<%# "killForeGift.aspx?" + ESP.Finance.Utility.RequestName.ReturnID + "=" + Eval("ReturnID") %>'
                                    Width="5%"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading">
                已抵消押金
            </td>
        </tr>
        <tr>
            <td class="oddrow-l">
                <asp:GridView ID="gvLog" runat="server" OnRowDataBound="gvLog_RowDataBound" PageSize="10"
                    DataKeyNames="returnId" AutoGenerateColumns="False" EmptyDataText="暂时没有相关记录"
                    PagerSettings-Mode="NumericFirstLast" OnPageIndexChanging="gvLog_PageIndexChanging"
                    AllowPaging="true" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="returnid" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="PurchasePayID" HeaderText="帐期流水" ItemStyle-HorizontalAlign="Center"
                            Visible="false" />
                        <asp:BoundField DataField="PRID" HeaderText="pr流水" ItemStyle-HorizontalAlign="Center"
                            Visible="false" />
                        <asp:BoundField DataField="ProjectID" HeaderText="项目流水" ItemStyle-HorizontalAlign="Center"
                            Visible="false" />
                        <asp:BoundField HeaderText="PR号" DataField="prno" />
                        <asp:TemplateField HeaderText="PN号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <%#Eval("ReturnCode")%>
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
                        <asp:TemplateField HeaderText="查看">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:HyperLink ID="hylEdit" runat="server" ImageUrl="/images/dc.gif" ToolTip="查看"
                                    NavigateUrl='<%# "killForeGiftDetail.aspx?" + ESP.Finance.Utility.RequestName.ReturnID + "=" + Eval("ReturnID") %>'
                                    Width="5%"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
