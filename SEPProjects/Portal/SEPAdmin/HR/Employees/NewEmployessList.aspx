<%@ Page Language="C#" AutoEventWireup="true" Inherits="Employees_NewEmployessList"
    MasterPageFile="~/MasterPage.master" CodeBehind="NewEmployessList.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="padding: 4px;">
                <table width="100%" border="0">
                    <tr>
                        <td>
                            <ul>
                                <li><a runat="server" id="NewUserUrl" style="color: #000000" href="EmployeeReadyEdit.aspx">
                                    <span style="color: #4f556a">新增入职员工</span></a></li>
                            </ul>
                        </td>
                    </tr>
                </table>
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="heading" colspan="3">
                            待入职人员检索
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
                            <asp:Button ID="btnSendMail" runat="server" CssClass="widebuttons" Text="发送邮件" OnClick="btnSendMail_Click" />
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
                            <asp:GridView ID="gvE" runat="server" AutoGenerateColumns="False" DataKeyNames="userID,IsSendMail"
                                Width="100%" AllowPaging="False" OnRowDataBound="gvE_RowDataBound"
                                OnRowCommand="gvE_RowCommand" onrowupdating="gvE_RowUpdating">
                                <Columns>
                                    <asp:TemplateField HeaderText="发邮件" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkMail" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="邮件发送" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="code" HeaderText="工号" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="中文名" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a href="EmployeeReadyView.aspx?userid=<%# Eval("userID").ToString() %>"><%# Eval("fullnamecn")%></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="fullnameen" HeaderText="英文名" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="入职日期" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("EmployeeJobInfo.joinDate") == null ? "" : DateTime.Parse(Eval("EmployeeJobInfo.joinDate").ToString()).ToString("yyyy-MM-dd")%>
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
                                    <asp:TemplateField HeaderText="职位" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("EmployeeJobInfo.joinJob") == null ? "" : Eval("EmployeeJobInfo.joinJob").ToString()%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="启动入职" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a href="EmployeeReadyJob.aspx?userid=<%# Eval("userID").ToString() %>">
                                                <img src="../../images/join.gif" border="0px;" alt="入职" /></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a href="EmployeeReadyEdit.aspx?userid=<%# Eval("userID").ToString() %>"><img src="../../images/edit.gif" border="0px;" alt="编辑"></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="打印">
                                        <ItemTemplate>
                                            <a href="../Print/OfferLetterPrint.aspx?userid=<%# Eval("userID").ToString() %>" target="_blank"><img src="../../Images/printno.gif" border="0px;" alt="打印Offer Letter" /></a>
                                             <asp:Label ID="lblPrint" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reject" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" CommandName="Update" runat="server" ImageUrl="../../images/lizhi.gif"
                                                CommandArgument='<%# Eval("userID") %>'/>
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
                            <asp:Button ID="Button1" runat="server" CssClass="widebuttons" Text="发送邮件" OnClick="btnSendMail_Click" />
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
