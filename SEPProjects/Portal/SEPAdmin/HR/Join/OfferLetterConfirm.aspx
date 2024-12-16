<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OfferLetterConfirm.aspx.cs"
    Inherits="OfferLetterConfirm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../public/CutImage/css/main.css" type="text/css" rel="Stylesheet" />

    <script type="text/javascript" src="../../public/CutImage/js/jquery1.2.6.pack.js"></script>

    <script type="text/javascript" src="../../public/CutImage/js/ui.core.packed.js"></script>

    <script type="text/javascript" src="../../public/CutImage/js/ui.draggable.packed.js"></script>

    <script type="text/javascript" src="../../public/CutImage/js/CutPic.js"></script>

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <style>
        *{
  padding: 0;
  margin: 0;
  box-sizing: border-box;
  font-family: "Poppins",sans-serif;
}
body{
  background-color: #025bee;
}
.wrapper{
  width: min(90%,800px);
  position: absolute;
  transform: translateX(-50%);
  left:50%;
  top:1em;
  background-color: #fff;
  padding: 2em 3em;
  border-radius: .5em;
}
.container{
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap:1em;
}
.container .image-container,.container .preview-container{
   width: 100%;
   /* background-color: aquamarine; */
}

input[type="file"]{
  display: none;
}

label{
  display: block;
  position: relative;
  background-color: #025bee;
  font-size: 16px;
  text-align: center;
  width: 250px;
  color:#fff;
  padding: 16px 0;
  margin: 16px auto;
  cursor: pointer;
  border-radius: 5px;
}

img{
  display: block;
  /**is key to cropper.js**/
  max-width: 100%;
}

.image-container{
  width: 60%;
  margin: 0 auto;
}

.options{
  display: flex;
  justify-content: center;
  gap:1em;
}
input[type="number"]{
  width: 100px;
  padding: 16px 5px;
  border-radius: .3em;
  border: 2px solid #000;
}

button{
  padding: 1em;
  border-radius: .3em;
  border: 2px solid #025bee;
  background-color: #fff;
  color: #025bee;
}

.btns{
  display: flex;
  justify-content: center;
  gap: 1em;
  margin-top: 1em;

}
.btns button{
  font-size: 1em;
}
.btns a {
  border: 2px solid #025bee;
  background-color: #025bee;
  color: #fff;
  text-decoration: none;
  padding: 1em;
  font-size: 1em;
  border-radius: .3em;
}
.hide{
  display: none;
}

    </style>
