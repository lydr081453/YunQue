<%@ Page Language="C#" AutoEventWireup="true" Inherits="System_NoUploadReporterWarning" MasterPageFile="~/MasterPage.master" Codebehind="NoUploadReporterWarning.aspx.cs" %>

<asp:Content ID="cntHead" ContentPlaceHolderID="contentHead" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">    
    <table style="width: 100%;" >    
    
        <tr>
            <td style="height:10px"></td>
        </tr>
        <tr>
            <td colspan="4">
                <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" align="center">
                    <tr>
                        <td style="background-image: url(/images/left.png);height:28px;width:4px">&nbsp;</td>
                        <td style="background-color: #f4be57;width:100%;height:28px">&nbsp;&nbsp;&nbsp;&nbsp;<img src="/images/g_03.png" border="0" style="vertical-align: bottom" alt="" /><img src="/images/g_04.png" border="0" style="vertical-align: bottom" />&nbsp;<label style="font-weight: bold;font-size: 14px;vertical-align:bottom" alt="">还未上传的简报</label></td>
                        <td style="background-image: url(/images/right.png);height:28px;width:4px">&nbsp;</td>
                    </tr>
                 </table>
                 <table style="width: 70%;" border="0" cellpadding="0" cellspacing="0" align="center">
                     <tr>
                        <td height="20"></td>
                    </tr>                     
                    <tr>
                        <td colspan="4" style="text-align: center">                            
                            <asp:PlaceHolder ID="phNoUploadReporterList" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>        
        <tr>
            <td style="height:10px"></td>
        </tr>
        <tr>
            <td colspan="4">            
                <table style="width: 70%;" border="0" cellpadding="0" cellspacing="0" align="center">                    
                    <tr>
                        <td align="right" colspan="4">
                            
                            <asp:Button ID="btnNoUpload" Text="稍后上传" runat="server" OnClientClick="window.close();return false;"  class="widebuttons" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

</asp:Content>
