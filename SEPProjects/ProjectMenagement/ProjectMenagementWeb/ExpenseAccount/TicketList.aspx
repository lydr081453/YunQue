<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeBehind="TicketList.aspx.cs" Inherits="FinanceWeb.ExpenseAccount.TicketList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript" src="../../public/js/DatePicker.js"></script>

    <script language="javascript" type="text/javascript">

        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }

        function selectAll(obj) {
            var theTable = obj.parentElement.parentElement.parentElement;
            var i;
            var j = obj.parentElement.cellIndex;

            for (i = 0; i < theTable.rows.length; i++) {
                var objCheckBox = theTable.rows[i].cells[j].firstChild;
                if (objCheckBox.checked != null) objCheckBox.checked = obj.checked;
            }
        }
        
        

    </script>

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
                公司选择:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:DropDownList runat="server" ID="ddlBranch" Style="width: auto">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                出票日:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtBeginDate" onkeyDown="return false; " Style="cursor: hand" runat="server"
                    onclick="setDate(this);" />--
                <asp:TextBox ID="txtEndDate" onkeyDown="return false; " Style="cursor: hand" runat="server"
                    onclick="setDate(this);" />
            </td>
            <td class="oddrow" style="width: 15%">
                供应商:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtSupplier" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click" />&nbsp;
                <asp:Button ID="btnSearchAll" runat="server" Text=" 检索全部 " CssClass="widebuttons"
                    OnClick="btnSearchAll_Click" />
            </td>
        </tr>
    </table>
    <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="ReturnID"
        OnRowCommand="gvG_RowCommand" OnRowDataBound="gvG_RowDataBound" PageSize="10"
        EmptyDataText="暂时没有相关记录" AllowPaging="false" Width="100%">
        <Columns>
            <asp:TemplateField ItemStyle-Width="5%">
                <ItemTemplate>
                    <asp:CheckBox ID="chkReturn" runat="server" OnCheckedChanged="chkReturn_OnCheckedChanged"
                        AutoPostBack="true" Checked="false" Text='' />
                </ItemTemplate>
                <HeaderTemplate>
                    &nbsp;<input id="CheckAll" type="checkbox" onclick="selectAll(this);" />all
                </HeaderTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ReturnID" HeaderText="ReturnID" ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="申请单号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                <ItemTemplate>
                    <asp:Label runat="server" ID="labReturncode" />
                </ItemTemplate>
            </asp:TemplateField>
             <asp:BoundField DataField="supplierName" HeaderText="供应商" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%"/>
            <asp:BoundField DataField="ProjectCode" HeaderText="项目号" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="8%" />
            <asp:BoundField DataField="RequestEmployeeName" HeaderText="申请人" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="8%" />
            <asp:BoundField DataField="DepartmentName" HeaderText="业务组别" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="8%" />
            <asp:TemplateField HeaderText="申请金额" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                <ItemTemplate>
                    <asp:Label runat="server" ID="labPrefee" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="出票日期" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                <ItemTemplate>
                    <asp:Label runat="server" ID="labDate" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                <ItemTemplate>
                    <asp:Label runat="server" ID="labState" Text='<%#Eval("ReturnStatus") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="待审批人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                <ItemTemplate>
                    <asp:Label runat="server" ID="labAuditor" Text='<%#Eval("ReturnStatus") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="出票状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%">
                <ItemTemplate>
                    <asp:Label runat="server" ID="labTicketUsed" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="打印" ItemStyle-Width="5%">
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:HyperLink runat="server" ID="hylPrint" ImageUrl="/images/Icon_PrintPo.gif"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作" ItemStyle-Width="5%">
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:HyperLink ID="hylEdit" runat="server" ImageUrl="/images/edit.gif" ToolTip="确认"
                        Width="4%"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="邮件" ItemStyle-Width="5%">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                    <asp:LinkButton ID="lnkMail" runat="server" Font-Underline="true" Text="<img title='发送邮件' src='/images/Icon_Sendmail.gif' border='0' />"
                        CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ReturnID")%>' CommandName="SendMail" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="审批状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                <ItemTemplate>
                    <a href="/project/ProjectWorkFlow.aspx?Type=oop&FlowID=<%#Eval("ReturnID") %>" target="_blank">
                        <img src="/images/AuditStatus.gif" border="0px;" title="审批状态"></a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                <asp:Button ID="btnBatchCreate" runat="server" Text=" 批次创建 " CssClass="widebuttons"
                    OnClick="btnBatchCreate_Click" />&nbsp;<div style="float: right; font-weight: bolder;">
                        <asp:Label runat="server" ID="lblTotal"></asp:Label>&nbsp;&nbsp;</div>
            </td>
        </tr>
    </table>
</asp:Content>
