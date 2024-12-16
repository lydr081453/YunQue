<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="PurchaseWeb.Purchase.Version.View"  Theme=""%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <link href="/public/css/newcss.css" type="text/css" rel="stylesheet" />
<style>
.optionstyle
{
}
</style>
    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script language="javascript" src="/public/js/dialog.js"></script>
  <script type="text/javascript">
      function showTempleteData(name, site) {
          dialog("编辑属性", "iframe:ItemEdit.aspx?name=" + name + "&site=" + site, "700px", "500px", "text");
      }

      function showSelectOption(id, index) {
          dialog("编辑属性", "iframe:ChoiceEdit.aspx?id=" + id + "&index=" + index, "700px", "500px", "text");
      }
    </script>
  
</head>
<body>
    <form id="form1" runat="server">
    <div>
 <table width="90%" border="0" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td style="background-repeat:no-repeat;  font-weight:bold;">
            <asp:Literal ID="txtBatchName" runat="server"></asp:Literal>
            </td>
        <td width="35%" height="32"style="background-repeat:no-repeat; background-position:right; text-align:right;">
            &nbsp;</td>
      </tr>
    </table>
    


    <table width="90%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                        
               <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td background="/images/tp_4.jpg" style="width: 6%; height: 30px" align="center">
                        <b>序号</b>
                    </td>
                    <td background="/images/tp_4.jpg" style="width: 15%; height: 30px" align="center">
                        <b>字段名称</b>
                    </td>
                    <td background="/images/tp_4.jpg" style="width: 12%; height: 30px" align="center">
                        <b>中文名称</b>
                    </td>
                    <td background="/images/tp_4.jpg" style="width: 12%; height: 30px" align="center">
                        <b>英文名称</b>
                    </td>
                    <td background="/images/tp_4.jpg" style="width: 12%; height: 30px" align="center">
                        <b>字段类型</b>
                    </td>
                    <td background="/images/tp_4.jpg" style="width: 10%; height: 30px" align="center">
                        <b>展示控件</b>
                    </td>
                    <td background="/images/tp_4.jpg" style="width: 10%; height: 30px" align="center">
                        <b>选项设置</b>
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
                            <%# Eval("ID").ToString()%>
                        </td>
                        <td style="border-bottom: #d8dfe7 1px solid; border-right: #d8dfe7 1px solid;" align="center">
                            <%# Eval("cnDescription").ToString()%>
                        </td>
                        <td style="border-bottom: #d8dfe7 1px solid; border-right: #d8dfe7 1px solid;" align="center">
                            <%# Eval("enDescription").ToString()%>
                        </td>
                        <td style="border-bottom: #d8dfe7 1px solid; border-right: #d8dfe7 1px solid;" align="center">
                            <%# getModelType(Eval("Type").ToString(), Eval("Length").ToString())%>
                        </td>
                        <td style="border-bottom: #d8dfe7 1px solid; border-right: #d8dfe7 1px solid;" align="center">
                            <%# getControls(Eval("Control").ToString())%>
                        </td>
                        <td style="border-bottom: #d8dfe7 1px solid; border-right: #d8dfe7 1px solid;" align="center">
                            <%# getOption(Eval("Option").ToString(), Eval("ID").ToString(), Container.ItemIndex.ToString())%>
                        </td>
                    </tr>
                </ItemTemplate>
        </asp:DataList>
</table>

                <br />
            </td>
        </tr>
    </table>
    

    <table width="90%" border="0" align="center" cellpadding="0" cellspacing="0" style="padding-top:8px;">
        <tr>
            <td align="center">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center">
                <input type="button" class="input_button_six" value=" 修改项目" onclick="location.href=('List.aspx?tid=<%=typeId%>');" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <input type="button" class="input_button_six" value="  关  闭 " 
                    onclick="window.close();" />
            </td>
        </tr>
    </table>
        
        
    <br />
    
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="gvMain">
       <tr>
            <td style=" padding-left:50px;">
                <strong>说明：</strong><br />
                <strong>修改报价单：</strong>返回继续编辑报价单项目。<br />
                <strong>生成报价单：</strong>当您确认添加完成全部报价项目后，点击此按钮保存，并转到下一步。<br />
            </td>
        </tr>
    </table>

    </div>
    </form>
</body>
</html>
