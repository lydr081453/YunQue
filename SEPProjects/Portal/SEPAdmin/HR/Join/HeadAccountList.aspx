<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HeadAccountList.aspx.cs"
    MasterPageFile="~/MasterPage.master" Inherits="SEPAdmin.HR.Join.HeadAccountList" %>

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
               <asp:ListItem Text="未占用" Value="99"></asp:ListItem>
               <asp:ListItem Text="审核中" Value="1,2,9"></asp:ListItem>
               <asp:ListItem Text="审核通过" Value="3"></asp:ListItem>
               <asp:ListItem Text="面谈完毕" Value="4,5,6"></asp:ListItem>
               <asp:ListItem Text="已经入职" Value="1"></asp:ListItem>
               <asp:ListItem Text="驳回" Value="0"></asp:ListItem>
               <asp:ListItem Text="Reject" Value="11"></asp:ListItem>
               </asp:DropDownList>&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click">
                </asp:Button>
                <asp:Button ID="btnCreate" runat="server" Text=" 创建 " CssClass="widebuttons" OnClick="btnCreate_Click">
                </asp:Button>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvHeadAccount" runat="server" AutoGenerateColumns="False" OnRowDeleting="gvHeadAccount_RowDeleting" OnRowDataBound="gvHeadAccount_RowDataBound" OnRowCommand="gvHeadAccount_RowCommand"
                    Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="流水号" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblId"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="部门" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblGroup"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Position" HeaderText="职务" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="LevelName" HeaderText="职级" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%"/>
                       <%-- <asp:TemplateField HeaderText="工资级别" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblSalary"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                         <asp:BoundField DataField="Creator" HeaderText="创建人" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="CreateDate" HeaderText="创建日期" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}"
                            ItemStyle-Width="10%" />
                           <%--  <asp:TemplateField HeaderText="替换员工" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblReplace"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                          <asp:TemplateField HeaderText="应聘者" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblOffer"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                            <asp:Label runat="server" ID="lblStatus"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="审核人" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblAuditor"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                              <a href="HeadCountView.aspx?haid=<%#Eval("Id").ToString() %>" target="_blank">查看</a>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                              <a href="HeadAccountCreate.aspx?hcid=<%#Eval("Id").ToString() %>">编辑</a>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="约见" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                               <asp:Label runat="server" ID="lblInterview"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="转组" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                               <asp:Label runat="server" ID="lblTransfer"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="约见表" ItemStyle-HorizontalAlign="Center">
                             <ItemTemplate>
                             <asp:Label runat="server" ID="lblPrint"></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                           <asp:TemplateField HeaderText="重约" ItemStyle-HorizontalAlign="Center">
                             <ItemTemplate>
                              <asp:ImageButton ID="btnInterview" Visible="false" CommandName="View" runat="server" ImageUrl="../../images/edit.gif" OnClientClick="return confirm('您是否要撤销上一次约见记录，重新约见？');" 
                                                CommandArgument='<%# Eval("Id") %>' />
                             </ItemTemplate>
                         </asp:TemplateField>
                           <asp:TemplateField HeaderText="Offer Email" ItemStyle-HorizontalAlign="Center">
                             <ItemTemplate>
                              <asp:ImageButton ID="btnMail" Visible="false" CommandName="Mail" runat="server" ImageUrl="../../images/dc.gif" OnClientClick="return confirm('您是否要发送offer letter？');" 
                                                CommandArgument='<%# Eval("Id") %>' />
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center">
                             <ItemTemplate>
                              <asp:ImageButton ID="btnDelete" Visible="false" CommandName="Delete" runat="server" ImageUrl="../../images/cancel.gif" OnClientClick="return confirm('您是否要删除该条headcount申请？');" 
                                                CommandArgument='<%# Eval("Id") %>' />
                             </ItemTemplate>
                         </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
