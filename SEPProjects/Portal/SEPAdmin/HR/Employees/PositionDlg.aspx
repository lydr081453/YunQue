<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PositionDlg.aspx.cs" Inherits="SEPAdmin.HR.Employees.PositionDlg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
            <script language="javascript" src="/HR/Employees/js/jquery-1.2.6.js"></script>

    <script type="text/javascript" src="/HR/Employees/js/artDialog.js?skin=default"></script>

    <script type="text/javascript" src="/HR/Employees/js/iframeTools.js"></script>
    <script type="text/javascript" src="/public/js/DatePicker.js"></script>
    <script type="text/javascript">
        function selectPosition(pid, pname, lv, sh, sl) {
            if (artDialog.open.origin.document.getElementById("ctl00_ContentPlaceHolder1_txtPosition") != null) {
                artDialog.open.origin.document.getElementById("ctl00_ContentPlaceHolder1_txtPosition").value = pname;
                artDialog.open.origin.document.getElementById("ctl00_ContentPlaceHolder1_txtJob_JoinJob").value = pid;

                if (artDialog.open.origin.document.getElementById("ctl00_ContentPlaceHolder1_lblLevel") != null) {
                    artDialog.open.origin.document.getElementById("ctl00_ContentPlaceHolder1_lblLevel").innerHTML = lv;
                }
                if (artDialog.open.origin.document.getElementById("ctl00_ContentPlaceHolder1_lblSalary") != null) {
                    artDialog.open.origin.document.getElementById("ctl00_ContentPlaceHolder1_lblSalary").innerHTML = sh + "-" + sl;
                }
            }
            else {
                artDialog.open.origin.document.getElementById("ctl00_MainContent_TabContainer1_TabPanel3_txtPosition").value = pname;
                artDialog.open.origin.document.getElementById("ctl00_MainContent_TabContainer1_TabPanel3_txtJob_JoinJob").value = pid;
            }
            art.dialog.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%;">
            <tr>
                <td style="padding-right: 0px; padding-left: 0px; padding-bottom: 0px; padding-top: 0px">
                    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" Width="100%"
                        EmptyDataText="没有符合要求的数据">
                        <Columns>
                            <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <a id='lblPosition' name='lblPosition' onclick="selectPosition('<%# Eval("departmentpositionid") %>','<%# Eval("departmentpositionname") %>','<%# Eval("levelname") %>','<%# Eval("salaryhigh","{0:c}") %>','<%# Eval("salarylow","{0:c}") %>');"  style=" text-decoration:underline; cursor:pointer;">选择</a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="departmentpositionname" HeaderText="职务" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="levelname" HeaderText="级别" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="salaryhigh" HeaderText="工资额度" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:C}"/>
                            <asp:BoundField DataField="salarylow" HeaderText="工资额度" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:C}"/>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
