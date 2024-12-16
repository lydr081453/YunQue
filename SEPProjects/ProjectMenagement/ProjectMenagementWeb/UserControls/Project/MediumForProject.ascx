<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MediumForProject.ascx.cs"
    Inherits="FinanceWeb.UserControls.Project.MediumForProject" %>

<script type="text/javascript" src="/public/js/jquery.js"></script>

<script type="text/javascript" src="/public/js/jquery.blockUI.js"></script>

<script type="text/javascript">
    function DisplayDetail(id, dis, idshow, idnone) {
        document.getElementById(id).style.display = dis;
        document.getElementById(idshow).style.display = "block";
        document.getElementById(idnone).style.display = "none";
    }
    function MediaClick() {
        var mediatype = document.getElementById('<% =ddlOption.ClientID %>').selectedIndex;
        var medianame = document.getElementById('<% = txtMediaSelect.ClientID %>').value;
        var win = window.open('MediaForReporterExportDlg.aspx?<% =ESP.Purchase.Common.RequestName.MediaType %>=' + mediatype + '&<% =ESP.Purchase.Common.RequestName.MediaName %>=' + medianame, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }

        
</script>

<table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="4">
            项目所使用媒体：
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            媒体类型：
        </td>
        <td class="oddrow-l" style="width: 30%">
            <asp:DropDownList runat="server" ID="ddlOption">
                <asp:ListItem Text="请选择.." Value="0"></asp:ListItem>
                <asp:ListItem Text="平面媒体" Value="1"></asp:ListItem>
                <asp:ListItem Text="网络媒体" Value="2"></asp:ListItem>
                <asp:ListItem Text="电视媒体" Value="3"></asp:ListItem>
                <asp:ListItem Text="广播媒体" Value="4"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td class="oddrow" style="width: 15%">
            媒体名称：
        </td>
        <td class="oddrow-l" style="width: 30%">
            <asp:TextBox ID="txtMediaSelect" runat="server"></asp:TextBox><input type="hidden"
                id="hidMediaID" runat="server" />
            <input type="button" value="请选择..." class="widebuttons" onclick="return MediaClick();" />
            <asp:LinkButton ID="linkbtnMediaForProject" runat="server" Visible="false" OnClick="linkbtnMediaForProject_Click"></asp:LinkButton>
        </td>
    </tr>
    
        <tr>
            <td align="center" colspan="4">
                <asp:GridView ID="gvMedia" runat="server" AutoGenerateColumns="false" DataKeyNames="MediaItemID"
                    OnRowDataBound="gvMedia_RowDataBound" OnRowCommand="gvMedia_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="媒体类型" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30%">
                            <ItemTemplate>
                                <asp:Label ID="lblMediaType" runat="server"></asp:Label>
                                <asp:HiddenField ID="hidID" runat="server" Value='<%# Eval("ID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="媒体名称" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40%">
                            <ItemTemplate>
                                <asp:Label ID="lblMediaName" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server"
                                    CommandName="DelMediaForPro" Text="<img src='../../images/disable.gif' title='删除' border='0'>" CommandArgument='<%# Eval("ID") %>'
                                    OnClientClick="return confirm('你确定删除吗？');" CausesValidation="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
</table>
