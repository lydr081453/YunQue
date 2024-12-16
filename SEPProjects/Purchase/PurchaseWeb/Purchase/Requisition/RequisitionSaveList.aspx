<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="Purchase_Requisition_RequisitionSaveList" MasterPageFile="~/MasterPage.master"
    EnableEventValidation="false" Codebehind="RequisitionSaveList.aspx.cs" %>
<%@ Import Namespace="ESP.Purchase.Common"%>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript" src="../../public/js/DatePicker.js"></script>

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script type="text/javascript" src="/public/js/jquery.jcarousellite.min.js"></script>

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
    </script>
    <table style="width: 100%">
        <tr>
            <td style="padding: 4px;">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <li><asp:LinkButton ID="NewUserUrl" runat="server" Text="新增申请单" OnClick="NewUserUrl_Click" /></li>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" class="tableForm" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="heading" colspan="4">检索</td>
                                </tr>
                                <tr>
                                    <td class="oddrow" style="width:15%">项目号:</td>
                                    <td class="oddrow-l" style="width:35%"><asp:TextBox ID="txtProjectNo" runat="server" MaxLength="300" /></td>
                                    <td  class="oddrow" style="width:15%">申请时间:</td>
                                    <td class="oddrow-l" style="width:35%">
                                        <asp:TextBox ID="txtBegin" runat="server" onfocus="javascript:this.blur();" Width="100px" />&nbsp;<img src="../../images/dynCalendar.gif"
                                            border="0" onclick="popUpCalendar(document.getElementById('ctl00_ContentPlaceHolder1_txtBegin'), document.getElementById('ctl00_ContentPlaceHolder1_txtBegin'), 'yyyy-mm-dd');" />-<asp:TextBox
                                                ID="txtEnd" onfocus="javascript:this.blur();" runat="server" Width="100px" />&nbsp;<img src="../../images/dynCalendar.gif"
                                                    border="0" onclick="popUpCalendar(document.getElementById('ctl00_ContentPlaceHolder1_txtEnd'), document.getElementById('ctl00_ContentPlaceHolder1_txtEnd'), 'yyyy-mm-dd');" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow">采购物品:</td>
                                    <td class="oddrow-l"><asp:TextBox ID="txtItemNo" runat="server" MaxLength="300" /></td>
                                    <td  class="oddrow">采购总金额:</td>
                                    <td class="oddrow-l">
                                        (Min)<asp:TextBox ID="txtTotalMin" runat="server"  MaxLength="300"></asp:TextBox>----<asp:TextBox ID="txtTotalMax" runat="server" MaxLength="300"></asp:TextBox>(Max)
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow-l" colspan="4">
                                        <asp:Button ID="SearchBtn" runat="server" Text=" 检索 "  CssClass="widebuttons" OnClick="SearchBtn_Click"></asp:Button>
                                    </td>
                                </tr>
                            </table>
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
                                    <td align="right"  class="recordTd">
                                        记录数:<asp:Label ID="labAllNumT" runat="server" />&nbsp;页数:<asp:Label ID="labPageCountT"
                                            runat="server" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <asp:GridView ID="gvG" runat="server"  AutoGenerateColumns="False"
                                DataKeyNames="id" PageSize="20" Width="100%" AllowPaging="True" OnRowCommand="gvG_RowCommand"
                                OnRowDataBound="gvG_RowDataBound" OnPageIndexChanging="gvG_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField DataField="id" HeaderText="id" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="addstatus" HeaderText="addstatus" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="glideNo" HeaderText="流水号"  ItemStyle-HorizontalAlign="Center"/>
                                    <asp:TemplateField HeaderText="申请人" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <span class="userLabel" onclick="ShowMsg('<%# ESP.Web.UI.PageBase.GetUserInfo(int.Parse(Eval("requestor").ToString())) %>');"><%# Eval("requestorname") %></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="申请时间" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%"  >
                                        <ItemTemplate>
                                            <%# Eval("app_date").ToString() == State.datetime_minvalue ? "" : DateTime.Parse(Eval("app_date").ToString()).ToString()%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="project_code" HeaderText="项目号" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="project_descripttion" HeaderText="项目名称" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%"/>
                                    <asp:BoundField DataField="supplier_name" HeaderText="供应商" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%"/>
                                    <asp:BoundField DataField="itemno" HeaderText="采购物品" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="15%"/>
                                    <asp:TemplateField HeaderText="采购总金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="6%" >
                                        <ItemTemplate>
                                          <%# Eval("moneytype").ToString() == "美元" ? "＄" + decimal.Parse(Eval("ototalprice").ToString()).ToString("#,##0.####") : "￥" + decimal.Parse(Eval("ototalprice").ToString()).ToString("#,##0.####")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="初审人" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <span class="userLabel" onclick="ShowMsg('<%# Eval("first_assessor").ToString() == "0" ? "" : ESP.Web.UI.PageBase.GetUserInfo(int.Parse(Eval("first_assessor").ToString())) %>');"><%# Eval("first_assessorname")%></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="查看">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <a href="ShowRequisitionDetail.aspx?<% =RequestName.GeneralID %>=<%#Eval("id") %>">
                                                <img src="../../images/dc.gif" border="0px;"  title="查看"/></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="编辑">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hylEdit" runat="server" ImageUrl="../../images/edit.gif" ToolTip="编辑"></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" CommandArgument='<%# Eval("id") %>' CommandName="Del" Text="<img src='../../images/disable.gif' title='删除' border='0'>" OnClientClick="return confirm('你确定删除吗？');" />
                                        </ItemTemplate>
                                    </asp:TemplateField>                                   
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" />
                                <PagerSettings Visible="false" />
                            </asp:GridView>
                            <table width="100%" id="tabBottom" runat="server">
                                <tr>
                                    <td width="50%" >
                                        <asp:Panel ID="PageBottom" runat="server">
                                            <asp:Button ID="btnFirst" runat="server" CssClass="widebuttons" Text="首页" OnClick="btnFirst_Click" />&nbsp;
                                            <asp:Button ID="btnPrevious" runat="server" CssClass="widebuttons" Text="上页" OnClick="btnPrevious_Click" />&nbsp;
                                            <asp:Button ID="btnNext" runat="server" CssClass="widebuttons" Text="下页" OnClick="btnNext_Click" />&nbsp;
                                            <asp:Button ID="btnLast" runat="server" CssClass="widebuttons" Text="末页" OnClick="btnLast_Click" />&nbsp;                                            
                                        </asp:Panel>
                                    </td>
                                    <td align="right"  class="recordTd">
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
