<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="Purchase_Requisition_SupplierInfoList" Title="Untitled Page" EnableEventValidation="false"
    CodeBehind="SupplierInfoList.aspx.cs" %>

<%@ Import Namespace="ESP.Purchase.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" src="../../public/js/DatePicker.js"></script>

    <script type="text/javascript">
        function openwindow(id) {
            window.open("SupplierInfoView.aspx?supplierId=" + id);
        }
    </script>

    <script type="text/javascript">

        function openwindow(id) {
            window.open("SupplierInfoView.aspx?supplierId=" + id);
        }
    </script>

    <li><a href="SupplierInfoEdit.aspx">添加新供应商</a></li>
    <br />
    <br />
    <table width="100%" class="tableForm">
        <tr>
            <td colspan="4" class="heading">
                检索
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">
                供应商名称:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtsupplierName" runat="server" Width="200px" MaxLength="200" />
            </td>
            <td class="oddrow" style="width: 10%">
                物料类别:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtType" MaxLength="200" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                所属地区:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtArea" runat="server" MaxLength="200" Width="200px" />
            </td>
            <td class="oddrow">
                联系人:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtlinkName" runat="server" MaxLength="200" Width="200px" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                供应商类型:
            </td>
            <td class="oddrow-l">
                <asp:DropDownList ID="ddlSupplierType" runat="server" />
            </td>
            <td class="oddrow">
                使用状态:
            </td>
            <td class="oddrow-l">
                <asp:DropDownList ID="ddlStatus" runat="server" />
            </td>
        </tr>
        <tr>
         <td class="oddrow">
                物料审核人:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtAuditor" runat="server" MaxLength="200" Width="200px" />
            </td>
            <td colspan="2" class="oddrow-l">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " OnClick="btnSearch_Click" CssClass="widebuttons" />
            </td>
        </tr>
    </table>
    <br />
    <table width="100%">
        <tr>
            <td align="right"><asp:Button ID="btnExport" runat="server" CssClass="widebuttons" OnClick="btnExport_Click" Text=" 导出 " /></td>
        </tr>
    </table>
    <table width="100%" id="tabTop" runat="server">
        <tr>
            <td width="50%">
                <asp:Panel ID="PageTop" runat="server">
                    <asp:Button ID="btnFirst2" runat="server" CssClass="widebuttons" Text="首页" OnClick="btnFirst_Click" />&nbsp;
                    <asp:Button ID="btnPrevious2" runat="server" CssClass="widebuttons" Text="上页" OnClick="btnPrevious_Click" />&nbsp;
                    <asp:Button ID="btnNext2" runat="server" CssClass="widebuttons" Text="下页" OnClick="btnNext_Click" />&nbsp;
                    <asp:Button ID="btnLast2" runat="server" CssClass="widebuttons" Text="末页" OnClick="btnLast_Click" />
                </asp:Panel>
            </td>
            <td align="right" class="recordTd">
                记录数:<asp:Label ID="labAllNumT" runat="server" />&nbsp;页数:<asp:Label ID="labPageCountT"
                    runat="server" />
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gvSupplier" runat="server" AutoGenerateColumns="False" OnRowDeleting="gvSupplier_RowDeleting"
        OnRowDataBound="gv_RowDataBound" DataKeyNames="id" PageSize="20" AllowPaging="True" OnSorting="gvSupplier_Sorting"
        OnRowCommand="gvSupplier_RowCommand" AllowSorting="true" Width="100%" PagerSettings-Visible="false" OnPageIndexChanging="gvSupplier_PageIndexChanging">
        <Columns>
            <asp:TemplateField HeaderText="供应商名称" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center"
                SortExpression="supplier_name">
                <ItemTemplate>
                    <label onclick="openwindow(<%#Eval("id") %>);" style="color: #7282a9; cursor: pointer">
                        <%#Eval("supplier_name")%></label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="supplier_area" HeaderText="所属地区" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="6%" SortExpression="supplier_area" />
            <asp:BoundField DataField="contact_name" HeaderText="联系人" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="6%" SortExpression="contact_name" />
            <asp:TemplateField HeaderText="联系电话" ItemStyle-Width="10%">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="labPhone" runat="server" Text='<%#Eval("contact_tel") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="contact_mobile" HeaderText="手机" Visible="false" ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="传真" Visible="false">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="labFax" runat="server" Text='<%#Eval("contact_fax") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="contact_email" HeaderText="电子邮件" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="15%" />
            <asp:BoundField DataField="productTypes" HeaderText="物料类别" 
                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" />
            <asp:BoundField DataField="supplier_frameNO" HeaderText="框架协议号" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="6%" />
            <asp:BoundField DataField="business_price" HeaderText="price" />
            <asp:TemplateField HeaderText="状态" ItemStyle-Width="4%">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="labStatus" runat="server" Text='<%#(State.supplierstatus[int.Parse(Eval("supplier_status").ToString())]) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="报价" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="4%">
                <ItemTemplate>
                    <%# Eval("business_price").ToString() == "" ? "" : "<a target='_blank' href='../../" + Eval("business_price").ToString() + "'><img src='/images/ico_04.gif' border='0' /></a>"%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="类型" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="5%">
                <ItemTemplate>
                    <asp:Literal ID="litType" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="编辑" HeaderStyle-Width="4%">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                    <a href='SupplierInfoEdit.aspx?supplierId=<%#DataBinder.Eval(Container.DataItem,"id")%>'>
                        <img src="../../images/edit.gif" border="0px;"></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="转换" HeaderStyle-Width="4%" ItemStyle-HorizontalAlign="Center"
                Visible="false">
                <ItemTemplate>
                    <asp:LinkButton CommandArgument='<%# Eval("id") %>' runat="server" OnClientClick="return confirm('您确定转换为推荐供应商吗？');"
                        Text="<img src='/images/changed.gif' border='0' />" ToolTip="转换为推荐供应商" CommandName="Changed"
                        CausesValidation="false" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="停用/启用" HeaderStyle-Width="4%" ItemStyle-HorizontalAlign="Center"
                Visible="false">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkUse" runat="server" CommandArgument='<%# Eval("id") %>' CausesValidation="false" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

    </asp:GridView>
    <table width="100%" id="tabBottom" runat="server">
        <tr>
            <td width="50%">
                <asp:Panel ID="PageBottom" runat="server">
                    <asp:Button ID="btnFirst" runat="server" CssClass="widebuttons" Text="首页" OnClick="btnFirst_Click" />&nbsp;
                    <asp:Button ID="btnPrevious" runat="server" CssClass="widebuttons" Text="上页" OnClick="btnPrevious_Click" />&nbsp;
                    <asp:Button ID="btnNext" runat="server" CssClass="widebuttons" Text="下页" OnClick="btnNext_Click" />&nbsp;
                    <asp:Button ID="btnLast" runat="server" CssClass="widebuttons" Text="末页" OnClick="btnLast_Click" />&nbsp;
                </asp:Panel>
            </td>
            <td align="right" class="recordTd">
                记录数:<asp:Label ID="labAllNum" runat="server" />&nbsp;页数:<asp:Label ID="labPageCount"
                    runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
