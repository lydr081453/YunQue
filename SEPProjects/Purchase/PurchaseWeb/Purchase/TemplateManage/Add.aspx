<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="PurchaseWeb.Purchase.TemplateManage.Add" Theme=""%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="/public/css/newcss.css" type="text/css" rel="stylesheet" />

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script language="javascript" src="/public/js/dialog.js"></script>
    
    <script type="text/javascript">
        function showTempleteData(id) {
            dialog("编辑属性", "iframe:View.aspx?new=1&tid=" + id, "750px", "500px", "text");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>

    <table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td style="padding-left: 5px">
                <font color="red">* 请选择您要使用的三级物料。</font>
            </td>
        </td>
        
        </tr>
        <tr>
            <td style="padding-left: 5px">
                <asp:DataList ID="list" ItemStyle-BorderStyle="None" runat="server" RepeatDirection="Vertical"
                    Width="100%">
                    <ItemTemplate>
                        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" background="/images/tp_4.jpg">
                            <tr>
                                <td background="/images/tp_3.jpg" style="background-repeat: no-repeat; padding-left: 25px;
                                    font-weight: bold;">
                                    <%# Eval("Name") %>
                                </td>
                                <td width="10%" height="32" background="/images/tp_5.jpg" style="background-repeat: no-repeat;
                                    background-position: right;">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <asp:DataList ID="list1" ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px"
                            ItemStyle-BorderColor="#d8dfe7" ItemStyle-Width="25%" runat="server" Width="100%"
                            ItemStyle-VerticalAlign="Top" CellPadding="0" CellSpacing="0" RepeatColumns="4"
                            RepeatDirection="Horizontal" DataSource='<%#getDataSource(Eval("ID").ToString())%>'>
                            <ItemTemplate>
                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td bgcolor="#d8dfe7" style="padding-left: 15px; border-right: #fff 1px solid; height: 30px;
                                            font-weight: bold;">
                                            <%# Eval("Name") %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DataList ID="list2" runat="server" DataSource='<%#getDataSource1(Eval("ID").ToString())%>'
                                                RepeatColumns="1" CellPadding="5" CellSpacing="0" Width="100%" RepeatDirection="Horizontal">
                                                <ItemTemplate>
                                                    <input type="button" value="<%#Eval("Name")%>" onclick="showTempleteData(<%#Eval("ID")%>)" />
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                    </ItemTemplate>
                </asp:DataList>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="display: none">
        <tr>
            <td style="padding-left: 5px" colspan="2">
                <img src="../Images/b1_06.jpg" width="98%" height="1" vspace="10" />
            </td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td style="padding-left: 5px" width="30%" align="center">
                <%if (IsUse == 1)
                  {%>&nbsp;
                <asp:Button ID="btnUpdate" CssClass="input_button_three" Text=" 关 闭" runat="server"
                    OnClientClick="parent.$('#floatBoxBg').hide(); parent.$('#floatBox').hide();" />
                 <% } %>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
