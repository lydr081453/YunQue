<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WaitReceivingEdit.aspx.cs"
    Inherits="FinanceWeb.ExpenseAccount.WaitReceivingEdit" MasterPageFile="~/MasterPage.master"
    EnableEventValidation="false" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <link href="css/treeStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript">
        function openProject() {
            var win = window.open('SelectedProjectCode.aspx', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }

        function setProjectInfo(projectId, projectCode, projectDesc, deptList) {
            document.getElementById("<%=hidProejctId.ClientID %>").value = projectId;
            document.getElementById("<%=txtproject_code1.ClientID %>").value = projectCode;
            document.getElementById("<%=txtproject_descripttion.ClientID %>").value = projectDesc;

            document.getElementById("<%=hidProject_Code1.ClientID %>").value = projectCode
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

            FinanceWeb.ExpenseAccount.CashExpenseAccountEdit.getTypeList(parseInt(projectid), parseInt(groupid), popdrp);
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
            var expenseDate = document.getElementById("<%=txtExpenseDate.ClientID %>").value.trim();
            var expenseType = document.getElementById("<%=hidExpenseTypeId.ClientID %>").value.trim(); ; //document.getElementById("<%//=drpExpenseType.ClientID %>").options[document.getElementById("<%//=drpExpenseType.ClientID %>").selectedIndex].value;
            var panMealFee = document.getElementById("<%=panMealFee.ClientID %>");
            var panPhone = document.getElementById("<%=panPhone.ClientID %>");
            var panOvertimeMeal = document.getElementById("<%=panOvertimeMeal.ClientID %>");
            var expenseMoney = document.getElementById("<%=txtExpenseMoney.ClientID%>").value;
            var expenseDesc = document.getElementById("<%=txtExpenseDesc.ClientID%>").value;
            var errorMessage = "";

            if (expenseType == "" || expenseType == "0") {
                errorMessage += "-- 请在左侧类型树中选择费用类型!\n";
            }

            if (expenseDate == "") {
                errorMessage += "-- 请选择费用发生日期!\n";
            }


            if (panMealFee != null) {
                if (!document.getElementById("<%=chkMealFee1.ClientID %>").checked && !document.getElementById("<%=chkMealFee2.ClientID %>").checked && !document.getElementById("<%=chkMealFee3.ClientID %>").checked) {
                    errorMessage += "-- 请选择餐费类型!\n";
                }

                //                var totalfee = 0;
                //                if (document.getElementById("<%//=chkMealFee1.ClientID %>").checked) {
                //                    totalfee += 20;
                //                }
                //                if (document.getElementById("<%//=chkMealFee2.ClientID %>").checked) {
                //                    totalfee += 40;
                //                }
                //                if (document.getElementById("<%//=chkMealFee3.ClientID %>").checked) {
                //                    totalfee += 40;
                //                }
                //                document.getElementById("<%//=mealMaxTotalFee.ClientID %>").value = totalfee;
                //                if (document.getElementById("<%//=txtExpenseMoney.ClientID %>").value > totalfee) {
                //                    errorMessage += "-- 餐费报销金额大于可报销额度" + totalfee + "!\n";
                //                }
            }



            if (panPhone != null) {

            }
//            if (expenseMoney <= 0) {
//                errorMessage += "-- 报销金额必须大于0!\n";
//            }

            if (panOvertimeMeal != null) {
                //                if (document.getElementById("<%//=txtExpenseMoney.ClientID %>").value > 15) {
                //                    errorMessage += "-- OT餐费报销金额超出可报销额度!\n";
                //                }
            }

            if (expenseDesc == "") {
                errorMessage += "-- 请填写费用描述!";
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
                        txtExpenseMoney.set_value(gasCostByKM * expenseNumber);
                    }
                    catch (e) {
                        txtExpenseMoney.set_value(gasCostByKM * 1);
                        txtNumber.set_value(1);
                    }
                }
            }
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%" class="tableForm">
                <tr>
                    <td class="heading" colspan="4">
                        冲销单信息<asp:HiddenField ID="hidProejctId" runat="server" />
                        <asp:HiddenField ID="hidProejctIds" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="oddrow" width="15%">
                        申请人:
                    </td>
                    <td class="oddrow-l" width="35%">
                        <asp:Label runat="server" ID="labRequestUserName" />(<asp:Label runat="server" ID="labRequestUserCode" />)
                    </td>
                    <td class="oddrow" width="15%">
                        申请日期:
                    </td>
                    <td class="oddrow-l" width="35%">
                        <asp:Label runat="server" ID="labRequestDate" />
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        项目号:
                    </td>
                    <td class="oddrow-l">
                        <asp:TextBox ID="txtproject_code1" runat="server" Width="50%" Enabled="false" />
                        <asp:HiddenField ID="hidProject_Code1" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtproject_code1"
                            Display="None"></asp:RequiredFieldValidator>
                        <font color="red">* </font>
                        <input type="button" value="请选择..." class="widebuttons" runat="server" id="btnOpenProject"
                            onclick="openProject();return false;" />
                    </td>
                    <td class="oddrow">
                        项目名称:
                    </td>
                    <td class="oddrow-l">
                        <asp:TextBox ID="txtproject_descripttion" runat="server" Width="80%" Enabled="false" />
                        <asp:HiddenField ID="hidProject_Description" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        成本所属组:
                    </td>
                    <td class="oddrow-l">
                        <asp:DropDownList ID="ddlDepartment" runat="server" onchange="clearTypes(this);"
                            Enabled="false" />
                        <asp:HiddenField ID="hidDeptId" runat="server" />
                    </td>
                    <td class="oddrow">
                        预计冲销总金额:
                    </td>
                    <td class="oddrow-l">
                        <asp:Label runat="server" ID="labPreFee" /><asp:HiddenField runat="server" ID="hidPreFee" />
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        成本使用情况:
                    </td>
                    <td class="oddrow-l" colspan="3">
                        <asp:Label runat="server" ID="lblCalculate" /><br />
                        <font color="red">单据只有在提交时才会真正占用成本，此时显示的成本使用情况并不涉及其他人正在编辑的报销单，因此请及时提交单据。</font>
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        审批记录:
                    </td>
                    <td class="oddrow-l" colspan="3">
                        <asp:Label runat="server" ID="labSuggestion" />
                    </td>
                </tr>
            </table>
            <asp:Panel runat="server" ID="panRecipient">
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="tableForm">
                    <tr>
                        <td class="oddrow-l" colspan="4">
                            <asp:GridView ID="gvRecipient" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvRecipient_RowDataBound"
                                DataKeyNames="recipientId" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="recipientId" HeaderText="recipientId" />
                                    <asp:TemplateField HeaderText="流水号" HeaderStyle-Width="6%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# int.Parse(Eval("GID").ToString()).ToString("0000000") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="orderid" HeaderText="订单号" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="8%" />
                                    <asp:BoundField DataField="RecipientNo" HeaderText="收货单号" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="8%" />
                                    <asp:BoundField DataField="requestorname" HeaderText="申请人" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="6%" />
                                    <asp:TemplateField HeaderText="申请时间" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <%# Eval("requisition_committime").ToString() == ESP.Purchase.Common.State.datetime_minvalue ? "" : DateTime.Parse(Eval("requisition_committime").ToString()).ToString("yyyy-MM-dd")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="project_code" HeaderText="项目号" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="11%" />
                                    <asp:TemplateField HeaderText="收货金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="11%">
                                        <ItemTemplate>
                                            <%# Eval("moneytype").ToString() == "美元" ? "＄" + decimal.Parse(Eval("RecipientAmount").ToString()).ToString("#,##0.##") : "￥" + decimal.Parse(Eval("RecipientAmount").ToString()).ToString("#,##0.##")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="supplier_name" HeaderText="供应商" ItemStyle-HorizontalAlign="Center"
                                        SortExpression="supplier_name" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="tableForm">
                    <tr>
                        <td class="oddrow-l" colspan="4">
                            <asp:Button ID="btnSetAuditor2" runat="server" Text=" 下一步 " CssClass="widebuttons"
                                OnClick="btnSetAuditor2_Click" />&nbsp;&nbsp;
                            <asp:Button ID="btnReturn2" runat="server" Text="返回至申请列表" CssClass="widebuttons"
                                OnClick="btnReturn2_Click" CausesValidation="false" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel runat="server" ID="panDetailInfo">
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="tableForm">
                    <tr>
                        <td class="oddrow-l" colspan="4">
                            <li>
                                <asp:HyperLink ID="hylPrint" runat="server" Text="查看借款单明细" ToolTip="查看借款单明细" Width="90px"></asp:HyperLink>
                            </li>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="tableForm">
                    <tr>
                        <td>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td class="heading" style="border-right-color: Gray; border-right: 1px">
                                        费用类型
                                    </td>
                                    <td class="heading" style="border-left-color: Gray; border-left: 1px">
                                        费用明细
                                    </td>
                                    <td class="heading" align="right">
                                        <asp:Button ID="btnSetAuditor" runat="server" Text="下一步" CssClass="widebuttons" OnClick="btnSetAuditor_Click" />&nbsp;&nbsp;
                                        <asp:Button ID="btnReturn" runat="server" Text="返回至申请列表" CssClass="widebuttons" OnClick="btnReturn_Click"
                                            CausesValidation="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="200" valign="top" class="oddrow-l2">
                                        <asp:TextBox runat="server" ID="txtNodeName" />&nbsp;<asp:ImageButton ID="bthSearchNode"
                                            runat="server" ImageUrl="~/ExpenseAccount/images/t2_03-07.jpg" ToolTip="搜索" ImageAlign="AbsMiddle"
                                            OnClick="bthSearchNode_Click" />&nbsp;<asp:ImageButton ID="btnClearNode" runat="server"
                                                ToolTip="清空" ImageUrl="~/ExpenseAccount/images/t2_03-07_2.jpg" ImageAlign="AbsMiddle"
                                                OnClick="btnClearNode_Click" />
                                        <!-- 人力节点树 -->
                                        <ComponentArt:TreeView ID="userTreeView" Height="400" Width="220" DragAndDropEnabled="false"
                                            NodeEditingEnabled="false" KeyboardEnabled="true" CssClass="TreeView" NodeCssClass="TreeNode"
                                            SelectedNodeCssClass="SelectedTreeNode" HoverNodeCssClass="HoverTreeNode" NodeEditCssClass="NodeEdit"
                                            LineImageWidth="19" LineImageHeight="20" DefaultImageWidth="16" DefaultImageHeight="16"
                                            ItemSpacing="0" ImagesBaseUrl="images/" NodeLabelPadding="3" ShowLines="true"
                                            LineImagesFolderUrl="images/lines/" EnableViewState="true" runat="server" OnNodeSelected="userTreeView_NodeSelected">
                                        </ComponentArt:TreeView>
                                    </td>
                                    <td valign="top" class="oddrow-l" colspan="2">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="tableForm">
                                            <tr>
                                                <td class="oddrow" width="15%">
                                                    费用类型:
                                                </td>
                                                <td class="oddrow-l" width="35%">
                                                    <asp:Label ID="labExpenseTypeName" runat="server" Text="请选择.." />
                                                    <asp:HiddenField runat="server" ID="hidExpenseTypeId" />
                                                    <asp:HiddenField runat="server" ID="hidGasCostByKM" />
                                                </td>
                                                <td class="oddrow" width="15%">
                                                    费用发生日期:
                                                </td>
                                                <td class="oddrow-l" width="35%">
                                                    <asp:TextBox ID="txtExpenseDate" runat="server" onclick="setDate(this);" onfocus="javascript:this.blur();"></asp:TextBox><font
                                                        color="red">*</font>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtExpenseDate" Text="必填"></asp:RequiredFieldValidator>--%>
                                                </td>
                                            </tr>
                                            <asp:Panel runat="server" ID="panMealFee">
                                                <tr>
                                                    <td class="oddrow">
                                                        &nbsp;
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
                                                    <td class="oddrow">
                                                        预计报销手机费年月:
                                                    </td>
                                                    <td class="oddrow-l" colspan="3">
                                                        <asp:DropDownList runat="server" ID="drpPhoneYear" OnSelectedIndexChanged="drpPhoneYear_SelectedIndexChanged"
                                                            AutoPostBack="true">
                                                        </asp:DropDownList>
                                                        年 &nbsp;
                                                        <asp:DropDownList runat="server" ID="drpPhoneMonth" OnSelectedIndexChanged="drpPhoneMonth_SelectedIndexChanged"
                                                            AutoPostBack="true">
                                                        </asp:DropDownList>
                                                        月
                                                    </td>
                                                </tr>
                                            </asp:Panel>
                                            <tr>
                                                <td class="oddrow">
                                                    数量:
                                                </td>
                                                <td class="oddrow-l">
                                                    <ComponentArt:NumberInput ID="txtNumber" NumberType="Number" runat="server" EmptyText="1"
                                                        DecimalDigits="0">
                                                        <ClientEvents>
                                                            <Blur EventHandler="CalculateMoney" />
                                                        </ClientEvents>
                                                    </ComponentArt:NumberInput>
                                                    <asp:Label runat="server" ID="labGasByKM" />
                                                </td>
                                                <td class="oddrow">
                                                    总金额:
                                                </td>
                                                <td class="oddrow-l">
                                                    <ComponentArt:NumberInput ID="txtExpenseMoney" runat="server" EmptyText="0.00" NumberType="Number">
                                                    </ComponentArt:NumberInput>
                                                    <font color="red">*</font>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="oddrow">
                                                    费用描述:
                                                </td>
                                                <td class="oddrow-l">
                                                    <asp:TextBox runat="server" ID="txtExpenseDesc" TextMode="MultiLine" Rows="2" Width="80%"
                                                        MaxLength="1500" /><font color="red">*</font>
                                                </td>
                                                <td class="oddrow">
                                                    物料类别:
                                                </td>
                                                <td class="oddrow-l">
                                                    <asp:DropDownList ID="ddlProjectType" runat="server" onchange="clearTypes2(this);" />
                                                    <asp:HiddenField ID="hidTypeId" runat="server" />
                                                    <asp:HiddenField ID="hidTypeIsMatch" runat="server" />
                                                    <%--&nbsp;&nbsp;&nbsp; <a href="#" onclick="return hs.htmlExpand(this, { contentId: 'highslide-html1' } )"
                                            class="highslide">
                                            <img src="/images/wen.jpg" border="0" style="vertical-align: bottom;" /></a>
                                        <div class="highslide-html-content" id="highslide-html1">
                                            <div class="highslide-header">
                                                <ul>
                                                    <li class="highslide-move"><a href="#" onclick="return false">移动</a> </li>
                                                    <li class="highslide-close"><a href="#" onclick="return hs.close(this)">关闭</a> </li>
                                                </ul>
                                            </div>
                                            <div class="highslide-body">
                                            </div>
                                            <div class="highslide-footer">
                                                <div>
                                                    <span class="highslide-resize" title="Resize"><span></span></span>
                                                </div>
                                            </div>
                                        </div>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="oddrow-l" colspan="2" style="height: 40px; border-right: 0px; border-right-color: White">
                                                    <asp:Button ID="btnAddDetail" runat="server" CssClass="widebuttons" OnClientClick="return CheckForm();"
                                                        OnClick="btnAddDetail_Click" />&nbsp;&nbsp;
                                                </td>
                                                <td class="oddrow-l" colspan="2" style="height: 40px; text-align: right; border-left: 0px;
                                                    border-left-color: White">
                                                    <%--<asp:Button ID="btnSave" runat="server" Text=" 保存 " CssClass="widebuttons" OnClick="btnSave_Click" />&nbsp;&nbsp;--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="heading" colspan="4">
                                                    申请单明细
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="85%" class="oddrow-l" colspan="4">
                                                    <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="ID,ReturnID"
                                                        OnRowCommand="gvG_RowCommand" OnRowDataBound="gvG_RowDataBound" PageSize="10"
                                                        EmptyDataText="暂时没有相关记录" PagerSettings-Mode="NumericFirstLast" OnPageIndexChanging="gvG_PageIndexChanging"
                                                        AllowPaging="true" Width="80%">
                                                        <Columns>
                                                            <asp:BoundField DataField="ExpenseDate" HeaderText="费用发生日期" ItemStyle-HorizontalAlign="Center"
                                                                DataFormatString="{0:d}" ItemStyle-Width="15%" />
                                                            <asp:TemplateField HeaderText="物料类别" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="labCostDetailName" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="费用类型" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="labExpenseTypeName" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="费用明细描述" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25%">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblExpenseDesc" Text='<%# Eval("ExpenseDesc") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="ExpenseTypeNumber" HeaderText="数量" ItemStyle-HorizontalAlign="Right"
                                                                ItemStyle-Width="10%" />
                                                            <asp:TemplateField HeaderText="总金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblExpenseMoney" Text='<%# Eval("ExpenseMoney") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText=" " ItemStyle-Width="5%">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="hylEdit" runat="server" Text="<img title='编辑' src='../../images/edit.gif' border='0' />"
                                                                        CausesValidation="false" CommandArgument='<%# Eval("ID")%>' CommandName="Modify" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center"
                                                                ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnDelete" runat="server" Text="<img title='删除' src='../../images/disable.gif' border='0' />"
                                                                        OnClientClick="return confirm('您是否确认删除？');" CausesValidation="false" CommandArgument='<%# Eval("ID") %>'
                                                                        CommandName="Del" />
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
            <asp:ValidationSummary runat="server" ID="ValidationSummary1" ShowMessageBox="true"
                ShowSummary="false" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
