<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="AdvertisementEdit.aspx.cs" Inherits="PurchaseWeb.Purchase.Requisition.Advertisement.AdvertisementEdit" %>

<%@ Import Namespace="ESP.Purchase.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../../../public/js/DatePicker.js"></script>
    <script language="javascript" type="text/javascript">
        function NewMediaDetailClick() {
            var win = window.open('AdvertisementDetailAddOrEdit.aspx?AdvertisementID=<%=Request["AdvertisementID"] %>', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }
        
        function EditMediaDetailClick(id) {
            var win = window.open('AdvertisementDetailAddOrEdit.aspx?AdvertisementID=<%=Request["AdvertisementID"] %>&DetailID=' + id, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }

        function openProductTypes() {
            var win = window.open('EditProducts.aspx', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }
    </script>
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
                                    <td class="oddrow" style="width: 15%"><asp:HiddenField ID="hidOrderID" runat="server" />
                                        所属物料:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:TextBox ID="txtThirdParty" runat="server"></asp:TextBox><asp:HiddenField ID="hidtypeIds" runat="server" />
                                        <asp:RequiredFieldValidator
                ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtThirdParty"
                Display="None" ErrorMessage="所属物料为必填"></asp:RequiredFieldValidator><font color="red">
                    * </font><input type="button" value="请选择..." id="btnSel" runat="server" class="widebuttons" onclick="openProductTypes();return false;" />
                                    </td>
                                    <td class="oddrow" style="width: 15%">
                                        媒体名称:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:TextBox ID="txtMediaName" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow" style="width: 15%"><asp:HiddenField ID="HiddenField1" runat="server" />
                                        广告媒体类型:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:DropDownList ID="dllType" runat="server"></asp:DropDownList>
                                    </td>
                                    <td class="oddrow" style="width: 15%">
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow-l" colspan="4" align="center">
                                        <asp:Button ID="btnSave" runat="server" Text="  保  存  " CssClass="widebuttons" OnClick="btnSave_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="添加媒体明细" CssClass="widebuttons" OnClientClick="NewMediaDetailClick();return false;" />&nbsp;<asp:LinkButton ID="btnRefsh" runat="server" OnClick="btnRefsh_Click"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Repeater ID="rptItems" runat="server" onitemcommand="rptItems_ItemCommand" 
                                onitemdatabound="rptItems_ItemDataBound">
                                <HeaderTemplate>
                                    <table width="100%" cellpadding="1" border="1">
                                        <tr>
                                            <td width="11%" height="25px" class="heading" style="text-align:center">折扣</td>
                                            <td width="20%" height="25px" class="heading" style="text-align:center">备注</td>
                                            <td width="11%" height="25px" class="heading" style="text-align:center">配送下限</td>
                                            <td width="11%" height="25px" class="heading" style="text-align:center">配送上限</td>
                                            <td width="11%" height="25px" class="heading" style="text-align:center">配送额</td>
                                            <td width="11%" height="25px" class="heading" style="text-align:center">返点</td>
                                            <td width="15%" height="25px" class="heading" style="text-align:center">账期</td>
                                            <td width="5%" height="25px" class="heading" style="text-align:center">编辑</td>
                                            <td width="5%" height="25px" class="heading" style="text-align:center">删除</td>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td colspan="13">
                                            <table width="100%">
                                                <tr>
                                                    <td width="11%" align="center">
                                                        <asp:Label ID="lblDiscount" runat="server" Width="100%" Text='<%# Eval("Discount") %>'></asp:Label>
                                                        <asp:HiddenField ID="hidID" runat="server" Value='<%# Eval("ID") %>' />
                                                    </td>
                                                    <td width="20%" align="center">
                                                        <asp:Label ID="txtDiscountDescription" runat="server" Width="100%" Text='<%# Eval("DiscountDescription") %>'></asp:Label>
                                                    </td>
                                                    <td width="11%" align="center">
                                                        <asp:Label ID="txtUnit" runat="server" Width="100%" Text='<%# Eval("DistributionMin") %>'></asp:Label>
                                                    </td>
                                                    <td width="11%" align="center">
                                                        <asp:Label ID="txtManuscriptType" runat="server" Width="100%" Text='<%# Eval("DistributionMax") %>'></asp:Label>                                            
                                                    </td>
                                                    <td width="11%" align="center">
                                                        <asp:Label ID="txtDesc" runat="server" Width="100%"  Text='<%# Eval("DistributionPercent") %>' ></asp:Label>
                                                    </td>
                                                    <td width="11%" align="center">
                                                        <asp:Label ID="txtLayout" Width="100%" runat="server" Text='<%# Eval("ReturnPoint") %>'></asp:Label>
                                                    </td>
                                                    <td width="15%" align="center">
                                                        <asp:Label ID="txtHopePrice" runat="server" Width="100%" Text='<%# Eval("AccountPeriod") %>'></asp:Label>
                                                    </td>
                                                    <td width="5%" align="center">
                                                        <asp:ImageButton ID="imgbtnDetails" runat="server" ImageUrl="~/images/edit.gif" ToolTip="编辑" OnClientClick='<%# "EditMediaDetailClick(" + Eval("ID") +")" %>' CommandName="Show" /><asp:HiddenField ID="hidIsShow" runat="server" Value="false" />
                                                    </td>
                                                    <td width="5%" align="center">
                                                        <asp:ImageButton ID="imgbtnDelete" runat="server" ImageUrl="~/images/disable.gif" ToolTip="删除" CommandName="deleteItem" CommandArgument='<%# Eval("ID") %>' /><asp:HiddenField ID="hidIndexID" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>