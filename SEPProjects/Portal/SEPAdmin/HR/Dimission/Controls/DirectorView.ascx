<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DirectorView.ascx.cs"
    Inherits="SEPAdmin.HR.Dimission.Controls.DirectorView" %>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td colspan="3">
            &nbsp;
        </td>
        <td align="right" class="recordTd">
            记录数:<asp:Label ID="labManNumberT" runat="server" />
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:GridView ID="gvDetailView" runat="server" AutoGenerateColumns="False" DataKeyNames="FormId"
                Width="100%" OnRowDataBound="gvDetailView_RowDataBound">
                <Columns>
                    <%--<asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="ckb" runat="server" />
                            </ItemTemplate>
                            <HeaderTemplate>
                                <input id="CheckAll" type="checkbox" onclick="selectAll(this);" />全选
                            </HeaderTemplate>
                        </asp:TemplateField>--%>
                    <asp:BoundField DataField="FormId" Visible="false" />
                    <asp:BoundField DataField="FormCode" HeaderText="单据编号" />
                    <asp:BoundField DataField="FormType" HeaderText="单据类型" />
                    <asp:BoundField DataField="ProjectCode" HeaderText="项目号" />
                    <asp:BoundField DataField="Description" HeaderText="项目号描述" />
                    <asp:BoundField DataField="TotalPrice" HeaderText="总金额" />
                    <asp:TemplateField HeaderText="交接人编号" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="labReceiverId" runat="server" />
                            <asp:HiddenField ID="hidReceiverId" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="交接人">
                        <ItemTemplate>
                            <asp:Label ID="labReceiverName" runat="server" />
                            <asp:HiddenField ID="hidReceiverName" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="交接人部门编号" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="labReceiverDepartmentId" runat="server" />
                            <asp:HiddenField ID="hidReceiverDepartmentId" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="交接状态">
                        <ItemTemplate>
                            <asp:Label ID="labReceiverStatus" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="设置交接人" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <img alt="设置交接人" src="../../images/edit.gif" runat="server" id="imgSetReceiver" onclick="SetReceiver(this);"
                                    style="cursor: pointer;" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                </Columns>
                <PagerSettings Visible="false" />
            </asp:GridView>
            <input type="hidden" id="Hidden1" value="" runat="server" />
            <asp:Label ID="Label2" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            &nbsp;
        </td>
        <td align="right" class="recordTd">
            记录数:<asp:Label ID="labManNumberB" runat="server" />
        </td>
    </tr>
</table>
