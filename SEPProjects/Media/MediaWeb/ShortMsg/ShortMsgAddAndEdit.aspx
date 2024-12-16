<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/MasterPage.master" Inherits="ShortMsg_ShortMsgAddAndEdit" Codebehind="ShortMsgAddAndEdit.aspx.cs" %>

<asp:Content ID="cntHead" ContentPlaceHolderID="contentHead" runat="server">

    <script type="text/javascript">
     function check(){
     var meg="";
     //主题
         if (document.getElementById("<% = txtSubject.ClientID %>").value =="")
            {
              meg += "主题不能为空！"+"\n";
             
            }
            //内容
         if (document.getElementById("<% = txtBody.ClientID %>").value =="")
            {
             meg +="内容不能为空！"+"\n";
              
            }
          if(meg!=""){
          alert(meg);
          return false;
          }  
      }
    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">
    <table style="width: 100%;" border="1" class="tableForm">
        <tr>
            <td colspan="4" class="heading">
                <asp:Label ID="labHeading" runat="server"> 新建短消息</asp:Label>
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
            <td class="oddrow">
                内容：
            </td>
            <td colspan="3" class="oddrow-l">
                <asp:TextBox ID="txtBody" runat="server" TextMode="MultiLine" Width="80%" 
                    Height="98px" MaxLength="250"></asp:TextBox><font
                    color="red"> *</font>
            </td>
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
