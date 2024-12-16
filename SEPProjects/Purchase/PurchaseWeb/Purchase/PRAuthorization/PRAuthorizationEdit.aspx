<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="PRAuthorizationEdit.aspx.cs" Inherits="PurchaseWeb.Purchase.PRAuthorization.PRAuthorizationEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script language="javascript" type="text/javascript">
    function EmplyeeClick() {
        var clientId = "<%= txtUser.ClientID %>".replace("txtUser", "");
        var win = window.open('/Purchase/Requisition/EmployeeList.aspx?clientId=' + clientId + '&type=prAuthorization', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');

        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);

    }

    function TypeClick() {
        var clientId = "<%= txtType.ClientID %>".replace("txtType", "");
        var win = window.open('selectType.aspx?clientId=' + clientId , null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');

        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);

    }
    function setTypes(ids, typenames) {
        document.getElementById("<%=txtType.ClientID %>").value = typenames;
        document.getElementById("<%=hidTypeId.ClientID %>").value = ids;
    }
</script>
    <table width="100%" border="0" class="tableForm">
        <tr>
            <td class="heading" colspan="2">
                特殊物料人员权限
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                <font color="red"> * </font>业务人员：
            </td>
            <td class="oddrow">
                <asp:TextBox ID="txtUser" runat="server" onkeypress="return false;" /><asp:RequiredFieldValidator ControlToValidate="txtUser" Display="None"
                    ID="RequiredFieldValidator1" runat="server" ErrorMessage="请选择业务人员"></asp:RequiredFieldValidator><asp:HiddenField ID="hidUser"
                    runat="server" />&nbsp;<asp:Button ID="btnSelect" runat="server" Text=" 选择 " CssClass="widebuttons" OnClientClick="EmplyeeClick();return false;" CausesValidation="false" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                <font color="red"> * </font>第三方物料：
            </td>
            <td class="oddrow">
                <asp:TextBox ID="txtType" runat="server" onkeypress="return false;" /><asp:RequiredFieldValidator ControlToValidate="txtType" Display="None"
                    ID="RequiredFieldValidator2" runat="server" ErrorMessage="请选择第三方物料"></asp:RequiredFieldValidator><asp:HiddenField ID="hidTypeId"
                    runat="server" />&nbsp;<asp:Button ID="btnTypeSelect" runat="server" Text=" 选择 " CssClass="widebuttons" OnClientClick="TypeClick();return false;" CausesValidation="false" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                备注：
            </td>
            <td class="oddrow">
                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="70%" Height="80px" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="widebuttons" />
                <asp:Button ID="btnClose" runat="server" Text="关闭" OnClientClick="return confirm('您确定关闭吗？');" Visible="false" OnClick="btnClose_Click" CssClass="widebuttons" />
                &nbsp;<asp:Button ID="btnBack" OnClick="btnBack_Click" CssClass="widebuttons" Text="返回" CausesValidation="false"
                    runat="server" />
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ShowMessageBox="true" ShowSummary="false"
                    ID="ValidationSummary1" runat="server" />
    <br />
    <asp:Panel ID="palDetail" runat="server" Visible="false">
        <table width="100%" border="0" class="tableForm">
            <tr>
                <td class="heading" colspan="4">
                    详细信息
                </td>
            </tr>
            <tr>
                <td class="oddrow">
                    创建人：
                </td>
                <td class="oddrow-l">
                    <asp:Literal ID="litCreateUser" runat="server" />
                </td>
                <td class="oddrow">
                    创建时间：
                </td>
                <td class="oddrow-l">
                    <asp:Literal ID="litCreateTime" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="oddrow">
                    状态：
                </td>
                <td class="oddrow-l">
                    <asp:Literal ID="litStatus" runat="server" />
                </td>
                <td class="oddrow">
                    关闭时间：
                </td>
                <td class="oddrow-l">
                    <asp:Literal ID="litCloseTime" runat="server" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="glideNo" HeaderText="流水号" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="申请人" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# Eval("requestorname") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="申请时间" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                    <ItemTemplate>
                        <%# Eval("app_date").ToString() == ESP.Purchase.Common.State.datetime_minvalue ? "" : DateTime.Parse(Eval("app_date").ToString()).ToString()%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="project_code" HeaderText="项目号" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="project_descripttion" HeaderText="项目名称" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="15%" />
                <asp:BoundField DataField="supplier_name" HeaderText="供应商" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="15%" />
                <asp:BoundField DataField="itemno" HeaderText="采购物品" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="15%" />
                <asp:TemplateField HeaderText="采购总金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="6%">
                    <ItemTemplate>
                        <%# Eval("moneytype").ToString() == "美元" ? "＄" + decimal.Parse(Eval("ototalprice").ToString()).ToString("#,##0.####") : "￥" + decimal.Parse(Eval("ototalprice").ToString()).ToString("#,##0.####")%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
</asp:Content>
