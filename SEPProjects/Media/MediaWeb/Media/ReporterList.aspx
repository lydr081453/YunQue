<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Media_ReporterList" Title="记者列表" Codebehind="ReporterList.aspx.cs" %>

<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<%@ Register TagPrefix="uc" TagName="Experience" Src="~/Media/skins/Experience.ascx" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="contentHead" runat="server">

    <script type="text/javascript">
       function check()
        {
             var meg="";

             
//           if(document.getElementById("<% =txtIdCard.ClientID %>").value!="")
//           {
//               if (document.getElementById("<% =txtIdCard.ClientID %>").value.search(/^\d{15}(\d{2}[A-Za-z0-9])?$/) == -1)
//                {
//                    meg += "身份证输入错误"+"\n";
//       
//                }
//            }
//            if(document.getElementById("<% =txtEmail.ClientID %>").value!=""){    
//                if (document.getElementById("<% =txtEmail.ClientID %>").value.search(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/) == -1)
//                {
//                    meg += "邮箱输入错误"+"\n";
//             
//                }
//            }
//            if(document.getElementById("<% =txtMobile.ClientID %>").value!=""){
//                if (document.getElementById("<% =txtMobile.ClientID %>").value.search(/^((\(\d{2,3}\))|(\d{3}\-))?13\d{9}$/) == -1)
//                {
//                   meg += "手机号码输入错误"+"\n";
//             
//                }
//            }
           
            if(meg!=""){
            alert(meg);
             return  false;
            }
        }
        
        function selected(obj)
        {
            if (obj.checked)
            {
                hide = document.getElementById("<% =hidChecked.ClientID %>");
                str = obj.value + ",";
                hide.value = hide.value.replace(str,"");
                hide.value += str;
            }
        }
       function   WinOpen()   
      {   
      window.open("ReporterSelectMedia.aspx","选择媒体","<%= ESP.Media.Access.Utilities.Global.OpenClass.Common%>");    
      }
      
      //转向邮件页
        function   MailOpen()   
      {          
        MID=document.getElementById("<% = hidChkID.ClientID %>");
        selectone();
        if(MID.value == "")return false;
        window.open("../ShortMsg/SendMail.aspx?MID=" + MID.value +"&action=Media","发送邮件","<%=ESP.Media.Access.Utilities.Global.OpenClass.Common %>"); 
      } 
      //转向短信页
      function   MsgOpen()   
      {  
      selectone();
      if(hidChkID.value == "")return false;      
      MID=document.getElementById("<% = hidChkID.ClientID %>");
    
      window.open("../ShortMsg/SendShortMsg.aspx?MID=" + MID.value +"&action=Media","发送短信","<%=ESP.Media.Access.Utilities.Global.OpenClass.Common %>"); 
      } 
      //选择媒体
      function selectone()
        {
        var   Element=document.getElementsByName("chkRep");
        hidChkID=document.getElementById("<% = hidChkID.ClientID %>");
        hidChkID.value = "";
        var ids = "";
        for(var  j=0;j<Element.length;j++)   
        {  
            if(Element[j].checked)   
            { 
                ids += Element[j].value + ","; 
                hidChkID.value += Element[j].value + ",";
            }
         }
         ids = ids.substring(0,ids.length-1);
         hidChkID.value = ids;
         if(ids != ""){
            return ids;
         }else{
            alert("请选择媒体");
            return false;
         }
        }
    function selectedcheck(parent,sub)
    {   
    var chkSelect = document.getElementById("chk"+parent);
    var elem = document.getElementsByName("chk"+sub);
    for(i=0;i<elem.length;i++)
    {
        if(elem[i].type == "checkbox")
        {
            elem[i].checked = chkSelect.checked;
        }
    } 
    }
    function btnReporterSign_ClientClick(fn) {
            if (selectone()) {
            hidChkID = document.getElementById("<% = hidChkID.ClientID %>");
            window.open('/DownLoad/ReporterList.aspx?ExportType=sign&FileName=' + fn + '&Term=' + hidChkID.value, "");
                return false;
            }
            else return false;
        }
        function btnReporterContact_ClientClick(fn) {
            if (selectone()) {
                window.open('/DownLoad/ReporterList.aspx?ExportType=contact&FileName=' + fn + '&Term=' + hidChkID.value, "");
                return false;
            }
            else return false;
        }
    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">
    <input type="hidden" id="hidChecked" runat="server" value="" />
    <input type="hidden" id="hidMediaId" runat="server" value="0" />
    <%--    <table class="tablehead">
        <tr>
            <td>
    <img src="/images/add.gif" border="0" style="vertical-align: bottom" />&nbsp;<asp:LinkButton
        ID="btnAddReporter" runat="server" class="bigfont" Text="添加新记者" OnClick="btnAdd_Click" />
        </td>
        </tr>
        </table>--%>
    <table width="100%">
        <tr>
            <td>
                <table width="100%" border="0" class="tableForm">
                    <tr>
                        <td colspan="4" class="heading">
                            查找条件
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" width="20%">
                            记者姓名：
                        </td>
                        <td class="oddrow-l" width="30%">
                            <asp:TextBox ID="txtReporterName" runat="server" MaxLength="20"></asp:TextBox>
                        </td>
                        <td class="oddrow">
                            所属媒体：
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtMedia" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" width="20%">
                            手机号：
                        </td>
                        <td class="oddrow-l" width="30%">
                            <asp:TextBox ID="txtMobile" runat="server"></asp:TextBox><br />
                            <asp:Label ID="labMobile" runat="server">例如: 13682160586</asp:Label>
                        </td>
                        <td class="oddrow">
                            身份证号：
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtIdCard" runat="server"></asp:TextBox><br />
                            <asp:Label ID="labIdCard" runat="server">请正确输入身份证号码</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow">
                            邮箱：
                        </td>
                        <td class="oddrow-l" colspan="3">
                            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox><br />
                            <asp:Label ID="labEmail" runat="server">例如: aa@163.com</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Button ID="btnFind" runat="server" Text="查找" Width="83px" OnClientClick="return check();"
                                CssClass="widebuttons" OnClick="btnFind_Click" />
                            <asp:Button ID="btnClear" runat="server" CssClass="widebuttons" Text="返回总库" CausesValidation="true"
                                OnClick="btnClear_OnClick" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 20">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="0">
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td colspan="4" class="headinglist" align="left" width="10%">
                                        记者列表
                                    </td>
                                    <td width="74%">
                                    </td>
                                    <td width="16%" align="right" class="tablehead">
                                        <img src="/images/add.gif" border="0" style="vertical-align: bottom" />&nbsp;<asp:LinkButton
                                            ID="btnAddReporter" runat="server" class="bigfont" Text="添加新记者" OnClick="btnAdd_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <cc4:MyGridView ID="dgList" runat="server" OnRowDataBound="dgList_RowDataBound" OnSorting="dgList_Sorting">
                            </cc4:MyGridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="right">
                            <asp:Button ID="btnLink" runat="server" Text="关联到媒体" OnClientClick="return check();"
                                CssClass="widebuttons" OnClick="btnLink_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Button ID="btnSendMail" runat="server" CssClass="widebuttons" Text="发送邮件" CausesValidation="true"
                    OnClientClick="return MailOpen() ;"></asp:Button>
                <asp:Button ID="btnSendMsg" runat="server" CssClass="widebuttons" Text="发送短信" CausesValidation="true"
                    OnClientClick="return MsgOpen() ;"></asp:Button>
                <asp:Button ID="btnReporterSign" runat="server" CssClass="widebuttons" Text="生成签到表"
                    OnClick="btnReporterSign_Click" />
                <asp:Button ID="btnReporterContact" runat="server" CssClass="widebuttons" Text="生成通联表"
                    OnClick="btnReporterContact_Click" />
            </td>
        </tr>
    </table>
    <input type="hidden" value="0" runat="server" id="hidChkID" />
</asp:Content>
