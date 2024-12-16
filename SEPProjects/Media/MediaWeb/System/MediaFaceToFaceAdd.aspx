<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="System_MediaFaceToFaceAdd" Title="添加媒介面对面" EnableEventValidation="false" Codebehind="MediaFaceToFaceAdd.aspx.cs" %>

<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<%@ Register TagPrefix="cc2" Namespace="MyControls" Assembly="MyControls" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="contentHead" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">
    <input type="hidden" id="hidClientId" runat="server" value="0" /><input type="hidden"
        id="hidUrl" runat="server" />
    <table width="100%">
        <tr>
            <td>
                <table width="100%" border="0" class="tableForm">
                    <tr>
                        <td class="heading" colspan="4">
                            添加媒介面对面
                        </td>
                    </tr>
                     <tr>
                        <td style="width: 20%" class="oddrow">
                            期号：
                        </td>
                        <td class="oddrow-l" >
                            第<asp:TextBox ID="txtCycle" runat="server" Width="10%"></asp:TextBox>期
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%" class="oddrow">
                            标题：
                        </td>
                        <td class="oddrow-l" colspan="3">
                            <asp:TextBox ID="txtSubject" runat="server" Width="80%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 20%">
                            选择文件：
                        </td>
                        <td colspan="3" class="oddrow-l">
                            <asp:FileUpload ID="fplTitle" runat="server" Width="80%" Height="24px" unselectable="on" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%" class="oddrow">
                            备注：
                        </td>
                        <td class="oddrow-l" colspan="3">
                            <asp:TextBox ID="txtRemark" TextMode="MultiLine" Height="137px" runat="server" Width="80%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Button ID="btnOk" runat="server" Text="保存" OnClick="btnOk_Click" CssClass="widebuttons" />
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="0" class="tableForm">
                    <tr>
                        <td class="headinglist" colspan="3">
                            媒介面对面往期列表
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="3">
                            <cc4:MyGridView ID="dgList" runat="server" OnRowDataBound="dgList_RowDataBound" DataKeyNames="path">
                            </cc4:MyGridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
