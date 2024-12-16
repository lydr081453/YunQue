<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WrittingFeeExport.aspx.cs"
    MasterPageFile="~/MasterPage.master" Inherits="PurchaseWeb.Purchase.Requisition.WrittingFeeExport" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script type="text/javascript" src="/public/js/jquery.blockUI.js"></script>

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

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

        function ReporterClick() {
            var mid = document.getElementById('<% = hidMediaID.ClientID %>').value;
            if (mid == null || mid == "" || mid == "undefined") {
                alert("请先选择媒体." + mid);
                return false;
            }
            var win = window.open('ReporterForExportDlg.aspx?Mid=' + mid, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }

        
    </script>
    
    <table width="100%" class="tableform">
        <tr>
            <td>
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="oddrow">
                            稿件费用描述
                        </td>
                        <td class="oddrow-l" colspan="3">
                            <asp:TextBox ID="txtDescription" runat="server" Width="400px" Text=""></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="heading" colspan="4">
                            模板导出
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
                            <input type="button" value="请选择..." class="widebuttons" onclick="return MediaClick();" /><font
                                color="red">点击请选择添加新媒体</font>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 15%">
                            记者：
                        </td>
                        <td class="oddrow-l" colspan="3">
                            <%--<asp:TextBox ID="txtReporterSelect" runat="server"></asp:TextBox><input type="hidden"
                                id="hidReporterID" runat="server" />--%><input type="hidden"
                                id="hidReporterIDs" runat="server" />
                            <input type="button" value="选择记者..." class="widebuttons" onclick="return ReporterClick();" /><font
                                color="red">点击请选择添加新记者</font><asp:LinkButton ID="linkbtnReporter" runat="server" OnClick="linkbtnReporter_Click" Text=""></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:GridView ID="gvReporter" runat="server" AutoGenerateColumns="false" DataKeyNames="ReporterID"
                    OnRowDataBound="gvReporter_RowDataBound" OnRowCommand="gvReporter_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="记者" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30%">
                            <ItemTemplate>
                                <asp:Label ID="lblReporterName" runat="server" Text='<%# Eval("ReporterName") %>'></asp:Label>
                                <asp:HiddenField ID="hidReporterID" runat="server" Value='<%# Eval("ReporterID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="所属媒体" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40%">
                            <ItemTemplate>
                                <asp:Label ID="lblMediaName" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server"
                                    CommandName="Del" Text="<img src='../../images/disable.gif' title='删除' border='0'>" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                    OnClientClick="return confirm('你确定删除吗？');" CausesValidation="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td align="center"><asp:Button ID="btnExport" runat="server" Text="导出至Excel格式模板" OnClick="btnExport_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
