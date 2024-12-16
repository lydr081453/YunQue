<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Dialogs_IndustryDlg" Title="行业选择" Codebehind="IndustryDlg.aspx.cs" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <script type="text/javascript">
        function renderRating(val) {

            if (val > 8) {
                return String.format('<span style="color:green;">{0}</span>', val);
            }
            else {
                return String.format('<span style="color:red;">{0}</span>', val);
            }

        }
        function DoClient() {
            alert("Menu clicked");
        }

    </script>
<table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="4">
            行业查询
        </td>
    </tr>
    <tr>
     <td class="oddrow">
            关键字：
            <asp:TextBox ID="txtCode" runat="server" />

           <asp:Button ID="btnOK" runat="server"  CssClass="widebuttons" Text=" 检索 " 
                onclick="btnOK_Click" />&nbsp;
           <asp:Button ID="btnClean" runat="server" CssClass="widebuttons"  Text=" 重新搜索 " 
                onclick="btnClean_Click" />
        </td>
            </tr>
                  <tr>
                <td colspan="4" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                    padding-top: 4px;">
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:GridView ID="gvIndustry" runat="server" AutoGenerateColumns="False" DataKeyNames="IndustryID"
                                    OnRowCommand="gvIndustry_RowCommand" OnRowDataBound="gvIndustry_RowDataBound"  EmptyDataText="暂时没有行业信息" 
                                    AllowPaging="false" Width="100%">
                                    <Columns>
                                        <asp:ButtonField Text="选择" CommandName="Add" ButtonType="button" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" />
                                        <asp:BoundField DataField="IndustryID" HeaderText="IndustryID" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="IndustryCode" HeaderText="行业代码" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="20%" />
                                        <asp:BoundField DataField="CategoryName" HeaderText="行业名称" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="40%" />
                                        <asp:BoundField DataField="Description" HeaderText="行业描述" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="30%" />
                                    </Columns>
                                    <PagerStyle  Font-Size="X-Large" ForeColor="SteelBlue"/>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

    </table>
</asp:Content>