<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="PurchaseWeb.Purchase.TemplateManage.List" Theme=""%>

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
      function showTempleteData(id) {
          dialog("编辑属性", "iframe:View.aspx?mid=" + id, "750px", "500px", "text");
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
        <td colspan="2" height="20">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        </td>
    </tr>
      <tr>
        <td style="background-repeat:no-repeat;  font-weight:bold;">
            服务类型：<asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged"  />
            &nbsp;
            <asp:DropDownList ID="ddlLevel3" runat="server" AutoPostBack="true"  DataTextField="Name" DataValueField="ID" onselectedindexchanged="ddlLevel3_SelectedIndexChanged"/>
            </td>
        <td height="32"style="background-repeat:no-repeat; background-position:right; text-align:right; padding-right:20px;">
            &nbsp;<a href="Add.aspx" style="background:url(/public/images/icon/add.png) no-repeat; padding-left:20px;">创建报价单</a></td>
      </tr>
      <tr>
        <td colspan="2" height="10"></td>
      </tr>
    </table>
    


    <table width="90%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                        
                        <asp:GridView ID="gvList" runat="server" CssClass="agvMain" Width="100%" 
                                EmptyDataText="暂无数据.." AutoGenerateColumns="False" AllowPaging="True" OnDataBound="GridView1_DataBound" PageSize="20">
                                <RowStyle CssClass="agvItem1" HorizontalAlign="Center" />
                                
                                <Columns>
                                    <asp:TemplateField HeaderText="报价单名称">
                                        <ItemTemplate>
                                            <a href="javascript:void(0);" onclick="showTempleteData(<%#Eval("id")%>);"><%#Eval("Name")%></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="物料类别">
                                        <ItemTemplate>
                                            <%#GetTypeName(Eval("TypeId").ToString())%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="创建者">
                                        <ItemTemplate>
                                            <%#GetTypeName(Eval("TypeId").ToString())%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="创建时间" DataField="CreateTime" DataFormatString="{0:yyyy-MM-dd}"/>
                                    <asp:TemplateField HeaderText="修改者">
                                        <ItemTemplate>
                                            <%# Convert.ToDateTime(Eval("CreateTime").ToString()).ToString("yyyy-MM-dd")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="修改时间">
                                        <ItemTemplate>
                                            <%# Convert.ToDateTime(Eval("CreateTime").ToString()).ToString("yyyy-MM-dd")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="操作">
                                        <ItemTemplate>
                                            <a href="javascript:void(0);" onclick="showTempleteData(<%#Eval("id")%>);" title="编辑"><img src="/public/images/icon/application_form_edit.png" /></a>
                                            &nbsp;&nbsp;
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="/public/images/icon/cross.png" CausesValidation="false" CommandArgument='<%# Eval("ID") %>' OnClick="lnkDel_Click" OnClientClick="return confirm('您确定删除该报价单吗？');" ToolTip="删除"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                
                                <PagerTemplate>
                                    总数：<asp:Literal ID="pageTotal" runat="server"></asp:Literal> &nbsp;
                                    每页显示：<%# this.gvList.PageSize %> &nbsp;
                                    当前页面：<%# this.gvList.PageIndex + 1%>/<%# this.gvList.PageCount%>
                                    &nbsp;&nbsp;
                                    <asp:LinkButton ID="LinkButton1" Text="上一页" CommandArgument="p" OnCommand="LinkPageChanged" runat="server"></asp:LinkButton>
                                    &nbsp;&nbsp;
                                    <asp:LinkButton ID="LinkButton2" Text="下一页" CommandArgument="n" OnCommand="LinkPageChanged" runat="server"></asp:LinkButton>
                                    &nbsp;&nbsp;
                                    <asp:DropDownList ID="ddl" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropPageChanged">
                                    </asp:DropDownList>
                                </PagerTemplate>
                                
                                <HeaderStyle CssClass="agvHead" />
                                <AlternatingRowStyle CssClass="agvItem1" />
                                <EmptyDataRowStyle Height="55px" HorizontalAlign="Center" />
                                
                            </asp:GridView>
                        
            </td>
        </tr>
    </table>
    

    <table width="90%" border="0" align="center" cellpadding="0" cellspacing="0" style="padding-top:8px;">
        <tr>
            <td align="center">
                &nbsp;</td>
        </tr>
        </table>
        
        
    <br />
    
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="gvMain" style="display:none;">
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
