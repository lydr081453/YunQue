<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinanceBatchAuditEdit.aspx.cs"
    Inherits="FinanceWeb.ExpenseAccount.FinanceBatchAuditEdit" MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript">
        function NextUserSelect() {
            var win = window.open('/Purchase/FinancialUserList.aspx', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }

        function setnextAuditor(name, sysid) {
            document.getElementById("<%=txtNextAuditor.ClientID %>").value = name;
            document.getElementById("<%=hidNextAuditor.ClientID %>").value = sysid;
        }
    </script>

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                批次信息
            </td>
        </tr>
        <tr>
            <td style="width: 15%">
                批次流水:
            </td>
            <td style="width: 35%">
                <asp:Label runat="server" ID="lblBatchId"></asp:Label>
            </td>
            <td style="width: 15%">
                批次号:
            </td>
            <td style="width: 35%">
                <asp:Label runat="server" ID="lblPurchaseBatchCode"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                银行凭证号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtBatchCode" runat="server" />&nbsp;&nbsp;&nbsp;
            </td>
            <td class="oddrow" style="width: 15%">
                总金额:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label runat="server" ID="lblTotal"></asp:Label>
            </td>
        </tr>
        <tr id="trNextAuditer" runat="server">
            <td class="oddrow">
                下级审批人:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtNextAuditor" runat="server" onkeyDown="return false; " Style="cursor: hand" /><font
                    color="red"> * </font>
                <input type="button" value="选择" class="widebuttons" onclick="return  NextUserSelect();" />
                <asp:HiddenField ID="hidNextAuditor" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="width: 15%">
                公司选择:
            </td>
            <td style="width: 35%">
                <asp:DropDownList runat="server" ID="ddlBranch" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChangeed">
                </asp:DropDownList>
            </td>
            <td style="width: 15%">
                选择开户行:
            </td>
            <td style="width: 35%">
                <asp:DropDownList runat="server" ID="ddlBank"  style="width:auto" AutoPostBack="true" OnSelectedIndexChanged="ddlBank_SelectedIndexChangeed">
                </asp:DropDownList>
                <font color="red">* </font>
            </td>
        </tr>
        <tr>
            <td style="width: 15%">
                帐号名称:
            </td>
            <td style="width: 35%">
                <asp:Label ID="lblAccountName" runat="server"></asp:Label>
            </td>
            <td style="width: 15%">
                银行帐号:
            </td>
            <td style="width: 35%">
                <asp:Label ID="lblAccount" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 15%">
                银行地址:
            </td>
            <td style="width: 35%">
                <asp:Label ID="lblBankAddress" runat="server" Width="60%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                审批日志:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblLog"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                审批意见:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox runat="server" ID="txtSuggestion" TextMode="MultiLine" Width="30%" Rows="3"></asp:TextBox><font
                    color="red"> * </font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="请填写审批意见!"
                    ControlToValidate="txtSuggestion" Display="None" ErrorMessage="请填写审批意见"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Button ID="btnAuditF" runat="server" Text="审批通过" OnClientClick="showLoading();" usesubmitbehavior="false" CssClass="widebuttons" OnClick="btnAuditF_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnUnAudit" runat="server" Text="驳回至申请人" CssClass="widebuttons" OnClick="btnUnAudit_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnUnAuditF" runat="server" Text="驳回至财务第一级" CssClass="widebuttons"
                    OnClick="btnUnAuditF_Click" />&nbsp;&nbsp;
                <asp:Button runat="server" ID="btnModifyBatchCode" class="widebuttons" OnClick="btnModifyBatchCode_Click"
                    Text=" 保存 " CausesValidation="false" />&nbsp;&nbsp;
                <asp:Button ID="btnReturn" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnReturn_Click"
                    CausesValidation="false" />
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                申请单明细
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="WorkItemID,ReturnID"
                    OnRowCommand="gvG_RowCommand" OnRowDataBound="gvG_RowDataBound" EmptyDataText="暂时没有相关记录"
                    AllowPaging="false" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="ParticipantName" HeaderText="审批角色" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="ReturnCode" HeaderText="申请单号" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="ProjectCode" HeaderText="项目号" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="ReturnContent" HeaderText="项目名称" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="DepartmentName" HeaderText="费用所属组" ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="预计报销金额" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPreFee" Text='<%# Eval("PreFee") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="RequestEmployeeName" HeaderText="申请人" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="CommitDate" HeaderText="提交日期" ItemStyle-HorizontalAlign="Center"
                            DataFormatString="{0:d}" />
                        <asp:BoundField DataField="LastAuditPassTime" HeaderText="业务审批通过日期" ItemStyle-HorizontalAlign="Center"
                            DataFormatString="{0:d}" />
                        <asp:BoundField DataField="ReturnTypeName" HeaderText="单据类型" ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="打印预览">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:HyperLink ID="hylPrint" runat="server" ImageUrl="/images/Icon_Output.gif" ToolTip="打印预览"
                                    Width="4%"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="查看">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:HyperLink ID="hylView" runat="server" ImageUrl="/images/dc.gif" ToolTip="查看"
                                    Width="4%"></asp:HyperLink>
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
