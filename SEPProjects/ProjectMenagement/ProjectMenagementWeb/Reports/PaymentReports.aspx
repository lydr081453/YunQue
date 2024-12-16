<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" EnableEventValidation="false" CodeBehind="PaymentReports.aspx.cs" Inherits="FinanceWeb.Reports.PaymentReports" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

        <link href="/public/css/buttonLoading.css" rel="stylesheet" />

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script type="text/javascript" src="/publicjs/jquery.jcarousellite.min.js"></script>

    <script type="text/javascript" src="/publicjs/script_jcarousellite.js"></script>

    <script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>

    <script src="/public/js/dimensions.js" type="text/javascript"></script>

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script type="text/javascript">

        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');

        }

        function setLoading(button) {
            button.classList.add('loading');
            button.classList.add('disabled');

            setTimeout(function () {
                button.classList.remove('loading');
                button.classList.remove('disabled');
            }, 2000);
        }

        $().ready(function () {

            FinanceWeb.Reports.PaymentReports.showAlert(showalert);
            function showalert(r) {
                if (r.value == "true") {
                    alert('请定期查看应收报表相关数据！');
                }
            };

            $("#<%=ddltype1.ClientID %>").empty();
            $("#<%=ddltype2.ClientID %>").empty();
            FinanceWeb.Reports.PaymentReports.getalist($("#<%=hidtype.ClientID %>").val(), init1);
            function init1(r) {
                if (r.value != null) {
                    for (k = 0; k < r.value.length; k++) {
                        if (r.value[k][0] == $("#<%=hidtype1.ClientID %>").val()) {
                            $("#<%=ddltype1.ClientID %>").append("<option value=\"" + r.value[k][0] + "\" selected>" + r.value[k][1] + "</option>");
                        }
                        else {
                            $("#<%=ddltype1.ClientID %>").append("<option value=\"" + r.value[k][0] + "\">" + r.value[k][1] + "</option>");
                        }
                    }

                }
            }
            if ($("#<%=hidtype.ClientID %>").val() == "") {
                $("#<%=ddltype.ClientID %>").val("-1");
            }
            FinanceWeb.Reports.PaymentReports.getalist($("#<%=hidtype1.ClientID %>").val(), init2);
            function init2(r) {
                $("#<%=ddltype2.ClientID %>").empty();
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




            $("#<%=ddltype.ClientID %>").change(function () {
                $("#<%=hidtype.ClientID %>").val($("#<%=ddltype.ClientID %>").val());
                $("#<%=ddltype1.ClientID %>").empty();
                $("#<%=ddltype2.ClientID %>").empty();

                FinanceWeb.Reports.PaymentReports.getalist($("#<%=ddltype.ClientID %>").val(), pop1);
                function pop1(r) {
                    if (r.value != null)
                        for (i = 0; i < r.value.length; i++) {
                            $("#<%=ddltype1.ClientID %>").append("<option value=\"" + r.value[i][0] + "\">" + r.value[i][1] + "</option>");
                        }
                }
                $("#<%=ddltype2.ClientID %>").append("<option value='-1'>全部</option>");
                $("#<%=hidtype1.ClientID %>").val("-1");
                $("#<%=hidtype2.ClientID %>").val("-1");
            });

            $("#<%=ddltype1.ClientID %>").change(function () {
                $("#<%=ddltype2.ClientID %>").empty();

                FinanceWeb.Reports.PaymentReports.getalist($("#<%=ddltype1.ClientID %>").val(), pop2);
                function pop2(r) {
                    if (r.value != null)
                        for (i = 0; i < r.value.length; i++) {
                            $("#<%=ddltype2.ClientID %>").append("<option value=\"" + r.value[i][0] + "\">" + r.value[i][1] + "</option>");
                        }
                }
                $("#<%=hidtype1.ClientID %>").val($("#<%=ddltype1.ClientID %>").val());
                $("#<%=hidtype2.ClientID %>").val("-1");
            });

            $("#<%=ddltype2.ClientID %>").change(function () {
                $("#<%=hidtype2.ClientID %>").val($("#<%=ddltype2.ClientID %>").val());
            });

        });
    </script>


    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">检索 <asp:HiddenField runat="server" ID="hidShowAlert" Value="false" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">团队名称:
            </td>
            <td class="oddrow-l" align="left" colspan="3">
                <asp:DropDownList ID="ddltype" Width="100px" runat="server" AutoPostBack="false" />
                &nbsp;<asp:DropDownList ID="ddltype1" runat="server" Width="100px" />
                &nbsp;<asp:DropDownList ID="ddltype2" runat="server" Width="100px" />
                <asp:HiddenField ID="hidtype" Value="-1" runat="server" />
                <asp:HiddenField ID="hidtype1" Value="-1" runat="server" />
                <asp:HiddenField ID="hidtype2" Value="-1" runat="server" />
            </td>
        </tr>
        <tr>

            <td class="oddrow" style="width: 15%">实际回款日期:
            </td>
            <td class="oddrow-l" align="left">
                <asp:TextBox ID="txtBeginDate" onkeyDown="return false; " Style="cursor: hand" runat="server"
                    onclick="setDate(this);" />--
                                        <asp:TextBox ID="txtEndDate" onkeyDown="return false; " Style="cursor: hand" runat="server"
                                            onclick="setDate(this);" />
            </td>
            <td class="oddrow" style="width: 15%">关键字:
            </td>
            <td class="oddrow-l" align="left">
                <asp:TextBox runat="server" ID="txtKey"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">账龄范围:
            </td>
            <td class="oddrow-l" align="left">
                <asp:DropDownList ID="ddlMonth" runat="server" Width="30px">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="1">小于180</asp:ListItem>
                    <asp:ListItem Value="2">180至365</asp:ListItem>
                    <asp:ListItem Value="3">365至730</asp:ListItem>
                    <asp:ListItem Value="4">730以上</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="oddrow" style="width: 15%">是否包含已回款:
            </td>
            <td class="oddrow-l" align="left">
                <asp:DropDownList runat="server" ID="ddlPaid">
                    <asp:ListItem Text="导出全部" Value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="仅导出未回款" Value="1"></asp:ListItem>
                    <asp:ListItem Text="仅导出已回款" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " OnClick="btnSearch_Click" CssClass="widebuttons" OnClientClick="setLoading(this);"/>&nbsp;
                <asp:Button ID="btnExportForFinance" runat="server" Text=" 项目应收导出 " CssClass="widebuttons" OnClick="btnExportForFinance_Click" OnClientClick="setLoading(this);"
                    Style="width: 120px" />&nbsp;
                 <asp:Button ID="btnExportByMonth3" runat="server" Text=" 未来三个月预计应收导出 " CssClass="widebuttons" OnClick="btnExportByMonth3_Click" OnClientClick="setLoading(this);"
                    Style="width: 150px" />&nbsp;
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td class="heading" colspan="4">项目应收列表
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvG_RowDataBound"
                    EmptyDataText="暂时没有相关记录"  AllowPaging="false" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="ProjectId" Visible="false" />
                        <asp:BoundField DataField="PaymentId" Visible="false" />
                        <asp:BoundField DataField="PaymentCode" HeaderText="通知号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%" />
                        <asp:BoundField DataField="PaymentPreDate" HeaderText="应收日期" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}"
                            ItemStyle-Width="8%" />
                        <asp:BoundField DataField="InvoiceTitle" HeaderText="客户抬头" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" />
                        <asp:BoundField DataField="ProjectCode" HeaderText="项目号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" />
                        <asp:BoundField DataField="PaymentContent" HeaderText="描述" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%" />
                        <asp:BoundField DataField="paymentAge" HeaderText="账龄" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%" />
                        <asp:BoundField DataField="Dept" HeaderText="部门" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%" />
                        <asp:BoundField DataField="PaymentBudgetConfirm" HeaderText="应收金额" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:C}" ItemStyle-Width="8%" />
                        <asp:BoundField DataField="ApplicantEmployeeName" HeaderText="负责人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%" />
                        <asp:BoundField DataField="PaymentFactDate" HeaderText="回款日期" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" ItemStyle-Width="8%" />
                        <asp:BoundField DataField="PaymentFee" HeaderText="回款金额" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:C}" ItemStyle-Width="8%" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
