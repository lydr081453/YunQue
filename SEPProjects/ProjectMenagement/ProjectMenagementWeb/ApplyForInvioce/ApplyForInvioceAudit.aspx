<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ApplyForInvioceAudit.aspx.cs" Inherits="FinanceWeb.ApplyForInvioce.ApplyForInvioceAudit" %>

<%@ Register Src="/UserControls/Project/PrepareDisplay.ascx" TagName="Prepare" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <style>
        .abc {
            width:100%;
            margin-left:5px;
        }
            .abc td {
                border: 0px;
                height:18px;
                padding:0px;
                text-align:left;
            }
    </style>
    <script>
        function checkSel() {
            if ($("[type='checkbox']:checked").length == 0) {
                alert('请选择审批的发票申请！');
                return false;
            }
        }
    </script>
    <table width="100%">
        <tr>
            <td>
                <uc1:Prepare ID="PrepareDisplay" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" class="tableForm">
                    <tr>
                        <td colspan="2" class="heading">发票申请信息</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="false" AllowPaging="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center">
                                       <ItemTemplate>
                                           <input type="checkbox" name="chkId" id="chkId" value="<%# Eval("Id") %>" <%# Eval("Status") != null && (int)Eval("Status") == (int)ESP.Finance.Utility.ApplyForInvioceStatus.Status.Auditing?"checked='checked'" :"disabled='disabled'" %>" />
                                       </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="流向" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("FlowTo") == null ? "客户" : ESP.Finance.Utility.Common.ApplyForInvioceFlowTo_Names[ (int)Eval("FlowTo")] %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="发票信息" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                                <table class="abc">
                                                <tr><td width="85px;">发票类型:</td><td><%# Eval("InvoiceType") == null ? "普票" : ESP.Finance.Utility.Common.InvoiceType_Names[ (int)Eval("InvoiceType")] %></td></tr>
                                                <tr><td>开票金额:</td><td><%# decimal.Parse(Eval("InviocePrice").ToString()).ToString("#,##0.00") %></td></tr>
                                                <tr><td>单位名称:</td><td><%# Eval("InvoiceTitle") %></td></tr>
                                                <tr><td>开户银行:</td><td><%# Eval("BankName") %></td></tr>
                                                <tr><td>账号:</td><td><%# Eval("BankNum") %></td></tr>
                                                <tr><td>纳税人识别号:</td><td><%# Eval("TIN") %></td></tr>
                                                <tr><td>开户地址及电话:</td><td><%# Eval("AddressPhone") %></td></tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
<%--                                    <asp:TemplateField HeaderText="媒体付款主体" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("SupplierId") == null ? "" : Eval("MediaName") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="发票金额" ItemStyle-HorizontalAlign="Center" DataField="InviocePrice" DataFormatString="{0:#,##0.00}" />--%>
                                    <asp:BoundField HeaderText="描述" ItemStyle-HorizontalAlign="Center" DataField="Remark" />
                                    <asp:BoundField HeaderText="申请人" ItemStyle-HorizontalAlign="Center" DataField="CreatorUserName" />
                                    <asp:BoundField HeaderText="申请时间" ItemStyle-HorizontalAlign="Center" DataField="CreateDate" DataFormatString="{0:yyyy-MM-dd}" />
                                    <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("Status") == null ? "" : ESP.Finance.Utility.ApplyForInvioceStatus.Status_Names[int.Parse( Eval("Status").ToString())] %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
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
                        <td class="oddrow-l" colspan="4">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" width="15%">审批批示:
                        </td>
                        <td class="oddrow-l" colspan="3">
                            <asp:TextBox ID="txtAuditRemark" runat="server" TextMode="MultiLine" Height="80px"
                                Width="70%"></asp:TextBox>
                            <asp:LinkButton runat="server" ID="btnTip" OnClick="btnTip_Click" CausesValidation="false"
                                ToolTip="暂时留个Message,以后再操作"><font style=" text-decoration:underline; color:Blue; font-weight:bold">留言</font></asp:LinkButton><font
                                    color="red">*</font>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button ID="btnOk" runat="server" Text="审批通过" OnClientClick="return checkSel();" CssClass="widebuttons" OnClick="btnOk_Click" />
                            &nbsp;<asp:Button ID="btnNo" runat="server" Text="审批驳回" OnClientClick="return checkSel();" CssClass="widebuttons" OnClick="btnNo_Click" />
                            &nbsp;<asp:Button ID="btnBack" runat="server" Text="返回" CssClass="widebuttons" OnClick="btnBack_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
