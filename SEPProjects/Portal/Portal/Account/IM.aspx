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
                alert("请输入帐号！");
                return false;
            }
            if (password == null) {
                alert("请输入密码！");
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
                alert("请输入正确的账号！");
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
                    <tr><td class="title">设置</td></tr>
                </table>
                <table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr><td width="30%" valign="top" class="left">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr><td height="40" align="right"><a href="/account/settings.aspx">基本资料</a></td></tr>
                                <tr><td height="40" align="right">绑定设置</td></tr>
                            </table>
                        </td>
                        <td width="70%" valign="top" class="right">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr><td height="40" colspan="2" class="btn">聊天软件</td>
                                </tr>
                                <tr>
                                    <td>
                                       <asp:Panel ID="pnlReBinding" runat="server">
                                            <p>您已经绑定了MSN，如需要重新绑定请点击下面的按钮。</p>
                                            <asp:Button ID="btnRe" Text="重新绑定" runat="server" CssClass="enterbtn" 
                                                onclick="btnRe_Click"/>
                                       </asp:Panel>
                                       <asp:Panel ID="pnlBinding" runat="server">
                                           <div class="binding">
                                                <p>
                                                    通过MSN聊天软件就可以接收和更新你的消息，现在就来绑定吧。</p>
                                                <form id="Form2" action="/Account/IM.aspx?type=0" method="post" class="validator">
                                                    <p style="margin-bottom: 25px;">
                                                    输入帐号：<input type="text" id="Text1" name="address" check="null" alt="帐号" class="input" />&nbsp;<br/>
                                                    密&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;码：<input type="password" id="password1" name="password" check="null" alt="帐号" class="input" />
                                                    &nbsp;&nbsp;<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <input type="button" class="enterbtn" id="btnsub" value="绑定" onclick="banding();" runat="server"/>
                                                    <span id="pnlBtnCancel" runat="server">
                                                        <asp:Button ID="btnCancel" Text="取消" runat="server" CssClass="enterbtn" 
                                                            onclick="btnCancel_Click"/>
                                                    </span>
                                                    <div class="bindingIM">
                                                    <span class="floatright personalizedText">MSN绑定时会提示您<span class="black12">“您已经另一台计算机上登录了”</span>，请您耐心等待<span class="black12">1分钟</span>后重新登录</span></div>
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
            设置</p>
        <div id="wtMainBlock">
            <div class="leftdiv">
                <ul class="leftmenu">
                    <li><a href="/account/settings.aspx">基本资料</a></li>
                    <li><a href="/Account/IM.aspx" class="now">绑定设置</a></li>
                </ul>
            </div>
            <!-- leftdiv -->
            <div class="rightdiv">
                <div class="lookfriend">
                    <p class="right14">
                        &nbsp;&nbsp;<a href="/Account/IM.aspx" class="now">聊天软件</a>&nbsp;
                     </p>
                    <div class="binding">
                        <p>
                            通过MSN聊天软件就可以接收和更新你的消息，现在就来绑定吧。</p>
                        <form id="f" action="/Account/IM.aspx?type=0" method="post" class="validator">
                            <p style="margin-bottom: 25px;">
                            输入帐号<input type="text" id="address" name="address" check="null" alt="帐号" class="inputStyle" />&nbsp;<br />
                            密&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;码<input type="password" id="password" name="password" check="null" alt="帐号" class="inputStyle" />
                            &nbsp;&nbsp;
                            <input type="button" class="submitbutton" id="btnsub" value="绑定" onclick="banding();" runat="server"/>
                            
                            <div class="bindingIM">
                            <span class="floatright personalizedText">MSN绑定时会提示您<span class="black12">“您已经另一台计算机上登录了”</span>，请您耐心等待<span class="black12">1分钟</span>后重新登录</span></div>
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
