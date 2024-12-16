<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserForm.aspx.cs" Inherits="SEPAdmin.UserManagement.UserForm" MasterPageFile="~/MainMaster.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">
<table>
<tr colspan="4">
<td class="heading">用户信息</td>
</tr>
<tr>
<td class="oddrow"  style="width: 10%">员工号:</td>
<td class="oddrow-l" style="width: 20%"><asp:Label ID="labUserId" runat="server" /></td>
<td class="oddrow" style="width: 10%">员工姓名</td>
<td class="oddrow-l" style="width: 20%"><asp:Label ID="labName" runat="server"></asp:Label></td>
</tr>
<tr>
<td class="oddrow" style="width: 10%">公司Email:</td>
<td class="oddrow-l" style="width: 20%"><asp:Label ID="labEmail" runat="server"/></td>   
<td class="oddrow"  style="width: 10%">登录名:</td>
<td class="oddrow-l"  style="width: 20%"><asp:Label ID="labItCode" runat="server" /></td>
</tr>
<tr colspan="4">
<td class="heading">负责人设置</td>
</tr>
<tr>
<td colspan="4">
<asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="UserID,IsManager,IsActing,DepartmentID,DepartmentPositionID" OnRowDataBound="gvList_RowDataBound" Width="100%" >
         <Columns>            
            <asp:BoundField DataField="DepartmentName" HeaderText="部门" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="DepartmentPositionName" HeaderText="职位"  ItemStyle-HorizontalAlign="Center"/>            
            <asp:TemplateField HeaderText="负责人">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="代理负责人">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox2" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>            
        </Columns>
</asp:GridView>            
</td>
</tr>
<tr>
<td><asp:Button ID="btnCommit" runat="server" Text=" 提交 " CausesValidation="false" CssClass="widebuttons" OnClick="btnCommit_Click" /></td>
<td><asp:Button ID="btnBack" runat="server" Text=" 返回 " CssClass="widebuttons" CausesValidation="false" OnClick="btnBack_Click" /></td>
</tr>
</table>
</asp:Content>
