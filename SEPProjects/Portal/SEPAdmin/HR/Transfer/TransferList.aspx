<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransferList.aspx.cs" Inherits="SEPAdmin.HR.Transfer.TransferList" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <table width="100%" border="0" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                检索
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">
                关键字:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>&nbsp;
            </td>
            <td class="oddrow" style="width: 10%">
                申请状态:
            </td>
            <td class="oddrow-l">
               <asp:DropDownList runat="server" ID="ddlStatus">
               <asp:ListItem Selected="True" Text="全部" Value="-1"></asp:ListItem>
               <asp:ListItem Text="审核中" Value="1"></asp:ListItem>
               <asp:ListItem Text="审核通过" Value="3"></asp:ListItem>
               <asp:ListItem Text="驳回" Value="2"></asp:ListItem>
               </asp:DropDownList>&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click">
                </asp:Button>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvTransfer" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvTransfer_RowDataBound" OnRowCommand="gvTransfer_RowCommand"
                    Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="流水号" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblId"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:BoundField DataField="TransName" HeaderText="姓名" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="TransCode" HeaderText="编号" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="OldGroupName" HeaderText="转出部门" ItemStyle-HorizontalAlign="Center" />
                         <asp:BoundField DataField="NewGroupName" HeaderText="转入部门" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="NewPositionName" HeaderText="新职务" ItemStyle-HorizontalAlign="Center" />
                         <asp:BoundField DataField="TransInDate" HeaderText="转入日期" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}"
                            ItemStyle-Width="10%" />
                         <asp:BoundField DataField="Creater" HeaderText="创建人" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="CreateDate" HeaderText="创建日期" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}"
                            ItemStyle-Width="10%" />
                        <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                            <asp:Label runat="server" ID="lblStatus"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                              <a href="TransferView.aspx?id=<%#Eval("Id").ToString() %>" target="_blank"><img src="/images/dc.gif" /></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                              <a href="TransferEdit.aspx?id=<%#Eval("Id").ToString() %>"><img src="/images/edit.gif" /></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center">
                             <ItemTemplate>
                              <asp:ImageButton ID="btnDelete" CommandName="del" runat="server" ImageUrl="/images/cancel.gif" OnClientClick="return confirm('您是否要删除该条Transfer申请？');" 
                                                CommandArgument='<%# Eval("Id") %>' />
                             </ItemTemplate>
                         </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>