<%@ Page Language="C#" AutoEventWireup="true" Inherits="PRMedia_PRMediaOrderList"
    MasterPageFile="~/MasterPage.master" EnableEventValidation="false" CodeBehind="PRMediaOrderList.aspx.cs" %>

<%@ Register Namespace="MyControls" TagPrefix="cc2" Assembly="MyControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%@ register src="../UserControls/View/PRTopMessage.ascx" tagname="PRTopMessage"
        tagprefix="uc1" %>

    <script language="javascript" src="../../public/js/DatePicker.js"></script>

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script type="text/javascript" src="/public/js/jquery.jcarousellite.min.js"></script>

    <script type="text/javascript" src="/public/js/script_jcarousellite.js"></script>

    <script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>

    

    <uc1:PRTopMessage ID="PRTopMessage" runat="server" IsEditPage="true" />
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                付款申请
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                3000以上
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <cc2:NewGridView ID="gvMediaOrder" runat="server" AllowSorting="true" AutoGenerateColumns="False"
                    OnRowDataBound="gvMediaOrder_RowDataBound" DataKeyNames="id" PageSize="20" AllowPaging="False"
                    Width="100%" PagerSettings-Position="Bottom">
                    <Columns>
                        <asp:TemplateField HeaderText="流水号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                            <ItemTemplate>
                                <%# int.Parse(Eval("id").ToString()).ToString("0000000") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="申请单号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                            <ItemTemplate>
                                <%# Eval("prno") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="收款人" DataField="receiverName" />
                        <asp:BoundField HeaderText="身份证" DataField="CardNumber" />
                        <asp:TemplateField HeaderText="金额">
                            <ItemTemplate>
                                <%# decimal.Parse(Eval("totalAmount").ToString()).ToString("#,##0.00") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </cc2:NewGridView>
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                3000以下
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <cc2:NewGridView ID="NewGridView1" runat="server" AllowSorting="true" AutoGenerateColumns="False"
                    OnRowDataBound="gvMediaOrder_RowDataBound" DataKeyNames="id" PageSize="20" AllowPaging="False"
                    Width="100%" PagerSettings-Position="Bottom">
                    <Columns>
                        <asp:TemplateField HeaderText="流水号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                            <ItemTemplate>
                                <%# int.Parse(Eval("id").ToString()).ToString("0000000") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="申请单号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                            <ItemTemplate>
                                <%# Eval("prno") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="收款人" DataField="receiverName" />
                        <asp:BoundField HeaderText="身份证" DataField="CardNumber" />
                        <asp:TemplateField HeaderText="金额">
                            <ItemTemplate>
                                <%# decimal.Parse(Eval("totalAmount").ToString()).ToString("#,##0.00") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </cc2:NewGridView>
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                需要税单的PR申请
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <cc2:NewGridView ID="NewGridView2" runat="server" AllowSorting="true" AutoGenerateColumns="False"
                    OnRowDataBound="gvMediaOrder_RowDataBound" DataKeyNames="id" PageSize="20" AllowPaging="False"
                    Width="100%" PagerSettings-Position="Bottom">
                    <Columns>
                        <asp:TemplateField HeaderText="流水号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                            <ItemTemplate>
                                <%# int.Parse(Eval("id").ToString()).ToString("0000000") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="申请单号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                            <ItemTemplate>
                                <%# Eval("prno") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="收款人" DataField="receiverName" />
                        <asp:BoundField HeaderText="身份证" DataField="CardNumber" />
                        <asp:TemplateField HeaderText="金额">
                            <ItemTemplate>
                                <%# decimal.Parse(Eval("totalAmount").ToString()).ToString("#,##0.00") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </cc2:NewGridView>
            </td>
        </tr>
    </table>
    <br />
    <prc:CheckPRInputButton type="button" id="btnCreate" runat="server" value=" 创建 " class="widebuttons"
        onclick="if(confirm('您确定要创建吗？')){ this.disabled=true;}else{return false;}" onserverclick="btnCreate_Click" />
    &nbsp;<asp:Button ID="btnBack" runat="server" Text=" 返回 " CssClass="widebuttons"
        OnClick="btnBack_Click" />
    <%--    <asp:HiddenField ID="hidPeriodIds" runat="server" />
    <input type="button" value="创建付款申请" id="btnSubmit" runat="server" class="widebuttons"
                    onclick="if(!submitCheck()){return false;} else { if(!confirm('你确定要创建付款申请吗？')) return false;}" onserverclick="btnSubmit_Click" />
    <input type="button" value="创建采购申请" id="btnSumit1" runat="server" class="widebuttons"
                    onclick="if(!submitCheck()){return false;} else { if(!confirm('你确定要创建采购申请吗？')) return false;}" onserverclick="btnSubmit1_Click" />
    <asp:HiddenField ID="hidRecipientIds" runat="server" />--%>
</asp:Content>
