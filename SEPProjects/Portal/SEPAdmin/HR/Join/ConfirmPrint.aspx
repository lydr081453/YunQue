<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfirmPrint.aspx.cs" Inherits="SEPAdmin.HR.Join.ConfirmPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../public/CutImage/css/main.css" type="text/css" rel="Stylesheet" />

    <script type="text/javascript" src="../../public/CutImage/js/jquery1.2.6.pack.js"></script>

    <script type="text/javascript" src="../../public/CutImage/js/ui.core.packed.js"></script>

    <script type="text/javascript" src="../../public/CutImage/js/ui.draggable.packed.js"></script>

    <script type="text/javascript" src="../../public/CutImage/js/CutPic.js"></script>

</head>
<body>
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
            <td align="center">
                &nbsp;
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
                        <td style="font-size: 14pt; font-weight: bold;">
                            尊敬的<asp:Label ID="labUserName" runat="server" />您好
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 12pt;">
                            &nbsp;&nbsp;&nbsp;&nbsp;感谢您应聘本公司，我们非常荣幸地通知您，您经初审合格，依本公司任用规定给予录用，竭诚欢迎您加入本公司行列。有关录用报到事项如下，敬请参照办理。
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="font-size: 12pt; background-color: #f8f8f8;">
                <table border="0" cellpadding="0" cellspacing="0" align="center" width="95%" style="padding: 10px 0 10px 0;">
                    <tr>
                        <td style="font-size: 12pt; font-weight: bold; color: #f2720f">
                            一、报到时间：<asp:Label ID="labJoinDate" runat="server" />早9:30（工作时间 9:30---18:30）
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 12pt; padding: 0 0 0 32px;">
                            报到地点：<asp:Label ID="labAddress" runat="server" />
                        </td>
                    </tr>
                </table>
                <hr style="border-top: 1px dashed #ee770f; height: 1px" />
                <table border="0" cellpadding="0" cellspacing="0" align="center" width="95%" style="padding: 10px 0 10px 0;">
                    <tr>
                        <td style="font-size: 12pt; font-weight: bold; color: #f2720f">
                            二、携带正本资料：
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 12pt; padding: 0 0 0 32px;">
                            （一）居民身份证原件及复印件<font color="red">*</font>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 12pt; padding: 0 0 0 32px;">
                            （二）学生证原件<font color="red">*</font>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 12pt; padding: 0 0 0 32px;">
                            （三）学校工作介绍信（或推荐信）<font color="red">*</font>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 12pt; padding: 0 0 0 32px;">
                            （四）担保书（担保人：父亲或母亲；担保人工作单位；详细家庭住址；座机及手机电话）
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 12pt; padding: 0 0 0 32px;">
                            （五）一寸彩色相片一张及正面电子照片一张
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 12pt; padding: 0 0 0 32px;">
                            （六）本人银行卡（工资卡）北京：招行卡原件及复印件<asp:Label ID="lblCard" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <hr style="border-top: 1px dashed #ee770f; height: 1px" />
                <table border="0" cellpadding="0" cellspacing="0" align="center" width="95%" style="padding: 10px 0 10px 0;">
                    <tr>
                        <td style="font-size: 12pt; font-weight: bold; color: #f2720f">
                            三、薪资情况请参见附件中需要您确认的offer letter。
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 12pt; padding: 0 0 0 32px;">
                            （填写信息：1/ 身份证号码 2/ 应聘方签字 3/ 报到日期 ）
                        </td>
                    </tr>
                </table>
                <hr style="border-top: 1px dashed #ee770f; height: 1px" />
                <table border="0" cellpadding="0" cellspacing="0" align="center" width="95%" style="padding: 10px 0 10px 0;">
                    <tr>
                        <td style="font-size: 12pt; font-weight: bold; color: #f2720f">
                            四、我们双方都认同，出现下列情形之一将不予聘用：
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 12pt; padding: 0 0 0 32px;">
                            （一）有刑事犯罪行为者
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 12pt; padding: 0 0 0 32px;">
                            （二）身体有严重疾病者
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 12pt; padding: 0 0 0 32px;">
                            （三）年龄不满16周岁者
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 12pt; padding: 0 0 0 32px;">
                            （四）曾被本公司辞退或未经批准擅自离职者
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 12pt; padding: 0 0 0 32px;">
                            （五）与原单位劳动合同未解除者
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 12pt; padding: 0 0 0 32px;">
                            （六）或有其他不适合情况者（如提供不实履历等不诚信行为者）
                        </td>
                    </tr>
                </table>
                <hr style="border-top: 1px dashed #ee770f; height: 1px" />
                <table border="0" cellpadding="0" cellspacing="0" align="center" width="95%" style="padding: 10px 0 10px 0;">
                    <tr>
                        <td style="font-size: 12pt; font-weight: bold; color: #f2720f">
                            五、前列事项若有疑问或困难，请与本公司人力资源部联系。
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 12pt; padding: 0 0 0 32px;">
                            <asp:Label ID="lblPhone" runat="server"></asp:Label>
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
                北京市朝阳区双桥路12号 电子城数字新媒体创新产业园D1楼（请着正装）
                <br />
                <br />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>