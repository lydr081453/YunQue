<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"  CodeFile="ProcessMain.aspx.cs" Inherits="ProcessMain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<table style="width:100%; height:100%">
<tr><td><asp:Button ID="btnTmp" runat="server" Text="create template" onclick="btnTmp_Click" /></td></tr>
   <tr>  
       <td style="width:100%; height:100%">
            
           <asp:GridView ID="GridView1" runat="server" Height="100%" Width="100%" 
               AutoGenerateColumns="False" AutoGenerateSelectButton="True" 
               onselectedindexchanged="GridView1_SelectedIndexChanged">
           <FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5" />
　　       <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center"></PagerStyle>
　　       <HeaderStyle ForeColor="White" Font-Bold="True" BackColor="#A55129" />
　　       
<Columns>
　　<asp:BoundField ReadOnly="True" HeaderText="Process ID" InsertVisible="False" DataField="Id"
SortExpression="Id">
　　<ItemStyle HorizontalAlign="Center"></ItemStyle>
　</asp:BoundField>
　<asp:BoundField HeaderText="Process Name" DataField="PROCESSNAME" SortExpression="PROCESSNAME">
　</asp:BoundField>
　<asp:BoundField HeaderText="Initiator" 
　　　　DataField="INITIATORNAME"
　　　　SortExpression="INITIATORNAME"></asp:BoundField>
　<asp:BoundField HeaderText="Start Date" 
　　　　DataField="STARTDATE" SortExpression="STARTDATE" 
　　　　DataFormatString="">
　　　<ItemStyle HorizontalAlign="Center"></ItemStyle>
　</asp:BoundField>
　<asp:BoundField HeaderText="End Date" DataField="ENDDATE" 
　　　　SortExpression="ENDDATE" 
　　　　DataFormatString="">
　　<ItemStyle HorizontalAlign="Center"></ItemStyle>
　</asp:BoundField>
　　<asp:BoundField HeaderText="State" DataField="PROCESSINSTANCESTATE" 
　　　　SortExpression="PROCESSINSTANCESTATE" 
　　　　DataFormatString="">
　　<ItemStyle HorizontalAlign="Center"></ItemStyle>
　</asp:BoundField>
</Columns>

<SelectedRowStyle ForeColor="White" Font-Bold="True" 
　　BackColor="#738A9C" />
　　<RowStyle ForeColor="#8C4510" BackColor="#FFF7E7" />
           </asp:GridView>
            
       </td>
   </tr>
   <tr>
       <td><asp:Button id="btnCreate" Text="Create Process" runat="server" 
               onclick="txtCreate_Click" />
               <asp:Button id="btnSuspend" Text="Suspend Process" runat="server" onclick="btnSuspend_Click" 
               />
               <asp:Button id="btnResume" Text="Resume Process" runat="server" 
               />
               <br />
            
           <asp:GridView ID="GridView2" runat="server" Height="100%" Width="100%" 
               AutoGenerateColumns="False" AutoGenerateSelectButton="True">
           <FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5" />
　　       <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center"></PagerStyle>
　　       <HeaderStyle ForeColor="White" Font-Bold="True" BackColor="#A55129" />
　　       
<Columns>
　　<asp:BoundField ReadOnly="True" HeaderText="Task ID" InsertVisible="False" DataField="Id"
SortExpression="Id">
　　<ItemStyle HorizontalAlign="Center"></ItemStyle>
　</asp:BoundField>
　<asp:BoundField HeaderText="Task Name" DataField="TASKNAME" SortExpression="TASKNAME">
　</asp:BoundField>
　<asp:BoundField HeaderText="State" 
　　　　DataField="STATE"
　　　　SortExpression="STATE"></asp:BoundField>
　<asp:BoundField HeaderText="Start Date" 
　　　　DataField="STARTDATE" SortExpression="STARTDATE" 
　　　　DataFormatString="">
　　　<ItemStyle HorizontalAlign="Center"></ItemStyle>
　</asp:BoundField>
　<asp:BoundField HeaderText="End Date" DataField="ENDDATE" 
　　　　SortExpression="ENDDATE" 
　　　　DataFormatString="">
　　<ItemStyle HorizontalAlign="Center"></ItemStyle>
　</asp:BoundField>
</Columns>

<SelectedRowStyle ForeColor="White" Font-Bold="True" 
　　BackColor="#738A9C" />
　　<RowStyle ForeColor="#8C4510" BackColor="#FFF7E7" />
           </asp:GridView>

           <asp:Button id="btnActive" Text="Active Task" runat="server" onclick="btnActive_Click" 
                />

           <asp:Button id="btnSuspendTask" Text="Suspend Task" runat="server" 
               onclick="btnSuspendTask_Click" />

           <asp:Button id="btnResumeTask" Text="Resume Task" runat="server" 
               onclick="btnResumeTask_Click" style="height: 26px" 
               />

           <asp:Button id="btnCompleteTask" Text="Complete Task" runat="server" onclick="btnCompleteTask_Click" 
                />
                  <asp:Button id="Button1" Text="Delete test" runat="server" onclick="Button1_Click" 
                />
           &nbsp;
           </td>

   </tr>
</table>
</asp:Content>