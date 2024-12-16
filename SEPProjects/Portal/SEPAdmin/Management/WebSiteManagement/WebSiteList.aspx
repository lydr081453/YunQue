<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebSiteList.aspx.cs" Inherits="SEPAdmin.WebSiteManagement.WebSiteList"
    MasterPageFile="~/MainMaster.Master" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server" ID="up">
        <ContentTemplate>
            <asp:GridView runat="server" ID="gvWebSites" AutoGenerateColumns="False" DataKeyNames="WebSiteID,RowVersion"
                CellPadding="4" ForeColor="#333333" GridLines="None">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:BoundField DataField="WebSiteID" HeaderText="站点ID" />
                    <asp:BoundField DataField="WebSiteName" HeaderText="名称" />
                    <asp:BoundField DataField="Description" HeaderText="描述" />
                    <asp:BoundField DataField="UrlPrefix" HeaderText="根地址" />
                    <asp:BoundField DataField="FramePagePath" HeaderText="框架页路径" />
                    <asp:BoundField DataField="Ordinal" HeaderText="顺序" />
                    <asp:BoundField DataField="CreatorName" HeaderText="创建者" />
                    <asp:BoundField DataField="CreatedTime" HeaderText="创建时间" />
                    <asp:BoundField DataField="LastModifierName" HeaderText="最后修改者" />
                    <asp:TemplateField HeaderText="最后修改时间">
                        <ItemTemplate>
                            <%# EvalX("LastModifiedTime") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="编辑">
                        <ItemTemplate>
                            <asp:ImageButton runat="server" CommandName="EditItem" CommandArgument='<%# Eval("WebSiteID") %>'
                             Enabled='<%# IsEditable((ESP.Framework.Entity.WebSiteInfo)Container.DataItem) %>' OnCommand="EditItem_Command" ImageUrl="/images/edit.gif" />   
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            <%--    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#999999" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />--%>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
