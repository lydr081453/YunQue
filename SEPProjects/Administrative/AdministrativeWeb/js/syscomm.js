
var STATUS_INSERT = 1;
var STATUS_UPDATE = 2;
var STATUS_DELETE = 3;
var STATUS_SUBMIT = 4;
var STATUS_DESTROY = 5;

var TYPE_STRING = 1;
var TYPE_NUMBER = 2;
var TYPE_DATE = 3;

var SQLOPER_MORE = 1;
var SQLOPER_MOREOREQUAL = 2;
var SQLOPER_EQUAL = 3;
var SQLOPER_LESS = 4;
var SQLOPER_LESSOREQUAL = 5;
var SQLOPER_UNEQUAL = 6;
var SQLOPER_BETWEEN = 7;
var SQLOPER_IN = 8;
var SQLOPER_LIKE = 9;
var SQLOPER_UNLIKE = 10;
var SQLOPER_UNBETWEEN = 11;
var SQLOPER_UNIN = 12;

var SQLORDER_ASC = 1;
var SQLORDER_DESC = 2;
	

var IDENTITY_MSG = '[�Զ�����]';

var LOGON_URL = 'logon.jsp';

var ERRCODE_TIMEOUT = '999';
var ERRCODE_NOTUNIQ = '100';
var ERRCODE_DEFAULT = '-1';

var ERR_CODE = 'errorCode';
var ERR_DESCRIPTION = 'description';


function GetSelIndex(spValue,opSel)
{
	for(var i=0;i<opSel.length;i++)
	{
		if(opSel.item(i).value==spValue) return i;
	}
	
	return -1;
}


//General Select innerHTML by xml string

function SetSelectInnerHTML(objXml,objSel,sValue,sDisplay)
{
	try{
		var objOption;
		if(typeof(objXml)!='object') 
		{
			objOption = document.createElement("option");
			objOption.text = "Error in getting MacType data!";
			objSel.add(objOption);
			return;
		}
		
		objSel.innerHTML = '';
		
		var objNodes = objXml.selectNodes("//row");
		for(var i=0;i<objNodes.length;i++)
		{
			objOption = document.createElement("option");
			objOption.text = objNodes.item(i).selectSingleNode(sDisplay).text.trim();
			objOption.value = objNodes.item(i).selectSingleNode(sValue).text.trim();
			objSel.add(objOption);
		}	
	}catch(e)
	{
		alert("������ʾ : " + e.description);
	}	
}



function GetXmlError(objXml)
{
try{
	
	if(objXml.parseError==null) return;
	
	if(objXml.parseError.errorCode==0) return;
	
	var sMsg = 'XML document error.\n'
				  + 'doc path:' + objXml.parseError.url + '\n'
				  + 'line:' + objXml.parseError.line + '\n'
				  + 'column:' + objXml.parseError.linepos + '\n'
				  + 'position:' + objXml.parseError.filepos + '\n'
				  + 'char:' + objXml.parseError.srcText + '\n'
				  + 'error code:' + objXml.parseError.errorCode + '\n'
				  + 'description:' + objXml.parseError.reason;
	return sMsg;	
}catch(e)
{
	alert('system.getXmlEncode\n' + e.description);
}
}

function GetRuntimeError(e)
{
try{
	var sMsg = '����ʱ����.\n'
			+ '����λ��:' + e.number + '\n'
			+ '��������:' + e.name + '\n'
			+ '��������:' + e.description; 
	return sMsg;	
}catch(e)
{
	alert('system.getRuntimeError\n' + e.description);
}
}

function GetAttributeXml(obj)
{
	try{
	var oAttribs = obj.attributes;

	var sXml;

    	// Iterate through the collection.
    	for (var i = 0; i < oAttribs.length; i++)
    	{
        		var oAttrib = oAttribs[i];

        		// Print the name and value of the attribute. 
        		// Additionally print whether or not the attribute was specified
        		// in HTML or script.
        		sXml = '<attributes>'
        		sXml += '<' + oAttrib.nodeName + '>' + oAttrib.nodeValue + ' (' + oAttrib.specified + ')</' + oAttrib.nodeName + '></attributes>'; 
   	 }
	
	return sXml;
	}catch(e)
	{
		alert(getRuntimeError(e));
	}
}



function GetCell(obj)
{
try{
	//Get the obj's parent td object
	if(!isCellChild(obj)) return null;
	
	if(obj.tagName.toUpperCase()=='TD') return obj;
	while(obj.tagName.toUpperCase()!='TR')
	{
		obj = obj.parentElement;
		if(obj==null) return null;
		
		if(obj.tagName.toUpperCase()=='TD') return obj;
	}
	
	return null;
}catch(e)
{
	alert('system.getCell\n' + e.desription);
}
}


function IsCellChild(obj)
{
	// Check if the obj is a child of the td
	
	if(obj.tagName.toUpperCase()=='TD') return true;
	
	while(obj.tagName.toUpperCase()!='TABLE')
	{
		obj = obj.parentElement;
		if(obj==null) return null;
		
		if(obj.tagName.toUpperCase()=='TD') return true;
	}
	
	return false;
	
}

function GetCellParentTable(obj)
{
	//Get the obj's parent table object
	if(!isCellChild(obj)) return null;
	
	while(obj.tagName.toUpperCase()!='TABLE') {obj=obj.parentElement;}
	
	if(obj.tagName.toUpperCase()=='TABLE')
	{
		return obj
	}
	else
	{
		return null;
	}
	
}


//Append the trim function to stirng object
String.prototype.trim = function()
{
    // Use a regular expression to replace leading and trailing 
    // spaces with the empty string
    return this.replace(/(^\s*)|(\s*$)/g, "");
}

