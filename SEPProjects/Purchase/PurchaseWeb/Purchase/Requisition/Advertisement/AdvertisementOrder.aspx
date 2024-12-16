<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="AdvertisementOrder.aspx.cs" Inherits="PurchaseWeb.Purchase.Requisition.Advertisement.AdvertisementOrder" %>

<%@ Import Namespace="ESP.Purchase.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ScriptManager ID="S1" runat="server" />
    <script language="javascript" src="../../../public/js/DatePicker.js"></script>
    <script language="javascript">
        function MoreMediaClick() {
            var orderid = document.getElementById("<%=hidOrderID.ClientID %>");
            var supplierId = document.getElementById("<%=hidSupplierId.ClientID %>");
            var win = window.open('AdvertisementProductsReference.aspx?type=backup&SupplierId='+supplierId.value+'&orderID=' + orderid.value + '&GeneralID=<%=Request[RequestName.GeneralID] %>', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }
    </script><div id="ad" runat="server"></div>
    <table style="width: 100%">
        <tr>
            <td colspan="2" style="padding: 4px;">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <table width="100%" class="tableForm">
                                <tr>
                                    <td class="heading" colspan="4">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="oddrow" style="width: 15%"><asp:HiddenField ID="hidOrderID" runat="server" /><asp:HiddenField ID="hidSupplierId" runat="server" />
                                        项目编号:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:Label ID="lblProNO" runat="server"></asp:Label>
                                    </td>
                                    <td class="oddrow" style="width: 15%">
                                        客户名称:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:Label ID="lblCusName" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow" style="width: 15%"><asp:HiddenField ID="HiddenField1" runat="server" />
                                        投放日期:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:TextBox ID="txtStartDate" runat="server" onclick="popUpCalendar(this, this, 'yyyy-mm-dd');"></asp:TextBox>
                                    </td>
                                    <td class="oddrow" style="width: 15%">
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow-l" colspan="4" align="center">
                                        <asp:Button ID="btnSearch" runat="server" Text="添加媒体明细" CssClass="widebuttons" OnClientClick="MoreMediaClick();return false;" />&nbsp;<asp:LinkButton ID="btnRefsh" runat="server" OnClick="btnRefsh_Click"></asp:LinkButton>
                                        <asp:Button ID="btnSave" runat="server" Text="  保  存  " ValidationGroup="Save" CssClass="widebuttons" OnClick="btnSave_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
                            <table width="100%"><tr><td width="100%">
                            <asp:Repeater ID="rptItems" runat="server" onitemcommand="rptItems_ItemCommand" 
                                onitemdatabound="rptItems_ItemDataBound">
                                <HeaderTemplate>
                                    <table width="100%" cellpadding="1" border="1">
                                        <tr>
                                            <td width="20%" height="25px" class="heading" style="text-align:center">媒体名称</td>
                                            <td width="8%" height="25px" class="heading" style="text-align:center">媒体类型</td>
                                            <td width="12%" height="25px" class="heading" style="text-align:center">广告形式</td>
                                            <td width="10%" height="25px" class="heading" style="text-align:center">刊例总价</td>
                                            <td width="10%" height="25px" class="heading" style="text-align:center">折扣(Off)</td>
                                            <td width="10%" height="25px" class="heading" style="text-align:center">投放净价</td>
                                            <td width="5%" height="25px" class="heading" style="text-align:center">更多</td>
                                            <td width="5%" height="25px" class="heading" style="text-align:center">删除</td>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td colspan="8">
                                            <table width="100%">
                                                <tr>
                                                    <td width="20%" align="center">
                                                        <asp:Label ID="lblMediaName" runat="server" Width="100%" Text='<%# Eval("MediaName") %>'></asp:Label>
                                                        <asp:HiddenField ID="hidID" runat="server" Value='<%# Eval("ID") %>' />
                                                        <asp:HiddenField ID="hidAdvertisementDetailsID" runat="server" Value='<%# Eval("AdvertisementDetailsID") %>' />
                                                        <asp:HiddenField ID="hidAdvertisementID" runat="server" Value='<%# Eval("AdvertisementID") %>' />
                                                    </td>
                                                    <td width="8%" align="center">
                                                        <asp:Label ID="lblType" runat="server" Width="100%" Text='<%# Eval("MediaType") %>'></asp:Label>
                                                    </td>
                                                    <td width="12%" align="center">
                                                        <asp:TextBox ID="txtExemplar" style="text-align:right" runat="server" Width="100%" Text='<%# Eval("AdvertisementExemplar") %>'></asp:TextBox>
                                                    </td>
                                                    <td width="10%" align="center">
                                                        <asp:TextBox ID="txtPriceTotal" style="text-align:right" runat="server" Width="80%" AutoPostBack="true" 
                                                        OnTextChanged="txtPriceTotal_TextChanged" Text='<%# Eval("PriceTotal","{0:#,##0.00}") %>'></asp:TextBox><asp:RangeValidator ID="RangeValidator1" ControlToValidate="txtPriceTotal" ErrorMessage="数字" ValidationGroup="Save" Type="Double"  runat="server"></asp:RangeValidator>
                                                    </td>
                                                    <td width="10%" align="center">
                                                        <asp:TextBox ID="txtDiscount" style="text-align:right" runat="server" Width="50%" AutoPostBack="true" OnTextChanged="txtDiscount_TextChanged"></asp:TextBox> % Off<asp:RangeValidator ID="RangeValidator2" ControlToValidate="txtDiscount" ErrorMessage="数字" ValidationGroup="Save" Type="Double"  runat="server"></asp:RangeValidator>
                                                        <br /><asp:Label ID="lblDiscount" runat="server" ForeColor="Red" Text="" Visible="false"></asp:Label>
                                                    </td>
                                                    <td width="10%" align="center">
                                                        <asp:Label ID="txtTotal" Width="100%" style="text-align:right" runat="server" Text='<%# Eval("Total","{0:#,##0.00}") %>'></asp:Label>
                                                    </td>
                                                    <td width="5%" align="center">
                                                        <asp:ImageButton ID="imgbtnDetails" runat="server" ImageUrl="~/images/edit.gif" ToolTip="更多" CommandName="Show" /><asp:LinkButton ID="linkMore" runat="server" CommandName="Show"></asp:LinkButton><asp:HiddenField ID="hidIsShow" runat="server" Value="false" />
                                                    </td>
                                                    <td width="5%" align="center">
                                                        <asp:ImageButton ID="imgbtnDelete" runat="server" ImageUrl="~/images/disable.gif" ToolTip="删除" CommandName="deleteItem" CommandArgument='<%# Eval("ID") %>' /><asp:HiddenField ID="hidIndexID" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                                    <div id="divDetails" visible="false" runat="server">
                                                        <table width="100%" class="tableForm">
                                                            <tr>
                                                                <td class="oddrow">配送比例</td>
                                                                <td class="oddrow-l"><asp:TextBox ID="txtDistributionPercent" runat="server" style="text-align:right" AutoPostBack="true" OnTextChanged="txtDistributionPercent_TextChanged" Text='<%# Eval("DistributionPercent") %>'></asp:TextBox> %<asp:RangeValidator ID="RangeValidator3" ControlToValidate="txtDistributionPercent" ErrorMessage="数字" ValidationGroup="Save" Type="Double"  runat="server"></asp:RangeValidator><br /><asp:Label ID="lblDistributionPercent" runat="server" ForeColor="Red" Visible="false" Text=""></asp:Label></td>
                                                                <td class="oddrow">配送金额</td>
                                                                <td class="oddrow-l"><asp:Label ID="txtDistributionPrice" runat="server" style="text-align:right" Text='<%# Eval("DistributionPrice","{0:#,##0.00}") %>'></asp:Label></td>
                                                                <td class="oddrow">返点</td>
                                                                <td class="oddrow-l"><asp:TextBox ID="txtReturnPoint" runat="server" style="text-align:right" AutoPostBack="true" OnTextChanged="txtReturnPoint_TextChanged" Text='<%# Eval("ReturnPoint") %>'></asp:TextBox> %<asp:RangeValidator ID="RangeValidator4" ControlToValidate="txtReturnPoint" ErrorMessage="数字" ValidationGroup="Save" Type="Double"  runat="server"></asp:RangeValidator><br /><asp:Label ID="lblReturnPoint" runat="server" ForeColor="Red" Visible="false" Text=""></asp:Label></td>
                                                                <td class="oddrow">账期</td>
                                                                <td class="oddrow-l">单月<asp:TextBox ID="txtAccountPeriod" Width="50px" runat="server" Text='<%# Eval("AccountPeriod") %>' AutoPostBack="true" OnTextChanged="txtAccountPeriod_TextChanged"></asp:TextBox>天<br /><asp:Label ID="lblAccountPeriod" runat="server" ForeColor="Red" Visible="false" Text=""></asp:Label></td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                            </td></tr></table></ContentTemplate></asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>