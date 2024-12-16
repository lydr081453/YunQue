<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" Inherits="ShortMsg_MailList" Codebehind="MailList.aspx.cs" %>

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
      function   WinOpen()   
      {  
      window.open("CreaterList.aspx","选择客户","<%=ESP.Media.Access.Utilities.Global.OpenClass.Common %>");  
      } 
    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">
    <table width="100%" border="0">
        <tr>
            <td>
                <%--           <table class="tablehead">
            <tr>
                <td>          
                 <img src="/images/add.gif" border="0" style=" vertical-align:bottom" />&nbsp;<asp:LinkButton ID="btnAddMail" runat="server" class="bigfont" Text="添加新邮件" OnClick="btnAdd_Click" />
               </td>
            </tr>
            </table>  --%>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" class="tableForm" border="1">
                    <tr>
                        <td colspan="4" class="heading">
                            查找条件
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" width="20%">
                            创建人：
                        </td>
                        <td class="oddrow-l" width="30%" colspan="3">
                            <asp:TextBox ID="txtCreaterName" runat="server"></asp:TextBox>
                            <asp:Button ID="btnClient" runat="server" Text="请选择" OnClientClick="WinOpen();return false;"
                                CssClass="widebuttons" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" width="20%">
                            主题：
                        </td>
                        <td class="oddrow-l" width="30%">
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
                            <asp:Button ID="btnFind" runat="server" Text="查找" Width="83px" CssClass="widebuttons"
                                OnClick="btnFind_Click" />
                            <asp:Button ID="btnClear" runat="server" CssClass="widebuttons" Text="返回总库" CausesValidation="true"
                                OnClick="btnClear_OnClick" />
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
            <td style="height: 142px">
                <table width="100%" border="0">
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td colspan="4" class="headinglist" width="10%" align="left">
                                        邮件列表
                                    </td>
                                    <td width="74%">
                                    </td>
                                    <td width="16%" align="right" class="tablehead">
                                        <img src="/images/add.gif" border="0" style="vertical-align: bottom" />&nbsp;<asp:LinkButton
                                            ID="btnAddMail" runat="server" class="bigfont" Text="添加新邮件" OnClick="btnAdd_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <cc4:MyGridView ID="dgList" runat="server" OnRowDataBound="dgList_RowDataBound">
                            </cc4:MyGridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <input type="hidden" value="0" runat="server" id="hidChkID" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
