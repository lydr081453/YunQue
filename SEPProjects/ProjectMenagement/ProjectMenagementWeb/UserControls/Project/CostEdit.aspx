<%@ Page Language="C#" AutoEventWireup="true" Inherits="UserControls_Project_CostEdit" Codebehind="CostEdit.aspx.cs" %>

<html>
<head runat="server">
    <title></title>
    <style type="text/css">
        .Grid
        {
            background-image: url(/images/grid_bg.gif);
            background-color: #FFFFFF;
            border: 1px solid #C0C0C0;
            border-top-width: 0px;
            border-left-width: 0px;
            cursor: pointer;
        }
        .GridHeader
        {
            background-image: url(/images/header_rowBg.gif);
            background-color: #8988A5;
            border: 1px solid #57566F;
            height: 28px;
            padding-left: 3px;
            cursor: default;
            text-align: center;
            font-size: 14px;
            font-style: inherit;
            font-weight: bolder;
        }
        .ItemStyle
        {
            border-bottom-style: solid;
            border-left-style: none;
            border-right-style: solid;
            border-top-style: none;
            border-color: #E6E6EE;
            text-align: center;
            border-width: 1px;
        }
        .FirstCell
        {
            background-color: #D6D7E1;
            background-image: url(/images/headingSelectorCell_bg.gif);
            border-width: 0px;
            border-right: 1px solid #FFFFFF;
        }
        .AlternatingRow
        {
            background-color: #fcfcfc;
            border-color: white;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%">
        <tr>
            <td>
                <asp:DataGrid ID="gvEdit" runat="server" AutoGenerateColumns="False" DataKeyField="CostTypeID" CssClass="Grid" 
                    OnItemCreated="gvEdit_ItemCreated" OnItemDataBound="gvEdit_ItemDataBound">
                    <ItemStyle CssClass="ItemStyle" />
                    <HeaderStyle CssClass="GridHeader" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <AlternatingItemStyle BorderColor="" BackColor="#fcfcfc"></AlternatingItemStyle>
                    <Columns>
                        <asp:BoundColumn DataField="CostTypeID" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="ProjectID" Visible="false"></asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="选择" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle CssClass="FirstCell" Height="20px" />
                            <ItemTemplate>
                                <input type="hidden" id="hidSelected" runat="server" value='' name="SelectedID" />
                                <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="采购物料" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle CssClass="ItemStyle" Height="20px" />
                            <ItemTemplate>
                                <asp:Label ID="lblMaterial" runat="server" Text='<%#Eval("Description") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="成本金额">
                            <ItemStyle CssClass="ItemStyle" Height="20px" />
                            <ItemTemplate>
                                <asp:Label ID="lblCost" runat="server">
                                </asp:Label>
                                <asp:TextBox ID="txtCost" runat="server" Visible="False">
                                </asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="备注">
                            <ItemStyle CssClass="ItemStyle" Height="20px" />
                            <ItemTemplate>
                                <asp:Label ID="lblRemark" runat="server">
                                </asp:Label>
                                <asp:TextBox ID="txtRemark" runat="server" Visible="False">
                                </asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" />
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
