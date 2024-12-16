<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CashExpenseAccountList.aspx.cs" Inherits="FinanceWeb.ExpenseAccount.CashExpenseAccountList" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script type="text/javascript">
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }
    </script>

    <table style="width: 100%">
        <tr>
            <td colspan="2" style="padding: 4px;">
                <table style="width: 100%;">
                    <tr>
                        <td colspan="2">
                            <table width="100%" class="tableForm">
                                
                                <tr>
                                    <td class="heading" colspan="4">
                                        检索
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow" style="width: 15%">
                                        关键字:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:TextBox ID="txtKey" runat="server" />
                                    </td>
                                    
                                    <td class="oddrow" style="width: 15%">
                                        提交日期:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:TextBox ID="txtBeginDate" runat="server" onclick="setDate(this);" onkeyDown="return false; "></asp:TextBox>
                                        --
                                        <asp:TextBox ID="txtEndDate" runat="server" onclick="setDate(this);" onkeyDown="return false; "></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    
                                    <td class="oddrow-l" colspan="2">
                                        <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click" />
                                    </td>
                                    <td class="oddrow" style="width: 15%">
                                        <%--报销申请状态:--%>
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:DropDownList runat="server" ID="ddlStatus" Visible="false">
                                            <asp:ListItem Text="请选择.." Value="-1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow-l" colspan="4">
                            <li>
                                <asp:LinkButton ID="lbNewProject" runat="server" Text="创建现金借款单" OnClick="lnkNew_Click" /></li>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                            padding-top: 4px;">
                            <table width="100%">
                                <tr>
                                    <td class="heading" colspan="4">
                                        <asp:Label runat="server" ID="labHeadText"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow">
                                        <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="ReturnID"
                                            OnRowCommand="gvG_RowCommand" OnRowDataBound="gvG_RowDataBound" PageSize="20"
                                            EmptyDataText="暂时没有相关记录" PagerSettings-Mode="NumericFirstLast" OnPageIndexChanging="gvG_PageIndexChanging"
                                            AllowPaging="true" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="申请单号" ItemStyle-HorizontalAlign="Center" >
                                                    <ItemTemplate>
                                                        <a href="CashDisplay.aspx?id=<%#Eval("ReturnID") %>">
                                                            <%#Eval("ReturnCode")%></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ProjectCode" HeaderText="项目号" ItemStyle-HorizontalAlign="Center" />
                                                <asp:TemplateField HeaderText="项目名称" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="labProjectName" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="DepartmentName" HeaderText="费用所属组" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="PreFee" HeaderText="预计申请金额" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:f2}" />
                                                <asp:BoundField DataField="FactFee" HeaderText="实际冲销金额" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:f2}" />
                                                <%--<asp:TemplateField HeaderText="预计借款金额" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="labPreFee" Text='<%# Eval("PreFee")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="实际报销金额" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="labFactFee" Text='<%# Eval("FactFee")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="申请人" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="labRequestUserName"  CssClass="userLabel" Text='<%# Eval("RequestEmployeeName") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="RequestDate" HeaderText="申请日期" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:d}"/>
                                                <asp:TemplateField HeaderText="单据状态" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblStatus" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="当前审批人" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblAssigneeName" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="打印预览">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hylPrint" runat="server" ImageUrl="/images/Icon_Output.gif" ToolTip="打印预览"
                                                            Width="4%"></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="编辑">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hylEdit" runat="server"  ToolTip="编辑" Width="4%"></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="删除" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center" >
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDelete" runat="server" Text="<img title='删除现金借款申请' src='../../images/disable.gif' border='0' />"
                                                            OnClientClick="return confirm('您是否确认删除？');" CausesValidation="false" CommandArgument='<%# Eval("ReturnID") %>'
                                                            CommandName="Del" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="冲销">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <%--<asp:HyperLink ID="hylConfirm" runat="server"  ToolTip="收货" Width="4%"></asp:HyperLink>--%>
                                                        
                                                        <asp:LinkButton ID="hylConfirm" runat="server" Text="<img title='收货' src='../../images/recipent_icon.gif' border='0' />"
                                                            OnClientClick="return confirm('您是否要创建冲销单？');" CausesValidation="false" CommandArgument='<%# Eval("ReturnID") %>'
                                                            CommandName="Recived" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 10px">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
