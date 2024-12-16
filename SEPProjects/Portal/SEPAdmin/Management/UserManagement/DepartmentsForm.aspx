<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentsForm.aspx.cs" MasterPageFile="~/MainMaster.Master"
    Inherits="SEPAdmin.UserManagement.DepartmentsForm" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">

    <script language="javascript" type="text/javascript" src="../../public/js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../../public/js/jquery-ui.js"></script>
    <script type="text/javascript">
        function openDialog(obj, ct) {
            $("#hidct").val(ct);
            obj.__sep_dialog = $("#userList").dialog({
                modal: true, overlay: { opacity: 0.5, background: "black" },
                height: 300, width: 800,
                buttons: {
                    "取消": function () { $(this).dialog("close"); },
                    "确定": function () {
                        var selects = document.getElementById("userListFrame").contentWindow.__sep_dialogReturnValue;
                        if (selects && selects != "") {
                            var str = selects.substring(0, selects.indexOf(","));
                            $("#ctl00_MainContent_txt" + $("#hidct").val() + "Name").val(str.split('-')[1]);
                            $("#ctl00_MainContent_hid" + $("#hidct").val() + "Id").val(str.split('-')[0]);
                        }
                        $(this).dialog("close");
                    }
                }
            });
            obj.__sep_dialog.dialog("open");
        }
    </script>
    <div>
        <input type="hidden" id="hidct" />
        <table width="100%" class="tableForm">
            <tr>
                <td colspan="4">部门信息</td>
            </tr>
            <tr>
                <td class="oddrow" width="30%">编号：</td>
                <td class="oddrow-l" width="70%">
                    <asp:Label ID="lblID" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="oddrow">名称：</td>
                <td class="oddrow-l">
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtName"
                        Display="Dynamic" ErrorMessage="名称为必填"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="oddrow">描述：</td>
                <td class="oddrow-l">
                    <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="oddrow">序号：</td>
                <td class="oddrow-l">
                    <asp:TextBox ID="txtSort" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator
                        ID="RegularExpressionValidator17" runat="server" ErrorMessage="序号错误，请填写数字" ControlToValidate="txtSort"
                        Display="Dynamic" ValidationExpression="^\d$"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSort"
                        Display="Dynamic" ErrorMessage="序号为必填"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="oddrow">状态：</td>
                <td class="oddrow-l">
                    <asp:DropDownList ID="ddlStatus" runat="server">
                        <asp:ListItem Text="启用" Value="0" />
                        <asp:ListItem Text="停用" Value="1" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="oddrow">部门类别：</td>
                <td class="oddrow-l">
                    <asp:DropDownList ID="ddlDepType" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnAddNewChild" runat="server" Text="新增子节点" CssClass="widebuttons" OnClick="btnAddNewChild_Click" />
                    <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="widebuttons" OnClick="btnSave_Click" />
                    <asp:Button ID="btnDelete" runat="server" Text="删除" OnClientClick="return confirm('确定删除吗？');" CssClass="widebuttons" OnClick="btnDelete_Click" />
                </td>
            </tr>
            <tr>
                <td style="height: 30px"></td>
            </tr>
        </table>

        <br />
        <table class="tableForm" id="tab1" runat="server" width="100%">
            <tr>
                <td colspan="4">审批人员信息</td>
            </tr>
            <tr>
                <td class="oddrow" width="20%">总监：
                </td>
                <td class="oddrow-l" width="30%">
                    <asp:TextBox ID="txtdirectorName" runat="server" /><asp:HiddenField ID="hiddirectorId" Value="0" runat="server" />
                    &nbsp;<input type="button" value="选择" class="widebuttons" onclick="openDialog(this, 'director');" />
                </td>
                <td class="oddrow" width="20%">总经理：</td>
                <td class="oddrow-l">
                    <asp:TextBox ID="txtmanagerName" runat="server" /><asp:HiddenField ID="hidmanagerId" Value="0" runat="server" />
                    &nbsp;<input type="button" value="选择" class="widebuttons" onclick="openDialog(this, 'manager');" />
                </td>
            </tr>
            <tr>
                <td class="oddrow" width="20%">CEO：
                </td>
                <td class="oddrow-l" width="30%">
                    <asp:TextBox ID="txtceoName" runat="server" /><asp:HiddenField ID="hidceoId" Value="0" runat="server" />
                    &nbsp;<input type="button" value="选择" class="widebuttons" onclick="openDialog(this, 'ceo');" />
                </td>
                <td class="oddrow" width="20%">HR：</td>
                <td class="oddrow-l">
                    <asp:TextBox ID="txthrName" runat="server" /><asp:HiddenField ID="hidhrId" Value="0" runat="server" />
                    &nbsp;<input type="button" value="选择" class="widebuttons" onclick="openDialog(this, 'hr');" />
                </td>
            </tr>
            <tr>
                <td class="oddrow" width="20%">月度考勤审批人：
                </td>
                <td class="oddrow-l" width="30%">
                    <asp:TextBox ID="txthrattendanceName" runat="server" /><asp:HiddenField ID="hidhrattendanceId" Value="0" runat="server" />
                    &nbsp;<input type="button" value="选择" class="widebuttons" onclick="openDialog(this, 'hrattendance');" />
                </td>
                <td class="oddrow" width="20%">离职行政审批人：</td>
                <td class="oddrow-l">
                    <asp:TextBox ID="txtdimissionadauditorName" runat="server" /><asp:HiddenField ID="hiddimissionadauditorId" Value="0" runat="server" />
                    &nbsp;<input type="button" value="选择" class="widebuttons" onclick="openDialog(this, 'dimissionadauditor');" />
                </td>
            </tr>
            <tr>
                <td class="oddrow" width="20%">离职总监审批人：
                </td>
                <td class="oddrow-l" width="30%">
                    <asp:TextBox ID="txtdimissionDirectorName" runat="server" /><asp:HiddenField ID="hiddimissionDirectorId" Value="0" runat="server" />
                    &nbsp;<input type="button" value="选择" class="widebuttons" onclick="openDialog(this, 'dimissionDirector');" />
                </td>
                <td class="oddrow" width="20%">离职总经理审批人：</td>
                <td class="oddrow-l">
                    <asp:TextBox ID="txtdimissionManagerName" runat="server" /><asp:HiddenField ID="hiddimissionManagerId" Value="0" runat="server" />
                    &nbsp;<input type="button" value="选择" class="widebuttons" onclick="openDialog(this, 'dimissionManager');" />
                </td>
            </tr>
            <tr>
                <td class="oddrow" width="20%">HeadCount初审人：
                </td>
                <td class="oddrow-l" width="30%">
                    <asp:TextBox ID="txtheadCountAuditorName" runat="server" /><asp:HiddenField ID="hidheadCountAuditorId" Value="0" runat="server" />
                    &nbsp;<input type="button" value="选择" class="widebuttons" onclick="openDialog(this, 'headCountAuditor');" />
                </td>
                <td class="oddrow" width="20%">HeadCount总监级审核人：</td>
                <td class="oddrow-l">
                    <asp:TextBox ID="txtheadCountDirectorName" runat="server" /><asp:HiddenField ID="hidheadCountDirectorId" Value="0" runat="server" />
                    &nbsp;<input type="button" value="选择" class="widebuttons" onclick="openDialog(this, 'headCountDirector');" />
                </td>
            </tr>

            <tr>
                <td class="oddrow" width="20%">采购物料审核人：
                </td>
                <td class="oddrow-l" width="30%">
                    <asp:TextBox ID="txtpurchaseAuditorName" runat="server" /><asp:HiddenField ID="hidpurchaseAuditorId" Value="0" runat="server" />
                    &nbsp;<input type="button" value="选择" class="widebuttons" onclick="openDialog(this, 'purchaseAuditor');" />
                </td>
                <td class="oddrow" width="20%">采购附加收货人：</td>
                <td class="oddrow-l">
                    <asp:TextBox ID="txtappendReceiverName" runat="server" /><asp:HiddenField ID="hidappendReceiverId" Value="0" runat="server" />
                    &nbsp;<input type="button" value="选择" class="widebuttons" onclick="openDialog(this, 'appendReceiver');" />
                </td>
            </tr>
            <tr>
                <td class="oddrow" width="20%">采购总监级审核人：
                </td>
                <td class="oddrow-l" width="30%">
                    <asp:TextBox ID="txtpurchaseDirectorName" runat="server" /><asp:HiddenField ID="hidpurchaseDirectorId" Value="0" runat="server" />
                    &nbsp;<input type="button" value="选择" class="widebuttons" onclick="openDialog(this, 'purchaseDirector');" />
                </td>
                <td class="oddrow" width="20%">总监级审核人金额权限：</td>
                <td class="oddrow-l">
                    <asp:TextBox ID="txtdirectorAmount" Text="0" runat="server" /></td>
            </tr>
            <tr>
                <td class="oddrow" width="20%">总经理级审核人金额权限：
                </td>
                <td class="oddrow-l" width="30%">
                    <asp:TextBox ID="txtmanagerAmount" Text="0" runat="server" />
                </td>
                <td class="oddrow" width="20%">CEO级审核人金额权限：</td>
                <td class="oddrow-l">
                    <asp:TextBox ID="txtCEOAmount" Text="0" runat="server" /></td>
            </tr>
        </table>
        <table width="80%">
            <tr>
                <td style="height: 30px"></td>
            </tr>
            <tr>
                <td class="oddrow-l">
                    <li>
                        <asp:HyperLink ID="hypDepPost" Style="color: Black" runat="server" Text="新增职位"></asp:HyperLink></li>
                </td>
            </tr>
            <tr>
                <td style="height: 30px"></td>
            </tr>
            <tr>
                <td class="oddrow-l">部门所属职位
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvView" runat="server" AutoGenerateColumns="false" DataKeyNames="DepartmentPositionID" OnRowDataBound="gvView_RowDataBound" Font-Size="16px">
                        <Columns>
                            <asp:TemplateField HeaderText="职位ID">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hypDepID" runat="server" Text='<%# Eval("DepartmentPositionID") %>' NavigateUrl='DepartmentPositionForm.aspx?depposid=<%# Eval("DepartmentPositionID") %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="职位名" DataField="DepartmentPositionName" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="描述" DataField="Description" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="编辑">
                                <ItemTemplate>
                                    <asp:ImageButton ID="lblEdit" OnClick="btnEdit_Click" AlternateText="编辑" runat="server" ImageUrl="/images/edit.gif"
                                        Text="编辑" Enabled='<%# 1 != (int)Eval("DepartmentPositionID") %>'></asp:ImageButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="删除">
                                <ItemTemplate>
                                    <asp:ImageButton ID="lblDel" OnClick="btnDel_Click" AlternateText="删除" runat="server" ImageUrl="/images/disable.gif"
                                        Text="删除" Enabled='<%# 1 != (int)Eval("DepartmentPositionID") %>'></asp:ImageButton>
                                    <act:ConfirmButtonExtender ID="btnDelete_ConfirmButtonExtender" runat="server" TargetControlID="lblDel"
                                        ConfirmText="您是否要删除此条记录?">
                                    </act:ConfirmButtonExtender>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    <div style="width: 0px; height: 0px; overflow: hidden" class="oddrow">
        <div id="userList">
            <iframe src="UserList.aspx" id="userListFrame" height="90%" width="100%" style="background-color:white;"></iframe>
        </div>
    </div>
</asp:Content>
