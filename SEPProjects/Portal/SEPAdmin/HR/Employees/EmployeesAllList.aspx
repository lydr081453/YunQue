<%@ Page Language="C#" AutoEventWireup="true" Inherits="Employees_EmployeesAllList" EnableEventValidation="false" MasterPageFile="~/MasterPage.master" CodeBehind="EmployeesAllList.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link rel="stylesheet" href="/public/css/jquery-ui-new.css">
    <script type="text/javascript" src="/public/js/jquery-3.7.1.js"></script>
    <script src="/public/js/jquery-ui-new.js"></script>
    <script src="/public/js/jquery.tabs.pack.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function () {
            $.datepicker.setDefaults($.datepicker.regional["zh-CN"]);
            $("#" + "<%=txtContractBegin.ClientID%>").datepicker({
            changeMonth: true,
            changeYear: true
        });
        $("#" + "<%=txtContractEnd.ClientID%>").datepicker({
            changeMonth: true,
            changeYear: true
        });
        $("#" + "<%=txtBirthdayBegin.ClientID%>").datepicker({
            changeMonth: true,
            changeYear: true
        });
        $("#" + "<%=txtBirthdayEnd.ClientID%>").datepicker({
            changeMonth: true,
            changeYear: true
        });
        $("#" + "<%=txtProbationBegin.ClientID%>").datepicker({
            changeMonth: true,
            changeYear: true
        });
        $("#" + "<%=txtProbationEnd.ClientID%>").datepicker({
            changeMonth: true,
            changeYear: true
        });
    });

    $().ready(function () {
        //     $("#<%=ddltype1.ClientID %>").empty();
        $("#<%=ddltype2.ClientID %>").empty();
        Employees_EmployeesAllList.getalist($("#<%=hidtype.ClientID %>").val(), init1);
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
        Employees_EmployeesAllList.getalist($("#<%=hidtype1.ClientID %>").val(), init2);
        function init2(r) {
            $("#<%=ddltype2.ClientID %>").empty();
            //            $("#<%=ddltype2.ClientID %>").append("<option value=\"-1\" selected>请选择...</option>");
            if (r.value != null)
                for (j = 0; j < r.value.length; j++) {
                    if (r.value[j][0] == $("#<%=hidtype2.ClientID %>").val()) {
                        $("#<%=ddltype2.ClientID %>").append("<option value=\"" + r.value[j][0] + "\" selected>" + r.value[j][1] + "</option>");
                    document.getElementById("<% =hidGroupName.ClientID %>").value = r.value[j][1];
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

            Employees_EmployeesAllList.getalist($("#<%=ddltype.ClientID %>").val(), pop1);
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

            Employees_EmployeesAllList.getalist($("#<%=ddltype1.ClientID %>").val(), pop2);
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
            var ddl = document.getElementById("<%=ddltype2.ClientID %>");
            var deptname = "";
            if (ddl.options[ddl.selectedIndex].text != "请选择...") {
                deptname = ddl.options[ddl.selectedIndex].text;
                document.getElementById("<% =hidGroupName.ClientID %>").value = deptname;
            }
        });

    });

    </script>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="6">员工检索
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">员工编号:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtITCode" runat="server" />
            </td>
            <td class="oddrow" style="width: 10%">姓名:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtuserName" runat="server" />
            </td>
            <td class="oddrow" style="width: 10%">部门：</td>
            <td class="oddrow-l">
                <asp:DropDownList ID="ddltype" Width="100px" runat="server" AutoPostBack="false" />
                &nbsp;<asp:DropDownList ID="ddltype1" runat="server" Width="100px" />
                &nbsp;<asp:DropDownList ID="ddltype2" runat="server" Width="100px" />
                <asp:HiddenField ID="hidtype" Value="-1" runat="server" />
                <asp:HiddenField ID="hidtype1" Value="-1" runat="server" />
                <asp:HiddenField ID="hidtype2" Value="-1" runat="server" />
                <asp:HiddenField ID="hidGroupName" Value="" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">合同公司:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtContractCompany" runat="server" />
            </td>
            <td class="oddrow" style="width: 10%">工资所属公司:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtThisYearSalary" runat="server" />
            </td>
            <td class="oddrow" style="width: 10%">毕业院校：</td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtFinishSchool" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">合同到期日:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtContractBegin" runat="server" onkeyDown="return false; "  />
                -
                <asp:TextBox ID="txtContractEnd" runat="server" onkeyDown="return false; "  />
            </td>
            <td class="oddrow" style="width: 10%">生日区间:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtBirthdayBegin" runat="server" onkeyDown="return false; "  />
                -
                <asp:TextBox ID="txtBirthdayEnd" runat="server" onkeyDown="return false; "  />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">试用期至:
            </td>
            <td class="oddrow-l" colspan="5">
                <asp:TextBox ID="txtProbationBegin" runat="server" onkeyDown="return false; "  />
                -
                <asp:TextBox ID="txtProbationEnd" runat="server" onkeyDown="return false; "  />
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="8">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnPrint" runat="server" Text=" 导出 " CssClass="widebuttons" OnClick="btnPrint_Click" />
            </td>
        </tr>
    </table>
    <br />
    <table style="width: 100%;">
        <tr>
            <td>
                <%--            <table width="100%"  id="tableUp"> 

                    <tr>
                        <td >--%>
                <table width="100%" id="tabTop" runat="server">
                    <tr>
                        <td width="50%">
                            <asp:Panel ID="PageTop" runat="server">
                                <asp:Button ID="btnFirst2" runat="server" CssClass="widebuttons" Text="首页" OnClick="btnFirst_Click" />&nbsp;
                                <asp:Button ID="btnPrevious2" runat="server" CssClass="widebuttons" Text="上页" OnClick="btnPrevious_Click" />&nbsp;
                                <asp:Button ID="btnNext2" runat="server" CssClass="widebuttons" Text="下页" OnClick="btnNext_Click" />&nbsp;
                                <asp:Button ID="btnLast2" runat="server" CssClass="widebuttons" Text="末页" OnClick="btnLast_Click" />&nbsp;
                                跳转到第<asp:DropDownList ID="ddlCurrentPage2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                                </asp:DropDownList>页
                            </asp:Panel>
                        </td>
                        <td align="right" class="recordTd">记录数:<asp:Label ID="labAllNumT" runat="server" />&nbsp;页数:<asp:Label ID="labPageCountT"
                            runat="server" />
                        </td>
                    </tr>
                </table>
                <%--                </td>
                    </tr>          
                    <tr>
                        <td >--%>
                <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="20"
                    OnRowDataBound="gvList_RowDataBound" OnPageIndexChanging="gvList_PageIndexChanging" DataKeyNames="userid,status,EmployeesInPositionsList" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="code" HeaderText="工号" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="fullnamecn" HeaderText="员工中文姓名" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="fullnameen" HeaderText="员工英文姓名" ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="部门职位" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Repeater ID="repJob" runat="server">
                                    <ItemTemplate>
                                        部门：<%# Eval("CompanyName") == null ? "" : Eval("CompanyName").ToString() %>--
                                  <%# Eval("DepartmentName") == null ? "" : Eval("DepartmentName").ToString()%>--
                                  <%# Eval("GroupName") == null ? "" : Eval("GroupName").ToString() %>&nbsp;&nbsp;
                                  职位：<%# Eval("DepartmentPositionName").ToString()%><br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="入职日期" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# Eval("EmployeeJobInfo.joinDate") == null || DateTime.Parse(Eval("EmployeeJobInfo.joinDate").ToString()).ToString("yyyy-MM-dd") == "1900-01-01"  ? "" : DateTime.Parse(Eval("EmployeeJobInfo.joinDate").ToString()).ToString("yyyy-MM-dd")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
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
                        <td align="right" class="recordTd">记录数:<asp:Label ID="labAllNum" runat="server" />&nbsp;页数:<asp:Label ID="labPageCount"
                            runat="server" />
                        </td>
                    </tr>
                </table>

            </td>
        </tr>
    </table>
</asp:Content>

