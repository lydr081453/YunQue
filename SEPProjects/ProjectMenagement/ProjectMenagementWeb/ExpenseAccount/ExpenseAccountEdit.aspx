<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExpenseAccountEdit.aspx.cs"
    Inherits="FinanceWeb.ExpenseAccount.ExpenseAccountEdit" MasterPageFile="~/MasterPage.master"
    EnableEventValidation="false" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <link href="css/treeStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        function openProject() {
            var win = window.open('SelectedProjectCode.aspx', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }

        function setProjectInfo(projectId, projectCode, projectDesc, deptList) {
            document.getElementById("<%=hidProejctId.ClientID %>").value = projectId;
            document.getElementById("<%=txtproject_code1.ClientID %>").value = projectCode;
            document.getElementById("<%=txtproject_descripttion.ClientID %>").value = projectDesc;

            document.getElementById("<%=hidProject_Code1.ClientID %>").value = projectCode;
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
            //alert(document.getElementById("<%=hidTypeId.ClientID %>").value);
            var typeIsMatch = document.getElementById("<%=hidTypeIsMatch.ClientID %>").value;
            //alert(typeIsMatch);
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
            var panMealFee = document.getElementById("<%=panMealFee.ClientID %>");
            var panPhone = document.getElementById("<%=panPhone.ClientID %>");
            var panOvertimeMeal = document.getElementById("<%=panOvertimeMeal.ClientID %>");
            var panThirdParty = document.getElementById("<%=panThirdParty.ClientID %>");
            var panTicket = document.getElementById("<%=panTicket.ClientID %>");
            var panTicketCancel = document.getElementById("<%=panTicketCancel.ClientID %>");


            var expenseDesc = document.getElementById("<%=txtExpenseDesc.ClientID%>").value;
            var errorMessage = "";

            if (expenseType == "" || expenseType == "0") {
                errorMessage += "-- 请在左侧类型树中选择费用类型!\n";
            }
            if (panTicket == null) {
                var expenseMoney = document.getElementById("<%=txtExpenseMoney.ClientID%>").value;
                var expenseDate = document.getElementById("<%=txtExpenseDate.ClientID %>").value.trim();
                if (expenseDate == "") {
                    errorMessage += "-- 请选择费用发生日期!\n";
                }
                if (expenseMoney == "" || expenseMoney < 0) {
                    errorMessage += "-- 报销金额必须大于0!\n";
                }
            }

            if (panMealFee != null) {
                //                var expenseMoney = document.getElementById("<%=txtExpenseMoney.ClientID%>").value;
                //                if (!document.getElementById("<%=chkMealFee1.ClientID %>").checked && !document.getElementById("<%=chkMealFee2.ClientID %>").checked && !document.getElementById("<%=chkMealFee3.ClientID %>").checked) {
                //                    errorMessage += "-- 请选择餐费类型!\n";
                //                }
                //                var totalfee = 0;
                //                if (document.getElementById("<%=chkMealFee1.ClientID %>").checked) {
                //                    totalfee += 20;
                //                }
                //                if (document.getElementById("<%=chkMealFee2.ClientID %>").checked) {
                //                    totalfee += 40;
                //                }
                //                if (document.getElementById("<%=chkMealFee3.ClientID %>").checked) {
                //                    totalfee += 40;
                //                }
                //                document.getElementById("<%=mealMaxTotalFee.ClientID %>").value = totalfee;
                //                if (document.getElementById("<%=txtExpenseMoney.ClientID %>").value > totalfee) {
                //                    errorMessage += "-- 出差餐费报销金额超出可报销额度" + totalfee + "!\n";
                //                }

            }

            if (panPhone != null) {

            }


            if (panOvertimeMeal != null) {
                if (document.getElementById("<%=txtExpenseMoney.ClientID %>").value > 50) {
                    errorMessage += "-- OT餐费报销金额超出可报销额度!\n";
                }
            }
            if (panTicket == null) {
                if (expenseDesc == "") {
                    errorMessage += "-- 请填写费用描述!\n";
                }
            }
            if (panThirdParty != null) {
                if (document.getElementById("<%=txtRecipient.ClientID %>").value == "") {
                    errorMessage += "-- 请填写收款人!\n";
                }
                if (document.getElementById("<%=txtCity.ClientID %>").value == "") {
                    errorMessage += "-- 请填写所在城市!\n";
                }
                if (document.getElementById("<%=txtBankName.ClientID %>").value == "") {
                    errorMessage += "-- 请填写银行名称!\n";
                }
                if (document.getElementById("<%=txtBankAccountNo.ClientID %>").value == "") {
                    errorMessage += "-- 请填写银行账号!\n";
                }
            }
            if (panTicket != null) {
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
                if (document.getElementById("<%=txtPrice.ClientID %>").value == "") {
                    errorMessage += "-- 请填写价格!\n";
                }

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

        function CalculateMoney() {
            var expenseType = document.getElementById("<%=hidExpenseTypeId.ClientID %>").value.trim();
            var gasCostByKM = document.getElementById("<%=hidGasCostByKM.ClientID %>").value.trim();
            var expenseNumber = document.getElementById("<%=txtNumber.ClientID%>").value;
            if (expenseNumber == "") {
                expenseNumber = 1;
            }

            if (expenseType != "" && expenseType == "73") {
                if (gasCostByKM != "") {
                    try {
                        document.getElementById("<%=txtExpenseMoney.ClientID%>").value = (gasCostByKM * expenseNumber);
                    }
                    catch (e) {
                        document.getElementById("<%=txtExpenseMoney.ClientID%>").value = (gasCostByKM * 1);
                        txtNumber.set_value(1);
                    }
                }
            }
        }

        function BoarderClick() {
            var win = window.open('TicketDlg.aspx', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }

    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%" class="tableForm">
                <tr>
                    <td class="heading" colspan="4">报销单信息<asp:HiddenField ID="hidProejctId" runat="server" />
                        <asp:HiddenField ID="hidProejctIds" runat="server" />
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
                <asp:Panel runat="server" ID="panBank">
                <tr>
                    <td class="oddrow" width="15%">开户银行:
                    </td>
                    <td class="oddrow-l" width="35%">
                        <asp:TextBox runat="server" ID="txtCreatorBank" Width="50%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCreatorBank" ErrorMessage="请填写当前申请人的开户银行，详细到支行"
                            Display="None"></asp:RequiredFieldValidator> <font color="red">* </font><br />
                         <span style="color: red;">请填写完整的开户银行信息（例：招商银行北京朝外大街支行）</span>
                    </td>
                    <td class="oddrow" width="15%">银行账号:
                    </td>
                    <td class="oddrow-l" width="35%">
                        <asp:TextBox runat="server" ID="txtCreatorAccount" Width="50%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCreatorAccount" ErrorMessage="请填写银行账号"
                            Display="None"></asp:RequiredFieldValidator>
                        <font color="red">* </font>
                    </td>
                </tr>
                </asp:Panel>
                <tr>
                    <td class="oddrow">项目号:
                    </td>
                    <td class="oddrow-l">
                        <asp:TextBox ID="txtproject_code1" runat="server" Width="50%" Enabled="false" />
                        <asp:HiddenField ID="hidProject_Code1" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtproject_code1" ErrorMessage="请选择项目号"
                            Display="None"></asp:RequiredFieldValidator>
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
                    <td class="oddrow">审批记录:
                    </td>
                    <td class="oddrow-l" colspan="3">
                        <asp:Label runat="server" ID="labSuggestion" />
                    </td>
                </tr>
            </table>
            <asp:Panel runat="server" ID="panDetailInfo">
                <table width="100%" style="height: 100%;" border="0" cellpadding="0" cellspacing="0"
                    class="tableForm">
                    <tr>
                        <td>
                            <table width="100%" style="height: 100%;" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td class="heading">费用类型
                                    </td>
                                    <td class="heading">费用明细
                                    </td>
                                    <td class="heading" align="right">
                                        <asp:Button ID="btnSetAuditor" runat="server" Text="下一步" CssClass="widebuttons" OnClick="btnSetAuditor_Click" />&nbsp;&nbsp;
                                        <asp:Button ID="btnReturn" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnReturn_Click"
                                            CausesValidation="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="220" valign="top" class="oddrow-l2">
                                        <div id="divgrid" style="overflow: auto; height: 400;">
                                            <asp:TextBox runat="server" ID="txtNodeName" />&nbsp;<asp:ImageButton ID="bthSearchNode"
                                                runat="server" ToolTip="搜索" ImageUrl="~/ExpenseAccount/images/t2_03-07.jpg" ImageAlign="AbsMiddle"
                                                OnClick="bthSearchNode_Click" /><asp:ImageButton ID="btnClearNode" runat="server"
                                                    ToolTip="清空" ImageUrl="~/ExpenseAccount/images/t2_03-07_2.jpg" ImageAlign="AbsMiddle"
                                                    OnClick="btnClearNode_Click" />
                                            <!-- 人力节点树 -->
                                            <%--<ComponentArt:TreeView ID="userTreeView" Height="400" Width="220" DragAndDropEnabled="false"
                                            NodeEditingEnabled="false" KeyboardEnabled="true" CssClass="TreeView" NodeCssClass="TreeNode"
                                            SelectedNodeCssClass="SelectedTreeNode" HoverNodeCssClass="HoverTreeNode" NodeEditCssClass="NodeEdit"
                                            LineImageWidth="19" LineImageHeight="20" DefaultImageWidth="16" DefaultImageHeight="16"
                                            ItemSpacing="0" ImagesBaseUrl="images/" NodeLabelPadding="3" ShowLines="true"
                                            LineImagesFolderUrl="images/lines/" EnableViewState="true" runat="server" OnNodeSelected="userTreeView_NodeSelected">
                                        </ComponentArt:TreeView>--%>
                                            <asp:TreeView ID="userTreeView" runat="server" ShowExpandCollapse="true" CssClass="TreeView"
                                                ForeColor="Black" Height="400" Width="220" OnSelectedNodeChanged="userTreeView_NodeSelected">
                                            </asp:TreeView>
                                        </div>
                                    </td>
                                    <td valign="top" class="oddrow-l" colspan="2">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="tableForm">
                                            <tr>
                                                <td class="oddrow" width="15%">费用类型:
                                                </td>
                                                <td class="oddrow-l" width="35%">
                                                    <asp:Label ID="labExpenseTypeName" runat="server" Text="请选择.." /><br />
                                                    <font color="red">
                                                        <asp:Label ID="lblNotify" runat="server" /></font>
                                                    <asp:HiddenField runat="server" ID="hidExpenseTypeId" />
                                                    <asp:HiddenField runat="server" ID="hidGasCostByKM" />
                                                </td>
                                                <td class="oddrow">数量:
                                                </td>
                                                <td class="oddrow-l">
                                                    <ComponentArt:NumberInput ID="txtNumber" runat="server" EmptyText="1.00">
                                                        <ClientEvents>
                                                            <Blur EventHandler="CalculateMoney" />
                                                        </ClientEvents>
                                                    </ComponentArt:NumberInput>

                                                    <asp:Label runat="server" ID="labGasByKM" />
                                                </td>
                                            </tr>
                                            <asp:Panel runat="server" ID="panMealFee">
                                                <tr>
                                                    <td class="oddrow">&nbsp;
                                                    </td>
                                                    <td class="oddrow-l" colspan="3">
                                                        <asp:CheckBox runat="server" ID="chkMealFee1" Text="早餐" />&nbsp;
                                                        <asp:CheckBox runat="server" ID="chkMealFee2" Text="午餐" />&nbsp;
                                                        <asp:CheckBox runat="server" ID="chkMealFee3" Text="晚餐" />
                                                        <asp:HiddenField runat="server" ID="mealMaxTotalFee" />
                                                    </td>
                                                </tr>
                                            </asp:Panel>
                                            <asp:Panel runat="server" ID="panOvertimeMeal">
                                            </asp:Panel>
                                            <asp:Panel runat="server" ID="panPhone">
                                                <tr>
                                                    <td class="oddrow">预计报销手机费年月:
                                                    </td>
                                                    <td class="oddrow-l" colspan="3">
                                                        <asp:DropDownList runat="server" ID="drpPhoneYear" OnSelectedIndexChanged="drpPhoneYear_SelectedIndexChanged"
                                                            AutoPostBack="true">
                                                        </asp:DropDownList>
                                                        年 &nbsp;
                                                        <asp:DropDownList runat="server" ID="drpPhoneMonth" OnSelectedIndexChanged="drpPhoneMonth_SelectedIndexChanged"
                                                            AutoPostBack="true">
                                                        </asp:DropDownList>
                                                        月<br />
                                                        <font color='red'>
                                                            <asp:Label runat="server" ID="lblIphone"></asp:Label></font>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="oddrow">手机发票类型:
                                                    </td>
                                                    <td class="oddrow-l" colspan="3">
                                                        <asp:DropDownList runat="server" ID="ddlPhoneInvoice">
                                                            <asp:ListItem Text="普通发票" Value="1" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="电子发票" Value="2"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox runat="server" ID="txtInvoiceNo" MaxLength="8" /><asp:TextBox runat="server" ID="txtInvoiceNo2" MaxLength="8" /><asp:TextBox runat="server" ID="txtInvoiceNo3" MaxLength="8" /><br />
                                                        电子发票最多可输入3张，每张需输入8位发票号
                                                    </td>
                                                </tr>
                                            </asp:Panel>
                                            <tr runat="server" id="trTotalAmount">
                                                <td class="oddrow" width="15%">费用发生日期:
                                                </td>
                                                <td class="oddrow-l" width="35%">
                                                    <asp:TextBox ID="txtExpenseDate" runat="server" onclick="setDate(this);" onfocus="javascript:this.blur();"></asp:TextBox><font
                                                        color="red">*</font>
                                                </td>
                                                <td class="oddrow">总金额:
                                                </td>
                                                <td class="oddrow-l">
                                                    <%--<ComponentArt:NumberInput ID="txtExpenseMoney" runat="server" EmptyText="0.00" DecimalDigits="2"
                                                        NumberType="Number">
                                                    </ComponentArt:NumberInput>--%>
                                                    <asp:TextBox ID="txtExpenseMoney" runat="server"></asp:TextBox>
                                                    <font color="red">*</font>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="oddrow">费用描述:
                                                </td>
                                                <td class="oddrow-l">
                                                    <asp:TextBox runat="server" ID="txtExpenseDesc" TextMode="MultiLine" Rows="2" Width="80%"
                                                        MaxLength="1500" /><font color="red">*</font>
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
                                            <asp:Panel runat="server" ID="panThirdParty">
                                                <tr>
                                                    <td class="oddrow">收款人:
                                                    </td>
                                                    <td class="oddrow-l">
                                                        <asp:TextBox ID="txtRecipient" runat="server" MaxLength="50"></asp:TextBox><font
                                                            color="red">*</font>
                                                    </td>
                                                    <td class="oddrow">所在城市:
                                                    </td>
                                                    <td class="oddrow-l">
                                                        <asp:TextBox ID="txtCity" runat="server" MaxLength="50"></asp:TextBox><font color="red">*</font>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="oddrow">银行名称:
                                                    </td>
                                                    <td class="oddrow-l">
                                                        <asp:TextBox ID="txtBankName" runat="server" MaxLength="200"></asp:TextBox><font
                                                            color="red">*</font>
                                                    </td>
                                                    <td class="oddrow">银行账号:
                                                    </td>
                                                    <td class="oddrow-l">
                                                        <asp:TextBox ID="txtBankAccountNo" runat="server" MaxLength="50"></asp:TextBox><font
                                                            color="red">*</font>
                                                    </td>
                                                </tr>
                                            </asp:Panel>
                                            <asp:Panel runat="server" ID="panTicket">
                                                <tr>
                                                    <td class="oddrow">登机人:
                                                    </td>
                                                    <td class="oddrow-l">
                                                        <asp:Label runat="server" ID="lblBoarder"></asp:Label>&nbsp;<asp:HiddenField runat="server"
                                                            ID="hidBoarderId" />
                                                        <asp:HiddenField runat="server" ID="hidBoarder" />
                                                        <asp:HiddenField runat="server" ID="hidIDCard" />
                                                        <input type="button" id="btnBoarder" onclick="return BoarderClick();" class="widebuttons"
                                                            value=" 变更登机人 " />
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
                                                                <td align="center">出发地
                                                                </td>
                                                                <td align="center">目的地
                                                                </td>
                                                                <td align="center">出发日期
                                                                </td>
                                                                <td align="center">航班号
                                                                </td>
                                                                <td align="center">价格
                                                                </td>
                                                                <td align="center">备注
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox runat="server" ID="txtSource"></asp:TextBox><font color="red">*</font>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox runat="server" ID="txtDestination"></asp:TextBox><font color="red">*</font>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAirTime" runat="server" onclick="setDate(this);" onfocus="javascript:this.blur();"></asp:TextBox><font
                                                                        color="red">*</font>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox runat="server" ID="txtAirNo"></asp:TextBox><font color="red">*</font>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox runat="server" ID="txtPrice"></asp:TextBox><font color="red">*</font>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox runat="server" ID="txtRemark"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </asp:Panel>
                                            <asp:Panel ID="panTicketCancel" runat="server" Visible="false">
                                                <tr>
                                                    <td class="oddrow">退票相关费用:
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
                                                <td class="oddrow-l" colspan="2" style="height: 40px; text-align: right; border-left: 0px; border-left-color: White">
                                                    <%--<asp:Button ID="btnSave" runat="server" Text=" 保存 " CssClass="widebuttons" OnClick="btnSave_Click" />&nbsp;&nbsp;--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="heading" colspan="4">申请单明细
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="85%" class="oddrow-l" colspan="4">
                                                    <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="ID,ReturnID"
                                                        OnRowCommand="gvG_RowCommand" OnRowDataBound="gvG_RowDataBound" PageSize="10"
                                                        EmptyDataText="暂时没有相关记录" PagerSettings-Mode="NumericFirstLast" OnPageIndexChanging="gvG_PageIndexChanging"
                                                        OnRowCreated="gvG_RowCreated" AllowPaging="true" Width="80%">
                                                        <Columns>
                                                            <asp:BoundField DataField="ExpenseDate" HeaderText="发生日期" ItemStyle-HorizontalAlign="Center"
                                                                DataFormatString="{0:d}" ItemStyle-Width="8%" />
                                                            <asp:TemplateField HeaderText="物料类别" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="labCostDetailName" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="费用类型" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="labExpenseTypeName" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="费用描述" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblExpenseDesc" Text='<%# Eval("ExpenseDesc") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="ExpenseTypeNumber" HeaderText="数量" ItemStyle-HorizontalAlign="Right"
                                                                ItemStyle-Width="5%" />
                                                            <asp:TemplateField HeaderText="总金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblExpenseMoney" Text='<%# Eval("ExpenseMoney") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="收款人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="labRecipient" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="所在城市" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="labCity" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="银行名称" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="labBankName" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="银行账号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="labBankAccountNo" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Boarder" HeaderText="登机人" ItemStyle-HorizontalAlign="center"
                                                                ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="GoAirNo" HeaderText="航班号" ItemStyle-HorizontalAlign="center"
                                                                ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="BoarderMobile" HeaderText="联系电话" ItemStyle-HorizontalAlign="center"
                                                                ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="TicketSource" HeaderText="出发地" ItemStyle-HorizontalAlign="center"
                                                                ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="TicketDestination" HeaderText="目的地" ItemStyle-HorizontalAlign="center"
                                                                ItemStyle-Width="5%" />
                                                            <asp:TemplateField ItemStyle-Width="3%" HeaderText="编辑">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="hylEdit" runat="server" Text="<img title='编辑' src='../../images/edit.gif' border='0' />"
                                                                        CausesValidation="false" CommandArgument='<%# Eval("ID")%>' CommandName="Modify" />
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
                                            <tr>
                                                <td class="oddrow-l">
                                                    <table width="100%">
                                                        <tr>
                                                            <td class="oddrow" width="15%">
                                                                <asp:Label runat="server" ID="lblSuggestion">审批备注</asp:Label>
                                                            </td>
                                                            <td class="oddrow-1" width="50%">
                                                                <asp:TextBox runat="server" ID="txtSuggestion" TextMode="MultiLine" Width="75%"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
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
                                    </td>
                                </tr>
                            </table>
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
