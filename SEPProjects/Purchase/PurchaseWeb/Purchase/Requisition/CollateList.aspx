﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="CollateList.aspx.cs" Inherits="Purchase_Requisition_CollateList" %>

<%@ Import Namespace="ESP.Purchase.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script language="javascript" src="../../public/js/DatePicker.js"></script>

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>

    

    <script type="text/javascript" src="/public/js/jquery.blockUI.js"></script>

    <script language="javascript" type="text/javascript">

        var IsSelected = false;
        function checkInput(recordCount) {
            if (document.all("SearchStr").value.trim() == "") {
                if (confirm("您没有选择任何参数，该查询将返回前" + recordCount + "条的数据。是否继续？") == false) {
                    document.all("SearchStr").focus();
                    return false;
                }
            }

            return true;
        }

        function DeleteUser(usercode) {
            if (confirm("确认删除编号为[" + usercode + "]的员工？") == false) {
                return false;
            }
            else {
                window.navigate("UserModify.aspx?usercode=" + usercode + "&action=delete");
            }
        }
        function cbcheck() {
            if (document.all.cblist.value.length < 1) {
                alert("请选择您要处理的记录！");
                return false;
            }

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
        function openPrint(oldPRID) {
            var win = window.open("Print/CollatePrint.aspx?oldPRID=" + oldPRID, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
            return false;
        }
    </script>

    <table style="width: 100%">
        <tr>
            <td colspan="2" style="padding: 4px;">
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
                                        初审人:
                                    </td>
                                    <td class="oddrow-l">
                                        <asp:TextBox ID="txtAudit" runat="server" MaxLength="300" />
                                    </td>
                                    <td class="oddrow">
                                        申请人:
                                    </td>
                                    <td class="oddrow-l">
                                        <asp:TextBox ID="txtrequestor" runat="server" MaxLength="300" />
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
                                        供应商名称:
                                    </td>
                                    <td class="oddrow-l">
                                        <asp:TextBox ID="txtsupplier" runat="server" MaxLength="300" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow">
                                        申请时间:
                                    </td>
                                    <td class="oddrow-l" colspan="3">
                                        <asp:TextBox ID="txtBegin" runat="server" onfocus="javascript:this.blur();" Width="100px" />&nbsp;<img
                                            src="../../images/dynCalendar.gif" border="0" onclick="popUpCalendar(document.getElementById('ctl00_ContentPlaceHolder1_txtBegin'), document.getElementById('ctl00_ContentPlaceHolder1_txtBegin'), 'yyyy-mm-dd');" />-<asp:TextBox
                                                ID="txtEnd" onfocus="javascript:this.blur();" runat="server" Width="100px" />&nbsp;<img
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
                        <td colspan="2" align="right">
                            <asp:Button ID="btnExport" runat="server" Text="导出Excel" OnClick="btnExport_Click"
                                CssClass="widebuttons" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table width="100%" id="tabTop" runat="server">
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
                                    <asp:BoundField DataField="id" HeaderText="id" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="glideNo" HeaderText="流水号" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="6%" />
                                    <asp:TemplateField HeaderText="申请单号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
                                        <ItemTemplate>
                                            <a href="OrderDetail.aspx?<% =RequestName.GeneralID %>=<%#Eval("id") %>&backUrl=CollateList.aspx">
                                                <%#Eval("prNO") %></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="申请人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <span class="userLabel" onclick="ShowMsg('<%# ESP.Web.UI.PageBase.GetUserInfo(int.Parse(Eval("requestor").ToString())) %>');">
                                                <%# Eval("requestorname") %></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="requestor_group" HeaderText="业务组别" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="申请时间" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <%# Eval("requisition_committime").ToString() == ESP.Purchase.Common.State.datetime_minvalue ? "" : Eval("requisition_committime").ToString()%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="project_code" HeaderText="项目号" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%" />
                                    <asp:TemplateField HeaderText="分公司审核人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="4%">
                                        <ItemTemplate>
                                            <span class="userLabel" onclick="ShowMsg('<%# Eval("Filiale_Auditor").ToString() == "0" ? "" : ESP.Web.UI.PageBase.GetUserInfo(int.Parse(Eval("Filiale_Auditor").ToString())) %>');">
                                                <%# Eval("Filiale_AuditName")%></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="初审人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <span class="userLabel" onclick="ShowMsg('<%# Eval("first_assessor").ToString() == "0" ? "" : ESP.Web.UI.PageBase.GetUserInfo(int.Parse(Eval("first_assessor").ToString())) %>');">
                                                <%# Eval("first_assessorname")%></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="supplier_name" HeaderText="供应商" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="采购物品" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Repeater ID="repProduct" runat="server">
                                                <ItemTemplate>
                                                    <%# Eval("Item_No").ToString() %>&nbsp;Total:<%# Eval("moneytype").ToString() %><br />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="物料类别" ItemStyle-Width="6%" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Repeater ID="repProductType" runat="server">
                                                <ItemTemplate>
                                                    <%# Eval("producttypename").ToString() %><br />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="付款状态" ItemStyle-Width="6%" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Repeater ID="repReturnStatus" runat="server">
                                                <ItemTemplate>
                                                    <%# Eval("returnCode").ToString() + ":" + ESP.Finance.Utility.ReturnPaymentType.ReturnStatusString(int.Parse(Eval("ReturnStatus").ToString()),0,null)%><br />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="打印" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hylPrint" runat="server" Style="cursor: pointer" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
                                        <ItemTemplate>
                                            <a href="CollateDetail.aspx?oldPRID=<%#Eval("id") %>">
                                                <img src="../../images/dc.gif" border="0px;" title="查看"></a>
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
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>