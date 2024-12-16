<%@ Page Language="C#" AutoEventWireup="true" Inherits="Transfer_SalaryTransferEdit"  MasterPageFile="~/MasterPage.master" Codebehind="SalaryTransferEdit.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server" >
    <script type="text/javascript" src="/public/js/DatePicker.js"></script>
<script src="/public/js/jquery.js" type="text/javascript"></script>
    <script language="javascript">
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
    </script>
<input type="hidden" id="hidPayChange" runat="server"/>
<table width="100%" class="tableForm" id="tableUp"> 
        <tr>
             <td class="heading" colspan="10">薪资调整
             </td>
        </tr>
        <tr>
            <td class="oddrow"  style="width: 10%">中文姓名:</td>
            <td class="oddrow-l"  style="width:30%">
            <asp:TextBox ID="txtuserName" runat="server" Enabled="false" /><asp:HiddenField ID="hiduserId" runat="server" />
            </td>
            <td class="oddrow">工资调整</td>
            <td class="oddrow-l"><asp:DropDownList ID="ddlPayChange"  runat="server" />
            </td>
            </tr>   
            <div id="divSalary" runat="server">        
        <tr>
            <td class="oddrow" >
                原基本工资:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtnowBasePay" runat="server" Enabled="false"/>
            </td>  
            <td class="oddrow">
            原绩效工资               
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtnowMeritPay" runat="server" Enabled="false"/>
            </td>   
            </tr>
        <tr>
             <td class="oddrow" >
                新基本工资:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtnewBasePay" runat="server" /><font color="red">* </font><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                    runat="server" ErrorMessage="请填写新基本工资" Display="None" ControlToValidate="txtnewBasePay" /><asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtnewBasePay"
                    ErrorMessage="请输入正确新基本工资" ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">请输入正确新基本工资</asp:RegularExpressionValidator>
            </td>  
            <td class="oddrow">
            新绩效工资               
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtnewMeritPay" runat="server" /><font color="red"> * </font><asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                    runat="server" ErrorMessage="请填写新业务绩效" Display="None" ControlToValidate="txtnewMeritPay" /><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtnewMeritPay"
                    ErrorMessage="请输入正确新业务绩效" ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">请输入正确新绩效工资</asp:RegularExpressionValidator>
            </td> 
            </tr>
            </div>
        <tr>
            <td class="oddrow"  style="width: 10%">
                职位:
            </td>
            <td class="oddrow-l" >
                <asp:TextBox ID="txtjob" runat="server" Enabled="false"/>
            </td>
            <td class="oddrow">
                生效日期:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtoperationDate" runat="server" onclick="setDate(this);" /><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                    runat="server" ErrorMessage="请选择生效日期" Display="None" ControlToValidate="txtoperationDate" /><font
                        color="red"> * </font>
            </td>
            </tr>
        <tr>
            <td class="oddrow">
            说明：             
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtmemo" TextMode="MultiLine" Height="100px" Width="100%" runat="server" />
            </td>                               
        </tr>   
        <asp:HiddenField ID="hidcompanyId" runat="server" />
        <asp:HiddenField ID="hiddepartmentId" runat="server" />
        <asp:HiddenField ID="hidgroupId" runat="server" />    
        <asp:HiddenField ID="hidcompanyName" runat="server" />
        <asp:HiddenField ID="hiddepartmentName" runat="server" />
        <asp:HiddenField ID="hidgroupName" runat="server" />   
        <%--<tr>
         <td class="oddrow">
                申报人（总经理）:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtJob_declarer" runat="server" />
            </td>
            <td class="oddrow">
                申报时间:
            </td>
            <td class="oddrow-l"><asp:TextBox ID="txtJob_declarerdate" runat="server" onclick="setDate(this);" /></td>
            </tr>
            <tr>
            <td class="oddrow" >
              集团核准（总裁/执行副总裁）:
            </td>
            <td class="oddrow-l" >
                <asp:TextBox ID="txtJob_groupApprover" runat="server" />
            </td>  
            <td class="oddrow">
                集团核准时间:
            </td>
            <td class="oddrow-l"><asp:TextBox ID="txtJob_approverdate" runat="server" onclick="setDate(this);" /></td>            
        </tr>                 
        <tr>
            <td class="oddrow">
               集团人事部（主管人）:
            </td>           
            <td class="oddrow-l" >
                <asp:TextBox ID="txtJob_groupHr" runat="server" />
            </td>
            <td class="oddrow" >
                集团人事部时间:
            </td>
            <td class="oddrow-l" ><asp:TextBox ID="txtJob_hrdate" runat="server" onclick="setDate(this);" /></td>  
            </tr>--%>
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

</asp:Content>

