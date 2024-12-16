<%@ Page Language="C#" Title="密码重置" MasterPageFile="~/MailTemplates/Mail.Master"
    Inherits="ESP.Mail.AspxMail,ESP.Core" %>

<%@ Assembly Name="ESP.Core" %>
<%@ Import Namespace="ESP.Framework.BusinessLogic" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <%
        string webSiteName = WebSiteManager.Get().WebSiteName;
        this.Title += " - " + webSiteName;

        string username = null;
        if (this.MailMessage != null && this.MailMessage.To.Count > 0)
        {
            username = this.MailMessage.To[0].DisplayName;
        }
        if (string.IsNullOrEmpty(username))
            username = "用户";

        object url = this.DataSource;
    %>
    <div>
        <p>
            尊敬的<% = username %>，
        </p>
        <br />
        <p>
            欢迎使用重置密码功能。要重置您的密码，请点击下面的链接，
        </p>
        <p>
            <a href="<%=url %>">点击此处，重置密码。</a>
        </p>
        <p>
            如果点击以上链接没有效果，请尝试将下面的 URL 复制到你的浏览器地址栏。</p>
        <p>
            <%=url %></p>
        <p>
            如果您并未进行过密码重置操作，请忽略本邮件。
        </p>
        <p>
            请不要直接回复本邮件地址，你的任何回复都会被忽略。
        </p>
    </div>
</asp:Content>
