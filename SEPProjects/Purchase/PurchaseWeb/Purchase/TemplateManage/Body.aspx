<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Body.aspx.cs" Inherits="PurchaseWeb.Purchase.TemplateManage.Body" Theme=""%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <link href="/public/css/newcss.css" type="text/css" rel="stylesheet" />
    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script language="javascript" src="/public/js/dialog.js"></script>
<script type="text/javascript">

        function showTempleteData(pid,site) {
            dialog("报价项目管理", "iframe:BodyItem.aspx?tid=<%=typeId%>&pid=" + pid + "&site=" + site, "500px", "500px", "text");
        }

        
        function viewTemplete123(url) {
            dialog("系统标准报价项模版", "iframe:"+url, "650px", "500px", "text");
        }

        var ckbool = false;
        function OnCheckAll() {

            var obj = document.getElementsByTagName("input");
            if (ckbool) {
                //取消全选
                ckbool = false;
                document.getElementById("allck").innerHTML = "全选";

            }
            else {
                //全部选择
                ckbool = true;
                document.getElementById("allck").innerHTML = "取消全选";
            }

            for (i = 0; i < obj.length; i++) {
                obj[i].checked = ckbool;
            }

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
 <table width="90%" border="0" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td style="background-repeat:no-repeat;  font-weight:bold;">
            [<asp:Literal ID="txtBatchName" runat="server"></asp:Literal>
            ] 报价明细</td>
        <td width="35%" height="32"style="background-repeat:no-repeat; background-position:right; text-align:right;">
            
            &nbsp;</td>
      </tr>
    </table>
    


    <table width="90%" border="0" align="center" cellpadding="0" cellspacing="0" >
        <tr>
            <td>
                        
                            <asp:GridView ID="list" runat="server" CssClass="agvMain" Width="100%" 
                                EmptyDataText="暂无数据.." onrowdatabound="list_RowDataBound1">
                                <RowStyle CssClass="agvItem1" HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderText="操作">
                                        <ItemTemplate>
                                
                                <asp:CheckBox ID="ckbox" runat="server"/><asp:Label ID="mId" runat="server" Text='<%#Eval("ID")%>' Visible="false"></asp:Label>
                                &nbsp;&nbsp;     
                                <img src="/public/images/icon/add.png" title="添加新属性" onclick="showTempleteData(0,<%#Container.DataItemIndex%>);" style="cursor: pointer"/>
                                &nbsp;&nbsp;
                                <img src="/public/images/icon/application_form_edit.png" title="编辑属性" onclick="showTempleteData('<%# Eval("ID").ToString()%>',0);" style="cursor: pointer"/>
                                &nbsp;&nbsp;
                                <%--<asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="/public/images/icon/cross.png" CausesValidation="false" CommandArgument='<%# Eval("ID") %>' OnClick="lnkDel_Click" ToolTip="删除" OnClientClick="return confirm('您确定删除该项目吗？');" />
                                &nbsp;&nbsp;--%>
                                <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="/public/images/icon/arrow_up.png" CausesValidation="false" CommandArgument="<%#Container.DataItemIndex%>" CommandName="up" OnClick="btn_Move"  ToolTip="向上移动"/>
                                &nbsp;&nbsp;
                                <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="/public/images/icon/arrow_down.png" CausesValidation="false" CommandArgument="<%#Container.DataItemIndex%>" CommandName="down" OnClick="btn_Move"  ToolTip="向下移动"/>
                                
                                        </ItemTemplate>
                                        <ItemStyle Width="20%" />
                                    </asp:TemplateField>
                                </Columns>
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
            <td>
             [<a href="javascript:void(0)" onclick="OnCheckAll();"><span id="allck">全选</span></a>]
             &nbsp;&nbsp;
             <asp:Button  CssClass="input_button_three" ID="lnkLink2"
                        runat="server" Text="删除"  
                        OnClick="lnkDelAll_Click" 
                    OnClientClick="return confirm('确认是否删除？');"/>
             &nbsp;</td>
            <td align="left" style="padding-left:50px;">
            
             <input type="button" value=" 修改表头属性 " class="input_button_six" onclick="location.href=('Header.aspx?tid=<%=typeId%>&mid=<%=modelId%>')"/>
             
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             <asp:Button  CssClass="input_button_six" ID="lnkLink1"
                        runat="server" Text=" 确定保存报价 " CausesValidation="false" 
                        OnClick="lnkLink1_Click" 
                    OnClientClick="return confirm('确认是否保存？');"/>
             </td>
         </tr>
    </table>
    
    <br />
    
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="gvMain">
       <tr>
            <td style=" padding-left:50px;">
                <strong>说明：</strong><br />
                <strong>添加报价项：</strong>向已生成的报价模版中添加报价明细信息。<br />
                <strong>确定引用报价：</strong>当您确认添加完成全部报价项目后，点击此按钮保存，并转到下一步。<br />
                <strong>编辑：</strong>对指定的报价项进行修改。<br />
                <strong>删除：</strong>删除指定的一个报价项。<br />
                <strong>使用新报价单：</strong>如果您想采用全新格式的报价模版，点击此按钮即可重新开始创建。<br />
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
