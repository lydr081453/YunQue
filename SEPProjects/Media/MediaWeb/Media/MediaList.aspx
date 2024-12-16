<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Media_MediaList" Title="媒体列表" Codebehind="MediaList.aspx.cs" %>

<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">
<script type="text/javascript">
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
            window.open('/DownLoad/MediaList.aspx?ExportType=sign&FileName=' + fn + '&Term=' + hidChkID.value, "");
                return false;
            }
            else return false;
        }
        function btnReporterContact_ClientClick(fn) {
            if (selectone()) {
                window.open('/DownLoad/MediaList.aspx?ExportType=contact&FileName=' + fn + '&Term=' + hidChkID.value, "");
                return false;
            }
            else return false;
        }
</script>
    <table width="100%">
        <tr>
            <td> 
            <table class="tablehead">
              <tr>
            <td>
              <img src="/images/add.gif" border="0" style="vertical-align: bottom" />&nbsp;<asp:LinkButton
                    ID="btnAddReporter" runat="server" class="bigfont" Text="添加新媒体" OnClick="btnAdd_Click" />
                    </td>
             </tr>
            </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="1" class="tableForm">
                    <tr>
                        <td colspan="4" class="heading">
                            查找条件
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 20%">
                            媒体名称：
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:TextBox ID="txtCnName" runat="server"></asp:TextBox>
                        </td>
                        <td class="oddrow" style="width: 20%">
                            形态：
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:DropDownList ID="ddlMediaType" runat="server" AutoPostBack="true" CssClass="fixddl">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 20%">
                            覆盖区域：
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:TextBox ID="txtIssueRegion" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 20%">
                            行业属性：
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:DropDownList ID="ddlIndustry" runat="server" CssClass="fixddl" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Button ID="btnSearch" Text="查找" OnClick="btnSearch_Click" runat="server" CssClass="widebuttons">
                            </asp:Button>
                            <asp:Button ID="btnClear" runat="server" CssClass="widebuttons" Text="返回总库" CausesValidation="true" OnClick="btnClear_OnClick" />
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
                        <td colspan="4" class="headinglist">
                            媒体列表
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <cc4:MyGridView ID="dgList" runat="server" OnRowDataBound="dgList_RowDataBound"
                                OnSorting="dgList_Sorting">
                            </cc4:MyGridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%" border="0">       
       <tr>
            <td align="right">                
                <asp:Button ID="btnSendMail" runat="server" CssClass="widebuttons" Text="发送邮件" CausesValidation="true"
                    OnClientClick="return MailOpen() ;"></asp:Button>
                <asp:Button ID="btnSendMsg" runat="server" CssClass="widebuttons" Text="发送短信" CausesValidation="true"
                    OnClientClick="return MsgOpen() ;"></asp:Button>
                <asp:Button ID="btnReporterSign" runat="server" OnClientClick="return selectone();" CssClass="widebuttons" Text="生成签到表"
                    OnClick="btnReporterSign_Click" />
                <asp:Button ID="btnReporterContact" runat="server" OnClientClick="return selectone();" CssClass="widebuttons" Text="生成通联表"
                    OnClick="btnReporterContact_Click" />
           </td>
       </tr>
    </table>
    <input type="hidden" value="0" runat="server" id="hidChkID" />
</asp:Content>
