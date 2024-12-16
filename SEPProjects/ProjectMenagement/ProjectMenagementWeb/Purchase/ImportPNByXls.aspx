<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="ImportPNByXls.aspx.cs" Inherits="FinanceWeb.Purchase.ImportPNByXls" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">

    <table style="width: 100%">
        <tr>
            <td colspan="2" style="padding: 4px;">
                <table style="width: 100%;">
                    <tr>
                        <td colspan="2">
                            <table width="100%" class="tableForm">
                                <tr>
                                    <td class="heading" colspan="4">
                                        导入文件
                                    </td>
                                </tr>
                                <tr>
                                 <td class="oddrow" style="width: 15%">
                                        申请费用类型:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                    <asp:DropDownList ID="ddlCostType" runat="server">
                                    <asp:ListItem Value="-1" Text="请选择.." Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="机票" ></asp:ListItem>
                                    <asp:ListItem Value="1" Text="复印装订"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="快递" ></asp:ListItem>
                                    <asp:ListItem Value="3" Text="酒店" ></asp:ListItem>
                                    </asp:DropDownList>
                                    </td>
                                    <td class="oddrow" style="width: 15%">
                                        选择:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                       <asp:FileUpload ID="fileUp" runat="server" />
                                    </td>
                                  
                                </tr>
                                <tr>
                                   <td class="oddrow" style="width: 15%">
                                        下载模版:
                                    </td>
                                      <td class="oddrow-l" style="width: 35%">
                                      <a href="/Templates/ImportPNTemplate.xls" ><img src="/images/ico_04.gif"/></a>
                                      </td>
                                  <td class="oddrow-l" colspan="2">
                                     <asp:Button ID="btnSearch" runat="server" Text=" 读取 " CssClass="widebuttons" 
                                            onclick="btnSearch_Click" style="width: 50px" />&nbsp;
                                                    <asp:Button  ID="btnOK" runat="server" Text=" 导入 " CssClass="widebuttons" 
                                          onclick="btnOK_Click"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                            padding-top: 4px;">
                            <table width="100%">
                                <tr>
                                    <td class="heading" colspan="4">
                                        导入付款申请列表
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow">
                                        <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="ReturnID"
                                            OnRowDataBound="gvG_RowDataBound"
                                            EmptyDataText="暂时没有付款申请记录" PagerSettings-Mode="NumericFirstLast"
                                            AllowPaging="false" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                                    <ItemTemplate>
                                                    <asp:Label runat="server"  ID="lblNo"></asp:Label>
                                                      </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ProjectCode" HeaderText="项目号" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="PreBeginDate" HeaderText="费用发生日期" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="RequestEmployeeName" HeaderText="申请人" ItemStyle-HorizontalAlign="Center" />
                                                 <asp:BoundField DataField="RequestUserCode" HeaderText="员工编号" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="DepartmentName" HeaderText="费用所属组" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="ReturnContent" HeaderText="费用明细描述" ItemStyle-HorizontalAlign="Center" />
                                                <asp:TemplateField HeaderText="申请金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="labExpectPaymentPrice" Text='<%# Eval("PreFee") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:BoundField DataField="PrNo" HeaderText="单号" ItemStyle-HorizontalAlign="Center" />
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
    </table>
                
</asp:Content>
