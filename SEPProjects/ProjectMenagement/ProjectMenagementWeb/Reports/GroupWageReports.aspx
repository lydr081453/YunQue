<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupWageReports.aspx.cs"
    MasterPageFile="~/MasterPage.master" Inherits="FinanceWeb.Reports.GroupWageReports"
    EnableEventValidation="false" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <link href="/public/css/buttonLoading.css" rel="stylesheet" />

    <script type="text/javascript">
        function setLoading(button) {
                button.classList.add('loading');
                button.classList.add('disabled');
               
                setTimeout(function () {
                    button.classList.remove('loading');
                    button.classList.remove('disabled');
                }, 2000);
        }



        function validation() {
            var beginyear = document.getElementById("ctl00_ContentPlaceHolder1_ddlYear").value;
            var beginmonth = document.getElementById("ctl00_ContentPlaceHolder1_ddlMonth").value;
            var endyear = document.getElementById("ctl00_ContentPlaceHolder1_ddlEndYear").value;
            var endmonth = document.getElementById("ctl00_ContentPlaceHolder1_ddlEndMonth").value;
            if (beginyear > endyear || (beginyear == endyear && beginmonth > endmonth)) {
                    alert("请选择正确的时间范围！");
                    return false;                
            }
            return true;
        }


        $().ready(function() {
            $("#<%=ddltype1.ClientID %>").empty();
            $("#<%=ddltype2.ClientID %>").empty();
            Purchase_selectOperationAuditor.getalist($("#<%=hidtype.ClientID %>").val(), init1);
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
            <td class="oddrow" style="width: 15%">
                日期范围:
            </td>
            <td class="oddrow-l" align="left" colspan="3">
                <asp:DropDownList ID="ddlYear" runat="server" Width="30px"></asp:DropDownList>&nbsp;
                <asp:DropDownList ID="ddlMonth" runat="server" Width="30px">
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
                </asp:DropDownList>
                <%--<asp:TextBox ID="txtBeginDate" onkeyDown="return false; " Style="cursor: hand" runat="server"
                    onclick="setDate(this);" />--%>&nbsp;&nbsp;至&nbsp;&nbsp;
                <%--<asp:TextBox ID="txtEndDate" onkeyDown="return false; " Style="cursor: hand" runat="server"
                    onclick="setDate(this);" />--%>                    
                <asp:DropDownList ID="ddlEndYear" runat="server" Width="30px"></asp:DropDownList>&nbsp;
                <asp:DropDownList ID="ddlEndMonth" runat="server" Width="30px">
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
                </asp:DropDownList>
            </td>
        </tr>
        <tr> 
            <td class="oddrow-l" colspan="4">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " OnClick="btnSearch_Click" CssClass="widebuttons"
                    OnClientClick="if(validation()==true){ setLoading(this);}" />&nbsp;
                <asp:Button ID="btnExportForFinance" runat="server" Text=" 团队报告 " CssClass="widebuttons" CausesValidation="false"
                    Style="width: 80px" OnClick="btnExportForFinance_Click" OnClientClick="if(validation()==true){ setLoading(this);}" />&nbsp;
                <asp:Button ID="btnExportForGroup" runat="server" Text=" 项目报告 " CssClass="widebuttons" CausesValidation="false"
                    Style="width: 80px" OnClick="btnExportForGroup_Click" OnClientClick="if(validation()==true){ setLoading(this);}" />&nbsp;
                       <asp:Button ID="btnCostView" runat="server" Text=" 项目成本垫付报告 " CssClass="widebuttons" CausesValidation="false"
                    Style="width: 180px" OnClick="btnCostView_Click" OnClientClick="if(validation()==true){ setLoading(this);}" />&nbsp;
                 <asp:Button ID="btnCostViewSave" Visible="false" runat="server" Text=" 第三方成本一键保存 " CssClass="widebuttons" CausesValidation="false"
                    Style="width: 180px" OnClick="btnCostViewSave_Click" OnClientClick="if(validation()==true){ setLoading(this);}" />&nbsp;
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                部门月成本列表
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvG_RowDataBound"
                    PageSize="20" EmptyDataText="暂时没有相关记录" PagerSettings-Mode="NumericFirstLast"
                    OnPageIndexChanging="gvG_PageIndexChanging" AllowPaging="true" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="ProjectCode" HeaderText="项目号" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="businessdescription" HeaderText="项目名称" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="applicantEmployeeName" HeaderText="负责人" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="deptname" HeaderText="成本所属组" ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="项目总额" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblTotalAmount" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="成本总额" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblCostTotal" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="已使用成本" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblCostUsed" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="成本结余" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblCostBalance" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="毛利率" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblFeeRate" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="服务费" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblFee" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
