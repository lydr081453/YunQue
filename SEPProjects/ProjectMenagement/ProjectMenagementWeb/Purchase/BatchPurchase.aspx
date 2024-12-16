<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="BatchPurchase.aspx.cs" Inherits="Purchase_BatchPurchase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script language="javascript">
        function selectAll(obj) {
            var theTable = obj.parentElement.parentElement.parentElement;
            var i;
            var j = obj.parentElement.cellIndex;

            for (i = 0; i < theTable.rows.length; i++) {
                var objCheckBox = theTable.rows[i].cells[j].firstChild;
                if (objCheckBox.checked != null) objCheckBox.checked = obj.checked;
            }
        }
    </script>

    <asp:HiddenField ID="hidBatchId" runat="server" />
    <table width="100%" border="0" background="../images/allinfo_bg.gif">
        <tr style="height: 30px">
            <td width="50%">
               <font style="font-weight: bold; font-size: 15px"> 创建人:<asp:Label ID="labCreateUser"
                    runat="server" /></font>
            </td>
            <td align="right">
                <font style="font-weight: bold; font-size: 15px">批次号:<asp:Label ID="labPurchaeBatchCode"
                    runat="server" /></font>
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
                <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChangeed" />
                <font color="red">* </font>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="请选择公司"
                    Display="None" ControlToValidate="ddlCompany" ValueToCompare="0" Operator="NotEqual"></asp:CompareValidator>
            </td>
            <td class="oddrow" width="15%">
                付款方式:
            </td>
            <td class="oddrow-l" width="35%">
                <asp:DropDownList ID="ddlPaymentType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPaymentType_SelectedIndexChangeed">
                    <asp:ListItem Text="银行转帐" Value="3" />
                </asp:DropDownList>
                <font color="red">* </font>
                <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="请选择付款方式"
                    Display="None" ControlToValidate="ddlPaymentType" ValueToCompare="0" Operator="NotEqual"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                供应商:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtSupplier" runat="server" MaxLength="50" Width="200px" />
            </td>
            <td class="oddrow">
                关键字:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtKey" runat="server" MaxLength="20" Width="200px" />
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Button ID="btnSearch" runat="server" CausesValidation="false" Text=" 检索 " CssClass="widebuttons"
                    OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
    <asp:Panel ID="palPN" runat="server">
        <table width="100%" class="tableForm">
            <tr>
                <td class="heading">
                    待审核付款申请
                </td>
            </tr>
            <tr>
                <td class="oddrow-l">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ReturnID"
                        OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" EmptyDataText="暂时没有相关记录"
                        AllowPaging="False" Width="100%">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <input id="chkItem" name="chkItem" type="checkbox" value='<%#Eval("ReturnID") %>' />
                                </ItemTemplate>
                                <HeaderTemplate>
                                    &nbsp;<input id="CheckAll" type="checkbox" onclick="selectAll(this);" />全选
                                </HeaderTemplate>
                            </asp:TemplateField>
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
                            <asp:TemplateField HeaderText="预计付款时间" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblBeginDate" Text='<%# DateTime.Parse(Eval("PReBeginDate").ToString()).ToString("yyyy-MM-dd") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="供应商" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblEndDate" Text='<%# Eval("supplierName") %>' />
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
                            <asp:TemplateField HeaderText="驳回" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnCancel" runat="server" Text="<img title='驳回付款申请' src='../../images/Icon_Cancel.gif' border='0' />"
                                        OnClientClick="return confirm('您是否确认驳回付款申请？');" CausesValidation="false" CommandArgument='<%# Eval("ReturnID") %>'
                                        CommandName="Return" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text=" 添加 " CausesValidation="false" CssClass="widebuttons"
                        OnClick="btnCreate_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading">
                批次内付款申请<asp:Button ID="btnEditList" runat="server" Visible="false" Text="编辑付款列表"
                    OnClick="btnEditList_Click" CssClass="widebuttons" />
            </td>
        </tr>
        <tr>
            <td class="oddrow-l">
                <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="ReturnID"
                    OnRowCommand="gvG_RowCommand" OnRowDataBound="gvG_RowDataBound" EmptyDataText="暂时没有相关记录"
                    AllowPaging="False" Width="100%">
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
                        <asp:TemplateField HeaderText="预计付款日期" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblBeginDate" Text='<%# DateTime.Parse(Eval("PReBeginDate").ToString()).ToString("yyyy-MM-dd") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="供应商" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblEndDate" Text='<%# Eval("supplierName") %>' />
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
                        <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%"
                            Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblEdit" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="移除" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("ReturnID") %>'
                                    CommandName="Del" Text="<img src='../../images/disable.gif' title='移除' border='0'>"
                                    OnClientClick="return confirm('你确定移除吗？');" />
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
        <tr>
            <td class="oddrow" width="15%">
                审批备注:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtRemark" runat="server" Width="50%" /><font color="red"> * </font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtRemark"
                    Display="None" runat="server" ErrorMessage="请填写审批备注"></asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
    <table width="100%" class="XTable">
        <tr>
            <td align="center">
                <asp:Button ID="btnSave" runat="server" Text=" 保存 " Visible="false" CssClass="widebuttons"
                    CausesValidation="false" OnClick="btnSave_Click" />
                <asp:Button ID="btnYes" runat="server" Text="审批通过" OnClientClick="if(Page_ClientValidate()){return confirm('您确定审批通过吗？');}"
                    CausesValidation="false" CssClass="widebuttons" OnClick="btnYes_Click" />
                <asp:Button ID="btnNo" runat="server" Text="驳回至申请人" Visible="false" OnClientClick="return confirm('您确定驳回至申请人吗？');"
                    CssClass="widebuttons" OnClick="btnNo_Click" />
                <asp:Button ID="btnNo1" runat="server" Text=" 驳回 " Visible="false" OnClientClick="if(Page_ClientValidate()){return confirm('您确定驳回吗？');}"
                    CausesValidation="false" CssClass="widebuttons" OnClick="btnNo1_Click" />
                <asp:Button ID="btnBack" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnBack_Click"
                    CausesValidation="false" />
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
        runat="server" />
</asp:Content>
