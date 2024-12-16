<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupWageFeeSettings.aspx.cs" MasterPageFile="~/MasterPage.master" EnableEventValidation="false" Inherits="FinanceWeb.Reports.GroupWageFeeSettings" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

<script type="text/javascript" src="/publicjs/jquery.jcarousellite.min.js"></script>

<script type="text/javascript" src="/publicjs/script_jcarousellite.js"></script>

<script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>

<script src="/public/js/dimensions.js" type="text/javascript"></script>

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>
<script type="text/javascript">
    $().ready(function() {
        $("#<%=ddltype1.ClientID %>").empty();
        $("#<%=ddltype2.ClientID %>").empty();
        Purchase_selectOperationAuditor.getalist($("#<%=hidtype.ClientID %>").val(), init1);
        function init1(r) {
            if (r.value != null)
                for (k = 0; k < r.value.length; k++) {
                if (r.value[k][0] == $("#<%=hidtype1.ClientID %>").val()) {
                    $("#<%=ddltype1.ClientID %>").append("<option value=\"" + r.value[k][0] + "\" selected>" + r.value[k][1] + "</option>");
                }
                else {
                    $("#<%=ddltype1.ClientID %>").append("<option value=\"" + r.value[k][0] + "\">" + r.value[k][1] + "</option>");
                }
            }

        }
        if ($("#<%=hidtype.ClientID %>").val() == "") {
            $("#<%=ddltype.ClientID %>").val("-1");
        }
        Purchase_selectOperationAuditor.getalist($("#<%=hidtype1.ClientID %>").val(), init2);
        function init2(r) {
            $("#<%=ddltype2.ClientID %>").empty();
//            $("#<%=ddltype2.ClientID %>").append("<option value=\"-1\" selected>请选择...</option>");
            if (r.value != null)
                for (j = 0; j < r.value.length; j++) {
                if (r.value[j][0] == $("#<%=hidtype2.ClientID %>").val()) {
                    $("#<%=ddltype2.ClientID %>").append("<option value=\"" + r.value[j][0] + "\" selected>" + r.value[j][1] + "</option>");
                }
                else {
                    $("#<%=ddltype2.ClientID %>").append("<option value=\"" + r.value[j][0] + "\">" + r.value[j][1] + "</option>");
                }
            }
        }
        $("#<%=ddltype.ClientID %>").val($("#<%=hidtype.ClientID %>").val());




        $("#<%=ddltype.ClientID %>").change(function() {
            $("#<%=hidtype.ClientID %>").val($("#<%=ddltype.ClientID %>").val());
            $("#<%=ddltype1.ClientID %>").empty();
            $("#<%=ddltype2.ClientID %>").empty();

            Purchase_selectOperationAuditor.getalist($("#<%=ddltype.ClientID %>").val(), pop1);
            function pop1(r) {
                if (r.value != null)
                    for (i = 0; i < r.value.length; i++) {
                    $("#<%=ddltype1.ClientID %>").append("<option value=\"" + r.value[i][0] + "\">" + r.value[i][1] + "</option>");
                }
            }
            $("#<%=ddltype2.ClientID %>").append("<option value='-1'>请选择...</option>");
            $("#<%=hidtype1.ClientID %>").val("-1");
            $("#<%=hidtype2.ClientID %>").val("-1");
        });

        $("#<%=ddltype1.ClientID %>").change(function() {
            $("#<%=ddltype2.ClientID %>").empty();

            Purchase_selectOperationAuditor.getalist($("#<%=ddltype1.ClientID %>").val(), pop2);
            function pop2(r) {
                if (r.value != null)
                    for (i = 0; i < r.value.length; i++) {
                    $("#<%=ddltype2.ClientID %>").append("<option value=\"" + r.value[i][0] + "\">" + r.value[i][1] + "</option>");
                }
            }
            $("#<%=hidtype1.ClientID %>").val($("#<%=ddltype1.ClientID %>").val());
            $("#<%=hidtype2.ClientID %>").val("-1");
        });

        $("#<%=ddltype2.ClientID %>").change(function() {
            $("#<%=hidtype2.ClientID %>").val($("#<%=ddltype2.ClientID %>").val());
        });

    });
</script>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                检索
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                团队名称:
            </td>
            <td class="oddrow-l" align="left""width: 35%">
            <asp:DropDownList ID="ddltype" Width="100px" runat="server"  AutoPostBack="false" />
                            &nbsp;<asp:DropDownList ID="ddltype1" runat="server" Width="100px" />
                            &nbsp;<asp:DropDownList ID="ddltype2" runat="server" Width="100px" />
                            <asp:HiddenField ID="hidtype" Value="-1" runat="server" />
                            <asp:HiddenField ID="hidtype1" Value="-1" runat="server" />
                            <asp:HiddenField ID="hidtype2" Value="-1"  runat="server" />
            </td>
            <td class="oddrow" style="width: 15%">
                年 / 月:
            </td>
            <td class="oddrow-l" align="left">
                <asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList>&nbsp;&nbsp;年&nbsp;&nbsp;
                <asp:DropDownList ID="ddlMonth" runat="server">
                    <asp:ListItem Value="0">全部</asp:ListItem>
                    <asp:ListItem Value="1">1月</asp:ListItem>
                    <asp:ListItem Value="2">2月</asp:ListItem>
                    <asp:ListItem Value="3">3月</asp:ListItem>
                    <asp:ListItem Value="4">4月</asp:ListItem>
                    <asp:ListItem Value="5">5月</asp:ListItem>
                    <asp:ListItem Value="6">6月</asp:ListItem>
                    <asp:ListItem Value="7">7月</asp:ListItem>
                    <asp:ListItem Value="8">8月</asp:ListItem>
                    <asp:ListItem Value="9">9月</asp:ListItem>
                    <asp:ListItem Value="10">10月</asp:ListItem>
                    <asp:ListItem Value="11">11月</asp:ListItem>
                    <asp:ListItem Value="12">12月</asp:ListItem>
                </asp:DropDownList>&nbsp;&nbsp;月
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " OnClick="btnSearch_Click" CssClass="widebuttons" />&nbsp;
                <asp:Button ID="btnAdd" runat="server" Text=" 添加 " OnClick="btnAdd_Click" CssClass="widebuttons" />&nbsp;
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                已设置数据列表
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvG_RowDataBound" 
                    PageSize="10" EmptyDataText="暂时没有相关记录" PagerSettings-Mode="NumericFirstLast"
                    OnPageIndexChanging="gvG_PageIndexChanging" AllowPaging="true" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="部门名称" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDepartmentName" Text='<%# Eval("deptname") %>' />
                                <asp:HiddenField runat="server" ID="hidDepID" Value='<%# Eval("deptid") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="年份" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblYear" Text='<%# Eval("Year") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="月份" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblMonth" Text='<%# Eval("Month") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TotalFee" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblTotalFee" Text='<%# Eval("TotalFee") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TotalSalary" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblTotalSalary" Text='<%# Eval("TotalSalary") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="人数" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblEmpAmount" Text='<%# Eval("EmpAmounts") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                            <asp:TemplateField HeaderText="修改" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <a title="修改" href='groupwagefeesettingsedit.aspx?salaryid=<%# Eval("ID") %>'><img src='../../images/edit.gif' /></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>