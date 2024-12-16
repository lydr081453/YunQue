<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OfferLetterList.aspx.cs"
    Inherits="OfferLetterList" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="padding: 4px;">
                <table width="100%" border="0">
                    <tr>
                        <td>
                            <ul>
                                <li><a runat="server" id="NewUserUrl" style="color: #000000" href="OfferLetterEdit.aspx">
                                    <span style="color: #4f556a">新增入职员工</span></a></li>
                            </ul>
                        </td>
                    </tr>
                </table>
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="heading" colspan="3">
                            Offer Letter人员检索
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 10%">
                            员工姓名:
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>&nbsp;
                        </td>
                        <td class="oddrow-l">
                            <asp:Button ID="SearchBtn" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="SearchBtn_Click">
                            </asp:Button>
                        </td>
                    </tr>
                </table>
                <br />
                <table style="width: 100%;">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
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
                        <td colspan="2">
                            <asp:GridView ID="gvE" runat="server" AutoGenerateColumns="False" DataKeyNames="userID,Status"
                                Width="100%" AllowPaging="False" OnRowDataBound="gvE_RowDataBound" OnRowUpdating="gvE_RowUpdating"
                                OnRowCommand="gvE_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="fullnamecn" HeaderText="员工中文姓名" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="PrivateEmail" HeaderText="个人邮箱" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="入职日期" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("EmployeeJobInfo.joinDate") == null ? "" : DateTime.Parse(Eval("EmployeeJobInfo.joinDate").ToString()).ToString("yyyy-MM-dd")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="审核状态" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="公司" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("EmployeeJobInfo.companyName") == null ? "" : Eval("EmployeeJobInfo.companyName").ToString()%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="部门" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("EmployeeJobInfo.departmentName") == null ? "" : Eval("EmployeeJobInfo.departmentName").ToString()%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="组别" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("EmployeeJobInfo.groupname") == null ? "" : Eval("EmployeeJobInfo.groupname").ToString()%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="职位" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("EmployeeJobInfo.joinJob") == null ? "" : Eval("EmployeeJobInfo.joinJob").ToString()%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50">
                                        <ItemTemplate>
                                            <a href="OfferLetterEdit.aspx?userid=<%# Eval("userID").ToString() %>">
                                                <img src="../../images/edit.gif" border="0px;" alt="编辑"></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="打印">
                                        <ItemTemplate>
                                            <a href="../Print/OfferLetterPrint.aspx?userid=<%# Eval("userID").ToString() %>"
                                                target="_blank">
                                                <img src="../../Images/printno.gif" border="0px;" alt="打印Offer Letter" /></a>
                                                <asp:Label ID="lblPrint" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     
                                    <asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" CommandName="Del" runat="server" ImageUrl="../../images/disable.gif"
                                                CommandArgument='<%# Eval("userID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reject" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imageUpdate" CommandName="Update" runat="server" ImageUrl="../../images/lizhi.gif"
                                                CommandArgument='<%# Eval("userID") %>'/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Offer EMail" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnMail" CommandName="Mail" runat="server" ImageUrl="../../images/dc.gif"
                                                CommandArgument='<%# Eval("userID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" />
                                <PagerSettings Visible="false" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
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
</asp:Content>
