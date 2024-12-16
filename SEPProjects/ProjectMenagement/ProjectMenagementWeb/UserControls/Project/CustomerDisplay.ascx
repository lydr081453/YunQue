<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="UserControls_Project_CustomerDisplay" CodeBehind="CustomerDisplay.ascx.cs" %>
<script type="text/javascript">
    function showCustomer() {
        var cid = document.getElementById("<%=hidCustomerID.ClientID %>").value;
        var win = window.open('/Customer/CustomerDiffer.aspx?<% =ESP.Finance.Utility.RequestName.CustomerID %>=' + cid, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }
</script>
<table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="4">②客户信息
            <input type="hidden" runat="server" id="hidCustomerID" />
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">英文简称:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label ID="lblShortEN" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">客户名称:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="lblNameCN1" runat="server"></asp:Label>
        </td>
        <td class="oddrow">发票抬头:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="lblInvoiceTitle" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">客户地址:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label ID="lblAddress1" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">公司网址:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label ID="lblWebSite" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">所在地区:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="lblArea" runat="server"></asp:Label>
        </td>
        <td class="oddrow" style="width: 15%">所在行业:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="lblIndustry" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="heading" colspan="4">联系人信息
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">客户联系人:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="lblContact" runat="server"></asp:Label>
        </td>
        <td class="oddrow" style="width: 15%">联系人职务:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="lblContactPosition" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">联系人电话:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="lblContactMobile" runat="server"></asp:Label>
        </td>
        <td class="oddrow" style="width: 15%">联系人传真:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="lblContactFax" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">联系人Email:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label ID="lblEmail" runat="server" Width="40%"></asp:Label>
        </td>
    </tr>
    <tr runat="server" id="TrProxyHeader">
        <td class="heading" colspan="4">框架协议信息
        </td>
    </tr>
    <tr runat="server" id="TrProxyBody">
        <td class="oddrow" colspan="4">
            <asp:GridView ID="gvProxy" runat="server" AutoGenerateColumns="False" DataKeyNames="AttachID"
                OnRowDataBound="gvProxy_RowDataBound" Width="100%">
                <Columns>
                    <asp:TemplateField HeaderText="选择" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <input type="checkbox" onclick="setHid(this);" disabled name="radAttach" <%# projectInfo.CustomerAttachID.Contains("," +Eval("AttachID").ToString()+",") ? "checked" : "" %> value="<%# Eval("AttachID") %>" />
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="FrameContractTitle" HeaderText="协议描述" ItemStyle-HorizontalAlign="Center"
                         />
                    <asp:BoundField DataField="FrameBeginDate" HeaderText="起始日期" ItemStyle-HorizontalAlign="Center"
                         />
                    <asp:BoundField DataField="FrameEndDate" HeaderText="结束日期" ItemStyle-HorizontalAlign="Center"
                         />
                                                        <asp:TemplateField HeaderText="备注" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <span title="<%# Eval("Description") %>"><%# Eval("Description").ToString().Length<=100 ? Eval("Description").ToString() : Eval("Description").ToString().Substring(0,100)+"..." %></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="附件" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="labDown" runat="server" Text='' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerSettings Visible="false" />
            </asp:GridView>
        </td>
    </tr>
</table>
<div id="divCustomer" runat="server">
    <table width="100%">
        <tr>
            <td class="oddrow-1" colspan="4">
                <a href="#" onclick="return showCustomer();">
                    <img src="/images/differ.jpg" alt="客户信息变更" width="30px" height="25px" /><font color="red">客户信息有变动，请点这里查看比对数据</font></a>
            </td>
        </tr>
    </table>
</div>
