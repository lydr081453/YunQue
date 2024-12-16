<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="Purchase_TypeInfo_TypeInfoEdit" CodeBehind="TypeInfoEdit.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript">
        function EmplyeeClick(type) {
            var win = window.open('../Requisition/EmployeeList.aspx?type=' + type, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }
    </script>

    <table class="tableForm" border="0" width="100%">
        <tr>
            <td colspan="4">
                <table id="tab1" width="100%" border="0" runat="server">
                    <tr>
                        <td class="heading" colspan="4">
                            物料类别维护
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 20%">
                            物料类别名称：
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:TextBox ID="txtName" runat="server" /><font color="red"> * </font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="物料名称为必填"
                                Display="None" ControlToValidate="txtName" ValidationGroup="g1"></asp:RequiredFieldValidator>
                        </td>
                        <td class="oddrow" style="width: 20%">
                            初审人：
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:TextBox ID="txtAuditor" runat="server" Enabled="false" /><font color="red"><asp:Label
                                ID="labaudit1" runat="server"> * </asp:Label></font>
                            <asp:Button ID="btnSelect" OnClientClick="EmplyeeClick('producttype1');return false;"
                                CssClass="widebuttons" runat="server" Text="选择" />
                            <asp:HiddenField ID="hidAuditor" runat="server" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="请选择初审人"
                                Display="None" ControlToValidate="txtAuditor" ValidationGroup="g1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 20%">
                            上海分公司审核人：
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:TextBox ID="txtSHAuditor" runat="server" Enabled="false" />
                            <asp:Button ID="btnSelectSH" OnClientClick="EmplyeeClick('producttype3');return false;"
                                CssClass="widebuttons" runat="server" Text="选择" />
                            <asp:HiddenField ID="hidSHAuditor" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 20%">
                            广州分公司审核人：
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:TextBox ID="txtGZAuditor" runat="server" Enabled="false" />
                            <asp:Button ID="btnSelectGZ" OnClientClick="EmplyeeClick('producttype4');return false;"
                                CssClass="widebuttons" runat="server" Text="选择" />
                            <asp:HiddenField ID="hidGZAuditor" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="left" class="oddrow-l">
                            <asp:Button ID="btnSave" runat="server" Text=" 保存 " OnClick="btnSave_Click" ValidationGroup="g1"
                                CssClass="widebuttons" />
                        </td>
                    </tr>
                </table>
                <asp:Label ID="lab1" runat="server" Style="font-size: 14px; color: #333333; font-weight: bold;
                    padding: 0 0 0 10px;"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 20px" class="oddrow-l">
            </td>
        </tr>
        <tr>
            <td colspan="4" class="heading">
                添加子物料类别
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                物料类别名称：
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox ID="txtName1" runat="server" /><font color="red"> * </font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="物料名称为必填"
                    Display="None" ControlToValidate="txtName1" ValidationGroup="g2"></asp:RequiredFieldValidator>
            </td>
            <td class="oddrow" style="width: 20%">
                初审人：
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox ID="txtAuditor1" runat="server" Enabled="false" /><font color="red"><asp:Label
                    ID="labaudit2" runat="server"> * </asp:Label></font>
                <asp:Button ID="btnSelect1" OnClientClick="EmplyeeClick('producttype2');return false;"
                    CssClass="widebuttons" runat="server" Text="选择" />
                <asp:HiddenField ID="hidAuditor1" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="请选择初审人"
                    Display="None" ControlToValidate="txtAuditor1" ValidationGroup="g2"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                上海分公司审核人：
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox ID="txtSHAuditor1" runat="server" Enabled="false" />
                <asp:Button ID="btnSelectSH1" OnClientClick="EmplyeeClick('producttype5');return false;"
                    CssClass="widebuttons" runat="server" Text="选择" />
                <asp:HiddenField ID="hidSHAuditor1" runat="server" />
            </td>
            <td class="oddrow" style="width: 20%">
                广州分公司审核人：
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox ID="txtGZAuditor1" runat="server" Enabled="false" />
                <asp:Button ID="btnSelectGZ1" OnClientClick="EmplyeeClick('producttype6');return false;"
                    CssClass="widebuttons" runat="server" Text="选择" />
                <asp:HiddenField ID="hidGZAuditor1" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="4" class="oddrow-l">
                <asp:Button ID="btnSave1" runat="server" ValidationGroup="g2" CssClass="widebuttons"
                    Text=" 添加 " OnClick="btnSave1_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="4" class="oddrow-l" style="height: 20px">
            </td>
        </tr>
        <tr>
            <td colspan="4" class="heading">
                子物料类别列表
            </td>
        </tr>
        <tr>
            <td colspan="4" class="oddrow-l">
                <asp:GridView ID="gvItems" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                    OnRowDataBound="Items_RowDataBound" OnRowCommand="Items_RowCommand" DataKeyNames="typeid">
                    <Columns>
                        <asp:BoundField HeaderText="物料类别名称" DataField="typename" />
                        <%--<asp:BoundField HeaderText="初审人" DataField="auditorid" />--%>
                        <asp:TemplateField HeaderText="采购初审人">
                            <ItemTemplate>
                                <%# Eval("auditorid").ToString() == "" ? "" : new ESP.Compatible.Employee(int.Parse(Eval("auditorid").ToString())).Name%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="业务初审人">
                            <ItemTemplate>
                                <%# Eval("operationflow") == DBNull.Value ? "" : ESP.Purchase.Common.State.typeoperationflowAuditNames[int.Parse(Eval("operationflow").ToString())]%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <a href='TypeInfoEdit.aspx?pid=<%=Request["tid"] %>&tid=<%# Eval("typeid") %>'>
                                    <img src="../../images/edit.gif" border="0px;" title="编辑"></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:ButtonField ButtonType="image" ImageUrl="~/images/disable.gif" HeaderText="停用"
                            ControlStyle-Font-Underline="true" CommandName="Del" CausesValidation="false"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:ButtonField ButtonType="image" ImageUrl="~/images/used.gif" HeaderText="启用"
                            ControlStyle-Font-Underline="true" CommandName="Use" CausesValidation="false"
                            ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4" style="height: 20px">
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Button ID="btnBack" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnBack_Click" />
                &nbsp;<asp:Button ID="btnPre" runat="server" Text="上一步" CssClass="widebuttons" OnClick="btnPre_Click" />
            </td>
        </tr>
    </table>
    <asp:ValidationSummary runat="server" ShowMessageBox="true" ValidationGroup="g1"
        ShowSummary="false" />
    <asp:ValidationSummary runat="server" ShowMessageBox="true" ValidationGroup="g2"
        ShowSummary="false" />
</asp:Content>
