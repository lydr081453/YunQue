<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="SEPAdmin.Management.WebSiteManagement.Settings" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView runat="server" ID="gvSettings" AutoGenerateColumns="false">
            <Columns>
                <asp:CheckBoxField HeaderText="可重载" DataField="IsOverridable" />
                <asp:CheckBoxField HeaderText="公共设置" DataField="IsCommonSetting"  />
                <asp:BoundField HeaderText="名称" DataField="SettingName"  />
                <asp:BoundField HeaderText="值" DataField="SettingValue"  />
                <asp:BoundField HeaderText="类型" DataField="ValueType"  />
                <asp:BoundField HeaderText="描述" DataField="Description"  />
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
