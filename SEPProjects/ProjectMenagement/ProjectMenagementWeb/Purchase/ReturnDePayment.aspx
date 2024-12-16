<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeBehind="ReturnDePayment.aspx.cs" Inherits="FinanceWeb.Purchase.ReturnDePayment" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <%@ register src="../UserControls/Purchase/TopMessage.ascx" tagname="TopMessage"
        tagprefix="uc1" %>
          <script type="text/javascript">
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
    <uc1:TopMessage ID="TopMessage" runat="server" />
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                采购付款信息 [创建日期：<asp:Label ID="lblCreateTime" runat="server"></asp:Label>]
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                PR单号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblPRNo" runat="server"></asp:Label>
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
            <td class="oddrow-l" colspan="3">
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
                成本所属组:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="labDepartment" runat="server" />
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
                <asp:Label ID="lblPayRemark" runat="server" Width="80%" Height="80px"></asp:Label>
            </td>
        </tr>
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
                <asp:Label runat="server" ID="lblSupplierBank"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                开户行帐号:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblSupplierAccount"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="4">
                付款确认
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                实际付款金额:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblFactFee"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                是否有发票:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:RadioButtonList runat="server" ID="radioInvoice" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0">未开</asp:ListItem>
                    <asp:ListItem Value="1">已开</asp:ListItem>
                    <asp:ListItem Value="2">无需发票</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                预计付款日期:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblPreDate"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                选择开户行:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label runat="server" ID="lblBankName"></asp:Label>
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
                付款方式:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label runat="server" ID="lblPaymentType"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                网银号/支票号
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label runat="server" ID="lblPayCode"></asp:Label>
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
    <table class="tableForm" width="100%">
        <tr>
            <td>
                <asp:GridView ID="gvRecipient" runat="server" AutoGenerateColumns="False"  OnRowDataBound="gvRecipient_RowDataBound"
                    DataKeyNames="recipientId" Width="100%" >
                    <Columns>
                        <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%">
                            <HeaderTemplate>
                                <input name="chkAll" id="chkAll" onclick="SelectAll(this);" type="checkbox" />全选
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                             <asp:CheckBox ID="chkItem" runat="server" Checked="false"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="recipientId" HeaderText="recipientId" />
                        <asp:TemplateField HeaderText="流水号" HeaderStyle-Width="6%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# int.Parse(Eval("GID").ToString()).ToString("0000000") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="orderid" HeaderText="订单号" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="8%" />
                        <asp:BoundField DataField="RecipientNo" HeaderText="收货单号" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="8%" />
                        <asp:BoundField DataField="requestorname" HeaderText="申请人" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="6%" />
                        <asp:TemplateField HeaderText="申请时间" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <%# Eval("requisition_committime").ToString() == ESP.Purchase.Common.State.datetime_minvalue ? "" : DateTime.Parse(Eval("requisition_committime").ToString()).ToString("yyyy-MM-dd")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="project_code" HeaderText="项目号" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="11%" />
                        <asp:TemplateField HeaderText="收货金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="11%">
                            <ItemTemplate>
                                <%# Eval("moneytype").ToString() == "美元" ? "＄" + decimal.Parse(Eval("RecipientAmount").ToString()).ToString("#,##0.##") : "￥" + decimal.Parse(Eval("RecipientAmount").ToString()).ToString("#,##0.##")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="supplier_name" HeaderText="供应商" ItemStyle-HorizontalAlign="Center"
                            SortExpression="supplier_name" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
        <td>
             <asp:Button ID="btnSave" Text=" 确定 " runat="server" CssClass="widebuttons" OnClick="btnSave_Click" />
                &nbsp;<asp:Button ID="btnCancel" Text=" 取消 " CssClass="widebuttons" OnClick="btnCancel_Click"
                    runat="server" />
        </td>
        </tr>
    </table>
</asp:Content>
