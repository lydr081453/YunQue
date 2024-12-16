<%@ Page Title="修订历史" Language="C#" MasterPageFile="~/Reporter.Master" AutoEventWireup="true" CodeBehind="EvaluationLog.aspx.cs" Inherits="MediaWeb.newReporter.EvaluationLog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td width="70"><img src="/images/ico-3.gif" width="64" height="62" /></td>
          <td class="fontsize-30">修订历史</td>
        </tr>
      </table>
        <br />
      <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="270" valign="top"><table border="0" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
          <tr>
            <td width="250" class="fontsize-12 lineheiht" style="padding:30px 20px;"><strong>修订人：<asp:Literal ID="litUser" runat="server" /></strong><br />
              修订时间：<asp:Literal ID="litDate" runat="server" /></td>
          </tr>
        </table>
          </td>
        <td valign="top" style="padding-left:10px;"><table width="100%" border="0" cellpadding="0" cellspacing="0">
          <tr>
            <td width="1" valign="top"><img src="/images/corner.gif" width="5" height="13" vspace="20" /></td>
            <td bgcolor="#858585" style="padding:10px;"><table width="100%" border="0" cellspacing="0" cellpadding="5">
              <tr>
                <td height="30" class="fontsize-12 fontcolor-white lineheiht"><span style="float:right;"><a href="#" class="fontcolor-white"></a></span>
                  <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                      <td><span class="fontsize-14"><strong><img src="/images/ico-2.gif" width="14" height="13" / style="margin:0 5px -2px 0;">评价：</strong></span></td>
                      <td align="right"><a href="#" class="fontcolor-white">&nbsp;</a></td>
                    </tr>
                  </table></td>
              </tr>
              <tr>
                <td style="color:#dbdbdb; line-height:20px; padding-bottom:15px;" class="fontsize-12"><asp:Label ID="labEvaluation" runat="server" /></td>
              </tr>
              <tr>
                <td class="fontsize-14 fontcolor-white" style="padding-top:15px; border-top:1px dotted #CCC;"><strong><img src="/images/ico-1.gif" width="15" height="15" / style="margin:0 5px -2px 0;">修订原因：</strong></td>
              </tr>
              <tr>
                <td style="color:#dbdbdb; line-height:20px;" class="fontsize-12"><asp:Label ID="labReason" runat="server" /></td>
              </tr>
            </table></td>
          </tr>
        </table>
          <br />
          
          <asp:Button ID="btnBack" runat="server" CssClass="widebuttons" style="background-image:url(/images/btn-return.gif);" BorderWidth="0" width="71" height="32" OnClick="btnBack_Click" /><br />
          <br /></td>
      </tr>
    </table>
</asp:Content>
