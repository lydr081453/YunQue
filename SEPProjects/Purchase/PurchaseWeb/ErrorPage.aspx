<%@ Page Language="C#" AutoEventWireup="true" Inherits="ErrorPage" Codebehind="ErrorPage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript">
        function showW() {
            if (document.getElementById("trMsg").style.display == "none") {
                document.getElementById("trMsg").style.display = "block";
                if (document.all)
                    document.getElementById("s1").innerText = " - ";
                else
                    document.getElementById("s1").textContent = " - ";
            }
            else {
                document.getElementById("trMsg").style.display = "none";
                if (document.all)
                    document.getElementById("s1").innerText = " + ";
                else
                    document.getElementById("s1").textContent = " - ";
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="padding-left: 20px; font-size: 15px">
                <h2>
                    ��վ�޷���ʾ����ҳ</h2>
                <br />
                ����ܵ�ԭ����:
                <li style="font: 15px; color: #666666;">δ���ӵ� Internet��</li>
                <li>���ݿⷢ���쳣��</li>
                <li>��վ��������</li><br />
                <br />
                �����Գ���һ�²���
                <li><a href='/Purchase/Default.aspx'>���زɹ���ϵͳ��ҳ��</a></li>
            </td>
        </tr>
        <tr>
            <td style="height:30px"></td>
        </tr>
        <tr>
            <td style="padding-left: 20px; font-size: 15px" align="left">
                <table width="80%" border="0">
                    <tr>
                        <td><a onclick="showW();" style="color:Black; font-size:15px; cursor:pointer"><span id="s1"> + </span>��ϸ��Ϣ</a></td>
                    </tr>
                    <tr id="trMsg" style="display:none;">
                        <td><asp:Literal ID="litMsg" runat="server" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
