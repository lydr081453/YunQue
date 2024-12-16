<%@ Page Language="C#" AutoEventWireup="true" Inherits="project_ProjectPrint" Codebehind="ProjectPrint.aspx.cs" %>

<html>
<head runat="server">
    <title></title>
    <style type="text/css" media="print">
        .noprint
        {
            display: none;
        }
    </style>
    <style  type="text/css">
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
                        <td width="50%" align="right">
                            <asp:Image runat="server" ID="imgLogo" />
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" align="left" valign="bottom">
                            <asp:Label ID="lblSerialCode" Font-Size="Smaller" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top" class="nav" style="padding: 10px 0 10px 0;">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="15%" height="25">
                            <strong>ȷ����Ŀ��</strong>
                        </td>
                        <td width="35%">
                            <asp:Label runat="server" ID="labPrjCode"></asp:Label>
                        </td>
                        <td height="25">
                            <strong>���BD��Ŀ��</strong>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="labBDPrjCode"></asp:Label>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <strong>��ͬ״̬</strong>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="labConStatus"></asp:Label>
                        </td>
                         <td>
                            <strong>��Ŀ����</strong>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="labPrjType"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                         <td height="25">
                            <strong>��Ŀ���</strong>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="labPrjGroup"></asp:Label>
                        </td>
                        <td>
                            <strong>ҵ������</strong>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="labBizType"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="15%">
                            <strong>��Ŀ����</strong>
                        </td>
                        <td colspan="3">
                            <asp:Label runat="server" ID="labPrjName"></asp:Label>
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
                        <td height="25" colspan="4">
                            <em>��ͬ��Ϣ</em>
                        </td>
                    </tr>
                     <tr>
                        <td height="25">
                            <strong>��ͬ�ܽ��</strong>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="labTotalAmount"></asp:Label>
                        </td>
                          <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        
                    </tr>
                    <tr>
                        <td width="15%" height="25">
                            <strong>ҵ��ʼ����</strong>
                        </td>
                        <td width="35%">
                            <asp:Label runat="server" ID="labBizStartDate"></asp:Label>
                        </td>
                        <td height="25">
                            <strong>Ԥ�ƽ�������</strong>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="labBizFinishDate"></asp:Label>
                        </td>
                        
                    </tr>
                    <tr>
                        <td width="15%">
                            <strong>��ͬ����ѽ��</strong>
                        </td>
                        <td width="35%">
                            <asp:Label runat="server" ID="labServiceFee"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                   
                    <tr>
                    <td height="25">
                            <strong>������ֵ˰���</strong>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblTotalNoVAT"></asp:Label>
                        </td>
                         <td>
                            <strong>����˰(�����뷽)</strong>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblTaxFee"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                         <td height="25">
                            <strong>֧�ַ��ϼ�</strong>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblSupporterTot"></asp:Label>
                        </td>
                        <td>
                            <strong>����˰(֧�ַ�)</strong>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblSupporterTax"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>�ɱ��ϼ�</strong>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblCostTot"></asp:Label>
                        </td>
                        <td height="25">
                            <strong>��ͬë���ʣ�%��</strong>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblProfileRate"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25">
                            <strong>Ԥ�Ƹ�����ɰٷֱ�</strong>
                        </td>
                        <td colspan="3">
                            <asp:Label runat="server" ID="lblPercent"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="30" colspan="4">
                            <em>��ͬ�ɱ���ϸ</em>
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
                </table>
                <br />
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="30" colspan="6">
                            <em>֧�ַ���Ϣ</em>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" height="25" align="center">
                            <strong>֧�ַ����</strong>
                        </td>
                        <td width="10%" align="center">
                            <strong>������</strong>
                        </td>
                        <td width="15%" align="center">
                            <strong>ȷ��������</strong>
                        </td>
                         <td width="15%" align="center">
                            <strong>������ֵ˰���</strong>
                        </td>
                         <td width="15%" align="center">
                            <strong>����˰</strong>
                        </td>
                        <td width="20%" align="center">
                            <strong>��������</strong>
                        </td>
                    </tr>
                    <asp:Literal runat="server" ID="ltSupporters" />
                </table>
                <br />
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="30" colspan="4">
                            <em>�ͻ���Ϣ[<asp:Label runat="server" ID="labCustShortEn" />]</em>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%" height="25">
                            <strong>�ͻ����ƣ����ģ�</strong>
                        </td>
                        <td colspan="3">
                            <asp:Label runat="server" ID="labCustomerCName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%" height="25">
                            <strong>�ͻ����ƣ�Ӣ�ģ�</strong>
                        </td>
                        <td colspan="3">
                            <asp:Label runat="server" ID="labCustomerEName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%" height="25">
                            <strong>��Ʊ̧ͷ�����ģ�</strong>
                        </td>
                        <td colspan="3">
                            <asp:Label runat="server" ID="labInvoiceTitle"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%" height="25">
                            <strong>�ͻ���ϵ������/ְ��</strong>
                        </td>
                        <td width="30%">
                            <asp:Label runat="server" ID="labLinkerName"></asp:Label>
                        </td>
                        <td width="20%">
                            <strong>�ͻ���ϵ�˵绰/����</strong>
                        </td>
                        <td width="30%">
                            <asp:Label runat="server" ID="labLinkerPhone"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25">
                            <strong>�ͻ���ϵ�˵����ʼ�</strong>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="labLinkerEmail"></asp:Label>
                        </td>
                        <td>
                            <strong>��˾��ַ���¿ͻ���</strong>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="labCustWebSite"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25">
                            <strong>�ͻ���ַ���ʱࣩ</strong>
                        </td>
                        <td colspan="3">
                            <asp:Label runat="server" ID="labCustAddress"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25">
                            <strong>���ڵ������¿ͻ���</strong>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="labCustArea"></asp:Label>
                        </td>
                        <td>
                            <strong>������ҵ���¿ͻ���</strong>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="labCustIndustry"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="30" colspan="4">
                            <em>����֪ͨ</em>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%" height="25" align="center">
                            <strong>����֪ͨʱ��</strong>
                        </td>
                        <td width="30%" align="center">
                            <strong>����֪ͨ����</strong>
                        </td>
                        <td width="20%" align="center">
                            <strong>����֪ͨ���</strong>
                        </td>
                        <td width="30%" align="center">
                            <strong>��ע</strong>
                        </td>
                    </tr>
                    <asp:Literal runat="server" ID="ltPayment"></asp:Literal>
                    <tr>
                        <td height="25">
                            <strong>Ԥ�Ƹ�������</strong>
                        </td>
                        <td colspan="3">
                            <asp:Label runat="server" ID="lblCycle"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>�Ƿ����������Ʊ</strong>
                        </td>
                        <td colspan="3">
                            <asp:Label runat="server" ID="lblInvoice"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="30" colspan="6">
                            <em>��Ʊ����</em>
                        </td>
                    </tr>
                    <tr>
                        <td width="6%" align="center"><strong>����</strong></td>
                        <td align="center"><strong>ý�帶������</strong></td>
                        <td width="12%" align="center" height="25"><strong>��Ʊ���</strong></td>
                        <td width="10%" align="center"><strong>����ʱ��</strong></td>
                        <td align="center"><strong>����</strong></td>
                        <td width="8%" align="center"><strong>������</strong></td>
                        </tr>
                    <asp:Repeater runat="server" ID="repContract" >
                        <ItemTemplate>
                            <tr>
                               <td height="25" align="center">
                                            <%# Eval("FlowTo") == null ? "�ͻ�" : ESP.Finance.Utility.Common.ApplyForInvioceFlowTo_Names[ (int)Eval("FlowTo")] %>
                               </td>
                               <td height="25" align="center">
                                            <%# Eval("SupplierId") == null ? "" : Eval("MediaName") %>
                                </td>
                                <td height="25" align="center"><%# decimal.Parse(Eval("InviocePrice").ToString()).ToString("#,##0.00")%></td>
                                <td align="center"><%# DateTime.Parse(Eval("CreateDate").ToString()).ToString("yyyy-MM-dd")%></td>
                                <td><%# Eval("Remark") %></td>
                                <td align="center"><%# Eval("CreatorUserName") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    </table>
                <br />
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="30" colspan="6">
                            <em>������Ϣ</em>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%" height="25">
                            <strong>������ ����Ŀ���ܣ�</strong>
                        </td>
                        <td width="20%">
                            &nbsp;
                        </td>
                        <td width="15%">
                            <strong>��������</strong>
                        </td>
                        <td width="15%">
                            &nbsp;
                        </td>
                        <td width="15%">
                            <strong>��Ŀ�ܼ�</strong>
                        </td>
                        <td width="15">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td height="25">
                            <strong>�Ŷ��ܾ���ǩ��</strong>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <strong>�����ܼ�ǩ��</strong>
                        </td>
                        <td colspan="3">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                    <td colspan="6">
                   <em> ������¼</em>
                    </td>
                    </tr>
                    <tr>
                    <td colspan="6" width="100%" align="Left">
                    <asp:Label ID="lblLog" runat="server" Font-Size="XX-Small"></asp:Label>
                    </td>
                    </tr>
                </table>
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
