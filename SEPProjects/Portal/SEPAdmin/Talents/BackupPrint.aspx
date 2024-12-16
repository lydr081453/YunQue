<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BackupPrint.aspx.cs"
    Inherits="SEPAdmin.Talents.BackupPrint" %>

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
                    <span style="font-size: 15pt; font-weight: bold;">备选人才库简历</span>
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
                <td align="left" width="150px">
                    <asp:Label runat="server" ID="lblUserName"></asp:Label>
                </td>
                <td align="center" width="60px">
                    <strong>联系电话</strong>
                </td>
                <td align="left" width="160px">
                    <asp:Label runat="server" ID="lblMobile"></asp:Label>
                </td>
                <td align="center" width="60px">
                    <strong>出生年份</strong>
                </td>
                <td align="left" width="160px">
                    <asp:Label runat="server" ID="lblBirthday"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" width="60px">
                    <strong>学历</strong>
                </td>
                <td align="left" width="150px">
                    <asp:Label runat="server" ID="lblEducation"></asp:Label>
                </td>
                 <td align="center" width="60px">
                    <strong>语言</strong>
                </td>
                <td align="left" width="160px">
                    <asp:Label runat="server" ID="lblLanguage"></asp:Label>
                </td>
                 <td align="center" width="60px">
                    <strong>工作起始年度</strong>
                </td>
                <td align="left" width="160px">
                    <asp:Label runat="server" ID="lblWorkBegin"></asp:Label>
                </td>
              
            </tr>
            <tr>
                  <td align="center" width="60px">
                    <strong>工作所在地</strong>
                </td>
                 <td align="left" width="150px">
                    <asp:Label runat="server" ID="lblLocation"></asp:Label>
                </td>
               <td align="center" width="60px">
                    <strong>应聘职位</strong>
                </td>
                 <td align="left" width="160px" colspan="3">
                    <asp:Label runat="server" ID="lblPosition"></asp:Label>
                </td>
            </tr>
             <tr>
                <td height="60" align="center" colspan="2">
                    <strong>职能</strong>
                </td>
                <td height="60" width="80px" align="left" colspan="6">
                    <asp:Label runat="server" ID="lblProfessional"></asp:Label>
                </td>
            </tr>
             <tr>
                <td height="60" align="center" colspan="2">
                    <strong>服务行业</strong>
                </td>
                <td height="60" width="80px" align="left" colspan="6">
                    <asp:Label runat="server" ID="lblService"></asp:Label>
                </td>
            </tr>
            <tr>
                <td height="60" align="center" colspan="2">
                    <strong>工作经历</strong>
                </td>
                <td height="60" width="80px" align="left" colspan="6">
                    <asp:Label runat="server" ID="lblResume"></asp:Label>
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
                                <asp:Label runat="server" ID="lblHRAudit"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td height="150" align="left" colspan="4" valign="top">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <strong>业务团队意见：</strong>
                            </td>
                        </tr>
                        <tr height="100">
                            <td>
                                <asp:Label runat="server" ID="lblGroupAudit"></asp:Label>
                            </td>
                        </tr>
                    </table>
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
