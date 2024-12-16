<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" MasterPageFile="~/MasterPage.master" CodeBehind="DimissionList.aspx.cs" Inherits="SEPAdmin.Talents.DimissionList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" src="/public/js/jquery.js"></script>
   <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script type="text/javascript">

        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }
        $().ready(function() {
            $("#<%=ddltype2.ClientID %>").empty();

            SEPAdmin.Talents.DimissionList.getalist($("#<%=hidtype.ClientID %>").val(), init1);
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
            SEPAdmin.Talents.DimissionList.getalist($("#<%=hidtype1.ClientID %>").val(), init2);
            function init2(r) {
                $("#<%=ddltype2.ClientID %>").empty();
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

                SEPAdmin.Talents.DimissionList.getalist($("#<%=ddltype.ClientID %>").val(), pop1);
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

                SEPAdmin.Talents.DimissionList.getalist($("#<%=ddltype1.ClientID %>").val(), pop2);
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

    </script>

    <table width="100%" border="0" class="tableForm">
        <tr>
            <td class="heading" colspan="6">
                员工检索
            </td>
        </tr>
        <tr>
          <td class="oddrow" style="width: 10%">
                姓名:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtuserName" runat="server" />
            </td>
            <td class="oddrow" style="width: 10%">
                职位名称:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtPosition" runat="server" />
            </td>
            <td class="oddrow" style="width: 10%">
                部门：
            </td>
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
            <td class="oddrow" style="width: 10%">
                学 历:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtEducation" runat="server" />
            </td>
            <td class="oddrow" style="width: 10%">
                工作年限:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtYear" runat="server" />
            </td>
            <td class="oddrow" style="width: 10%">
                离职日期：
            </td>
            <td class="oddrow-l">
               <asp:TextBox ID="txtDimission" runat="server" onclick="setDate(this);" />
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="8">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click" />&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    <table style="width: 100%;">
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
                <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvList_RowDataBound"
                    PageSize="20" AllowPaging="True" EnableViewState="false" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="UserCode" HeaderText="工号" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="UserName" HeaderText="姓名" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="DeptName" HeaderText="部门" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="DepartmentPositionName" HeaderText="职位" ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="入职日期" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblJoinDate"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="离职日期" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDimissionDate"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Phone2" HeaderText="联系方式" ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="工作年限" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblWorkYear"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="简历" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                              <a href="TalentEdit.aspx?bakurl=DimissionList.aspx&userid=<%# Eval("UserID").ToString() %>">
                                    <img src="/images/edit.gif" border="0px;"></a>
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
    </table>
</asp:Content>
