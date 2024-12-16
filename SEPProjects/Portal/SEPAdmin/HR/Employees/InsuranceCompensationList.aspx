<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsuranceCompensationList.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="SEPAdmin.HR.Employees.InsuranceCompensationList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >		
	<table style="WIDTH: 100%">				
				<tr>
					<td  style="padding:4px;">
					<table width="100%" border="0" >
                       <tr>
								<td >
									<UL>
										<LI><A runat="server" id="NewUserUrl" style="color:#000000" href="InsuranceCompensationEdit.aspx"><span style="color:#4f556a">新增员工福利保险补发</span></A></LI>
									</UL>
								</td>
							</tr>
                     </table >
					 <table width="100%" class="tableForm">	
						<tr>
                          <td class="heading" colspan="3">员工福利保险补发检索</td>
                        </tr>					
						<tr>
							<td class="oddrow" style="width:10%">员工姓名:</td>
							<td class="oddrow-l" style="width:20%"><asp:textbox id="txtName" runat="server"></asp:textbox>&nbsp;</td>									
						    <td class="oddrow-l" ><asp:button id="SearchBtn" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="SearchBtn_Click"></asp:button></td>
						</tr>
					 </table>
					 <br />
<%--					 <table style="width:100%;">														
						<tr>						    
                            <td>--%>
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
                                    <td align="right"  class="recordTd">
                                        记录数:<asp:Label ID="labAllNumT" runat="server" />&nbsp;页数:<asp:Label ID="labPageCountT"
                                            runat="server" />
                                    </td>
                                </tr>
                            </table>
<%--                           </td>
                            </tr>
                            <tr>
                            <td colspan="2">--%>
                            <asp:GridView ID="gvE" runat="server"  AutoGenerateColumns="False" 
                                DataKeyNames="userID" Width="100%" AllowPaging="False" PageSize="20">
                                <Columns>
                                    <asp:BoundField DataField="fullnamecn" HeaderText="员工中文姓名" ItemStyle-HorizontalAlign="Center"/>                                    
                                    <asp:TemplateField HeaderText="补交年份" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("PayYear").ToString()%> 
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="补交年份" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("PayMonth").ToString()%> 
                                        </ItemTemplate>
                                    </asp:TemplateField>                                                                
                                    <%--<asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a href="EmployeeReadyEdit.aspx?userid=<%# Eval("userID").ToString() %>"><img src="../../images/edit.gif" border="0px;" alt="编辑"></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <%--<asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1"  CommandName="Del" runat="server" ImageUrl="../../images/disable.gif" CommandArgument='<%# Eval("userID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField> --%>
                                                                                                                                                                   
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" />
                                <PagerSettings Visible="false" />
                            </asp:GridView>
<%--                            </td>
                            </tr>
                            <tr>                            
                            <td>--%>
                            <table width="100%" id="tabBottom" runat="server">                            
                                <tr>                                
                                    <td width="50%" >
                                        <asp:Panel ID="PageBottom" runat="server">
                                            <asp:Button ID="btnFirst" runat="server" CssClass="widebuttons" Text="首页" OnClick="btnFirst_Click" />&nbsp;
                                            <asp:Button ID="btnPrevious" runat="server" CssClass="widebuttons" Text="上页" OnClick="btnPrevious_Click" />&nbsp;
                                            <asp:Button ID="btnNext" runat="server" CssClass="widebuttons" Text="下页" OnClick="btnNext_Click" />&nbsp;
                                            <asp:Button ID="btnLast" runat="server" CssClass="widebuttons" Text="末页" OnClick="btnLast_Click" />&nbsp;                                            
                                        </asp:Panel>
                                    </td>
                                    <td align="right"  class="recordTd">
                                        记录数:<asp:Label ID="labAllNum" runat="server" />&nbsp;页数:<asp:Label ID="labPageCount" runat="server" />
                                    </td>
                                </tr>
                            </table>
<%--                        </td>
                    </tr>
						</table>--%>
						
					</td>
				</tr>
			</table>
</asp:Content>

