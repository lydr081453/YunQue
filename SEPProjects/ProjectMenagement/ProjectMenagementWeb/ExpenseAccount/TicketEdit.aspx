<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeBehind="TicketEdit.aspx.cs" EnableEventValidation="false" Inherits="FinanceWeb.ExpenseAccount.TicketEdit" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript" language="javascript" src="/public/js/DatePicker.js"></script>

    <link href="css/treeStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        var TopList = 10; //填充数据数量


        function openProject() {
            var win = window.open('SelectedProjectCode.aspx', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }

        function setProjectInfo(projectId, projectCode, projectDesc, deptList) {
            document.getElementById("<%=hidProejctId.ClientID %>").value = projectId;
            document.getElementById("<%=txtproject_code1.ClientID %>").value = projectCode.split('-')[0];
            document.getElementById("<%=txtproject_code2.ClientID %>").value = projectCode.split('-')[1];
            document.getElementById("<%=txtproject_code3.ClientID %>").value = projectCode.split('-')[2];
            document.getElementById("<%=txtproject_code.ClientID %>").value = projectCode.split('-')[3];
            document.getElementById("<%=txtproject_descripttion.ClientID %>").value = projectDesc;

            document.getElementById("<%=hidProject_Code1.ClientID %>").value = projectCode.split('-')[0];
            document.getElementById("<%=hidProject_Code2.ClientID %>").value = projectCode.split('-')[1];
            document.getElementById("<%=hidProject_Code3.ClientID %>").value = projectCode.split('-')[2];
            document.getElementById("<%=hidProject_Code.ClientID %>").value = projectCode.split('-')[3];
            document.getElementById("<%=hidProject_Description.ClientID %>").value = projectDesc;

            document.getElementById("<%=hidProejctIds.ClientID %>").value = deptList;
            if (deptList != "")
                insertDepartment(deptList);
            else {
                deptControl = document.getElementById("<%= ddlDepartment.ClientID %>");
                deptControl.options.length = 0;
                document.getElementById("<%=hidDeptId.ClientID %>").value = "";

                var panDetailInfo = document.getElementById("<%=panDetailInfo.ClientID %>");
                if (panDetailInfo != null) {
                    typesControl = document.getElementById("<%= ddlProjectType.ClientID %>");
                    typesControl.options.length = 0;
                    document.getElementById("<%=hidTypeId.ClientID %>").value = "";
                }
            }
        }

        function insertDepartment(deptList) {
            deptControl = document.getElementById("<%= ddlDepartment.ClientID %>");
            var depts = deptList.split('#');
            deptControl.options.length = 0;
            for (i = 0; i < depts.length; i++) {
                var option = document.createElement("OPTION");
                option.value = depts[i].split(',')[0];
                option.text = depts[i].split(',')[1];
                deptControl.options.add(option);
            }
            if (deptControl.options.length > 0)
                document.getElementById("<%=hidDeptId.ClientID %>").value = document.getElementById("<% = ddlDepartment.ClientID %>").options[0].value + "," + document.getElementById("<% = ddlDepartment.ClientID %>").options[0].text;


            var panDetailInfo = document.getElementById("<%=panDetailInfo.ClientID %>");
            if (panDetailInfo != null)
                insertTypes(document.getElementById("<%=hidProejctId.ClientID %>").value, document.getElementById("<%=hidDeptId.ClientID %>").value.split(',')[0]);

        }

        function clearTypes(obj) {
            document.getElementById("<%=hidDeptId.ClientID %>").value = document.getElementById("<% = ddlDepartment.ClientID %>").options[obj.selectedIndex].value + "," + document.getElementById("<% = ddlDepartment.ClientID %>").options[obj.selectedIndex].text;

            var panDetailInfo = document.getElementById("<%=panDetailInfo.ClientID %>");
            if (panDetailInfo != null)
                insertTypes(document.getElementById("<%=hidProejctId.ClientID %>").value, document.getElementById("<%=hidDeptId.ClientID %>").value.split(',')[0]);
        }

        function clearTypes2(obj) {

            document.getElementById("<%=hidTypeId.ClientID %>").value = document.getElementById("<%= ddlProjectType.ClientID %>").options[obj.selectedIndex].value + "," + document.getElementById("<%= ddlProjectType.ClientID %>").options[obj.selectedIndex].text;
            var typeIsMatch = document.getElementById("<%=hidTypeIsMatch.ClientID %>").value;
            if (typeIsMatch == "True") {
                alert("请注意，您已更改了系统自动匹配出的项目物料类别！");
            }

        }

        function insertTypes(projectid, groupid) {

            FinanceWeb.ExpenseAccount.ExpenseAccountEdit.getTypeList(parseInt(projectid), parseInt(groupid), popdrp);
            function popdrp(r) {
                $("#<%=ddlProjectType.ClientID %>").empty();

                if (r != null) {
                    for (i = 0; i < r.value.length; i++) {
                        $("#<%=ddlProjectType.ClientID %>").append("<option value=\"" + r.value[i].toString().split(',')[0] + "\">" + r.value[i].toString().split(',')[1] + "</option>");
                    }

                    document.getElementById("<%=hidTypeId.ClientID %>").value = document.getElementById("<%= ddlProjectType.ClientID %>").options[0].value + "," + document.getElementById("<%= ddlProjectType.ClientID %>").options[0].text;
                }
            }

        }


        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }

        function DropDownListValidate(src, args) {
            var val = args.Value; //或者cmbRequestUnit.value;
            args.IsValid = (val != "-1"); //当值为-1的时候，提示出错
        }

        function CheckForm() {

            var expenseType = document.getElementById("<%=hidExpenseTypeId.ClientID %>").value.trim();; //document.getElementById("<%//=drpExpenseType.ClientID %>").options[document.getElementById("<%//=drpExpenseType.ClientID %>").selectedIndex].value;
            var panTicketCancel = document.getElementById("<%=panTicketCancel.ClientID %>");

            var errorMessage = "";

            if (document.getElementById("<%=txtSource.ClientID %>").value == "") {
                errorMessage += "-- 请填写出发城市!\n";
            }
            if (document.getElementById("<%=txtDestination.ClientID %>").value == "") {
                errorMessage += "-- 请填写目的城市!\n";
            }
            if (document.getElementById("<%=hidBoarder.ClientID %>").text == "") {
                errorMessage += "-- 请填写登机人!\n";
            }
            if (document.getElementById("<%=hidIDCard.ClientID %>").value == "") {
                errorMessage += "-- 请填写登机人的身份证号!\n";
            }
            if (document.getElementById("<%=txtPhone.ClientID %>").value == "") {
                errorMessage += "-- 请填写登机人联系电话!\n";
            }
            if (document.getElementById("<%=txtAirTime.ClientID %>").value == "") {
                errorMessage += "-- 请填写出发日期!\n";
            }
            if (document.getElementById("<%=txtAirNo.ClientID %>").value == "") {
                errorMessage += "-- 请填写航班号!\n";
            }
            if (document.getElementById("<%=txtPrices.ClientID %>").value == "") {
                errorMessage += "-- 请填写价格!\n";
            }
            if (document.getElementById("<%=ddlSeat.ClientID %>").value == "0") {
                errorMessage += "-- 请选择仓位!\n";
            }
            if (panTicketCancel != null) {
                if (document.getElementById("<%=txtCancelPrice.ClientID %>").value == "") {
                    errorMessage += "-- 请填写退票相关费用!\n";
                }
            }
            if (errorMessage != "") {
                alert(errorMessage);
                return false;
            } else {
                return true;
            }

        }

        function BoarderClick() {
            var win = window.open('TicketDlg.aspx', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }

        function getValue(url) {
            var xmlHttpRequest;
            var str = "";
            if (window.XMLHttpRequest) //For general cases. 
            {
                xmlHttpRequest = new XMLHttpRequest();
                xmlHttpRequest.open('post', url, false);
                xmlHttpRequest.setRequestHeader("Content-type", "application/x-www-form-urlencoded;");
                xmlHttpRequest.send('');
                if (xmlHttpRequest.status == 200)
                    str = xmlHttpRequest.responseText;
            }
            else //For IE. 
            {
                if (window.ActiveXObject) {
                    xmlHttpRequest = new ActiveXObject("Microsoft.XMLHTTP");
                    xmlHttpRequest.open("get", url, false);
                    xmlHttpRequest.send();
                    str = xmlHttpRequest.responseText;
                }
            }
            return str;
        }

        function aa(tr) {
            tr.bgColor = "#B8C2D3";
        }
        function bb(tr) {
            tr.bgColor = "#ffffff";
        }
        function completeField(tdvalue, obj, id) {
            document.getElementById(obj).value = id;
            document.getElementById("divResult").style.display = "none";

        }
        function clearValue() {

            document.getElementById("divResult").innerHTML = "";

            document.getElementById("divResult").style.border = "none";

        }
        function calculateOffset(field, attr) {

            var offset = 0;
            while (field) {
                offset += field[attr];

                field = field.offsetParent;
            }
            return offset;
        }

        function KeyDown(e) {
            var val;
            if (!e) {
                var e = window.event;
            }

            if (e.keyCode) {
                val = e.keyCode;
            }
            else if (e.which) {
                val = e.which;
            }
            if (val == 32) {
                val = 0;
                if (window.event)
                    window.event.returnValue = false;
                else
                    e.preventDefault();
                return false;
            }
        }

        function isIncSym(ui) {
            var valid = /[\ '\ "\,\ <\> \+\-\*\/\%\^\=\\\!\&\|\(\)\[\]\{\}\:\;\~\`\#\$]+/;
            return (valid.test(ui));
        }

        function procBusiness(http, obj) {
            document.getElementById("divResult").style.display = "";
            var divResult = document.getElementById("divResult");
            var p = document.getElementById(obj.id).value;

            var regS = new RegExp(p, "gi");
            http = http.substr(1);
            http = http.replace(regS, p); //全部替换


            var response = http.split(',');
            var str = "";
            str += "<table width=\"100%\" style=\"background-color:#ffffff;\">";
            for (var i = 0; i < response.length; i++) {

                var ppstr = response[i].split('|');
                str += "<tr onmouseover=\"aa(this);\" onmouseout=\"bb(this);\"><td onclick=\"completeField(this,'" + obj.id + "','" + ppstr[0] + "');\" style=\"cursor:pointer;\">" + ppstr[0] + "</td></tr>";
            }
            divResult.style.width = document.getElementById(obj.id).offsetWidth; +"px";
            var left = calculateOffset(document.getElementById(obj.id), "offsetLeft");
            var top = calculateOffset(document.getElementById(obj.id), "offsetTop") + document.getElementById(obj.id).offsetHeight;
            divResult.style.border = "black 1px solid";
            divResult.style.left = left + "px";
            divResult.style.top = top + "px";


            divResult.innerHTML = str;
            if (document.getElementById(obj.id).value == "" || response.length == 0) {
                clearValue();
            }
        }

        function btnOnClick(obj) {
            var name = document.getElementById("divResult");
            var txtValue = document.getElementById(obj.id).value;
            if (txtValue == "")
                return;
            if (!isIncSym(txtValue)) {
                var str = getValue("/SearchKeyWord.aspx?name=" + escape(txtValue) + "&top=" + TopList);
                if (str != "") {
                    str = procBusiness(str, obj);
                }
                else {
                    clearValue();
                }
            }
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="divResult" style="position: absolute; font-size: 12px; background-color: #ffffff;">
            </div>
            <table width="100%" class="tableForm">
                <tr>
                    <td class="heading" colspan="4">报销单信息<asp:HiddenField ID="hidProejctId" runat="server" />
                        <asp:HiddenField ID="hidProejctIds" runat="server" />
                        &nbsp;&nbsp;<asp:Label runat="server" ID="lblReturnCode"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="oddrow" width="15%">申请人:
                    </td>
                    <td class="oddrow-l" width="35%">
                        <asp:Label runat="server" ID="labRequestUserName" />(<asp:Label runat="server" ID="labRequestUserCode" />)
                    </td>
                    <td class="oddrow" width="15%">申请日期:
                    </td>
                    <td class="oddrow-l" width="35%">
                        <asp:Label runat="server" ID="labRequestDate" />
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">项目号:
                    </td>
                    <td class="oddrow-l">
                        <asp:TextBox ID="txtproject_code1" runat="server" Width="25px" Enabled="false" />
                        <asp:HiddenField ID="hidProject_Code1" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtproject_code1"
                            Display="None"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                runat="server" ControlToValidate="txtproject_code1" Display="Dynamic" ErrorMessage="项目号格式错误"
                                ValidationExpression="^[A-Za-z]{1,1}$"></asp:RegularExpressionValidator>
                        -&nbsp;<asp:TextBox ID="txtproject_code2" MaxLength="3" runat="server" Width="50px"
                            Enabled="false" />
                        <asp:HiddenField ID="hidProject_Code2" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtproject_code2"
                            Display="None"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator3"
                                runat="server" ControlToValidate="txtproject_code2" Display="Dynamic" ErrorMessage="项目号格式错误"
                                ValidationExpression="^[A-Za-z&*]{3,3}|[0-9]{3,3}$"></asp:RegularExpressionValidator>
                        -&nbsp;<asp:TextBox ID="txtproject_code3" MaxLength="1" runat="server" Width="25px"
                            Enabled="false" />
                        <asp:HiddenField ID="hidProject_Code3" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtproject_code3"
                            Display="None"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator4"
                                runat="server" ControlToValidate="txtproject_code3" Display="Dynamic" ErrorMessage="项目号格式错误"
                                ValidationExpression="^[A-Za-z*]{1,1}$"></asp:RegularExpressionValidator>
                        -&nbsp;<asp:TextBox ID="txtproject_code" MaxLength="8" runat="server" Width="63px"
                            Enabled="false" />
                        <asp:HiddenField ID="hidProject_Code" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtproject_code"
                            Display="None" ErrorMessage="项目号为必填"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtproject_code"
                                Display="Dynamic" ErrorMessage="项目号格式错误" ValidationExpression="^[0-9]{7,7}[A-Za-z*]{1,1}|[0-9*]{7,7}|[0-9]{4,4}[*]{3,3}$"></asp:RegularExpressionValidator>
                        <font color="red">* </font>
                        <input type="button" value="请选择..." class="widebuttons" runat="server" id="btnOpenProject"
                            onclick="openProject(); return false;" />
                    </td>
                    <td class="oddrow">项目名称:
                    </td>
                    <td class="oddrow-l">
                        <asp:TextBox ID="txtproject_descripttion" runat="server" Width="80%" Enabled="false" />
                        <asp:HiddenField ID="hidProject_Description" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">成本所属组:
                    </td>
                    <td class="oddrow-l">
                        <asp:DropDownList ID="ddlDepartment" runat="server" onchange="clearTypes(this);" />
                        <asp:HiddenField ID="hidDeptId" runat="server" />
                    </td>
                    <td class="oddrow">预计报销总金额:
                    </td>
                    <td class="oddrow-l">
                        <asp:Label runat="server" ID="labPreFee" /><asp:HiddenField runat="server" ID="hidPreFee" />
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">费用描述:
                    </td>
                    <td colspan="3" class="oddrow-l">
                        <asp:TextBox runat="server" ID="txtContent" Width="80%" Height="80px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="heading" colspan="4">审批信息<asp:HiddenField ID="HiddenField3" runat="server" />
                        <asp:HiddenField ID="HiddenField4" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">审批记录:
                    </td>
                    <td class="oddrow-l" colspan="3">
                        <asp:Label runat="server" ID="labSuggestion" />
                    </td>
                </tr>
            </table>
            <asp:Panel runat="server" ID="panDetailInfo">
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="tableForm">
                    <tr>
                        <td>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td class="heading">费用明细
                                    </td>
                                    <td class="heading" align="right">
                                        <asp:Button ID="btnSetAuditor" runat="server" Text="下一步" CssClass="widebuttons" OnClick="btnSetAuditor_Click" />&nbsp;&nbsp;
                                        <asp:Button ID="btnReturn" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnReturn_Click"
                                            CausesValidation="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" class="oddrow-l" colspan="2">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="tableForm">
                                            <tr>
                                                <td class="oddrow" width="15%">费用类型:
                                                </td>
                                                <td class="oddrow-l" width="35%">
                                                    <asp:Label ID="labExpenseTypeName" runat="server" Text="请选择.." />
                                                    <asp:HiddenField runat="server" ID="hidExpenseTypeId" />
                                                    <asp:HiddenField runat="server" ID="hidGasCostByKM" />
                                                </td>
                                                <td class="oddrow">物料类别:
                                                </td>
                                                <td class="oddrow-l">
                                                    <asp:DropDownList ID="ddlProjectType" runat="server" onchange="clearTypes2(this);" />
                                                    <asp:HiddenField ID="hidTypeId" runat="server" />
                                                    <asp:HiddenField ID="hidTypeIsMatch" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="oddrow">成本使用情况:
                                                </td>
                                                <td class="oddrow-l" colspan="3">
                                                    <asp:Label runat="server" ID="lblCalculate" /><br />
                                                    <font color="red">单据只有在提交时才会真正占用成本，此时显示的成本使用情况并不涉及其他人正在编辑的报销单，因此请及时提交单据。</font>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="oddrow-l" colspan="4"></td>
                                            </tr>
                                            <tr>
                                                <td class="oddrow">登机人:
                                                </td>
                                                <td class="oddrow-l">
                                                    <asp:Label runat="server" ID="lblBoarder"></asp:Label>&nbsp;<asp:HiddenField runat="server"
                                                        ID="hidBoarderId" />
                                                    <asp:HiddenField runat="server" ID="hidBoarder" />
                                                    <asp:HiddenField runat="server" ID="hidIDCard" Value="身份证" />
                                                    <asp:HiddenField runat="server" ID="hidCardType" />
                                                    <asp:Button runat="server" ID="btnBoarder" OnClientClick="BoarderClick();" CssClass="widebuttons"
                                                        Text=" 变更登机人 " />
                                                </td>
                                                <td class="oddrow">电话:
                                                </td>
                                                <td class="oddrow-l">
                                                    <asp:TextBox runat="server" ID="txtPhone"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="oddrow-l" colspan="4">
                                                    <table width="100%" border="1">
                                                        <tr bgcolor="#EAEDF1">
                                                            <td align="left">出发地(中文)
                                                            </td>
                                                            <td align="left">目的地(中文)
                                                            </td>
                                                            <td align="left">出发日期
                                                            </td>
                                                            <td align="left">航班号
                                                            </td>
                                                            <td align="left">价格(含机场建设费和燃油税)
                                                            </td>
                                                            <td>仓位
                                                            </td>
                                                            <td align="left">备注
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox runat="server" ID="txtSource" onkeydown='javascript:KeyDown(this);'
                                                                    onkeyup='javascript:btnOnClick(this);' AutoComplete="off"></asp:TextBox><font color="red">*</font>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" ID="txtDestination" onkeydown='javascript:KeyDown(this);'
                                                                    onkeyup='javascript:btnOnClick(this);' AutoComplete="off"></asp:TextBox><font color="red">*</font>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtAirTime" runat="server" onclick="setDate(this);" onfocus="javascript:this.blur();"></asp:TextBox><font
                                                                    color="red">*</font>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" ID="txtAirNo"></asp:TextBox><font color="red">*</font>
                                                            </td>
                                                            <td>
                                                                <ComponentArt:NumberInput ID="txtPrices" runat="server" EmptyText="0.00" DecimalDigits="2"
                                                                    NumberType="Number">
                                                                </ComponentArt:NumberInput>
                                                                <font color="red">*</font>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList runat="server" ID="ddlSeat">
                                                                    <asp:ListItem Text="请选择..." Selected="True" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="头等舱" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="商务舱" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="经济舱" Value="3"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <font color="red">*</font>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" ID="txtRemark"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <asp:Panel ID="panTicketCancel" runat="server" Visible="false">
                                                <tr>
                                                    <td class="oddrow">退票手续费:
                                                    </td>
                                                    <td class="oddrow-l" colspan="3">
                                                        <asp:TextBox runat="server" ID="txtCancelPrice"></asp:TextBox><font color="red">*</font>
                                                    </td>
                                                </tr>
                                            </asp:Panel>
                                            <tr>
                                                <td class="oddrow-l" colspan="2" style="height: 40px; border-right: 0px; border-right-color: White">
                                                    <asp:Button ID="btnAddDetail" runat="server" CssClass="widebuttons" OnClientClick="return CheckForm();"
                                                        OnClick="btnAddDetail_Click" />&nbsp;&nbsp;
                                                    <asp:Button ID="btnNew" runat="server" Text=" 新建 " CssClass="widebuttons" OnClick="btnNew_Click"
                                                        Visible="false" />&nbsp;&nbsp;
                                                </td>
                                                <td class="oddrow-l" colspan="2" style="height: 40px; text-align: right; border-left: 0px; border-left-color: White"></td>
                                            </tr>
                                            <tr>
                                                <td class="heading" colspan="4">申请单明细
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="85%" class="oddrow-l" colspan="4">
                                                    <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="ID,ReturnID"
                                                        OnRowCommand="gvG_RowCommand" OnRowDataBound="gvG_RowDataBound" EmptyDataText="暂时没有相关记录"
                                                        OnRowCreated="gvG_RowCreated" AllowPaging="false" Width="80%">
                                                        <Columns>
                                                            <asp:BoundField DataField="ExpenseDate" HeaderText="发生日期" ItemStyle-HorizontalAlign="Center"
                                                                DataFormatString="{0:d}" ItemStyle-Width="8%" />
                                                            <asp:TemplateField HeaderText="物料类别" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="labCostDetailName" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="费用描述" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblExpenseDesc" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Boarder" HeaderText="登机人" ItemStyle-HorizontalAlign="center"
                                                                ItemStyle-Width="5%" />
                                                            <asp:TemplateField HeaderText="员工编号" ItemStyle-HorizontalAlign="center" ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblUserCode" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="BoarderIDCard" HeaderText="ID" ItemStyle-HorizontalAlign="center"
                                                                ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="GoAirNo" HeaderText="航班号" ItemStyle-HorizontalAlign="center"
                                                                ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="BackAirNo" HeaderText="仓位" ItemStyle-HorizontalAlign="center"
                                                                ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="BoarderMobile" HeaderText="联系电话" ItemStyle-HorizontalAlign="center"
                                                                ItemStyle-Width="8%" />
                                                            <asp:BoundField DataField="TicketSource" HeaderText="出发地" ItemStyle-HorizontalAlign="center"
                                                                ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="TicketDestination" HeaderText="目的地" ItemStyle-HorizontalAlign="center"
                                                                ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="ExpenseTypeNumber" HeaderText="数量" ItemStyle-HorizontalAlign="Right"
                                                                ItemStyle-Width="5%" />
                                                            <asp:TemplateField HeaderText="总金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblExpenseMoney" Text='<%# Eval("ExpenseMoney") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="3%" HeaderText="编辑">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="hylEdit" runat="server" Text="<img title='编辑' src='../../images/edit.gif' border='0' />"
                                                                        CausesValidation="false" CommandArgument='<%# Eval("ID")%>' CommandName="Modify" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="3%" HeaderText="改签">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="hylUpdate" runat="server" Text="<img title='改签' src='../../images/edit.gif' border='0' />"
                                                                        CausesValidation="false" CommandArgument='<%# Eval("ID")%>' CommandName="Update" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="删除" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center"
                                                                ItemStyle-Width="3%">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnDelete" runat="server" Text="<img title='删除' src='../../images/disable.gif' border='0' />"
                                                                        OnClientClick="return confirm('您是否确认删除？');" CausesValidation="false" CommandArgument='<%# Eval("ID") %>'
                                                                        CommandName="Del" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="3%" HeaderText="退票">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="hylCancel" runat="server" Text="<img title='退票' src='../../images/edit.gif' border='0' />"
                                                                        CausesValidation="false" CommandArgument='<%# Eval("ID")%>' CommandName="Cancel" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel runat="server" ID="panAudit">
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="tableForm">

                    <tr>
                        <td class="heading" colspan="4">供应商信息<asp:HiddenField ID="hidSupplierAccount" runat="server" />
                            <asp:HiddenField ID="hidSupplierBank" runat="server" />
                            <asp:HiddenField ID="hidSupplierId" runat="server" />
                            <asp:HiddenField ID="hidReceptionId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 15%">供应商名称:
                        </td>
                        <td class="oddrow-l" colspan="3">
                            <asp:DropDownList runat="server" ID="ddlSupplier" AutoPostBack="true" OnSelectedIndexChanged="ddlSupplier_OnSelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 15%">供应商地址:
                        </td>
                        <td class="oddrow-l" colspan="3">
                            <asp:Label runat="server" ID="lblAddress"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 15%">联系人姓名:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:Label runat="server" ID="lblContacter"></asp:Label>
                        </td>
                        <td class="oddrow" style="width: 15%">电话:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:Label runat="server" ID="lblTel"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 15%">邮箱:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:Label runat="server" ID="lblMail"></asp:Label>
                        </td>
                        <td class="oddrow" style="width: 15%">手机:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:Label runat="server" ID="lblMobile"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" width="15%">
                            <asp:Label runat="server" ID="lblSuggestion">审批备注</asp:Label>
                        </td>
                        <td class="oddrow-1" colspan="3">
                            <asp:TextBox runat="server" ID="txtSuggestion" TextMode="MultiLine" Width="80%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnAuditConfirm" runat="server" Text=" 确认 " CssClass="widebuttons"
                                OnClick="btnAuditConfirm_Click" />&nbsp;&nbsp;
                            <asp:Button ID="btnAuditCancel" runat="server" Text=" 驳回 " CssClass="widebuttons"
                                OnClick="btnAuditCancel_Click" />&nbsp;&nbsp;
                            <asp:Button ID="btnAuditReturn" runat="server" Text=" 返回 " CssClass="widebuttons"
                                OnClick="btnAuditReturn_Click" CausesValidation="false" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="tableForm"
                runat="server" id="table2">
                <tr>
                    <td class="oddrow-l" colspan="4">
                        <asp:Button ID="btnSave2" runat="server" Text=" 创建 " CssClass="widebuttons" OnClick="btnSave2_Click" />&nbsp;&nbsp;
                        <asp:Button ID="btnReturn2" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnReturn2_Click"
                            CausesValidation="false" />
                    </td>
                </tr>
            </table>
            <asp:ValidationSummary runat="server" ID="ValidationSummary1" ShowMessageBox="true"
                ShowSummary="false" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