</head>
<body>
    <script type="text/javascript">

        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }


        function isIdCardNo(num) {
            // num = num.toUpperCase();
            //身份证号码为15位或者18位，15位时全为数字，18位前17位为数字，最后一位是校验位，可能为数字或字符X。   
            if (!(/(^\d{15}$)|(^\d{17}([0-9]|X)$)/.test(num))) {
                alert('输入的身份证号长度不对，或者号码不符合规定！\n15位号码应全为数字，18位号码末位可以为数字或X。');
                return false;
            }
            //校验位按照ISO 7064:1983.MOD 11-2的规定生成，X可以认为是数字10。 
            //下面分别分析出生日期和校验位 
            var len, re;
            len = num.length;
            if (len == 18) {
                re = new RegExp(/^(\d{6})(\d{4})(\d{2})(\d{2})(\d{3})([0-9]|X)$/);
                var arrSplit = num.match(re);

                //检查生日日期是否正确 
                var dtmBirth = new Date(arrSplit[2] + "/" + arrSplit[3] + "/" + arrSplit[4]);
                var bGoodDay;
                bGoodDay = (dtmBirth.getFullYear() == Number(arrSplit[2])) && ((dtmBirth.getMonth() + 1) == Number(arrSplit[3])) && (dtmBirth.getDate() == Number(arrSplit[4]));
                if (!bGoodDay) {
                    alert(dtmBirth.getYear());
                    alert(arrSplit[2]);
                    alert('输入的身份证号里出生日期不对！');
                    return false;
                }
                else {
                    //检验18位身份证的校验码是否正确。 
                    //校验位按照ISO 7064:1983.MOD 11-2的规定生成，X可以认为是数字10。 
                    var valnum;
                    var arrInt = new Array(7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2);
                    var arrCh = new Array('1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2');
                    var nTemp = 0, i;
                    for (i = 0; i < 17; i++) {
                        nTemp += num.substr(i, 1) * arrInt[i];
                    }
                    valnum = arrCh[nTemp % 11];
                    if (valnum != num.substr(17, 1)) {
                        alert('18位身份证的校验码不正确！应该为：' + valnum);
                        return false;
                    }
                    return num;
                }
            }
            return false;
        }


        function validInput() {
            var msg = "";
            if (document.getElementById("<%=txtBase_FirstNameEn.ClientID %>").value == "") {
                msg += "请填写名\n";
            }
            if (document.getElementById("<%=txtBase_LastNameEn.ClientID %>").value == "") {
                msg += "请填写姓\n";
            }
            if (document.getElementById("<%=txtIDCard.ClientID %>").value == "") {
                msg += "请填写身份证号\n";
            }
            if (document.getElementById("<%=txtMobilePhone.ClientID %>").value == "") {
                msg += "请填写手机号码\n";
            }
            if (document.getElementById("<%=ddlGender.ClientID %>").value == "-1") {
                msg += "请填写性别\n";
            }
            if (document.getElementById("<%=ddlHuji.ClientID %>").value == "-1") {
                msg += "请填写户口属性\n";
            }
            if (document.getElementById("<%=ddlPolicity.ClientID %>").value == "-1") {
                msg += "请填写政治面貌\n";
            }

            if (document.getElementById("<%=txtSalaryBank.ClientID %>").value == "") {
                msg += "请填写工资卡银行\n";
            }
            if (document.getElementById("<%=txtSalaryCardNo.ClientID %>").value == "") {
                msg += "请填写工资卡号\n";
            }

            if (document.getElementById("<%=ddlSocialSecurty.ClientID %>").value == "-1") {
                msg += "请填写社保类型\n";
            }
            if (document.getElementById("<%=txtNation.ClientID %>").value == "") {
                msg += "请填写民族\n";
            }
            if (document.getElementById("<%=txtBase_Marriage.ClientID %>").value == "0") {
                msg += "请填写婚姻状况\n";
            }
            if (document.getElementById("<%=txtBase_DomicilePlace.ClientID %>").value == "") {
                msg += "请填写户口所在地\n";
            }
            if (document.getElementById("<%=txtBase_Address1.ClientID %>").value == "") {
                msg += "请填写通讯地址\n";
            }
            if (document.getElementById("<%=txtProfessional.ClientID %>").value == "") {
                msg += "请填写专业\n";
            }
            if (document.getElementById("<%=txtSchool.ClientID %>").value == "") {
                msg += "请填写毕业院校\n";
            }
            if (document.getElementById("<%=txtWorkBegin.ClientID %>").value == "") {
                msg += "请填写参加工作日期\n";
            }
            if (document.getElementById("<%=txtBase_EmergencyLinkman.ClientID %>").value == "") {
                msg += "请填写紧急联系人\n";
            }
            if (document.getElementById("<%=txtBase_EmergencyPhone.ClientID %>").value == "") {
                msg += "请填写紧急联系人电话\n";
            }
            if (document.getElementById("<%=txtBase_WorkExperience.ClientID %>").value == "") {
                msg += "请填写工作简历\n";
            }
            if (document.getElementById("<%=txtBase_WorkSpecialty.ClientID %>").value == "") {
                msg += "请填写工作特长或荣誉\n";
            }


            if (msg != "") {
                alert(msg);
                return false;
            }

            return true;
        }
    </script>

    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="1024" align="center">
            <tr>
                <td align="center">
                    <p />
                    <span style="font-size: 15pt; font-weight: bold;">聘 用 通 知<p />
                    </span>
                </td>
            </tr>
            <tr>
                <td align="center">&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <img src="../../Images/xingyan.png" style="margin: 15px 0 20px 20px;" />
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" width="1024" align="center">
            <tr>
                <td colspan="4" style="background-color: #ee770f; color: White; margin: 10px 0 10px 0;">
                    <table border="0" cellpadding="0" cellspacing="0" align="center" width="95%" style="padding: 20px 0 20px 0;">
                        <tr>
                            <td style="font-size: 14pt; font-weight: bold;">尊敬的<asp:Label ID="labUserName" runat="server" />您好
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 12pt;">&nbsp;&nbsp;&nbsp;&nbsp;感谢您应聘本公司，我们非常荣幸地通知您，您经初审合格，依本公司任用规定给予录用，竭诚欢迎您加入本公司行列。有关录用报到事项如下，敬请参照办理。
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="font-size: 12pt; background-color: #f8f8f8;">
                    <table border="0" cellpadding="0" cellspacing="0" align="center" width="95%" style="padding: 10px 0 10px 0;">
                        <tr>
                            <td style="font-size: 12pt; font-weight: bold; color: #f2720f">一、报到时间：<asp:Label ID="labJoinDate" runat="server" />早9:30（工作时间 9:30---18:30）
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 12pt; padding: 0 0 0 32px;">报到地点：<asp:Label ID="labAddress" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <hr style="border-top: 1px dashed #ee770f; height: 1px" />
                    <table border="0" cellpadding="0" cellspacing="0" align="center" width="95%" style="padding: 10px 0 10px 0;">
                        <tr>
                            <td style="font-size: 12pt; font-weight: bold; color: #f2720f">二、携带正本资料：
                            </td>
                        </tr>	
                         <tr>
                            <td style="font-size: 12pt; padding: 0 0 0 32px;">（一）离职证明原件（需加盖人事部门章或者公司公章）<font color="red">*</font>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 12pt; padding: 0 0 0 32px;">（二）居民身份证原件及复印件<font color="red">*</font>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 12pt; padding: 0 0 0 32px;">（三）毕业证和学位证（原件及复印件）<font color="red">*</font>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 12pt; padding: 0 0 0 32px;">（四）近三月入职体检报告<font color="red">*</font>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 12pt; padding: 0 0 0 32px;">（五）担保书（担保人：父亲或母亲；担保人工作单位；详细家庭住址；座机及手机电话）
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 12pt; padding: 0 0 0 32px;">（六）一寸照片：近期免冠白底彩色照片
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 12pt; padding: 0 0 0 32px;">（七）本人招商银行储蓄卡一类卡账号复印件（北京工资卡）；本人工商银行储蓄卡一类卡账号复印件（重庆工资卡）<asp:Label ID="lblCard" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <hr style="border-top: 1px dashed #ee770f; height: 1px" />
                    <table border="0" cellpadding="0" cellspacing="0" align="center" width="95%" style="padding: 10px 0 10px 0;">
                        <tr>
                            <td style="font-size: 12pt; font-weight: bold; color: #f2720f">三、薪资情况请参见附件中需要您确认的offer letter。
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 12pt; padding: 0 0 0 32px;">（填写信息：1/ 身份证号码 2/ 应聘方签字 3/ 报到日期 ）
                            </td>
                        </tr>
                    </table>
                    <hr style="border-top: 1px dashed #ee770f; height: 1px" />
                    <table border="0" cellpadding="0" cellspacing="0" align="center" width="95%" style="padding: 10px 0 10px 0;">
                        <tr>
                            <td style="font-size: 12pt; font-weight: bold; color: #f2720f">四、我们双方都认同，出现下列情形之一将不予聘用：
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 12pt; padding: 0 0 0 32px;">（一）有刑事犯罪行为者
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 12pt; padding: 0 0 0 32px;">（二）身体有严重疾病者
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 12pt; padding: 0 0 0 32px;">（三）年龄不满16周岁者
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 12pt; padding: 0 0 0 32px;">（四）曾被本公司辞退或未经批准擅自离职者
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 12pt; padding: 0 0 0 32px;">（五）与原单位劳动合同未解除者
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 12pt; padding: 0 0 0 32px;">（六）或有其他不适合情况者（如提供不实履历等不诚信行为者）
                            </td>
                        </tr>
                    </table>
                    <hr style="border-top: 1px dashed #ee770f; height: 1px" />
                    <table border="0" cellpadding="0" cellspacing="0" align="center" width="95%" style="padding: 10px 0 10px 0;">
                        <tr>
                            <td style="font-size: 12pt; font-weight: bold; color: #f2720f">五、前列事项若有疑问或困难，请与本公司人力资源部联系。
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 12pt; padding: 0 0 0 32px;">
                                <asp:Label ID="lblPhone" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <hr style="border-top: 1px dashed #ee770f; height: 1px" />
                    <table border="0" cellpadding="0" cellspacing="0" align="center" width="95%" style="padding: 10px 0 10px 0;">
                        <tr>
                            <td style="font-size: 12pt; font-weight: bold; color: #f2720f">六、请正确填写以下信息。
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 12pt; padding: 0 0 0 32px;">为方便您办理入职手续，以及星言云汇内网系统的设置，请填写您的英文名，此英文名将作为您入职以后公司内网系统的<span style="font-size: 12pt; font-weight: bold; text-decoration: underline;">登录名（格式如：FirstName.LastName）</span>，和公司邮箱前缀（FirstName.LastName@xc-ch.com），请妥善保管好。
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table border="0" cellpadding="1" cellspacing="1" align="center" width="95%" style="background-color: #f5f5f5;">
                                    <tr>
                                        <td style="background-color: #ffffff;">
                                            <table border="0" cellpadding="1" cellspacing="0" width="97%" align="center" style="margin: 10px 0 10px 0; background-color: #ffffff;">
                                                <tr>
                                                    <td colspan="4" style="font-size: 10pt; padding: 5px 10px 5px 10px; background-color: #f3f3f3;">first name.
                                                    <asp:TextBox ID="txtBase_FirstNameEn" runat="server" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtBase_FirstNameEn"
                                                            Display="Dynamic" ErrorMessage="请填写名">请填写名。</asp:RequiredFieldValidator>
                                                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="只能输入英文字母" ControlToValidate="txtBase_FirstNameEn" ValidationExpression="^[A-Za-z]+$"></asp:RegularExpressionValidator>--%>
                                                        last name
                                                    <asp:TextBox ID="txtBase_LastNameEn" runat="server" />
                                                        <font color="red">*</font>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtBase_LastNameEn"
                                                            Display="Dynamic" ErrorMessage="请填写姓">请填写姓。</asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="只能输入英文字母" ControlToValidate="txtBase_LastNameEn" ValidationExpression="^[A-Za-z]+$"></asp:RegularExpressionValidator>&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <br />
                                                        请填写汉语拼音或英文，名在前，姓氏在后。<br />
                                                        推荐格式:james.zhao,gang.zhao;如有重复:jameszhao.zhao,zhaogang.zhao,zg.zhao
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 10pt; padding: 5px 10px 0 10px; font-weight: bold; width: 10%;">身份证号:
                                                    </td>
                                                    <td align="left" style="padding: 5px 10px 0 10px;" colspan="3">
                                                        <asp:TextBox ID="txtIDCard" runat="server" onblur="isIdCardNo(this.value);" />
                                                        <font color="red">*</font>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtIDCard"
                                                            Display="Dynamic" ErrorMessage="请填写身份证号">请填写身份证号。</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="font-size: 10pt; padding: 5px 10px 0 10px; font-weight: bold;">手机号码:
                                                    </td>
                                                    <td align="left" style="padding: 5px 10px 0 10px;" colspan="3">
                                                        <asp:TextBox ID="txtMobilePhone" runat="server" />
                                                        <font color="red">*</font>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMobilePhone"
                                                            Display="Dynamic" ErrorMessage="请填写手机号码">请填写手机号码。</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 10pt; padding: 5px 10px 0 10px; font-weight: bold;">政治面貌:
                                                    </td>
                                                    <td align="left" style="padding: 5px 10px 0 10px;" colspan="3">
                                                        <asp:DropDownList ID="ddlPolicity" runat="server">
                                                            <asp:ListItem Selected="True" Value="-1" Text="请选择" />
                                                            <asp:ListItem Value="中共党员" Text="中共党员" />
                                                            <asp:ListItem Value="中共预备党员" Text="中共预备党员" />
                                                            <asp:ListItem Value="中共团员" Text="中共团员" />
                                                            <asp:ListItem Value="其他党派" Text="其他党派" />
                                                            <asp:ListItem Value="群众" Text="群众" />
                                                        </asp:DropDownList><font color="red">*</font>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 10pt; padding: 5px 10px 0 10px; font-weight: bold; width: 10%;">户口属性:
                                                    </td>
                                                    <td align="left" style="padding: 5px 10px 0 10px;">
                                                        <asp:DropDownList runat="server" ID="ddlHuji">
                                                            <asp:ListItem Selected="True" Text="请选择.." Value="-1"></asp:ListItem>
                                                            <asp:ListItem Text="本市城镇" Value="本市城镇"></asp:ListItem>
                                                            <asp:ListItem Text="外埠城镇" Value="外埠城镇"></asp:ListItem>
                                                            <asp:ListItem Text="本市农业" Value="本市农业"></asp:ListItem>
                                                            <asp:ListItem Text="外埠农业" Value="外埠农业"></asp:ListItem>
                                                        </asp:DropDownList><font color="red">*</font>
                                                    </td>
                                                    <td style="font-size: 10pt; padding: 5px 10px 0 10px; font-weight: bold; width: 10%;">社保类别:
                                                    </td>
                                                    <td align="left" style="padding: 5px 10px 0 10px;">
                                                        <asp:DropDownList runat="server" ID="ddlSocialSecurty">
                                                            <asp:ListItem Selected="True" Text="请选择.." Value="-1"></asp:ListItem>
                                                            <asp:ListItem Text="转入" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="新增" Value="1"></asp:ListItem>
                                                        </asp:DropDownList><font color="red">*</font>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 10pt; padding: 5px 10px 0 10px; font-weight: bold;">民族:
                                                    </td>
                                                    <td align="left" style="padding: 5px 10px 0 10px;">
                                                        <asp:TextBox ID="txtNation" runat="server" />
                                                        <font color="red">*</font>
                                                    </td>
                                                    <td style="font-size: 10pt; padding: 5px 10px 0 10px; font-weight: bold;">性别:
                                                    </td>
                                                    <td align="left" style="padding: 5px 10px 0 10px;">
                                                        <asp:DropDownList runat="server" ID="ddlGender">
                                                            <asp:ListItem Selected="True" Text="请选择.." Value="-1"></asp:ListItem>
                                                            <asp:ListItem Text="男" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="女" Value="2"></asp:ListItem>
                                                        </asp:DropDownList><font color="red">*</font>
                                                    </td>
                                                </tr>
                                                <tr>

                                                    <td style="font-size: 10pt; padding: 5px 10px 0 10px; font-weight: bold; width: 10%;">婚姻状况:
                                                    </td>
                                                    <td align="left" style="padding: 5px 10px 0 10px;">
                                                        <asp:DropDownList ID="txtBase_Marriage" runat="server">
                                                            <asp:ListItem Value="0" Text="未知" />
                                                            <asp:ListItem Value="1" Text="已婚" />
                                                            <asp:ListItem Value="2" Text="未婚" />
                                                            <asp:ListItem Value="3" Text="已婚有子" />
                                                            <asp:ListItem Value="4" Text="离异" />
                                                        </asp:DropDownList>
                                                        <font color="red">*</font>

                                                    </td>

                                                    <td style="font-size: 10pt; padding: 5px 10px 0 10px; font-weight: bold; width: 12%;">户口所在地:
                                                    </td>
                                                    <td align="left" style="padding: 5px 10px 0 10px;">
                                                        <asp:TextBox ID="txtBase_DomicilePlace" runat="server" />
                                                        <font color="red">*</font>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 10pt; padding: 5px 10px 0 10px; font-weight: bold; width: 12%;">紧急联系人:
                                                    </td>
                                                    <td align="left" style="padding: 5px 10px 0 10px;">
                                                        <asp:TextBox ID="txtBase_EmergencyLinkman" runat="server" />
                                                        <font color="red">*</font>
                                                    </td>
                                                    <td style="font-size: 10pt; padding: 5px 10px 0 10px; font-weight: bold; width: 12%;">紧急联系人电话:
                                                    </td>
                                                    <td align="left" style="padding: 5px 10px 0 10px;">
                                                        <asp:TextBox ID="txtBase_EmergencyPhone" runat="server" />
                                                        <font color="red">*</font>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 10pt; padding: 5px 10px 0 10px; font-weight: bold; width: 12%;">通讯地址:
                                                    </td>
                                                    <td colspan="3" align="left" style="padding: 5px 10px 0 10px;">
                                                        <asp:TextBox runat="server" ID="txtBase_Address1" Width="65%"></asp:TextBox>
                                                        <font color="red">*</font>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <hr style="border-top: 1px solid #e1e1e1; height: 0px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 10pt; padding: 5px 10px 0 10px; font-weight: bold; width: 12%;">学历:
                                                    </td>
                                                    <td align="left" style="padding: 5px 10px 0 10px;">
                                                        <asp:DropDownList ID="txtBase_Education" runat="server">
                                                            <asp:ListItem Text="高中/中专/中技及以下" Value="高中/中专/中技及以下"></asp:ListItem>
                                                            <asp:ListItem Text="大专及同等学历" Value="大专及同等学历"></asp:ListItem>
                                                            <asp:ListItem Text="本科/学士及等同学历" Value="本科/学士及等同学历"></asp:ListItem>
                                                            <asp:ListItem Text="硕士/研究生及等同学历" Value="硕士/研究生及等同学历"></asp:ListItem>
                                                            <asp:ListItem Text="博士及以上" Value="博士及以上"></asp:ListItem>
                                                            <asp:ListItem Text="其他" Value="其他"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <font color="red">*</font>
                                                    </td>
                                                    <td style="font-size: 10pt; padding: 5px 10px 0 10px; font-weight: bold; width: 12%;">专业:
                                                    </td>
                                                    <td align="left" style="padding: 5px 10px 0 10px;">
                                                        <asp:TextBox runat="server" ID="txtProfessional"></asp:TextBox>
                                                        <font color="red">*</font>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 10pt; padding: 5px 10px 0 10px; font-weight: bold; width: 12%;">毕业院校:
                                                    </td>
                                                    <td colspan="3" align="left" style="padding: 5px 10px 0 10px;">
                                                        <asp:TextBox runat="server" ID="txtSchool" Width="65%"></asp:TextBox>
                                                        <font color="red">*</font>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <hr style="border-top: 1px solid #e1e1e1; height: 0px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 10pt; padding: 5px 10px 0 10px; font-weight: bold; width: 12%;">工资卡银行:
                                                    </td>
                                                    <td align="left" style="padding: 5px 10px 0 10px;">
                                                        <asp:TextBox ID="txtSalaryBank" runat="server" /> <font color="red">*</font>
                                                    </td>
                                                    <td style="font-size: 10pt; padding: 5px 10px 0 10px; font-weight: bold; width: 12%;">工资卡号:
                                                    </td>
                                                    <td align="left" style="padding: 5px 10px 0 10px;">
                                                        <asp:TextBox ID="txtSalaryCardNo" runat="server" /> <font color="red">*</font>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <hr style="border-top: 1px solid #e1e1e1; height: 0px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 10pt; padding: 5px 10px 0 10px; font-weight: bold; width: 12%;">首次参加工作日期:
                                                    </td>
                                                    <td colspan="3" align="left" style="padding: 5px 10px 0 10px;">
                                                        <asp:TextBox ID="txtWorkBegin" runat="server" onclick="setDate(this);" />
                                                        <font color="red">*</font>
                                                         </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <hr style="border-top: 1px solid #e1e1e1; height: 0px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 10pt; padding: 5px 10px 0 10px; font-weight: bold; width: 12%;" colspan="4">工作简历:简述入职前的工作经历概要.格式:时间段、公司名称、职位、客户类型 客户名称<br />
                                                        <span style="color: gray;">(举例：2015-1-1到2015-12-31 XXX有限公司 客户主管 汽车类 长安马自达)</span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" align="left" style="padding: 5px 10px 0 10px;">
                                                        <asp:TextBox ID="txtBase_WorkExperience" TextMode="MultiLine" Height="100px" Width="100%" runat="server" />
                                                        <font color="red">*</font>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 10pt; padding: 5px 10px 0 10px; font-weight: bold; width: 12%;" colspan="4">工作特长或荣誉:
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" align="left" style="padding: 5px 10px 0 10px;">
                                                        <asp:TextBox ID="txtBase_WorkSpecialty" TextMode="MultiLine" Height="100px" Width="100%"
                                                            runat="server" /><font color="red">*</font>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <hr style="border-top: 1px solid #e1e1e1; height: 0px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 10pt; padding: 5px 10px 0 10px; font-weight: bold;">笔记本:
                                                    </td>
                                                    <td align="left" style="padding: 5px 10px 0 10px;" colspan="3">
                                                        <asp:RadioButtonList runat="server" ID="rdComputer" RepeatColumns="2" RepeatDirection="Horizontal">
                                                            <asp:ListItem Text="自带笔记本" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="使用公司电脑" Value="2"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <hr style="border-top: 1px solid #e1e1e1; height: 0px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px 10px 0 10px;" colspan="4">
                                                        <asp:Button ID="btnOK" runat="server" Text=" 确认Offer " OnClick="btnOK_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <hr style="border-top: 3px solid #ee770f; height: 0px" />
                </td>
            </tr>
            <tr>
                <td colspan="4" style="font-size: 10px; color: Gray" align="center">
                    <br />
                    <br />
                    中国北京市朝阳区双桥路12号 电子城数字新媒体创新产业园D1楼（请着正装）
                <br />
                    <br />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
