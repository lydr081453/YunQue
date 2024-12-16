<%@ Page Language="C#" AutoEventWireup="true" Inherits="project_SupporterPrint" Codebehind="SupporterPrint.aspx.cs" %>

<html>
<head runat="server">
    <title>֧�ַ���ӡԤ��</title>
    <style type="text/css" media="print">
        .noprint
        {
            display: none;
        }
    </style>
    <style type="text/css">
        body
        {
            margin: 0px;
        }
        img
        {
            border: none;
        }
        .nav
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
        }
        .nav table
        {
            border-top-width: 1px;
            border-left-width: 1px;
            border-top-style: solid;
            border-left-style: solid;
            border-top-color: #999999;
            border-left-color: #999999;
        }
        .nav td
        {
            border-right-width: 1px;
            border-bottom-width: 1px;
            border-right-style: solid;
            border-bottom-style: solid;
            border-right-color: #999999;
            border-bottom-color: #999999;
            padding: 0 0 0 5px;
            color: #333333;
            line-height: 150%;
        }
        .nav em
        {
            font-style: normal;
            font-size: 14px;
            color: #CC6633;
            font-weight: bold;
        }
        .topline
        {
            border-top-width: 2px;
            border-top-style: solid;
            border-top-color: #999999;
        }
    </style>
</head>
<body>
    <table width="750" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin: 40px 0 10px 0;">
                    <tr>
                        <td width="50%" style="font-weight:bolder;font-size:large;">
                            ��Ŀ������
                        </td>
                        <td width="50%" align="right" valign="top">
                            <img src="/images/xingyan.png" width="63" height="35" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center" style="padding: 10px 0 0 0;">
                            <img src="/images/print_img/title_03.gif" width="446" height="42" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top" class="nav" style="padding: 10px 0 10px 0;">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="25" colspan="2">
                            <strong>Project supporter/��Ŀ֧�ַ����칫�����ƣ�/���</strong>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblGroupName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" colspan="2">
                            <strong>Job number supported/��֧��֮��Ŀ��</strong>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblProjectCode" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" colspan="2">
                            <p>
                                <strong>Service Type/�������ͣ�Ӧ����֧��֮��Ŀһ�£�</strong></p>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblServiceType" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="100" colspan="2">
                            <strong>Service description/ҵ����������������⣩</strong>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblBizDesc" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" colspan="2">
                            <strong>Group project leader/��Ŀ����</strong>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblLeader" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" colspan="2">
                            <strong>Budget allocated/ҵ���ܶ�</strong>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblBudget" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" colspan="2">
                            <strong>������ֵ˰���</strong>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblBudgetNoVAT" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" colspan="2">
                            <strong>����˰</strong>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblTaxVAT" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" colspan="2">
                            <strong>Project completed percentage in advance/<br />
                                Ԥ����ɰٷֱ�</strong>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblPercent" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" colspan="2">
                            <strong>Supporter fee income/���������</strong>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblFee" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" colspan="2">
                            <strong>Billed tax/�ɿͻ�֧��֮˰��</strong>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblTax" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" height="25">
                            <strong>Requested by/������</strong>
                        </td>
                        <td width="25%">
                            &nbsp;
                        </td>
                        <td width="25%">
                            <strong>Date of request/��������</strong>
                        </td>
                        <td width="25%">
                            <asp:Label ID="lblAppDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="30" colspan="5">
                            <em>��Ŀ���Ա</em>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%" height="25" align="center">
                            <strong>��ʵ����</strong>
                        </td>
                        <td width="20%" align="center">
                            <strong>��Ա���</strong>
                        </td>
                        <td width="20%" align="center">
                            <strong>��Ա�˺�</strong>
                        </td>
                        <td width="20%" align="center">
                            <strong>����</strong>
                        </td>
                        <td width="20%" align="center">
                            <strong>�绰</strong>
                        </td>
                    </tr>
                    <asp:Literal runat="server" ID="ltPrjMem"></asp:Literal>
                </table>
                <br />
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="30" colspan="4">
                            <em>�ɱ���ϸ</em>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" height="25" align="center">
                            <strong>�ɱ�����</strong>
                        </td>
                        <td width="25%" align="center">
                            <strong>�ɱ����</strong>
                        </td>
                        <td width="50%" align="center" colspan="2">
                            <strong>��ע</strong>
                        </td>
                    </tr>
                    <asp:Literal runat="server" ID="ltContractDetail"></asp:Literal>
                    <tr>
                        <td colspan="4">
                            <em>������¼</em>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" width="100%" align="Left">
                            <asp:Label ID="lblLog" runat="server" Font-Size="XX-Small"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <p>
                    <br />
                    - The form shall be filled out by the project Supporter.<br />
                    &nbsp;&nbsp;��������Ŀ֧�ַ���д��<br />
                    <br />
                    - The form is taken as effective when it has no conflict with the corresponding
                    Owner
                    <br />
                    &nbsp;&nbsp;Version submitted by the project owner.<br />
                    &nbsp;&nbsp;��������������Ŀ�����뷽�ṩ���������������Ч��<br />
                    <br />
                    - The form is updated on January 10,2005 and is subject to any necessary change.<br />
                    &nbsp;&nbsp;���������2006��1��10�ո��£�����������Ҫ��ʱ�Ķ���</p>
            </td>
        </tr>
    </table>
    <table width="750" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr class="noprint">
            <td height="25" align="right">
                &nbsp;
            </td>
            <td width="1%" align="right">
                <a href="#" style="cursor: pointer">
                    <img src="/images/print_img/1_11.gif" width="50" height="20" hspace="1" vspace="5"
                        onclick="window.print();" /></a>
            </td>
            <td width="1%" align="right">
                <a href="#" style="cursor: pointer">
                    <img src="/images/print_img/1_13.gif" width="50" height="20" vspace="5" onclick="window.close();" /></a>
            </td>
        </tr>
    </table>
</body>
</html>
