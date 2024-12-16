<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="Customer_CustomerInfoView" MasterPageFile="~/MasterPage.master" Codebehind="CustomerInfoView.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="/public/css/dialog.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script type="text/javascript" language="javascript" src="/public/js/dialog.js"></script>

    <script type="text/javascript" language="javascript">
        function show(url) {
            document.getElementById("img1").src = url.replace(".jpg", "_full.jpg");
            dialog("图片", "id:divshow", "900px", "400px", "text");
        }
    </script>

    <table style="width: 100%">
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="heading" colspan="4">
                            客户信息编辑
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 20%">
                            客户代码:
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:Label ID="txtCustomerCode" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 20%">
                            地址代码:
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:Label ID="txtAddressCode" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 20%">
                            客户缩写:
                        </td>
                        <td class="oddrow-l" colspan="3">
                            <asp:Label ID="txtShortEN" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 20%">
                            客户中文名称1:
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:Label ID="txtNameCN1" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 20%">
                            客户中文名称2:
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:Label ID="txtNameCN2" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 20%">
                            客户英文名称1:
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:Label ID="txtNameEN1" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 20%">
                            客户英文名称2:
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:Label ID="txtNameEN2" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 20%">
                            地址1:
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:Label ID="txtAddress1" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 20%">
                            地址2:
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:Label ID="txtAddress2" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 20%">
                            初次申请公司:
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:Label ID="txtAppCompany" runat="server" Width="200px" Enabled="false" />
                        </td>
                        <td class="oddrow" style="width: 20%">
                            初次申请时间:
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:Label ID="txtAppDate" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 15%">
                            发票抬头:
                        </td>
                        <td class="oddrow-l" colspan="3">
                            <asp:Label ID="lblInvoiceTitle" runat="server" />
                        </td>
                    </tr>
    </table>
    </td> </tr>
    <tr>
        <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
            padding-top: 4px;">
            <table width="100%" class="tableForm">
                <tr>
                    <td class="heading" colspan="4">
                        联系信息
                    </td>
                </tr>
                <tr>
                    <td class="oddrow" style="width: 20%">
                        联系人姓名:
                    </td>
                    <td class="oddrow-l">
                        <asp:Label ID="txtContactName" runat="server" />
                    </td>
                    <td class="oddrow">
                        A0:
                    </td>
                    <td class="oddrow-l">
                        <asp:Label ID="txtA0" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="oddrow" style="width: 20%">
                        固话:
                    </td>
                    <td class="oddrow-l" style="width: 30%">
                        <asp:Label ID="txtContactTel" runat="server" />
                    </td>
                    <td class="oddrow" style="width: 20%">
                        传真:
                    </td>
                    <td class="oddrow-l" style="width: 30%">
                        <asp:Label ID="txtContactFax" runat="server" />
                    </td>
                </tr>
                <%--<tr>
                        <td class="oddrow">移动电话:</td>
                        <td class="oddrow-l"><asp:TextBox ID="txtContactMobile" runat="server" MaxLength="20" Width="50%"/></td>
                        <td class="oddrow">&nbsp;</td>
                        <td class="oddrow-l">&nbsp;</td>
                    </tr>--%>
                <tr>
                    <td class="oddrow">
                        网址
                    </td>
                    <td class="oddrow-l">
                        <asp:Label ID="txtContactWebsite" runat="server" />
                    </td>
                    <td class="oddrow">
                        Email:
                    </td>
                    <td class="oddrow-l">
                        <asp:Label ID="txtContactEmail" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        邮编:
                    </td>
                    <td class="oddrow-l">
                        <asp:Label ID="txtPostCode" runat="server" />
                    </td>
                    <td class="oddrow">
                        默认税率:
                    </td>
                    <td class="oddrow-l">
                        <asp:Label ID="txtTaxRate" runat="server" Width="200px" />
                </tr>
                <tr>
                    <td class="oddrow">
                        所在地区:
                    </td>
                    <td class="oddrow-l">
                        <asp:Label ID="lblArea" runat="server" />
                    </td>
                    <td class="oddrow">
                        所属行业:
                    </td>
                    <td class="oddrow-l">
                        <asp:Label ID="lblIndustry" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
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
                                    />
                                <asp:BoundField DataField="FrameContractCode" HeaderText="协议号" ItemStyle-HorizontalAlign="Center"
                                     />
                                                                    <asp:TemplateField HeaderText="备注" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <span title="<%# Eval("Description") %>"><%# Eval("Description").ToString().Length<=100 ? Eval("Description").ToString() : Eval("Description").ToString().Substring(0,100)+"..." %></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="附件" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Center">
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
        <td colspan="4" align="center">
            <asp:Button ID="btnBack" runat="server" Text=" 返回 " CausesValidation="false" CssClass="widebuttons"
                OnClick="btnBack_Click" />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="false"
                ShowMessageBox="true" DisplayMode="bulletList" />
        </td>
    </tr>
    <tr>
        <td style="height: 10px">
            &nbsp;
        </td>
    </tr>
    </table>
</asp:Content>
