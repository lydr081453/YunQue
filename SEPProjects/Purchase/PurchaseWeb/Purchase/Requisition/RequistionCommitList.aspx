<%@ Page Language="C#" AutoEventWireup="true" Inherits="Purchase_Requisition_RequistionCommitList"
    MasterPageFile="~/MasterPage.master" EnableEventValidation="false" CodeBehind="RequistionCommitList.aspx.cs" %>

<%@ Import Namespace="ESP.Purchase.Common" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript" src="../../public/js/DatePicker.js"></script>

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script language="javascript" src="/public/js/jquery.jcarousellite.min.js"></script>
    <script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>

    <script type="text/javascript" src="/public/js/jquery.blockUI.js"></script>

    <script language="javascript" type="text/javascript">

        function openAuditOperation(generalId) {
            var win = window.open('operationAuditView.aspx?<%= RequestName.GeneralID %>=' + generalId, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }

        function SelectAll() {
            for (var i = 0; i < document.getElementsByName("chkItem").length; i++) {
                var e = document.getElementsByName("chkItem")[i];
                e.checked = document.forms[0].chkAll.checked;
            }
        }

 function confirmEmail(mail) {
            var newMail = prompt("供应商邮箱为：" + mail + "，您确定发送此邮件吗？如要更改邮件信息，请于下边输入新的邮件。", mail)
            if (newMail != null && newMail != "") {
                document.getElementById('<%= hidSupplierEmail.ClientID %>').value = newMail;
                return 2;
            } else if (newMail == null) {
                return 0;
            } else {
                return 1;
            }
        }

        function confirmEamil1(mail) {
            var newMail = prompt("供应商邮箱为：" + mail + "，已给供应商发送过邮件，确定再次发送吗？如要更改邮件信息，请于下边输入新的邮件。", mail)
            if (newMail != null && newMail != "") {
                document.getElementById('<%= hidSupplierEmail.ClientID %>').value = newMail;
                return 2;
            } else if (newMail == null) {
                return 0;
            } else {
                return 1;
            }
        }
        function printPr() {
            var chks = document.getElementsByName("chkItem");
            var gids = "";
            for (var i = 0; i < chks.length; i++) {
                if (chks[i].checked == true) {
                    gids += chks[i].value + ",";
                }
            }
            if (gids == "") {
                alert("请选择您要打印的数据！");
                return false;
            }
            gids = gids.substring(0, gids.length - 1);
            window.open("Print/RequisitionPrint.aspx?id=" + gids);
            return false;
        }

        function cblist1() {
            var iCountSelected = 0;
            var cbid = "";
            var cbtotal = document.all.cblist.value;
            var obj = document.all.tags("input");
            for (i = 0; i < obj.length; i++) {
                if (obj[i].type == "checkbox") {
                    cbid = obj[i].id + ";";
                    if (document.all.cblist.value.indexOf(cbid) >= 0) {
                        document.all.cblist.value = document.all.cblist.value.replace(cbid, "");
                    }
                    if (obj[i].checked) {
                        document.all.cblist.value += cbid;
                    }
                }
            }
        }

        function initcb() {
            var obj = document.all.tags("input");
            for (i = 0; i < obj.length; i++) {
                if (obj[i].type == "checkbox") {
                    cbid = obj[i].id + ";";
                    if (document.all.cblist.value.indexOf(cbid) >= 0) {
                        obj[i].checked = true;
                    }
                }
            }
        }

        function selectedcheck(parent, sub) {
            var chkSelect = document.getElementById("chk" + parent);
            var elem = document.getElementsByName("chk" + sub);
            for (i = 0; i < elem.length; i++) {
                if (elem[i].type == "checkbox") {
                    elem[i].checked = chkSelect.checked;
                }
            }
        }
	
    </script>

    <table style="width: 100%">
        <tr>
            <td colspan="2" style="padding: 4px;">
                <table style="width: 100%;">
                    <tr>
                        <td colspan="2">
                            <table style="width: 100%;">
                                <tr>
                                    <td colspan="2">
                                        <table width="100%" class="tableForm">
                                            <tr>
                                                <td class="heading" colspan="4">
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
                                                    项目号:
                                                </td>
                                                <td class="oddrow-l">
                                                    <asp:TextBox ID="txtProjectCode" runat="server" MaxLength="20" />
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
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table width="100%" id="tabTop" runat="server">
                                            <tr>
                                                <td colspan="2" align="right">
                                                    <input type="button" id="btnPrint" value="批量打印PR单" class="widebuttons" onclick="return printPr();" />
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
                                        <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="id,moneytype"
                                            PageSize="20" AllowPaging="True" Width="100%" OnRowCommand="gvG_RowCommand" OnRowDataBound="gvG_RowDataBound"
                                            OnPageIndexChanging="gvG_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>
                                                        <input name="chkAll" id="chkAll" onclick="SelectAll();" type="checkbox" />全选
                                                    </HeaderTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                                    <ItemTemplate>
                                                        <input name="chkItem" id="chkItem" type="checkbox" value='<%# Eval("id").ToString() %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="glideNo" HeaderText="流水号" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="6%" />
                                                <asp:TemplateField HeaderText="申请单号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hypNoView" runat="server" Text='<%# Eval("prNo") %>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="订单号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="labOrderid" Text='<%#Eval("orderid") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="申请时间" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%">
                                                    <ItemTemplate>
                                                        <%# Eval("requisition_committime").ToString() == State.datetime_minvalue ? "" : DateTime.Parse(Eval("requisition_committime").ToString()).ToString()%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="project_code" HeaderText="项目号" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="7%" />
                                                <asp:TemplateField HeaderText="初审人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <span class="userLabel" onclick="ShowMsg('<%# Eval("first_assessor").ToString() == "0" ? "" : ESP.Web.UI.PageBase.GetUserInfo(int.Parse(Eval("first_assessor").ToString())) %>');">
                                                            <%# Eval("first_assessorname")%></span>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="supplier_name" HeaderText="供应商" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="10%" />
                                                <asp:BoundField DataField="PRType" HeaderText="PRType" Visible="false" />
                                                <asp:TemplateField HeaderText="采购物品" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Repeater ID="repProduct" runat="server">
                                                            <ItemTemplate>
                                                                <%# Eval("Item_No").ToString() %>&nbsp;Total:<%# Eval("moneytype").ToString() %><br />
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="labState" Text='<%#Eval("status") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="流向" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="labRequisitionflow" Text='<%#Eval("requisitionflow") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="合同追加" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="4%">
                                                    <ItemTemplate>
                                                        <a href='ContrSupplement.aspx?GeneralID=<%#DataBinder.Eval(Container.DataItem,"id")%>'
                                                            target="_blank">
                                                            <img title="合同追加" src="/images/changed.gif" border="0" /></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hypView" runat="server" ImageUrl="/images/dc.gif" title="查看"></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="审核列表" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litOperation" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="打印" ItemStyle-Width="3%">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <a href='Print/RequisitionPrint.aspx?id=<%#DataBinder.Eval(Container.DataItem,"id")%>'
                                                            target="_blank">
                                                            <img title="打印申请单" src="../../images/Icon_PrintPr.gif" border="0" /></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="打印" ItemStyle-Width="3%">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Panel ID="printpo" runat="server">
                                                            <a href='Print/OrderPrint.aspx?id=<%#DataBinder.Eval(Container.DataItem,"id")%>'
                                                                target="_blank">
                                                                <img title="打印订单" src="../../images/Icon_PrintPo.gif" border='0' /></a>
                                                        </asp:Panel>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="撤销" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center"
                                                    ItemStyle-Width="3%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnCancel" runat="server" Text="<img title='撤销申请单' src='../../images/Icon_Cancel.gif' border='0' />"
                                                            OnClientClick="return confirm('您是否确认撤销？');" CausesValidation="false" CommandArgument='<%# Eval("id") %>'
                                                            CommandName="Return" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="邮件" ItemStyle-Width="3%">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkSend" runat="server" Font-Underline="true" Text="<img title='发送邮件' src='../../images/Icon_Sendmail.gif' border='0' />"
                                                            CommandName="SendMail"  CommandArgument='<%# Eval("id") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="确认" ItemStyle-Width="3%">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <%--<asp:LinkButton ID="lnkHankConfirm" runat="server" Font-Underline="true" OnClientClick="return confirm('您确定要手动确认订单吗？');" Text="<img title='手动确认' src='../../images/Icon_handconfrim.gif' border='0' />" CommandName="HandConfirm" />--%>
                                                        <asp:LinkButton ID="lnkHankConfirm" runat="server" Font-Underline="true" OnClientClick="return confirm('请确认已收到供应商书面确认作为支持文件？');"
                                                            Text="<img title='手动确认' src='../../images/Icon_handconfrim.gif' border='0' />"
                                                            CommandName="HandConfirm" CommandArgument='<%# Eval("id") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerSettings Visible="false" />
                                        </asp:GridView>
                                                                    <asp:HiddenField ID="hidSupplierEmail" runat="server" />
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
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
