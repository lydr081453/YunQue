<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TicketConfirm.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="FinanceWeb.Ticket.TicketConfirm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript" src="../../public/js/DatePicker.js"></script>

    <script language="javascript" type="text/javascript">

        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }

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

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">检索
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">关键字:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtKey" runat="server" />
            </td>
            <td class="oddrow" style="width: 15%">公司选择:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:DropDownList runat="server" ID="ddlBranch" Style="width: auto">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">出票日:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtBeginDate" onkeyDown="return false; " Style="cursor: hand" runat="server"
                    onclick="setDate(this);" />--
                <asp:TextBox ID="txtEndDate" onkeyDown="return false; " Style="cursor: hand" runat="server"
                    onclick="setDate(this);" />
            </td>
            <td class="oddrow" style="width: 15%">供应商:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtSupplier" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click" />&nbsp;
                <asp:Button ID="btnSearchAll" runat="server" Text=" 检索全部 " CssClass="widebuttons"
                    OnClick="btnSearchAll_Click" />
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td style="color: Red;">
                <asp:CheckBox runat="server" ID="chkUsed" Checked="false" Text="查询已使用的机票申请" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                <asp:GridView ID="gvUsed" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                    OnRowDataBound="gvUsed_RowDataBound" EmptyDataText="暂时没有机票记录" AllowPaging="false"
                    Width="100%">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" />
                        <asp:TemplateField ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkTicket" AutoPostBack="true" runat="server" OnCheckedChanged="chkTicket_OnCheckedChanged"
                                    Checked="false" Text='' />
                            </ItemTemplate>
                            <HeaderTemplate>
                                &nbsp;<input id="CheckAll" type="checkbox" onclick="selectAll(this);" />全选
                            </HeaderTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="使用状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblTicketUsed" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="出票日" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="labOrderDate" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="申请单号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblReturnCode" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="GoAirNo" HeaderText="航班号" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="8%" />
                        <asp:BoundField DataField="TicketSource" HeaderText="出发地" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="8%" />
                        <asp:BoundField DataField="TicketDestination" HeaderText="目的地" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="8%" />
                        <asp:BoundField DataField="BankName" HeaderText="出发日期" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="8%" />
                        <asp:BoundField DataField="Boarder" HeaderText="登机人" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="8%" />
                        <asp:BoundField DataField="BoarderMobile" HeaderText="联系电话" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="8%" />
                        <asp:BoundField DataField="ExpenseMoney" HeaderText="价格" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="8%" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnUsed" runat="server" Text=" 更新使用状态 " CssClass="widebuttons" OnClick="btnUsed_Click" />&nbsp;
                <div style="float: right; font-weight: bolder;">
                    <asp:Label runat="server" ID="lblUsedTotal"></asp:Label>&nbsp;&nbsp;
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
