<%@ Page Title="记者信息" Language="C#" MasterPageFile="~/Reporter.Master" AutoEventWireup="true" CodeBehind="ReporterInfo.aspx.cs" Inherits="MediaWeb.newReporter.ReporterInfo" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script language="javascript">
    function changedView(col) {
       
        if (document.getElementById("trEdit").style.display == "none") {
            document.getElementById("trEdit").style.display = "block";
            document.getElementById("trView").style.display = "none";
            col.innerHTML = "取消";
        }
        else {
            document.getElementById("trEdit").style.display = "none";
            document.getElementById("trView").style.display = "block";
            col.innerHTML = "修订";
        }
    }
</script>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="570" valign="top"><table border="0" cellpadding="0" cellspacing="10" bgcolor="#FFFFFF">
          <tr>
            <td><a href="#"></a>
              <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                  <td width="1"><a href="#"><asp:Image ID="imgPic" runat="server" Width="190px" Height="130px" /></td>
                  <td style="padding-left:20px;"><span class="fontsize-36"><strong><asp:Label ID="labName" runat="server" /></strong></span><br />                      <span class="fontsize-20">所属媒体：<asp:LinkButton ID="lnkMediaName" runat="server" /></span></td>
                </tr>
              </table></td>
          </tr>
          <tr>
            <td width="550" bgcolor="#fafafa" style="line-height:22px; border:1px solid #e6e6e6;"><table width="100%" border="0" cellpadding="5" cellspacing="0" class="fontsize-12">
              <tr>
                <td width="100" style="border-bottom:1px dotted #CCC;"><strong>性别：</strong></td>
                <td style="border-bottom:1px dotted #CCC;"><asp:Label ID="labSex" runat="server" />&nbsp;</td>
              </tr>
              <tr>
                <td style="border-bottom:1px dotted #CCC;"><strong>职务：</strong></td>
                <td style="border-bottom:1px dotted #CCC;"><asp:Label ID="labreporterposition" runat="server" />&nbsp;</td>
              </tr>
              <tr>
                <td style="border-bottom:1px dotted #CCC;"><strong>负责领域：</strong></td>
                <td style="border-bottom:1px dotted #CCC;"><asp:Label ID="labresponsibledomain" runat="server" />&nbsp;</td>
              </tr>
              <tr>
                <td style="border-bottom:1px dotted #CCC;"><strong>MSN：</strong></td>
                <td style="border-bottom:1px dotted #CCC;"><asp:Label ID="labMsn" runat="server"></asp:Label>&nbsp;</td>
              </tr>
              <tr>
                <td style="border-bottom:1px dotted #CCC;"><strong>QQ：</strong></td>
                <td style="border-bottom:1px dotted #CCC;"><asp:Label ID="labQq" runat="server"></asp:Label>&nbsp;</td>
              </tr>
              <tr>
                <td style="border-bottom:1px dotted #CCC;"><strong>邮箱：</strong></td>
                <td style="border-bottom:1px dotted #CCC;"><asp:Label ID="labEmailOne" runat="server" />&nbsp;</td>
              </tr>
              <tr>
                <td style="border-bottom:1px dotted #CCC;"><strong>手机：</strong></td>
                <td style="border-bottom:1px dotted #CCC;"><asp:Label ID="labUsualMobile" runat="server" />&nbsp;</td>
              </tr>
              <tr>
                <td style="border-bottom:1px dotted #CCC;"><strong>固话：</strong></td>
                <td style="border-bottom:1px dotted #CCC;"><asp:Label ID="labOfficePhone" runat="server" />&nbsp;</td>
              </tr>
              <tr>
                <td style="border-bottom:1px dotted #CCC;"><strong>传真：</strong></td>
                <td style="border-bottom:1px dotted #CCC;"><asp:Label ID="lblFax" runat="server" />&nbsp;</td>
              </tr>
              <tr>
                <td style="border-bottom:1px dotted #CCC;"><strong>其他通讯方式：</strong></td>
                <td style="border-bottom:1px dotted #CCC;"><asp:Label ID="labOtherMessageSoftware" runat="server"></asp:Label>&nbsp;</td>
              </tr>
              <tr>
                <td style="border-bottom:1px dotted #CCC;"><strong>办公地址：</strong></td>
                <td style="border-bottom:1px dotted #CCC;"><asp:Label ID="lblOfficeAddress" runat="server" />&nbsp;</td>
              </tr>
               <tr>
                <td colspan="2" class="fontsize-14 fontcolor-black" style="padding-top:15px; border-top:1px dotted #CCC;"><strong><img src="/images/histicon.gif" width="15" height="15" / style="margin:0 5px -2px 0;"><asp:Label ID="lblHist" runat="server" /></strong></td>
              </tr>
              <tr>
                <td colspan="2">
                    <asp:Button ID="btnEdit" runat="server" CssClass="widebuttons" style="background-image:url(/images/btn-bgwhite.jpg);" BorderWidth="0" width="71" height="32" OnClick="btnEdit_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnBack" runat="server" CssClass="widebuttons" style="background-image:url(/images/btn-return.gif);" BorderWidth="0" width="71" height="32" OnClick="btnBack_Click" />
                </td>
              </tr>
            </table></td>
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
                      <td align="right"><a href="#" onclick="changedView(this);" class="fontcolor-white">修订</a></td>
                    </tr>
                  </table></td>
              </tr>
              <tr id="trView">
                <td style="color:#dbdbdb; line-height:20px; padding-bottom:15px;" class="fontsize-12">
                    <asp:Label ID="labEvaluation" runat="server" />
                </td>
              </tr>
              <tr id="trEdit" style="display:none;">
                <td>
                    <CKEditor:CKEditorControl ID="txtEvaluation" runat="server" Height="200" ></CKEditor:CKEditorControl><br />
                    <span class="fontsize-14" style="color:White; font-weight:bold;">修订原因</span><br /><asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" Height="80px" Width="99%" />
                    <br />
                    <asp:Button ID="btnSave" runat="server" width="71" height="32" style="background-image:url(/images/baocun.jpg);border:0px;" OnClick="btnSave_Click" />
                </td>
              </tr>
              <tr>
                <td class="fontsize-14 fontcolor-white" style="padding-top:15px; border-top:1px dotted #CCC;"><strong><img src="/images/ico-1.gif" width="15" height="15" / style="margin:0 5px -2px 0;"><asp:Label ID="labEditInfo" runat="server" /></strong></td>
              </tr>
            </table></td>
          </tr>
        </table></td>
      </tr>
    </table>
</asp:Content>
