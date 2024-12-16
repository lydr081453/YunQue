<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeesDisplay.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="SEPAdmin.HR.Employees.EmployeesDisplay" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server" >
<table width="100%" class="tableForm">
  <tr>
            <td class="heading" colspan="4">
                员工检索
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">
                员工编号:
            </td>
            <td class="oddrow-l" >
                <asp:TextBox ID="txtITCode" runat="server" />
            </td>
            <td class="oddrow" style="width: 10%">
                姓名:
            </td>
            <td class="oddrow-l" >
                <asp:TextBox ID="txtuserName" runat="server" />
            </td>
            <td class="oddrow" style="width: 10%">入职日期：</td>
            <td class="oddrow-l">          
                <asp:DropDownList ID="drpJoinDate" runat="server">
                                <asp:ListItem Value="1" Text="全部" />                                
                                <asp:ListItem Value="2" Text="当月" />                                
                            </asp:DropDownList>
             </td>
        </tr>        
        <tr>
            <td class="oddrow-l" colspan="8">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click" />
            </td>
        </tr>
  </table> 
  <br />
  <table style="width:100%;">
     <tr>
        <td>
            <table width="100%"  id="tableUp"> 

                    <tr>
                        <td >
                        <table width="100%" id="tabTop" runat="server">
                        <tr>
                        <td width="50%">
                            <asp:Panel ID="PageTop" runat="server">
                                <asp:Button ID="btnFirst2" runat="server" CssClass="widebuttons" Text="首页" OnClick="btnFirst_Click" />&nbsp;
                                <asp:Button ID="btnPrevious2" runat="server" CssClass="widebuttons" Text="上页" OnClick="btnPrevious_Click" />&nbsp;
                                <asp:Button ID="btnNext2" runat="server" CssClass="widebuttons" Text="下页" OnClick="btnNext_Click" />&nbsp;
                                <asp:Button ID="btnLast2" runat="server" CssClass="widebuttons" Text="末页" OnClick="btnLast_Click" />&nbsp;
                                跳转到第<asp:DropDownList ID="ddlCurrentPage2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
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
                        <td >
                        <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="20" 
                      OnRowDataBound="gvList_RowDataBound" OnPageIndexChanging="gvList_PageIndexChanging" DataKeyNames="userid,status,EmployeesInPositionsList" Width="100%" >
                     <Columns>
                        <asp:BoundField DataField="code" HeaderText="工号"  ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="fullnamecn" HeaderText="员工中文姓名" ItemStyle-HorizontalAlign="Center"/>                       
                        <asp:BoundField DataField="fullnameen" HeaderText="员工英文姓名" ItemStyle-HorizontalAlign="Center"/>
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
                                <%# Eval("EmployeeJobInfo.joinDate") == null || DateTime.Parse(Eval("EmployeeJobInfo.joinDate").ToString()).ToString("yyyy-MM-dd") == "1900-01-01"  ? "" : DateTime.Parse(Eval("EmployeeJobInfo.joinDate").ToString()).ToString("yyyy-MM-dd")%> 
                            </ItemTemplate>
                        </asp:TemplateField>                        
                        <asp:TemplateField HeaderText="身份证号" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# Eval("IDNumber") == null ? "" : Eval("IDNumber").ToString()%> 
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
</asp:Content>
