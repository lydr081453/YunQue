<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProvinceEdit.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="MediaWeb.NewMedia.BaseData.ProvinceEdit" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentHead" runat="server">
    <link href="../css/demos.css" type="text/css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMain" runat="server">
    <asp:UpdatePanel ID="Panel" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td width="25%">
                        所属国家:
                    </td>
                    <td width="35%">
                        <div style="margin-bottom: 15px;">
                            <ComponentArt:ComboBox ID="ddlCountry" runat="Server" Width="192" Height="20" AutoHighlight="false"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_OnSelectedIndexChanged"
                                AutoComplete="true" AutoFilter="true" DataTextField="CountryName" DataValueField="CountryID"
                                TextBoxCssClass="txt" DropHoverImageUrl="../images/ddn-hover.png" DropImageUrl="../images/ddn.png"
                                DropDownResizingMode="bottom" DropDownWidth="190" DropDownHeight="300" DropDownCssClass="ddn"
                                DropDownContentCssClass="ddn-con">
                                <DropDownFooter>
                                    <div class="ddn-ftr">
                                    </div>
                                </DropDownFooter>
                            </ComponentArt:ComboBox>
                        </div>
                    </td>
                    <td width="25%">
                        省份/直辖市:
                    </td>
                    <td width="35%">
                        <asp:TextBox ID="txtProvince_Name" runat="server" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Button ID="btnSearch" OnClick="btnSave_Click" runat="server" Text="保存并返回" />&nbsp;&nbsp;<asp:Button ID="btnAdd" OnClick="btnReturn_Click" runat="server" Text=" 返  回 " />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>