function ShowMessageBox(spCaption,spContext,ipButton)
{
	if(spCaption=='') spCaption = '��ʾ��Ϣ';
	if(spContext=='') spContext = 'ȷ��?';
	if(ipButton=='') ipButton = 0;
	
	var objXml = new ActiveXObject("Microsoft.XMLDOM");
	objXml.async = false;
	objXml.loadXML("<msgbox><caption>" + xmlEncode(spCaption) +  "</caption><context>" + xmlEncode(spContext) + "</context><button>" + xmlEncode(ipButton) +"</button></msgbox>");
	
	if(objXml.parseError.errorCode!=0)
	{
		alert(getXmlError(objXml));
		return;
	}
	
	return window.showModalDialog("/public/page/confirm.htm",objXml,"dialogHeight: 180px; dialogWidth: 320px; dialogTop: px; dialogLeft: px; center: Yes; help: No; resizable: No; status: No;");
	
	
}


function GetPositionArr(sStr,iArr)
{
	var iInc = 0;
	for(var i = 0;i<sStr.length;i++)
	{
		if(sStr.charAt(i)=='1')
		{
			iArr[iInc] = i + 1;
			iInc ++;
		}
	}
}

// �ж�sStr�ַ����Ƿ�ȫ��������
function CheckStrFormat(sStr,iType)
{
	switch(iType)
	{
		case 1:	// numeric
			for(var i=0;i<sStr.trim().length;i++)
			{
				if(sStr.charCodeAt(i)<48 || sStr.charCodeAt(i) > 57)		// �ַ����벻��48-57�����ַ�Χ
				{
					return false;
				}
			}
			
			return true;
			break;
		case 2: // numeric and char
			for(var i=0;i<sStr.trim().length;i++)
			{
				if(sStr.charCodeAt(i)==0)		// �ַ����벻�����ֻ�����ĸ
				{
					return false;
				}
			}
			return true;
			break;
	}
}

//xmlEncode
function XmlEncode(sStr)
{
	if(sStr=='' || sStr == null)
	{
		return '';
	}
	
	var sRet = '';
	
	for(var i =0;i<sStr.length;i++)
	{
		switch(sStr.substr(i,1))
		{
			case '&':
				sRet += '&amp;';
				break;
			case '"':
				sRet += '&quot;';
				break;
			case "'":
				sRet += '&apos;';
				break;
			case '<':
				sRet += '&lt;';
				break;
			case '>':
				sRet += '&gt;';
				break;
			default:
				sRet += sStr.substr(i,1);
		}
	}
	
	return sRet;
}

function XmlReverseEncode(strXml)
{
	if(strXml=="" || strXml == null)
	{
		return "";
	}
	
	strXml = strXml.replace(/\&gt;/g,">");
	strXml = strXml.replace(/\&lt;/g,"<");
	strXml = strXml.replace(/\&apos;/g,"\'");
	strXml = strXml.replace(/\&quot;/g,"\"");
	strXml = strXml.replace(/\&amp;/g,"&");
	
	return strXml;
}

function DoubleQuote(sStr)
{
	if(sStr==null)	return '';
	var sRet = '';
	
	for(var i=0;i<sStr.length;i++)
	{
		if(sStr.substr(i,1)=='"')
		{
			sRet += '\"';
		}
		else
		{
			sRet += sStr.substr(i,1); 
		}
	}
	
	return sRet;
}

function XPathEncode(sStr)
{
	if(sStr==null) return '';
	var sRet = '';
	
	for(var i=0;i<sStr.length;i++)
	{
		switch(sStr.substr(i,1))
		{
			case '\'':
				sRet += '\\' + '\'';
				break;
			case '\"':
				sRet += '\\' + '\"';
				break;
			default:
				sRet += sStr.substr(i,1);
		}
	}
	
	return sRet;
}


function IsEMail (s)//��֤E-MAIL��ʽ����
{
        
        if (s.length > 100)
        {
                window.alert("email��ַ���Ȳ��ܳ���100λ!");
                return false;
        }

         var regu = "^(([0-9a-zA-Z]+)|([0-9a-zA-Z]+[_.0-9a-zA-Z-]*[0-9a-zA-Z]+))@([a-zA-Z0-9-]+[.])+([a-zA-Z]{2}|net|NET|com|COM|gov|GOV|mil|MIL|org|ORG|edu|EDU|int|INT)$"
         var re = new RegExp(regu);
         if (s.search(re) != -1) 
         {
               return true;
         } else 
         {
               return false;
         }
}


// ��ҳ����ѡ������
function SelectDateFromPage(objTarget)
{
	try{
		var sDate = window.showModalDialog("/public/page/CalendarSel.aspx",null,"dialogHeight: 190px; dialogWidth: 150px; dialogTop: px; dialogLeft: px; center: Yes; help: No; resizable: No; status: No;");
		
		if(typeof(sDate)=="undefined") return;
		
		if(sDate.trim()!="")		objTarget.value = sDate;
	}
	catch(e)
	{
		//alert(e.description);
	}
}

//��ҳ����ѡ����Ա
function SelectUserFromPage()
{
	try{
	var oXml = new ActiveXObject("Microsoft.XMLDOM");
	oXml.loadXML(window.showModalDialog("/public/page/UserChoice.aspx",null,"dialogHeight: 590px; dialogWidth: 450px; dialogTop: px; dialogLeft: px; center: Yes; help: No; resizable: Yes; status: No;"));
	
	if(oXml.parseError.errorCode==0)
	{
		return oXml;
	}
	}
	catch(e)
	{
		//
	}
}

