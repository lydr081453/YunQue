<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeBehind="PCCredenceRpt.aspx.cs" Inherits="FinanceWeb.Reports.PCCredenceRpt" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script type="text/javascript">
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }

      
        function selectBranch(id, text) {
            if (id == "-1") {
                document.getElementById("<% =hidBranchID.ClientID %>").value = "";
                document.getElementById("<% =hidBranchName.ClientID %>").value = "";
            }
            else {
                document.getElementById("<% =hidBranchID.ClientID %>").value = id;
                document.getElementById("<% =hidBranchName.ClientID %>").value = text;
            }
        }
    </script>

    <asp:GridView ID="gvTest" runat="server">
    </asp:GridView>
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
                                        关键字:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:TextBox ID="txtKey" runat="server" />
                                    </td>
                                    <td class="oddrow" style="width: 15%">
                                        付款区间:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:TextBox ID="txtBeginDate" onkeyDown="return false; " Style="cursor: hand" runat="server"
                                            onclick="setDate(this);" />--
                                        <asp:TextBox ID="txtEndDate" onkeyDown="return false; " Style="cursor: hand" runat="server"
                                            onclick="setDate(this);" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow" style="width: 15%">
                                        公司选择:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:DropDownList runat="server" ID="ddlBranch" Style="width: auto">
                                        </asp:DropDownList>
                                        <input type="hidden" id="hidBranchID" runat="server" />
                                        <input type="hidden" id="hidBranchName" runat="server" />
                                    </td>
                                    <td class="oddrow" style="width: 15%">
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow-l" colspan="4">
                                        <asp:Button ID="btnSearch" runat="server" Text=" 检索 " OnClientClick="return CheckInput();"
                                            CssClass="widebuttons" OnClick="btnSearch_Click" />&nbsp;
                                        <asp:Button ID="btnSearchAll" runat="server" Text=" 检索全部 " CssClass="widebuttons"
                                            OnClick="btnSearchAll_Click" />&nbsp;
                                             <asp:Button ID="btnExport" runat="server" Text=" 导出 " CssClass="widebuttons"
                                            OnClick="btnExport_Click" />
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
                                        <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="ReturnId"
                                            OnRowDataBound="gvG_RowDataBound" PageSize="50" EmptyDataText="暂时没有付款申请记录" PagerSettings-Mode="NumericFirstLast"
                                            OnPageIndexChanging="gvG_PageIndexChanging" AllowPaging="true" Width="100%">
                                            <Columns>
                                                <asp:BoundField DataField="ReturnCode" HeaderText="PN单号" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="8%" />
                                                <asp:TemplateField HeaderText="申请人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblRequest" Text='<%#Eval("RequestEmployeeName") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="业务组" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblGroup" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="付款日期" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblDate"  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="付款金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblFee" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ProjectCode" HeaderText="项目号" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="15%" />
                                               
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
