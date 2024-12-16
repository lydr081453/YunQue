<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="PrFileList.aspx.cs" Inherits="PurchaseWeb.Purchase.Requisition.PrFileList" %>
<%@ Import Namespace="ESP.Purchase.Common"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" src="../../public/js/DatePicker.js"></script>

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>

    

    <script type="text/javascript" src="/public/js/jquery.blockUI.js"></script>

    <script type="text/javascript">

        function SelectAll() {
            for (var i = 0; i < document.getElementsByName("chkItem").length; i++) {
                var e = document.getElementsByName("chkItem")[i];
                e.checked = document.forms[0].chkAll.checked;
            }
        }

        function getGids() {
            var chks = document.getElementsByName("chkItem");
            var gids = "";
            for (var i = 0; i < chks.length; i++) {
                if (chks[i].checked == true) {
                    gids += chks[i].value + ",";
                }
            }
            if (gids == "") {
                alert("请选择您要打印的数据！");
                return "";
            }
            gids = gids.substring(0, gids.length - 1);
            return gids;
        }

        function printPr() {
            var gids = getGids();
            if (gids == "")
                return false;
            window.open("Print/RequisitionPrint.aspx?id=" + gids);
            return false;
        }
        function openPaymentPreview(id) {
            var gids = "";
            if (id != "")
                gids = id;
            else
                gids = getGids();
            if (gids == "")
                return false;
            var win = window.open("Print/PaymentPreview.aspx?gid=" + gids, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);

            return false;
        }

        function openRecipient(id) {
            var gids = "";
            if (id != "")
                gids = id;
            else
                gids = getGids();
            if (gids == "")
                return false;
            var win = window.open("Print/MultiRecipientPrint.aspx?id=" + gids, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
            return false;
        }
    </script>

    <table width="100%" class="tableForm">
        <tr>
            <td colspan="4" class="heading">
                检索
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                流水号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtGlideNo" runat="server" MaxLength="300" />
            </td>
            <td class="oddrow" style="width: 15%">
                申请单号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtPrNo" runat="server" MaxLength="300"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                供应商名称:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtsupplierName" runat="server" MaxLength="300" />
            </td>
            <td class="oddrow">
                初审人:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtAudit" runat="server" MaxLength="300" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                采购总金额:
            </td>
            <td class="oddrow-l">
                (Min)<asp:TextBox ID="txtTotalMin" runat="server" MaxLength="300"></asp:TextBox>----
                <asp:TextBox ID="txtTotalMax" runat="server" MaxLength="300"></asp:TextBox>(Max)
            </td>
            <td class="oddrow">
                申请人:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtRequestor" runat="server" MaxLength="300" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                申请单流向:
            </td>
            <td class="oddrow-l">
                <asp:DropDownList ID="ddlRequisitionflow" runat="server">
                </asp:DropDownList>
            </td>
            <td class="oddrow">
                状态:
            </td>
            <td class="oddrow-l">
                <asp:DropDownList ID="ddlState" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                分公司审核人:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtFiliale_AuditName" runat="server" MaxLength="300" />
            </td>
            <td class="oddrow">
                审批时间:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtBegin" runat="server" onfocus="javascript:this.blur();" Width="100px" />&nbsp;<img
                    src="../../images/dynCalendar.gif" border="0" onclick="popUpCalendar(document.getElementById('ctl00_ContentPlaceHolder1_txtBegin'), document.getElementById('ctl00_ContentPlaceHolder1_txtBegin'), 'yyyy-mm-dd');" />-<asp:TextBox
                        ID="txtEnd" runat="server" onfocus="javascript:this.blur();" Width="100px" />&nbsp;<img
                            src="../../images/dynCalendar.gif" border="0" onclick="popUpCalendar(document.getElementById('ctl00_ContentPlaceHolder1_txtEnd'), document.getElementById('ctl00_ContentPlaceHolder1_txtEnd'), 'yyyy-mm-dd');" />
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4" align="center">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" id="tabTop" runat="server">
        <tr>
            <td colspan="2" align="right">
                <input type="button" id="btnPrint" value="批量打印PR单" class="widebuttons" onclick="return printPr();" />
                &nbsp;<input type="button" id="btnPrintGR" value="批量打印GR单" class="widebuttons" onclick="return openRecipient('');" />
                &nbsp;<input type="button" id="btnPrintPN" value="批量打印PN单" class="widebuttons" onclick="return openPaymentPreview('');" />
            </td>
        </tr>
        <tr>
            <td width="50%">
                <asp:Panel ID="PageTop" runat="server">
                    <asp:Button ID="btnFirst2" runat="server" CssClass="widebuttons" Text="首页" OnClick="btnFirst_Click" />&nbsp;
                    <asp:Button ID="btnPrevious2" runat="server" CssClass="widebuttons" Text="上页" OnClick="btnPrevious_Click" />&nbsp;
                    <asp:Button ID="btnNext2" runat="server" CssClass="widebuttons" Text="下页" OnClick="btnNext_Click" />&nbsp;
                    <asp:Button ID="btnLast2" runat="server" CssClass="widebuttons" Text="末页" OnClick="btnLast_Click" />
                </asp:Panel>
            </td>
            <td align="right" class="recordTd">
                记录数:<asp:Label ID="labAllNumT" runat="server" />&nbsp;页数:<asp:Label ID="labPageCountT"
                    runat="server" />
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gvList" runat="server" AllowSorting="true" AutoGenerateColumns="False"
        OnPageIndexChanging="gvList_PageIndexChanging" OnRowDataBound="gvList_RowDataBound"
        DataKeyNames="id" PageSize="50" AllowPaging="True" Width="100%">
        <Columns>
            <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                <HeaderTemplate>
                    <input name="chkAll" id="chkAll" onclick="SelectAll();" type="checkbox" />全选
                </HeaderTemplate>
                <ItemTemplate>
                    <input name="chkItem" id="chkItem" type="checkbox" value='<%# Eval("id").ToString() %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="glideNo" HeaderText="流水号" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="7%" />
            <asp:TemplateField HeaderText="申请单号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
                <ItemTemplate>
                    <%# Eval("prNo")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="申请人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%">
                <ItemTemplate>
                    <span class="userLabel" onclick="ShowMsg('<%# ESP.Web.UI.PageBase.GetUserInfo(int.Parse(Eval("requestor").ToString())) %>');">
                        <%# Eval("requestorname") %></span>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="申请时间" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%">
                <ItemTemplate>
                    <%# Eval("requisition_committime").ToString() == ESP.Purchase.Common.State.datetime_minvalue ? "" : DateTime.Parse(Eval("requisition_committime").ToString()).ToString()%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="初审人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%">
                <ItemTemplate>
                    <span class="userLabel" onclick="ShowMsg('<%# Eval("first_assessor").ToString() == "0" ? "" : ESP.Web.UI.PageBase.GetUserInfo(int.Parse(Eval("first_assessor").ToString())) %>');">
                        <%# Eval("first_assessorname")%></span>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="project_code" HeaderText="项目号" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="10%" />
            <asp:TemplateField HeaderText="比价信息备注" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Literal ID="litContrastUpFiles" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="工作需求描述" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Literal ID="litSow" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="比价节约" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Literal ID="litContrast" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="议价节约" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Literal ID="litConsult" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="客户指定邮件" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Literal ID="litCusAskEmailFile" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="采购物品附件" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Repeater ID="repProduct" runat="server">
                        <ItemTemplate>
                            <%# Eval("item_no") + " : " + ((Eval("upfile") == null || Eval("upfile").ToString() == "") ? "" : "<a target='_blank' href='/Purchase/Requisition/UpfileDownload.aspx?OrderId=" + Eval("id").ToString() + "&Index=0'>'><img src='/images/ico_04.gif' border='0' /></a>") + "<br />"%>
                        </ItemTemplate>
                    </asp:Repeater>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="收货单" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <a href="" onclick="return openRecipient('<%# Eval("id") %>');">
                        <img src="../../images/pri_gr.gif" /></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="付款申请" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <a href="" onclick="return openPaymentPreview('<%# Eval("id") %>');">
                        <img src="../../images/pri_pn.gif" border="0px;" title="打印"></a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerSettings Visible="false" />
    </asp:GridView>
    <table width="100%" id="tabBottom" runat="server">
        <tr>
            <td width="50%">
                <asp:Panel ID="PageBottom" runat="server">
                    <asp:Button ID="btnFirst" runat="server" CssClass="widebuttons" Text="首页" OnClick="btnFirst_Click" />&nbsp;
                    <asp:Button ID="btnPrevious" runat="server" CssClass="widebuttons" Text="上页" OnClick="btnPrevious_Click" />&nbsp;
                    <asp:Button ID="btnNext" runat="server" CssClass="widebuttons" Text="下页" OnClick="btnNext_Click" />&nbsp;
                    <asp:Button ID="btnLast" runat="server" CssClass="widebuttons" Text="末页" OnClick="btnLast_Click" />&nbsp;
                </asp:Panel>
            </td>
            <td align="right" class="recordTd">
                记录数:<asp:Label ID="labAllNum" runat="server" />&nbsp;页数:<asp:Label ID="labPageCount"
                    runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
