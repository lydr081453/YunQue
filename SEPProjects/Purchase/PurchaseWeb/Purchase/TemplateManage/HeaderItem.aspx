<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HeaderItem.aspx.cs" Inherits="PurchaseWeb.Purchase.TemplateManage.HeaderItem" Theme=""%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <link href="/public/css/newcss.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
<script>
function closeAndSave() {
    parent.$('#floatBoxBg').hide(); parent.$('#floatBox').hide();
}
</script>   
                        
<table width="90%" border="0" align="center" cellpadding="0" cellspacing="0" class="frameTable">
        <tr>
            <td>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" background="/images/tp_4.jpg">
        <tr>
            <td background="/images/tp_3.jpg" style="background-repeat: no-repeat; padding-left: 25px;
                font-weight: bold;">
                报价模版属性</td>
            <td width="5%" height="32" background="/images/tp_5.jpg" style="background-repeat: no-repeat;
                background-position: right;">
                &nbsp;
            </td>
        </tr>
    </table>
                
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="gvMain">
                    <tr>
                        <td style="width:30%; text-align:right;height:35px; background-color:#F2F4F6;">
                            属性名称：</td>
                        <td style="width:70%;text-align:left;height:35px;">
                            <asp:TextBox ID="txtName" runat="server" Width="296px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:30%; text-align:right;height:35px; background-color:#F2F4F6;">
                            属性类型：</td>
                        <td style="width:70%;text-align:left;height:35px;">
                            <asp:RadioButtonList ID="ddlType" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="">文字型</asp:ListItem>
                                <asp:ListItem Value="integral">数字型</asp:ListItem>
                                <asp:ListItem Value="datetime">日期型</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:30%; text-align:right;height:35px; background-color:#F2F4F6;">
                            是否要求供应商填写此项？</td>
                        <td style="width:70%;text-align:left;height:35px;">
                            <asp:RadioButtonList ID="addUse" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">是</asp:ListItem>
                                <asp:ListItem Value="0" Selected="True">否</asp:ListItem>
                              </asp:RadioButtonList>
                        </td>
                    </tr>
                    
                    <tr>
                        <td style="width:30%; text-align:right;height:35px; background-color:#F2F4F6;">
                            将此属性添加到：</td>
                        <td style="width:70%;text-align:left;height:35px;">
                            <asp:RadioButtonList ID="ddlSite" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">当前属性之前</asp:ListItem>
                                <asp:ListItem Value="0" Selected="True">当前属性之后</asp:ListItem>
                              </asp:RadioButtonList>
                        </td>
                    </tr>
                    
                    <tr>
                        <td style="width:30%; text-align:right;height:35px; background-color:#F2F4F6;">
                            &nbsp;
                        </td>
                        <td style="width:70%;text-align:left;height:35px;">
                            <asp:Button ID="btnSave" Text=" 保 存" CssClass="input_button_three" runat="server"
                                OnClick="btnSave_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnCancel" Text=" 取 消" CssClass="input_button_three" runat="server"
                                OnClientClick="closeAndSave();" />
                        </td>
                    </tr>
                    </table>
            </td>
        </tr>
    </table>

    <br />
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="gvMain">
       <tr>
            <td style=" padding-left:30px;">
                <strong>说明：</strong><br />
                 <strong>属性名称：</strong>属性将要显示的名字。<br />
                 <strong>属性类型：</strong>默认为“无”，如果此项需要进行日期上的选择，则选中“日期型”。<br />
                 <strong>要求供应商填写此项：</strong>如果这个属性项中的内容需要由供应商来填充完成，则须勾选上此项。<br />

            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
