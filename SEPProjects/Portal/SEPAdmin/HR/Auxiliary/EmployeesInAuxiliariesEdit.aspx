<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeesInAuxiliariesEdit.aspx.cs" Inherits="SEPAdmin.HR.Auxiliary.EmployeesInAuxiliariesEdit" MasterPageFile="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script language="javascript" type="text/javascript" src="../../public/js/jquery.js"></script>

    <script language="javascript" type="text/javascript" src="../../public/js/dialog.js"></script>

    <script language="javascript" type="text/javascript" src="../../public/js/jquery-ui.js"></script>

    <script language="javascript" type="text/javascript">
        function onPageSubmit() { };
        $(document).ready(function() {            
            
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

<table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                <asp:Label ID="labHeader" runat="server" />
            </td>
        </tr>        
    </table>
    <br />
    <table width="100%" class="tableForm">         
    <tr>
    <td>    
    <asp:UpdatePanel runat="server" ID="upUserList">
        <ContentTemplate>
            <asp:GridView ID="gvUser" runat="server" AutoGenerateColumns="False" DataKeyNames="userid,auxiliaryid"
                OnRowCommand="gvUser_RowCommand" BackColor="White" BorderColor="#3366CC"
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
                                Text="删除" />
                            <act:ConfirmButtonExtender ID="btnDeleteUser_ConfirmButtonExtender" runat="server"
                                TargetControlID="btnDeleteUser" ConfirmText="您是否要删除此条记录?" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>                
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
        <asp:HiddenField runat='server' ID="hidAux" />
        <input type="button" value="添加用户" id="btnAddUsersDialog" class="widebuttons" server-button-id="<% = btnAddUsers.UniqueID %>"
            server-hidden-id="<% = hdnAddUsers.ClientID %>" />
        <asp:Button runat='server' ID="btnBack" Text="返 回" CssClass="widebuttons" OnClick="btnBack_Click" />
    </div>
    <div style="width: 0px; height: 0px; overflow: hidden" class="oddrow">
        <div id="userList">
            <iframe src="UserList.aspx" id="userListFrame" height="90%" width="100%"></iframe>
        </div>
    </div>
    </td>
    </tr> 
        </table> 
</asp:Content>
