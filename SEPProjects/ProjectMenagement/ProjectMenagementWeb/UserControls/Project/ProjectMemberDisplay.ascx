<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_Project_ProjectMemberDisplay"
    CodeBehind="ProjectMemberDisplay.ascx.cs" %>


<table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="4">
            项目组成员
        </td>
    </tr>
    <tr>
        <td class="oddrow" colspan="4">
            <asp:UpdatePanel ID="updatepanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="gvMember" runat="server" AutoGenerateColumns="False" DataKeyNames="MemberID"
                        OnRowDataBound="gvMember_RowDataBound" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="MemberID" HeaderText="MemberID" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="MemberUserID" HeaderText="系统ID" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="序号" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="成员姓名" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" CssClass="userLabel" Text='<%# Eval("MemberEmployeeName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="MemberCode" HeaderText="成员编号" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="15%" />
                            <asp:BoundField DataField="MemberUserName" HeaderText="成员帐号" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="10%" />
                            <asp:BoundField DataField="GroupID" HeaderText="组ID" ItemStyle-HorizontalAlign="Center"
                                Visible="false" />
                            <asp:TemplateField HeaderText="业务组别" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblGroupName" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="MemberEmail" HeaderText="邮箱" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="15%" />
                            <asp:TemplateField HeaderText="电话" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("MemberPhone") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="RoleName" HeaderText="职务" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="12%" />
                        </Columns>
                        <PagerSettings Visible="false" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
