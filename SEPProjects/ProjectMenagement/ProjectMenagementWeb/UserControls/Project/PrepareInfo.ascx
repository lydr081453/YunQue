<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_Project_PrepareInfo" CodeBehind="PrepareInfo.ascx.cs" %>

<script type="text/javascript" src="/public/js/DatePicker.js"></script>

<script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

<script language="javascript" src="/public/js/jquery.blockUI.js"></script>

<script language="javascript">

    function setDate(obj) {
        popUpCalendar(obj, obj, 'yyyy-mm-dd');
    }

    function BizClick() {
        var win = window.open('/Dialogs/BizDescriptionDlg.aspx', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }
    function BDProjectSelect() {
        var win = window.open('/Dialogs/ProjectDlg.aspx', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }

    function ApplicantClick() {
        var username = document.getElementById("<% =txtApplicant.ClientID %>").value;
        var sysid = document.getElementById("<% =hidApplicantID.ClientID %>").value;
        var dept = document.getElementById("<% =hidGroupID.ClientID %>").value;
        username = encodeURIComponent ? encodeURIComponent(username) : escape(username);
        var win = window.open('/Dialogs/EmployeeList.aspx?showSelectAll=hidden&<% =ESP.Finance.Utility.RequestName.SearchType %>=Applicant&UserSysID=' + sysid + '&<% =ESP.Finance.Utility.RequestName.UserName %>=' + username + '&<% =ESP.Finance.Utility.RequestName.DeptID %>=' + dept, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }
    
    function BusinessPersonClick() {
            var win = window.open('/Dialogs/EmployeeList.aspx?showSelectAll=hidden&<% =ESP.Finance.Utility.RequestName.SearchType %>=BusinessPerson', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }

    $().ready(function () {

        $("#<%=ddlContactStatus.ClientID %>").empty();

        UserControls_Project_PrepareInfo.getContactStatus(initContract);
        function initContract(r) {
            if (r.value != null)
                for (k = 0; k < r.value.length; k++) {
                    if (r.value[k][0] + ',' + r.value[k][1] == $("#<%=hidContactStatus.ClientID %>").val()) {
                        $("#<%=ddlContactStatus.ClientID %>").append("<option value=\"" + r.value[k][0] + "\" selected>" + r.value[k][1] + "</option>");
                    }
                    else {
                        $("#<%=ddlContactStatus.ClientID %>").append("<option value=\"" + r.value[k][0] + "\">" + r.value[k][1] + "</option>");
                    }
                }
        }

        $("#<%=ddltype1.ClientID %>").empty();
        $("#<%=ddltype2.ClientID %>").empty();
        UserControls_Project_PrepareInfo.getalist($("#<%=hidtype.ClientID %>").val(), init1);
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
        UserControls_Project_PrepareInfo.getalist($("#<%=hidtype1.ClientID %>").val(), init2);
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

            UserControls_Project_PrepareInfo.getalist($("#<%=ddltype.ClientID %>").val(), pop1);
            function pop1(r) {
                if (r.value != null)
                    for (i = 0; i < r.value.length; i++) {
                        $("#<%=ddltype1.ClientID %>").append("<option value=\"" + r.value[i][0] + "\">" + r.value[i][1] + "</option>");
                    }
            }
            $("#<%=hidtype1.ClientID %>").val("-1");
            $("#<%=hidtype2.ClientID %>").val("-1");

        });

        $("#<%=ddltype1.ClientID %>").change(function () {
            $("#<%=ddltype2.ClientID %>").empty();

            UserControls_Project_PrepareInfo.getalist($("#<%=ddltype1.ClientID %>").val(), pop2);
            function pop2(r) {
                if (r.value != null) {
                    for (i = 0; i < r.value.length; i++) {
                        $("#<%=ddltype2.ClientID %>").append("<option value=\"" + r.value[i][0] + "\">" + r.value[i][1] + "</option>");
                    }
                }
                if (r.value.length == 1) {
                    document.getElementById("<% =hidGroupID.ClientID %>").value = document.getElementById("<% =hidtype1.ClientID %>").value;
                    document.getElementById("<% =txtGroup.ClientID %>").value = document.getElementById("<% =ddltype1.ClientID %>").options[document.getElementById("<% =ddltype1.ClientID %>").selectedIndex].innerHTML;
                    document.getElementById("divDept").style.display = "none";
                }
            }
            $("#<%=hidtype1.ClientID %>").val($("#<%=ddltype1.ClientID %>").val());
            $("#<%=hidtype2.ClientID %>").val("-1");

        });

        $("#<%=ddltype2.ClientID %>").change(function () {
            $("#<%=hidtype2.ClientID %>").val($("#<%=ddltype2.ClientID %>").val());
            document.getElementById("<% =hidGroupID.ClientID %>").value = document.getElementById("<% =hidtype2.ClientID %>").value;
            document.getElementById("<% =txtGroup.ClientID %>").value = document.getElementById("<% =ddltype2.ClientID %>").options[document.getElementById("<% =ddltype2.ClientID %>").selectedIndex].innerHTML;
            document.getElementById("divDept").style.display = "none";
        });
        
    });


    function selectContactStatus(id, text) {
        if (id == "-1") {
            document.getElementById("<% =hidContactStatus.ClientID %>").value = "";
        }
        else
            document.getElementById("<% =hidContactStatus.ClientID %>").value = id + "," + text;
    }

    function DeptClick() {
        if (document.getElementById("divDept").style.display == "" || document.getElementById("divDept").style.display == "none") {
            document.getElementById("divDept").style.display = "block";
        }
        else {
            document.getElementById("divDept").style.display = "none";
        }

    }
    function RelevanceProjectCode(obj) {
        UserControls_Project_PrepareInfo.SetRelevanceProjectId($(obj).val(), function (val) {
            if (val.value == "error") {
                alert("关联项目号错误");
                $(obj).val('');
                $("#<%= hidRelevanceProjectId.ClientID%>").val('');
        }
        else
            $("#<%= hidRelevanceProjectId.ClientID%>").val(val.value);
        });
    }

</script>
<table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="4">① 项目准备信息<a name="top_A" />
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 10%">确认项目号:
        </td>
        <td class="oddrow-l" style="width: 40%">
            <asp:Label ID="lblProjectCode" runat="server">（财务填写）</asp:Label>
            <asp:TextBox ID="txtProjectCode" runat="server" Visible="false"></asp:TextBox>
        </td>
                        <td class="oddrow">关联项目号：
        </td>
        <td class="oddrow-l">
                                <asp:TextBox ID="txtRelevanceProjectCode" runat="server" onchange="RelevanceProjectCode(this);" />
                    <asp:HiddenField ID="hidRelevanceProjectId" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="oddrow">项目类型:
        </td>
        <td class="oddrow-l">
            <asp:HiddenField ID="hidProjectTypeCode" runat="server" />
            <asp:DropDownList ID="ddlProjectType" runat="server" AutoPostBack="true" style="width:100px;">
            </asp:DropDownList>&nbsp;<asp:DropDownList ID="ddlTypeLevel2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTypeLevel2_SelectedIndexChanged" style="width:100px;">
            </asp:DropDownList>&nbsp;<asp:DropDownList ID="ddlTypeLevel3" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTypeLevel3_SelectedIndexChanged" style="width:130px;" />
            <font color="red">*</font>
        </td>
                <td class="oddrow" style="width: 10%">合同状态:
        </td>
        <td class="oddrow-l">
            <asp:DropDownList ID="ddlContactStatus" runat="server">
            </asp:DropDownList>
            <input type="hidden" id="hidContactStatus" runat="server" /><font color="red">*</font>
        </td>

    </tr>

    <tr>
        <td class="oddrow">负责人:
        </td>
        <td class="oddrow-l">
            <asp:TextBox ID="txtApplicant" runat="server" onkeyDown="return false; " Style="cursor: pointer;"></asp:TextBox><input type="button"
                id="btnApplicant" onclick="return ApplicantClick();" runat="server" class="widebuttons" value="  选择  " /><input
                    type="hidden" id="hidApplicantID" runat="server" />
            <font color="red">*</font>
            <input type="hidden" id="hidApplicantUserID" runat="server" />
            <input type="hidden" id="hidApplicantUserCode" runat="server" />
        </td>
                <td class="oddrow">业务类型:
        </td>
        <td class="oddrow-l">
            <asp:DropDownList runat="server" ID="ddlBizType" Enabled="false">
                <asp:ListItem Text="请选择..." ></asp:ListItem>
                <asp:ListItem Text="数智营销服务" Value="1"></asp:ListItem>
                <asp:ListItem Text="数字广告服务" Selected="True" Value="2"></asp:ListItem>
                <asp:ListItem Text="数据技术产品服务" Value="3"></asp:ListItem>
            </asp:DropDownList>&nbsp;<font color="red">*</font>
        </td>
        <%--        <td class="oddrow">相关BD项目号:
        </td>
        <td class="oddrow-l">
            <asp:Label runat="server" ID="lblBdProjectCode"></asp:Label>
        </td>--%>
    </tr>
    <tr>
            <td class="oddrow">商务负责人:
        </td>
        <td class="oddrow-l">
            <asp:TextBox ID="txtBusinessPersonName" runat="server"  onfocus="this.blur();" Style="cursor: pointer;"></asp:TextBox><input type="button"
                id="Button1" onclick="return BusinessPersonClick();" runat="server" class="widebuttons" value="  选择  " /><input
                    type="hidden" id="hidBusinessPersonId" runat="server" />
            <font color="red">*</font>
            </td>
    
                <td class="oddrow">
            项目组别:
        </td>
        <td class="oddrow-l">
                        <asp:TextBox ID="txtGroup" runat="server" onkeyDown="return false; " Style="cursor: hand"></asp:TextBox><input type="button" id="btnGroup"
                onclick="return DeptClick();" runat="server" class="widebuttons" value="  变更  " /><input type="hidden"
                    id="hidGroupID" runat="server" />&nbsp;<font color="red">*</font>
            <div id="divDept" style="display: none">
                <asp:DropDownList ID="ddltype" Width="100px" runat="server" AutoPostBack="false" />
                <asp:DropDownList ID="ddltype1" runat="server" Width="100px" />
                <asp:DropDownList ID="ddltype2" runat="server" Width="100px" />
                <font color="red">*</font>
            </div>
            <asp:HiddenField ID="hidtype" Value="-1" runat="server" />
            <asp:HiddenField ID="hidtype1" Value="-1" runat="server" />
            <asp:HiddenField ID="hidtype2" Value="-1" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="oddrow">项目名称:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:TextBox ID="txtBizDesc" runat="server" Width="70%" MaxLength="100"></asp:TextBox>&nbsp;<font color="red">*</font>
        </td>
    </tr>
    <tr>
        <td class="oddrow">品牌:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:TextBox ID="txtBrands" runat="server" Width="70%"></asp:TextBox>&nbsp;<font color="red">*</font>
        </td>
    </tr>
        <tr>
        <td class="oddrow">广告主账户ID:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:TextBox ID="txtAdvertiserID" runat="server" Width="70%"></asp:TextBox>
        </td>
    </tr>
        <tr>
        <td class="oddrow">客户项目编号:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:TextBox ID="txtCustomerProjectCode" runat="server" Width="70%" ></asp:TextBox>
        </td>
    </tr>
</table>
