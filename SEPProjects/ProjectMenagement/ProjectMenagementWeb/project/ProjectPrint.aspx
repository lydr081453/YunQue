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
                            项目号申请
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
                            <strong>确认项目号</strong>
                        </td>
                        <td width="35%">
                            <asp:Label runat="server" ID="labPrjCode"></asp:Label>
                        </td>
                        <td height="25">
                            <strong>相关BD项目号</strong>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="labBDPrjCode"></asp:Label>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <strong>合同状态</strong>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="labConStatus"></asp:Label>
                        </td>
                         <td>
                            <strong>项目类型</strong>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="labPrjType"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                         <td height="25">
                            <strong>项目组别</strong>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="labPrjGroup"></asp:Label>
                        </td>
                        <td>
                            <strong>业务类型</strong>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="labBizType"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="15%">
                            <strong>项目名称</strong>
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
                            <em>项目组成员</em>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%" height="25" align="center">
                            <strong>真实姓名</strong>
                        </td>
                        <td width="20%" align="center">
                            <strong>成员编号</strong>
                        </td>
                        <td width="20%" align="center">
                            <strong>成员账号</strong>
                        </td>
                        <td width="20%" align="center">
                            <strong>邮箱</strong>
                        </td>
                        <td width="20%" align="center">
                            <strong>电话</strong>
                        </td>
                    </tr>
                    <asp:Literal runat="server" ID="ltPrjMem"></asp:Literal>
                </table>
                <br />
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="25" colspan="4">
                            <em>合同信息</em>
                        </td>
                    </tr>
                     <tr>
                        <td height="25">
                            <strong>合同总金额</strong>
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
                            <strong>业务开始日期</strong>
                        </td>
                        <td width="35%">
                            <asp:Label runat="server" ID="labBizStartDate"></asp:Label>
                        </td>
                        <td height="25">
                            <strong>预计结束日期</strong>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="labBizFinishDate"></asp:Label>
                        </td>
                        
                    </tr>
                    <tr>
                        <td width="15%">
                            <strong>合同服务费金额</strong>
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
                            <strong>不含增值税金额</strong>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblTotalNoVAT"></asp:Label>
                        </td>
                         <td>
                            <strong>附加税(主申请方)</strong>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblTaxFee"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                         <td height="25">
                            <strong>支持方合计</strong>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblSupporterTot"></asp:Label>
                        </td>
                        <td>
                            <strong>附加税(支持方)</strong>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblSupporterTax"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>成本合计</strong>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblCostTot"></asp:Label>
                        </td>
                        <td height="25">
                            <strong>合同毛利率（%）</strong>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblProfileRate"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25">
                            <strong>预计各月完成百分比</strong>
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
                            <em>合同成本明细</em>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" height="25" align="center">
                            <strong>成本描述</strong>
                        </td>
                        <td width="25%" align="center">
                            <strong>成本金额</strong>
                        </td>
                        <td width="50%" align="center" colspan="2">
                            <strong>备注</strong>
                        </td>
                    </tr>
                    <asp:Literal runat="server" ID="ltContractDetail"></asp:Literal>
                </table>
                <br />
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="30" colspan="6">
                            <em>支持方信息</em>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" height="25" align="center">
                            <strong>支持方组别</strong>
                        </td>
                        <td width="10%" align="center">
                            <strong>负责人</strong>
                        </td>
                        <td width="15%" align="center">
                            <strong>确认收入金额</strong>
                        </td>
                         <td width="15%" align="center">
                            <strong>不含增值税金额</strong>
                        </td>
                         <td width="15%" align="center">
                            <strong>附加税</strong>
                        </td>
                        <td width="20%" align="center">
                            <strong>费用类型</strong>
                        </td>
                    </tr>
                    <asp:Literal runat="server" ID="ltSupporters" />
                </table>
                <br />
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="30" colspan="4">
                            <em>客户信息[<asp:Label runat="server" ID="labCustShortEn" />]</em>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%" height="25">
                            <strong>客户名称（中文）</strong>
                        </td>
                        <td colspan="3">
                            <asp:Label runat="server" ID="labCustomerCName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%" height="25">
                            <strong>客户名称（英文）</strong>
                        </td>
                        <td colspan="3">
                            <asp:Label runat="server" ID="labCustomerEName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%" height="25">
                            <strong>发票抬头（中文）</strong>
                        </td>
                        <td colspan="3">
                            <asp:Label runat="server" ID="labInvoiceTitle"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%" height="25">
                            <strong>客户联系人姓名/职务</strong>
                        </td>
                        <td width="30%">
                            <asp:Label runat="server" ID="labLinkerName"></asp:Label>
                        </td>
                        <td width="20%">
                            <strong>客户联系人电话/传真</strong>
                        </td>
                        <td width="30%">
                            <asp:Label runat="server" ID="labLinkerPhone"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25">
                            <strong>客户联系人电子邮件</strong>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="labLinkerEmail"></asp:Label>
                        </td>
                        <td>
                            <strong>公司网址（新客户）</strong>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="labCustWebSite"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25">
                            <strong>客户地址（邮编）</strong>
                        </td>
                        <td colspan="3">
                            <asp:Label runat="server" ID="labCustAddress"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25">
                            <strong>所在地区（新客户）</strong>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="labCustArea"></asp:Label>
                        </td>
                        <td>
                            <strong>所在行业（新客户）</strong>
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
                            <em>付款通知</em>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%" height="25" align="center">
                            <strong>付款通知时间</strong>
                        </td>
                        <td width="30%" align="center">
                            <strong>付款通知内容</strong>
                        </td>
                        <td width="20%" align="center">
                            <strong>付款通知金额</strong>
                        </td>
                        <td width="30%" align="center">
                            <strong>备注</strong>
                        </td>
                    </tr>
                    <asp:Literal runat="server" ID="ltPayment"></asp:Literal>
                    <tr>
                        <td height="25">
                            <strong>预计付款周期</strong>
                        </td>
                        <td colspan="3">
                            <asp:Label runat="server" ID="lblCycle"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>是否需第三方发票</strong>
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
                            <em>发票申请</em>
                        </td>
                    </tr>
                    <tr>
                        <td width="6%" align="center"><strong>流向</strong></td>
                        <td align="center"><strong>媒体付款主体</strong></td>
                        <td width="12%" align="center" height="25"><strong>发票金额</strong></td>
                        <td width="10%" align="center"><strong>申请时间</strong></td>
                        <td align="center"><strong>描述</strong></td>
                        <td width="8%" align="center"><strong>申请人</strong></td>
                        </tr>
                    <asp:Repeater runat="server" ID="repContract" >
                        <ItemTemplate>
                            <tr>
                               <td height="25" align="center">
                                            <%# Eval("FlowTo") == null ? "客户" : ESP.Finance.Utility.Common.ApplyForInvioceFlowTo_Names[ (int)Eval("FlowTo")] %>
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
                            <em>审批信息</em>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%" height="25">
                            <strong>申请人 （项目主管）</strong>
                        </td>
                        <td width="20%">
                            &nbsp;
                        </td>
                        <td width="15%">
                            <strong>申请日期</strong>
                        </td>
                        <td width="15%">
                            &nbsp;
                        </td>
                        <td width="15%">
                            <strong>项目总监</strong>
                        </td>
                        <td width="15">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td height="25">
                            <strong>团队总经理签字</strong>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <strong>财务总监签字</strong>
                        </td>
                        <td colspan="3">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                    <td colspan="6">
                   <em> 审批记录</em>
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
