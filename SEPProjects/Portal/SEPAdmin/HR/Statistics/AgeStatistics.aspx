﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgeStatistics.aspx.cs" Inherits="SEPAdmin.HR.Statistics.AgeStatistics" MasterPageFile="~/MasterPage.master"  EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >
<script language="javascript" src="/public/js/jquery.js"></script>
<script type="text/javascript">
    $().ready(function() {
        $("#<%=ddltype1.ClientID %>").empty();
        $("#<%=ddltype2.ClientID %>").empty();
        SEPAdmin.HR.Statistics.AgeStatistics.getalist($("#<%=hidtype.ClientID %>").val(), init1);
        function init1(r) {
            if (r.value != null)
                for (k = 0; k < r.value.length; k++) {
                if (r.value[k][0] == $("#<%=hidtype1.ClientID %>").val()) {
                    $("#<%=ddltype1.ClientID %>").append("<option value=\"" + r.value[k][0] + "\" selected>" + r.value[k][1] + "</option>");
                }
                else {
                    $("#<%=ddltype1.ClientID %>").append("<option value=\"" + r.value[k][0] + "\">" + r.value[k][1] + "</option>");
                }
            }

        }
        if ($("#<%=hidtype.ClientID %>").val() == "") {
            $("#<%=ddltype.ClientID %>").val("-1");
        }
        SEPAdmin.HR.Statistics.AgeStatistics.getalist($("#<%=hidtype1.ClientID %>").val(), init2);
        function init2(r) {
            $("#<%=ddltype2.ClientID %>").empty();
            //            $("#<%=ddltype2.ClientID %>").append("<option value=\"-1\" selected>请选择...</option>");
            if (r.value != null)
                for (j = 0; j < r.value.length; j++) {
                if (r.value[j][0] == $("#<%=hidtype2.ClientID %>").val()) {
                    $("#<%=ddltype2.ClientID %>").append("<option value=\"" + r.value[j][0] + "\" selected>" + r.value[j][1] + "</option>");
                    document.getElementById("<% =hidGroupName.ClientID %>").value = r.value[j][1];
                }
                else {
                    $("#<%=ddltype2.ClientID %>").append("<option value=\"" + r.value[j][0] + "\">" + r.value[j][1] + "</option>");
                }
            }
        }
        $("#<%=ddltype.ClientID %>").val($("#<%=hidtype.ClientID %>").val());




        $("#<%=ddltype.ClientID %>").change(function() {
            $("#<%=hidtype.ClientID %>").val($("#<%=ddltype.ClientID %>").val());
            $("#<%=ddltype1.ClientID %>").empty();
            $("#<%=ddltype2.ClientID %>").empty();

            SEPAdmin.HR.Statistics.AgeStatistics.getalist($("#<%=ddltype.ClientID %>").val(), pop1);
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

        $("#<%=ddltype1.ClientID %>").change(function() {
            $("#<%=ddltype2.ClientID %>").empty();

            SEPAdmin.HR.Statistics.AgeStatistics.getalist($("#<%=ddltype1.ClientID %>").val(), pop2);
            function pop2(r) {
                if (r.value != null)
                    for (i = 0; i < r.value.length; i++) {
                    $("#<%=ddltype2.ClientID %>").append("<option value=\"" + r.value[i][0] + "\">" + r.value[i][1] + "</option>");
                }
            }
            $("#<%=hidtype1.ClientID %>").val($("#<%=ddltype1.ClientID %>").val());
            $("#<%=hidtype2.ClientID %>").val("-1");
        });

        $("#<%=ddltype2.ClientID %>").change(function() {
            $("#<%=hidtype2.ClientID %>").val($("#<%=ddltype2.ClientID %>").val());
            var ddl = document.getElementById("<%=ddltype2.ClientID %>");
            var deptname = "";
            if (ddl.options[ddl.selectedIndex].text != "请选择...") {
                deptname = ddl.options[ddl.selectedIndex].text;
                document.getElementById("<% =hidGroupName.ClientID %>").value = deptname;
            }
        });

    });

</script>
<table width="100%" class="tableForm">
            <tr>
                <td class="oddrow" colspan="6">
                    年龄段统计:
                </td>
            </tr> 
    <tr>        
        <td class="oddrow" style="width: 10%">部门：</td>
        <td class="oddrow-l">          
            <asp:DropDownList ID="ddltype" Width="100px" runat="server" AutoPostBack="false"/>
                        &nbsp;<asp:DropDownList ID="ddltype1" runat="server" Width="100px" />
                        &nbsp;<asp:DropDownList ID="ddltype2" runat="server" Width="100px" />
                        <asp:HiddenField ID="hidtype" Value="-1" runat="server" />
                        <asp:HiddenField ID="hidtype1" Value="-1" runat="server" />
                        <asp:HiddenField ID="hidtype2" Value="-1"  runat="server" />
                        <asp:HiddenField ID="hidGroupName" Value=""  runat="server" />
         </td>        
        <td class="oddrow-l" >
            <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click" />
        </td>
    </tr>
  </table> 
  <br />
<table width="100%">  
                   
            <tr>
                <td >
                    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" Width="100%" >
                         <Columns>
                             <asp:BoundField DataField="compayName" HeaderText="集团" ItemStyle-HorizontalAlign="Center"/>
                            <asp:BoundField DataField="departmentName" HeaderText="公司" ItemStyle-HorizontalAlign="Center"/>
                            <asp:BoundField DataField="groupName" HeaderText="组别" ItemStyle-HorizontalAlign="Center"/> 
                            <asp:BoundField DataField="l60NumByDep" HeaderText="组别中60年代前人数" ItemStyle-HorizontalAlign="Center"/>  
                            <asp:TemplateField HeaderText="组别中60年代前比例">
                                <ItemTemplate>
                                    <%# Double.Parse(Eval("l60RateByDep").ToString()).ToString("0.00") + "%"%>
                                </ItemTemplate>
                           </asp:TemplateField>  
                           <asp:TemplateField HeaderText="组别中60年代前在公司比例">
                                <ItemTemplate>
                                    <%# Double.Parse(Eval("l60RateByCo").ToString()).ToString("0.00") + "%"%>
                                </ItemTemplate>
                           </asp:TemplateField>  
                            <asp:BoundField DataField="y60NumByDep" HeaderText="组别中60年代人数" ItemStyle-HorizontalAlign="Center"/> 
                            <asp:TemplateField HeaderText="组别中60年代比例">
                                <ItemTemplate>
                                    <%# Double.Parse(Eval("y60RateByDep").ToString()).ToString("0.00")+"%"%>
                                </ItemTemplate>
                           </asp:TemplateField>  
                           <asp:TemplateField HeaderText="组别中60年代在公司比例">
                                <ItemTemplate>
                                    <%# Double.Parse(Eval("y60RateByCo").ToString()).ToString("0.00") + "%"%>
                                </ItemTemplate>
                           </asp:TemplateField> 
                            <asp:BoundField DataField="y70NumByDep" HeaderText="组别中70年代人数" ItemStyle-HorizontalAlign="Center"/> 
                            <asp:TemplateField HeaderText="组别中70年代比例">
                                <ItemTemplate>
                                    <%# Double.Parse(Eval("y70RateByDep").ToString()).ToString("0.00")+"%"%>
                                </ItemTemplate>
                           </asp:TemplateField>  
                           <asp:TemplateField HeaderText="组别中70年代在公司比例">
                                <ItemTemplate>
                                    <%# Double.Parse(Eval("y70RateByCo").ToString()).ToString("0.00") + "%"%>
                                </ItemTemplate>
                           </asp:TemplateField> 
                            <asp:BoundField DataField="y80NumByDep" HeaderText="组别中80年代人数" ItemStyle-HorizontalAlign="Center"/> 
                            <asp:TemplateField HeaderText="组别中80年代比例">
                                <ItemTemplate>
                                    <%# Double.Parse(Eval("y80RateByDep").ToString()).ToString("0.00")+"%"%>
                                </ItemTemplate>
                           </asp:TemplateField>  
                           <asp:TemplateField HeaderText="组别中80年代在公司比例">
                                <ItemTemplate>
                                    <%# Double.Parse(Eval("y80RateByCo").ToString()).ToString("0.00") + "%"%>
                                </ItemTemplate>
                           </asp:TemplateField> 
                            <asp:BoundField DataField="y90NumByDep" HeaderText="组别中90年代人数" ItemStyle-HorizontalAlign="Center"/> 
                            <asp:TemplateField HeaderText="组别中90年代比例">
                                <ItemTemplate>
                                    <%# Double.Parse(Eval("y90RateByDep").ToString()).ToString("0.00")+"%"%>
                                </ItemTemplate>
                           </asp:TemplateField>  
                           <asp:TemplateField HeaderText="组别中90年代在公司比例">
                                <ItemTemplate>
                                    <%# Double.Parse(Eval("y90RateByCo").ToString()).ToString("0.00") + "%"%>
                                </ItemTemplate>
                           </asp:TemplateField> 
                            <asp:BoundField DataField="y2kNumByDep" HeaderText="组别中90年代后人数" ItemStyle-HorizontalAlign="Center"/> 
                            <asp:TemplateField HeaderText="组别中90年代后比例">
                                <ItemTemplate>
                                    <%# Double.Parse(Eval("y2kRateByDep").ToString()).ToString("0.00")+"%"%>
                                </ItemTemplate>
                           </asp:TemplateField>  
                           <asp:TemplateField HeaderText="组别中90年代后在公司比例">
                                <ItemTemplate>
                                    <%# Double.Parse(Eval("y2kRateByCo").ToString()).ToString("0.00") + "%"%>
                                </ItemTemplate>
                           </asp:TemplateField>  
                            <asp:BoundField DataField="unknow3NumByDep" HeaderText="组别中未知年龄段人数" ItemStyle-HorizontalAlign="Center"/>    
                            <asp:TemplateField HeaderText="组别中未知年龄段比例">
                                <ItemTemplate>
                                    <%# Double.Parse(Eval("unknow3RateByDep").ToString()).ToString("0.00")+"%"%>
                                </ItemTemplate>
                           </asp:TemplateField>  
                           <asp:TemplateField HeaderText="组别中未知年龄段在公司比例">
                                <ItemTemplate>
                                    <%# Double.Parse(Eval("unknow3RateByCo").ToString()).ToString("0.00")+"%"%>
                                </ItemTemplate>
                           </asp:TemplateField>                            
                        </Columns>
                    </asp:GridView>     
                </td>
            </tr>                      
     </table>
</asp:Content>