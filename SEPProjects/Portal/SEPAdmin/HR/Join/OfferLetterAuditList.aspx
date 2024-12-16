<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OfferLetterAuditList.aspx.cs"
    Inherits="SEPAdmin.HR.Join.OfferLetterAuditList" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="padding: 4px;">
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
                                Width="100%" AllowPaging="False">
                                <Columns>
                                    <asp:BoundField DataField="fullnamecn" HeaderText="员工中文姓名" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="PrivateEmail" HeaderText="个人邮箱" ItemStyle-HorizontalAlign="Center" />
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
                                    <asp:TemplateField HeaderText="审批">
                                        <ItemTemplate>
                                            <a href="/HR/Join/OfferLetterAuditDetail.aspx?userid=<%# Eval("userID") %>">审批</a>
                                            <%--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/HR/Join/OfferLetterAuditDetail.aspx?userid=<%# Eval("userID") %>">审批</asp:HyperLink>--%>
                                            <%--<asp:ImageButton ID="ImageButton1" CommandName="Audit" runat="server" ImageUrl="../../images/disable.gif" PostBackUrl="~/HR/Join/OfferLetterAuditDetail.aspx?userID=<%# Eval("userID") %>" />--%>
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
