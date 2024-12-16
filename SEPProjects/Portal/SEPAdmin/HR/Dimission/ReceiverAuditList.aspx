<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReceiverAuditList.aspx.cs" Inherits="ReceiverAuditList" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>
    <script type="text/javascript" src="/public/js/jquery1.12.js"></script>

    <script type="text/javascript">
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }

        // 全选
        function selectAll(obj) {
            var allobj = document.getElementsByTagName("input");
            var obj = document.getElementById("CheckAll");
            for (var i = 0; i < allobj.length; i++) {
                var o = allobj[i];
                if (o.type == "checkbox") {
                    if (o.id != "CheckAll" && o.id.indexOf("ctl00_ContentPlaceHolder1_gvDetailList") > -1) {
                        o.checked = obj.checked;
                    }
                }
            }
        }

        function Receiver() {
            var allobj = document.getElementsByTagName("input");
            var obj = document.getElementById("CheckAll");
            var flag = 0;
            if (allobj != null) {
                for (var i = 0; i < allobj.length; i++) {
                    var o = allobj[i];
                    if (o != null && o.type == "checkbox") {
                        if (o.id != "CheckAll" && o.id.indexOf("ctl00_ContentPlaceHolder1_gvDetailList") > -1 && o.checked == true) {
                            flag = 1;
                        }
                    }
                }
            }

            if (flag == 0) {
                alert("请选择您要接受的单据。");
                return false;
            }
            if (confirm("您确认接收此业务单据？")) {
                showLoading();
                return true;
            }
            else { return false;}
        }
    </script>

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                离职检索
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                员工编号:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox ID="txtUserCode" runat="server" />
            </td>
            <td class="oddrow" style="width: 15%">
                姓名:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox ID="txtuserName" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                部门:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtDepartments" runat="server" />
            </td>
            <td class="oddrow" style="width: 15%">
                离职日期:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtBeginTime" runat="server" onclick="setDate(this);" />&nbsp --
                &nbsp<asp:TextBox ID="txtEndTime" runat="server" onclick="setDate(this);" />
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
    <br />
    <table width="100%">
        <tr>
            <td>
                <table width="100%" id="tabTop" runat="server">
                    <tr>
                        <td width="50%">
                            <asp:Button ID="btnReceiverTop" runat="server" CssClass="widebuttons" Text="确认交接"
                                OnClick="btnReceiverTop_Click" OnClientClick="return Receiver();" />&nbsp;
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
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvDetailList" runat="server" AutoGenerateColumns="False" DataKeyNames="DimissionDetailId"
                    OnPageIndexChanging="gvList_PageIndexChanging" Width="100%" PageSize="2">
                    <Columns>
                        <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="ckb" runat="server" />
                            </ItemTemplate>
                            <HeaderTemplate>
                                <input id="CheckAll" type="checkbox" onclick="selectAll(this);" />全选
                            </HeaderTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DimissionDetailId" Visible="false" />
                        <asp:BoundField DataField="FormId" Visible="false" />
                        <asp:TemplateField HeaderText="单据编号">
                            <ItemTemplate>
                                <a href='http://<%# Eval("Website").ToString() + Eval("Url").ToString() %>' target="_blank" title="<%# Eval("FormCode") %>">
                                    <%# Eval("FormCode") %></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="FormType" HeaderText="单据类型" />
                        <asp:BoundField DataField="UserName" HeaderText="单据负责人" />
                        <asp:BoundField DataField="ProjectCode" HeaderText="项目号" />
                        <asp:BoundField DataField="Description" HeaderText="项目号描述" />
                        <asp:BoundField DataField="TotalPrice" HeaderText="总金额" />
                        <asp:BoundField DataField="DirectorName" HeaderText="总监审批人" />
                    </Columns>
                    <PagerSettings Visible="false" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" id="tabBottom" runat="server">
                    <tr>
                        <td width="50%">
                            <asp:Button ID="btnReceiverBottom" runat="server" CssClass="widebuttons" Text="确认交接"
                                OnClick="btnReceiverTop_Click" OnClientClick="return Receiver();" />&nbsp;
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
</asp:Content>
