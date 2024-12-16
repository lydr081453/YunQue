<%@ Page Language="C#" AutoEventWireup="true" Inherits="Transfer_PromotionTransferEdit" MasterPageFile="~/MasterPage.master" EnableEventValidation="false"  Codebehind="PromotionTransferEdit.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server" >
<script src="/public/js/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="/public/js/DatePicker.js"></script>
    <script language="javascript">
        $(document).ready(function() {
            showjob();
            $("#<%=ddlcurrentTitle.ClientID %>").change(function() {
                $("#<%=hidcurrentTitle.ClientID %>").val($("#<%=ddlcurrentTitle.ClientID %>").val());
                var obj = document.getElementById("<%=ddlcurrentTitle.ClientID %>");
                $("#<%=hidcurrentName.ClientID %>").val(obj(obj.selectedIndex).text);                
            });

        });

    function att() {
        if (Page_ClientValidate()) {
            var Ka = navigator.userAgent.toLowerCase();
            var rt = Ka.indexOf("opera") != -1;
            var r = Ka.indexOf("msie") != -1 && (document.all && !rt);

            if (r) {
                window.attachEvent("onbeforeunload", function() { $(".widebuttons").val("请等待").attr("disabled", "true"); });
            } else {
                window.addEventListener('onbeforeunload', function() { $(".widebuttons").val("请等待").attr("disabled", "true"); }, false);
            }
        }
    }
        function setDate(obj) {
              popUpCalendar(obj, obj, 'yyyy-mm-dd');
            
        }
        function showjob() {

            var ddljob = document.getElementById("<%=ddlnowGroupName.ClientID %>").value;            
            var str = ddljob.split(',');
            document.getElementById("<%=labnowjob.ClientID %>").innerHTML = str[0];
            var idValues = str[1].split('-');
            if (idValues[0] != "") {
                drpPositionsBind(idValues[0]);
            }      
        }
        function drpPositionsBind(depid) {

            ShunYaGroup.BLL.SEP_EmployeeBaseInfo.GetPositionsByDepId(depid, popdrp);
            function popdrp(r) {
                $("#<%=ddlcurrentTitle.ClientID %>").empty();
                $("#<%=ddlcurrentTitle.ClientID %>").append("<option value=\"-1\">请选择...</option>");
                for (i = 0; i < r.value.length; i++) {
                    $("#<%=ddlcurrentTitle.ClientID %>").append("<option value=\"" + r.value[i].DepartmentPositionID + "\">" + r.value[i].DepartmentPositionName + "</option>");
                }
            }

        }
       
    </script>
