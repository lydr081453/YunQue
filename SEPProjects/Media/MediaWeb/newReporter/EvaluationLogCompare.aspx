<%@ Page Title="" Language="C#" MasterPageFile="~/Reporter.Master" AutoEventWireup="true" CodeBehind="EvaluationLogCompare.aspx.cs" Inherits="MediaWeb.newReporter.EvaluationLogCompare" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td width="70"><img src="/images/ico-3.gif" width="64" height="62" /></td>
          <td class="fontsize-30">历史对比</td>
        </tr>
      </table>
        <br />
        <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" style="background-image:url(/images/btn-return.gif);margin-left:5px;" BorderWidth="0" width="71" height="32" /><br /><br />
    <asp:DataList ID="dgList" runat="server" RepeatColumns="10" BackColor="#858585" ForeColor="White" Font-Size="12px" ItemStyle-VerticalAlign="Top" RepeatDirection="Horizontal" CellPadding="0" CellSpacing="10" ItemStyle-HorizontalAlign="Center">
        <ItemTemplate>
            <table style="border-left:1px dotted #CCC;">
                <tr>
                    <td style="border-bottom:1px dotted #CCC;">修订人：<%# Eval("username") %></td>
                </tr>
                <tr>
                    <td style="border-bottom:1px dotted #CCC;">修订时间：<%# DateTime.Parse(Eval("CreateDate").ToString()).ToString("yyyy-MM-dd hh:ss") %></td>
                </tr>
                <tr>
                    <td style="border-bottom:1px dotted #CCC;">修订原因：<%# Eval("Reason") %></td>
                </tr>
                <tr>
                    <td><%# Eval("Evaluation") %></td>
                </tr>
            </table>
        </ItemTemplate>
        </asp:DataList>
</asp:Content>
