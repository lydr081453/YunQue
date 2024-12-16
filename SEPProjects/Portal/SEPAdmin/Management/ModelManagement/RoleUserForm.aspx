<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleUserForm.aspx.cs" Inherits="SEPAdmin.RoleUserForm"
    MasterPageFile="~/MainMaster.Master" %>

<asp:Content ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript" src="../../public/js/jquery.js"></script>

    <script language="javascript" type="text/javascript" src="../../public/js/dialog.js"></script>

    <script language="javascript" type="text/javascript" src="../../public/js/jquery-ui.js"></script>

    <script language="javascript" type="text/javascript">
        function onPageSubmit() { };
        $(document).ready(function() {
            $("#btnAddDepartmentsDialog").click(function() {
                if (!document.getElementById("btnAddDepartmentsDialog").__sep_dialog) {
                    document.getElementById("btnAddDepartmentsDialog").__sep_dialog = $("#departmentTree").dialog({
                        modal: true, overlay: { opacity: 0.5, background: "black" },
                        height: 500, width: 800,
                        buttons: {
                            "取消": function() { $(this).dialog("close"); },
                            "确定": function() {
                                var buttonId = $("#btnAddDepartmentsDialog").attr("server-button-id");
                                var hiddenId = $("#btnAddDepartmentsDialog").attr("server-hidden-id");
                                var selects = document.getElementById("departmentTreeFrame").contentWindow.__sep_dialogReturnValue;
                                if (selects && selects != "") {
                                    $("#" + hiddenId).val(selects);
                                    //alert(document.getElementById(hiddenId).value);
                                    __doPostBack(buttonId, "");
                                }
                                $(this).dialog("close");
                            }
                        }
                    });
                }
                document.getElementById("btnAddDepartmentsDialog").__sep_dialog.dialog("open");
            });
//            $("#btnAddUsersDialog").click(function() {
//                dialog("添加部门", "iframe:UserList.aspx", 800, 500, "");
//            });
            
            $("#btnAddUsersDialog").click(function() {
            if (!document.getElementById("btnAddUsersDialog").__sep_dialog) {
            document.getElementById("btnAddUsersDialog").__sep_dialog = $("#userList").dialog({
            modal: true, overlay: { opacity: 0.5, background: "black" },
            height: 500, width: 800,
            buttons: {
            "取消": function() { $(this).dialog("close"); },
            "确定": function() {
            var buttonId = $("#btnAddUsersDialog").attr("server-button-id");
            var hiddenId = $("#btnAddUsersDialog").attr("server-hidden-id");
            var selects = document.getElementById("userListFrame").contentWindow.__sep_dialogReturnValue;
            if (selects && selects != "") {
            $("#" + hiddenId).val(selects);
            //alert(document.getElementById(hiddenId).value);
            __doPostBack(buttonId, "");
            }
            $(this).dialog("close");
            }
            }
            });
            }
            document.getElementById("btnAddUsersDialog").__sep_dialog.dialog("open");
            });
        });
    </script>

</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">
    <div class="heading">
        角色信息
    </div>
    <div>
        <hr />
    </div>
    <div>
        角色部门信息
    </div>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <asp:GridView ID="gvRoleDep" runat="server" AutoGenerateColumns="False" DataKeyNames="DepartmentID"
                OnRowCommand="gvRoleDep_RowCommand" BackColor="White"  BorderStyle="None"
                BorderWidth="1px" CellPadding="4">                
                <Columns>
                    <asp:TemplateField HeaderText="部门名">
                        <ItemTemplate>
                            <asp:Label ID="lblDepartmentName" runat="server" Text='<%# Eval("DepartmentName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="部门描述">
                        <ItemTemplate>
                            <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnDeleteUser" AlternateText="删除" runat="server" CommandName="DeleteUser"
                                CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' ImageUrl="/images/disable.gif"
                                Text="删除" />
                            <act:ConfirmButtonExtender ID="btnDeleteUser_ConfirmButtonExtender" runat="server"
                                TargetControlID="btnDeleteUser" ConfirmText="您是否要删除此条记录?" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <%--<FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />--%>
            </asp:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnAddDepartments" />
        </Triggers>
    </asp:UpdatePanel>
    <div>
        <div style="display: none">
            <asp:HiddenField runat='server' ID="hdnNewDepartments" />
            <asp:Button runat='server' ID="btnAddDepartments" Text="不要点击" OnCommand="btnAddDepartments_Command" />
        </div>
        <input type="button" value="添加部门" id="btnAddDepartmentsDialog" server-button-id="<% = btnAddDepartments.UniqueID %>"
            server-hidden-id="<% = hdnNewDepartments.ClientID %>" />
    </div>
    <div>
        <hr />
    </div>
    <div>
        角色用户信息
    </div>
    <asp:UpdatePanel runat="server" ID="upUserList">
        <ContentTemplate>
            <asp:GridView ID="gvRoleUser" runat="server" AutoGenerateColumns="False" DataKeyNames="UserID"
                OnRowCommand="gvRoleUser_RowCommand" BackColor="White" BorderColor="#3366CC"
                BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <RowStyle BackColor="White" ForeColor="#003399" />
                <Columns>
                    <asp:TemplateField HeaderText="用户名">
                        <ItemTemplate>
                            <asp:Label ID="lblUsername" runat="server" Text='<%# Eval("Username") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="中文姓名">
                        <ItemTemplate>
                            <asp:Label ID="lblFullNameCN" runat="server" Text='<%# Eval("FullNameCN") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="英文姓名">
                        <ItemTemplate>
                            <asp:Label ID="lblFullNameEN" runat="server" Text='<%# Eval("FullNameEN") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="电子邮箱">
                        <ItemTemplate>
                            <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnDeleteUser" AlternateText="删除" runat="server" CommandName="DeleteUser"
                                CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' ImageUrl="/images/disable.gif"
                                Text="删除" Enabled='<%# 1 != (int)Eval("UserID") || 1 != this.RoleID %>' />
                            <act:ConfirmButtonExtender ID="btnDeleteUser_ConfirmButtonExtender" runat="server"
                                TargetControlID="btnDeleteUser" ConfirmText="您是否要删除此条记录?" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <%--<FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />--%>
            </asp:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnAddUsers" />
        </Triggers>
    </asp:UpdatePanel>
    <div>
        <div style="display: none">
            <asp:HiddenField runat='server' ID="hdnAddUsers" />
            <asp:Button runat='server' ID="btnAddUsers" Text="不要点击" OnCommand="btnAddUsers_Command" />
        </div>
        <input type="button" value="添加用户" id="btnAddUsersDialog" server-button-id="<% = btnAddUsers.UniqueID %>"
            server-hidden-id="<% = hdnAddUsers.ClientID %>" />
    </div>
    <div style="width: 0px; height: 0px; overflow: hidden">
        <div id="departmentTree">
            <iframe src="DepartmentTree.aspx" id="departmentTreeFrame" height="90%" width="100%">
            </iframe>
        </div>
    </div>
    <div style="width: 0px; height: 0px; overflow: hidden">
        <div id="userList">
            <iframe src="UserList.aspx" id="userListFrame" height="90%" width="100%"></iframe>
        </div>
    </div>
</asp:Content>
