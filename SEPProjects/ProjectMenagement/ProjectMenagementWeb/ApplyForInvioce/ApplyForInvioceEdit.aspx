<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ApplyForInvioceEdit.aspx.cs" Inherits="FinanceWeb.ApplyForInvioce.ApplyForInvioceEdit" %>

<%@ Register Src="/UserControls/Project/PrepareDisplay.ascx" TagName="Prepare" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/Project/ProjectInfoView.ascx" TagPrefix="uc1" TagName="ProjectInfoView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript">
        function check() {
            if ($("[type='checkbox']:checked").length == 0) {
                alert('请选择提交的发票申请！');
                return false;
            }
        }
        $(document).ready(function () {
            $(window).scrollTop($(document).height());
        });
    </script>
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
    <table width="100%">
        <tr>
            <td>
                <uc1:Prepare ID="PrepareDisplay" runat="server" />
            </td>
        </tr>
                <tr>
            <td>
                <uc1:ProjectInfoView runat="server" ID="ProjectInfoView" />
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="heading">发票申请信息&nbsp;<asp:HyperLink ID="hyNew" ForeColor="Red" runat="server" Text="[新增]" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvContract" runat="server" AutoGenerateColumns="false" AllowPaging="false" OnRowCommand="gvContract_RowCommand">
                                <Columns>
                                   <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center">
                                       <ItemTemplate>
                                           <input type="checkbox" name="chkId" id="chkId" <%# Eval("Status") != null && (int)Eval("Status") == (int)ESP.Finance.Utility.ApplyForInvioceStatus.Status.Wait_Submit?"checked='checked'" :"disabled='disabled'" %>" value="<%# Eval("Id") %>" />
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                    <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("Status") == null ? "" : ESP.Finance.Utility.ApplyForInvioceStatus.Status_Names[int.Parse( Eval("Status").ToString())] %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="审核人" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("Status") == null ? "" : AuditorName %>
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
                                    </asp:TemplateField>--%>
<%--                                    <asp:BoundField HeaderText="发票金额" ItemStyle-HorizontalAlign="Center" DataField="InviocePrice" DataFormatString="{0:#,##0.00}" />--%>
                                    <asp:BoundField HeaderText="申请时间" ItemStyle-HorizontalAlign="Center" DataField="CreateDate" DataFormatString="{0:yyyy-MM-dd}" />
                                    <asp:BoundField HeaderText="描述" ItemStyle-HorizontalAlign="Center" DataField="Remark" />
                                    <asp:BoundField HeaderText="申请人" ItemStyle-HorizontalAlign="Center" DataField="CreatorUserName" />
                                    <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete"  Visible='<%# Eval("Status") != null && ((int)Eval("Status") == (int)ESP.Finance.Utility.ApplyForInvioceStatus.Status.Wait_Submit) ? true : false %>' runat="server" CommandArgument='<%# Eval("Id") %>'
                                                            CommandName="Del" Text="<img src='../../images/disable.gif' title='删除' border='0'>"
                                                            OnClientClick="return confirm('你确定删除吗？');" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>

                    <tr>
                        <td><asp:Button ID="btnSubmit" runat="server" OnClientClick="return check();" Text="提交" CssClass="widebuttons" OnClick="btnSubmit_Click"  />
                            &nbsp;<asp:Button ID="btnBack" runat="server" Text="返回" CssClass="widebuttons" OnClick="btnBack_Click" />
                        </td>
                    </tr>
                                        <tr>
                        <td class="oddrow" width="15%">审批历史:<asp:Label ID="lblLog" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
