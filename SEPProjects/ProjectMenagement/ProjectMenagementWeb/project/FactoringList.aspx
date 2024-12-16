<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="FactoringList.aspx.cs" EnableEventValidation="false"
    Inherits="FinanceWeb.project.FactoringList" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript" type="text/javascript">

        $().ready(function () {
            $("#<%=ddlBranch.ClientID %>").empty();
            FinanceWeb.project.FactoringList.GetBranchList(initBranch);
            function initBranch(r) {
                if (r.value != null) {
                    for (k = 0; k < r.value.length; k++) {
                        if (r.value[k][0] == $("#<%=hidBranchID.ClientID %>").val()) {
                            $("#<%=ddlBranch.ClientID %>").append("<option value=\"" + r.value[k][0] + "\" selected>" + r.value[k][1] + "</option>");
                        }
                        else {
                            $("#<%=ddlBranch.ClientID %>").append("<option value=\"" + r.value[k][0] + "\">" + r.value[k][1] + "</option>");
                        }
                    }
                }
            }
        });

        function selectBranch(id, text) {
            if (id == "-1") {
                document.getElementById("<% =hidBranchID.ClientID %>").value = "";
                document.getElementById("<% =hidBranchCode.ClientID %>").value = "";
            }
            else {
                document.getElementById("<% =hidBranchID.ClientID %>").value = id;
                document.getElementById("<% =hidBranchCode.ClientID %>").value = text;
            }
        }
    </script>

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">检索
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">关键字:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtKey" runat="server" />
            </td>
            <td class="oddrow" style="width: 15%">公司选择:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:DropDownList runat="server" ID="ddlBranch" Style="width: auto">
                </asp:DropDownList>
                <input type="hidden" id="hidBranchID" runat="server" />
                <input type="hidden" id="hidBranchCode" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click" />&nbsp;
                                        <asp:Button ID="btnSearchAll" runat="server" Text=" 检索全部 " CssClass="widebuttons"
                                            OnClick="btnSearchAll_Click" />
            </td>
        </tr>
    </table>

    <table width="100%">
        <tr>
            <td class="heading" colspan="4">付款申请列表
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="ReturnId"
                    OnRowCommand="gvG_RowCommand" OnRowDataBound="gvG_RowDataBound" PageSize="10"
                    EmptyDataText="暂时没有相关记录" PagerSettings-Mode="NumericFirstLast" OnPageIndexChanging="gvG_PageIndexChanging"
                    AllowPaging="true" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="prno" HeaderText="流水号" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="8%" />
                        <asp:BoundField DataField="project_code" HeaderText="项目号" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="8%" />
                        <asp:BoundField DataField="project_descripttion" HeaderText="项目名称" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="10%" />
                        <asp:BoundField DataField="requestorname" HeaderText="申请人" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="8%" />
                        <asp:BoundField DataField="department" HeaderText="业务组别" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="8%" />
                        <asp:BoundField DataField="totalprice" HeaderText="申请总额" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:C}"
                            ItemStyle-Width="8%" />
                        <asp:BoundField DataField="supplier_name" HeaderText="供应商" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="8%" />
                        <asp:BoundField DataField="returnCode" HeaderText="付款单号" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="8%" />
                        <asp:BoundField DataField="PreFee" HeaderText="付款金额" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:C}"
                            ItemStyle-Width="8%" />
                        <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="labState" Text='<%#Eval("ReturnStatus") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:HyperLink ID="hylEdit" runat="server" ImageUrl="/images/edit.gif" ToolTip="操作"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>


</asp:Content>
