<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ProjectMediaChange.aspx.cs" Inherits="FinanceWeb.project.ProjectMediaChange" %>

<%@ Register Src="/UserControls/Project/PrepareDisplay.ascx" TagName="Prepare" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" src="../../public/js/DatePicker.js"></script>
    <script>
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }

        function MediaClick() {
            var win = window.open('/Dialogs/searchSupplier.aspx', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }
        function setMedia(id, name, rate) {
            $("#<% =hidMediaId.ClientID %>").val(id);
            $("#<% =txtMediaName.ClientID %>").val(name);
            $("#<% =txtSupplierCostRate.ClientID %>").val(rate);
        }

        function mediaMath(s) {
            var total = $("#<%=hidTotalRecharge.ClientID%>").val();
            var old = $("#<%=txtOldRecharge.ClientID%>").val();
            var nnew = $("#<%=txtRecharge.ClientID%>").val();
            if (s == "o") {
                $("#<%=txtRecharge.ClientID%>").val(parseFloat(total-old).toFixed(2))
            } else {
                $("#<%=txtOldRecharge.ClientID%>").val(parseFloat(total - nnew).toFixed(2))
            }
        }
    </script>
    <table width="100%">
        <tr>
            <td>
                <uc1:Prepare ID="PrepareDisplay" runat="server" />
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading">媒体付款主体信息&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="false" AllowPaging="false" OnRowCommand="gvList_RowCommand">
                    <Columns>
                        <asp:BoundField HeaderText="媒体付款主体" ItemStyle-HorizontalAlign="Center" DataField="MediaName" />
                        <asp:BoundField HeaderText="预估媒体成本比例" DataField="CostRate" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:#,##0.00}" />
                        <asp:BoundField HeaderText="充值金额" DataField="Recharge" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:#,##0.00}" />
                        <asp:BoundField HeaderText="起始日期" ItemStyle-HorizontalAlign="Center" DataField="BeginDate" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:BoundField HeaderText="结束日期" ItemStyle-HorizontalAlign="Center" DataField="EndDate" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton CausesValidation="false" ID="LinkButton1" Visible='<%# Eval("EndDate") == null ? true : false %>' runat="server" CommandArgument='<%# Eval("Id") %>'
                                    CommandName="Change" Text="<img src='../../images/Icon_Copy.gif' title='拆分' border='0'>" />&nbsp;
                                <asp:LinkButton CausesValidation="false" ID="lnkDelete" Visible='<%# Eval("EndDate") == null ? false : false %>' runat="server" CommandArgument='<%# Eval("Id") %>'
                                    CommandName="Del" Text="<img src='../../images/disable.gif' title='删除' border='0'>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>


    </table>
    <asp:HiddenField ID="hidProjectId" runat="server" />
    <asp:HiddenField ID="hidPmId" runat="server" />
    <table width="100%" class="tableForm">
        <tr>
            <td colspan="4" style="border-right:1px solid #fff" class="heading" align="center">原媒体付款主体</td>
            <td colspan="4" style="border-left:1px solid #fff" class="heading" align="center">新媒体付款主体</td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 9%;">媒体主体名称：</td>
            <td style="width: 17%;" class="oddrow-l">
                <asp:Label runat="server" ID="labOldMediaName" /></td>
            <td class="oddrow" style="width: 12%;">预估媒体成本比例：</td>
            <td style="width: 12%;" class="oddrow-l">
                <asp:Label runat="server" ID="labOldSupplierCostRate" /></td>
            <td class="oddrow" style="width: 9%;">媒体主体名称：</td>
            <td style="width: 17%;" class="oddrow-l">
                <asp:HiddenField ID="hidMediaId" runat="server" />
                <asp:TextBox runat="server" onfocus="this.blur();" Style="cursor: hand;" ID="txtMediaName"
                    Width="70%" />&nbsp;<input type="button" id="Button1" class="widebuttons" onclick="return MediaClick();"
                        class="widebuttons" value="搜索" /><font color="red"> *</font><asp:RequiredFieldValidator
                            ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtMediaName" Display="None" ErrorMessage="媒体付款主体为必填项"></asp:RequiredFieldValidator></td>
            <td class="oddrow" style="width: 12%;">预估媒体成本比例：</td>
            <td style="width: 12%;" class="oddrow-l">
                <asp:TextBox ID="txtSupplierCostRate" Text="0" Width="60%" runat="server" /><font color="red"> *</font><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtSupplierCostRate" Display="None" ErrorMessage="预估媒体成本比例为必填项"></asp:RequiredFieldValidator><asp:CompareValidator ID="CompareValidator3" Type="Double" ValueToCompare="0" Operator="GreaterThanEqual" ControlToValidate="txtSupplierCostRate" runat="server" ErrorMessage=" 错误"></asp:CompareValidator></td>
        </tr>
        <tr>
            <td colspan="2" class="oddrow-l">&nbsp;<asp:HiddenField ID="hidTotalRecharge" runat="server" /></td>
                        <td class="oddrow">充值金额：</td>
            <td class="oddrow-l"><asp:TextBox ID="txtOldRecharge" Text="0" Width="60%" runat="server" onchange="mediaMath('o');" /><font color="red"> *</font><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtOldRecharge" Display="None" ErrorMessage="充值金额为必填项"></asp:RequiredFieldValidator><asp:CompareValidator ID="CompareValidator2" Type="Double" ValueToCompare="0" Operator="GreaterThanEqual" ControlToValidate="txtOldRecharge" runat="server" ErrorMessage=" 错误"></asp:CompareValidator></td>
            <td class="oddrow">起始日期：</td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtBeginDate" onkeyDown="return false; " Style="cursor: hand;width:150px;" runat="server"
                                            onclick="setDate(this);" /><font color="red"> *</font><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBeginDate" Display="None" ErrorMessage="起始日期为必填项" />
            </td>
            <td class="oddrow">充值金额：</td>
            <td class="oddrow-l"><asp:TextBox ID="txtRecharge" Text="0" Width="60%" runat="server" onchange="mediaMath('n');" /><font color="red"> *</font><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRecharge" Display="None" ErrorMessage="充值金额为必填项"></asp:RequiredFieldValidator><asp:CompareValidator ID="CompareValidator1" Type="Double" ValueToCompare="0" Operator="GreaterThanEqual" ControlToValidate="txtRecharge" runat="server" ErrorMessage=" 错误"></asp:CompareValidator></td>
        </tr>
        <tr>
            <td colspan="8" class="oddrow-l">
                <asp:Button ID="btnSubmit" runat="server" Text="提交" Visible="false" CssClass="widebuttons" OnClick="btnSubmit_Click" />
                &nbsp;<asp:Button ID="btnBack" runat="server" Text="返回" CausesValidation="false" CssClass="widebuttons" OnClick="btnBack_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
