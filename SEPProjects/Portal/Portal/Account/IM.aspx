<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IM.aspx.cs" Inherits="Portal.WebSite.Account.IM" MasterPageFile="~/Default.Master"%>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
    <script type="text/javascript" language="javascript">
        function banding() {
            var address = document.getElementById('address').value;
            var password = document.getElementById('password').value;
            address = address.replace(/(\s*$)/g, "");
            password = password.replace(/(\s*$)/g, "");
            if (address == null) {
                alert("�������ʺţ�");
                return false;
            }
            if (password == null) {
                alert("���������룡");
                return false;
            }

            var reg = /^([\.a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((\.[a-zA-Z0-9_-]{2,3}){1,2})$/;
            flag = reg.test(address);
            if (flag) {
                document.forms[0].action = "/Account/IM.aspx?type=0";
                document.forms[0].submit();
                setTimeout(ref, 10000);
            }
            else {
                alert("��������ȷ���˺ţ�");
                return false;
            }
            
        }
        
        function ref(){  
            window.location.href="/Account/IM.aspx";  
        }   
    </script>
    
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="margin: 10px 0 10px 0;">
        <tr>
            <td class="nav">
                <table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr><td class="title">����</td></tr>
                </table>
                <table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr><td width="30%" valign="top" class="left">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr><td height="40" align="right"><a href="/account/settings.aspx">��������</a></td></tr>
                                <tr><td height="40" align="right">������</td></tr>
                            </table>
                        </td>
                        <td width="70%" valign="top" class="right">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr><td height="40" colspan="2" class="btn">�������</td>
                                </tr>
                                <tr>
                                    <td>
                                       <asp:Panel ID="pnlReBinding" runat="server">
                                            <p>���Ѿ�����MSN������Ҫ���°���������İ�ť��</p>
                                            <asp:Button ID="btnRe" Text="���°�" runat="server" CssClass="enterbtn" 
                                                onclick="btnRe_Click"/>
                                       </asp:Panel>
                                       <asp:Panel ID="pnlBinding" runat="server">
                                           <div class="binding">
                                                <p>
                                                    ͨ��MSN��������Ϳ��Խ��պ͸��������Ϣ�����ھ����󶨰ɡ�</p>
                                                <form id="Form2" action="/Account/IM.aspx?type=0" method="post" class="validator">
                                                    <p style="margin-bottom: 25px;">
                                                    �����ʺţ�<input type="text" id="Text1" name="address" check="null" alt="�ʺ�" class="input" />&nbsp;<br/>
                                                    ��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;�룺<input type="password" id="password1" name="password" check="null" alt="�ʺ�" class="input" />
                                                    &nbsp;&nbsp;<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <input type="button" class="enterbtn" id="btnsub" value="��" onclick="banding();" runat="server"/>
                                                    <span id="pnlBtnCancel" runat="server">
                                                        <asp:Button ID="btnCancel" Text="ȡ��" runat="server" CssClass="enterbtn" 
                                                            onclick="btnCancel_Click"/>
                                                    </span>
                                                    <div class="bindingIM">
                                                    <span class="floatright personalizedText">MSN��ʱ����ʾ��<span class="black12">�����Ѿ���һ̨������ϵ�¼�ˡ�</span>���������ĵȴ�<span class="black12">1����</span>�����µ�¼</span></div>
                                                </form>
                                            </div>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
<%--    <div id="container">
        <p class="top">
            ����</p>
        <div id="wtMainBlock">
            <div class="leftdiv">
                <ul class="leftmenu">
                    <li><a href="/account/settings.aspx">��������</a></li>
                    <li><a href="/Account/IM.aspx" class="now">������</a></li>
                </ul>
            </div>
            <!-- leftdiv -->
            <div class="rightdiv">
                <div class="lookfriend">
                    <p class="right14">
                        &nbsp;&nbsp;<a href="/Account/IM.aspx" class="now">�������</a>&nbsp;
                     </p>
                    <div class="binding">
                        <p>
                            ͨ��MSN��������Ϳ��Խ��պ͸��������Ϣ�����ھ����󶨰ɡ�</p>
                        <form id="f" action="/Account/IM.aspx?type=0" method="post" class="validator">
                            <p style="margin-bottom: 25px;">
                            �����ʺ�<input type="text" id="address" name="address" check="null" alt="�ʺ�" class="inputStyle" />&nbsp;<br />
                            ��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��<input type="password" id="password" name="password" check="null" alt="�ʺ�" class="inputStyle" />
                            &nbsp;&nbsp;
                            <input type="button" class="submitbutton" id="btnsub" value="��" onclick="banding();" runat="server"/>
                            
                            <div class="bindingIM">
                            <span class="floatright personalizedText">MSN��ʱ����ʾ��<span class="black12">�����Ѿ���һ̨������ϵ�¼�ˡ�</span>���������ĵȴ�<span class="black12">1����</span>�����µ�¼</span></div>
                        </form>
                    </div>
                    <div style="overflow: hidden; clear: both; height: 50px; line-height: 1px; font-size: 1px;">
                    </div>
                </div>
                <!-- lookfriend -->
                <div style="overflow: hidden; clear: both; height: 50px; line-height: 1px; font-size: 1px;">
                </div>
            </div>
            <!-- rightdiv -->
        </div>
        <!-- #wtMainBlock -->
        <div style="overflow: hidden; clear: both; height: 7px; line-height: 1px; font-size: 1px;">
        </div>
    </div>
    <!-- #container -->--%>
    
</asp:Content>
