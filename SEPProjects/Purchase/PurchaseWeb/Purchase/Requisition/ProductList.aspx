<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Purchase_ProductList" Title="Untitled Page"
    EnableEventValidation="false" Codebehind="ProductList.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" src="../../public/js/DatePicker.js"></script>

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>

    

    <script type="text/javascript">
    
        $().ready(function() {
            TypeDataProvider.GetListByParentIdA($("#<%=ddltype.ClientID %>").val(), pop11);
            function pop11(r) {
                $("#<%=ddltype1.ClientID %>").empty();
                $("#<%=ddltype1.ClientID %>").append("<option value=\"-1\">请选择...</option>");
                for (i = 0; i < r.value.length; i++) {
                    $("#<%=ddltype1.ClientID %>").append("<option value=\"" + r.value[i].typeid + "\">" + r.value[i].typename + "</option>");
                }
                $("#<%=ddltype1.ClientID %>").val($("#<%=hidtype1.ClientID %>").val());
            }
            TypeDataProvider.GetListByParentIdA($("#<%=hidtype1.ClientID %>").val(), pop22);
            function pop22(r) {
                $("#<%=ddltype2.ClientID %>").empty();
                $("#<%=ddltype2.ClientID %>").append("<option value=\"-1\">请选择...</option>");
                if (r.value != null && r.value.length > 0) {
                    for (i = 0; i < r.value.length; i++) {
                        $("#<%=ddltype2.ClientID %>").append("<option value=\"" + r.value[i].typeid + "\">" + r.value[i].typename + "</option>");
                    }
                    $("#<%=ddltype2.ClientID %>").val($("#<%=hidtype2.ClientID %>").val());
                }
            }

            $("#<%=ddltype.ClientID %>").change(function() {
                $("#<%=ddltype1.ClientID %>").val($("#<%=ddltype1.ClientID %>").val());
                $("#<%=ddltype2.ClientID %>").empty();
                $("#<%=ddltype2.ClientID %>").append("<option value=\"-1\">请选择...</option>");
                $("#<%=hidtype1.ClientID %>").val("");
                $("#<%=hidtype2.ClientID %>").val("");

                TypeDataProvider.GetListByParentIdA($("#<%=ddltype.ClientID %>").val(), pop1);
                function pop1(r) {
                    $("#<%=ddltype1.ClientID %>").empty();
                    $("#<%=ddltype1.ClientID %>").append("<option value=\"-1\">请选择...</option>");
                    for (i = 0; i < r.value.length; i++) {
                        $("#<%=ddltype1.ClientID %>").append("<option value=\"" + r.value[i].typeid + "\">" + r.value[i].typename + "</option>");
                    }
                }
            });

            $("#<%=ddltype1.ClientID %>").change(function() {

                TypeDataProvider.GetListByParentIdA($("#<%=ddltype1.ClientID %>").val(), pop2);
                function pop2(r) {
                    $("#<%=ddltype2.ClientID %>").empty();
                    $("#<%=ddltype2.ClientID %>").append("<option value=\"-1\">请选择...</option>");
                    for (i = 0; i < r.value.length; i++) {
                        $("#<%=ddltype2.ClientID %>").append("<option value=\"" + r.value[i].typeid + "\">" + r.value[i].typename + "</option>");
                    }
                    $("#<%=hidtype1.ClientID %>").val($("#<%=ddltype1.ClientID %>").val());
                }
            });

            $("#<%=ddltype2.ClientID %>").change(function() {
                $("#<%=hidtype2.ClientID %>").val($("#<%=ddltype2.ClientID %>").val());
            });
        });
    </script>

    <li><a href="ProductEdit.aspx">添加新物品</a></li>
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
                <asp:TextBox ID="txtsupplierName" runat="server" MaxLength="200" Width="150px" />
            </td>
            <td class="oddrow" style="width: 10%">
                物料类别:
            </td>
            <td class="oddrow-l" style="width: 45%">
                <asp:DropDownList ID="ddltype" runat="server" AutoPostBack="false" Width="120px" />
                &nbsp;<asp:DropDownList ID="ddltype1" runat="server" Width="150px" />
                &nbsp;<asp:DropDownList ID="ddltype2" runat="server" Width="150px" />
                <asp:HiddenField ID="hidtype" runat="server" />
                <asp:HiddenField ID="hidtype1" runat="server" />
                <asp:HiddenField ID="hidtype2" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                物品名称:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtproductName" MaxLength="200" runat="server" Width="150px" />
            </td>
            <td class="oddrow">
                物品种类:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtproductClass"  MaxLength="200" runat="server" Width="150px" />
            </td>
        </tr>
        <tr>
            <td colspan="4" class="oddrow-l">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " OnClick="btnSearch_Click" CssClass="widebuttons" />
            </td>
        </tr>
    </table>
    <br />
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
    <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="False"
        OnRowDeleting="gvProduct_RowDeleting" DataKeyNames="id" PageSize="20" AllowPaging="True"
        Width="100%" OnPageIndexChanging="gvProduct_PageIndexChanging">
        
        <Columns>
            <asp:BoundField DataField="productName" HeaderText="物品名称" />
            <asp:BoundField DataField="typename" HeaderText="物料类别" />
            <asp:BoundField DataField="productClass" HeaderText="物品种类" />
            <asp:BoundField DataField="suppliername" HeaderText="供应商名称" />
            <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <a href="ProductView.aspx?productId=<%#Eval("id") %>">
                        <img src="../../images/dc.gif" border="0px;"></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="编辑">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                    <a href='ProductEdit.aspx?productId=<%#DataBinder.Eval(Container.DataItem,"id")%>'>
                        <img src="../../images/edit.gif" border="0px;"></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="删除" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="return confirm('您确定删除吗？');" Text="<img src='/images/disable.gif' border='0' />" CommandName="Delete" CausesValidation="false" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        
        <PagerSettings Visible="false" />
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
