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
                                        �����ļ�
                                    </td>
                                </tr>
                                <tr>
                                 <td class="oddrow" style="width: 15%">
                                        �����������:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                    <asp:DropDownList ID="ddlCostType" runat="server">
                                    <asp:ListItem Value="-1" Text="��ѡ��.." Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="��Ʊ" ></asp:ListItem>
                                    <asp:ListItem Value="1" Text="��ӡװ��"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="���" ></asp:ListItem>
                                    <asp:ListItem Value="3" Text="�Ƶ�" ></asp:ListItem>
                                    </asp:DropDownList>
                                    </td>
                                    <td class="oddrow" style="width: 15%">
                                        ѡ��:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                       <asp:FileUpload ID="fileUp" runat="server" />
                                    </td>
                                  
                                </tr>
                                <tr>
                                   <td class="oddrow" style="width: 15%">
                                        ����ģ��:
                                    </td>
                                      <td class="oddrow-l" style="width: 35%">
                                      <a href="/Templates/ImportPNTemplate.xls" ><img src="/images/ico_04.gif"/></a>
                                      </td>
                                  <td class="oddrow-l" colspan="2">
                                     <asp:Button ID="btnSearch" runat="server" Text=" ��ȡ " CssClass="widebuttons" 
                                            onclick="btnSearch_Click" style="width: 50px" />&nbsp;
                                                    <asp:Button  ID="btnOK" runat="server" Text=" ���� " CssClass="widebuttons" 
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
                                        ���븶�������б�
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow">
                                        <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="ReturnID"
                                            OnRowDataBound="gvG_RowDataBound"
                                            EmptyDataText="��ʱû�и��������¼" PagerSettings-Mode="NumericFirstLast"
                                            AllowPaging="false" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="���" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                                    <ItemTemplate>
                                                    <asp:Label runat="server"  ID="lblNo"></asp:Label>
                                                      </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ProjectCode" HeaderText="��Ŀ��" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="PreBeginDate" HeaderText="���÷�������" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="RequestEmployeeName" HeaderText="������" ItemStyle-HorizontalAlign="Center" />
                                                 <asp:BoundField DataField="RequestUserCode" HeaderText="Ա�����" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="DepartmentName" HeaderText="����������" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="ReturnContent" HeaderText="������ϸ����" ItemStyle-HorizontalAlign="Center" />
                                                <asp:TemplateField HeaderText="������" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="labExpectPaymentPrice" Text='<%# Eval("PreFee") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:BoundField DataField="PrNo" HeaderText="����" ItemStyle-HorizontalAlign="Center" />
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
