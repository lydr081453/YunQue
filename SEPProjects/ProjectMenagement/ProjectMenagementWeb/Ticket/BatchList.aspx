<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="BatchList.aspx.cs" Inherits="FinanceWeb.Ticket.BatchList" %>

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
                公司选择:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:DropDownList runat="server" ID="ddlBranch" Style="width: auto">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                出票日:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtBeginDate" onkeyDown="return false; " Style="cursor: hand" runat="server"
                    onclick="setDate(this);" />--
                <asp:TextBox ID="txtEndDate" onkeyDown="return false; " Style="cursor: hand" runat="server"
                    onclick="setDate(this);" />
            </td>
            <td class="oddrow" style="width: 15%">
                供应商:
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
    
    
    <asp:GridView ID="gdBatch" runat="server" AutoGenerateColumns="False" OnRowDataBound="gdBatch_RowDataBound"
                                            OnRowCommand="gdBatch_RowCommand" OnPageIndexChanging="gdBatch_PageIndexChanging"
                                            DataKeyNames="BatchID" OnRowDeleting="gdBatch_RowDeleting" EmptyDataText="暂时没有相关记录"
                                            Width="100%">
                                            <Columns>
                                                <asp:BoundField DataField="BatchID" HeaderText="批次流水" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="10%" />
                                                <asp:BoundField DataField="BatchCode" HeaderText="批次号" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="10%" />
                                                <asp:BoundField DataField="BranchCode" HeaderText="公司代码" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="10%" />
                                                <asp:TemplateField HeaderText="批次合计" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblTotal"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="返点金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblReturn"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="付款总额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblAmounts"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="付款状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="PaymentEmployeeName" HeaderText="当前审批人" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="10%" />
                                                <asp:TemplateField HeaderText="编辑" ItemStyle-Width="5%">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:HyperLink runat="server" ID="hylEdit" ImageUrl="/images/edit.gif"></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="删除" ItemStyle-Width="5%">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("BatchID") %>'
                                                            CommandName="Delete" Text="<img src='/images/disable.gif' title='删除' border='0'>"
                                                            OnClientClick="return confirm('你确定删除吗？');" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="打印" ItemStyle-Width="5%">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:HyperLink runat="server" ID="hylPrint" ImageUrl="/images/Icon_PrintPo.gif"></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="导出" ItemStyle-Width="5%">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkExport" runat="server" CommandArgument='<%# Eval("BatchID") %>' OnClick="lnkExport_Click"
                                                            CommandName="Export" Text="<img src='/images/dc.gif' title='导出到财务' border='0'>" />
                                                        <asp:LinkButton ID="lnkExportHR" runat="server" CommandArgument='<%# Eval("BatchID") %>' OnClick="lnkExportHR_Click"
                                                            CommandName="ExportHR" Text="<img src='/images/dc.gif' title='导出到HR' border='0'>" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
    
    </asp:Content>