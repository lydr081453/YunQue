<%@ Page Language="C#" AutoEventWireup="true" Inherits="Purchase_Message_MessageView" MasterPageFile="~/MasterPage.master" Codebehind="MessageView.aspx.cs" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <table width="100%" border="0" class="tableForm">
                    <tr>
                        <td class="heading" colspan="2">
                            发布公告</td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width:20%">主题：</td>
                        <td  class="oddrow" ><asp:Label ID="labSubject" runat="server" Width="71%" MaxLength="50"></asp:Label>                            
                        </td>
                    </tr> 
                    <tr>
                        <td class="oddrow" style="width:20%">所属地区</td>
                        <td class="oddrow"><asp:Label ID="labArea" runat="server" Width="71%"/></td>
                    </tr>                   
                    <tr>
                    <td class="oddrow" style="width:20%">内容：</td>
                        <td  class="oddrow-l" >
                            
                            <asp:Label id="labBody" TextMode="MultiLine" Height="200px" runat="server" Width="510px"></asp:Label>                            
                        </td>
                    </tr>
                                        <td class="oddrow" style="width:20%">附件：</td>
                        <td class="oddrow-l">
                            <asp:Literal ID="labdown" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="right">                            
                            <asp:Button ID="btnBack" OnClick="btnBack_Click" CssClass="widebuttons" Text="返回" runat="server" />
                        </td>
                    </tr>
                </table>
    </div>
</asp:Content>

