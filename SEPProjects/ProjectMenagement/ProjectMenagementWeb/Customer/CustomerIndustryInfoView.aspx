<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" Inherits="Customer_CustomerIndustryInfoView" Codebehind="CustomerIndustryInfoView.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<link href="/public/css/dialog.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" language="javascript" src="/public/js/jquery-1.2.6.js"></script>
    <script type="text/javascript" language="javascript" src="/public/js/dialog.js"></script>
    
    <table style="width: 100%">
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="heading" colspan="4">行业类型信息查看
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 15%">行业代码:
                        </td>
                        <td class="oddrow-l" style="width: 35%"><asp:Label ID="txtCode" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 15%">行业名称:</td>
                        <td class="oddrow-l" style="width: 35%"><asp:Label ID="txtName" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow">描述</td>
                        <td class="oddrow-l"  colspan="3"><asp:TextBox ID="txtNote" runat="server" Width="70%" Height="60px" runat="server" TextMode="MultiLine" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Button ID="btnBack" runat="server" Text=" 返回 "
                        CausesValidation="false" CssClass="widebuttons" OnClick="btnBack_Click" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="false"
                    ShowMessageBox="true" DisplayMode="bulletList" />
            </td>
        </tr>
        <tr>
            <td style="height:10px">&nbsp;</td>
        </tr>
    </table>
</asp:Content>