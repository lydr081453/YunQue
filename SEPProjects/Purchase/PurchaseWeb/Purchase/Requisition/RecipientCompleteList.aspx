<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Purchase_Requisition_RecipientCompleteList" Codebehind="RecipientCompleteList.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script language="javascript" src="../../public/js/DatePicker.js"></script>
    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>
    <script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>
    
    <%--
    <table style="width: 100%">
        <tr>
            <td colspan="2" style="padding: 4px;">
                <table style="width: 100%;">
                    <tr>
                        <td colspan="2">
                            <table width="100%" class="tableForm">
                                <tr>
                                    <td class="heading" colspan="4">检索</td>
                                </tr>
                                <tr>
                                    <td class="oddrow" style="width:15%">流水号:</td>
                                    <td class="oddrow-l" style="width:35%"><asp:TextBox ID="txtGlideNo" runat="server" /></td>
                                    <td class="oddrow" style="width:15%">申请单号:</td>
                                    <td class="oddrow-l" style="width:35%"><asp:TextBox ID="txtPrNo" runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="oddrow">订单编号:</td>
                                    <td class="oddrow-l"><asp:TextBox ID="txtOrderNo" runat="server" /></td>
                                    <td class="oddrow">初审人:</td>
                                    <td class="oddrow-l"><asp:TextBox ID="txtAudit" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="oddrow-l" colspan="4" align="center"><asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table width="100%" id="tabTop" runat="server">
                            <tr><td width="50%">
                            <asp:Panel ID="PageTop" runat="server">
                            <asp:Button ID="btnFirst2" runat="server" CssClass="widebuttons" Text="首页" OnClick="btnFirst_Click" />&nbsp;
                            <asp:Button ID="btnPrevious2" runat="server" CssClass="widebuttons" Text="上页" OnClick="btnPrevious_Click" />&nbsp;
                            <asp:Button ID="btnNext2" runat="server" CssClass="widebuttons" Text="下页" OnClick="btnNext_Click" />&nbsp;
                            <asp:Button ID="btnLast2" runat="server" CssClass="widebuttons" Text="末页" OnClick="btnLast_Click" />
                            </asp:Panel>
                            </td>
                            <td align="right" class="recordTd">记录数:<asp:Label ID="labAllNumT" runat="server" />&nbsp;页数:<asp:Label ID="labPageCountT" runat="server" /></td>
                            </tr>
                            </table><br />
                            <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="id,moneytype" 
                                PageSize="20" AllowPaging="True" Width="100%"  OnRowCommand="gvG_RowCommand" OnRowDataBound="gvG_RowDataBound" OnPageIndexChanging="gvG_PageIndexChanging">
                                
                                <Columns>
                                    <asp:BoundField DataField="id" HeaderText="id" ItemStyle-HorizontalAlign="Center"/>
									<asp:TemplateField HeaderText="收货" ItemStyle-Width="3%">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <a href="RecipientDetail.aspx?backUrl=RecipientList.aspx&<% =RequestName.GeneralID %>=<%#Eval("id") %>" title="收货"><img src="../../images/dc.gif" border="0px;"></a>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:BoundField DataField="glideNo" HeaderText="流水号" ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="申请单号" DataField="prNo" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center"/>
                                    <asp:TemplateField HeaderText="订单号"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
                                        <ItemTemplate>
                                            <a href="RecipientDetail.aspx?backUrl=RecipientList.aspx&<% =RequestName.GeneralID %>=<%#Eval("id") %>"><%#Eval("orderid") %></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="requestorname" HeaderText="申请人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%"/>
                                    <asp:TemplateField HeaderText="申请时间"  ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("requisition_committime").ToString() == Common.State.datetime_minvalue ? "" : DateTime.Parse(Eval("requisition_committime").ToString()).ToString()%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="requestor_group" HeaderText="业务组别" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%"/>
                                    <asp:BoundField DataField="project_code" HeaderText="项目号" ItemStyle-Width="13%" ItemStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField HeaderText="分公司审核人" DataField="Filiale_AuditName" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" />
                                    <asp:BoundField DataField="first_assessorname" HeaderText="初审人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%"/>
                                    <asp:TemplateField HeaderText="订单审批时间"  ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("order_audittime").ToString() == Common.State.datetime_minvalue ? "" : DateTime.Parse(Eval("order_audittime").ToString()).ToString("yyyy-MM-dd")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="采购物品" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Repeater ID="repProduct" runat="server">
                                                <ItemTemplate>
                                                    <%# Eval("Item_No").ToString() %>&nbsp;Total:<%# Eval("moneytype").ToString() %><br />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ItemTemplate>
                                    </asp:TemplateField>
									<asp:TemplateField HeaderText="打印">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<a href='Print/RequisitionPrint.aspx?id=<%#DataBinder.Eval(Container.DataItem,"id")%>' target="_blank"><img title="打印申请单" src="../../images/Icon_PrintPr.gif" /></a>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="打印">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<a href='Print/OrderPrint.aspx?id=<%#DataBinder.Eval(Container.DataItem,"id")%>' target="_blank"><img title="打印订单" src="../../images/Icon_PrintPo.gif" /></a>
										</ItemTemplate>
									</asp:TemplateField>
                                </Columns>
                                
                                <PagerSettings Visible="false"/>
                            </asp:GridView>
                            <table width="100%" id="tabBottom" runat="server">
                            <tr><td width="50%">
                            <asp:Panel ID="PageBottom" runat="server">
                            <asp:Button ID="btnFirst" runat="server" CssClass="widebuttons" Text="首页" OnClick="btnFirst_Click" />&nbsp;
                            <asp:Button ID="btnPrevious" runat="server" CssClass="widebuttons" Text="上页" OnClick="btnPrevious_Click" />&nbsp;
                            <asp:Button ID="btnNext" runat="server" CssClass="widebuttons" Text="下页" OnClick="btnNext_Click" />&nbsp;
                            <asp:Button ID="btnLast" runat="server" CssClass="widebuttons" Text="末页" OnClick="btnLast_Click" />&nbsp;
                            </asp:Panel>
                            </td>
                            <td align="right" class="recordTd">记录数:<asp:Label ID="labAllNum" runat="server" />&nbsp;页数:<asp:Label ID="labPageCount" runat="server" /></td>
                            </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>--%>
</asp:Content>