<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SEPAdmin.HR.Default" MasterPageFile="~/MasterPage.master"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >	
<table width="100%" border="0" class="tableForm">
   <tr>
      <td class="heading" colspan="2">转正提醒</td>
   </tr>
</table >
<table width="100%"> 
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
            <td >
            <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="20"
          OnRowDataBound="gvList_RowDataBound" OnPageIndexChanging="gvList_PageIndexChanging" DataKeyNames="userid,status,EmployeesInPositionsList" Width="100%" >
         <Columns>
            <asp:BoundField DataField="username" HeaderText="员工"  ItemStyle-HorizontalAlign="Center"/>
            <asp:BoundField DataField="fullnamecn" HeaderText="中文姓名" ItemStyle-HorizontalAlign="Center"/>
            <asp:BoundField DataField="fullnameen" HeaderText="英文姓名" ItemStyle-HorizontalAlign="Center"/>
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
            <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>                    
                </ItemTemplate>
            </asp:TemplateField>    
                <asp:TemplateField HeaderText="转正" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                       <a href='Employees/PassedCheckInEdit.aspx?userid=<%# Eval("UserID").ToString() %>'><img src='../../images/edit.gif' border='0px;'></a>
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
<table width="100%" border="0" >
   <tr>
      <td style="height:30px"></td>
   </tr>
</table >
<table width="100%" border="0" class="tableForm">
   <tr>
      <td class="heading" colspan="2">合同提醒</td>
   </tr>
</table >
<table width="100%"> 
        <tr>
            <td >
            <table width="100%" id="tabTop2" runat="server">
            <tr>
            <td width="50%">
                <asp:Panel ID="PageTop2" runat="server">
                    <asp:Button ID="btnFirst3" runat="server" CssClass="widebuttons" Text="首页" OnClick="btnFirst2_Click" />&nbsp;
                    <asp:Button ID="btnPrevious3" runat="server" CssClass="widebuttons" Text="上页" OnClick="btnPrevious2_Click" />&nbsp;
                    <asp:Button ID="btnNext3" runat="server" CssClass="widebuttons" Text="下页" OnClick="btnNext2_Click" />&nbsp;
                    <asp:Button ID="btnLast3" runat="server" CssClass="widebuttons" Text="末页" OnClick="btnLast2_Click" />
                </asp:Panel>
            </td>
            <td align="right" class="recordTd">
                记录数:<asp:Label ID="labAllNumT2" runat="server" />&nbsp;页数:<asp:Label ID="labPageCountT2"
                    runat="server" />
            </td>
        </tr>
    </table>    
    </td>
        </tr>          
        <tr>
            <td >
            <asp:GridView ID="gvList2" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="20"
          OnRowDataBound="gvList2_RowDataBound" OnPageIndexChanging="gvList2_PageIndexChanging" DataKeyNames="userid,status,EmployeesInPositionsList" Width="100%" >
         <Columns>
            <asp:BoundField DataField="username" HeaderText="员工"  ItemStyle-HorizontalAlign="Center"/>
            <asp:BoundField DataField="fullnamecn" HeaderText="中文姓名" ItemStyle-HorizontalAlign="Center"/>
            <asp:BoundField DataField="fullnameen" HeaderText="英文姓名" ItemStyle-HorizontalAlign="Center"/>
            <asp:TemplateField HeaderText="部门职位" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                    <asp:Repeater ID="repJob2" runat="server" >
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
            <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>                    
                </ItemTemplate>
            </asp:TemplateField> 
            <asp:TemplateField HeaderText="合同截止时间" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                    <%# Eval("EmployeeWelfareInfo.contractEndDate") == null || DateTime.Parse(Eval("EmployeeWelfareInfo.contractEndDate").ToString()).ToString("yyyy-MM-dd") == "1900-01-01" ? "" : DateTime.Parse(Eval("EmployeeWelfareInfo.contractEndDate").ToString()).ToString("yyyy-MM-dd")%> 
                </ItemTemplate>
            </asp:TemplateField>      
                <asp:TemplateField HeaderText="合同" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                       <a href="Employees/EmployeesEdit.aspx?userid=<%# Eval("UserID").ToString() %>"><img src="../../images/edit.gif" border="0px;"></a>
                    </ItemTemplate>
            </asp:TemplateField>            
        </Columns>
            </asp:GridView>            
            </td>                   
        </tr> 
        <tr>
        <td>
        <table width="100%" id="tabBottom2" runat="server">
        <tr>
            <td width="50%">
                <asp:Panel ID="PageBottom2" runat="server">
                    <asp:Button ID="btnFirst4" runat="server" CssClass="widebuttons" Text="首页" OnClick="btnFirst2_Click" />&nbsp;
                    <asp:Button ID="btnPrevious4" runat="server" CssClass="widebuttons" Text="上页" OnClick="btnPrevious2_Click" />&nbsp;
                    <asp:Button ID="btnNext4" runat="server" CssClass="widebuttons" Text="下页" OnClick="btnNext2_Click" />&nbsp;
                    <asp:Button ID="btnLast4" runat="server" CssClass="widebuttons" Text="末页" OnClick="btnLast2_Click" />&nbsp;
                </asp:Panel>
            </td>
            <td align="right" class="recordTd">
                记录数:<asp:Label ID="labAllNum2" runat="server" />&nbsp;页数:<asp:Label ID="labPageCount2"
                    runat="server" />
            </td>
        </tr>
    </table>
        </td>
        </tr>
</table>
</asp:Content>
