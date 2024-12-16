<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HeadAccountEdit.aspx.cs"
    MasterPageFile="~/MasterPage.master" Inherits="SEPAdmin.HR.Join.HeadAccountEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="/HR/Employees/js/jquery-1.2.6.js"></script>

    <script type="text/javascript" src="/HR/Employees/js/artDialog.js?skin=default"></script>

    <script type="text/javascript" src="/HR/Employees/js/iframeTools.js"></script>

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script type="text/javascript">
        function btnClick() {
            art.dialog.open('/HR/Employees/DepartmentsTree.aspx?principal=1', { title: '部门列表', width: 600, height: 400, background: '#BFBFBF', opacity: 0.7, lock: true });
        }
        function createha() {
            var deptid = document.getElementById("<%=hidGroupId.ClientID %>").value;
            var talentId = "<%=Request["talentId"]%>";
            if (deptid == "") {
                alert("请选择部门！");
                return false;
            }
            if (talentId != "") {
                window.location = "HeadAccountCreate.aspx?deptid=" + deptid+"&talentId="+talentId;
            }
            else {
                window.location = "HeadAccountCreate.aspx?deptid=" + deptid;
            }
        }
    </script>

    <table width="100%" class="tableForm" style="margin: 20px 0px 220px 0px; border-color: #CED4E7;">
        <tr>
            <td class="oddrow" style="width: 10%">
                所属公司:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:TextBox ID="txtJob_CompanyName" runat="server" />
                <font color="red">*</font>
                <asp:HiddenField ID="hidCompanyId" runat="server" />
            </td>
            <td class="oddrow" style="width: 10%">
                部门:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:TextBox ID="txtJob_DepartmentName" runat="server" onkeyDown="return false; " />
                <asp:HiddenField ID="hidDepartmentID" runat="server" />
            </td>
            <td class="oddrow" style="width: 10%">
                组别:
            </td>
            <td class="oddrow-l" style="width: 20%">
                <asp:TextBox ID="txtJob_GroupName" runat="server" onkeyDown="return false; " />
                <asp:HiddenField ID="hidGroupId" runat="server" />
                <input type="button" id="btndepartment" class="widebuttons" value=" 选择部门 " onclick="btnClick();" />
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <asp:Button runat="server" ID="btnCheck" Text=" 检索该部门信息 " OnClick="btnCheck_Click" />&nbsp;&nbsp;
               
            </td>
        </tr>
         <tr>
            <td colspan="6" class="title">
            现有员工
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False"   OnRowDataBound="gvList_RowDataBound" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="code" HeaderText="工号" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="fullnamecn" HeaderText="员工中文姓名" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="fullnameen" HeaderText="员工英文姓名" ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="职位" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Repeater ID="Job" runat="server">
                                    <ItemTemplate>
                                        <%# Eval("DepartmentPositionName").ToString()%><br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="InternalEmail" HeaderText="邮箱" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="120" />
                        <asp:BoundField DataField="MobilePhone" HeaderText="手机" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="120" />
                        <asp:BoundField DataField="Phone1" HeaderText="分机" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="120" />
                        <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# ESP.HumanResource.Common.Status.Employee_StatusName[int.Parse(Eval("Status").ToString())]%><br />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Phone1" HeaderText="分机" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="120" />
                        <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                            <asp:Label runat="server" ID="lblReplace"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="6">
            已申请Headcount
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <asp:GridView ID="gvHeadAccount" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvHeadAccount_RowDataBound" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="Position" HeaderText="职务" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="LevelName" HeaderText="职级" ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="工资级别" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblSalary"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CreateDate" HeaderText="创建日期" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="120" />
                             <asp:TemplateField HeaderText="员工" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblUsername"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# ESP.HumanResource.Common.Status.HeadAccountStatus_Names[int.Parse(Eval("Status").ToString())]%><br />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr runat="server" id="trNew" visible="false">
        <td colspan="6">
         <input type="button" onclick="createha();" class="button_org" value=" New Headcount " />
        </td>
        </tr>
    </table>
</asp:Content>
