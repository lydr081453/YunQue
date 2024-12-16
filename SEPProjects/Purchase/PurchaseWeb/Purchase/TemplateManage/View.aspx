<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="PurchaseWeb.Purchase.TemplateManage.View" Theme=""%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <link href="/public/css/newcss.css" type="text/css" rel="stylesheet" />

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script language="javascript" src="/public/js/dialog.js"></script>
  
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
                        
                            <asp:GridView ID="list" runat="server" CssClass="agvMain" Width="100%" 
                                EmptyDataText="暂无数据.." 
                                onrowdatabound="list_RowDataBound1">
                                <RowStyle CssClass="agvItem1" HorizontalAlign="Center" />
                                
                                <HeaderStyle CssClass="agvHead" />
                                <AlternatingRowStyle CssClass="agvItem1" />
                                <EmptyDataRowStyle Height="55px" HorizontalAlign="Center" />
                            </asp:GridView>

                <br />
            </td>
        </tr>
    </table>
    

    <table width="90%" border="0" align="center" cellpadding="0" cellspacing="0" style="padding-top:8px;">
         <tr>
            <td align="center">
                报价单名称：<asp:TextBox ID="txtModelName" runat="server" Width="232px"></asp:TextBox>
             </td>
         </tr>
        <tr>
            <td align="center">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center">
                <input type="button" class="input_button_six" value="  修改表头属性" onclick="location.href=('Header.aspx?tid=<%=typeId%>&mid=<%=modelId%>');" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <input type="button" class="input_button_six" value="  修改报价项目" onclick="location.href=('Body.aspx?tid=<%=typeId%>&mid=<%=modelId%>');" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="lnkLink" runat="server" CausesValidation="false" 
                    CssClass="input_button_six" OnClick="lnkLink_Click" 
                    OnClientClick="return saveNew();" Text="  保存报价单" />
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
