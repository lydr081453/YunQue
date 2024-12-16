<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="logManage_OperateLogList" Codebehind="OperateLogList.aspx.cs" %>

<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<%@ Register TagPrefix="cc2" Namespace="MyControls" Assembly="MyControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentHead" runat="Server">

    <script type="text/javascript">
  function   WinOpen()   
      {   
      window.open("LogUserList.aspx","选择邮件","<%= ESP.Media.Access.Utilities.Global.OpenClass.Common%>");    
      } 
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMain" runat="Server">
    <input type="hidden" value="" runat="server" id="hidChkID" />
    <table width="100%">
        <tr>
            <td>
                <table width="100%" border="1" class="tableForm">
                    <tr>
                        <td colspan="4" class="heading">
                            查找条件：</td>
                    </tr>
                    <tr>
                        <td class="oddrow">
                            操作人姓名：</td>
                        <td colspan="3" class="oddrow-l">
                            <asp:TextBox ID="txtOperateName" runat="server" ReadOnly="true"></asp:TextBox>
                            <asp:Button ID="btnSel" Text="选择" OnClientClick=" WinOpen();return false;" runat="server" CssClass="widebuttons">
                            </asp:Button></td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 20%">
                            操作类型：</td>
                        <td style="width: 30%" class="oddrow-l">
                            <asp:DropDownList ID="ddlOperateType" runat="server" CssClass="fixddl">
                            </asp:DropDownList></td>
                        <td class="oddrow" style="width: 20%">
                            操作表：</td>
                        <td style="width: 30%" class="oddrow-l">
                            <asp:DropDownList ID="ddlTable" runat="server" CssClass="fixddl">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="oddrow">
                            操作时间 从：</td>
                        <td class="oddrow-l">
                            <cc2:DatePicker ID="dpBeginDate" Width="120px" runat="server"></cc2:DatePicker>
                        </td>
                        <td class="oddrow">
                            到：</td>
                        <td class="oddrow-l">
                            <cc2:DatePicker ID="dpEndDate" Width="120px" runat="server"></cc2:DatePicker>
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
        <tr>
            <td>
                <table width="100%" border="0">
                    <tr>
                        <td colspan="4" class="headinglist">
                            操作日志列表</td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <cc4:MyGridView ID="dgList" runat="server" OnRowDataBound="dgList_RowDataBound">
                            </cc4:MyGridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