<table width="100%" class="tableForm">   
        <tr>
             <td class="heading" colspan="6">员工调动信息</td>
        </tr>
        <tr>
            <td class="oddrow"  style="width: 20%">
                员工姓名:
            </td>
            <td class="oddrow-l"  style="width: 30%">                        
                <asp:TextBox ID="txtuserName" runat="server" Enabled="false" /><asp:HiddenField ID="hiduserId" runat="server" />
            </td>
            <td class="oddrow"  style="width: 20%">
                加入公司时间:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox ID="txtJob_JoinDate" runat="server" Enabled="false" />
            </td>
         </tr>
         <tr>
            <td class="oddrow">
                现属业务团队:
            </td>
            <td class="oddrow-l" >
                <asp:DropDownList ID="ddlnowGroupName" onchange="showjob();" runat="server" style="width:auto" />
            </td>  
            <td class="oddrow">
            现职位:
            </td>  
            <td class="oddrow-l">
                <asp:Label ID="labnowjob" runat="server"  /><asp:HiddenField ID="hidnowGroupName" runat="server" />
            </td>                     
        </tr> 
        <tr>
            <td class="oddrow">
                最终职位:
            </td>
            <td class="oddrow-l" >
            <asp:HiddenField ID="hidcurrentTitle" runat="server" /><asp:HiddenField ID="hidcurrentName" runat="server" />
                <asp:DropDownList ID="ddlcurrentTitle" runat="server" style="width:auto" />
                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ControlToValidate="ddlcurrentTitle" Display="Dynamic" ErrorMessage="请选择最终职位" 
                    ValueToCompare="-1" Operator="NotEqual"></asp:CompareValidator>
            </td>  
            <td class="oddrow">
                生效日期:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtoperationDate" runat="server" onkeyDown="return false; " onclick="setDate(this);" /><asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                    runat="server" ErrorMessage="请选择生效日期" Display="None" ControlToValidate="txtoperationDate" /><font
                        color="red"> * </font>
            </td>                  
        </tr>
        <div id="divSalary" runat="server"> 
        <tr>
            <td class="oddrow">
                原基本工资:
            </td>
            <td class="oddrow-l" >
                <asp:TextBox ID="txtnowBasePay" runat="server" Enabled="false" />
            </td>
            <td class="oddrow">
            原绩效工资              
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtnowMeritPay" runat="server" Enabled="false" />
            </td> 
            </tr>  
        <tr>
            <td class="oddrow">
                新基本工资:
            </td>
            <td class="oddrow-l" >
                <asp:TextBox ID="txtnewBasePay" runat="server" /><font color="red">* </font><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                    runat="server" ErrorMessage="请填写新基本工资" Display="None" ControlToValidate="txtnewBasePay" /><asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtnewBasePay"
                    ErrorMessage="请输入正确新基本工资" ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">请输入正确新基本工资</asp:RegularExpressionValidator>
            </td>
            <td class="oddrow">
            新绩效工资               
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtnewMeritPay" runat="server" /><font color="red"> * </font><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                    runat="server" ErrorMessage="请填写新业务绩效" Display="None" ControlToValidate="txtnewMeritPay" /><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtnewMeritPay"
                    ErrorMessage="请输入正确新业务绩效" ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">请输入正确新绩效工资</asp:RegularExpressionValidator>
            </td> 
            </tr>  
            </div>   
        </table> 
   <table width="100%" class="tableForm">         
        <tr>
         <td class="oddrow">
                备注:
            </td>                      
        </tr>                 
        <tr>
            <td class="oddrow-l" >
                 <asp:TextBox ID="txtJob_Memo" TextMode="MultiLine" Height="100px" Width="100%" runat="server" />
            </td>
        </tr>   
        <tr>
            <td colspan="4" class="oddrow-l">
                <%--<asp:Button ID="btnSave" runat="server" Text=" 保存 " OnClientClick="att();" CausesValidation="false" CssClass="widebuttons" OnClick="btnSave_Click" />   
                    &nbsp;--%><asp:Button ID="btnCommit" runat="server" Text=" 提交 " OnClientClick="att();" CausesValidation="false" CssClass="widebuttons" OnClick="btnCommit_Click" />                 
                &nbsp;<asp:Button ID="btnBack" runat="server" Text=" 返回 " CssClass="widebuttons" CausesValidation="false" OnClick="btnBack_Click" />
            </td>
        </tr>            
   </table>
    <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
        runat="server" />
        <input id="SelectedModuleArr" type="hidden" value="" runat="server" NAME="SelectedModuleArr" /> 
        <input id="SelectedModuleName" type="hidden" value="" runat="server" name="SelectedModuleName" />
		<input id="UpdateModuleArr" type="hidden" value="" runat="server" NAME="UpdateModuleArr" />
		<input id="UpdateModuleName" type="hidden" value="" runat="server" name="UpdateModuleName" />
		<input id="SelectedBossArr" type="hidden" value="" runat="server" NAME="SelectedBossArr" />
		<input id="SelectedBossName" type="hidden" value="" runat="server" name="SelectedBossName" />
		<input id="UpdateBossArr" type="hidden" value="" runat="server" NAME="UpdateBossArr" />
		<input id="UpdateBossName" type="hidden" value="" runat="server" name="UpdateBossName" />
		<input id="hidnowBasePay" type="hidden" runat="server" />
		<input id="hidnowMeritPay" type="hidden" runat="server" />
</asp:Content>
