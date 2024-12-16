<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="RecipientFinanceRpt.aspx.cs" Inherits="FinanceWeb.Reports.RecipientFinanceRpt" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript" src="../../public/js/DatePicker.js"></script>
    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>
    <script language="javascript" type="text/javascript">

        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }
        function CheckInput() {
            var msg = "";
            var begindate = document.getElementById("<%=txtBeginDate.ClientID %>").value;
            var enddate = document.getElementById("<%=txtEndDate.ClientID %>").value;
            if (begindate > enddate) {
                msg += "请输入正确的查询日期！" + "\n";
            }
            if (msg == "")
                return true;
            else {
                alert(msg);
                return false;
            }
        }

        function selectBranch(id, text) {
            if (id == "-1") {
                document.getElementById("<% =hidBranchID.ClientID %>").value = "";
                document.getElementById("<% =hidBranchCode.ClientID %>").value = "";
            }
            else {
                document.getElementById("<% =hidBranchID.ClientID %>").value = id;
                document.getElementById("<% =hidBranchCode.ClientID %>").value = text;
            }
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
                                        关键字:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:TextBox ID="txtKey" runat="server" />
                                    </td>
                                    <td class="oddrow" style="width: 15%">
                                        登记区间:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:TextBox ID="txtBeginDate" onkeyDown="return false; " Style="cursor: hand" runat="server"
                                            onclick="setDate(this);" />--
                                        <asp:TextBox ID="txtEndDate" onkeyDown="return false; " Style="cursor: hand" runat="server"
                                            onclick="setDate(this);" />
                                    </td>
                                </tr>
                                <tr>
                                  <td class="oddrow" style="width: 15%">
                                        公司选择:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:DropDownList runat="server" ID="ddlBranch" style="width:auto"></asp:DropDownList>
                                           <input type="hidden" id="hidBranchID" runat="server" />
                                           <input type="hidden" id="hidBranchCode" runat="server" />
                         
                                    </td>
                                       <td class="oddrow" style="width: 15%"></td>
                                       <td class="oddrow-l" style="width: 35%"></td>
                                </tr>
                                <tr>
                                    <td class="oddrow-l" colspan="4">
                                         <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click" />&nbsp;
                                        <asp:Button ID="btnSearchAll" runat="server" Text=" 检索全部 " CssClass="widebuttons"
                                            OnClick="btnSearchAll_Click" />&nbsp;
                                        <asp:Button ID="btnExport" runat="server" Text=" 导出 " CssClass="widebuttons" OnClick="btnExport_Click"
                                            Style="width: 50px" />&nbsp;
                                        <asp:Button ID="btnExportPaid" runat="server" Text=" 导出区间已付申请 " CssClass="widebuttons" OnClick="btnExportPaid_Click"
                                            Style="width: 200px" />&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                            padding-top: 4px;">
                            <table width="100%" class="tableForm">
                                <tr>
                                    <td class="heading" colspan="4">
                                        付款信息列表
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow">
                                        <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False"
                                            EmptyDataText="暂时没有相关记录" OnRowDataBound="gvG_RowDataBound"
                                            OnPageIndexChanging="gvG_PageIndexChanging"
                                            AllowPaging="false" Width="100%">
                                            <Columns>
                                                <asp:BoundField DataField="prno" HeaderText="PR单号" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="projectCode" HeaderText="项目号" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="customer" HeaderText=" 客户代码" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="projecttype" HeaderText=" 项目类型" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="requestor" HeaderText="申请人" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="userCode" HeaderText="员工编号" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="dept" HeaderText="费用所属组" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="supplierName" HeaderText="供应商" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="fee" HeaderText="预付金额" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="commitDate" HeaderText="申请日期" ItemStyle-HorizontalAlign="Center" />
                                                
                                                      <asp:TemplateField HeaderText="申请状态" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("pnstatus") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                <asp:BoundField DataField="invoice" HeaderText="发票状态" ItemStyle-HorizontalAlign="Center" />
                                                 <asp:BoundField DataField="pnid" HeaderText="pnid" />
                                                 <asp:BoundField DataField="flag" HeaderText="flag" />
                                                 <asp:BoundField DataField="pnstatus" HeaderText="pnstatus" />
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 10px">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>