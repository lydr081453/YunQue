<%@ Page Title="修订历史" Language="C#" MasterPageFile="~/Reporter.Master" AutoEventWireup="true"
    CodeBehind="EvaluationLogList.aspx.cs" Inherits="MediaWeb.newReporter.EvaluationLogList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script language="javascript">
    function checkSelect() {
        var items = document.getElementsByName("chk");
        var selectedCount = 0;
        for (var i = 0; i < items.length; i++) {
            if (items[i].checked)
                selectedCount++;
        }
        if (selectedCount < 2) {
            alert("请至少选择两个对比项。");
            return false;
        }
    }
</script>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td width="70">
                <img src="/images/ico-3.gif" width="64" height="62" />
            </td>
            <td class="fontsize-30">
                修订历史
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="570" valign="top"><table width="1000" border="0" cellpadding="0" cellspacing="10" bgcolor="#FFFFFF">
          <tr>
            <td>
    <asp:GridView ID="gvList" runat="server" AllowPaging="true" AutoGenerateColumns="false" HeaderStyle-CssClass="listHeader" Font-Size="12px" CellPadding="10"
        OnPageIndexChanging="gvList_PageIndexChanging" PageSize="10" PagerSettings-Mode="NextPreviousFirstLast" PagerSettings-FirstPageText="首页"
        PagerSettings-LastPageText="尾页" PagerSettings-NextPageText="下页" PagerSettings-PreviousPageText="上页">
        <Columns>
            <asp:TemplateField HeaderText="选择" HeaderStyle-CssClass="listHeader" ItemStyle-CssClass="listItem" ItemStyle-Width="5%">
                <ItemTemplate>
                    <input type="checkbox" name="chk" value='<%# Eval("id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="修订时间" HeaderStyle-CssClass="listHeader" ItemStyle-CssClass="listItem" ItemStyle-Width="20%">
                <ItemTemplate>
                    <%# DateTime.Parse(Eval("CreateDate").ToString()).ToString("yyyy-MM-dd hh:ss") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="修订人" HeaderStyle-CssClass="listHeader" ItemStyle-CssClass="listItem" ItemStyle-Width="10%">
                <ItemTemplate>
                    <%# Eval("UserName") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="原因" HeaderStyle-CssClass="listHeader" ItemStyle-CssClass="listItem" ItemStyle-Width="60%">
                <ItemTemplate>
                    <%# Eval("Reason") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="查看" HeaderStyle-CssClass="listHeader" ItemStyle-CssClass="listItem" ItemStyle-Width="5%">
                <ItemTemplate>
                    <a href='EvaluationLog.aspx?Rid=<%= Request[RequestName.ReporterID] %>&id=<%# Eval("id") %>'>
                        查看</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </td>
    </tr>
    <tr>
        <td><asp:Button ID="btnCompare" runat="server" Text="对比" OnClick="btnCompare_Click" OnClientClick="return checkSelect();" /></td>
    </tr>
    </table>
    </td>
    </tr>
    </table>
</asp:Content>
