<%@ Page Language="C#" AutoEventWireup="true" Inherits="Purchase_Message_NewPost" MasterPageFile="~/MasterPage.master"  Codebehind="NewPost.aspx.cs" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript">
    function verify() {
        str1 = document.getElementById("<%= txtSubject.ClientID %>");
        str2 = document.getElementById("<%= txtBody.ClientID %>");
        var msg = "";
        if (str1.value.length <= 0 || str2.value.length <= 0) {
            msg += "主题和内容不能为空";
        }
        else if (str1.value.length > 50) {
            msg += "主题长度超标";
        }
        else if (str2.value.length > 3900) {
        msg += "内容长度超标";
    }
    if (msg != "") {
        alert(msg);
        return false;
    }
        
    }
</script>
    <div>
        <table width="100%" border="0" class="tableForm">
                    <tr>
                        <td class="heading" colspan="2">
                            发布公告</td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width:20%">主题：</td>
                        <td  class="oddrow" ><asp:TextBox ID="txtSubject" runat="server" Width="71%" MaxLength="50"></asp:TextBox>                            
                        </td>
                    </tr> 
                    <tr>
                        <td class="oddrow" style="width:20%">所属地区：</td>
                        <td class="oddrow"><asp:DropDownList ID="drpArea" runat="server" Width="71%"></asp:DropDownList></td>
                    </tr>                   
                    <tr>
                    <td class="oddrow" style="width:20%">内容：</td>
                        <td  class="oddrow-l" >
                            
                            <asp:TextBox id="txtBody" TextMode="MultiLine" Height="200px" runat="server" Width="71%"></asp:TextBox>                            
                        </td>
                    </tr>
                    <tr>
                    <td class="oddrow" style="width:20%">附件：</td>
                        <td class="oddrow-l">
                            <asp:FileUpload ID="fil" runat="server" /><asp:Literal ID="labdown" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="right">
                            <asp:Button ID="btnSend"  OnClick="btnSend_Click" OnClientClick="return verify();"  CssClass="widebuttons" Text="确定" runat="server" />
                            <asp:Button ID="btnBack" OnClick="btnBack_Click" CssClass="widebuttons" Text="返回" runat="server" />
                        </td>
                    </tr>
                </table>
    </div>
</asp:Content>
