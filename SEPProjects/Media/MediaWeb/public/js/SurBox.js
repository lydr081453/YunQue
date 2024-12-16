/* SuranceBox 0.9
 * Required: http://jquery.com/
 * Written by: Surance
 * Website: www.fltek.com.cn 
 * Email:suranceyin@yahoo.com.cn
 * License: LGPL 
 */

//ȫ�ֱ��� �ɸ�Ĭ�ϵ�����
var SUR_FIRST = true;
var SUR_HEIGHT = 450;
var SUR_WIDTH = 450;
var SUR_WEB = 'http://www.fltek.com.cn/';
var SUR_Refresh = false; //�Ƿ�رպ�Ҫˢ��
var SUR_OverClick = true; //�Ƿ���Ե㴰�������ر�

//��չ����
//��ʾ����
//url:��ַ
//height:�߶�
//width�����
//refreshable:�Ƿ�رպ�Ҫˢ��
//overclickalbe:�Ƿ���Ե㴰�������ر�
function SUR_show_ex(url, height, width,refreshable,overclickalbe)
{
	SUR_Refresh = refreshable;
	SUR_OverClick = overclickalbe;
	SUR_show(url, height, width)
}

//Ĭ�Ϸ���
//��ʾ����
//url:��ַ
//height:�߶�
//width�����
function SUR_show(url, height, width)
{
  SUR_HEIGHT = height || SUR_HEIGHT;
  SUR_WIDTH = width || SUR_WIDTH;
  //alert(SUR_WIDTH);
  SUR_WEB = "http://www.fltek.com.cn/";
  if(SUR_FIRST) 
  {
    $(document.body).append("<div id='SUR_overlay' style='filter:alpha(opacity=50, Style=0);opacity: 0.50;'></div><div id=SUR_container><div id='SUR_window'><iframe id='SUR_frame' src='http://www.fltek.com.cn/'></iframe></div><div id='SUR_caption'><img src='../images/close.png' alt='�� ��1'/></div></div>");
    $("#SUR_caption img").click(SUR_hide);
		if(SUR_OverClick)
		{
			$("#SUR_overlay").click(SUR_hide);
		}

    $(window).resize(SUR_calPosition);
		SUR_calOverLayerHeight();
    SUR_FIRST = false;
  }
  
  SUR_WEB = url;
  $("#SUR_frame").attr("src",SUR_WEB);

  $("#SUR_overlay").show();
  SUR_calPosition();

  $("#SUR_container").show("slow");
  $("select").css('visibility','hidden');

}

//���ط���
function SUR_hide()
{
  $("#SUR_container").hide();
  $("#SUR_overlay").hide();
  $("select").css('visibility','visible');
  if(SUR_Refresh)
  {
		window.location.reload();  
   }
}

//����λ��
function SUR_calPosition() 
{
  var doc = document.documentElement;
  var w = self.innerWidth || (doc&&doc.clientWidth) || document.body.clientWidth;
  var h = self.innerHeight ||(doc&&doc.clientHeight) ||document.body.clientHeight;
  
  $("#SUR_container").css({width:SUR_WIDTH+"px",height:SUR_HEIGHT+"px",
    left: ((w - SUR_WIDTH)/2)+"px",top:((h - SUR_HEIGHT)/2-30)+"px"});
  $("#SUR_frame").css("height",SUR_HEIGHT - 40 +"px");
  
  doc.setAttribute("scrollTop",0);
  
}

function SUR_calOverLayerHeight()
{
	//var h = document.body.clientHeight||600;
	var h1 = document.body.clientHeight +200;
	var w1 = document.body.clientWidth+ 40;
	var h2 = window.screen.availHeight;
	var w2 = window.screen.availWidth;
	//alert(h);
	var h,w;
	if(h1>h2)
		h=h1;
	else
		h=h2;
	
	if(w1>w2)
		w=w1;
	else
		w=w2;
	$("#SUR_overlay").css("height",h+"px");
	$("#SUR_overlay").css("width",w+"px");
}
