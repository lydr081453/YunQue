<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportProjectFee.aspx.cs" EnableEventValidation="false"  MasterPageFile="~/MasterPage.master" Inherits="FinanceWeb.Reports.ImportProjectFee" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

     <script type="text/javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script type="text/javascript" src="/publicjs/jquery.jcarousellite.min.js"></script>

    <script type="text/javascript" src="/publicjs/script_jcarousellite.js"></script>

    <script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>

    <script src="/public/js/dimensions.js" type="text/javascript"></script>

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script type="text/javascript">

        $().ready(function () {
            $("#<%=ddltype1.ClientID %>").empty();
            $("#<%=ddltype2.ClientID %>").empty();
            Purchase_selectOperationAuditor.getalist($("#<%=hidtype.ClientID %>").val(), init1);
            function init1(r) {
                if (r.value != null) {
                    for (k = 0; k < r.value.length; k++) {
                        if (r.value[k][0] == $("#<%=hidtype1.ClientID %>").val()) {
                            $("#<%=ddltype1.ClientID %>").append("<option value=\"" + r.value[k][0] + "\" selected>" + r.value[k][1] + "</option>");
                        }
                        else {
                            $("#<%=ddltype1.ClientID %>").append("<option value=\"" + r.value[k][0] + "\">" + r.value[k][1] + "</option>");
                        }
                    }

                }
            }
            if ($("#<%=hidtype.ClientID %>").val() == "") {
                $("#<%=ddltype.ClientID %>").val("-1");
            }
            Purchase_selectOperationAuditor.getalist($("#<%=hidtype1.ClientID %>").val(), init2);
            function init2(r) {
                $("#<%=ddltype2.ClientID %>").empty();
                //            $("#<%=ddltype2.ClientID %>").append("<option value=\"-1\" selected>请选择...</option>");
                if (r.value != null)
                    for (j = 0; j < r.value.length; j++) {
                        if (r.value[j][0] == $("#<%=hidtype2.ClientID %>").val()) {
                            $("#<%=ddltype2.ClientID %>").append("<option value=\"" + r.value[j][0] + "\" selected>" + r.value[j][1] + "</option>");
                    }
                    else {
                        $("#<%=ddltype2.ClientID %>").append("<option value=\"" + r.value[j][0] + "\">" + r.value[j][1] + "</option>");
                    }
                }
        }
            $("#<%=ddltype.ClientID %>").val($("#<%=hidtype.ClientID %>").val());




            $("#<%=ddltype.ClientID %>").change(function () {
                $("#<%=hidtype.ClientID %>").val($("#<%=ddltype.ClientID %>").val());
                $("#<%=ddltype1.ClientID %>").empty();
                $("#<%=ddltype2.ClientID %>").empty();

                Purchase_selectOperationAuditor.getalist($("#<%=ddltype.ClientID %>").val(), pop1);
                function pop1(r) {
                    if (r.value != null)
                        for (i = 0; i < r.value.length; i++) {
                            $("#<%=ddltype1.ClientID %>").append("<option value=\"" + r.value[i][0] + "\">" + r.value[i][1] + "</option>");
                        }
                }
                $("#<%=ddltype2.ClientID %>").append("<option value='-1'>请选择...</option>");
                $("#<%=hidtype1.ClientID %>").val("-1");
                $("#<%=hidtype2.ClientID %>").val("-1");
            });

            $("#<%=ddltype1.ClientID %>").change(function () {
                $("#<%=ddltype2.ClientID %>").empty();

                Purchase_selectOperationAuditor.getalist($("#<%=ddltype1.ClientID %>").val(), pop2);
                function pop2(r) {
                    if (r.value != null)
                        for (i = 0; i < r.value.length; i++) {
                            $("#<%=ddltype2.ClientID %>").append("<option value=\"" + r.value[i][0] + "\">" + r.value[i][1] + "</option>");
                        }
                }
                $("#<%=hidtype1.ClientID %>").val($("#<%=ddltype1.ClientID %>").val());
                $("#<%=hidtype2.ClientID %>").val("-1");
            });

            $("#<%=ddltype2.ClientID %>").change(function () {
                $("#<%=hidtype2.ClientID %>").val($("#<%=ddltype2.ClientID %>").val());
            });

        });
    </script>


     <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                导入信息
            </td>
        </tr>
          <tr>
            <td class="oddrow" style="width: 15%">
                团队名称:
            </td>
            <td class="oddrow-l" align="left" colspan="3">
                <asp:DropDownList ID="ddltype" Width="100px" runat="server" AutoPostBack="false" />
                &nbsp;<asp:DropDownList ID="ddltype1" runat="server" Width="100px" />
                &nbsp;<asp:DropDownList ID="ddltype2" runat="server" Width="100px" />
                <asp:HiddenField ID="hidtype" Value="-1" runat="server" />
                <asp:HiddenField ID="hidtype1" Value="-1" runat="server" />
                <asp:HiddenField ID="hidtype2" Value="-1" runat="server" />
            </td>
        </tr>
          
        <tr>
            <td class="oddrow" style="width: 15%">
                选择文件:
            </td>
            <td class="oddrow-l" align="left" colspan="3">
                  <asp:FileUpload ID="fileId" runat="server" Width ="500px" /> <br /> 
                <font color="gray">EXCEL格式模板下载</font>&nbsp;&nbsp;<a href="/tmp/Salary/FEELISTTemplate.xlsx" target="_blank"><img src="/images/ico_04.gif" /></a>
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Button ID="btnImport" runat="server" Text=" 导入 " OnClick="btnImport_Click" CssClass="widebuttons"/>&nbsp;
                
            </td>
        </tr>
    </table>
        <table width="100%" class="tableForm">
             <tr>
            <td class="heading" colspan="4">
                查询信息
            </td>
        </tr>
            <tr>
            <td class="oddrow" style="width: 15%">
                导入年份:
            </td>
            <td class="oddrow-l" align="left" >
                 <asp:DropDownList runat="server" ID="ddlYear"></asp:DropDownList>
            </td>
              <td class="oddrow" style="width: 15%">
                导入月份:
            </td>
            <td class="oddrow-l" align="left" >
                <asp:DropDownList runat="server" ID="ddlMonth">
                    <asp:ListItem Value="1" Text="1月"></asp:ListItem>
                    <asp:ListItem Value="2" Text="2月"></asp:ListItem>
                    <asp:ListItem Value="3" Text="3月"></asp:ListItem>
                    <asp:ListItem Value="4" Text="4月"></asp:ListItem>
                    <asp:ListItem Value="5" Text="5月"></asp:ListItem>
                    <asp:ListItem Value="6" Text="6月"></asp:ListItem>
                    <asp:ListItem Value="7" Text="7月"></asp:ListItem>
                    <asp:ListItem Value="8" Text="8月"></asp:ListItem>
                    <asp:ListItem Value="9" Text="9月"></asp:ListItem>
                    <asp:ListItem Value="10" Text="10月"></asp:ListItem>
                    <asp:ListItem Value="11" Text="11月"></asp:ListItem>
                    <asp:ListItem Value="12" Text="12月"></asp:ListItem>
                    <asp:ListItem Value="13" Text="13月"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
            <tr>
                <td class="oddrow-l" colspan="4">
                     <asp:Button ID="btnSearch" runat="server" Text=" 检索 " OnClick="btnSearch_Click" CssClass="widebuttons"/>&nbsp;
                     <asp:Button ID="btnDelete" runat="server" Text=" 删除 " OnClick="btnDelete_Click" CssClass="widebuttons"/>
                </td>
            </tr>
        <tr>
            <td class="heading" colspan="4">
                部门月成本列表
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvG_RowDataBound"
                    EmptyDataText="暂时没有相关记录"  AllowPaging="false" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="ProjectCode" HeaderText="项目号" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Year" HeaderText="年份" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="month" HeaderText="月份" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Fee" HeaderText="Fee" ItemStyle-HorizontalAlign="right" DataFormatString ="{0:C}" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
            <tr>
                 <td colspan="4" align="right">
                     <asp:Label runat="server" ID="lblTotal"></asp:Label>
                 </td>
            </tr>
    </table>
</asp:Content>