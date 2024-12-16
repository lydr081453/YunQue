<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="RefundAudit.aspx.cs" Inherits="FinanceWeb.Refund.RefundAudit" %>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>
    <script type="text/javascript" src="/public/js/DatePicker.js"></script>
    <script language="javascript">

        $().ready(function () {


        });

        function NextUserSelect() {
            var win = window.open('/Purchase/FinancialUserList.aspx', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }

        function setnextAuditor(name, sysid) {
            document.getElementById("<%=txtNextAuditor.ClientID %>").value = name;
                    document.getElementById("<%=hidNextAuditor.ClientID %>").value = sysid;
        }

        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }

        function checkValid() {
            var msg = "";
            if (document.getElementById("<%=txtRemark.ClientID %>").value == "") {
                msg += " - 请输入退款说明！" + "\n";
            }
            if (document.getElementById("<%=txtFee.ClientID %>").value == "") {
                msg += " - 请输入预计付款金额！" + "\n";
            } else {
                cost = document.getElementById("<%=txtFee.ClientID %>").value.replace(/,/g, '');
                if (!testNum(cost)) {
                    msg += " - 预计付款金额输入错误！" + "\n";
                }
            }

            if (msg == "") {
                return true;
            }
            else {
                alert(msg);
                return false;
            }
        }
        function testNum(a) {
            a += "";
            a = a.replace(/(^[\\s]*)|([\\s]*$)/g, "");
            if (a != "" && !isNaN(a) && Number(a) >= 0) {
                return true;
            }
            else {
                return false;
            }
        }

    </script>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">采购申请息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">PR单号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                 <asp:Label ID="lblPRNO" runat="server"></asp:Label>
                <asp:HiddenField ID="hidPRID" runat="server" />
            </td>
            <td class="oddrow" style="width: 15%">项目号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblProjectCode" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">申请人姓名:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblApplicant" runat="server" CssClass="userLabel"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">采购金额:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblTotalprice" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">供应商名称:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblSupplierName"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">开户行名称:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblBank"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">开户行帐号:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblAccount"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="4">付款信息
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvG_RowDataBound"
                    DataKeyNames="ReturnID" EmptyDataText="暂时没有相关记录" AllowPaging="false"
                    Width="100%">
                    <Columns>
                        <asp:BoundField DataField="ReturnCode" HeaderText="付款单号" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="10%" />
                        <asp:BoundField DataField="ReturnFactDate" HeaderText="付款日期" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="10%" />
                        <asp:BoundField DataField="ReturnContent" HeaderText="付款内容" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="20%" />
                        <asp:BoundField DataField="SupplierName" HeaderText="供应商" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="20%" />
                        <asp:TemplateField HeaderText="付款金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblAmounts"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="付款状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>

        <tr>
            <td class="heading" colspan="4">退款申请
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">退款金额:
            </td>
            <td class="oddrow-l" style="width: 35%" colspan="3">
                <asp:TextBox ID="txtFee" runat="server" Enabled="false"></asp:TextBox><font color="red">*</font>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtFee"
                            runat="server" ValidationGroup="audit" ErrorMessage="<br />必须填写退款金额!"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">退款日期:
            </td>
            <td class="oddrow-l" style="width: 35%" colspan="3">
                <asp:TextBox ID="txtRefundDate" runat="server" onclick="setDate(this);" onfocus="javascript:this.blur();" ></asp:TextBox><font
                    color="red">*</font>  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtRefundDate"
                            runat="server" ValidationGroup="audit" ErrorMessage="<br />必须填写退款日期!"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">成本项:
            </td>
            <td class="oddrow-l" style="width: 35%" colspan="3">
                <asp:Label runat="server" ID="lblCost"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">退款说明:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtRemark" runat="server" Width="500px" TextMode="MultiLine"
                    Height="50px"></asp:TextBox><font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtRemark"
                            runat="server" ValidationGroup="audit" ErrorMessage="<br />必须填写退款说明!"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="4">审批信息
            </td>
        </tr>
          <tr>
            <td class="oddrow" width="15%">审批历史:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="lblLog" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">审批批示:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtAudit" runat="server" Width="500px" TextMode="MultiLine"
                    Height="50px"></asp:TextBox><asp:LinkButton
                    runat="server" ID="btnTip" OnClick="btnTip_Click" CausesValidation="false" ToolTip="暂时留个Message,以后再操作"><font style=" text-decoration:underline; color:Blue; font-weight:bold">留言</font></asp:LinkButton><font
                        color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtAudit"
                            runat="server" ValidationGroup="audit" ErrorMessage="<br />必须填写审批批示!"></asp:RequiredFieldValidator>
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
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <prc:CheckPRAspButton ID="btnYes" Text="审批通过" ValidationGroup="audit" runat="server" OnClick="btnYes_Click"  CssClass="widebuttons" />
                &nbsp;<prc:CheckPRAspButton ID="btnNo" Text="驳回至申请人" runat="server" CssClass="widebuttons" OnClick="btnNo_Click" />
                &nbsp;<asp:Button ID="btnReturn" Text=" 返回 " CssClass="widebuttons" OnClick="btnReturn_Click" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
