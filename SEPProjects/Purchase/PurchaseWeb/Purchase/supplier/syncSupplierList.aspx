<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master"  AutoEventWireup="true"  EnableEventValidation="false"  CodeBehind="syncSupplierList.aspx.cs" Inherits="PurchaseWeb.Purchase.supplier.syncSupplierList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script  type="text/javascript" src="../../public/js/DatePicker.js"></script>

    <script  type="text/javascript" src="/public/js/jquery-1.2.6.js"></script>
   
    <script type="text/javascript">

        $().ready(function() {
            ESP.Purchase.DataAccess.TypeDataProvider.GetListByParentIdA($("#<%=ddltype.ClientID %>").val(), pop11);
            function pop11(r) {
                $("#<%=ddltype1.ClientID %>").empty();
                $("#<%=ddltype1.ClientID %>").append("<option value=\"-1\">请选择...</option>");
                for (i = 0; i < r.value.length; i++) {
                    $("#<%=ddltype1.ClientID %>").append("<option value=\"" + r.value[i].typeid + "\">" + r.value[i].typename + "</option>");
                }
                $("#<%=ddltype1.ClientID %>").val($("#<%=hidtype1.ClientID %>").val());
            }
            ESP.Purchase.DataAccess.TypeDataProvider.GetListByParentIdA($("#<%=hidtype1.ClientID %>").val(), pop22);
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

                ESP.Purchase.DataAccess.TypeDataProvider.GetListByParentIdA($("#<%=ddltype.ClientID %>").val(), pop1);
                function pop1(r) {
                    $("#<%=ddltype1.ClientID %>").empty();
                    $("#<%=ddltype1.ClientID %>").append("<option value=\"-1\">请选择...</option>");
                    for (i = 0; i < r.value.length; i++) {
                        $("#<%=ddltype1.ClientID %>").append("<option value=\"" + r.value[i].typeid + "\">" + r.value[i].typename + "</option>");
                    }
                    $("#<%=hidtype.ClientID %>").val($("#<%=ddltype.ClientID %>").val());
                    $("#<%=hidtype1.ClientID %>").val($("#<%=ddltype1.ClientID %>").val());
                    $("#<%=hidtype2.ClientID %>").val($("#<%=ddltype2.ClientID %>").val());
                }
            });

            $("#<%=ddltype1.ClientID %>").change(function() {

                ESP.Purchase.DataAccess.TypeDataProvider.GetListByParentIdA($("#<%=ddltype1.ClientID %>").val(), pop2);
                function pop2(r) {
                    $("#<%=ddltype2.ClientID %>").empty();
                    $("#<%=ddltype2.ClientID %>").append("<option value=\"-1\">请选择...</option>");
                    for (i = 0; i < r.value.length; i++) {
                        $("#<%=ddltype2.ClientID %>").append("<option value=\"" + r.value[i].typeid + "\">" + r.value[i].typename + "</option>");
                    }
                    $("#<%=hidtype1.ClientID %>").val($("#<%=ddltype1.ClientID %>").val());
                    $("#<%=hidtype2.ClientID %>").val($("#<%=ddltype2.ClientID %>").val());
                }
            });

            $("#<%=ddltype2.ClientID %>").change(function() {
                $("#<%=hidtype2.ClientID %>").val($("#<%=ddltype2.ClientID %>").val());
            });
        });
    </script>
       
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
                是否关联:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:DropDownList ID="ddlSync" runat="server">
                    <asp:ListItem Text="全部" Value="0" />
                    <asp:ListItem Text="是" Value="1" />
                    <asp:ListItem Text="否" Value="2" />
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
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
            <td colspan="4" class="oddrow-l">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " OnClick="btnSerach_Click"  CssClass="widebuttons" />
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gvSupplier" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvSupplier_RowDataBound"
        PageSize="20" AllowPaging="True" OnPageIndexChanging="gvSupplier_PageIndexChanging"
         AllowSorting="true" Width="100%" PagerSettings-Mode="NumericFirstLast">
        <Columns>
            <asp:BoundField DataField="supplier_code" HeaderText="编号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" />
            <asp:TemplateField HeaderText="供应商名称" ItemStyle-Width="20%">
                <ItemTemplate>
                    <%# Eval("supplier_name") %>
                </ItemTemplate>
            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="类别" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# (int.Parse(Eval("IsPerson").ToString()) == 0 ? "公司" : "个人")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="城市" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Literal ID="litCity" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>   
                                    <asp:TemplateField HeaderText="电话" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Literal ID="litTel" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField> 

                                                                        <asp:TemplateField HeaderText="服务类型" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="labTypes" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblState" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>
            <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# "<a href='syncSupplierInfo.aspx?syncId=" + Eval("RealationId") + "&eid=" + Eval("espId") + "&sid=" + Eval("id") + "'>编辑</a>"%>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle ForeColor="Black" Font-Size="12px" Height="30px" />
    </asp:GridView>
</asp:Content>
