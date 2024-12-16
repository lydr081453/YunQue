<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="Purchase_Requisition_HandConfirmRecipient" CodeBehind="HandConfirmRecipient.aspx.cs" %>

<%@ Import Namespace="ESP.Purchase.Common" %>
<%@ Register Namespace="MyControls" TagPrefix="cc2" Assembly="MyControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" src="../../public/js/DatePicker.js"></script>

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>

    

    <script type="text/javascript" src="/public/js/jquery.blockUI.js"></script>

        <script type="text/javascript">

            function confirmEmail(mail) {
                var newMail = prompt("供应商邮箱为：" + mail + "，您确定发送此邮件吗？如要更改邮件信息，请于下边输入新的邮件。", mail)
                if (newMail != null && newMail != "") {
                    document.getElementById('<%= hidSupplierEmail.ClientID %>').value = newMail;
                return 2;
            } else if (newMail == null) {
                return 0;
            } else {
                return 1;
            }
        }

        function confirmEamil1(mail) {
            var newMail = prompt("供应商邮箱为：" + mail + "，已给供应商发送过邮件，确定再次发送吗？如要更改邮件信息，请于下边输入新的邮件。", mail)
            if (newMail != null && newMail != "") {
                document.getElementById('<%= hidSupplierEmail.ClientID %>').value = newMail;
                return 2;
            } else if (newMail == null) {
                return 0;
            } else {
                return 1;
            }
        }

    </script>

    <asp:HiddenField ID="hidSupplierEmail" runat="server" />
    <table width="100%" class="tableForm">
        <tr>
            <td colspan="4" class="heading">
                检索
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                流水号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtGlideNo" MaxLength="200" runat="server" />
            </td>
            <td class="oddrow" style="width: 15%">
                订单号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtPrNo" MaxLength="200" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                收货单号:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtRNo" runat="server" MaxLength="200" />
            </td>
            <td class="oddrow">
                协议供应商名称:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtsupplier_name" runat="server" Width="200px" MaxLength="200" />&nbsp;<asp:Button
                    ID="btn" runat="server" OnClientClick="SupplierClick();return false;" Text="请选择..."
                    CssClass="widebuttons" Visible="false" />&nbsp;<br />
                <asp:HiddenField ID="hidsupplierType" runat="server" />
            </td>
        </tr>
        <tr>
             <td class="oddrow">
                收货人:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtReceiver" runat="server" MaxLength="200" />
            </td>
            <td colspan="2" class="oddrow-l">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading">
                已收货待确认
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" id="tabTop" runat="server">
                    <tr>
                        <td width="50%">
                            <asp:Panel ID="PageTop" runat="server">
                                <asp:Button ID="btnFirst2" runat="server" CssClass="widebuttons" Text="首页" OnClick="btnFirst_Click" />&nbsp;
                                <asp:Button ID="btnPrevious2" runat="server" CssClass="widebuttons" Text="上页" OnClick="btnPrevious_Click" />&nbsp;
                                <asp:Button ID="btnNext2" runat="server" CssClass="widebuttons" Text="下页" OnClick="btnNext_Click" />&nbsp;
                                <asp:Button ID="btnLast2" runat="server" CssClass="widebuttons" Text="末页" OnClick="btnLast_Click" />
                            </asp:Panel>
                        </td>
                        <td align="right" class="recordTd">
                            记录数:<asp:Label ID="labAllNumT" runat="server" />&nbsp;页数:<asp:Label ID="labPageCountT"
                                runat="server" />
                        </td>
                    </tr>
                </table>
                <br />
                <asp:GridView ID="gvSupplier" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvSupplier_RowDataBound" OnPageIndexChanging="gvSupplier_PageIndexChanging"
                    DataKeyNames="recipientId" PageSize="20" Width="100%" OnRowCommand="gvSupplier_RowCommand" AllowPaging="true"
                    >
                    <Columns>
                        <asp:TemplateField HeaderText="确认">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                 <asp:HyperLink runat="server" ID="hpConfirm" ImageUrl="/images/Icon_handconfrim.gif"></asp:HyperLink>
                          
                                <%--<asp:LinkButton ID="lnkHankConfirm" runat="server" CommandArgument='<%# Eval("recipientId") %>'
                                    Font-Underline="true" 
                                    Text="<img title='手动确认' src='../../images/Icon_handconfrim.gif' border='0' />"
                                    CommandName="HandConfirm" />--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="供应商">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkHankConfirm2" runat="server" CommandArgument='<%# Eval("recipientId") %>'
                                    Font-Underline="true" OnClientClick="return confirm('请确认已收到供应商书面确认作为支持文件？');"
                                    Text="<img title='供应商确认' src='../../images/Icon_handconfrim.gif' border='0' />"
                                    CommandName="HandConfirm2" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="邮件">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkSend" runat="server" Font-Underline="true" Text="<img title='发送邮件' src='../../images/Icon_Sendmail.gif' border='0' />"
                                    CommandName="SendMail" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="撤销" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnCancel" runat="server" Text="<img title='撤销申请单' src='../../images/Icon_Cancel.gif' border='0' />"
                                    OnClientClick="return confirm('您是否确认撤销？');" CausesValidation="false" CommandArgument='<%# Eval("recipientId") %>'
                                    CommandName="Return" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="打印">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <a href="Print/MultiRecipientPrint.aspx?newPrint=true&id=<%# Eval("recipientId") %>"
                                    target="_blank">
                                    <img src="../../images/pri_gr.gif" /></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="查看">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <a href="OrderDetailTab.aspx?GeneralID=<%# Eval("gid") %>"
                                    target="_blank">
                                    <img src="../../images/pri_gr.gif" /></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="流水号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <%# int.Parse(Eval("gid").ToString()).ToString("0000000") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="orderid" HeaderText="订单号" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="7%" />
                        <asp:BoundField DataField="RecipientNo" HeaderText="收货单号" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="7%" />
                        <asp:TemplateField HeaderText="申请人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="4%">
                            <ItemTemplate>
                                <span class="userLabel" onclick="ShowMsg('<%# ESP.Web.UI.PageBase.GetUserInfo(int.Parse(Eval("requestor").ToString())) %>');">
                                    <%# Eval("requestorname") %></span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="申请时间" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <%# Eval("requisition_committime").ToString() == ESP.Purchase.Common.State.datetime_minvalue ? "" : DateTime.Parse(Eval("requisition_committime").ToString()).ToString()%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="project_code" HeaderText="项目号" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="8%" />
                        <asp:TemplateField HeaderText="收货金额" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <%# Eval("moneytype").ToString() == "美元" ? "＄" + decimal.Parse(Eval("RecipientAmount").ToString()).ToString("#,##0.##") : "￥" + decimal.Parse(Eval("RecipientAmount").ToString()).ToString("#,##0.##")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="收货人" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="labRev" runat="server" CssClass="userLabel" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="附加收货人" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="labAppend" runat="server" CssClass="userLabel" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="确认状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%">
                            <ItemTemplate>
                                <asp:Label ID="labConfirm" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="supplier_name" HeaderText="供应商" ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                    <PagerSettings Visible="false" />
                </asp:GridView>
                <table width="100%" id="tabBottom" runat="server">
                    <tr>
                        <td width="50%">
                            <asp:Panel ID="PageBottom" runat="server">
                                <asp:Button ID="btnFirst" runat="server" CssClass="widebuttons" Text="首页" OnClick="btnFirst_Click" />&nbsp;
                                <asp:Button ID="btnPrevious" runat="server" CssClass="widebuttons" Text="上页" OnClick="btnPrevious_Click" />&nbsp;
                                <asp:Button ID="btnNext" runat="server" CssClass="widebuttons" Text="下页" OnClick="btnNext_Click" />&nbsp;
                                <asp:Button ID="btnLast" runat="server" CssClass="widebuttons" Text="末页" OnClick="btnLast_Click" />&nbsp;
                            </asp:Panel>
                        </td>
                        <td align="right" class="recordTd">
                            记录数:<asp:Label ID="labAllNum" runat="server" />&nbsp;页数:<asp:Label ID="labPageCount"
                                runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
