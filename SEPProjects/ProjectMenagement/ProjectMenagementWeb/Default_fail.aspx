<%@ Page Language="C#" Inherits="_Default" CodeBehind="Default_fail.aspx.cs" %>

<%@ Register Assembly="ExtExtenders" Namespace="ExtExtenders" TagPrefix="cc1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="stylesheet" type="text/css" href="/include/sinorca-screen.css" media="screen"
        title="Sinorca (screen)" />
    <style type="text/css">
        body
        {
            overflow: hidden;
            margin: 0;
            padding: 0;
            border: 0px none;
        }
        html, body
        {
            height: 100%;
        }
        form
        {
            width: 100%;
            height: 100%;
        }
    </style>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:Panel ID="pnlManager" runat="server" Width="100%">
        </asp:Panel>
        <iframe id="CenterPanel" name="CenterPanel" frameborder="0"
            height="100%" width="100%" runat="server" />
        <cc1:TreePane runat="server" ID="TreePane" AutoPostBack="true" Height="500px" 
            autoScroll="true">
            <TreeNodes>
            </TreeNodes>
        </cc1:TreePane>
        <iframe id="HeaderFrame" name="HeaderFrame" src="/include/header.aspx" frameborder="0" scrolling="no"
            height="100%" width="100%" runat="server" />
        <cc1:LayoutRegion ID="rgNorth" split="false" initialSize="90" TargetControlID="HeaderFrame"
            runat="server"></cc1:LayoutRegion>
        <cc1:LayoutRegion ID="rgCenter" TargetControlID="CenterPanel" runat="server" initialSize="200"
            minSize="200" maxSize="500" title="Financial System"></cc1:LayoutRegion>
        <cc1:LayoutRegion ID="rgWest" TargetControlID="TreePane" runat="server" initialSize="200"
            split="true" minSize="200" maxSize="500" collapsible="true" title="Navigation">
        </cc1:LayoutRegion>
        <cc1:LayoutManager ID="rgManager" TargetControlId="pnlManager" North="rgNorth" Center="rgCenter"
            West="rgWest" runat="server">
        </cc1:LayoutManager>
    </div>
    </form>
</body>
</html>
