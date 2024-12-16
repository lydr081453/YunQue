<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Header.aspx.cs" Inherits="PurchaseWeb.Purchase.TemplateManage.Header" Theme=""%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <link href="/public/css/newcss.css" type="text/css" rel="stylesheet" />

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script language="javascript" src="/public/js/dialog.js"></script>
  <script type="text/javascript">
        function showTempleteData(name, site) {
            dialog("编辑属性", "iframe:HeaderItem.aspx?name=" + name + "&site=" + site, "500px", "500px", "text");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="90%" border="0" align="center" cellpadding="0" cellspacing="0" background="/images/tp_4.jpg">
      <tr>
        <td background="/images/tp_3.jpg" style="background-repeat:no-repeat; padding-left:25px; font-weight:bold;">
            编辑&nbsp;[<asp:Literal ID="lbModelName" runat="server"></asp:Literal>]
            报价单模版表头</td>
        <td width="35%" height="32" background="/images/tp_5.jpg" 
              style="background-repeat:no-repeat; background-position:right;padding-right:20px;" align="right">
            &nbsp;</td>
      </tr>
    </table>

<br />
<table width="90%" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td background="/images/tp_4.jpg" style="width: 6%; height: 30px" align="center">
                        <b>序号</b>
                    </td>
                    <td background="/images/tp_4.jpg" style="width: 16%; height: 30px" align="center">
                        <b>属性名称</b>
                    </td>
                    <td background="/images/tp_4.jpg" style="width: 16%; height: 30px" align="center">
                        <b>属性类型</b>
                    </td>
                    <td background="/images/tp_4.jpg" style="width: 16%; height: 30px" align="center">
                        <b>要求供应商填写此项</b>
                    </td>
                    <td background="/images/tp_4.jpg" style="width: 18%;" align="center">
                        <b>操作</b>
                    </td>
                </tr>
<asp:DataList ID="gvList" runat="server" Width="100%" RepeatLayout="Flow" ShowFooter="False" 
                    ShowHeader="False" RepeatDirection="Horizontal" onitemdatabound="gvList_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td style="height: 30px; border-bottom: #d8dfe7 1px solid; border-right: #d8dfe7 1px solid;
                            border-left: #d8dfe7 1px solid;" align="center">
                            <%#Container.ItemIndex+1%>
                        </td>
                        <td style="border-bottom: #d8dfe7 1px solid; border-right: #d8dfe7 1px solid;" align="center">
                            <%# Eval("Name").ToString()%>
                        </td>
                        <td style="border-bottom: #d8dfe7 1px solid; border-right: #d8dfe7 1px solid;" align="center">
                            <%#getModelType(Eval("Type").ToString())%>
                        </td>
                        <td style="border-bottom: #d8dfe7 1px solid; border-right: #d8dfe7 1px solid;" align="center">
                            <%#getUseType(Eval("Use").ToString())%>
                        </td>
                        <td style="border-bottom: #d8dfe7 1px solid; border-right: #d8dfe7 1px solid;" align="center">
                               
                                <img src="/public/images/icon/add.png" title="添加新属性" onclick="showTempleteData('',<%#Container.ItemIndex%>);" style="cursor: pointer"/>
                                &nbsp;&nbsp;
                                <img src="/public/images/icon/application_form_edit.png" title="编辑属性" onclick="showTempleteData('<%# Eval("Name").ToString()%>',0);" style="cursor: pointer"/>
                                &nbsp;&nbsp;
                                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="/public/images/icon/cross.png" CausesValidation="false" CommandArgument='<%# Eval("Name") %>' OnClick="lnkDel_Click" ToolTip="删除" OnClientClick="return confirm('您确定删除该属性吗？');" />
                                &nbsp;&nbsp;
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="/public/images/icon/arrow_up.png" CausesValidation="false" CommandArgument='<%#Container.ItemIndex%>' CommandName="up" OnClick="btn_Move"  ToolTip="向上移动"/>
                                &nbsp;&nbsp;
                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="/public/images/icon/arrow_down.png" CausesValidation="false" CommandArgument='<%#Container.ItemIndex%>' CommandName="down" OnClick="btn_Move"  ToolTip="向下移动"/>
                        </td>
                    </tr>
                </ItemTemplate>
        </asp:DataList>
</table>
       
        </td>
      </tr>
      </table>
 <table width="90%" border="0" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td height="100" align="center">
 
    
    <table width="90%" border="0" align="center" cellpadding="0" cellspacing="0" style="padding-top:8px;">
        <tr>
            <td style="height:20px;"></td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="lnkLink" runat="server" CausesValidation="false" 
                    CssClass="input_button_six" OnClick="lnkLink_Click1" 
                    OnClientClick="return saveNew();return confirm('是否保存报价单模版？');" 
                    Text=" 确定保存表头 " />
             </td>
         </tr>
    </table>
<br />
<div style="text-align:left;">
<table width="100%" border="0"  cellpadding="0" cellspacing="0" align="left">
<tr>
        <td style="color:Red;">
            <strong>注意事项：</strong><br />
            任意一个报价模版中，必须包含“单价”、“数量”等属性。<br /><br />
        </td>
    </tr>
   <tr>
        <td style="">
            <strong>说明：</strong><br />
            <strong>模版名称：</strong>为您的模版取一个名字，可是是由任意字符构成；<br />
            <strong>添加新属性：</strong>为模版添加一个新的属性项。<br />
            <strong>编辑：</strong>对指定的属性项进行修改。<br />
            <strong>删除：</strong>删除模版的一个属性项。<br />
            <strong>确定使用此模版：</strong>当您最终确定了新建报价模版的格式属性后，点击此按钮保存并转到下一步。<br />
        </td>
    </tr>
    
</table>
</div>
    </table>
    </div>
    </form>
</body>
</html>
