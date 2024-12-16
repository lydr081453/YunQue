<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RolePermissionModify.aspx.cs"
    MasterPageFile="~/MainMaster.Master" Inherits="SEPAdmin.RolePermissionModify" %>

<asp:Content ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">

        function sep_CascadeCheckBox(ele, idprefix, selectedKeys) {
            if (ele._sep_CascadeCheckBox)
                return;

            ele._sep_CascadeCheckBox = this;

            this._key = ele.value;
            this._element = ele;
            this._children = [];
            this._parentKey = ele["parent-module-key"];
            this._parent = null;
            this._isChecked = ele.checked;

            var parentNodeId = idprefix + this._parentKey;
            var parentEle = document.getElementById(parentNodeId);
            if (parentEle) {
                var parent = parentEle._sep_CascadeCheckBox;
                if (!parent) {
                    new sep_CascadeCheckBox(parentEle, idprefix);
                    parent = parentEle._sep_CascadeCheckBox;
                }
                this._parent = parent;
                parent._children.push(this);
            }

            this._getAllChildrenStatus = function() {
                var allChecked = true;
                var allUnchecked = true;
                for (var i = 0; i < this._children.length; i++) {
                    allChecked &= this._children[i]._isChecked;
                    allUnchecked &= !this._children[i]._isChecked;
                }
                return (allChecked ? 2 : 0) | (allUnchecked ? 1 : 0);
            }
            this.isAllChildrenChecked = function() {
                return (this._getAllChildrenStatus() & 2) == 2;
            }
            this.isAllChildrenUnchecked = function() {
                return (this._getAllChildrenStatus() & 1) == 1;
            }


            this.checkChange = function(isChecked, isUp, isDown) {
                this._isChecked = isChecked;
                this._element.checked = isChecked;
                if (isDown) {
                    for (var i = 0; i < this._children.length; i++) {
                        this._children[i].checkChange(isChecked, false, true);
                    }
                }
                if (isUp) {
                    if (this._parent) {
                        if (isChecked || this._parent.isAllChildrenUnchecked())
                            this._parent.checkChange(isChecked, true, false);
                    }
                }
            }

            ele.onclick = function() {
                this._sep_CascadeCheckBox.checkChange(this.checked, true, true);
            }

            //var selectedKeys = new String();
            if (selectedKeys.indexOf("," + ele.value + ",") >= 0) {
                this.checkChange(true, true, false);
            }
        }

        function sep_RolePermissions_wrapperCheckboxes(ckbname, idprefix, selectedKeysEleId) {
            var checkboxes = document.getElementsByName(ckbname);
            var selectedKeysEle = document.getElementById(selectedKeysEleId);
            var selectedKeys = selectedKeysEle.value;
            for (var i = 0; i < checkboxes.length; i++) {
                new sep_CascadeCheckBox(checkboxes[i], idprefix, selectedKeys);
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">

    <script language="javascript" src="/js/RolePermission.js" type="text/javascript"></script>

    <script type="text/javascript" src="/js/UserDepartment.js" language="javascript"></script>

    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <div>
                <table>
                    <tr>
                        <td>
                            详细信息
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HiddenField runat="server" ID="hdnSelectedKeys" />
                            <yyc:SmartTreeView ID="stv1" runat="server" AllowCascadeCheckbox="True" ImageSet="Msdn"
                                ShowLines="true" Style="border-right: 0px inset; border-top: 0px inset; overflow: auto;
                                border-left: 0px inset; width: 100%; border-bottom: 0px inset; height: 100%;
                                background-color: white" NodeIndent="20">
                                <ParentNodeStyle Font-Bold="False" />
                                <HoverNodeStyle BackColor="#ffffff" BorderColor="#888888" BorderStyle="None" Font-Underline="False" />
                                <SelectedNodeStyle BackColor="#ffffff" BorderColor="#888888" BorderStyle="None" Font-Underline="False" />
                                <NodeStyle Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" HorizontalPadding="2px"
                                    NodeSpacing="1px" VerticalPadding="2px" />
                            </yyc:SmartTreeView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="btnSave" Text="保存" OnClick="btnSave_Click" />
                            <asp:Button runat="server" ID="btnCancel" Text="取消" OnClick="btnCancel_Click" />
                            <asp:Label runat="server" ID="lblMsg" EnableViewState="false" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
