<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="Default2" Title="Untitled Page" EnableViewState="false" CodeBehind="Default2.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="/public/css/dialog.css" type="text/css" rel="stylesheet" />

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script language="javascript" src="/public/js/dialog.js"></script>

    <script language="javascript">
        function show(id) {
            dialog("查看", "url:get?/Purchase/Message/MessageView.aspx?action=show&isback=1&id=" + id, "900px", "400px", "text");
        }
        function localwindow() {
            window.location.href = "Requisition/SupplierInfoListD.aspx";
        }

        function openwindow(id) {
            window.open("Requisition/ProposedSupplierView.aspx?supplierId=" + id);
        }
        function openProductTypes(type) {
            win = window.open('Requisition/ProductTypeItems.aspx?type=' + type, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }
        function openAll() {
            win = window.open('allMessage.aspx', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }
        function change(flg) {
            if (flg == "0") {
                document.getElementById("trBack").style.display = "none";
                document.getElementById("trRegular").style = "";
                document.getElementById("tdBack").background = "/images/dbg1.gif";
                document.getElementById("tdRegular").background = "/images/dbg2.gif";
                document.getElementById("spanRegular").style.color = "#ffffff";
                document.getElementById("spanBack").style.color = "#223d72";
            }
            else {
                document.getElementById("trBack").style = "";
                document.getElementById("trRegular").style.display = "none";
                document.getElementById("tdBack").background = "/images/dbg2.gif";
                document.getElementById("tdRegular").background = "/images/dbg1.gif";
                document.getElementById("spanRegular").style.color = "#223d72";
                document.getElementById("spanBack").style.color = "#ffffff";
            }
        }
    </script>

    <style type="text/css">
        body, td, th
        {
            font-size: 12px;
        }
        body
        {
            margin: 0px;
        }
        a:link
        {
            text-decoration: none;
        }
        a:visited
        {
            text-decoration: none;
        }
        a:hover
        {
            text-decoration: none;
        }
        a:active
        {
            text-decoration: none;
        }

     .hrefover{color:Blue;text-decoration:underline;}
     .hrefout{color:Black;}

</style>
    </style>
    <table width="860" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="left">
                <table width="596" height="315" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="36" valign="bottom" background="/images/btop.gif">
                            <div style="margin-left: 20px; font-size: 13px; color: #5c5c7e; font-weight: bold;">
                                新闻中心</div>
                        </td>
                    </tr>
                    <tr>
                        <td background="/images/btopbg1.gif" height="8">
                        </td>
                    </tr>
                    <tr>
                        <td background="/images/bbg.gif" valign="top">
                            <div style="margin-left: 20px; vertical-align: top; color: #24242c; line-height: 23px;font-size:13px; font-weight:bolder;">
                                <asp:Literal runat="server" ID="litMessage"></asp:Literal><br />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td background="/images/bbottom.gif" height="12">
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table width="262" height="315" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="35" background="/images/ctop.gif">
                            <div style="margin-left: 20px; font-size: 13px; color: #5c5c7e; font-weight: bold;">
                                政策中心</div>
                        </td>
                    </tr>
                    <tr>
                        <td background="/images/ctopbg1.gif" width="262" height="10">
                        </td>
                    </tr>
                    <tr>
                        <td background="/images/cbg.gif" valign="top">
                            <div style="margin-left: 20px; vertical-align: top; color: #24242c; line-height: 23px;font-size:13px; font-weight:bolder;">
                                <asp:Literal runat="server" ID="litNews"></asp:Literal></div>
                        </td>
                    </tr>
                    <tr>
                        <td background="/images/cbottom.gif" width="262" height="11">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="100%" height="315" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="left">
                            <table width="596" height="315" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td height="36" valign="bottom" background="/images/btop.gif">
                                        <div style="margin-left: 20px; font-size: 12px; font-weight: bold;">
                                            <table width="50%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td id="tdRegular" width="65" height="27" align="center" background="/images/dbg2.gif">
                                                        <span id="spanRegular" onclick="change(0);" style="cursor: hand; color: #ffffff">基本流程</span>
                                                    </td>
                                                    <td width="5">
                                                    </td>
                                                    <td id="tdBack" width="65" height="27" align="center" background="/images/dbg1.gif">
                                                        <span id="spanBack" onclick="change(1);" style="cursor: hand; color: #223d72">撤销流程</span>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td background="/images/btopbg1.gif" height="8">
                                    </td>
                                </tr>
                                <tr id="trRegular">
                                    <td valign="top" background="/images/bbg.gif">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td align="center" valign="middle">
                                                    <img src="/images/img_tu1.jpg" width="451" height="258" border="0" usemap="#MapMap" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr style="display: none;" id="trBack">
                                    <td valign="top" background="/images/bbg.gif">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td align="center" valign="middle">
                                                    <img src="/images/img_tu2.jpg" width="477" height="59" border="0" usemap="#MapBack" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td background="/images/bbottom.gif" height="11">
                                    </td>
                                </tr>
                            </table>
                            <map name="MapMap" id="MapMap">
                                <area shape="rect" coords="2,77,65,123" href="/Purchase/Requisition/AddRequisitionStep2.aspx" />
                                <area shape="rect" coords="98,80,163,125" href="/Purchase/Requisition/OperationAuditList.aspx" />
                                <area shape="rect" coords="296,11,355,58" href="/Purchase/Requisition/prADAuditList.aspx" />
                                <area shape="rect" coords="295,74,358,126" href="/Purchase/Requisition/prMediaAuditList.aspx" />
                                <area shape="rect" coords="292,140,361,184" href="/Purchase/Requisition/OrderList.aspx" />
                                <area shape="rect" coords="293,207,359,255" href="/Purchase/Requisition/FilialeAuditList.aspx" />
                                <area shape="rect" coords="389,170,459,226" href="/Purchase/Requisition/AuditRequistion.aspx" />
                                <area shape="rect" coords="481,107,555,156" href="/Purchase/Requisition/PaymentGeneralList.aspx" />
                            </map>
                            <map name="MapBack" id="MapBack">
                                <area shape="rect" coords="12,6,86,66" target="_top" href="http://xf.shunyagroup.com/Edit/ReturnTabEdit.aspx?Type=return" />
                                <area shape="rect" coords="113,8,187,65" href="/Purchase/Requisition/PaymentGeneralList.aspx" />
                                <area shape="rect" coords="206,8,280,63" href="/Purchase/Requisition/RecipientOkList.aspx" />
                                <area shape="rect" coords="305,11,367,65" href="/Purchase/Requisition/AuditOrderPassList.aspx" />
                                <area shape="rect" coords="405,9,466,62" href="/Purchase/Requisition/RequistionCommitList.aspx" />
                            </map>
                        </td>
                        <td valign="top">
                            <table width="262" height="315" border="0" align="center" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td height="35" background="/images/ctop.gif">
                                        <div style="margin-left: 20px; font-size: 13px; color: #5c5c7e; font-weight: bold;">
                                            供应商中心</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td background="/images/ctopbg1.gif" width="262" height="10">
                                    </td>
                                </tr>
                                <tr>
                                    <td background="/images/cbg.gif">
                                         <div style="margin-left: 20px; color: #24242c; line-height: 23px; vertical-align:top;">
                                         &nbsp;<br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
                            </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td background="/images/cbottom.gif" width="262" height="11">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
      
    </table>
</asp:Content>
