<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="Purchase_Requisition_AuditOrderPassViewList" EnableEventValidation="false"
    Title="Untitled Page" CodeBehind="AuditOrderPassViewList.aspx.cs" %>
<%@ Import Namespace="ESP.Purchase.Common"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" src="../../public/js/DatePicker.js"></script>

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>

    

    <script type="text/javascript" src="/public/js/jquery.blockUI.js"></script>

    <script type="text/javascript">
        
       
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
                                        ����
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow" style="width: 15%">
                                        ��ˮ��:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:TextBox ID="txtGlideNo" runat="server" MaxLength="200" />
                                    </td>
                                    <td class="oddrow" style="width: 15%">
                                        ���뵥��:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:TextBox ID="txtPrNo" runat="server" MaxLength="200"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow">
                                        ��Ӧ������:
                                    </td>
                                    <td class="oddrow-l">
                                        <asp:TextBox ID="txtsupplierName" runat="server" MaxLength="200" />
                                    </td>
                                    <td class="oddrow">
                                        ������:
                                    </td>
                                    <td class="oddrow-l">
                                        <asp:TextBox ID="txtAudit" runat="server" MaxLength="200" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow">
                                        �ɹ��ܽ��:
                                    </td>
                                    <td class="oddrow-l">
                                        (Min)<asp:TextBox ID="txtTotalMin" runat="server" MaxLength="200"></asp:TextBox>----
                                        <asp:TextBox ID="txtTotalMax" runat="server" MaxLength="200"></asp:TextBox>(Max)
                                    </td>
                                    <td class="oddrow">
                                        ������:
                                    </td>
                                    <td class="oddrow-l">
                                        <asp:TextBox ID="txtRequestor" MaxLength="200" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow">
                                        ���뵥����:
                                    </td>
                                    <td class="oddrow-l">
                                        <asp:DropDownList ID="ddlRequisitionflow" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="oddrow">
                                        ״̬:
                                    </td>
                                    <td class="oddrow-l">
                                        <asp:DropDownList ID="ddlState" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow">
                                        �ֹ�˾�����:
                                    </td>
                                    <td class="oddrow-l">
                                        <asp:TextBox ID="txtFiliale_AuditName" MaxLength="200" runat="server" />
                                    </td>
                                    <td class="oddrow">
                                        ����ʱ��:
                                    </td>
                                    <td class="oddrow-l">
                                        <asp:TextBox ID="txtBegin" runat="server" Width="100px" onfocus="javascript:this.blur();" />&nbsp;<img
                                            src="../../images/dynCalendar.gif" border="0" onclick="popUpCalendar(document.getElementById('ctl00_ContentPlaceHolder1_txtBegin'), document.getElementById('ctl00_ContentPlaceHolder1_txtBegin'), 'yyyy-mm-dd');" />-<asp:TextBox
                                                ID="txtEnd" onfocus="javascript:this.blur();" runat="server" Width="100px" />&nbsp;<img
                                                    src="../../images/dynCalendar.gif" border="0" onclick="popUpCalendar(document.getElementById('ctl00_ContentPlaceHolder1_txtEnd'), document.getElementById('ctl00_ContentPlaceHolder1_txtEnd'), 'yyyy-mm-dd');" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow-l" colspan="4" align="center">
                                        <asp:Button ID="btnSearch" runat="server" Text=" ���� " CssClass="widebuttons" OnClick="btnSearch_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table width="100%" id="tabTop" runat="server">
                                <tr>
                                    <td width="50%">
                                        <asp:Panel ID="PageTop" runat="server">
                                            <asp:Button ID="btnFirst2" runat="server" CssClass="widebuttons" Text="��ҳ" OnClick="btnFirst_Click" />&nbsp;
                                            <asp:Button ID="btnPrevious2" runat="server" CssClass="widebuttons" Text="��ҳ" OnClick="btnPrevious_Click" />&nbsp;
                                            <asp:Button ID="btnNext2" runat="server" CssClass="widebuttons" Text="��ҳ" OnClick="btnNext_Click" />&nbsp;
                                            <asp:Button ID="btnLast2" runat="server" CssClass="widebuttons" Text="ĩҳ" OnClick="btnLast_Click" />
                                        </asp:Panel>
                                    </td>
                                    <td align="right" class="recordTd">
                                        ��¼��:<asp:Label ID="labAllNumT" runat="server" />&nbsp;ҳ��:<asp:Label ID="labPageCountT"
                                            runat="server" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="id,moneytype"
                                PageSize="20" AllowPaging="True" Width="100%" OnRowCommand="gvG_RowCommand" OnRowDataBound="gvG_RowDataBound"
                                OnPageIndexChanging="gvG_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField DataField="id" HeaderText="id" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="glideNo" HeaderText="��ˮ��" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="6%" />
                                    <asp:TemplateField HeaderText="���뵥��" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hypNoView" Text='<%# Eval("prNo") %>' runat="server"></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="������" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="4%">
                                        <ItemTemplate>
                                            <span class="userLabel" onclick="ShowMsg('<%# ESP.Web.UI.PageBase.GetUserInfo(int.Parse(Eval("requestor").ToString())) %>');">
                                                <%# Eval("requestorname") %></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="requestor_group" HeaderText="ҵ�����" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="����ʱ��" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%">
                                        <ItemTemplate>
                                            <%# Eval("requisition_committime").ToString() == ESP.Purchase.Common.State.datetime_minvalue ? "" : DateTime.Parse(Eval("requisition_committime").ToString()).ToString()%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="project_code" HeaderText="��Ŀ��" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="11%" />
                                    <asp:TemplateField HeaderText="�ֹ�˾�����" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="4%">
                                        <ItemTemplate>
                                            <span class="userLabel" onclick="ShowMsg('<%# Eval("Filiale_Auditor").ToString() == "0" ? "" : ESP.Web.UI.PageBase.GetUserInfo(int.Parse(Eval("Filiale_Auditor").ToString())) %>');">
                                                <%# Eval("Filiale_AuditName")%></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="������" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="4%">
                                        <ItemTemplate>
                                            <span class="userLabel" onclick="ShowMsg('<%# Eval("first_assessor").ToString() == "0" ? "" : ESP.Web.UI.PageBase.GetUserInfo(int.Parse(Eval("first_assessor").ToString())) %>');">
                                                <%# Eval("first_assessorname")%></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="supplier_name" HeaderText="��Ӧ��" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="�ɹ���Ʒ" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Repeater ID="repProduct" runat="server">
                                                <ItemTemplate>
                                                    <%# Eval("Item_No").ToString() %>&nbsp;Total:<%# Eval("moneytype").ToString() %><br />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="�������" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Repeater ID="repProductType" runat="server">
                                                <ItemTemplate>
                                                    <%# Eval("producttypename").ToString() %><br />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="����ʱ��" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%">
                                        <ItemTemplate>
                                            <%# Eval("order_audittime").ToString() == ESP.Purchase.Common.State.datetime_minvalue ? "" : DateTime.Parse(Eval("order_audittime").ToString()).ToString()%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="״̬" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="labState" Text='<%#Eval("status") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="����" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="labRequisitionflow" Text='<%#Eval("requisitionflow") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="�鿴">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hypView" runat="server" ImageUrl="/images/dc.gif" title="�鿴"></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="��ӡ">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <a href='Print/RequisitionPrint.aspx?id=<%#DataBinder.Eval(Container.DataItem,"id")%>'
                                                target="_blank">
                                                <img title="��ӡ���뵥" src="../../images/Icon_PrintPr.gif" border='0' /></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="��ӡ">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Panel ID="printpo" runat="server">
                                                <a href='Print/OrderPrint.aspx?id=<%#DataBinder.Eval(Container.DataItem,"id")%>'
                                                    target="_blank">
                                                    <img title="��ӡ����" src="../../images/Icon_PrintPo.gif" border='0' /></a>
                                            </asp:Panel>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerSettings Visible="false" />
                            </asp:GridView>
                            <asp:HiddenField ID="hidSupplierEmail" runat="server" />
                            <table width="100%" id="tabBottom" runat="server">
                                <tr>
                                    <td width="50%">
                                        <asp:Panel ID="PageBottom" runat="server">
                                            <asp:Button ID="btnFirst" runat="server" CssClass="widebuttons" Text="��ҳ" OnClick="btnFirst_Click" />&nbsp;
                                            <asp:Button ID="btnPrevious" runat="server" CssClass="widebuttons" Text="��ҳ" OnClick="btnPrevious_Click" />&nbsp;
                                            <asp:Button ID="btnNext" runat="server" CssClass="widebuttons" Text="��ҳ" OnClick="btnNext_Click" />&nbsp;
                                            <asp:Button ID="btnLast" runat="server" CssClass="widebuttons" Text="ĩҳ" OnClick="btnLast_Click" />&nbsp;
                                        </asp:Panel>
                                    </td>
                                    <td align="right" class="recordTd">
                                        ��¼��:<asp:Label ID="labAllNum" runat="server" />&nbsp;ҳ��:<asp:Label ID="labPageCount"
                                            runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
