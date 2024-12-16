<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="AdvertisementProductsReference.aspx.cs" Inherits="PurchaseWeb.Purchase.Requisition.Advertisement.AdvertisementProductsReference" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

</script>
       <style type="text/css">                  
           .cssPager span { font-size:14px;}             
           </style>
<style type="text/css">
        a{color:black;text-decoration:none ; }
        .ffff{text-decoration:none ; color:#000000 ;}
    </style>

    <table style="width: 100%">
        <tr>
            <td colspan="2" style="padding: 4px;">
                <table style="width: 100%;">
                    <tr>
                        <td colspan="2">
                            <table width="100%" class="tableForm">
                                <tr>
                                    <td class="heading" colspan="4">
                                        检索
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow" style="width: 15%">
                                        媒体名称:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:TextBox ID="txtName" runat="server" MaxLength="300" />
                                    </td>
                                    <td class="oddrow" style="width: 15%">
                                        媒体类型:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:DropDownList ID="dllType" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow" style="height: 10px">
                                        &nbsp;</td>
                                    <td class="oddrow-l" style="height: 10px">
                                        &nbsp;</td>
                                    <td class="oddrow" style="height: 10px">
                                        &nbsp;</td>
                                    <td class="oddrow-l" style="height: 10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow-l" colspan="4" align="center">
                                        <asp:Button ID="btnSearch" runat="server" Text=" 检  索 " CssClass="widebuttons" OnClick="btnSearch_Click" />
                                        <asp:Button ID="btnSave" runat="server" Text=" 保  存 " CssClass="widebuttons" OnClick="btnSave_Click" />
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
                            <table width="100%" id="tabTop" runat="server" style="display:none">
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
                            <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="id" PagerSettings-Mode="NumericFirstLast" PagerSettings-Visible="true"
                                PageSize="10" AllowPaging="True" Width="100%" OnRowCommand="gvG_RowCommand" OnRowDataBound="gvG_RowDataBound" PagerSettings-Position="TopAndBottom"
                                OnPageIndexChanging="gvG_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="名称" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMediaName" runat="server" Text='<%# Eval("MediaName")%>'></asp:Label><asp:HiddenField ID="hidID" runat="server" Value='<%# Eval("ID") %>' /><%--<asp:HiddenField ID="hidMediaID" runat="server" Value='<%# Eval("AdvertisementID") %>' />--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="媒体类型" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMediaType" runat="server" Text='<%# Eval("MediaTypeID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText=" 报  价 " ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                        <asp:LinkButton ID="lkShow" runat="server" CommandName="Show" Text="显示/隐藏" ToolTip="查看详细内容" ></asp:LinkButton>
                                            <%--<asp:ImageButton ID="imgbtnDetails" runat="server" ImageUrl="~/images/edit.gif" ToolTip="详细内容" CommandName="Show" />--%><asp:HiddenField ID="hidIsShow" runat="server" Value="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    
                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70%">
                                        <ItemTemplate>
                                        
                                            <asp:GridView ID="gvDetails" runat="server" Visible="false" Width="100%" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ckSel" runat="server" /><asp:HiddenField ID="hidDetailID" runat="server" Value='<%# Eval("ID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="折扣" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <%# Eval("Discount")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="折扣备注" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="14%">
                                                        <ItemTemplate>
                                                            <%# Eval("DiscountDescription")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="刊例投放额度" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                                        <ItemTemplate>
                                                            <%# Eval("DistributionMin", "{0:#,##0.00}")%> <= T < <%# Eval("DistributionMax", "{0:#,##0.00}")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="配送百分比" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <%# Eval("DistributionPercent")%>%
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="配送备注" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <%# Eval("DistributionDescription")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="返点" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <%# Eval("ReturnPoint")%>%
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="账期" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            单月<%# Eval("AccountPeriod")%>天
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns><PagerStyle CssClass="cssPager" />
                            </asp:GridView>
                            <asp:HiddenField ID="hidSupplierEmail" runat="server" />
                            <table width="100%" id="tabBottom" runat="server" style="display:none">
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
            </td>
        </tr>
    </table>
</asp:Content>