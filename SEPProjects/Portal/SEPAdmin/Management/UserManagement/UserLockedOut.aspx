<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserLockedOut.aspx.cs" Inherits="SEPAdmin.Management.UserManagement.UserLockedOut"
 MasterPageFile="~/MainMaster.Master"  %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <table width="100%">                
                <tr>
                    <td colspan="2">
                        <table width="100%">
                            <tr>
                                <td class="heading">
                                    已锁定的用户
                                </td>
                            </tr>
                            <tr>
            <td >
            <table width="100%" id="tabTop" runat="server">
            <tr>
            <td width="50%">
                <asp:Panel ID="PageTop" runat="server">
                    <asp:Button ID="btnFirst2" runat="server" CssClass="widebuttons" Text="首页" OnClick="btnFirst_Click" />&nbsp;
                    <asp:Button ID="btnPrevious2" runat="server" CssClass="widebuttons" Text="上页" OnClick="btnPrevious_Click" />&nbsp;
                    <asp:Button ID="btnNext2" runat="server" CssClass="widebuttons" Text="下页" OnClick="btnNext_Click" />&nbsp;
                    <asp:Button ID="btnLast2" runat="server" CssClass="widebuttons" Text="末页" OnClick="btnLast_Click" />
                </asp:Panel>
            </td>
            <td align="right" class="recordTd">
                记录数:<asp:Label ID="labAllNumT" runat="server" />&nbsp;页数:<asp:Label ID="labPageCountT"
                    runat="server" />
            </td>
        </tr>
    </table>    
    </td>
        </tr>          
            <tr>
                <td>
                    <asp:GridView ID="gvView" runat="server" AutoGenerateColumns="false" OnPageIndexChanging="gvView_PageIndexChanging" OnRowCommand="gvView_RowCommand" PageSize="20" AllowPaging="True" >
                        <Columns>                            
                            <asp:TemplateField HeaderText="ITCode">
                                <ItemTemplate>
                                    <asp:Label ID="lblCode" runat="server" Text='<%# Eval("Username") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="姓名">
                                <ItemTemplate>
                                     <asp:Label ID="lblName" runat="server" Text='<%# Eval("FullNameCN") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>                             
                            <asp:TemplateField HeaderText="解锁" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" CommandName="Del" runat="server" ImageUrl="../../images/dc.gif" CommandArgument='<%# Eval("UserID")%>' />
                                    <act:ConfirmButtonExtender ID="btnDelete_ConfirmButtonExtender" runat="server" TargetControlID="ImageButton1"
                                        ConfirmText="您是否要解锁?"></act:ConfirmButtonExtender>
                                </ItemTemplate>
                            </asp:TemplateField>                            
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
        <td>
        <table width="100%" id="tabBottom" runat="server">
        <tr>
            <td width="50%">
                <asp:Panel ID="PageBottom" runat="server">
                    <asp:Button ID="btnFirst" runat="server" CssClass="widebuttons" Text="首页" OnClick="btnFirst_Click" />&nbsp;
                    <asp:Button ID="btnPrevious" runat="server" CssClass="widebuttons" Text="上页" OnClick="btnPrevious_Click" />&nbsp;
                    <asp:Button ID="btnNext" runat="server" CssClass="widebuttons" Text="下页" OnClick="btnNext_Click" />&nbsp;
                    <asp:Button ID="btnLast" runat="server" CssClass="widebuttons" Text="末页" OnClick="btnLast_Click" />&nbsp;
                </asp:Panel>
            </td>
            <td align="right" class="recordTd">
                记录数:<asp:Label ID="labAllNum" runat="server" />&nbsp;页数:<asp:Label ID="labPageCount"
                    runat="server" />
            </td>
        </tr>
    </table>
        </td>
        </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
