<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemEdit.aspx.cs" Inherits="PurchaseWeb.Purchase.Version.ItemEdit"  Theme=""%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <link href="/public/css/newcss.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>


    <script src="/public/js/DatePicker.js"></script>
    <table width="90%" border="0" align="center" cellpadding="0" cellspacing="0" class="frameTable">
        <tr>
            <td>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" background="/images/tp_4.jpg">
        <tr>
            <td background="/images/tp_3.jpg" style="background-repeat: no-repeat; padding-left: 25px;
                font-weight: bold;">
                <asp:Literal ID="lbTitle" runat="server" Text="添加"></asp:Literal>报价项</td>
            <td width="25%" height="32" background="/images/tp_5.jpg" style="background-repeat: no-repeat;
                background-position: right;">
            </td>
        </tr>
    </table>
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="gvMain">
                    <tr>
                        <td style="width:20%; text-align:right;height:35px; background-color:#F2F4F6;">
                            字段名称：</td>
                        <td style="width:80%;text-align:left;height:35px;">
                           <asp:TextBox ID="t_ID" runat="server" Width="365px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width:20%; text-align:right;height:35px; background-color:#F2F4F6;">
                            中文名称：</td>
                        <td style="width:80%;text-align:left;height:35px;">
                            <asp:TextBox ID="t_cnDescription" runat="server" Width="365px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:20%; text-align:right;height:35px; background-color:#F2F4F6;">
                            英文名称：</td>
                        <td style="width:80%;text-align:left;height:35px;">
                            <asp:TextBox ID="t_enDescription" runat="server" Width="365px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:20%; text-align:right;height:35px; background-color:#F2F4F6;">
                            字段类型：</td>
                        <td style="width:80%;text-align:left;height:35px;">
                            <asp:RadioButtonList ID="t_Type" runat="server" RepeatDirection="Horizontal" 
                                RepeatLayout="Flow">
                                <asp:ListItem Selected="True" Value="0">普通文字</asp:ListItem>
                                <asp:ListItem Value="1">大文本</asp:ListItem>
                                <asp:ListItem Value="2">数字</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:20%; text-align:right;height:35px; background-color:#F2F4F6;">
                            展示控件：</td>
                        <td style="width:80%;text-align:left;height:35px;">
                            <asp:DropDownList ID="t_Control" runat="server">
                                <asp:ListItem Selected="True" Value="TextBox">文本框</asp:ListItem>
                                <asp:ListItem Value="FileUpload">上传文件框</asp:ListItem>
                                <asp:ListItem Value="CheckBox">选择框</asp:ListItem>
                                <asp:ListItem Value="DropDownList">下拉菜单</asp:ListItem>
                                <asp:ListItem Value="RadioButtonList">单选框</asp:ListItem>
                                <asp:ListItem Value="CheckBoxList">多选框</asp:ListItem>
                                <asp:ListItem Value="DateTime">日期选择框</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:20%; text-align:right;height:35px; background-color:#F2F4F6;">
                            选项设置：</td>
                        <td style="width:80%;text-align:left;height:35px;">
                            <asp:RadioButtonList ID="t_Option" runat="server" 
                                RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Selected="True" Value="None">无</asp:ListItem>
                                <asp:ListItem Value="Single">单选</asp:ListItem>
                                <asp:ListItem Value="Multiple">多选</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:20%; text-align:right;height:35px; background-color:#F2F4F6;">
                            将此项目添加到：</td>
                        <td style="width:80%;text-align:left;height:35px;">
                            <asp:RadioButtonList ID="ddlSite" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">当前项目之前</asp:ListItem>
                                <asp:ListItem Value="0" Selected="True">当前项目之后</asp:ListItem>
                              </asp:RadioButtonList>
                        </td>
                    </tr>
                    
                    
                    </table>
                    
 
                
            </td>
        </tr>
    </table>
    <table width="90%" align="center" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td width="1%" align="right" height="10px">
                &nbsp;
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td width="1%" align="right">
                &nbsp;
            </td>
            <td align="center">
                <asp:Button ID="btnSave" Text=" 保 存" CssClass="input_button_three" runat="server"
                    OnClick="btnSave_Click" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCancel" Text=" 取 消" CssClass="input_button_three" runat="server"
                    OnClientClick="parent.$('#floatBoxBg').hide();parent.$('#floatBox').hide();" />
                
                <br />
                <br />
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
