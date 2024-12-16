<%@ Page Language="C#" MasterPageFile="~/Reporter.Master" AutoEventWireup="true" Title="记者列表"
    CodeBehind="ReporterList.aspx.cs" Inherits="MediaWeb.newReporter.ReporterList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <table border="0" cellpadding="0" cellspacing="0" style="width: 600px;">
        <tr>
            <td width="1"><img src="/images/01-02.jpg" width="10" height="74" /></td>
            <td background="/images/01-03.jpg">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" style="height: 64px;">
                    <tr>
                        <td>
                            <img src="/images/01-20.gif" width="12" height="12" hspace="5" /><strong class="fontsize-14 fontcolor-white">检索</strong>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="1">
                            <asp:TextBox ID="txtKeys" runat="server" CssClass="input-1" />
                        </td>
                        <td>
                            &nbsp;<asp:Button ID="btnSearch" runat="server" BorderWidth="0px" OnClick="btnSearch_Click" style="background-image:url(/images/01-05.jpg);width:71px;height:32px; vertical-align:bottom;" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="1" align="right"><img src="/images/01-04.jpg" width="10" height="74" /></td>
        </tr>
    </table>
    <br />
    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="border-top: 1px solid #ababab;">
        <tr>
            <td>
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td width="1">
                    <asp:Button ID="btnView1" OnClick="btnView1_Click" runat="server" Width="39" Height="33" style="background-image:url(/images/01-06.jpg);border:0px; cursor:pointer;" />
            </td>
            <td width="1">
                <asp:Button ID="btnView2" OnClick="btnView2_Click" runat="server" Width="41" Height="33" style="background-image:url(/images/01-07.jpg);border:0px; cursor:pointer;" />
            </td>
            <td align="right">
                &nbsp;
            </td>
        </tr>
    </table>
    <br />
    <asp:DataList ID="dgList" Visible="false" runat="server" RepeatColumns="6" RepeatDirection="Horizontal" CellPadding="0" CellSpacing="10" ItemStyle-HorizontalAlign="Center">
        <ItemTemplate>
            <table border="0" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF" class="index-headpic">
                <tr>
                    <td>
                        <a href='ReporterInfo.aspx?Rid=<%# Eval("reporterid") %>'>
                            <img src='<%# Eval("photo") == "" ? "/images/head-e.jpg" : ("/" + Eval("photo").ToString().Replace("~/","")) %>' width="190px" height="130px" />
                            </a>
                    </td>
                </tr>
                <tr>
                    <td style="line-height: 22px; padding-left:10px;" align="left">
                        <span class="fontsize-14"><strong><a href='ReporterInfo.aspx?Rid=<%# Eval("reporterid") %>'><%# Eval("reportername") %></a></strong></span><br />
                        <span class="fontsize-12">所属媒体：<%# Eval("medianame")%></span>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:DataList>
    <asp:Repeater ID="repList" runat="server">
    <HeaderTemplate>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td>
    </HeaderTemplate>
        <ItemTemplate>
            <table border="0" cellpadding="0" cellspacing="10" bgcolor="#FFFFFF" class="index-headpic-list">
            <tr>
              <td width="1"><a href='ReporterInfo.aspx?Rid=<%# Eval("reporterid") %>'><img src='<%# Eval("photo") == "" ? "/images/head-e.jpg" : ("/" + Eval("photo").ToString().Replace("~/","")) %>' width="190px" height="130px" /></a></td>
              <td><table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td><strong><a href='ReporterInfo.aspx?Rid=<%# Eval("reporterid") %>'><%# Eval("reportername") %></a></strong></td>
                    <td align="right">&nbsp;</td>
                  </tr>
                </table>
                  <table width="100%" border="0" cellspacing="0" cellpadding="0" style="line-height:22px; font-size:12px; border-top:1px dotted #ccc; margin-top:5px;">
                    <tr>
                      <td width="40%" valign="top" style="padding-top:5px;">所属媒体：<%# Eval("medianame")%><br />
                        性别：<%# Eval("sex")%> <br />
                        职务：<%# Eval("ReporterPosition")%><br />
                        负责领域：<%# Eval("responsibledomain")%></td>
                      <td valign="top" style="padding-top:5px;">手机：<%# Eval("mobile")%><br />
                        固话：<%# Eval("tel")%><br />
                        邮箱：<%# Eval("email")%></td>
                    </tr>
                </table></td>
            </tr>
          </table>
        </ItemTemplate>
        <FooterTemplate>
            </td>
            </tr>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr><td>
                                                    <span class="fontsize-12">记录总数：<%= RecordCount %>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;<strong>当前页</strong>：<font color="red"><%= page+1%></font>
                                                    /
                                                    <%= PageCount%></span>&nbsp;&nbsp;
        <asp:Button ID="btnFirst" runat="server" Text="首页" OnClick="btnFirst_Click" CssClass="btn-page" hspace="5" />
        <asp:Button ID="btnPre" runat="server" Text="上页" OnClick="btnPre_Click" CssClass="btn-page" hspace="5" />
        <asp:Button ID="btnNext" runat="server" Text="下页" OnClick="btnNext_Click" CssClass="btn-page" hspace="5" />
        <asp:Button ID="btnLast" runat="server" Text="尾页" OnClick="btnLast_Click" CssClass="btn-page" hspace="5" />
        </td></tr>
    </table>
    <asp:HiddenField ID="hidPage" runat="server" Value="0" />
    <asp:HiddenField ID="hidRecordCount" runat="server" Value="0" />
</asp:Content>
