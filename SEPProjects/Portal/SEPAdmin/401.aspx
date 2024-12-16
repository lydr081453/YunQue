<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="401.aspx.cs" Inherits="SEPAdmin._01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>未授权的访问</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    您当前的登录不具备所请求的页面要求的授权级别，这可能是由于你没有登录后登录的帐户权限不足。
    <br />
    <br />
    点击<a href='<%= Request["loginUrl"] %>' style="color:Blue">登录</a>转到登录页面。
    </div>
    </form>
</body>
</html>
