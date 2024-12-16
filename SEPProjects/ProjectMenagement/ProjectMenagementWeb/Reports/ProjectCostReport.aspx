<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectCostReport.aspx.cs" Inherits="FinanceWeb.Reports.ProjectCostReport"
    MasterPageFile="~/MasterPage.master" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="uc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
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

        $().ready(function () {
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




            $("#<%=ddltype.ClientID %>").change(function () {
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

            $("#<%=ddltype1.ClientID %>").change(function () {
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

            $("#<%=ddltype2.ClientID %>").change(function () {
                $("#<%=hidtype2.ClientID %>").val($("#<%=ddltype2.ClientID %>").val());
            });

        });
    </script>



    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">正在使用和预关闭有待支付成本的项目核查
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">按组别查询:
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
            <td class="oddrow" style="width: 15%">按日期区间:
            </td>
            <td class="oddrow-l" align="left" colspan="3">
                <asp:TextBox ID="txtBeginDate" onkeyDown="return false; " Style="cursor: pointer" runat="server"
                    onclick="setDate(this);" />-
                <asp:TextBox ID="txtEndDate" onkeyDown="return false; " Style="cursor: pointer;" runat="server"
                    onclick="setDate(this);" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">按员工编号:
            </td>
            <td class="oddrow-l" align="left" colspan="3">
                <asp:TextBox runat="server" ID="txtUserCode"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">已付截至日期:
            </td>
            <td class="oddrow-l" align="left" colspan="3">
                <asp:TextBox ID="txtPaidDate" onkeyDown="return false; " Style="cursor: pointer;" runat="server"
                    onclick="setDate(this);" />
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" Style="width: 100px"
                    OnClick="btnSearch_Click" />
                &nbsp;<asp:Button ID="btnExport" runat="server" Text=" 导出 " CssClass="widebuttons" Style="width: 100px"
                    OnClick="btnExport_Click" />
            </td>
        </tr>
    </table>

    <uc1:TabContainer ID="TabContainer1" runat="server" CssClass="AjaxTabStrip" Width="98%"
        EnableViewState="false">
        <uc1:TabPanel ID="TabPanel1" HeaderText="项目信息" runat="server">
            <ContentTemplate>
                <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="ProjectID"
                    OnRowCommand="gvG_RowCommand" OnRowDataBound="gvG_RowDataBound" PageSize="10"
                    EmptyDataText="暂时没有相关记录" PagerSettings-Mode="NumericFirstLast" OnPageIndexChanging="gvG_PageIndexChanging"
                    AllowPaging="true" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="SerialCode" HeaderText="流水号" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="8%" />
                        <asp:BoundField DataField="ProjectCode" HeaderText="项目号" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="8%" />
                        <asp:BoundField DataField="BusinessDescription" HeaderText="项目名称" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="13%" />
                        <asp:TemplateField HeaderText="负责人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%">
                            <ItemTemplate>
                                <asp:Label ID="lblName" runat="server" CssClass="userLabel" Text='<%# Eval("ApplicantEmployeeName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="业务组别" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <asp:Label ID="lblGroupName" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="BenginDate" HeaderText="开始日期" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="8%" />
                        <asp:BoundField DataField="EndDate" HeaderText="结束日期" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="8%" />
                        <asp:BoundField DataField="BusinessTypeName" HeaderText="业务类型" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="8%" />
                        <asp:BoundField DataField="ProjectTypeName" HeaderText="项目类型" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="8%" />
                        <asp:BoundField DataField="ContractStatusName" HeaderText="合同状态" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="8%" />
                        <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="labState" Text='<%#Eval("Status") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <a target="_blank" href="ProjectDisplay.aspx?<% =ESP.Finance.Utility.RequestName.ProjectID %>=<%#DataBinder.Eval(Container.DataItem,"ProjectID")%>">
                                    <img src="../images/dc.gif" border="0px;" title="查看"></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </uc1:TabPanel>
        <uc1:TabPanel ID="TabPanel2" HeaderText="第三方待支付" runat="server">
            <ContentTemplate>
            </ContentTemplate>
        </uc1:TabPanel>
        <uc1:TabPanel ID="TabPanel3" HeaderText="报销待支付" runat="server">
            <ContentTemplate>
            </ContentTemplate>
        </uc1:TabPanel>
    </uc1:TabContainer>
</asp:Content>
