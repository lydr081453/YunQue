<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebSiteEdit.aspx.cs" Inherits="SEPAdmin.WebSiteManagement.WebSiteEdit"
    MasterPageFile="~/MainMaster.Master" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server" ID="up">
        <ContentTemplate>
            <asp:Label runat="server" ID="lblEditTitle" Text="编辑站点" />
            <asp:Label runat="server" ID="lblAddTitle" Text="创建站点" />
            <table style="width: 100%">
                <tr>
                    <td valign="top" class="oddrow">
                        站点ID
                    </td>
                    <td valign="top" class="oddrow-l">
                        <asp:TextBox runat="server" ID="txtWebSiteID" MaxLength="256" ReadOnly="true" />
                    </td>
                </tr>
                <tr>
                    <td valign="top" class="oddrow">
                        名称
                    </td>
                    <td valign="top" class="oddrow-l">
                        <asp:TextBox runat="server" ID="txtWebSiteName" MaxLength="256" />
                        <asp:RequiredFieldValidator ID="rfvWebSiteName" runat="server" ControlToValidate="txtWebSiteName"
                            ErrorMessage="名称不可为空。" Text="*" />
                        <act:ValidatorCalloutExtender runat="server" ID="vceWebSiteName" TargetControlID="rfvWebSiteName" />
                    </td>
                </tr>
                <tr>
                    <td valign="top" class="oddrow">
                        描述
                    </td>
                    <td valign="top" class="oddrow-l">
                        <asp:TextBox runat="server" ID="txtDescription" MaxLength="256" />
                    </td>
                </tr>
                <tr>
                    <td valign="top" class="oddrow">
                        根地址
                    </td>
                    <td valign="top" class="oddrow-l">
                        <asp:TextBox runat="server" ID="txtUrlPrefix" />
                        <asp:RequiredFieldValidator ID="rfvUrlPrefix" runat="server" ControlToValidate="txtUrlPrefix"
                            ErrorMessage="根地址不可为空。" Text="*" />
                        <asp:RegularExpressionValidator runat="server" ID="revUrlPrefix" ControlToValidate="txtUrlPrefix"
                            Display="None" ErrorMessage="无效的 URL 地址，必须符合以下格式：subdomain.domain/path/subpath"
                            ValidationExpression="^([a-zA-Z0-9_\-]+)(\.[a-zA-Z0-9_\-]+)*(\:[1-9]\d+)?(/[a-zA-Z0-9_\-]+)*(/)?$" />
                        <act:ValidatorCalloutExtender runat="server" ID="vceUrlPrefix" TargetControlID="revUrlPrefix" />
                        <act:ValidatorCalloutExtender runat="server" ID="vce_rfvUrlPrefix" TargetControlID="rfvUrlPrefix" />
                    </td>
                </tr>
                <tr>
                    <td valign="top" class="oddrow">
                        排序
                    </td>
                    <td valign="top" class="oddrow-l">
                        <asp:TextBox runat="server" ID="txtOrdinal" Text="0" />
                        <asp:Button runat='server' ID="btnOrdinalUp" Text="↑" Style="display: none" />
                        <asp:Button runat='server' ID="btnOrdinalDown" Text="↓" Style="display: none" />
                        <act:NumericUpDownExtender TargetButtonDownID="btnOrdinalDown" TargetButtonUpID="btnOrdinalUp"
                            TargetControlID="txtOrdinal" runat="server" Step="1" Maximum="32767" Minimum="0" />
                    </td>
                </tr>
                <tr>
                    <td valign="top" class="oddrow">
                        框架页面路径
                    </td>
                    <td valign="top" class="oddrow-l">
                        <div>
                            <asp:TextBox runat="server" ID="txtFramePage" Text="" Width="350px" />
                        </div>
                    </td>
                </tr>
            </table>
            <div>
                <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" CssClass="widebuttons" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button runat="server" ID="btnCancel" Text="返回" OnClick="btnCancel_Click" CausesValidation="false"
                    CssClass="widebuttons" />
            </div>
            <div>
                <asp:Label runat="server" ID="lblMessage" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
