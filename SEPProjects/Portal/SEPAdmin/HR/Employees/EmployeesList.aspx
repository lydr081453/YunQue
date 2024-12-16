<%@ Page Language="C#" AutoEventWireup="true" Inherits="Employees_EmployeesList"
    MasterPageFile="~/MasterPage.master" Codebehind="EmployeesList.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="padding: 4px;">
                <table width="100%" border="0" class="tableForm">
                    <tr>
                        <td class="heading" colspan="4">
                            入职登记检索
                        </td>
                    </tr>                
                    <tr>
                        <td class="oddrow" style="width: 10%">
                            员工工号:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>&nbsp;
                        </td>                    
                        <td class="oddrow" style="width: 10%">
                            员工姓名:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="SearchBtn" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="SearchBtn_Click">
                            </asp:Button>
                        </td>
                    </tr>
                  </table>
					 <br />
				  <table style="width:100%;">
                    <tr>
                        <td>
                            <table width="100%" id="tabTop" runat="server">
                                <tr>
                                    <td width="50%">
                                        <asp:Panel ID="PageTop" runat="server">
                                            <asp:Button ID="btnFirst2" runat="server" CssClass="widebuttons" Text="首页" OnClick="btnFirst_Click" />&nbsp;
                                            <asp:Button ID="btnPrevious2" runat="server" CssClass="widebuttons" Text="上页" OnClick="btnPrevious_Click" />&nbsp;
                                            <asp:Button ID="btnNext2" runat="server" CssClass="widebuttons" Text="下页" OnClick="btnNext_Click" />&nbsp;
                                            <asp:Button ID="btnLast2" runat="server" CssClass="widebuttons" Text="末页" OnClick="btnLast_Click" />&nbsp;
                                跳转到第<asp:DropDownList ID="ddlCurrentPage2" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                                    </asp:DropDownList>页
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
                            <asp:GridView ID="gvE" runat="server" AutoGenerateColumns="False" DataKeyNames="UserID,EmployeesInPositionsList" OnRowDataBound="gvE_RowDataBound"
                                PageSize="20" Width="100%" AllowPaging="True" EnableViewState="false">
                                <Columns>
                                    <asp:BoundField DataField="code" HeaderText="工号" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="fullnamecn" HeaderText="员工中文姓名" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="fullnameen" HeaderText="员工英文姓名" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="部门职位" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                        <asp:Repeater ID="repJob" runat="server" >
                                             <ItemTemplate>
                                                  部门：<%# Eval("CompanyName") == null ? "" : Eval("CompanyName").ToString() %>--
                                  <%# Eval("DepartmentName") == null ? "" : Eval("DepartmentName").ToString()%>--
                                  <%# Eval("GroupName") == null ? "" : Eval("GroupName").ToString() %>&nbsp;&nbsp;
                                  职位：<%# Eval("DepartmentPositionName").ToString()%><br />
                                              </ItemTemplate>
                                        </asp:Repeater>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                    <asp:TemplateField HeaderText="入职日期" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("EmployeeJobInfo.joinDate") == null || DateTime.Parse(Eval("EmployeeJobInfo.joinDate").ToString()).ToString("yyyy-MM-dd") == "1900-01-01" ? "" : DateTime.Parse(Eval("EmployeeJobInfo.joinDate").ToString()).ToString("yyyy-MM-dd")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="基本信息录入" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("baseInfoOk").ToString() == "True" ? "是" : "否"%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="合同情况录入" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("contractInfoOk").ToString() == "True" ? "是" : "否"%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="五险一金录入" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("insuranceInfoOk").ToString() == "True" ? "是" : "否"%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="档案情况录入" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("archiveInfoOk").ToString() == "True" ? "是" : "否"%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a href="EmployeesEdit.aspx?userid=<%# Eval("UserID").ToString() %>"><img src="../../images/edit.gif" border="0px;"></a>
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
