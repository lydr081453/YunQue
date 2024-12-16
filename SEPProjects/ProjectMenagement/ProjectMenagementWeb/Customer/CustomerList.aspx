<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" Inherits="Customer_CustomerList" Codebehind="CustomerList.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <script language="javascript" src="../../public/js/DatePicker.js"></script>

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script type="text/javascript" src="/publicjs/jquery.jcarousellite.min.js"></script>

    <script type="text/javascript" src="/publicjs/script_jcarousellite.js"></script>

    <script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>

    <script src="/public/js/dimensions.js" type="text/javascript"></script>
    
    <li><a href="CustomerInfoEdit.aspx">添加新客户</a></li>
    <br /><br />
    <table style="width: 100%">
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="heading" colspan="4">检索</td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 20%">
                            客户缩写:
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:TextBox ID="txtShortNameEN" runat="server" />
                        </td>
                                                <td class="oddrow" style="width: 20%">
                            客户名称:
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:TextBox ID="txtCustomerNameCN" runat="server" />
                        </td>
                    </tr>
                    <tr>

                    </tr>
                    <tr>
                        <td class="oddrow-l" colspan="4" align="center"><asp:Button ID="btnSearch" runat="server" Text=" 检索 " OnClick="btnSearch_Click" CssClass="widebuttons" />
                        &nbsp;<asp:Button ID="btnSearchAll" runat="server" Text="重新检索" OnClick="btnSearchAll_Click" CssClass="widebuttons" /></td>
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
                            客户列表
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow">
                            <asp:GridView ID="gvCustomers" runat="server"  AutoGenerateColumns="False" DataKeyNames="CustomerID,CustomerCode" 
                                PageSize="10" 
                                EmptyDataText="暂时没有相关记录" OnPageIndexChanging="gvCustomers_PageIndexChanging" AllowPaging="true" Width="100%" >
                                <Columns>
                                    <asp:BoundField DataField="CustomerID" HeaderText="id" ItemStyle-HorizontalAlign="Center" Visible="false"/>
                                    <asp:BoundField DataField="CustomerCode" HeaderText="客户代码"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" />
                                    <asp:TemplateField HeaderText="缩写" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNameEN" runat="server" Text='<%# Eval("ShortEN")%>' ToolTip='<%# Eval("ShortEN")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="客户名称" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNameCN" runat="server" Text='<%# Eval("FullNameCN")%>' ToolTip='<%# Eval("FullNameCN")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="所属行业" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIndustryName" runat="server" Text='<%# Eval("FullIndustryName")%>' ToolTip='<%# Eval("IndustryName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="所在地区" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAreaName" Text='<%# Eval("FullAreaName")%>' ToolTip='<%# Eval("AreaName")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     
                                    <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
                                        <ItemTemplate>
                                            <a href='CustomerInfoView.aspx?<% =ESP.Finance.Utility.RequestName.CustomerID %>=<%# Eval("CustomerID")%>'><img src="../images/dc.gif" border="0px;" title="查看"></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
									<asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<a href='CustomerInfoEdit.aspx?<% =ESP.Finance.Utility.RequestName.CustomerID %>=<%# Eval("CustomerID")%>'><img src="../images/edit.gif" border="0px;" title="编辑"></a>
										</ItemTemplate>
									</asp:TemplateField>
                                </Columns>
                            </asp:GridView>
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