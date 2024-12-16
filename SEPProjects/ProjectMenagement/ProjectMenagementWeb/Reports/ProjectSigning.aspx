<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectSigning.aspx.cs"
    MasterPageFile="~/MasterPage.master" Inherits="Reports_ProjectSigning" EnableEventValidation="false" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="/public/css/buttonLoading.css" rel="stylesheet" />
    <script language="javascript" src="../../public/js/DatePicker.js"></script>
    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>
    <script language="javascript" type="text/javascript">
        
            function setLoading(button) {
                button.classList.add('loading');
                button.classList.add('disabled');
               
                setTimeout(function () {
                    button.classList.remove('loading');
                    button.classList.remove('disabled');
                }, 2000);
            }

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
                                           <input type="hidden" id="hidBrancName" runat="server" />
                                    </td>
                                       <td class="oddrow" style="width: 15%"></td>
                                       <td class="oddrow-l" style="width: 35%"></td>
                                </tr>
                                <tr>
                                    <td class="oddrow-l" colspan="4">
                                        <asp:Button ID="btnSearch" runat="server" Text=" 检索 " OnClientClick="return CheckInput();"
                                            CssClass="widebuttons" OnClick="btnSearch_Click" />&nbsp;
                                        <asp:Button ID="btnSearchAll" runat="server" Text=" 检索全部 " CssClass="widebuttons"
                                            OnClick="btnSearchAll_Click" />&nbsp;
                                        <asp:Button ID="btnExport" runat="server" Text=" 导出 " CssClass="widebuttons" OnClick="btnExport_Click" OnClientClick="setLoading(this);"
                                            Style="width: 50px" />&nbsp;
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
                                        项目号列表
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow">
                                        <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="ProjectID"
                                            OnPageIndexChanging="gvG_PageIndexChanging" OnRowDataBound="gvG_RowDataBound"
                                            PageSize="10" EmptyDataText="暂时没有相关记录" PagerSettings-Mode="NumericFirstLast"
                                            AllowPaging="true" Width="100%">
                                            <Columns>
                                                <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Step" HeaderText="Step" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="SerialCode" HeaderText="流水号" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="7%" />
                                                <asp:BoundField DataField="ProjectCode" HeaderText="项目号" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="10%" />
                                                <asp:BoundField DataField="BusinessDescription" HeaderText="项目名称" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="10%" />
                                                  <asp:TemplateField HeaderText="负责人" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="5%" >
                                                    <ItemTemplate>
                                                    <asp:Label ID="lblName" runat="server" CssClass="userLabel" Text='<%# Eval("ApplicantEmployeeName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    </asp:TemplateField>
                                                <asp:BoundField DataField="GroupName" HeaderText="业务组别" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="8%" />
                                                <asp:BoundField DataField="CreateDate" HeaderText="申请日期" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="8%" />
                                                <asp:BoundField DataField="BusinessTypeName" HeaderText="业务类型" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="8%" />
                                                <asp:BoundField DataField="ProjectTypeName" HeaderText="项目类型" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="8%" />
                                                <asp:BoundField DataField="ContractStatusName" HeaderText="合同状态" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="8%" />
                                                <asp:TemplateField HeaderText="申请状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="labState" Text='<%#Eval("Status") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
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
