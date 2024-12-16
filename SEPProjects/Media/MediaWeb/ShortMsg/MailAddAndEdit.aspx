<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    Inherits="ShortMsg_MailAddAndEdit" ValidateRequest="false" Codebehind="MailAddAndEdit.aspx.cs" %>
<%@ Register TagPrefix="ucWE" Namespace="MyControls.MyEditor" Assembly="MyControls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">
    <script type="text/javascript">
     function check(){
     var meg="";
     //主题
         if (document.getElementById("<% = txtSubject.ClientID %>").value =="")
            {
            meg += "主题不能为空！"+"\n";

            }
            if (document.getElementById("<% = wtpNew.ClientID %>").value =="")
            {
            meg += "内容不能为空！"+"\n";

            }
        if(meg!="")
        {
        alert(meg);
         return  false;
        }
           
                   
    }
      
    function showAnnex(){
	var td = document.getElementById("annex");
	td.innerHTML = '<asp:FileUpload ID="updateAnnex" runat="server" Width="80%" unselectable="on" />';
    }
     
    </script>



    <table style="width: 100%;" border="1" class="tableForm">
        <tr>
            <td colspan="5" class="heading">
                <asp:Label ID="labHeading" runat="server"> 新建邮件</asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 20%;" class="oddrow">
                主题：
            </td>
            <td colspan="3" class="oddrow-l">
                <asp:TextBox ID="txtSubject" runat="server" Width="80%" MaxLength="25"></asp:TextBox><font color="red"> *</font>
            </td>
        </tr>         
        <tr>
            <td style="width: 20%;" class="oddrow"> 
                <input id="Button1" type="button" value="添加附件" onclick="showAnnex()" class="widebuttons"  runat="server"/></td>
            <td colspan="3" id="annex" class="oddrow"></td>
        </tr>
        <tr>
            <td style="width: 20%;" class="oddrow" colspan="4">
                内容：
            </td>
        </tr>
         <tr>
             <td style="width:70%;" colspan="4"><ucWE:WebTextPane ID="wtpNew" runat="server" Width="100%" Height="240px" BackColor="white"></ucWE:WebTextPane></td>
        </tr>
    </table>
    <table style="width:100%" border="0">
        <tr>
            <td colspan="4" style="text-align: right">
                <asp:Button ID="btnOk" runat="server" OnClientClick="return check();" CssClass="widebuttons"
                    Text="保存" OnClick="btnOk_Click" />
                <input type="reset" class="widebuttons" value="重置" />
                <asp:Button ID="btnBack" Text="返回" OnClick="btnBack_Click" runat="server" CssClass="widebuttons"></asp:Button>
            </td>
        </tr>
    </table>
</asp:Content>
