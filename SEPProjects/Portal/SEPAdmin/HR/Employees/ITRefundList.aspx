<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ITRefundList.aspx.cs" EnableEventValidation="false" MasterPageFile="~/MasterPage.master" Inherits="SEPAdmin.HR.Employees._ITRefundList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" src="/public/js/jquery.js"></script>

    <script language="javascript" type="text/javascript" src="../../public/js/dialog.js"></script>

    <script language="javascript" type="text/javascript" src="../../public/js/jquery-ui.js"></script>

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script type="text/javascript">

        $().ready(function() {
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




            $("#<%=ddltype.ClientID %>").change(function() {
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

            $("#<%=ddltype1.ClientID %>").change(function() {
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

            $("#<%=ddltype2.ClientID %>").change(function() {
                $("#<%=hidtype2.ClientID %>").val($("#<%=ddltype2.ClientID %>").val());
                var ddl = document.getElementById("<%=ddltype2.ClientID %>");
                var deptname = "";
                if (ddl.options[ddl.selectedIndex].text != "请选择...") {
                    deptname = ddl.options[ddl.selectedIndex].text;
                    document.getElementById("<% =hidGroupName.ClientID %>").value = deptname;
                }
            });

        });

        function onPageSubmit() { };
        $(document).ready(function() {

            $("#btnAddUsersDialog").click(function() {
                if (!document.getElementById("btnAddUsersDialog").__sep_dialog) {
                    document.getElementById("btnAddUsersDialog").__sep_dialog = $("#userList").dialog({
                        modal: true, overlay: { opacity: 0.5, background: "black" },
                        height: 500, width: 800,
                        buttons: {
                            "取消": function() { $(this).dialog("close"); },
                            "确定": function() {
                                if (confirm('确定添加笔记本租赁？')) {
                                    var buttonId = $("#btnAddUsersDialog").attr("server-button-id");
                                    var hiddenId = $("#btnAddUsersDialog").attr("server-hidden-id");
                                    var selects = document.getElementById("userListFrame").contentWindow.__sep_dialogReturnValue;
                                    if (selects && selects != "") {
                                        $("#" + hiddenId).val(selects);
                                        //alert(document.getElementById(hiddenId).value);
                                        __doPostBack(buttonId, "");
                                    }

                                    $(this).dialog("close");
                                }
                            }
                        }
                    });
                }
                document.getElementById("btnAddUsersDialog").__sep_dialog.dialog("open");
            });
        });

        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }

        function deleteRefundInfo() {
            if (confirm('您确定要删除此条笔记本租赁记录？'))
                return true;

            return false;
        }
    </script>

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                员工检索
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">
                关键字:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtKeyword" runat="server" />
            </td>
              <td class="oddrow" style="width: 10%">
                部门：
            </td>
            <td class="oddrow-l" >
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
            <td class="oddrow" style="width: 10%">
                租赁状态:
            </td>
            <td class="oddrow-l">
                <asp:DropDownList ID="ddlStatus" Width="100px" runat="server" AutoPostBack="false">
                    <asp:ListItem Text="全部" Value="" Selected="True" />
                    <asp:ListItem Text="未启动租赁" Value="0" />
                    <asp:ListItem Text="租赁进行中" Value="1" />
                    <asp:ListItem Text="租赁已结束" Value="2" />
                </asp:DropDownList>
            </td>
           <td class="oddrow" style="width: 10%">
                租赁类型:
            </td>
            <td class="oddrow-l">
                <asp:DropDownList ID="ddlProductType" Width="100px" runat="server" AutoPostBack="false">
                    <asp:ListItem Text="全部" Value="" Selected="True" />
                    <asp:ListItem Text="个人使用" Value="1" />
                    <asp:ListItem Text="公共物品" Value="2" />
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td class="oddrow-l" colspan="8">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
    <br />
    <table style="width: 100%;">
        <tr>
            <td>
                <table width="100%" id="tableUp">
                    <tr>
                        <td>
                            <div>
                                <asp:LinkButton ID="lbAddUsers" PostBackUrl="~/HR/Employees/RefundAdd.aspx" Text="添加租赁人员"
                                    CssClass="widebuttons" runat="server" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" id="tabTop" runat="server">
                                <tr>
                                    <td width="50%">
                                        <asp:Panel ID="PageTop" runat="server">
                                            <asp:Button ID="btnFirst2" runat="server" CssClass="widebuttons" Text="首页" OnClick="btnFirst_Click" />&nbsp;
                                            <asp:Button ID="btnPrevious2" runat="server" CssClass="widebuttons" Text="上页" OnClick="btnPrevious_Click" />&nbsp;
                                            <asp:Button ID="btnNext2" runat="server" CssClass="widebuttons" Text="下页" OnClick="btnNext_Click" />&nbsp;
                                            <asp:Button ID="btnLast2" runat="server" CssClass="widebuttons" Text="末页" OnClick="btnLast_Click" />&nbsp;
                                            跳转到第<asp:DropDownList ID="ddlCurrentPage2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            页
                                        </asp:Panel>
                                    </td>
                                    <td align="right" class="recordTd">
                                        记录数:<asp:Label ID="labAllNumT" runat="server" />&nbsp;页数:<asp:Label ID="labPageCountT"
                                            runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                PageSize="20" OnPageIndexChanging="gvList_PageIndexChanging" OnRowDataBound="gvList_RowDataBound"
                                DataKeyNames="id,status" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="code" HeaderText="工号" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="姓名" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("lastnamecn").ToString()%><%# Eval("firstnamecn").ToString() %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="部门" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("level3") == null ? "" : Eval("level3").ToString()%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ProductName" HeaderText="资产名称" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="ProductNo" HeaderText="资产编号" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="日租费" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("Cost") == null || string.IsNullOrEmpty(Eval("Cost").ToString()) ? "" : string.Format(Eval("Cost").ToString(),"#,##0.00")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="类型" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("Type").ToString()=="1" ? "笔记本租赁" : "公共资产"%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="登记起始" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("BeginTime") == null || string.IsNullOrEmpty(Eval("BeginTime").ToString()) ? "" : DateTime.Parse(Eval("BeginTime").ToString()).ToString("yyyy-MM-dd")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="登记结束" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("EndTime") == null || string.IsNullOrEmpty(Eval("EndTime").ToString()) ? "" : DateTime.Parse(Eval("EndTime").ToString()).ToString("yyyy-MM-dd")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Creator" HeaderText="租赁登记人" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
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
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div style="width: 0px; height: 0px; overflow: hidden" class="oddrow">
        <div id="userList">
            <iframe src="UserList.aspx" id="userListFrame" height="90%" width="100%"></iframe>
        </div>
    </div>
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
</asp:Content>
