<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" Title="发送短信" Inherits="ShortMsg_SendShortMsg" Codebehind="SendShortMsg.aspx.cs" %>

<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<%@ Register TagPrefix="cc2" Namespace="MyControls" Assembly="MyControls" %>
<%@ Register TagPrefix="uc" TagName="Experience" Src="~/Media/skins/Experience.ascx" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="contentHead" runat="server">

    <script type="text/javascript">
    
function selectone() {
            var Element = document.getElementsByName("chkRep");
            hidChkID = document.getElementById("<% = hidChkID.ClientID %>");
            hidChkID.value = "";
            var ids = "";
            for (var j = 0; j < Element.length; j++) {
                if (Element[j].checked) {
                    ids += Element[j].value + ",";
                    hidChkID.value += Element[j].value + ",";
                }
            }
            ids = ids.substring(0, ids.length - 1);
            hidChkID.value = ids;
            if (ids != "") {
                return ids;
            } else {
                alert("请选择记者");
                return false;
            }
        }
        function selectedcheck(parent, sub) {
            var chkSelect = document.getElementById("chk" + parent);
            var elem = document.getElementsByName("chk" + sub);
            for (i = 0; i < elem.length; i++) {
                if (elem[i].type == "checkbox") {
                    elem[i].checked = chkSelect.checked;
                }
            }
        }
        
        function check()
        {
        
            //内容
            var meg="";
            selectone();
            if( document.getElementById("<% =hidChkID.ClientID %>").value == "0")
            {
                meg += "请选择要发送的记者！";
              
            }
         if (document.getElementById("<% = txtBody.ClientID %>").value =="")
            {
                meg += "短消息内容不能为空！"+"\n";
                
            }

            if( document.getElementById("<% =hidChkID.ClientID %>").value == "0")
            {
                 meg += "请选择要发送的手机号码！";
               
            }
         if( meg !="")
            {
                alert(meg);
             return false;
            }
        }
       function   WinOpen()   
      {   
      window.open("SendShortMsgList.aspx","选择邮件","<%=ESP.Media.Access.Utilities.Global.OpenClass.Common %>");    
      }   
      
      function mail()
      {
      str = document.getElementById('<% =hidPJID.ClientID %>').value;
      if(document.getElementById("<% =txtSubject.ClientID %>").value != "" && document.getElementById("<% =txtBody.ClientID %>").value != "")
      {
        
        if(confirm("是否保存编辑邮件？")==false)
			{

			    window.open("/Media/ReporterAddAndEdit.aspx?alert=1&Operate=ADD&Mid=0&<%=RequestName.ProjectID %>=" + str, "添加新记者", "<%=ESP.Media.Access.Utilities.Global.OpenClass.Common %>");
				return false;
			}
			else
			{

			    window.open("/Media/ReporterAddAndEdit.aspx?alert=1&Operate=ADD&Mid=0&<%=RequestName.ProjectID %>=" + str, "添加新记者", "<%=ESP.Media.Access.Utilities.Global.OpenClass.Common %>");
				
			}
      }
      else{
          window.open("/Media/ReporterAddAndEdit.aspx?alert=1&Operate=ADD&Mid=0&<%=RequestName.ProjectID %>=" + str, "添加新记者", "<%=ESP.Media.Access.Utilities.Global.OpenClass.Common %>");
        return false;
      }
      
      }
      
      function btnReporterSign_ClientClick(fn) {
            if (selectone()) {
            hidChkID = document.getElementById("<% = hidChkID.ClientID %>");
            window.open('/DownLoad/SendShortMsg.aspx?ExportType=sign&FileName=' + fn + '&Term=' + hidChkID.value, "");
                return false;
            }
            else return false;
        }
        function btnReporterContact_ClientClick(fn) {
            if (selectone()) {
                window.open('/DownLoad/SendShortMsg.aspx?ExportType=contact&FileName=' + fn + '&Term=' + hidChkID.value, "");
                return false;
            }
            else return false;
        }
    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">
    <input type="hidden" value="0" runat="server" id="hidChkID" />
    <input type="hidden" value="0" runat="server" id="hidShortMsgID" />
    <input type="hidden" value="0" runat="server" id="hidPJID" /> 
    <table width="100%">
        <tr>
            <td>
                <table width="100%" border="1" class="tableForm">
                    <tr>
                        <td colspan="4" class="heading">
                            发送短信
                        </td>
                    </tr>
                    <tr>
                        <td  class="oddrow-l" Width="20%">
                            短信主题：
                        </td>
                        <td class="oddrow-l" colspan="3">
                            <asp:TextBox ID="txtSubject" runat="server" Width="70%" MaxLength="50"></asp:TextBox>
                             <asp:Button ID="btnSelect" runat="server" Text="选择" Width="83px" CssClass="widebuttons"
                                OnClientClick="WinOpen();return false;" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow-l" valign="middle">
                            短信内容：</td>
                        <td colspan="3" class="oddrow-l">
                            <asp:TextBox ID="txtBody" runat="server" Width="70%" Height="100"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 30">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="1" class="tableForm">
                    <tr>
                        <td colspan="5" class="heading">
                            搜索记者：
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="oddrow-l">
                            记者名：
                            <asp:TextBox ID="txtReporter" runat="server" Width="40%"></asp:TextBox>
                        </td>
                        <td colspan="2" class="oddrow-l">
                            所属媒体：<asp:TextBox ID="txtMedia" runat="server" Width="40%"></asp:TextBox>
                        </td>
                        <td class="oddrow">
                            <asp:Button ID="btnSerach" runat="server" Text="搜索" Width="83px" CssClass="widebuttons"
                                OnClick="btnSearch_Click" />
                            <asp:Button ID="Button3" runat="server" CssClass="widebuttons" Text="返回总库" CausesValidation="true" OnClick="btnClear_OnClick" />                         
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 30">
            <td>
            </td>
        </tr>
        <tr>
            <td>
            <table class="tablehead">
            <tr>
                <td style="display:none;">
                <img src="/images/add.gif" border="0" style="vertical-align: bottom" />&nbsp;<asp:LinkButton
                    ID="btnAddReporter" runat="server" Text="添加新记者" class="bigfont" OnClick="SaveMsg" OnClientClick="return mail();" Visible="false"/>
                    </td>
                </tr>
                </table>
                <table width="100%" border="0">
                    <tr>
                        <td colspan="4" style="height:10px">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="headinglist">
                            记者列表
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <cc4:MyGridView ID="dgList"  runat="server" OnRowDataBound="dgList_RowDataBound">
                            </cc4:MyGridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="right">                            
                            <asp:Button ID="btnReporterSign" runat="server" CssClass="widebuttons" Text="生成签到表" OnClick="btnReporterSign_Click" />
                            <asp:Button ID="btnReporterContact" runat="server" CssClass="widebuttons" Text="生成通联表" OnClick="btnReporterContact_Click" />
                            <asp:Button ID="btnSend" runat="server" Text="发送" Width="83px" CssClass="widebuttons"
                                OnClientClick="return check();" OnClick="btnSend_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
