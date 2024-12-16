<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExpenseAccountView.aspx.cs"
    EnableEventValidation="false" Inherits="FinanceWeb.ExpenseAccount.ExpenseAccountView"
    MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script type="text/javascript" src="/public/js/jquery.blockUI.js"></script>

    <script language="javascript">
        $().ready(function () {
            $("#<%=ddlBank.ClientID %>").empty();
            FinanceWeb.ExpenseAccount.ExpenseAccountView.GetBanks(parseInt('<%=Request["workitemid"] %>'), initBank);
            function initBank(r) {
                if (r.value != null)
                    for (k = 0; k < r.value.length; k++) {
                        if (r.value[k][0] + ',' + r.value[k][1] == $("#<%=hidBankID.ClientID %>").val()) {
                            $("#<%=ddlBank.ClientID %>").append("<option value=\"" + r.value[k][0] + "\" selected>" + r.value[k][1] + "</option>");
                    }
                    else {
                        $("#<%=ddlBank.ClientID %>").append("<option value=\"" + r.value[k][0] + "\">" + r.value[k][1] + "</option>");
                    }
                }
        }
        });

    function NextUserSelect() {
        var win = window.open('/Purchase/FinancialUserList.aspx', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }

    function setnextAuditor(name, sysid) {
        document.getElementById("<%=txtNextAuditor.ClientID %>").value = name;
            document.getElementById("<%=hidNextAuditor.ClientID %>").value = sysid;
        }




        function selectBank(id, text) {
            if (id == "-1") {
                document.getElementById("<% =hidBankID.ClientID %>").value = "";
            }
            else {
                FinanceWeb.ExpenseAccount.ExpenseAccountView.GetBankModel(id, ChangeBank);
                function ChangeBank(r) {
                    if (r.value != null) {
                        if (r.value[2] != null)
                            document.getElementById("<%=lblAccount.ClientID %>").innerHTML = r.value[2];
                        if (r.value[3] != null)
                            document.getElementById("<%=lblAccountName.ClientID %>").innerHTML = r.value[3]
                        if (r.value[4] != null)
                            document.getElementById("<%=lblBankAddress.ClientID %>").innerHTML = r.value[4];;
                    }
                }
                document.getElementById("<% =hidBankID.ClientID %>").value = id + "," + text;

            }
        }

    </script>

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">单据信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">PN号:
            </td>
            <td class="oddrow-l" style="width: 90%" colspan="3">
                <asp:Label runat="server" ID="labPNcode" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">申请人:
            </td>
            <td class="oddrow-l" style="width: 40%">
                <asp:Label runat="server" ID="labRequestUserName" />(<asp:Label runat="server" ID="labRequestUserCode" />)
            </td>
            <td class="oddrow" style="width: 10%">申请日期:
            </td>
            <td class="oddrow-l" style="width: 40%">
                <asp:Label runat="server" ID="labRequestDate" />
            </td>
        </tr>
        <asp:Panel runat="server" ID="panBank" Visible="false">
            <tr>
                <td class="oddrow" style="width: 10%">开户银行:
                </td>
                <td class="oddrow-l" style="width: 40%">
                    <asp:Label runat="server" ID="lblCreatorBank" />
                </td>
                <td class="oddrow" style="width: 10%">银行账号:
                </td>
                <td class="oddrow-l" style="width: 40%">
                    <asp:Label runat="server" ID="lblCreatorAccount" />
                </td>
            </tr>
        </asp:Panel>
        <tr>
            <td class="oddrow">项目号:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="txtproject_code1" runat="server" />
            </td>
            <td class="oddrow">项目名称:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="txtproject_descripttion" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">成本所属组:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labDepartment" runat="server" />
            </td>
            <td class="oddrow">预计申请金额:
            </td>
            <td class="oddrow-l">
                <asp:Label runat="server" ID="labPreFee" />
            </td>
        </tr>
        <asp:Panel ID="panInvoice" runat="server" Visible="false">
            <tr>
                <td class="oddrow" style="width: 15%">是否有发票:
                </td>
                <td class="oddrow-l" colspan="3">
                    <asp:RadioButtonList runat="server" ID="radioInvoice" RepeatDirection="Horizontal">
                        <asp:ListItem Value="0" Selected="True">未开</asp:ListItem>
                        <asp:ListItem Value="1">已开</asp:ListItem>
                        <asp:ListItem Value="2">无需发票</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </asp:Panel>
        <tr>
            <td class="oddrow">单据类型:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="labReturnType" />
            </td>
        </tr>
        <tr runat="server" id="trCheckNo1" visible="false">
            <td class="oddrow" style="width: 15%">选择开户行:<input type="hidden" runat="server" id="hidBankID" />
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:DropDownList runat="server" ID="ddlBank" Style="width: auto">
                </asp:DropDownList>
            </td>
            <td class="oddrow" style="width: 15%">帐号名称:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblAccountName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr runat="server" id="trCheckNo2" visible="false">
            <td class="oddrow" style="width: 15%">银行帐号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblAccount" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">银行地址:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblBankAddress" runat="server" Width="60%"></asp:Label>
            </td>
        </tr>
        <tr runat="server" id="trCheckNo" visible="false">
            <td class="oddrow">支票号:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox runat="server" ID="txtCheckNo"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow">历次审批记录:
            </td>
            <td class="oddrow-l" colspan="3">
                <%--<asp:TextBox runat="server" ID="txtHisSuggestion" TextMode="MultiLine" Width="60%" Rows="6" ReadOnly="true"></asp:TextBox>--%>
                <asp:Label runat="server" ID="labSuggestion" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">审批意见:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox runat="server" ID="txtSuggestion" TextMode="MultiLine" Width="40%" Rows="3"></asp:TextBox><font
                    color="red"> * </font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="请填写审批意见!"
                    ControlToValidate="txtSuggestion" Display="None" ErrorMessage="请填写审批意见"></asp:RequiredFieldValidator>
                <asp:LinkButton runat="server" ID="btnTip" OnClick="btnTip_Click" CausesValidation="false"
                    ToolTip="暂时留个Message,以后再操作"><font style=" text-decoration:underline; color:Blue; font-weight:bold">留言</font></asp:LinkButton>
            </td>
        </tr>
        <tr id="trConfirmFee" runat="server">
            <td class="oddrow">收货金额:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="labConfirmFee" />
            </td>
        </tr>
        <tr id="trF3" runat="server">
            <td class="oddrow">
                <asp:Label ID="labFactFeeName" runat="server" Text="实际支付金额:" />
            </td>
            <td class="oddrow-l" colspan="3">
                <ComponentArt:NumberInput ID="txtFactFee" runat="server" EmptyText="0.00" NumberType="Number">
                </ComponentArt:NumberInput>
                <font color="red">* </font>
            </td>
        </tr>
        <tr id="trNext" runat="server">
            <td class="oddrow">下级审批人:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtNextAuditor" runat="server" onkeyDown="return false; " Style="cursor: hand" /><font
                    color="red"> * </font>
                <input type="button" value="选择" class="widebuttons" onclick="return NextUserSelect();" />
                <asp:HiddenField ID="hidNextAuditor" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Button ID="btnAudit" runat="server" Text="审批通过" CssClass="widebuttons" OnClick="btnAudit_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnAuditF" runat="server" Text="审批通过" CssClass="widebuttons" OnClick="btnAuditF_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnUnAudit" runat="server" Text="驳回至申请人" CssClass="widebuttons" OnClick="btnUnAudit_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnUnAuditF" runat="server" Text="驳回至财务第一级" CssClass="widebuttons"
                    OnClick="btnUnAuditF_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnPrint" runat="server" Text=" 打印 " CssClass="widebuttons" CausesValidation="false"
                        OnClick="btnPrint_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnReturn" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnReturn_Click"
                    CausesValidation="false" />
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">申请单明细
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="ID,ReturnID"
                    OnRowCommand="gvG_RowCommand" OnRowDataBound="gvG_RowDataBound" OnRowCreated="gvG_RowCreated"
                    EmptyDataText="暂时没有相关记录" AllowPaging="false" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="ExpenseDate" HeaderText="费用发生日期" ItemStyle-HorizontalAlign="Center"
                            DataFormatString="{0:d}" />
                        <asp:TemplateField HeaderText="项目成本明细" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="labCostDetailName" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="费用类型" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="labExpenseTypeName" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="费用明细描述" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblExpenseDesc" Text='<%# Eval("ExpenseDesc") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ExpenseTypeNumber" HeaderText="数量" ItemStyle-HorizontalAlign="Right" />
                        <asp:TemplateField HeaderText="总金额" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblExpenseMoney" Text='<%# Eval("ExpenseMoney") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="收款人" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="labRecipient" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="所在城市" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="labCity" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="银行名称" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="labBankName" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="银行账号" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="labBankAccountNo" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Boarder" HeaderText="登机人" ItemStyle-HorizontalAlign="center"
                            ItemStyle-Width="7%" />
                        <asp:BoundField DataField="GoAirNo" HeaderText="航班号" ItemStyle-HorizontalAlign="center"
                            ItemStyle-Width="7%" />
                        <asp:BoundField DataField="BoarderMobile" HeaderText="联系电话" ItemStyle-HorizontalAlign="center"
                            ItemStyle-Width="7%" />
                        <asp:BoundField DataField="TicketSource" HeaderText="出发地" ItemStyle-HorizontalAlign="center"
                            ItemStyle-Width="7%" />
                        <asp:BoundField DataField="TicketDestination" HeaderText="目的地" ItemStyle-HorizontalAlign="center"
                            ItemStyle-Width="7%" />
                         <asp:TemplateField HeaderText="新建">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:LinkButton ID="faAdd" runat="server" Text="<img title='新建' src='../../images/dc.gif' border='0' />"
                                    CausesValidation="false" CommandArgument='<%# Eval("ID")%>' CommandName="faAdd" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="编辑">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:LinkButton ID="faEdit" runat="server" Text="<img title='编辑' src='../../images/edit.gif' border='0' />"
                                    CausesValidation="false" CommandArgument='<%# Eval("ID")%>' CommandName="faEdit" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                    ShowSummary="false" />
            </td>
        </tr>
    </table>
</asp:Content>
