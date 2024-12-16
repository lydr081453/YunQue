<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgencyDisplay.aspx.cs" Inherits="MediaWeb.NewMedia.BaseData.AgencyDisplay"  MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="cntHead" ContentPlaceHolderID="contentHead" runat="server">

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">
    <input type="hidden" id="hidUrl" runat="server" />
    <input type="hidden" id="hidMediaId" runat="server" value="0" />
    <table style="width: 100%;">
        <tr>
            <td>
                <table style="width: 100%;" border="0">
                    <tr>
                        <td colspan="4">
                            <table style="width: 100%;" border="1" class="tableForm">
    <tr>
        <td colspan="4" class="menusection-Packages">
            ������Ϣά��
        </td>
    </tr>
    <tr>
        <td colspan="4" class="heading">
            ������Ϣ
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 20%">
            �����������ƣ�
        </td>
        <td class="oddrow-l" style="width: 30%">
            <asp:Label ID="labAgencyName" runat="server" MaxLength="50"></asp:Label>
        </td>
         <td class="oddrow" style="width: 20%">
            ����Ӣ�����ƣ�
        </td>
        <td class="oddrow-l" style="width: 30%">
            <asp:Label ID="labAgencyEngName" runat="server" MaxLength="50"></asp:Label>
        </td>   
    </tr>
    <tr>
        <td class="oddrow" style="width: 20%">
            ����ý�壺
        </td>
        <td class="oddrow-l" style="width: 30%">
            <asp:Label ID="lblMediaName" runat="server" MaxLength="50"></asp:Label>
        </td>
         <td class="oddrow" style="width: 20%">
        </td>
        <td class="oddrow-l" style="width: 30%">
        </td>   
    </tr>
    <tr>        
        <td class="oddrow">
            �������ԣ�
        </td>
        <td class="oddrow-l">
            <asp:Label ID="labRegionAttribute" runat="server" />
        </td>
        <td class="oddrow" >
            ���ң�
        </td>
        <td class="oddrow-l" >
            <asp:Label ID="labCountry" runat="server"/>            
        </td>
    </tr>    
    <tr >
                        <td class="oddrow">
                            ʡ��
                        </td>
                        <td class="oddrow-l" >
                            <asp:Label ID="labProvince"  runat="server" />                            
                        </td>
                        <td class="oddrow" >
                            �У�
                        </td>
                        <td class="oddrow-l"  >
                            <asp:Label ID="labCity" runat="server"/>
                            
                        </td>
                    </tr>
    <tr>
        <td class="oddrow">
            �����ˣ�
        </td>
        <td class="oddrow-l">
            <asp:Label ID="labResponsiblePerson" runat="server" MaxLength="20"></asp:Label>
        </td>
        <td class="oddrow">
            ��ϵ�ˣ�
        </td>
        <td class="oddrow-l">
            <asp:Label ID="labContacter" runat="server" MaxLength="20"></asp:Label>
        </td>
    </tr>   

    <tr>
        <td colspan="4" class="heading">
            ��ϵ��ʽ�����ڵ�ַ
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            �ܻ���
        </td>
        <td class="oddrow-l">
            <asp:Label ID="labTelephoneExchange" runat="server" MaxLength="20"></asp:Label>           
        </td>
        <td class="oddrow">
            ���棺
        </td>
        <td class="oddrow-l">            
            <asp:Label ID="labFax" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            ����1��
        </td>
        <td class="oddrow-l">            
            <asp:Label ID="labPhoneOne" runat="server"></asp:Label>
        </td>
        <td class="oddrow">
            ����2��
        </td>
        <td class="oddrow-l">            
            <asp:Label ID="labPhoneTwo" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            ��ϸ��ַ��
        </td>
        <td class="oddrow-l">
            <asp:Label ID="labAddress1" runat="server" MaxLength="50"></asp:Label>
        </td>
        <td class="oddrow">
            �������룺
        </td>
        <td class="oddrow-l">
            <asp:Label ID="labPostCode" runat="server" MaxLength="10" />
        </td>
    </tr>
    <tr>
        <td colspan="4" style="height: 30px">
        </td>
    </tr>
    <tr>
        <td colspan="4" class="heading">
            ������Ϣ
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            ������飺
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label ID="labAgencyIntro" runat="server" TextMode="MultiLine" Height="98px"
                Width="80%"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            Ӣ�ļ�飺
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label ID="labEngIntro" runat="server" TextMode="MultiLine" Height="98px" Width="80%"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            ��ע��
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label ID="labRemarks" runat="server" TextMode="MultiLine" Height="98px" Width="80%"></asp:Label>
        </td>
    </tr>
<tr>
            <td colspan="4" style="text-align: right">                
                                
                <asp:Button ID="btnBack" runat="server" Text="����" CssClass="widebuttons" OnClick="btnBack_Click" />
            </td>
            <asp:HiddenField ID="hidBackUrl" runat="server" />
            <asp:HiddenField ID="hidMid" runat="server" />
        </tr>
    <input type="hidden" id="RoleColl" runat="server">
    <input type="hidden" id="hidCityAddr1" runat="server">
    <input type="hidden" id="hidCity" runat="server">
    <input type="hidden" id="hidPro" runat="server">
    <input type="hidden" id="hidCountry" runat="server">
</table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td style="height:25px" />
        </tr>        
    </table>
</asp:Content>

