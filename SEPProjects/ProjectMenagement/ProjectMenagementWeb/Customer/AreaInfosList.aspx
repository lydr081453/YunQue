<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" Inherits="Customer_AreaInfosList" Codebehind="AreaInfosList.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

   <script language="javascript" src="../../public/js/DatePicker.js"></script>

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script type="text/javascript" src="/publicjs/jquery.jcarousellite.min.js"></script>

    <script type="text/javascript" src="/publicjs/script_jcarousellite.js"></script>

    <script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>

    <script src="/public/js/dimensions.js" type="text/javascript"></script>
    
    <li><a href="AreaInfoEdit.aspx">添加地区</a></li>
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
                            地区代码:
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:TextBox ID="txtAreaCode" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 20%">
                            地区名称:
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:TextBox ID="txtAreaName" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow">
                            查询码:
                        </td>
                        <td class="oddrow-l" colspan="3">
                            <asp:TextBox ID="txtSearchCode" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow-l" colspan="4" align="center"><asp:Button ID="btnSearch" runat="server" Text=" 检索 " OnClick="btnSearch_Click" CssClass="widebuttons" /></td>
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
                            地区列表
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow">
                            
                            <asp:GridView ID="gvArea" runat="server"  AutoGenerateColumns="False" DataKeyNames="AreaID,AreaCode" Width="100%" >
                                
                                <Columns>
                                    <asp:BoundField DataField="AreaID" HeaderText="id" ItemStyle-HorizontalAlign="Center" Visible="false"/>
                                    <asp:BoundField DataField="AreaCode" HeaderText="地区代码"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="AreaName" HeaderText="地区名称" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%" />
                                    <asp:BoundField DataField="SearchCode" HeaderText="查询码"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" />
                                     <asp:BoundField DataField="Description" HeaderText="英文描述"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30%"/>
                                     <asp:BoundField DataField="Others" HeaderText="其它"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%" />
                                     
                                    <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
                                        <ItemTemplate>
                                            <a href='AreaInfoView.aspx?AreaID=<%# Eval("AreaID")%>'><img src="../images/dc.gif" border="0px;" title="查看"></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
									<asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<a href='AreaInfoEdit.aspx?AreaID=<%# Eval("AreaID")%>'><img src="../images/edit.gif" border="0px;" title="编辑"></a>
										</ItemTemplate>
									</asp:TemplateField>
                                </Columns>
                                
                                <PagerSettings Visible="false"/>
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