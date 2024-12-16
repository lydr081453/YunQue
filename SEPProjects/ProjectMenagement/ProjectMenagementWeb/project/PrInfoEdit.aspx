<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeBehind="PrInfoEdit.aspx.cs" Inherits="FinanceWeb.project.PrInfoEdit" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>
    <script type="text/javascript">
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }
    </script>

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">搜索:
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">PR流水:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
            <td class="oddrow" style="width: 15%">PR单号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtPrNo" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Button ID="btnSearch" runat="server" Text=" 查询 " OnClick="btnSearch_Click" />&nbsp;
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="4">PR单信息<asp:HiddenField ID="hidId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">PR流水:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblPrId" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">PR单号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblPrno" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">申请人:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblRequestor" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">项目号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblProjectCode" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">供应商:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="lblSupplier" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">成本类别:
            </td>
            <td class="oddrow-l">
                <asp:DropDownList runat="server" ID="ddlType2" OnSelectedIndexChanged="ddlType2_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="4">采购审批信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">PR采购审批:
            </td>
            <td class="oddrow-l" colspan="3" style="width: 80%">
                <asp:TextBox ID="txtPrOverrule" runat="server" Width="80%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">PO采购审批:
            </td>
            <td class="oddrow-l" colspan="3" style="width: 80%">
                <asp:TextBox ID="txtPOOverrule" runat="server" Width="80%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">备注1:
            </td>
            <td class="oddrow-l" colspan="3" style="width: 80%">
                <asp:TextBox ID="txtSow" runat="server" Width="80%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">备注2:
            </td>
            <td class="oddrow-l" colspan="3" style="width: 80%">
                <asp:TextBox ID="txtContrastRemark" runat="server" Width="80%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">备注3:
            </td>
            <td class="oddrow-l" colspan="3" style="width: 80%">
                <asp:TextBox ID="txtSow3" runat="server" Width="80%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">备注4:
            </td>
            <td class="oddrow-l" colspan="3" style="width: 80%">
                <asp:TextBox ID="txtSow2" runat="server" Width="80%"></asp:TextBox>
            </td>
        </tr>
        <asp:Panel ID="panel1" runat="server" Visible="false">
            <tr>
                <td class="heading" colspan="4">物料信息
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:GridView ID="gvMaterial" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
                        OnRowDataBound="gvMaterial_RowDataBound" EmptyDataText="暂时没有相关记录" AllowPaging="false"
                        Width="100%">
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="id" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="物料项" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtItemNo" runat="server" Width="100%"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="物料描述" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDesc" runat="server" Width="100%"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="价格" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblPrice" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="三级物料" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:DropDownList runat="server" ID="ddlType3"></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="附件" HeaderStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:Label ID="labDown" runat="server" />
                                    <asp:FileUpload ID="FileUpML" runat="server" ToolTip="替换附件" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="heading" colspan="4">帐期信息
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:GridView ID="gvPeriod" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
                        OnRowDataBound="gvPeriod_RowDataBound" EmptyDataText="暂时没有相关记录" AllowPaging="false"
                        Width="100%">
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="id" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="付款日期" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPaymentDate" onkeyDown="return false; " Style="cursor: hand" onclick="setDate(this);"
                                        runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="帐期说明" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRemark" runat="server" Width="70%"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="heading" colspan="4">收货信息
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:GridView ID="gvRecipient" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
                        OnRowDataBound="gvRecipient_RowDataBound" EmptyDataText="暂时没有相关记录" AllowPaging="false"
                        Width="100%">
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="id" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="RecipientNo" HeaderText="收货单号" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="10%" />
                            <asp:BoundField DataField="RecipientName" HeaderText="收货人" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="10%" />
                            <asp:TemplateField HeaderText="备注" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNote" runat="server" Width="100%"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="备注2" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAppraiseRemark" runat="server" Width="100%"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="备注3" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDes" runat="server" Width="100%"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="heading" colspan="4">审批信息
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:GridView ID="gvAuditLog" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
                        OnRowDataBound="gvAuditLog_RowDataBound" EmptyDataText="暂时没有相关记录" AllowPaging="false"
                        Width="100%">
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="id" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="AuditUserName" HeaderText="审批人" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="10%" />
                            <asp:TemplateField HeaderText="审批日志" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="90%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtLog" runat="server" Width="90%"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="heading" colspan="4">其他审批信息
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:GridView ID="gvLog" runat="server" AutoGenerateColumns="False" DataKeyNames="logId"
                        OnRowDataBound="gvLog_RowDataBound" EmptyDataText="暂时没有相关记录" AllowPaging="false"
                        Width="100%">
                        <Columns>
                            <asp:BoundField DataField="logId" HeaderText="logId" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtLog" runat="server" Width="100%"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="heading" colspan="4">收货日志信息
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:GridView ID="gvRecipientLog" runat="server" AutoGenerateColumns="False" DataKeyNames="logId"
                        OnRowDataBound="gvRecipientLog_RowDataBound" EmptyDataText="暂时没有相关记录" AllowPaging="false"
                        Width="100%">
                        <Columns>
                            <asp:BoundField DataField="logId" HeaderText="id" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDes" runat="server" Width="100%"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="oddrow-l" colspan="4" align="center">
                    <asp:Button ID="btnPr" OnClick="btnPr_Click" Text=" 全部保存 " runat="server" />
                </td>
            </tr>
        </asp:Panel>
    </table>
</asp:Content>
