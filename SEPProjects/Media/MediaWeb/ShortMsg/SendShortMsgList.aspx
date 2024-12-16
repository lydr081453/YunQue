<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" Title="短信列表" Inherits="ShortMsg_SendShortMsgList" Codebehind="SendShortMsgList.aspx.cs" %>

<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<%@ Register TagPrefix="cc2" Namespace="MyControls" Assembly="MyControls" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="contentHead" runat="server">

    <script type="text/javascript">
  
      function selectone(obj)
        {
            if (obj.checked)
            {
               hide = document.getElementById("<% =hidChkID.ClientID %>");
                 str = obj.value ;
                hide.value = hide.value.replace(str,"");
                hide.value = str;
            }
        }
           function closereturn()
        {
          hide = document.getElementById("<% =hidChkID.ClientID %>");
         var bb= hide.value.split(",")
          if(hide.value=="0")  
         {
             bb[2]="";
             bb[1]="";
         }  
           opener.document.getElementById("<% =hidShortMsgID.ClientID %>").value=bb[0]; 
         opener.document.getElementById("<% =txtSubject.ClientID %>").value=bb[1]; 
         opener.document.getElementById("<% =txtBody.ClientID %>").value=bb[2];
          window.parent.close();   
   
        }
    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">
    <input type="hidden" value="0" runat="server" id="hidChkID" />
    <input type="hidden" value="" runat="server" id="txtBody" />
    <input type="hidden" id="hidPjID" runat="server" value="0" /> 
    <input type="hidden" value="0" runat="server" id="hidShortMsgID" />
    <table width="100%">
        <tr>
            <td>
            <table class="tablehead">
                <tr>
                    <td>
                        <img src="/images/add.gif" border="0" style="vertical-align: bottom" />&nbsp;<asp:LinkButton
                            ID="btnAdd" runat="server" class="bigfont" Text="添加" OnClick="btnAdd_Click" />
                    </td>
                </tr>
                </table>
                <table width="100%" border="0" class="tableForm">
                    <tr>
                        <td colspan="4" class="heading">
                            查找条件
                        </td>
                    </tr>                 
                    <tr>
                        <td class="oddrow" width="20%">
                            主题：
                        </td>
                        <td width="30%" class="oddrow-l">
                            <asp:TextBox ID="txtSubject" runat="server"></asp:TextBox>
                        </td>
                        <td class="oddrow">
                            关键字：
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtBodyKey" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow">
                            创建日期 从：
                        </td>
                        <td class="oddrow-l">
                            <cc2:DatePicker ID="dpBeginDate" Width="120px" runat="server"></cc2:DatePicker>
                        </td>
                        <td class="oddrow">
                            到：
                        </td>
                        <td class="oddrow-l">
                            <cc2:DatePicker ID="dpEndDate" Width="120px" runat="server"></cc2:DatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Button ID="btnFind" runat="server" Text="查找" CssClass="widebuttons"
                                OnClick="btnFind_Click" />
                                <asp:Button ID="btnClear" runat="server" CssClass="widebuttons" Text="返回总库" CausesValidation="true" OnClick="btnClear_OnClick" />                         
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
                <table width="100%" border="0">
                    <tr>
                        <td colspan="4" class="headinglist">
                            搜索结果
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <cc4:MyGridView ID="dgList" runat="server" OnRowDataBound="dgList_RowDataBound">
                            </cc4:MyGridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="right">
                            <asp:Button ID="btnReturn" runat="server" Text="确定" CssClass="widebuttons"
                                OnClientClick="closereturn();" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
