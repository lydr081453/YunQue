<%@ Page Title="选择供应商" Language="C#" MasterPageFile="~/MasterPage.master" EnableEventValidation="false" AutoEventWireup="true" Inherits="Purchase_Requisition_ChangedSupplier" Codebehind="ChangedSupplier.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%@ register src="../../UserControls/Edit/supplierInfo.ascx" tagname="supplierInfo"
        tagprefix="uc1" %>
    <link href="/public/css/dialog.css" type="text/css" rel="stylesheet" />
    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>
    <script language="javascript" src="/public/js/dialog.js"></script>
    <script language="javascript">

    </script>
    <script language="javascript">
        function show(sname, sid) {
            var win = window.open('SupplierInfoView.aspx?isback=1&supplierId=' + sid, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }
        function show2() {

            dialog("物料类别结构图", "id:testID", "500px", "300px", "123123123");
        }
    </script>

    <span id="sp" onclick="show2();" style="color: #7282a9; text-decoration: underline; display:none;
        cursor: pointer">物料类别结构图</span>
    <br />
    <br />

    供应商名称:&nbsp;<asp:TextBox ID="txtSupplierName" runat="server" />&nbsp;&nbsp;<asp:Button
        Text=" 检索 " CssClass="widebuttons" runat="server" ID="btnSearch" />&nbsp;<asp:Button
            ID="btnClean" runat="server" CssClass="widebuttons" Visible="false" OnClick="btnClean_Click"
            Text=" 重新搜索 " />&nbsp;<input
        type="button" class="widebuttons" value=" 关闭 " onclick="window.close();" />
    <br />
    <br />
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left">
                                <asp:Button ID="btnFP1" runat="server" Text="首页" CssClass="widebuttons" OnClick="btnFP_Click" />
                                <asp:Button ID="btnPP1" runat="server" Text="上页" CssClass="widebuttons" OnClick="btnPP_Click" />
                                <asp:Button ID="btnNP1" runat="server" Text="下页" CssClass="widebuttons" OnClick="btnNP_Click" />
                                <asp:Button ID="btnLP1" runat="server" Text="末页" CssClass="widebuttons" OnClick="btnLP_Click" />
                            </td>
                            <td align="right" width="50%">页数：<asp:Literal ID="litCurrentPage1" runat="server" />/<asp:Literal ID="litTotalPage1" runat="server" /> &nbsp;&nbsp;记录数：<asp:Literal ID="litCount1" runat="server" /></td>
                        </tr>
                    </table>
    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="false" AllowPaging="false"
        DataKeyNames="id" OnRowCommand="gv_RowCommand" Width="100%" CellPadding="4">
        <Columns>
            <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Button ID="btnSelect" runat="server" Text="选择" CommandArgument='<%# Eval("id").ToString()+"-"+ Eval("supplyId").ToString() %>' OnClick="btnSelect_Click" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="id" HeaderText="id" Visible="False" ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="供应商名称">
                <ItemTemplate>
                    <%# Eval("supplyName") == "" ? Eval("supplier_name") : Eval("supplyName")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="contact_name" HeaderText="联系人" />
            <asp:BoundField DataField="contact_tel" HeaderText="联系电话" />
        </Columns>
    </asp:GridView>
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left">
                                <asp:Button ID="btnFP2" runat="server" Text="首页" CssClass="widebuttons" OnClick="btnFP_Click" />
                                <asp:Button ID="btnPP2" runat="server" Text="上页" CssClass="widebuttons" OnClick="btnPP_Click" />
                                <asp:Button ID="btnNP2" runat="server" Text="下页" CssClass="widebuttons" OnClick="btnNP_Click" />
                                <asp:Button ID="btnLP2" runat="server" Text="末页" CssClass="widebuttons" OnClick="btnLP_Click" />
                            </td>
                            <td align="right" width="50%">页数：<asp:Literal ID="litCurrentPage2" runat="server" />/<asp:Literal ID="litTotalPage2" runat="server" /> &nbsp;&nbsp;记录数：<asp:Literal ID="litCount2" runat="server" /></td>
                        </tr>
                    </table>
    <asp:Button ID="btnX" runat="server" Text="非协议供应商" Visible="false" CssClass="widebuttons" OnClick="btnX_Click" />
    <br /><br />
    <uc1:supplierInfo ID="supplierInfo" runat="server" Visible="false" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" />
    <table width="100%" border="0">
        <tr>
            <td><asp:Button ID="btnSave" Visible="false" runat="server" Text=" 保存 " CssClass="widebuttons" OnClick="btnSave_Click" /></td>

        </tr>
    </table>

    <div id="testID" style="display: none">
        <div id="wl111" style="border-right: 1px inset; border-top: 1px inset; overflow: auto;
            border-left: 1px inset; width: 400px; border-bottom: 1px inset; height: 300px;
            background-color: white">
            <yyc:SmartTreeView ID="treeView" ShowLines="true" runat="server" Enabled="false"
                AllowCascadeCheckbox="True" ImageSet="Msdn" BackColor="#FFFFFF" Style="background-color: White"
                NodeIndent="10">
                <ParentNodeStyle Font-Bold="False" BackColor="#FFFFFF" />
                <HoverNodeStyle BackColor="#ffffff" BorderColor="#FFFFFF" BorderStyle="None" Font-Underline="False" />
                <SelectedNodeStyle BackColor="#FFFFFF" BorderColor="#FFFFFF" BorderStyle="None" Font-Underline="False" />
                <NodeStyle Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" HorizontalPadding="5px"
                    BackColor="#FFFFFF" NodeSpacing="1px" VerticalPadding="2px" />
            </yyc:SmartTreeView>
        </div>
    </div>
</asp:Content>

