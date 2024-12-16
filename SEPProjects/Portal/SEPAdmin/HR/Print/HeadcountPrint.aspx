<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HeadcountPrint.aspx.cs"
    Inherits="SEPAdmin.HR.Print.HeadcountPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .title
        {
            font-size: 12pt;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="DefaultOfferLetter" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="650" align="center" style="line-height: 200%;">
            <tr>
                <td colspan="8" align="right">
                    <br />
                    <img id="imgShunya" src="~/Images/xingyan.png" runat="server" alt="xingyan" /><br />
                </td>
            </tr>
            <tr>
                <td colspan="8">
                    <hr />
                </td>
            </tr>
            <tr>
                <td colspan="8" align="center">
                    <br />
                    <span style="font-size: 15pt; font-weight: bold;">应聘面谈记录表</span>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <table width="650" border="1" style=" border-color:Gray;" cellpadding="0" cellspacing="1" align="center" style="line-height: 110%;">
            <tr >
                <td height="28" width="60px" align="center" >
                    <strong>应 聘 者</strong>
                </td>
                <td align="left" width="100px">
                    <asp:Label runat="server" ID="lblUserName"></asp:Label>
                </td>
                <td align="center" width="60px">
                    <strong>应聘职位</strong>
                </td>
                <td align="left" width="150px">
                    <asp:Label runat="server" ID="lblPosition"></asp:Label>
                </td>
                <td align="center" width="60px">
                    <strong>性 别</strong>
                </td>
                <td align="left" width="60px">
                    <asp:Label runat="server" ID="lblGender"></asp:Label>
                </td>
                <td align="center" width="60px">
                    <strong>出生年月</strong>
                </td>
                <td align="left" width="100px">
                    <asp:Label runat="server" ID="lblBirthday"></asp:Label>
                </td>
            </tr>
            <tr>
                <td height="28" align="center">
                    <strong>个人户口所在地</strong>
                </td>
                <td align="left">
                    <asp:Label runat="server" ID="lblLocation"></asp:Label>
                </td>
                <td height="28" align="center">
                    <strong>目前住址</strong>
                </td>
                <td align="left" colspan="5">
                    <asp:Label runat="server" ID="lblAddress"></asp:Label>
                </td>
            </tr>
            <tr>
                <td height="28" align="center">
                    <strong>个人状况</strong>（婚否）
                </td>
                <td align="left">
                    <asp:Label runat="server" ID="lblMarry"></asp:Label>
                </td>
                <td height="28" align="center">
                    <strong>教育背景</strong>（学习时间段、院校、专业、学历）
                </td>
                <td align="left" colspan="5">
                    <asp:Label runat="server" ID="lblEducation"></asp:Label>
                </td>
            </tr>
            <tr>
                <td height="28" align="center" colspan="2">
                    <strong>仪容/礼貌/精神态度</strong>
                </td>
                <td height="28" width="80px" align="left" colspan="6">
                    <asp:Label runat="server" ID="lblAppearance"></asp:Label>
                </td>
            </tr>
            <tr>
                <td height="28" align="center" colspan="2">
                    <strong>领悟、反应及综合素质</strong>
                </td>
                <td height="28" width="80px" align="left" colspan="6">
                     <asp:Label runat="server" ID="lblQuality"></asp:Label>
                </td>
            </tr>
            <tr>
                <td height="28" align="center" colspan="2">
                    <strong>对所应聘职位的了解</strong>
                </td>
                <td height="28" width="80px" align="left" colspan="6">
                    <asp:Label runat="server" ID="lblKnow"></asp:Label>
                </td>
            </tr>
            <tr>
                <td height="28" align="center" colspan="2">
                    <strong>所具工作经历与本公司的配合程度</strong>
                </td>
                <td height="28" width="80px" align="left" colspan="6">
                    <asp:Label runat="server" ID="lblEqual"></asp:Label>
                </td>
            </tr>
            <tr>
                <td height="28" align="center" colspan="2">
                    <strong>对本公司的了解和前来工作的动机</strong>
                </td>
                <td height="28" width="80px" align="left" colspan="6">
                    <asp:Label runat="server" ID="lblReason"></asp:Label>
                </td>
            </tr>
            <tr>
                <td height="28" align="center" colspan="2">
                    <strong>综合测试结果</strong>
                </td>
                <td height="28" align="left" colspan="3">
                    <strong>4D性格测试：<asp:Label runat="server" ID="lbl4D"></asp:Label></strong>
                </td>
                <td height="28" align="left" colspan="3">
                    <strong>EQ测试：<asp:Label runat="server" ID="lblEQ"></asp:Label></strong>
                </td>
            </tr>
            <tr>
                <td height="150" align="left" colspan="4" valign="top">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <strong>HR意见：</strong>
                            </td>
                        </tr>
                       <tr height="100">
                            <td>
                                <asp:Label runat="server" ID="lblHRAudit1"></asp:Label>
                            </td>
                        </tr>
                         <tr valign="bottom">
                            <td>
                               <strong> 初试面谈人：</strong><asp:Label runat="server" ID="lblHR1"></asp:Label>
                               <span  style=" padding-left:80px;"><strong>日期：</strong>
                                <asp:Label runat="server" ID="lblHRDate1"></asp:Label></span>
                            </td>
                        </tr>
                    </table>
                </td>
                <td height="150" align="left" colspan="4" valign="top">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <strong>业务团队面谈意见：</strong>
                            </td>
                        </tr>
                        <tr height="100">
                            <td>
                                <asp:Label runat="server" ID="lblGroupAudit"></asp:Label>
                            </td>
                        </tr>
                        <tr valign="bottom">
                            <td>
                               <strong> 面谈人：</strong><asp:Label runat="server" ID="lblGroup"></asp:Label>
                               <span  style=" padding-left:180px;"><strong>日期：</strong>
                                <asp:Label runat="server" ID="lblGroupDate"></asp:Label></span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td height="28" align="center">
                    <strong>业务<br />
                        部门<br />
                        确认</strong>
                </td>
                <td height="150" align="left" colspan="7" style="line-height: 300%;">
                    <table border="0" cellpadding="0" cellspacing="0" style="line-height: 300%;">
                        <tr>
                            <td width="150px">
                             <strong>   试用期职位：</strong>
                            </td>
                            <td width="150px">
                                <asp:Label runat="server" ID="lblPositionTest"></asp:Label>
                            </td>
                            <td width="150px">
                               <strong> 期望到职日期：</strong>
                            </td>
                            <td width="100px">
                                <asp:Label runat="server" ID="lblJoinDate"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                               <strong> 基本工资：</strong>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblSalary"></asp:Label>
                            </td>
                            <td>
                              <strong> 绩效工资：</strong>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblSalary2"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                               <strong> 日常工作向谁报告：</strong>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblDirector"></asp:Label>
                            </td>
                            <td>
                                <strong>  考勤审批人：</strong>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblAuditor"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                               <strong> 批准人：</strong>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblApprove"></asp:Label>
                            </td>
                            <td>
                                <strong>  日 期：</strong>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblApproveDate"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td height="28" align="center">
                    <strong>结论</strong>
                </td>
                <td colspan="7">
                   <span  style=" padding-left:20px;"> □ 列入考虑 </span> <span  style=" padding-left:80px;"> □ 人才库</span><span  style=" padding-left:80px;">
                    □ 不予考虑</span><span  style=" padding-left:80px;">□拟予试用</span>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <table border="1" cellpadding="0" cellspacing="0" align="center">
        <tr>
            <td width="50" id="Td1" runat="server" align="center" valign="bottom" background="../../Images/btnbgimg.gif"
                class="white_font">
                <a style="cursor: pointer" onclick="javascript:window.print();">打印</a>
            </td>
            <td width="50" id="Td2" runat="server" align="center" valign="bottom" background="../../Images/btnbgimg.gif"
                class="white_font">
                <a style="cursor: pointer" onclick="javascript:window.close();">关闭</a>
            </td>
        </tr>
    </table>
    <br />
    </form>
</body>
</html>
