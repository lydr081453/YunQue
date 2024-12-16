<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Media_ReporterAddAndEdit" Title="新建记者" Codebehind="ReporterAddAndEdit.aspx.cs" %>

<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<%@ Register TagPrefix="cc2" Namespace="MyControls" Assembly="MyControls" %>
<%@ Register TagPrefix="uc" TagName="Experience" Src="~/Media/skins/Experience.ascx" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="contentHead" runat="server">

    <script type="text/javascript">

        function ReloadOpener(tabIndex, pjid) {
            window.opener.location.replace("../Project/ProjectMediaAddAndEdit.aspx?<%=RequestName.ProjectID %>=" + pjid);
            window.close();
        }

        function FileChange(Value) {
            document.getElementById("<% = uploadimage.ClientID %>").width = 24;
            document.getElementById("<% = uploadimage.ClientID %>").height = 24;
            document.getElementById("<% = uploadimage.ClientID %>").alt = "";
            document.getElementById("<% = uploadimage.ClientID %>").src = Value;

        }

        function check() {
            var meg = "";
            //姓名
            if (document.getElementById("<% = txtName.ClientID %>").value == "") {

                meg += "姓名必须填写" + "\n";

            }
            //邮编
            if (document.getElementById("<% = txtPostCord.ClientID %>").value != "") {

                if (document.getElementById("<% = txtPostCord.ClientID %>").value.search(/^(\d){6}$/) == -1) {
                    meg += "家庭邮编输入错误！" + "\n";
                }
            }
            //身份证号 
            if (document.getElementById("<% = txtIdCard.ClientID %>").value != "") {

                if (document.getElementById("<% = txtIdCard.ClientID %>").value.search(/^\d{15}(\d{2}[A-Za-z0-9])?$/) == -1) {
                    meg += "身份证输入错误！" + "\n";
                }
            }
            // 办公电话
//            if (document.getElementById("<% = txtOfficePhone.ClientID %>").value != "") {
//                if (document.getElementById("<% = txtOfficePhone.ClientID %>").value.search(/^((\(\d{2,3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9]\d{6,7}(\-\d{1,4})?$/) == -1) {
//                    meg += "办公电话输入错误！" + "\n";
//                }
//            }
            //家庭电话
//            if (document.getElementById("<% = txtHomePhone.ClientID %>").value != "") {
//                if (document.getElementById("<% = txtHomePhone.ClientID %>").value.search(/^((\(\d{2,3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9]\d{6,7}(\-\d{1,4})?$/) == -1) {
//                    meg += "家庭电话输入错误！" + "\n";
//                }
//            }

            //常用手机
            if (document.getElementById("<% = txtUsualMobile.ClientID %>").value == "") {
                meg += "常用手机号码必须输入" + "\n";
            }
//            else if (document.getElementById("<% = txtUsualMobile.ClientID %>").value.search(/^((\(\d{2,3}\))|(\d{3}\-))?13\d{9}\d{5}$/) == -1) {
//                meg += "常用手机号码输入错误" + "\n";
//            }


            //备用手机
            if (document.getElementById("<% = txtBackupMobile.ClientID %>").value != "") {
                if (document.getElementById("<% = txtBackupMobile.ClientID %>").value.search(/^((\(\d{2,3}\))|(\d{3}\-))?13\d{9}$/) == -1) {
                    meg += "备用手机号码输入错误" + "\n";

                }
            }

            //传真
            if (document.getElementById("<% = txtFax.ClientID %>").value != "") {
                if (document.getElementById("<% = txtFax.ClientID %>").value.search(/^((\(\d{2,3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9]\d{6,7}(\-\d{1,4})?$/) == -1) {
                    meg += "传真号码输入错误！" + "\n";
                }
            }
            //QQ号码

            if (document.getElementById("<% = txtQq.ClientID %>").value != "") {
                if (document.getElementById("<% = txtQq.ClientID %>").value.search(/^[1-9]\d{4,8}$/) == -1) {
                    meg += "QQ号码输入错误！" + "\n";
                }
            }
            //MSN号码

            if (document.getElementById("<% = txtMsn.ClientID %>").value != "") {
                if (document.getElementById("<% = txtMsn.ClientID %>").value.search(/(\S)+[@]{1}(\S)+[.]{1}(\w)/) == -1) {
                    meg += "MSN号码输入错误！" + "\n";
                }
            }
          
            if (document.getElementById("<% = txtEmailOne.ClientID %>").value != "") {
                if (document.getElementById("<% = txtEmailOne.ClientID %>").value.search(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/) == -1) {
                    meg += "邮箱1输入错误！" + "\n";
                }
            }
            // 邮箱2
            if (document.getElementById("<% = txtEmailTwo.ClientID %>").value != "") {
                if (document.getElementById("<% = txtEmailTwo.ClientID %>").value.search(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/) == -1) {
                    meg += "邮箱2输入错误！" + "\n";
                }
            }

            if (document.getElementById("<%=txtMediaName.ClientID %>").value == "") {
                meg += "请选择关联媒体！";
            }

            if (meg != "") {
                alert(meg);
                return false;
            }

        }
        function checkbank() {
            var meg = "";
            if (document.getElementById("<% = txtBankcardCode.ClientID %>").value == "") {
                meg += "记者银行卡号不能为空！" + "\n";

            }

            if (document.getElementById("<% = txtBankName.ClientID %>").value == "") {
                meg += "开户行名称不能为空！\n";


            }
            if (document.getElementById("<% = txtbankacountname.ClientID %>").value == "") {
                meg += "开户姓名不能为空！\n";


            }
            if (document.getElementById("<% = txtWritingfee.ClientID %>").value != "") {
                if (document.getElementById("<% = txtWritingfee.ClientID %>").value.search(/^[-\+]?\d+(\.\d+)?$/) == -1) {

                    meg += "记者稿酬输入错误！\n";
                }
            }


            ddlpaymentmode = document.getElementById("<% = ddlpaymentmode.ClientID %>");
            if (ddlpaymentmode.selectedIndex <= 0) {
                meg += "请选择付款方式！" + "\n";

            }
            if (meg != "") {
                alert(meg);
                return false;
            }

        }
    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">
    <table style="width: 100%" border="1" class="tableForm">
        <tr>
            <td class="menusection-Packages" colspan="4">
                <asp:Label ID="labHeading" runat="server">添加记者</asp:Label>
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="4">
                基本信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                姓名：
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox ID="txtName" runat="server" MaxLength="30"></asp:TextBox><font color="red">
                    *</font>
            </td>
            <td class="oddrow" style="width: 20%">
                笔名：
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox ID="txtPenName" runat="server" MaxLength="30"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                所属媒体名称：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtMediaName" runat="server" Enabled="false" /><font color="red"> *
                </font>
                <asp:HiddenField ID="hidMedia" runat="server" />
                &nbsp;<asp:Button ID="btnLink" runat="server" OnClick="btnLink_Click" Text="变更所属媒体"
                    CssClass="bigwidebuttons" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                性别：
            </td>
            <td class="oddrow-l">
                <asp:DropDownList ID="ddlSex" runat="server" CssClass="fixddl" Width="60px">
                    <asp:ListItem Selected="True" Text="男" Value="1"></asp:ListItem>
                    <asp:ListItem Text="女" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="oddrow">
                职务：
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtReporterPosition" runat="server" MaxLength="50" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                负责领域：
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtresponsibledomain" runat="server" MaxLength="100" />
            </td>
            <td class="oddrow">
                办公电话：
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtOfficePhone" runat="server" MaxLength="50"></asp:TextBox><br />
                <asp:Label ID="labOfficePhone" runat="server">例如: 010-88888888</asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                常用手机：
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtUsualMobile" runat="server" MaxLength="50"></asp:TextBox>
                <font color="red">*</font><br />
                <asp:Label ID="labUsualMobile" runat="server">例如: 13088888888</asp:Label>
            </td>
            <td class="oddrow">
                备用手机：
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtBackupMobile" runat="server" MaxLength="50"></asp:TextBox><br />
                <asp:Label ID="labBackupMobile" runat="server">例如: 13688888888</asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                Msn 号码：
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtMsn" runat="server" MaxLength="50"></asp:TextBox><br />
                <asp:Label ID="labMsn" runat="server">例如: aa@163.com</asp:Label>
            </td>
            <td class="oddrow">
                QQ 号码：
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtQq" runat="server" MaxLength="20"></asp:TextBox><br />
                <asp:Label ID="labQq" runat="server">请正确输入QQ 号码</asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                其它通讯方式：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtOtherMessageSoftware" runat="server" MaxLength="30"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                电子邮件1：
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtEmailOne" runat="server" MaxLength="50"></asp:TextBox><br />
                <asp:Label ID="labEmailOne" runat="server">例如: aa@163.com</asp:Label>
            </td>
            <td class="oddrow">
                电子邮件2：
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtEmailTwo" runat="server" MaxLength="50"></asp:TextBox><br />
                <asp:Label ID="labEmailTwo" runat="server">例如: aa@163.com</asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                办公邮编：
            </td>
            <td class="oddrow-l">
                  <asp:TextBox ID="txtOfficePostID" runat="server" MaxLength="10"></asp:TextBox><br />
                <asp:Label ID="Label4" runat="server">例如: 100000</asp:Label>
            </td>
            <td id="Td1" class="oddrow">
                传 真：
            </td>
            <td id="Td2" class="oddrow-l">
                <asp:TextBox ID="txtFax" runat="server" MaxLength="50"></asp:TextBox><br />
                <asp:Label ID="labFax" runat="server">例如: 010-88888888</asp:Label>
            </td>
        </tr>
        <tr>
        <td class="oddrow">办公地址：</td>
        <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtOfficeAddress" runat="server" Width="80%" MaxLength="500"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="border: 0px; height: 30px">
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="4">
                个人信息
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                生日：
            </td>
            <td class="oddrow-l">
                <cc2:DatePicker ID="dpBirthday" runat="server"></cc2:DatePicker>
            </td>
            <td class="oddrow">
                身份证号：
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtIdCard" runat="server" MaxLength="30"></asp:TextBox><br />
                <asp:Label ID="labIdCard" runat="server">请正确输入身份证号码</asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                籍贯：
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtHometown" runat="server" MaxLength="50"></asp:TextBox>
            </td>
            <td class="oddrow">
                婚姻状况：
            </td>
            <td class="oddrow-l">
                <asp:DropDownList ID="ddlMarriage" runat="server" CssClass="fixddl" Width="60px">
                    <asp:ListItem Text="保密" Value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="未婚" Value="1"></asp:ListItem>
                    <asp:ListItem Text="已婚" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                性格特点：
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtCharacter" runat="server" MaxLength="200"></asp:TextBox>
            </td>
            <td class="oddrow">
                兴趣爱好：
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtHobby" runat="server" MaxLength="200"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                家庭邮编：
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtPostCord" runat="server" MaxLength="10"></asp:TextBox><br />
                <asp:Label ID="labPostCord" runat="server">例如: 100000</asp:Label>
            </td>
            <td class="oddrow">
                家庭电话：
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtHomePhone" runat="server" MaxLength="50"></asp:TextBox><br />
                <asp:Label ID="labHomePhone" runat="server">例如: 010-88888888</asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                家庭地址：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtAddress" runat="server" Width="80%" MaxLength="500"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                家庭成员：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtFamily" Height="137px" Width="80%" TextMode="MultiLine" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                个人照片：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:FileUpload ID="ImageUpload" runat="server" Width="80%" />
                <asp:Image ID="uploadimage" runat="server" Height="24px" Width="24px" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                主要作品：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtWriting" runat="server" Height="137px" TextMode="MultiLine" Width="80%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                教育背景：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtEducation" runat="server" Height="137px" TextMode="MultiLine"
                    Width="80%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                职业经历：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtExperience" runat="server" Height="137px" TextMode="MultiLine"
                    Width="80%"></asp:TextBox>
            </td>
        </tr>
        <asp:HiddenField ID="hidBackUrl" runat="server" />
        <asp:HiddenField ID="hidRid" runat="server" />
    </table>
    <table style="width: 100%;" border="1" class="tableForm" id="tbPrivacy" visible="false"
        runat="server">
        <tr>
            <td colspan="4" class="heading">
                <asp:Label ID="Label1" runat="server"> 请添加记者私密信息</asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%;">
                开户银行：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtBankName" runat="server" MaxLength="30" /><font color="red"> *</font>例：中国工商银行北京市朝阳区建外分行
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%;">
                开户姓名：
            </td>
            <td class="oddrow-l" style="width: 30%;">
                <asp:TextBox ID="txtbankacountname" runat="server" MaxLength="20" /><font color="red">
                    *</font>
            </td>
            <td class="oddrow" style="width: 20%;">
                账号：
            </td>
            <td class="oddrow-l" style="width: 30%;">
                <asp:TextBox ID="txtBankcardCode" runat="server" MaxLength="20" /><font color="red">
                    *</font>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                稿酬标准：
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtWritingfee" runat="server" MaxLength="12" />
            </td>
            <td class="oddrow">
                付款方式：
            </td>
            <td class="oddrow-l">
                <asp:DropDownList CssClass="fixddl" ID="ddlpaymentmode" runat="server">
                </asp:DropDownList>
                <font color="red">*</font>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                有无发票：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:DropDownList ID="ddlHaveInvoice" runat="server" CssClass="fixddl">
                    <asp:ListItem Text="无" Value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="有" Value="1"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                合作情况：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox TextMode="MultiLine" ID="txtcooperatecircs" runat="server" Width="86%"
                    Height="35px" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                备注：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox TextMode="MultiLine" ID="txtPrivateRemark" runat="server" Width="86%"
                    Height="35px" />
            </td>
        </tr>
        <tr>
            <td colspan="4" align="right">
                <asp:Button ID="btnSave" runat="server" Text="保存记者私密信息" OnClientClick="return checkbank();"
                    CssClass="widebuttons" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellpadding="2" cellspacing="2" id="tbPrivacyList"
        runat="server" visible="false">
        <tr>
            <td class="headinglist">
                记者私密信息列表
            </td>
        </tr>
        <tr>
            <td>
                <cc4:MyGridView ID="dgList" runat="server" DataKeyNames="id" OnRowDataBound="dgList_OnRowDataBound">
                </cc4:MyGridView>
            </td>
        </tr>
    </table>
    <table border="0" width="100%">
        <tr>
            <td align="right" colspan="4">
                <asp:Button ID="btnOk" runat="server" OnClientClick="return check();" CssClass="widebuttons"
                    Text="保存" OnClick="btnOk_Click" />
                <input type="reset" class="widebuttons" value="重置" />
                <asp:Button ID="btnBack" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnBack_Click" />
                <asp:Button ID="btnClose" runat="server" Text=" 关闭 " CssClass="widebuttons" OnClientClick="window.close();return false;" />
            </td>
        </tr>
    </table>
</asp:Content>
