<%@ Page Title="项目号列表" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Purchase_Requisition_selectProjectList" Codebehind="selectProjectList.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="4">检索</td>
    </tr>
    <tr>
        <td class="oddrow" style="width:15%">PA号:</td>
        <td class="oddrow-l" style="width:35%"><asp:TextBox ID="txtPA" runat="server" /></td>
        <td class="oddrow" style="width:15%">项目号:</td>
        <td class="oddrow-l" style="width:35%"><asp:TextBox ID="txtProjectCode" runat="server" /></td>
    </tr>
    <tr>
        <td class="oddrow-l" colspan="4" ><asp:Button ID="btnSelect" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSelect_Click" /></td>
    </tr>
</table>
    <asp:GridView ID="gvProject" runat="server" AutoGenerateColumns="false" DataKeyNames="ProjectId" OnRowDataBound="gvProject_RowDataBound" OnRowCommand="gvProject_RowCommand" Width="100%" CellPadding="4">
        
        <Columns>
            <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                <ItemTemplate>
                    <asp:Button ID="btnAdd" runat="server" Text="选择" CssClass="widebuttons" CommandName="Add" CommandArgument='<%# Eval("projectId").ToString() == "0" ? Eval("projectCode") : Eval("projectId") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="serialCode" HeaderText="PA号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" />
            <asp:BoundField DataField="projectCode" HeaderText="项目号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="12%" />
            <asp:BoundField DataField="SubmitDate" HeaderText="项目提交时间" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="12%" />
            <asp:BoundField DataField="BusinessDescription" HeaderText="项目名称" />
            <asp:TemplateField HeaderText="成本所属组" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="12%">
                <ItemTemplate>
                    <asp:Literal ID="litGroup" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <br />
    <input type="button" value=" 关闭 " class="widebuttons" onclick="window.close();" />
</asp:Content>

