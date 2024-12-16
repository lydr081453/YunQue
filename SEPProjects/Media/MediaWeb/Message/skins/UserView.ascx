<%@ Control Language="C#" AutoEventWireup="true" Inherits="Message_skins_UserView" Codebehind="UserView.ascx.cs" %>

<div style="width: 180px; height: 304px;">
            <asp:Image ID="imgUser" runat="server" Height="150px" Width="120px" />
            <br />
            <asp:Label ID="labUserNickName" Text="昵称：" runat="server"></asp:Label><asp:Label
                ID="labNickName" runat="server"></asp:Label><br />
            <asp:Label ID="labUserLevel" Text="级别：" runat="server"></asp:Label><asp:Label ID="labLevel"
                runat="server"></asp:Label><br />
            <asp:Label ID="labUserSex" Text="性别：" runat="server"></asp:Label><asp:Label ID="labSex"
                runat="server"></asp:Label><br />
            <asp:Label ID="labUserStatus" Text="状态：" runat="server"></asp:Label><asp:Label ID="labStatus"
                runat="server"></asp:Label><br />
</div>
