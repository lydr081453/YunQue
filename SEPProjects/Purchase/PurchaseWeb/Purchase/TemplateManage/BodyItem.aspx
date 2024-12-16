<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BodyItem.aspx.cs" Inherits="PurchaseWeb.Purchase.TemplateManage.BodyItem" Theme=""%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <link href="/public/css/newcss.css" type="text/css" rel="stylesheet" />
        <style>
.tdHide
{
	display:none;
}
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div>

<script>
    function changeitem(t) {
        document.getElementById("div_class").style.display = "none";
        document.getElementById("div_item").style.display = "none";
        document.getElementById("div_tax").style.display = "none";
        document.getElementById("div_discount").style.display = "none";
        document.getElementById("commanditemlink").style.display = "none";
        
       
        switch(t) {
            case 1: //新分类
                document.getElementById("div_class").style.display = "block";
                break;
            case 0: //新项目
                {
                    document.getElementById("div_item").style.display = "block";
                    document.getElementById("commanditemlink").style.display = "block";
                }
                break;
            case 2: //税金
                document.getElementById("div_tax").style.display = "block";
                break;
            case 3: //折扣
                document.getElementById("div_discount").style.display = "block";
                break;
        }
    }
</script>

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
                background-position: right;"><a href="SC_TemplateItemsAuto.aspx?tid=<%=typeId%>&mid=0&peid=0&site=<%=mSite%>" id="commanditemlink" style="display:none;"></a>
            </td>
        </tr>
    </table>
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="gvMain">
                    <tr>
                        <td style="width:20%; text-align:right;height:35px; background-color:#F2F4F6;">
                            添加内容：</td>
                        <td style="width:80%;text-align:left;height:35px;">
                            <asp:RadioButtonList ID="ddlType" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1" onclick="changeitem(1);"  Selected="True">新分类</asp:ListItem>
                                <asp:ListItem Value="0" onclick="changeitem(0);">新项目</asp:ListItem>
                                <asp:ListItem Value="2" onclick="changeitem(2);">税金</asp:ListItem>
                                <asp:ListItem Value="3" onclick="changeitem(3);">折扣</asp:ListItem>
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
                    
                    <tr id="div_class" style="display:;">
                        <td style="width:20%; text-align:right;height:35px; background-color:#F2F4F6;">
                            分类名称：</td>
                        <td style="width:80%;text-align:left;height:35px;">
                            <asp:TextBox ID="txtName" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    </table>
                    
                <div id="div_item" style="display:none;">
                    <asp:GridView ID="list" runat="server" CssClass="gvMain" Width="100%" ShowHeader="False"
                    AutoGenerateColumns="False" CellPadding="0" OnRowDataBound="list_RowDataBound">
                    <RowStyle CssClass="gvItem1" HorizontalAlign="Left" Height="35px" />
                    <FooterStyle Font-Bold="False" />
                    <PagerStyle CssClass="gvPage" HorizontalAlign="Left" />
                    <HeaderStyle CssClass="gvHead" />
                    <AlternatingRowStyle CssClass="gvItem2" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HiddenField ID="hType" runat="server" Value='<%#Eval("Type")%>'/>
                                <asp:HiddenField ID="hName" runat="server" Value='<%#Eval("Name")%>'/>
                                <asp:HiddenField ID="hUse" runat="server" Value='<%#Eval("Use")%>'/>
                                <%#Eval("Name")%>：
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" Width="20%" BackColor="#F2F4F6" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                            </ItemTemplate>
                            <ItemStyle Width="80%" CssClass="gvItem3" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                </div>
                
                <div id="div_tax" style="display:none;">
                </div>
                
                <div id="div_discount" style="display:none;">
                </div>
                
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
