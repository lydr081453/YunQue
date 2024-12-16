<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_Edit_projectInfo" Codebehind="projectInfo.ascx.cs" %>

<script language="javascript">
    function openProject() {
        var win = window.open('selectProjectList.aspx', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }

    function setProjectInfo(projectId, projectCode, projectDesc, deptList) {
        
        document.getElementById("<%=hidProejctId.ClientID %>").value = projectId;
        document.getElementById("<%=txtproject_code.ClientID %>").value = projectCode;
        document.getElementById("<%=txtproject_descripttion.ClientID %>").value = projectDesc;
        
        if (deptList != "") {
            insertDepartment(deptList);
        }
        else {
            document.getElementById("<%= ddlDepartment.ClientID %>").options.length = 0;
        }
    }

    function insertDepartment(deptList) {
        deptControl = document.getElementById("<%= ddlDepartment.ClientID %>");
        deptControl.options.length = 0;

        var depts = deptList.split('#');
        
        for (i = 0; i < depts.length; i++) {
            var option = document.createElement("OPTION");
            option.value = depts[i].split(',')[0];
            option.text = depts[i].split(',')[1];
            deptControl.options.add(option);
        }

        if (deptControl.options.length > 0) {
            document.getElementById("<%=hidDeptId.ClientID %>").value = document.getElementById("<% = ddlDepartment.ClientID %>").options[0].value + "," + document.getElementById("<% = ddlDepartment.ClientID %>").options[0].text;
        }
    }

    function clearTypes(obj) {
        document.getElementById("<%=hidDeptId.ClientID %>").value = document.getElementById("<% = ddlDepartment.ClientID %>").options[obj.selectedIndex].value + "," + document.getElementById("<% = ddlDepartment.ClientID %>").options[obj.selectedIndex].text;
    }
</script>

<table width="100%" border="0" cellpadding="0" runat="server" visible="true" id="tabTitle">
    <tr>
        <td background="../../images/allinfo_bg.gif" width="100%">
            <table width="100%" border="0" cellspacing="8" cellpadding="0">
                <tr>
                    <td width="38%" class="f_16px" style="height:20px">
                        <span class="f_12px"><strong>申请人：</strong><asp:Label ID="labTitleUser" runat="server" />
                    </td>
                    <td width="62%" align="right" class="f_16px">
                       <strong>最后编辑时间：</strong><asp:Label ID="labTitleDateTime" runat="server" /></span>
                    </td>
                </tr>
            </table>
        </td>
    </tr>    
    <tr>
        <td style="height: 15px">
        </td>
    </tr>
</table>
<table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="4">
            ① 项目信息<asp:HiddenField ID="hidProejctId" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            流水号:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="labGlideNo" runat="server" />
        </td>
        <td class="oddrow" style="width:15%">
            申请单号:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="txtprNo" runat="server"   style="width: 35%"/>
        </td>
    </tr>
    <tr>
        <td class="oddrow" >
            项目号:
        </td>
        <td class="oddrow-l" >
            <asp:TextBox ID="txtproject_code" runat="server" Width="200px" MaxLength="1" onfocus="javascript:this.blur();" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtproject_code"
                Display="None" ErrorMessage="请选择项目号"></asp:RequiredFieldValidator><font color="red">* </font><input type="button" value="请选择..." class="widebuttons" onclick="openProject();return false;" />
        </td>
        <td class="oddrow">
            项目名称:
        </td>
        <td class="oddrow-l">
            <asp:TextBox ID="txtproject_descripttion" runat="server" Width="200px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtproject_descripttion"
                Display="None" ErrorMessage="请填写项目名称"></asp:RequiredFieldValidator><font color="red">*</font>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            成本所属组:
        </td>
        <td class="oddrow-l">
            <asp:DropDownList ID="ddlDepartment" runat="server" onchange="clearTypes(this);" /><asp:HiddenField ID="hidDeptId" runat="server" />
        </td>
        <td class="oddrow">
            货币:
        </td>
        <td class="oddrow-l">
            <asp:DropDownList ID="ddlMoneyType" runat="server" />
        </td>
    </tr>
<%--    <tr>
        <td class="oddrow">
            采购物料:
        </td>
        <td class="oddrow-l">
            <asp:HiddenField ID="hidtypeIds" runat="server" />
            <asp:TextBox ID="txtThirdParty" runat="server" Width="200px" /><asp:RequiredFieldValidator
                ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtThirdParty"
                Display="None" ErrorMessage="采购物料为必填"></asp:RequiredFieldValidator><font color="red">
                    * </font><input type="button" value="请选择..." class="widebuttons" onclick="openProductTypes();return false;" />
        </td>
        <td class="oddrow">
            采购成本预算:
        </td>
        <td class="oddrow-l">
            <asp:TextBox ID="txtbuggeted" runat="server" Width="200px" MaxLength="8" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtbuggeted"
                Display="None" ErrorMessage="采购成本预算为必填"></asp:RequiredFieldValidator><font color="red">*</font>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtbuggeted"
                Display="None" ErrorMessage="采购成本预算格式错误" ValidationExpression="^(\d{1,3},?)+(\.\d+)?$"></asp:RegularExpressionValidator>
        </td>
    </tr>--%>
</table>
