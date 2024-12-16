<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SocialSecurityList.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="SEPAdmin.HR.SocialSecurity.SocialSecurityList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >		
	<table style="WIDTH: 100%">				
				<tr>
					<td  style="padding:4px;">
					<table width="100%" border="0" >
                       <tr>
								<td >
									<UL>
										<LI><A runat="server" id="NewUserUrl" style="color:#000000" href="SocialSecurityEdit.aspx"><span style="color:#4f556a">新增福利保险比例</span></A></LI>
									</UL>
								</td>
							</tr>
                     </table >					 
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
                                            <asp:Button ID="btnLast2" runat="server" CssClass="widebuttons" Text="末页" OnClick="btnLast_Click" />
                                        </asp:Panel>
                                    </td>
                                    <td align="right"  class="recordTd">
                                        记录数:<asp:Label ID="labAllNumT" runat="server" />&nbsp;页数:<asp:Label ID="labPageCountT"
                                            runat="server" />
                                    </td>
                                </tr>
                            </table>
                           </td>
                            </tr>
                            <tr>
                            <td colspan="2">
                            <asp:GridView ID="gvE" runat="server"  AutoGenerateColumns="False" 
                                 Width="100%" AllowPaging="False" PageSize="20">
                                <Columns>
                                    <asp:TemplateField HeaderText="养老公司比例(%)" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# decimal.Parse(Eval("EIProportionOfFirms").ToString()).ToString("0.00")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="养老个人比例(%)" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# decimal.Parse(Eval("EIProportionOfIndividuals").ToString()).ToString("0.00")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="失业公司比例(%)" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# decimal.Parse(Eval("UIProportionOfFirms").ToString()).ToString("0.00")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="失业个人比例(%)" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# decimal.Parse(Eval("UIProportionOfIndividuals").ToString()).ToString("0.00")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>   
                                    <asp:TemplateField HeaderText="生育公司比例(%)" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# decimal.Parse(Eval("BIProportionOfFirms").ToString()).ToString("0.00")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="工伤公司比例(%)" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# decimal.Parse(Eval("CIProportionOfFirms").ToString()).ToString("0.00")%>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="医疗公司比例(%)" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# decimal.Parse(Eval("MIProportionOfFirms").ToString()).ToString("0.00")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="医疗个人比例(%)" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# decimal.Parse(Eval("MIProportionOfIndividuals").ToString()).ToString("0.00")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="大额医疗(元)" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# decimal.Parse(Eval("MIBigProportionOfIndividuals").ToString()).ToString("0.00")%>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="公积金比例(%)" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# decimal.Parse(Eval("PRFProportionOfFirms").ToString()).ToString("0.00")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>                              
                                    <asp:TemplateField HeaderText="开始时间" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# DateTime.Parse(Eval("BeginTime").ToString()).Year.ToString()%>-<%# DateTime.Parse(Eval("BeginTime").ToString()).Month.ToString()%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="结束时间" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# DateTime.Parse(Eval("EndTime").ToString()).Year.ToString()%>-<%# DateTime.Parse(Eval("EndTime").ToString()).Month.ToString()%> 
                                        </ItemTemplate>
                                    </asp:TemplateField>                                                                
                                    <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a href="SocialSecurityEdit.aspx?id=<%# Eval("ID").ToString() %>"><img src="../../images/edit.gif" border="0px;" alt="编辑"></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1"  CommandName="Del" runat="server" ImageUrl="../../images/disable.gif" CommandArgument='<%# Eval("userID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField> --%>
                                                                                                                                                                   
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
                        </td>
                    </tr>
						</table>
						
					</td>
				</tr>
			</table>
</asp:Content>

