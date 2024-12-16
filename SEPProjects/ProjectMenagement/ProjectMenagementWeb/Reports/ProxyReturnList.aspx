<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeBehind="ProxyReturnList.aspx.cs" Inherits="FinanceWeb.Reports.ProxyReturnList" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script type="text/javascript">
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }
        function showLog() {
            var log = document.getElementById("<%=lblLog.ClientID %>");
            if (log.style.display == "" || log.style.display == "none") {
                log.style.display = "block";
                document.getElementById("btnLog").value = " 隐藏日志 ";
            }
            else {
                log.style.display = "none";
                document.getElementById("btnLog").value = " 显示日志 ";
            }
        }
    </script>

    <table style="width: 100%">
        <tr>
            <td colspan="2" style="padding: 4px;">
                <table style="width: 100%;">
                    <tr>
                        <td colspan="2">
                            <table width="100%" class="tableForm">
                                <tr>
                                    <td class="heading" colspan="4">
                                        检索
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow" style="width: 15%">
                                        报表区间:
                                    </td>
                                    <td class="oddrow-l" colspan="3">
                                        <asp:TextBox ID="txtBeginDate" onkeyDown="return false; " Style="cursor: hand" runat="server"
                                            onclick="setDate(this);" />--
                                        <asp:TextBox ID="txtEndDate" onkeyDown="return false; " Style="cursor: hand" runat="server"
                                            onclick="setDate(this);" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow" style="width: 15%">
                                    </td>
                                    <td class="oddrow-l" colspan="3">
                                        <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click" />&nbsp;
                                        <asp:Button ID="btnSearchAll" runat="server" Text=" 检索全部 " CssClass="widebuttons"
                                            OnClick="btnSearchAll_Click" />&nbsp;
                                        <asp:Button ID="btnExport" runat="server" Text=" 导出 " CssClass="widebuttons" OnClick="btnExport_Click"
                                            Style="width: 50px" />&nbsp;
                                        <input type="button" id="btnLog" onclick="showLog();" value=" 隐藏日志 "  class="widebuttons" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow-l" colspan="4">
                                        <asp:Label runat="server" ID="lblLog"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                            padding-top: 4px;">
                            <table width="100%" class="tableForm">
                                <tr>
                                    <td class="heading" colspan="4">
                                        付款申请列表
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow">
                                        <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvG_RowDataBound"
                                            PageSize="10" EmptyDataText="暂时没有付款申请记录" PagerSettings-Mode="NumericFirstLast"
                                            OnPageIndexChanging="gvG_PageIndexChanging" AllowPaging="true" Width="100%">
                                            <Columns>
                                                <asp:BoundField DataField="ReturnCode" HeaderText="PN单号" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="8%" />
                                                <asp:BoundField DataField="ProjectCode" HeaderText="项目号" ItemStyle-HorizontalAlign="Center" />
                                                <asp:TemplateField HeaderText="费用发生日期" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblDate" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="requestemployeename" HeaderText="申请人" ItemStyle-HorizontalAlign="Center">
                                                </asp:BoundField>
                                                <asp:BoundField DataField="code" HeaderText="员工编码" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="15%" />
                                                <asp:BoundField DataField="departmentname" HeaderText="费用所属组" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="15%" />
                                                <asp:BoundField DataField="SupplierName" HeaderText="供应商" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="returncontent" HeaderText="费用描述" ItemStyle-HorizontalAlign="Center" />
                                                <asp:TemplateField HeaderText="申请金额" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblFee" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 10px">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
