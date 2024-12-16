<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MainMaster.Master" EnableEventValidation="false"
    CodeBehind="UserBrowse.aspx.cs" Inherits="SEPAdmin.UserManagement.UserBrowse" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <table width="100%">
                <tr >
                 <td class="heading">用户浏览</td>
                </tr>
                <tr>
                    <td class="oddrow" width="300px">
                        检索用户信息(输入用户名称或者ITCode):
                    </td>
                    <td class="oddrow-l">
                        <asp:TextBox ID="txtSearch" runat="server"  Width="300px"/><asp:Button ID="btnSearch" Text="检索" CssClass="widebuttons"
                            runat="server" />
                    </td>
                </tr>
                <%--<tr>
                    <td colspan="2">
                        <a href="UserForm.aspx">新增用户</a>
                    </td>
                </tr>--%>
                <tr>
                    <td colspan="2">
                        <table width="100%">
                            <tr>
                                <td class="heading">
                                    详细信息
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
                    <asp:GridView ID="gvView" runat="server" AutoGenerateColumns="false" OnPageIndexChanging="gvView_PageIndexChanging" OnRowDataBound="gvView_RowDataBound" PageSize="20" AllowPaging="True" DataKeyNames="UserID">
                        <Columns>
                            <%--<asp:TemplateField HeaderText="选择">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkRow" runat="server" Checked="false" />
                                    <asp:HiddenField ID="hdnID" runat="server" Value='<%# Eval("UserID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="ITCode">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Username") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="姓名">
                                <ItemTemplate>
                                    <asp:Label ID="ID" runat="server" Text='<%# Eval("FullNameCN") %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="用户类别">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmployeeType" runat="server" Text='<%# Eval("TypeName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="职位描述">
                                <ItemTemplate>
                                    <asp:Label ID="lblPositions" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="状态">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>    
                            <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a href="EmployeesEdit.aspx?userid=<%# Eval("UserID").ToString() %>"><img src="../../images/edit.gif" border="0px;"></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>                       
                            <asp:TemplateField HeaderText="部门负责人">
                                <ItemTemplate>                                    
                                    <a href='UserForm.aspx?userid=<%# Eval("UserID") %>'><img src="../../images/edit.gif" border="0px;"></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="角色">
                                <ItemTemplate>                                    
                                    <a href='UserRolesForm.aspx?userid=<%# Eval("UserID") %>'><img src="../../images/edit.gif" border="0px;"></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                           <%-- <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnDelete" runat="server" Text="删除"></asp:ImageButton>
                                    <act:ConfirmButtonExtender ID="btnDelete_ConfirmButtonExtender" runat="server" TargetControlID="btnDelete"
                                        ConfirmText="您是否要删除此条记录?">
                                    </act:ConfirmButtonExtender>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="lblUser" runat="server" Text="用户"></asp:Button>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="lblHomePage" runat="server" Text="首页"></asp:Button>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
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
