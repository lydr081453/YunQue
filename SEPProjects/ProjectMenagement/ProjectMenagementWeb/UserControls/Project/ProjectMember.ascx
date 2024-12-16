<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="UserControls_Project_ProjectMember" Codebehind="ProjectMember.ascx.cs" %>

<script type="text/javascript">
    function selectMember() {
        var backurl = window.location.pathname;
        var operate = '<%=Request[ESP.Finance.Utility.RequestName.Operate] %>';
        var win = window.open('/Dialogs/EmployeeList.aspx?<% =ESP.Finance.Utility.RequestName.ProjectID %>=<%=Request[ESP.Finance.Utility.RequestName.ProjectID] %>&<% =ESP.Finance.Utility.RequestName.SearchType %>=Member&<% =ESP.Finance.Utility.RequestName.DeptID %>=<%=ProjectInfo.GroupID %>&<% =ESP.Finance.Utility.RequestName.BackUrl %>=' + backurl + '&<% =ESP.Finance.Utility.RequestName.Operate %>=' + operate, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }
</script>

<table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="4">
          项目成员信息&nbsp;&nbsp;<%--<img align="absbottom" id="imgMember" src="/images/a3_07.jpg" onclick="return selectMember();"  style=" cursor:hand" alt="添加项目成员"/>--%><asp:Button ID="btnAddMember" runat="server" OnClientClick="return selectMember();" Text=" 添加 " CssClass="widebuttons"></asp:Button>
            <asp:LinkButton runat='server' ID="btnMember" OnClick="btnMember_Click" /><a name="top_A" />
        </td>
    </tr>
    <input type="hidden" id="hidMembers" runat="server" />
    <tr>
        <td class="oddrow" colspan="4">
            <asp:GridView ID="gvMember" runat="server" AutoGenerateColumns="False" DataKeyNames="MemberID"
                OnRowCommand="gvMember_RowCommand" OnRowDataBound="gvMember_RowDataBound" Width="100%">
                <Columns>
                    <asp:BoundField DataField="MemberID" HeaderText="MemberID" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MemberUserID" HeaderText="系统ID" ItemStyle-HorizontalAlign="Center" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="序号" ItemStyle-Width="5%">
                         <ItemTemplate>
                           <asp:Label ID="lblNo" runat="server"></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="MemberEmployeeName" HeaderText="成员姓名" ItemStyle-HorizontalAlign="Center"
                       ItemStyle-Width="10%"  />
                    <asp:BoundField DataField="MemberCode" HeaderText="成员编号" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="8%" />
                    <asp:BoundField DataField="MemberUserName" HeaderText="成员帐号" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="13%" />
                    <asp:BoundField DataField="GroupID" HeaderText="组ID" ItemStyle-HorizontalAlign="Center"
                        visible="false"/>
                    <asp:BoundField DataField="GroupName" HeaderText="业务组" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="10%" />
                    <asp:BoundField DataField="MemberEmail" HeaderText="邮箱" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="12%" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="电话" ItemStyle-Width="15%">
                         <ItemTemplate>
                           <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("MemberPhone") %>'></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("MemberID") %>'
                                CommandName="Del" Text="<img src='/images/disable.gif' title='删除' border='0'>"
                                OnClientClick="return confirm('你确定删除吗？');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>
