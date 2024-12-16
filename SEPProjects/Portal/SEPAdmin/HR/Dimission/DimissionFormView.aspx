<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DimissionFormView.aspx.cs"
    Inherits="DimissionFormView" MasterPageFile="~/MasterPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../public/js/DatePicker.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function setDate(obj) { popUpCalendar(obj, obj, 'yyyy-mm-dd'); }
    </script>

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="3" valign="middle">离职申请单
                <asp:HiddenField ID="hidDimissionFormID" runat="server" />
            </td>
            <td class="heading" valign="middle">审批状态:
                <asp:Label ID="labStatus" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp; <a href="DimissionAuditStatus.aspx?dimissionId=<%=DimissionFormId %>"
                    target="_blank">
                    <img src="../../Images/AuditStatus.gif" border="0px;" title="审批状态" /></a>
            </td>
        </tr>
        <tr>
            <td colspan="4" class="oddrow">
                <asp:Label runat="server" ID="labTip" />
            </td>
        </tr>
        <asp:Panel ID="pnlCash" runat="server" Visible="false">
            <tr>
                <td colspan="4" class="heading">未处理单据
                </td>
            </tr>
            <tr>
                <td colspan="4" class="oddrow">
                    <asp:GridView ID="gvCashList" runat="server" AutoGenerateColumns="False" DataKeyNames="FormId,FormType"
                        Width="100%">
                        <Columns>
                            <asp:BoundField DataField="FormId" Visible="false" />
                            <asp:TemplateField HeaderText="单据编号">
                                <ItemTemplate>
                                    <a href='http://<%# Eval("Website").ToString() + Eval("Url").ToString() %>' target="_blank"
                                        title="<%# Eval("FormCode") %>">
                                        <%# Eval("FormCode") %></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="FormType" HeaderText="单据类型" />
                            <asp:BoundField DataField="ProjectCode" HeaderText="项目号" />
                            <asp:BoundField DataField="Description" HeaderText="项目号描述" />
                            <asp:BoundField DataField="TotalPrice" HeaderText="总金额" />
                        </Columns>
                        <PagerSettings Visible="false" />
                    </asp:GridView>
                </td>
            </tr>
        </asp:Panel>
        <tr>
            <td class="oddrow" style="width: 20%">用户编号:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labCode" runat="server" />
            </td>
            <td class="oddrow">所在部门:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtdepartmentName" runat="server" Enabled="false" />
                <asp:HiddenField ID="hiddepartmentId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">姓名:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtuserName" runat="server" Enabled="false" />
            </td>
            <td class="oddrow">所在公司:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtcompanyName" runat="server" Enabled="false" />
                <asp:HiddenField ID="hidcompanyId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">职务:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtPosition" runat="server" Enabled="false" />
            </td>
            <td class="oddrow">所属团队:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtgroupName" runat="server" Enabled="false" />
                <asp:HiddenField ID="hidgroupId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">手机:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtMobilePhone" runat="server" Enabled="false" />
            </td>
            <td class="oddrow">分机:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtPhone" runat="server" Enabled="false" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">私人邮箱:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtEmail" runat="server" Enabled="false" />
            </td>
            <td class="oddrow">入职日期:
            </td>
            <td class="oddrow-l">
                <asp:TextBox runat="server" ID="txtjoinJobDate" Enabled="false" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">期望离职日期:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtdimissionDate2" onkeyDown="return false; " Enabled="false" runat="server" />
            </td>
            <td class="oddrow" style="width: 20%">最后离职日期:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox ID="txtdimissionDate" onkeyDown="return false; " Enabled="false" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">离职原因:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtdimissionCause" runat="server" TextMode="MultiLine" Width="90%"
                    Height="80px" Enabled="false" />
            </td>
        </tr>
        <!-- 业务审批 -->
        <tr>
            <td colspan="4">
                <asp:Panel ID="pnlManageAudit" runat="server">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="heading" colspan="4">①业务交接
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">&nbsp;
                            </td>
                            <td align="right" class="recordTd">记录数:<asp:Label ID="labManNumberT" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:GridView ID="gvDetailView" runat="server" AutoGenerateColumns="False" DataKeyNames="FormId"
                                    Width="100%" OnRowDataBound="gvDetailView_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="FormId" Visible="false" />
                                        <asp:TemplateField HeaderText="单据编号">
                                            <ItemTemplate>
                                                <a href='http://<%# Eval("Website").ToString() + Eval("Url").ToString() %>' target="_blank"
                                                    title="<%# Eval("FormCode") %>">
                                                    <%# Eval("FormCode") %></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="FormType" HeaderText="单据类型" />
                                        <asp:BoundField DataField="ProjectCode" HeaderText="项目号" />
                                        <asp:BoundField DataField="Description" HeaderText="项目号描述" />
                                        <asp:BoundField DataField="TotalPrice" HeaderText="总金额" />
                                        <asp:TemplateField HeaderText="交接人编号" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="labReceiverId" runat="server" />
                                                <asp:HiddenField ID="hidReceiverId" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="交接人">
                                            <ItemTemplate>
                                                <asp:Label ID="labReceiverName" runat="server" />
                                                <asp:HiddenField ID="hidReceiverName" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="交接人部门编号" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="labReceiverDepartmentId" runat="server" />
                                                <asp:HiddenField ID="hidReceiverDepartmentId" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="交接状态">
                                            <ItemTemplate>
                                                <asp:Label ID="labReceiverStatus" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerSettings Visible="false" />
                                </asp:GridView>
                                <input type="hidden" id="Hidden1" value="" runat="server" />
                                <asp:Label ID="Label2" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">&nbsp;
                            </td>
                            <td align="right" class="recordTd">记录数:<asp:Label ID="labManNumberB" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" colspan="4">报销单据
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">&nbsp;
                            </td>
                            <td align="right" class="recordTd">记录数:<asp:Label ID="labOOP2T" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:GridView ID="gvOOP2" runat="server" AutoGenerateColumns="False" DataKeyNames="FormId,FormType"
                                    Width="100%">
                                    <Columns>
                                        <asp:BoundField DataField="FormId" Visible="false" />
                                        <asp:TemplateField HeaderText="单据编号">
                                            <ItemTemplate>
                                                <a href='http://<%# Eval("Website").ToString() + Eval("Url").ToString() %>' target="_blank"
                                                    title="<%# Eval("FormCode") %>">
                                                    <%# Eval("FormCode") %></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="FormType" HeaderText="单据类型" />
                                        <asp:BoundField DataField="UserName" HeaderText="申请人" />
                                        <asp:BoundField DataField="ProjectCode" HeaderText="项目号" />
                                        <asp:BoundField DataField="Description" HeaderText="项目号描述" />
                                        <asp:BoundField DataField="TotalPrice" HeaderText="总金额" />
                                    </Columns>
                                    <PagerSettings Visible="false" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">&nbsp;
                            </td>
                            <td align="right" class="recordTd">记录数:<asp:Label ID="labOOP2B" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlPerformance" runat="server" Visible="true">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="oddrow">绩效:
                            </td>
                            <td class="oddrow-l" colspan="3">是否按实际工作日结算&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="radIsNormalPerTure" runat="server" Text="是" GroupName="IsNormalPer" />
                                <asp:RadioButton ID="radIsNormalPerFalse" runat="server" Text="否" GroupName="IsNormalPer" />
                                离职当月的绩效金额为：<asp:TextBox ID="txtSumPerformance" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <!-- 团队行政审批 -->
        <asp:Panel ID="pnlGroupAudit" runat="server">
            <tr>
                <td class="heading" colspan="4">②团队行政
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <font style="font-weight: bolder">&nbsp;&nbsp;考勤</font>
                </td>
            </tr>
            <tr>
                <td colspan="3" class="oddrow-l">&nbsp;&nbsp;1 / 上月考勤信息：
                </td>
                <td align="right">考勤审批状态：<asp:Label ID="labAttPreAuditStatus" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="oddrow-l">
                    <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#c1c1c1">
                        <tr>
                            <td height="25" align="center" bgcolor="#ececec">迟到
                            </td>
                            <td align="center" bgcolor="#ececec">早退
                            </td>
                            <td align="center" bgcolor="#ececec">旷工
                            </td>
                            <td align="center" bgcolor="#ececec">工作日OT
                            </td>
                            <td align="center" bgcolor="#ececec">节假日OT
                            </td>
                            <td align="center" bgcolor="#ececec">出差
                            </td>
                            <td align="center" bgcolor="#ececec">外出
                            </td>
                            <td align="center" bgcolor="#ececec">病假
                            </td>
                            <td align="center" bgcolor="#ececec">年度累计病假
                            </td>
                            <td align="center" bgcolor="#ececec">事假
                            </td>
                            <td align="center" bgcolor="#ececec">年假
                            </td>
                            <td align="center" bgcolor="#ececec">婚假
                            </td>
                            <td align="center" bgcolor="#ececec">丧假
                            </td>
                            <td align="center" bgcolor="#ececec">产假
                            </td>
                            <td align="center" bgcolor="#ececec">产检
                            </td>
                            <td align="center" bgcolor="#ececec">笔记本报销
                            </td>
                        </tr>
                        <tr>
                            <td height="25" align="center" bgcolor="#ffffff">
                                <asp:Label ID="labLate" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labLeaveEarly" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labAbsent" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labOverTime" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labHolidayOverTime" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labEvection" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labEgress" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labSickLeave" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labAddupSickLeave" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labAffiairLeave" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labAnnualLeave" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labMarriageLeave" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labFuneralLeave" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labMaternityLeave" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labPrenatalCheck" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labPCRefund" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3" class="oddrow-l">&nbsp;&nbsp;2 / 当月考勤信息：
                </td>
                <td align="right">考勤审批状态：<asp:Label ID="labAttCurAuditStatus" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="oddrow-l">
                    <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#c1c1c1">
                        <tr>
                            <td height="25" align="center" bgcolor="#ececec">迟到
                            </td>
                            <td align="center" bgcolor="#ececec">早退
                            </td>
                            <td align="center" bgcolor="#ececec">旷工
                            </td>
                            <td align="center" bgcolor="#ececec">工作日OT
                            </td>
                            <td align="center" bgcolor="#ececec">节假日OT
                            </td>
                            <td align="center" bgcolor="#ececec">出差
                            </td>
                            <td align="center" bgcolor="#ececec">外出
                            </td>
                            <td align="center" bgcolor="#ececec">病假
                            </td>
                            <td align="center" bgcolor="#ececec">年度累计病假
                            </td>
                            <td align="center" bgcolor="#ececec">事假
                            </td>
                            <td align="center" bgcolor="#ececec">年假
                            </td>
                            <td align="center" bgcolor="#ececec">婚假
                            </td>
                            <td align="center" bgcolor="#ececec">丧假
                            </td>
                            <td align="center" bgcolor="#ececec">产假
                            </td>
                            <td align="center" bgcolor="#ececec">产检
                            </td>
                            <td align="center" bgcolor="#ececec">笔记本报销
                            </td>
                        </tr>
                        <tr>
                            <td height="25" align="center" bgcolor="#ffffff">
                                <asp:Label ID="labLateCur" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labLeaveEarlyCur" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labAbsentCur" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labOverTimeCur" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labHolidayOverTimeCur" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labEvectionCur" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labEgressCur" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labSickLeaveCur" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labAddupSickLeaveCur" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labAffiairLeaveCur" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labAnnualLeaveCur" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labMarriageLeaveCur" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labFuneralLeaveCur" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labMaternityLeaveCur" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labPrenatalCheckCur" runat="server"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#ffffff">
                                <asp:Label ID="labPCRefundCur" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="oddrow-l">&nbsp;&nbsp;3 / 年假余&nbsp;<font style="font-weight: bolder"><asp:Label ID="labAnnual"
                    runat="server" /></font>&nbsp;天； 预支<asp:Label ID="labOverDraft"
                        runat="server" />天年假&nbsp;<font style="font-weight: bolder; color: Red;">其中法定假<asp:Label
                            ID="labOverAnnual" runat="server" />天，福利假<asp:Label
                                ID="labOverReward" runat="server" />天。</font>
                </td>
            </tr>
            <tr>
                <td colspan="4">离职原因:
                    <asp:Label runat="server" ID="lblHRReason"></asp:Label>
                    &nbsp;&nbsp;备注:
                    <asp:Label runat="server" ID="lblHRReasonRemark"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <font style="font-weight: bolder">&nbsp;&nbsp;行政（固定资产）</font>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="oddrow-l">&nbsp;&nbsp;交还非消耗性办公用品清单，工位牌人名标签，推柜钥匙：<br />
                    <asp:TextBox ID="txtOfficeSupplies" runat="server" TextMode="MultiLine" Width="90%"
                        Height="80px" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="请填写办公用品归还说明。"
                        ControlToValidate="txtOfficeSupplies" />
                </td>
            </tr>
        </asp:Panel>
        <!-- 集团人力资源审批 -->
        <asp:Panel ID="pnlHRAudit1" runat="server">
            <tr>
                <td class="heading" colspan="4">③集团人力资源
                </td>
            </tr>
            <tr>
                <td colspan="4" class="oddrow-l">&nbsp;&nbsp;1 / 各项保险及公积金：
                </td>
            </tr>
            <tr>
                <td class="oddrow">社保缴费至:
                </td>
                <td class="oddrow-l" colspan="3">
                    <asp:DropDownList ID="drpSocialInsY" runat="server" Enabled="false">
                    </asp:DropDownList>
                    年
                    <asp:DropDownList ID="drpSocialInsM" runat="server" Enabled="false">
                    </asp:DropDownList>
                    月
                </td>
            </tr>
            <tr>
                <td class="oddrow">医疗缴费至:
                </td>
                <td class="oddrow-l" colspan="3">
                    <asp:DropDownList ID="drpMedicalInsY" runat="server" Enabled="false">
                    </asp:DropDownList>
                    年
                    <asp:DropDownList ID="drpMedicalInsM" runat="server" Enabled="false">
                    </asp:DropDownList>
                    月
                </td>
            </tr>
            <tr>
                <td class="oddrow">住房公积金缴费至:
                </td>
                <td class="oddrow-l" colspan="3">
                    <asp:DropDownList ID="drpCapitalReserveY" runat="server" Enabled="false">
                    </asp:DropDownList>
                    年
                    <asp:DropDownList ID="drpCapitalReserveM" runat="server" Enabled="false">
                    </asp:DropDownList>
                    月
                </td>
            </tr>
            <tr>
                <td class="oddrow">补充医疗缴费至:
                </td>
                <td class="oddrow-l" colspan="3">
                    <asp:CheckBox ID="chkComplementaryMedical" runat="server" />
                    <asp:DropDownList ID="drpAddedMedicalInsY" runat="server" Enabled="false">
                    </asp:DropDownList>
                    年
                    <asp:DropDownList ID="drpAddedMedicalInsM" runat="server" Enabled="false">
                    </asp:DropDownList>
                    月
                </td>
            </tr>
            <tr>
                <td class="oddrow">备注:
                </td>
                <td class="oddrow-l" colspan="3">
                    <asp:TextBox ID="txtHRRemark" runat="server" TextMode="MultiLine" Width="90%" Height="80px" />
                </td>
            </tr>
        </asp:Panel>
        <!-- IT部审批 -->
        <asp:Panel ID="pnlITAudit" runat="server">
            <tr>
                <td class="heading" colspan="4">④IT
                </td>
            </tr>
            <tr>
                <td colspan="4" class="oddrow-l">&nbsp;&nbsp;1 / 电子邮箱帐户：<asp:TextBox ID="txtCompanyEmail" runat="server" Width="220" />
                </td>
            </tr>
            <tr>
                <td colspan="4" class="oddrow-l">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton GroupName="radEmailIsDelete"
                    runat="server" ID="radEmailIsDeleteTrue" Text="删除" Checked="true" /><br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton GroupName="radEmailIsDelete"
                        runat="server" ID="radEmailIsDeleteFalse" Text="保留期限" />
                    &nbsp;&nbsp;<asp:TextBox ID="txtEmailSaveLastDay" onkeyDown="return false; " onclick="setDate(this);"
                        runat="server" Width="220" />
                </td>
            </tr>
            <%--            <tr>
                <td colspan="4" class="oddrow-l">
                    &nbsp;&nbsp;2 / OA系统帐户：
                </td>
            </tr>
            <tr>
                <td colspan="4" class="oddrow-l">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton GroupName="radAccountIsDelete"
                        runat="server" ID="radAccountIsDeleteTrue" Text="删除" Checked="true" /><br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton GroupName="radAccountIsDelete"
                        runat="server" ID="radAccountIsDeleteFalse" Text="保留期限" />
                    &nbsp;&nbsp;<asp:TextBox ID="txtAccountSaveLastDay" onkeyDown="return false; " onclick="setDate(this);"
                        runat="server" Width="220" />
                </td>
            </tr>--%>
            <tr>
                <td colspan="4" class="oddrow-l">&nbsp;&nbsp;2 / OA设备/自购、自带电脑编号：<asp:TextBox ID="txtOwnPCCode" runat="server" Width="220" />
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;公司电脑编号：<asp:TextBox ID="txtPCCode" runat="server" Width="220" />
                </td>
            </tr>
            <tr>
                <td colspan="4" class="oddrow-l">&nbsp;&nbsp;&nbsp;&nbsp;公司电脑状况（含网线）：<br />
                    <asp:TextBox ID="txtPCUsedDes" runat="server" TextMode="MultiLine" Width="90%" Height="80px" />
                </td>
            </tr>
            <tr>
                <td colspan="4" class="oddrow-l">&nbsp;&nbsp;3 / 其他说明：
                </td>
            </tr>
            <tr>
                <td colspan="4" class="oddrow-l">
                    <asp:TextBox ID="txtITOther" runat="server" TextMode="MultiLine" Width="90%" Height="80px" />
                </td>
            </tr>
            <tr>
                <td colspan="4" class="oddrow-l">&nbsp;&nbsp;
                </td>
            </tr>
        </asp:Panel>
        <!-- 财务审批 -->
        <asp:Panel ID="pnlFinanceAudit" runat="server">
            <tr>
                <td class="heading" colspan="4">⑤财务
                </td>
            </tr>
            <asp:Panel ID="pnlFinanceAuditLevel1" runat="server">
                <tr>
                    <td colspan="4" class="oddrow-l">&nbsp;&nbsp;1 / 借款/发票/报销：
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="oddrow-l">
                        <asp:Label ID="labLoan" runat="server" />
                        <asp:Panel ID="pnlLoan" runat="server">
                            <asp:Label ID="labBranchInfo" runat="server" />
                            <%--<table width="90%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td width="30%">
                                        G：<asp:TextBox ID="txtLoanG" runat="server" TextMode="MultiLine" Width="100%" Height="50px" />
                                    </td>
                                    <td>
                                        H：<asp:TextBox ID="txtLoanH" runat="server" TextMode="MultiLine" Width="100%" Height="50px" />
                                    </td>
                                    <td>
                                        I：<asp:TextBox ID="txtLoanI" runat="server" TextMode="MultiLine" Width="100%" Height="50px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        J：<asp:TextBox ID="txtLoanJ" runat="server" TextMode="MultiLine" Width="100%" Height="50px" />
                                    </td>
                                    <td>
                                        K：<asp:TextBox ID="txtLoanK" runat="server" TextMode="MultiLine" Width="100%" Height="50px" />
                                    </td>
                                    <td>
                                        L：<asp:TextBox ID="txtLoanL" runat="server" TextMode="MultiLine" Width="100%" Height="50px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        M：<asp:TextBox ID="txtLoanM" runat="server" TextMode="MultiLine" Width="100%" Height="50px" />
                                    </td>
                                    <td>
                                        N：<asp:TextBox ID="txtLoanN" runat="server" TextMode="MultiLine" Width="100%" Height="50px" />
                                    </td>
                                    <td>
                                        A：<asp:TextBox ID="txtLoanA" runat="server" TextMode="MultiLine" Width="100%" Height="50px" />
                                    </td>
                                </tr>
                            </table>--%>
                        </asp:Panel>
                    </td>
                </tr>
            </asp:Panel>
            <%--<asp:Panel ID="pnlFinanceAuditLevel2" runat="server">
                <tr>
                    <td colspan="4" class="oddrow-l">
                        &nbsp;&nbsp;2 / 应收/应付款：
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="oddrow-l">
                        <asp:Label ID="labAccountsPayable" runat="server" />
                        <asp:Panel ID="pnlAccountsPayable" runat="server">
                            <asp:Label ID="labBranchInfo2" runat="server" />
                            <table width="90%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td width="30%">
                                        G：<asp:TextBox ID="txtAccountsPayableG" runat="server" TextMode="MultiLine" Width="100%" Height="50px" />
                                    </td>
                                    <td>
                                        H：<asp:TextBox ID="txtAccountsPayableH" runat="server" TextMode="MultiLine" Width="100%" Height="50px" />
                                    </td>
                                    <td>
                                        I：<asp:TextBox ID="txtAccountsPayableI" runat="server" TextMode="MultiLine" Width="100%" Height="50px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        J：<asp:TextBox ID="txtAccountsPayableJ" runat="server" TextMode="MultiLine" Width="100%" Height="50px" />
                                    </td>
                                    <td>
                                        K：<asp:TextBox ID="txtAccountsPayableK" runat="server" TextMode="MultiLine" Width="100%" Height="50px" />
                                    </td>
                                    <td>
                                        L：<asp:TextBox ID="txtAccountsPayableL" runat="server" TextMode="MultiLine" Width="100%" Height="50px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        M：<asp:TextBox ID="txtAccountsPayableM" runat="server" TextMode="MultiLine" Width="100%" Height="50px" />
                                    </td>
                                    <td>
                                        N：<asp:TextBox ID="txtAccountsPayableN" runat="server" TextMode="MultiLine" Width="100%" Height="50px" />
                                    </td>
                                    <td>
                                        A：<asp:TextBox ID="txtAccountsPayableA" runat="server" TextMode="MultiLine" Width="100%" Height="50px" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </asp:Panel>--%>
            <asp:Panel ID="pnlFinanceAuditLevel3" runat="server">
                <tr>
                    <td colspan="4" class="oddrow-l">&nbsp;&nbsp;2 / 商务卡：
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="oddrow-l">
                        <asp:TextBox ID="txtBusinessCard" runat="server" TextMode="MultiLine" Width="90%"
                            Height="80px" />
                    </td>
                </tr>
            </asp:Panel>
            <asp:Panel ID="pnlFinanceAuditLevel4" runat="server">
                <tr>
                    <td colspan="4" class="oddrow-l">&nbsp;&nbsp;3 / 工资结算情况：
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="oddrow-l">
                        <asp:RadioButton ID="radSalary1" runat="server" Text="综合以上意见后工资结算正常" GroupName="SalaryRadioButton" /><br />
                        <asp:RadioButton ID="radSalary2" runat="server" GroupName="SalaryRadioButton" />需补缴<asp:TextBox
                            ID="txtSalary" runat="server" />请选择所属分公司：
                        <asp:DropDownList ID="drpBranchList" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
            </asp:Panel>
            <asp:Panel ID="pnlFinanceAuditLevel6" runat="server">
                <tr>
                    <td colspan="4" class="oddrow-l">&nbsp;&nbsp;4 / 工资结算后出纳收款情况：
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="oddrow-l">
                        <asp:TextBox ID="txtSalary2" runat="server" TextMode="MultiLine" Width="90%" Height="80px" />
                    </td>
                </tr>
            </asp:Panel>
            <asp:Panel ID="pnlFinanceAuditLevel5" runat="server">
                <tr>
                    <td colspan="4" class="oddrow-l">&nbsp;&nbsp;5 / 其他说明：
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="oddrow-l">
                        <asp:TextBox ID="txtOther" runat="server" TextMode="MultiLine" Width="90%" Height="80px" />
                    </td>
                </tr>
            </asp:Panel>
        </asp:Panel>
        <!-- 集团行政审批 -->
        <asp:Panel runat="server" ID="pnlADAudit">
            <tr>
                <td class="heading" colspan="4">⑥集团行政
                </td>
            </tr>
            <tr>
                <td colspan="4" class="oddrow-l">&nbsp;&nbsp;1 / 门卡卡号：<asp:TextBox ID="txtDoorCard" runat="server" Enabled="false" />
                </td>
            </tr>
            <tr>
                <td colspan="4" class="oddrow-l">&nbsp;&nbsp;2 / 图书管理：
                </td>
            </tr>
            <tr>
                <td colspan="4" class="oddrow-l">
                    <asp:TextBox ID="txtLibraryManage" runat="server" TextMode="MultiLine" Width="90%"
                        Height="80px" Enabled="false" />
                </td>
            </tr>
        </asp:Panel>
        <!-- 集团人力资源第二次审批 -->
        <asp:Panel runat="server" ID="pnlHRAudit2">
            <tr>
                <td class="heading" colspan="4">⑦集团人力资源
                </td>
            </tr>
            <tr>
                <td colspan="4" class="oddrow-l">&nbsp;&nbsp;1 / 人事档案：
                </td>
            </tr>
            <tr>
                <td colspan="4" class="oddrow-l">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton GroupName="radIsHaveArchives"
                    runat="server" ID="radIsHaveArchivesFalse" Text="无" Checked="true" Enabled="false" /><br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton GroupName="radIsHaveArchives"
                        runat="server" ID="radIsHaveArchivesTrue" Text="有" Enabled="false" /><br />
                    &nbsp;&nbsp;须于<asp:TextBox ID="txtTurnAroundDate" onkeyDown="return false; " Enabled="false"
                        runat="server" />前办理完毕调转手续。
                </td>
            </tr>
        </asp:Panel>
        <tr>
            <td class="oddrow">审批日志:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="labAuditLog" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                <asp:Button runat="server" ID="btnReturn" Text=" 返回 " OnClick="btnReturn_Click" />
            </td>
            <td class="oddrow-l" colspan="3">&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
