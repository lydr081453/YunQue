<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserBaseInfo.aspx.cs" Inherits="UserBaseInfo"
    MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="/public/js/jquery.js" type="text/javascript"></script>

    <script src="/public/js/jquery.history_remote.pack.js" type="text/javascript"></script>

    <script src="/public/js/jquery.tabs.pack.js" type="text/javascript"></script>

    <script src="/public/js/dialog.js" type="text/javascript"></script>

    <script src="../Employees/js/UserDepartment.js" type="text/javascript"></script>

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script type="text/javascript">
        $(function() {
            $('#container-1').tabs();
            show();
        });

        $(document).ready(function() {
            show();
        });

        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }

        function setCosts(pro, base, costs) {
            var pro = "ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder2_" + pro;
            var base = "ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder2_" + base;
            var costs = "ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder2_" + costs;
            var eif = Number(document.getElementById("" + pro + "").value);
            var sbase = Number(document.getElementById("" + base + "").value);
            if (eif != "NaN" && sbase != "NaN") {
                var val = sbase * (eif / 100);
                document.getElementById("" + costs + "").value = ForDight(val, 2);
            }
        }

        //四舍五入
        function ForDight(Dight, How) {
            var Dight = Math.round(Dight * Math.pow(10, How)) / Math.pow(10, How);
            return Dight;
        }

        var ContractCount =<%=ContractCount %>;
        function checked() {
            ContractCount = ContractCount + 1;
            document.getElementById("<%=hidReadCount.ClientID %>").value = ContractCount;
        }
        function Checking() {
            if (ContractCount >= 1) {
                return true;
            }
            else {
                return false;
            }
        }

        function PrintForm() {
            //window.open('JoinFormPrint.aspx');
            return true;
        }

        function checkIdcard(idcard) {
            var Errors = new Array(
            "验证通过!",
            "身份证号码位数不对!",
            "身份证号码出生日期超出范围或含有非法字符!",
            "身份证号码校验错误!",
            "身份证地区非法!");
            var area = { 11: "北京", 12: "天津", 13: "河北", 14: "山西", 15: "内蒙古", 21: "辽宁", 22: "吉林", 23: "黑龙江", 31: "上海", 32: "江苏", 33: "浙江", 34: "安徽", 35: "福建", 36: "江西", 37: "山东", 41: "河南", 42: "湖北", 43: "湖南", 44: "广东", 45: "广西", 46: "海南", 50: "重庆", 51: "四川", 52: "贵州", 53: "云南", 54: "西藏", 61: "陕西", 62: "甘肃", 63: "青海", 64: "宁夏", 65: "新疆", 71: "台湾", 81: "香港", 82: "澳门", 91: "国外" }
            var Y, JYM;
            var S, M;
            var idcard_array = new Array();
            idcard_array = idcard.split("");
            //地区检验
            if (area[parseInt(idcard.substr(0, 2))] == null) return Errors[4];
            //身份号码位数及格式检验
            switch (idcard.length) {
                case 15:
                    if ((parseInt(idcard.substr(6, 2)) + 1900) % 4 == 0 || ((parseInt(idcard.substr(6, 2)) + 1900) % 100 == 0 && (parseInt(idcard.substr(6, 2)) + 1900) % 4 == 0)) {
                        ereg = /^[1-9][0-9]{5}[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|[1-2][0-9]))[0-9]{3}$/; //测试出生日期的合法性
                    } else {
                        ereg = /^[1-9][0-9]{5}[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|1[0-9]|2[0-8]))[0-9]{3}$/; //测试出生日期的合法性
                    }
                    if (ereg.test(idcard)) return Errors[0];
                    else return Errors[2];
                    break;
                case 18:
                    //18位身份号码检测
                    //出生日期的合法性检查 
                    //闰年月日:((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|[1-2][0-9]))
                    //平年月日:((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|1[0-9]|2[0-8]))
                    if (parseInt(idcard.substr(6, 4)) % 4 == 0 || (parseInt(idcard.substr(6, 4)) % 100 == 0 && parseInt(idcard.substr(6, 4)) % 4 == 0)) {
                        ereg = /^[1-9][0-9]{5}19[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|[1-2][0-9]))[0-9]{3}[0-9Xx]$/; //闰年出生日期的合法性正则表达式
                    } else {
                        ereg = /^[1-9][0-9]{5}19[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|1[0-9]|2[0-8]))[0-9]{3}[0-9Xx]$/; //平年出生日期的合法性正则表达式
                    }
                    if (ereg.test(idcard)) {//测试出生日期的合法性
                        //计算校验位
                        S = (parseInt(idcard_array[0]) + parseInt(idcard_array[10])) * 7
                + (parseInt(idcard_array[1]) + parseInt(idcard_array[11])) * 9
                + (parseInt(idcard_array[2]) + parseInt(idcard_array[12])) * 10
                + (parseInt(idcard_array[3]) + parseInt(idcard_array[13])) * 5
                + (parseInt(idcard_array[4]) + parseInt(idcard_array[14])) * 8
                + (parseInt(idcard_array[5]) + parseInt(idcard_array[15])) * 4
                + (parseInt(idcard_array[6]) + parseInt(idcard_array[16])) * 2
                + parseInt(idcard_array[7]) * 1
                + parseInt(idcard_array[8]) * 6
                + parseInt(idcard_array[9]) * 3;
                        Y = S % 11;
                        M = "F";
                        JYM = "10X98765432";
                        M = JYM.substr(Y, 1); //判断校验位
                        if (M == idcard_array[17]) return true; //检测ID的校验位
                        else return Errors[3];
                    }
                    else return Errors[2];
                    break;
                default:
                    return Errors[1];
                    break;
            }
        }

        function checkCard(sender, args) {
            var msg = checkIdcard(document.getElementById("<%= txtBase_IdNo.ClientID%>").value);
            if (msg == true) {
                args.IsValid = true;
            }
            else {
                args.IsValid = false;
            }
        }
    </script>

    <asp:HiddenField ID="hidReadCount" runat="server" />
    <table width="100%" class="tableForm" style="border-color: #CED4E7;">
        <tr>
            <td class="oddrow-l" colspan="6" align="center">
                <div class="photocontainer">
                    <asp:Image ID="imgBase_Photo" runat="server" ImageUrl="../../public/CutImage/image/blank.jpg"
                        CssClass="imagePhoto" ToolTip="头像" />
                </div>
                <a href="UpLoadUserPhoto.aspx?userid=<%=Request["userid"]%>">点击上传照片</a>
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm" style="border-color: #CED4E7;">
        <tr>
            <td class="oddrow" style="width: 10%">
                员工编号:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:TextBox ID="txtUserCode" runat="server" Enabled="false" />
            </td>
            <td class="oddrow" style="width: 10%">
                入职日期:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:TextBox ID="txtJob_JoinDate" runat="server" onclick="setDate(this);" />
            </td>
            <td class="oddrow" style="width: 10%">
                公司邮箱:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:Label ID="txtBase_Email" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">
                中文姓名:
            </td>
            <td class="oddrow-l" style="width: 20%;">
                姓&nbsp;<asp:TextBox ID="txtBase_LastNameCn" runat="server" Width="50px" />&nbsp;名&nbsp;<asp:TextBox
                    ID="txtBase_FitstNameCn" runat="server" Width="50px" />
            </td>
            <td class="oddrow" style="width: 10%">
                英文姓名:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:TextBox ID="txtBase_FirstNameEn" runat="server" Width="50px" />.<asp:TextBox
                    ID="txtBase_LastNameEn" runat="server" Width="50px" /><br />
                (firstname.lastname)
            </td>
            <td class="oddrow" style="width: 10%">
                个人昵称:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:TextBox ID="txtCommonName" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">
                性别:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:RadioButton ID="radBase_Sex1" runat="server" Text="男" GroupName="Base_Sex" Checked="true" />
                <asp:RadioButton ID="radBase_Sex2" runat="server" Text="女" GroupName="Base_Sex" />
            </td>
            <td class="oddrow" style="width: 10%">
                出生日期:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:TextBox ID="txtBase_Birthday" runat="server" onclick="setDate(this);" />
            </td>
            <td class="oddrow" style="width: 10%">
                籍贯:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:TextBox ID="txtBase_PlaceOfBirth" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">
                身份证号码:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:TextBox ID="txtBase_IdNo" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtBase_IdNo"
                    Display="None" ErrorMessage="请填写身份证号"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="checkCard"
                    Display="None" ErrorMessage="请正确填写身份证号" />
            </td>
            <td class="oddrow-l" style="width: 10%">
                户口性质:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:DropDownList runat="server" ID="ddlHuji">
                    <asp:ListItem Selected="True" Text="请选择.." Value="0"></asp:ListItem>
                    <asp:ListItem Text="本市城镇" Value="本市城镇"></asp:ListItem>
                    <asp:ListItem Text="外埠城镇" Value="外埠城镇"></asp:ListItem>
                    <asp:ListItem Text="本市农业" Value="本市农业"></asp:ListItem>
                    <asp:ListItem Text="外埠农业" Value="外埠农业"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="oddrow" style="width: 10%">
                户口所在地:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:TextBox ID="txtBase_DomicilePlace" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">
                婚姻状况:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:DropDownList ID="txtBase_Marriage" runat="server">
                    <asp:ListItem Value="2" Text="未婚" />
                    <asp:ListItem Value="1" Text="已婚有子" />
                    <asp:ListItem Value="4" Text="已婚无子" />
                    <asp:ListItem Value="3" Text="离异" />
                </asp:DropDownList>
            </td>
            <td class="oddrow-l" style="width: 10%">
                配偶姓名:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:TextBox ID="txtMate" runat="server" />
            </td>
            <td class="oddrow" style="width: 10%">
                健康状况:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:TextBox ID="txtBase_Health" runat="server" />
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm" style="margin: 20px 0px 0px 0px; border-color: #CED4E7;">
        <tr>
            <td class="oddrow" style="width: 10%">
                毕业院校:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:TextBox ID="txtBase_FinishSchool" runat="server" />
            </td>
            <td class="oddrow" style="width: 10%">
                毕业时间:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:TextBox ID="txtBase_FinishSchoolDate" runat="server" onclick="setDate(this);" />
            </td>
            <td class="oddrow" style="width: 10%">
                专业:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:TextBox ID="txtBase_Speciality" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">
                最高学历:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:DropDownList ID="txtBase_Education" runat="server">
                    <asp:ListItem Text="高中/中专/中技及以下" Value="高中/中专/中技及以下"></asp:ListItem>
                    <asp:ListItem Text="大专及同等学历" Value="大专及同等学历"></asp:ListItem>
                    <asp:ListItem Text="本科/学士及等同学历" Value="本科/学士及等同学历"></asp:ListItem>
                    <asp:ListItem Text="硕士/研究生及等同学历" Value="硕士/研究生及等同学历"></asp:ListItem>
                    <asp:ListItem Text="博士及以上" Value="博士及以上"></asp:ListItem>
                    <asp:ListItem Text="其他" Value="其他"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="oddrow" style="width: 10%">
                个人特长:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:TextBox ID="txtBase_WorkSpecialty" runat="server" />
            </td>
            <td class="oddrow-l" style="width: 10%">
                &nbsp;
            </td>
            <td class="oddrow-l" style="width: 20%">
                &nbsp;
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm" style="margin: 20px 0px 0px 0px; border-color: #CED4E7;">
        <tr>
            <td class="oddrow" style="width: 10%">
                联系电话:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:TextBox ID="txtBase_HomePhone" runat="server" />
            </td>
            <td class="oddrow" style="width: 10%">
                手机:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:TextBox ID="txtBase_MobilePhone" runat="server" />
            </td>
            <td class="oddrow" style="width: 10%">
                个人邮箱:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:TextBox ID="txtBase_PrivateEmail" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                家庭地址:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtBase_Address1" runat="server" />
            </td>
            <td class="oddrow">
                邮政编码:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtBase_PostCode" runat="server" />
            </td>
            <td class="oddrow">
                &nbsp;
            </td>
            <td class="oddrow-l">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                本人现住址:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtAddressNow" runat="server" />
            </td>
            <td class="oddrow">
                邮政编码:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtPostCodeNow" runat="server" />
            </td>
            <td class="oddrow">
                &nbsp;
            </td>
            <td class="oddrow-l">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                紧急联系人:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtBase_EmergencyLinkman" runat="server" />
            </td>
            <td class="oddrow">
                紧急联系人电话:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtBase_EmergencyPhone" runat="server" />
            </td>
            <td class="oddrow">
                &nbsp;
            </td>
            <td class="oddrow-l">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                家庭成员:
            </td>
            <td class="oddrow-l" colspan="5">
                <asp:TextBox ID="txtFamilly" runat="server"  Height="80px" Width="85%"/>
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm" style="border-color: #CED4E7;">
        <tr>
            <td>
                <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    PageSize="20" OnRowDataBound="gvList_RowDataBound" OnPageIndexChanging="gvList_PageIndexChanging"
                    OnRowCommand="gvList_RowCommand" DataKeyNames="UserID,DepartmentPositionID,DepartmentID"
                    Width="100%">
                    <Columns>
                        <asp:BoundField DataField="CompanyName" HeaderText="公司" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="DepartmentName" HeaderText="部门" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="GroupName" HeaderText="团队" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="DepartmentPositionName" HeaderText="职务" ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm" style="border-color: #CED4E7;">
        <tr>
            <td class="oddrow" style="width: 10%">
                简历文档上传
            </td>
            <td class="oddrow-l">
                <asp:FileUpload ID="fileCV" runat="server" Width="50%" />&nbsp;&nbsp;<asp:Label ID="labResume"
                    runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" colspan="6">
                工作履历:
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="6">
                <asp:TextBox ID="txtBase_WorkExperience" TextMode="MultiLine" Height="100px" Width="100%"
                    runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" colspan="6">
                在六个月内是否有严重的疾病或意外的事故，无/有，请详细说明:
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="6">
                <asp:TextBox ID="txtBase_DiseaseInSixMonths" TextMode="MultiLine" Height="100px"
                    Width="100%" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" colspan="6">
                备注:
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="6">
                <asp:TextBox ID="txtJob_Memo" TextMode="MultiLine" Height="100px" Width="100%" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" colspan="6">
                员工手册:(请点击下载阅读《星言云汇员工手册》)<a id="aDownLoad" href="emp.doc" target="_blank" style="color: Blue;
                    margin: 0 0 0 10">星言云汇员工手册</a>
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="6">
                <asp:RadioButton ID="radAccept" runat="server" GroupName="Accept" />我已阅读并接受星言云汇员工手册中的全部内容<br />
                <asp:RadioButton ID="radNoAccept" runat="server" GroupName="Accept" Checked="true" />我已阅读但不接受星言云汇员工手册中的全部内容
            </td>
        </tr>
    </table>
    <table width="90%">
        <tr>
            <td>
                <asp:Button ID="btnSave" runat="server" CssClass="widebuttons" OnClick="btnSave_Click"  OnClientClick="return PrintForm();" 
                    Text=" 保 存 " />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnPrint" runat="server" CssClass="widebuttons" OnClick="btnPrint_Click"
                    Text=" 打 印 " OnClientClick="return PrintForm();" />
            </td>
        </tr>
    </table>
</asp:Content>
