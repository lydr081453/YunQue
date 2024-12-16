<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" Inherits="Customer_CustomerDiffer" Title="客户信息比对" Codebehind="CustomerDiffer.aspx.cs" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<table width="100%">
<tr>
<td align="left" width="1%">
 <img src="/images/differ.jpg" width="30px" height="30px"/>
</td>
<td  align="left">
  <font color="red">黑色字体为正式库的客户信息<br />
        红色字体为业务人员调整后的客户信息</font>
</td>
</tr>
</table>
 
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                客户信息比对
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                英文简称:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="lblShortEN" runat="server"></asp:Label><br />
                <asp:Label ID="lblShortEND" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                客户名称:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblNameCN1" runat="server"></asp:Label><br />
                <asp:Label ID="lblNameCN1D" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            </td>
<%--            <td class="oddrow" style="width: 15%">
                中文名称2:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblNameCN2" runat="server"></asp:Label><br />
                <asp:Label ID="lblNameCN2D" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                中文简称：
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblShortCN" runat="server"></asp:Label><br />
                <asp:Label ID="lblShortCND" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            </td>--%>
            <td class="oddrow">
                发票抬头:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblInvoiceTitle" runat="server"></asp:Label><br />
                <asp:Label ID="lblInvoiceTitleD" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            </td>
        </tr>
<%--        <tr>
            <td class="oddrow" style="width: 15%">
                英文名称1:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblNameEN1" runat="server"></asp:Label><br />
                <asp:Label ID="lblNameEN1D" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                英文名称2:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblNameEN2" runat="server"></asp:Label><br />
                <asp:Label ID="lblNameEN2D" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            </td>
        </tr>--%>
        <tr>
            <td class="oddrow" style="width: 15%">
                客户地址:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="lblAddress1" runat="server"></asp:Label><br />
                <asp:Label ID="lblAddress1D" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            </td>
        </tr>
<%--        <tr>
            <td class="oddrow" style="width: 15%">
                客户地址2:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="lblAddress2" runat="server"></asp:Label><br />
                <asp:Label ID="lblAddress2D" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            </td>
        </tr>--%>
        <tr>
<%--            <td class="oddrow" style="width: 15%">
                公司邮编:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblPostCode" runat="server"></asp:Label><br />
                <asp:Label ID="lblPostCodeD" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            </td>--%>
            <td class="oddrow" style="width: 15%">
                公司网址:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="lblWebSite" runat="server"></asp:Label><br />
                <asp:Label ID="lblWebSiteD" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                所在地区:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblArea" runat="server"></asp:Label><br />
                <asp:Label ID="lblAreaD" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                所在行业:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblIndustry" runat="server"></asp:Label><br />
                <asp:Label ID="lblIndustryD" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="4">
                联系人信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                客户联系人:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblContact" runat="server"></asp:Label><br />
                <asp:Label ID="lblContactD" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                联系人职务:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblContactPosition" runat="server"></asp:Label><br />
                <asp:Label ID="lblContactPositionD" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                联系人电话:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblContactMobile" runat="server"></asp:Label><br />
                <asp:Label ID="lblContactMobileD" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                联系人传真:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="lblContactFax" runat="server"></asp:Label><br />
                <asp:Label ID="lblContactFaxD" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                联系人Email:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="lblEmail" runat="server" Width="40%"></asp:Label><br />
                <asp:Label ID="lblEmailD" runat="server" Width="40%" ForeColor="Red" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="heading" colspan="4">
                            协议列表
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow">
                            <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="AttachID"
                                OnRowDataBound="gvG_RowDataBound" EmptyDataText="暂时没有相关记录" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="AttachID" HeaderText="AttachID" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="FrameContractTitle" HeaderText="协议描述" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="FrameBeginDate" HeaderText="起始日期" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="FrameEndDate" HeaderText="结束日期" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="16%" />
                                    <asp:BoundField DataField="FrameContractCode" HeaderText="协议号" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="5%" />
                                    <asp:TemplateField HeaderText="附件" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="labDown" runat="server" Text='' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" class="oddrow-1" align="center">
                <asp:Button ID="btnSubmit" runat="server" Text=" 变更 " CausesValidation="false" CssClass="widebuttons"
                    OnClick="btnSubmit_Click" />
                <asp:Button ID="btnBack" runat="server" Text=" 关闭 " CausesValidation="false" CssClass="widebuttons"
                    OnClick="btnBack_Click" />
            </td>
        </tr>
        <tr>
            <td style="height: 10px">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
