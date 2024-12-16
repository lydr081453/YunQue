<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="TalentNew.aspx.cs" MasterPageFile="~/MasterPage.master"
    Inherits="SEPAdmin.Talents.TalentNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="/HR/Employees/js/jquery-1.2.6.js"></script>

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script type="text/javascript">
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }
    </script>

    <table width="100%" class="tableForm" style="border-color: #CED4E7;">
        <tr>
            <td class="oddrow" style="width: 10%">姓名:
            </td>
            <td class="oddrow-l" style="width: 20%;">
                <asp:TextBox runat="server" ID="txtName"></asp:TextBox>
                <font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
                    Display="Dynamic" ErrorMessage="请填写姓名">请填写姓名</asp:RequiredFieldValidator>
            </td>
            <td class="oddrow" style="width: 10%">联系电话:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:TextBox runat="server" ID="txtMobile"></asp:TextBox>
                <font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMobile"
                    Display="Dynamic" ErrorMessage="请填写联系电话">请填写联系电话</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">出生年份:
            </td>
            <td class="oddrow-l" style="width: 20%;">
                <asp:TextBox runat="server" ID="txtBirthday" onkeyDown="return false; " onclick="setDate(this);"></asp:TextBox>
                <font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtBirthday"
                    Display="Dynamic" ErrorMessage="请填写出生年份">请填写出生年份</asp:RequiredFieldValidator>
            </td>
            <td class="oddrow" style="width: 10%">学 历:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:DropDownList runat="server" ID="ddlEducation">
                    <asp:ListItem Text="请选择" Value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="博士" Value="博士"></asp:ListItem>
                    <asp:ListItem Text="EMBA" Value="EMBA"></asp:ListItem>
                    <asp:ListItem Text="MBA" Value="MBA"></asp:ListItem>
                    <asp:ListItem Text="硕士" Value="硕士"></asp:ListItem>
                    <asp:ListItem Text="本科" Value="本科"></asp:ListItem>
                    <asp:ListItem Text="大专" Value="大专"></asp:ListItem>
                    <asp:ListItem Text="中专" Value="中专"></asp:ListItem>
                    <asp:ListItem Text="高中" Value="高中"></asp:ListItem>
                    <asp:ListItem Text="中技" Value="中技"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">语 言:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:DropDownList runat="server" ID="ddlLanguage">
                    <asp:ListItem Text="请选择" Value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="英语" Value="英语"></asp:ListItem>
                    <asp:ListItem Text="日语" Value="日语"></asp:ListItem>
                    <asp:ListItem Text="韩语" Value="韩语"></asp:ListItem>
                    <asp:ListItem Text="其他" Value="其他"></asp:ListItem>
                </asp:DropDownList>
                 <font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlLanguage" InitialValue="0"
                    Display="Dynamic" ErrorMessage="请填写语言">请填写语言</asp:RequiredFieldValidator>
            </td>
            <td class="oddrow" style="width: 10%">工作起始年度:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:TextBox runat="server" ID="txtWorkBegin" onkeyDown="return false; " onclick="setDate(this);"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">工作所在地：
            </td>
            <td class="oddrow-l">
                <asp:TextBox runat="server" ID="txtArea"></asp:TextBox>
                <font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtArea"
                    Display="Dynamic" ErrorMessage="请填写工作所在地">请填写工作所在地</asp:RequiredFieldValidator>
            </td>
            <td class="oddrow" style="width: 10%">应聘职位:
            </td>
            <td class="oddrow-l" style="width: 20%;">
                <asp:TextBox runat="server" ID="txtPosition"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">职能:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:CheckBoxList runat="server" ID="chkProfessional" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="8" Width="90%" CellPadding="10" CellSpacing="10">
                    <asp:ListItem Value="客户管理" Text="客户管理" Selected="False"></asp:ListItem>
                    <asp:ListItem Value="媒介" Text="媒介" Selected="False"></asp:ListItem>
                    <asp:ListItem Value="策划" Text="策划" Selected="False"></asp:ListItem>
                    <asp:ListItem Value="文案" Text="文案" Selected="False"></asp:ListItem>
                    <asp:ListItem Value="活动" Text="活动" Selected="False"></asp:ListItem>
                    <asp:ListItem Value="创意" Text="创意" Selected="False"></asp:ListItem>
                    <asp:ListItem Value="策略" Text="策略" Selected="False"></asp:ListItem>
                    <asp:ListItem Value="设计" Text="设计" Selected="False"></asp:ListItem>
                    <asp:ListItem Value="电商" Text="电商" Selected="False"></asp:ListItem>
                    <asp:ListItem Value="传统PR" Text="传统PR" Selected="False"></asp:ListItem>
                    <asp:ListItem Value="Social" Text="Social" Selected="False"></asp:ListItem>
                    <asp:ListItem Value="财务" Text="财务" Selected="False"></asp:ListItem>
                    <asp:ListItem Value="产品" Text="产品" Selected="False"></asp:ListItem>
                    <asp:ListItem Value="技术" Text="技术" Selected="False"></asp:ListItem>
                    <asp:ListItem Value="其他" Text="其他" Selected="False"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">服务行业:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:CheckBoxList runat="server" ID="chklist" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="8" Width="90%" CellPadding="10" CellSpacing="10">
                    <asp:ListItem Value="汽车" Text="汽车" Selected="False"></asp:ListItem>
                    <asp:ListItem Value="消费品" Text="消费品" Selected="False"></asp:ListItem>
                    <asp:ListItem Value="奢侈品" Text="奢侈品" Selected="False"></asp:ListItem>
                    <asp:ListItem Value="科技" Text="科技" Selected="False"></asp:ListItem>
                    <asp:ListItem Value="互联网" Text="互联网" Selected="False"></asp:ListItem>
                    <asp:ListItem Value="3C" Text="3C" Selected="False"></asp:ListItem>
                    <asp:ListItem Value="娱乐营销" Text="娱乐营销" Selected="False"></asp:ListItem>
                    <asp:ListItem Value="游戏" Text="游戏" Selected="False"></asp:ListItem>
                    <asp:ListItem Value="B2B" Text="B2B" Selected="False"></asp:ListItem>
                    <asp:ListItem Value="其他" Text="其他" Selected="False"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td class="oddrow" colspan="4">HR面试意见及反馈:
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:TextBox runat="server" ID="txtHR" TextMode="MultiLine" Height="100px" Width="80%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow" colspan="4">团队面试意见及反馈:
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:TextBox runat="server" ID="txtGroup" TextMode="MultiLine" Height="100px" Width="80%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow" colspan="4">简历详细信息:
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:TextBox runat="server" ID="txtResume" TextMode="MultiLine" Width="80%" Height="150px"></asp:TextBox>
                <font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtResume"
                    Display="Dynamic" ErrorMessage="请填写简历详细信息">请填写简历详细信息</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow">简历附件:（5M以内，格式不限）
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:FileUpload runat="server" ID="upFiles"/>
                <asp:HyperLink runat="server" ID="hpFile" Target="_blank"></asp:HyperLink><br />
            </td>
        </tr>
        <tr>
            <td class="oddrow" colspan="4">留言板:
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Label runat="server" ID="lblMessage"></asp:Label>
                <asp:TextBox runat="server" ID="txtMessage" TextMode="MultiLine" Height="60px" Width="80%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Button ID="btnSave" runat="server" CssClass="widebuttons" OnClick="btnSave_Click"
                    Text=" 保 存 " />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnReturn" runat="server" CssClass="widebuttons" OnClick="btnReturn_Click"
                    CausesValidation="false" Text=" 返 回 " />
            </td>
        </tr>
    </table>
</asp:Content>
