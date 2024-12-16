
function msg(message)
{
    message = "\n智能卡数据管理自动化系统信息提示：\n\n======================================           \n\n"+message;
    message += "\n\n======================================           ";
    alert(message);
    //var sFeatures = "resizable:no;scroll:no;status:no;dialogHeight:15;dialogWidth:20"
    //window.showModalDialog("/common/msg/message.html",message,sFeatures)
}

/**
 * 打开一个对话框。
 * @param argument可以为 null;
 * @param url
 */
function showWin(url,argument)
{
    var sFeatures = "resizable:no;scroll:auto;status:no;dialogHeight:30;dialogWidth:40"
    var rets = window.showModalDialog(url,argument,sFeatures);
    return rets;
}

/**
 * 打开一个对话框。
 * @param argument可以为 null;
 * @param url
 */
function openWin(url)
{
    var sFeatures = "toolbar=no,width=620,height=400,directories=no,status=no,scrollbars=yes,resize=no,menubar=no"
    var rets = window.open(url,"",sFeatures);
    return rets;
}

function focusNext(srcObj)
{
    if(event.keyCode==13)
    {
        event.keyCode = 9;
        return;
    }
}

var formcheck = true;
function doCheckForm(form)
{
	alert("aaa");
    if(formcheck == false) return true;//是否做验证的开关。调试用。
    isNotSubmit = false;
    var elements = form.elements;   
    var len = elements.length;

    var ErrMsg = "";
    var ErrCount = 0;
    

    for(var i=0;i< len ;i++ )
    {
        var field = elements[i];
        //加入password by qubo 2004.09.04
        if(field.type=="text" || field.type=="textarea"|| field.type=="file"|| field.type=="hidden"|| field.type=="password")
        {
            if(field.isMust=="1" && field.value=="")
            {
                ErrMsg = ErrMsg + ++ErrCount +". ["+field.title+"] 必须录入，谢谢合作!\n\n";
            }
            if(field.maxLength-0 < RealLength(field.value))
            {
                ErrMsg += (++ErrCount)+". ["+field.title+"] 字段不能超过"+field.maxLength+"个字节!\n\n";
            }

            //如果是空就检查下一个。
            if(field.value=="" ) continue;
            switch (field.dataType){
                case "Email":
                    if(!isEmail(field.value))
                    {
                        ErrMsg += (++ErrCount)+". ["+field.title+"] 必须为合法的Email地址!\n\n";
                    }
                    break;
                case "URL":// not finish
                    if(!IsURL(field.value))
                    {
                        ErrMsg += (++ErrCount)+". ["+field.title+"] 必须以http://开头的合法字符!\n\n";
                        
                    }
                    break;
                case "Telephone":
                    if(!IsTelephone(field.value))
                    {
                        ErrMsg += (++ErrCount)+". ["+field.title+"] 必须是正确的电话号码!\n\n";
                    }
                    break;
                case "PostCode":
                    if(!isPostCode(field.value))
                    {
                        ErrMsg += (++ErrCount)+". ["+field.title+"] 必须为合法的邮政编码!\n\n";
                    }
                    break;
                case "Date":
                    if(!isDate(field.value))
                    {
                        ErrMsg += (++ErrCount)+". ["+field.title+"] 必须是格式为[YYYY-MM-DD]的合法日期\n\n";
                    }//*/
                    break;
                case "Int":
                    if(!isInt(field.value))
                    {
                        ErrMsg += (++ErrCount)+". ["+field.title+"] 必须是数字!\n\n";
                        
                    }
                    break;
                case "Int2":
                    if(!isInt2(field.value))
                    {
                        ErrMsg += (++ErrCount)+". ["+field.title+"] 必须是大于 0 的整数!\n\n";
                        
                    }
                    break;
                case "Int3_A":
                    if(!isInt3(field.value))
                    {
                        ErrMsg += (++ErrCount)+". ["+field.title+"] 必须是大于 0 并小于等于 100000 的数字!\n\n";
                        
                    }
                    break;
                case "Int3_B":
                    if(!isInt4(field.value))
                    {
                        ErrMsg += (++ErrCount)+". ["+field.title+"] 必须是大于 0 并小于等于 10000 的数字!\n\n";
                        
                    }
                    break;
                case "Float":
                    if(!isFloat(field.value))  //float value
                    {
                        ErrMsg += (++ErrCount)+". ["+field.title+"] 必须是实数!\n\n";
                        
                    }//*/
                    break;
                case "LetterDigit":
                   if(!isValid(field.value))
                   {
                        ErrMsg += (++ErrCount)+". ["+field.title+"] 只能是数字和字母!\n\n";
                                            
                   }
                   break;
                case "Letter":
                   if(!isLetter(field.value))
                   {
                        ErrMsg += (++ErrCount)+". ["+field.title+"] 只能是字母!\n\n";
                                            
                   }
                   break;
                case "UpperCase":
                   if(!isUpperCase(field.value))
                   {
                        ErrMsg += (++ErrCount)+". ["+field.title+"] 只能是大写字母!\n\n";
                                            
                   }
                   break;
                case "RightString":
                   if(!isValidChar(field.value))
                   {
                        ErrMsg += (++ErrCount)+". ["+field.title+"] 请输入合法字符!\n\n";
                                            
                   }
                   break;
                case "RightString2":
                   if(!isValidChar2(field.value))
                   {
                        ErrMsg += (++ErrCount)+". ["+field.title+"] 请输入合法字符!\n\n";
                                            
                   }
                   break;
                case "Int2B":
                    if(!isNInt(field.value,2))
                    {
                        ErrMsg += (++ErrCount)+". ["+field.title+"] 必须是2位数字!\n\n";
                        
                    }
                    break;
                case "Int3":
                    if(!isNInt(field.value,3))
                    {
                        ErrMsg += (++ErrCount)+". ["+field.title+"] 必须是3位数字!\n\n";
                        
                    }
                    break;
                case "Int4":
                    if(!isNInt(field.value,4))
                    {
                        ErrMsg += (++ErrCount)+". ["+field.title+"] 必须是4位数字!\n\n";
                        
                    }
                    break;
                case "path":
                   if(!isPath(field.value))
                   {
                        ErrMsg += (++ErrCount)+". ["+field.title+"] 请输入相对路径!\n\n";
                                            
                   }
                   break;
                case "RightString4":
                   if(!(isValidChar(field.value)&& RealLength(field.value)==4) )
                   {
                        ErrMsg += (++ErrCount)+". ["+field.title+"] 请输入4位合法字符!\n\n";
                                            
                   }
                   break;
                case "RightString2to4":
                   if(!(isValidChar(field.value)&& RealLength(field.value)<=4 && RealLength(field.value)>=2 ) )
                   {
                        ErrMsg += (++ErrCount)+". ["+field.title+"] 请输入2到4位合法字符!\n\n";
                                            
                   }
                   break;  
                case "RightString16":
                   if(!(isValidChar(field.value)&& RealLength(field.value)==16) )
                   {
                        ErrMsg += (++ErrCount)+". ["+field.title+"] 请输入16位合法字符!\n\n";
                                            
                   }
                   break;
                case "NumChar":
                    if(!(isNumChar(field.value) && RealLength(field.value)==4))
                    {
                        ErrMsg += (++ErrCount)+". ["+field.title+"] 请输入4位合法字符!\n\n";
                    }
                   break;
                case "NumChar2":
                    if(!isNumChar2(field.value))
                    {
                        ErrMsg += (++ErrCount)+". ["+field.title+"] 请输入合法字符!\n\n";
                    } 
                   break;
                case "Int5":
                    if(!isInt5(field.value))
                    {
                        ErrMsg += (++ErrCount)+". ["+field.title+"] 必须是数字,可包含“+”号!\n\n";
                    }
                   break;
                case "isChar":
                    if(!isChar(field.value))
                    {
                        ErrMsg += (++ErrCount)+". ["+field.title+"] 请输入合法字符!\n\n";
                    }  
                   break;
                case "isChar2to4":
                    if(!(isChar(field.value)&& RealLength(field.value)<=4 && RealLength(field.value)>=2 ) )
                    {
                        ErrMsg += (++ErrCount)+". ["+field.title+"] 请输入2到4位合法字符!\n\n";
                    }  
                   break;                            
            }//end switch       

        }//end if
        
        if(field.type=="select-one")
        {
            if(field.isMust=="1" && (field.value=="" || field.value == null))
            {
                ErrMsg += (++ErrCount)+". ["+field.title+"] 必须录入，谢谢合作!\n\n";
            }
        }
    }//end for

    if(ErrMsg.length > 1 )
    {
        msg(ErrMsg);
        return false;
    }

    return true;
}

function Is0Start(value)
{
   if (value.substring(0,1)=="0")
   {
     if( isNInt(value,2) )
     {
        return true;
     }   
   }
   return false;
}

function IsURL(value)
{
    return (value.substring(0,7)=="http://")
}
            
//location : 1
//检查是否是大写字母！
//参数： 字符串
//返回一个boolean值！
function isUpperCase(value){
    var returnValue = true;
    var re =  new RegExp("^([A-Z]*)$");
    if(value.search(re) == -1)
    {
    returnValue=false;
    }
    return returnValue;
}


//检查是否为N位是数字！含0
//参数： 字符串，字符串
//返回一个boolean值！
function isNInt(value,N){
    if( value.length == N )
    {
        if( isInt(value) )
        {
            return true;
        }
    }
    return false;
}
            
//location : 1
//检查是否是数字！含0
//参数： 字符串
//返回一个boolean值！
function isInt(value){
    var returnValue = true;
    var re =  new RegExp("^([0-9]+)$");
    if(value.search(re) == -1)
    {
    returnValue=false;
    }
    return returnValue;
}

function isInt2(value){
    var returnValue = true;
    var re =  new RegExp("^([1-9][0-9]*)$");
    if(value.search(re) == -1)
    {
    returnValue=false;
    }
    return returnValue;
}

function isInt3(value){
    var returnValue = true;
    var re =  new RegExp("^([1-9][0-9]*)$");
    if(value.search(re) == -1)
    {
    returnValue=false;
    }
    if(value > 100000)
    {
    returnValue=false;
    }
    return returnValue;
}

function isInt4(value){
    var returnValue = true;
    var re =  new RegExp("^([1-9][0-9]*)$");
    if(value.search(re) == -1)
    {
    returnValue=false;
    }
    if(value > 10000)
    {
    returnValue=false;
    }
    return returnValue;
}

//检查是否为合法数字
function isFloat(value){
    var returnValue = true;
    var re =  new RegExp("^(([0-9]+)|([0-9]+.[0-9]+))$");
    if(value.search(re) == -1)
    {
    returnValue=false;
    }
    return returnValue;
}

// 是否为合法价格，只要应用于成交系统
// 将来可能需要：最高价/最低价区间限制判断
// 返回boolean值
function isPrice(value){
    var returnValue = true;
    fValue = parseFloat(value);
    if(fValue + "" == "NaN")
    {
        returnValue=false;
    } 
    if (fValue <= 0) {
        returnValue=false; // 价格必须大于0
    }
    return returnValue;
}

//检查是否为合法数字
function isLetter(value){
    var returnValue = true;
    var re =  new RegExp("^([A-Za-z]*)$");
    if(value.search(re) == -1)
    {
    returnValue=false;
    }
    return returnValue;
}
//location : 2
//求判断一个字符是否是ASCII值
//cValue：参数值
//返回一个boolean值！
function isASCII( cValue )
{
    var sFormat = " !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";
    var iLocation = sFormat.indexOf( cValue );
    return( iLocation != -1 );
}
//Location : 3
//将一个字符串中的汉字计为2个字符，以利于数据库中正确处理
//sString：待记数的字符串
//返回含有汉字的字符串长度
function RealLength( sString )
{
    var iLength = 0;    // 真实长度记数器
    for( i = 0; i < sString.length; i ++ )
    {
        if( isASCII( sString.charAt( i ) ) )
        {
            iLength += 1;
        }
        else
        {
            iLength += 2;
        }
    }
    return( iLength );
}
//Location : 4
//IsTelephone函数判断一个字符串是否由数字或'-','*','(',')'组成的电话号码 
//str：待检查的字符串
//返回一个boolean值！
function IsTelephone(str)   
{
for(ilen=0;ilen<str.length;ilen++)
{
    if(str.charAt(ilen) < '0' || str.charAt(ilen) > '9' )
    {
    if((str.charAt(ilen)!='-')&&(str.charAt(ilen)!='*')&&(str.charAt(ilen)!='(')&&(str.charAt(ilen)!=')'))
        return false;
    }   
}
return true;
}
//Location : 5
//检查是否是邮编
//sValue：输入的字符串值，合法格式为六位整数
//返回一个boolean值！
function isPostCode( sValue )
{
    if( sValue == null )
    {
        return false;
    }
    if( sValue.length != 6 )
    {
        return false;
    }
    else
    {
        var re =  new RegExp("^([0-9]+)$");
        for( i = 0; i < 6; i ++ )
        {
            if(sValue.search(re) == -1)
            {
                return false;
            }
        }
    }
    return true;
} 
//Location : 6
//Trim函数去掉一字符串两边的空格
//参数为字符串，返回一个处理后字符串
function Trim(his)
{
//找到字符串开始位置
Pos_Start = -1;
for(var i=0;i<his.length;i++)
{
    if(his.charAt(i)!=" ")
    {
        Pos_Start = i;
        break; 
    }
}
//找到字符串结束位置
Pos_End = -1;
for(var i=his.length-1;i>=0;i--)
{
    if(his.charAt(i)!=" ")
    {
        Pos_End = i; 
        break; 
    }
}
//返回的字符串
Str_Return = ""
if(Pos_Start!=-1 && Pos_End!=-1)
{   
        for(var i=Pos_Start;i<=Pos_End;i++)
        {
            Str_Return = Str_Return + his.charAt(i); 
        }
}
return Str_Return;
} 
//Location : 7
//消除字符串中的所有空格
//参数为字符串，返回一个处理后字符串
function trimAll(x)
{
    rtn = x;
    while((rtn.length>0) && (rtn.charAt(0)==' '))
        rtn = rtn.substring(1,rtn.length);
    while((rtn.length>0) && (rtn.charAt(rtn.length-1)==' '))
        rtn = rtn.substring(0,rtn.length-1);
    return rtn;
}
//location : 8
//检查字符串是否是一个只含有字母、数字、下划线
//参数：一个字符串
//返回一个boolean值！
function isValid(value){
    var returnValue = true;
    var re =  new RegExp("^[A-Za-z0-9_]*$");
    if(value.search(re) == -1)
    {
    	returnValue=false;
    }
    return returnValue;
}
//location : 9
//检查E-mail是否合法
//参数：字符串
//返回 boolean 值
function isEmail(value){
    var returnValue = true;
    var re =  new RegExp("^[A-Za-z0-9_][A-Za-z0-9_]*@([A-Za-z0-9]+[.])+([A-Za-z]{2,3})$");
    //此方法未充分测试，如有问题请与liangping@unc.com.cn联系。
    if(value.search(re) == -1)
    {
    returnValue=false;
    }
    return returnValue;
}
//location:10
//检查是否只准许子母和数字
//参数：字符串
//返回 boolean 值
function isNumChar(value){
    var returnValue = true;
    var re =  new RegExp("^[A-Za-z0-9]*$");
    if(value.search(re) == -1)
    {
    returnValue=false;
    }
    return returnValue;
}
//location:11
//检查是否只准许子母、数字、-、/
//参数：字符串
//返回 boolean 值
function isNumChar2(value){
    var returnValue = true;
    var re =  new RegExp("^[A-Za-z0-9-/]*$");
    if(value.search(re) == -1)
    {
    returnValue=false;
    }
    return returnValue;
}
//location : 12
//检查是否是数字！含0和+
//参数： 字符串
//返回一个boolean值！
function isInt5(value){
    var returnValue = true;
    var re =  new RegExp("^([0-9+]+)$");
    if(value.search(re) == -1)
    {
    returnValue=false;
    }
    return returnValue;
}
//location : 13
//检查是否是字母
//参数： 字符串
//返回一个boolean值！
function isChar(value){
    var returnValue = true;
    var re =  new RegExp("^([a-zA-Z]*)$");
    if(value.search(re) == -1)
    {
    returnValue=false;
    }
    return returnValue;
}

//检查字符串是否是一个只含有字母、数字、下划线和中文、方括号
//参数：一个字符串
//返回一个boolean值！
function isValidChar(value){
  if (value.length==0 || value.length != Trim(value).length){
    return false;
  }
  var re =  new RegExp("^[a-zA-Z0-9_]*$");
  for(i=0; i<value.length; i++){
    code=escape(value.charAt(i));
    if (code.length < 4) {
    	if (value.charAt(i) == '[' || value.charAt(i) == ']')
	    	continue;
	    if(code.search(re) == -1)
	    {
	        return false;
	    }
    }
  }
  return true;
}

function isValidChar2(value){
  var returnValue = true;
  var re =  new RegExp("^([#a-zA-Z0-9]*)$");
  if(value.search(re) == -1)
  {
    returnValue=false;
  }
  return returnValue;
}

//检查字符串是否是一个只含有字母、数字、下划线和中文,左右斜杠
//参数：一个字符串
//返回一个boolean值！
function isPath(value){
  if (value.length==0){
    return false;
  }
  var re =  new RegExp("^[A-Za-z0-9_/\]*$");
  for(i=0; i<value.length; i++){
    code=escape(value.charAt(i));
    if (code.length < 4) {
      if(code.search(re) == -1)
      {
        return false;
      }
    }
  }
    return true;
}

function isDate(value)
{
    //请不要删除注释,有问题请和liangping@unc.com.cn联系。
    //1 3 5 7 8 10 12
    //var re = new RegExp("^(((0{0,1}[13578])|([1][02]))-((0{0,1}[1-9])|([12][0-9])|(3[01])))$");
    //4, 6, 9 ,11
    //var re = new RegExp("^(((0{0,1}[469])|(11))-((0{0,1}[1-9])|([12][0-9])|(30)))$");
    //2
    //var re = new RegExp("^((2|(02))-((0{0,1}[1-9])|([12][0-9])))$");
    //组合表达度
    //var re = new RegExp("^([1-9][0-9]{3}-((cc)|(aa)|(bb)))$");
    var re = new RegExp("^([1-9][0-9]{3}-(((2|(02))-((0{0,1}[1-9])|([12][0-9])))|(((0{0,1}[469])|(11))-((0{0,1}[1-9])|([12][0-9])|(30)))|(((0{0,1}[13578])|([1][02]))-((0{0,1}[1-9])|([12][0-9])|(3[01])))))$");
    if(value.search(re) == -1)
    {
        return false;
    }else{
        split = value.split("-");
        
        if( parseInt( split[0] ) % 4 != 0 && split[1] == '02' && split[2] == '29' )
        {
            return false;
        }
        {
            return true;
        }
    }

}

function checkAll(name,status)
{
    var inputs = document.all.tags("INPUT");
    var len = inputs.length;
    for(var i=0;i < len;i++ )
    {
        if(inputs[i].type == "checkbox" && inputs[i].name == name)
        {
            inputs[i].checked = status;
        }
    }
}


    function compareDate(firstDate , lastDate){
      if(!isDate(firstDate) || !isDate(lastDate)){
        msg("存在非法日期！");
        return -1;
      }else{
        time1 = parseDate(firstDate).getTime();
        time2 = parseDate(lastDate).getTime();
        if(time1 > time2)
          return 1;
        else if(time1 == time2)
          return 0;
        else 
          return 2;
      }
    }
    
    function parseDate(str){
      var year , month , day;
      year = str.substr(0 , parseInt(str.indexOf('-')));
      str1 = str.substr(parseInt(str.indexOf('-')) + 1);
      month = str1.substr(0 , parseInt(str1.indexOf('-')));
      str2 = str1.substr(parseInt(str1.indexOf('-')) + 1);
      day = str2.substr(parseInt(str2.indexOf('-')) + 1);
      var myDate=new Date(parseInt(month) -1 + "/" + day + "/" + year);
      myDate.setHours(0);
      myDate.setMinutes(0); 
      myDate.setSeconds(0);
      return myDate;
    }

function isDateTime(value)
{
    var re = new RegExp("^([1-9][0-9]{3}-(((2|(02))-((0{0,1}[1-9])|([12][0-9])))|(((0{0,1}[469])|(11))-((0{0,1}[1-9])|([12][0-9])|(30)))|(((0{0,1}[13578])|([1][02]))-((0{0,1}[1-9])|([12][0-9])|(3[01])))) (((0{0,1}[0-9])|(1[0-9])|2[0-3])):((0{0,1}[0-9])|([1-5][0-9])):((0{0,1}[0-9])|([1-5][0-9])))$");
    if(value.search(re) == -1)
    {
        return false;
    }else{
        return true;
    }

}

//结束日期大于开始日期：返回true
function compareDateTime(start , endTime){
  if(!isDateTime(start) || !isDateTime(endTime)){
    msg("存在非法日期！");
    return false;
  }else{
    return parseDateTime(endTime) > parseDateTime(start);
  }
}
    
    function parseDateTime(str){
      var year , month , day;
      year = str.substr(0 , parseInt(str.indexOf('-')));
      str1 = str.substr(parseInt(str.indexOf('-')) + 1);
      month = str1.substr(0 , parseInt(str1.indexOf('-')));
      month = month - 1;
      str2 = str1.substr(parseInt(str1.indexOf('-')) + 1);
      day = str2.substr(0 , parseInt(str2.indexOf(' ')));
      str3 = str2.substr(parseInt(str2.indexOf(' ')) + 1);
      hour = str3.substr(0 , parseInt(str3.indexOf(':')));
      str4 = str3.substr(parseInt(str3.indexOf(':')) + 1);
      minute = str4.substr(0 , parseInt(str4.indexOf(':')));
      str5 = str4.substr(parseInt(str4.indexOf(':')) + 1);
      seconds = str5;
      
      var myDate=new Date(month + "/" + day + "/" + year);
      myDate.setHours(hour);
      myDate.setMinutes(minute); 
      myDate.setSeconds(seconds);
      return myDate;
    }
    /**
    * H0_name 是 H0 在 form 中的名字。
    * M0X_name 是 M/OX 在 form 中的名字。
    * cid_name 是 City 在 form 中的名字。
    * netType 是网络类型的值。
    */
    function getH0(H0_name,M0X_name,cid_name,netType,cardtype_name,btype_name,defaults )
    {
        //getM(netType,cid_name,M0X_name,cardtype_name,btype_name);
        
        var M0X = document.all(M0X_name).value;
        var cid = document.all(cid_name).value;
        var opts = document.all(H0_name);
        var cardtype = document.all(cardtype_name).value;
        var btype = document.all(btype_name).value;

        if(btype=="预付费"){
            btype="1";
        }else{
            btype="0";
        }
        if(opts==null)
        {
            alert("can not find H0 options");
            return;
        }else if(opts.type=="text")
        {
            //getValidCardType(netType>=2?netType-2:netType, cid_name, M0X_name, H0_name, 0, "card_type");
            return;
        }
        for(var i=opts.length-1;i>=0;i--)
        {
            opts.remove(i);
        }
        
        var doc= new ActiveXObject("Microsoft.XMLDOM");
        var url = "http://"+window.location.host+CONTEXTPATH+"/common/xml.jsp?type=H0";
        url+="&M0X="+M0X+"&CID="+cid+"&netType="+netType+"&cardtype="+cardtype+"&btype="+btype;

        doc.async = false;
        doc.load(url);
        //alert(doc.xml);
        var root = doc.childNodes.item(1);
        var len = root.childNodes.length;
        //alert(root.xml);
        var selectIndex = 0;
        for(var i = 0;i< len;i++ )
        {
            var OptValue = root.childNodes.item(i).childNodes.item(0).text;
            var OptName = root.childNodes.item(i).childNodes.item(1).text;
            var opt = new Option(OptName,OptValue);
            if(OptValue==defaults)  selectIndex = i;
            opts.add(opt);
        }
        opts.selectedIndex = selectIndex;
      
        getH1H2H3("H1H2H3",H0_name,M0X_name,cid_name,netType,cardtype_name,btype_name,document.all("defaultH1H2H3").value);
    }
    
    
    function getH1H2H3(H123_name,H0_name,M0X_name,cid_name,netType,cardtype_name,btype_name,defaults)
    {
        var H0 = document.all(H0_name).value;
        var M0X = document.all(M0X_name).value;
        var cid = document.all(cid_name).value;
        var cardtype=document.all(cardtype_name).value;
        var btype = document.all(btype_name).value;
        if(btype=="预付费")
        {
            btype="1";
        }else{
            btype="0"
        }
        var opts = document.all(H123_name);
        if(opts==null)
        {
            return;
        }
        for(var i=opts.length-1;i>=0;i--)
        {
            opts.remove(i);
        }
        
        var doc= new ActiveXObject("Microsoft.XMLDOM");
        var url = "http://"+window.location.host+CONTEXTPATH+"/common/xml.jsp?type=H1H2H3";
        url+="&M0X="+M0X+"&CID="+cid+"&netType="+netType+"&H0="+H0+"&cardtype="+cardtype+"&btype="+btype;
        doc.async = false;
        //prompt("",url);
        doc.load(url);
        //alert(doc.xml);
        var root = doc.childNodes.item(1);
        var len = root.childNodes.length;
        //alert(root.xml);
        var selectIndex = 0;
        for(var i = 0;i< len;i++ )
        {
            var OptValue = root.childNodes.item(i).childNodes.item(0).text;
            var OptName = root.childNodes.item(i).childNodes.item(1).text;
            var opt = new Option(OptName,OptValue);
            //original is if OptValue==defaults,modified by wangt.2005-02-28
            if(OptName==defaults)  selectIndex = i;
            opts.add(opt);
        }
        opts.selectedIndex = selectIndex;
        //getValidCardType(netType>=2?netType-2:netType, cid_name, M0X_name, H0_name, H123_name, "card_type");
    }
    //-----------------------王朋 2004-07-16 添加查询当前可用卡类型函数和业务类型函数--开始-------
    function getValidCardType(nettype, city_name, m_name, h0_name, h1h2h3_name, cardtype_name)
    {
        var opts = document.all(cardtype_name);
        if(opts==null)
        {
            alert("can not find the card type options");
            return;
        }
        for(var i=opts.length-1;i>=0;i--)
        {
            opts.remove(i);
        }       
        var cid = document.all(city_name).value;
        var m = document.all(m_name).value;
        var h0Field = document.all(h0_name);
        var defaults = document.all('defaultCardType').value;
        var h0=0;
        var h1h2h3=0;
        if(h0Field.type!="text")
        {
            h0=document.all(h0_name).value
            h1h2h3=document.all(h1h2h3_name).value
        }
        var doc= new ActiveXObject("Microsoft.XMLDOM");
        var url = "http://"+window.location.host+CONTEXTPATH+"/common/xml.jsp?Cid="+cid;
        url += "&netType="+nettype+"&M0X="+m+"&H0="+h0+"&H1H2H3="+h1h2h3;
        if(h0Field.type!="text")
        {
            url+="&type=CardType";
        }else{
            url+="&type=CardType2";
        }      
        doc.async = false;
        doc.load(url);
        var root = doc.childNodes.item(1);
        var len = root.childNodes.length;
        
        var selectIndex = 0;
        for(var i = 0;i< len;i++ )
        {
            var OptValue = root.childNodes.item(i).childNodes.item(0).text;
            var OptName = root.childNodes.item(i).childNodes.item(1).text;
            var opt = new Option(OptName,OptValue);
            if(OptValue==defaults)  selectIndex = i;
            opts.add(opt);
        }
        opts.selectedIndex = selectIndex;
        if(h0Field.type=="text")
        {
            return;
        }
        
        getM(nettype,city_name,m_name,cardtype_name,'business_type');
        getH0(h0_name,m_name,city_name,nettype,cardtype_name,'business_type', document.all('defaultH0').value )
        //getValidBusiness(nettype, city_name, m_name, h0_name, h1h2h3_name, cardtype_name,"business_type");
    }
    
    function getM(nettype,city_name,m_name,cardtype_name,business_name)
    {
        if(document.all('OX')!=null) return;
        var defaults = document.all('defaultM').value;
        
        var opts = document.all(m_name);
        if(opts==null)
        {
            alert("can not find the card type options");
            return;
        }
        for(var i=opts.length-1;i>=0;i--)
        {
            opts.remove(i);
        }  
             
        var cid = document.all(city_name).value;
        var m = document.all(m_name).value;
        //defaults = m;
        var cardtype = document.all(cardtype_name).value;
        var btype = document.all(business_name).value;
        if(btype=="预付费")
        {
            btype="1";
        }else{
            btype="0"
        }
        var h0=0;
        var h1h2h3=0;

        var doc= new ActiveXObject("Microsoft.XMLDOM");
        var url = "http://"+window.location.host+CONTEXTPATH+"/common/xml.jsp?Cid="+cid;
        url += "&netType="+nettype+"&M0X="+m+"&H0="+h0+"&H1H2H3="+h1h2h3;
        url+="&type=M&cardtype="+cardtype+"&btype="+btype;
   
        doc.async = false;
        doc.load(url);

        var root = doc.childNodes.item(1);
        var len = root.childNodes.length;
        
        var selectIndex = 0;
        for(var i = 0;i< len;i++ )
        {
            var OptValue = root.childNodes.item(i).childNodes.item(0).text;
            var OptName = root.childNodes.item(i).childNodes.item(1).text;
            var opt = new Option(OptName,OptValue);
            if(OptValue==defaults)  selectIndex = i;
            opts.add(opt);
        }
        opts.selectedIndex = selectIndex;

    }
    
    function getValidBusiness(nettype, city_name, m_name, h0_name, h1h2h3_name, cardtype_name, business_name)
    {
        var opts = document.all(business_name);
        if(opts==null)
        {
            alert("can not find the business options");
            return;
        }
        for(var i=opts.length-1;i>=0;i--)
        {
            opts.remove(i);
        }
                
        var cid = document.all(city_name).value;
        var m = document.all(m_name).value;
        var h0 = document.all(h0_name).value;
        var h1h2h3 = document.all(h1h2h3_name).value;
        var cardtype = document.all(cardtype_name).value;
        var defaults = document.all('defaultBtype').value;
        
        var doc= new ActiveXObject("Microsoft.XMLDOM");
        var url = "http://"+window.location.host+CONTEXTPATH+"/common/xml.jsp?Cid="+cid;
        url += "&netType="+nettype+"&M0X="+m+"&H0="+h0+"&H1H2H3="+h1h2h3+"&CardType="+cardtype+"&type=Business";
        doc.async = false;
        doc.load(url);
        //prompt("",url);
        //alert(doc.xml);
        var root = doc.childNodes.item(1);
        var len = root.childNodes.length;
        //alert(root.xml);
        var selectIndex = 0;
        for(var i = 0;i< len;i++ )
        {
            var OptValue = root.childNodes.item(i).childNodes.item(0).text;
            var OptName = root.childNodes.item(i).childNodes.item(1).text;
            var opt = new Option(OptName,OptValue);
            if(OptValue==defaults)  selectIndex = i;
            opts.add(opt);
        }
        opts.selectedIndex = selectIndex;
        getValidCardType(nettype>=2?nettype-2:nettype, city_name, m_name, h0_name, h1h2h3_name, "card_type");
    }
    //-----------------------王朋 2004-07-16 添加查询当前可用卡类型函数和业务类型函数==结束-------
    function getCitys(City_name,pid,defaults)
    {
        var opts = document.all(City_name);
        if(opts==null)
        {
            alert("can not find the city options");
            return;
        }
        for(var i=opts.length-1;i>=0;i--)
        {
            opts.remove(i);
        }
        
        var doc= new ActiveXObject("Microsoft.XMLDOM");
        var url = "http://"+window.location.host+CONTEXTPATH+"/common/city.jsp?pid="+pid;
        doc.async = false;
        doc.load(url);
        //prompt("",url);
        //alert(doc.xml);
        var root = doc.childNodes.item(1);
        var len = root.childNodes.length;
        //alert(root.xml);
        var selectIndex = 0;
        for(var i = 0;i< len;i++ )
        {
            var OptValue = root.childNodes.item(i).childNodes.item(0).text;
            var OptName = root.childNodes.item(i).childNodes.item(1).text;
            var opt = new Option(OptName,OptValue);
            if(OptValue==defaults)  selectIndex = i;
            opts.add(opt);
        }
        opts.selectedIndex = selectIndex;
    }
    //shilei
    function getOperation(name,_value)
    {   
        var opts= name.operation;	  
          
	    if(opts==null)
        {
            return;
        }        
        for(var i=opts.length-1;i>=0;i--)
        {
            opts.remove(i);
        }
        opts.add(new Option("全部操作","0"));
	        switch(_value)
            {
                case "0":                    
                    for(var j=1;j<=11;j++)
                    {
                        opts.add(new Option(oper[j],j));                        
                    }                   
                    break;
                //资源相关
                case "1": 
                    for(var j=12;j<=16;j++)
                    {
                        opts.add(new Option(oper[j],j));
                    }
                    break;
                //订单相关
                case "2":
                    for(var j=17;j<=24;j++)
                    {
                        opts.add(new Option(oper[j],j));
                    }
                    break;
                //任务单相关
                case "3":
                    for(var j=25;j<=32;j++)
                    {
                        opts.add(new Option(oper[j],j));
                    }
                    break;
                //数据单相关
                case "4":
                    for(var j=33;j<=35;j++)
                    {
                        opts.add(new Option(oper[j],j));
                    }
                    break;
                //基础数据相关
                case "5":
                    for(var j=36;j<=51;j++)
                    {
                        opts.add(new Option(oper[j],j));
                    }
                    break;
                //日志本身相关
                case "6":
                    for(var j=52;j<=59;j++)
                    {
                        opts.add(new Option(oper[j],j));
                    }
                    break;
            }       
	}
    function getCitysForSearch(City_name,pid)
    {
        var opts = document.all(City_name);
        if(opts==null)
        {
            alert("can not find the city options");
            return;
        }
        for(var i=opts.length-1;i>=0;i--)
        {
            opts.remove(i);
        }
        
        var doc= new ActiveXObject("Microsoft.XMLDOM");
        var url = "http://"+window.location.host+CONTEXTPATH+"/common/city.jsp?pid="+pid+"&all=Y";
        doc.async = false;
        doc.load(url);
        //prompt("",url);
        //alert(doc.xml);
        var root = doc.childNodes.item(1);
        var len = root.childNodes.length;
        //alert(root.xml);
        var selectIndex = 0;
        opts.add(new Option("--全部--",""));
        for(var i = 0;i< len;i++ )
        {
            var OptValue = root.childNodes.item(i).childNodes.item(0).text;
            var OptName = root.childNodes.item(i).childNodes.item(1).text;
            var opt = new Option(OptName,OptValue);
            opts.add(opt);
        }
        opts.selectedIndex = selectIndex;
    }
    
    
    function getEncrpytCode(enc_name, city_name,defaults)
    {
        var opts = document.all(enc_name);
        if(opts==null)
        {
            alert("can not find the enc options");
            return;
        }
        for(var i=opts.length-1;i>=0;i--)
        {
            opts.remove(i);
        }
        if(defaults=="")
        {
            defaults = getDefault('encrypt',document.all(city_name).value);
        }
        
        var doc= new ActiveXObject("Microsoft.XMLDOM");
        var url = "http://"+window.location.host+CONTEXTPATH+"/common/xml.jsp?type=encrypt";
        url += "&cid="+document.all(city_name).value;
        doc.async = false;
        doc.load(url);
        //prompt("",url);
        //alert(doc.xml);
        var root = doc.childNodes.item(1);
        var len = root.childNodes.length;
        //alert(root.xml);
        var selectIndex = 0;
        for(var i = 0;i< len;i++ )
        {
            var OptValue = root.childNodes.item(i).childNodes.item(0).text;
            var OptName = root.childNodes.item(i).childNodes.item(1).text;
            var opt = new Option(OptName,OptValue);
            if(OptValue==defaults)  selectIndex = i;
            opts.add(opt);
        }
        opts.selectedIndex = selectIndex;
    }
    
   
/*旧代码
    function getSMSEncrypt(SMSEncrypt_name,city_name)
    {
        var opts = document.all(SMSEncrypt_name);
        defaults = opts.options[opts.selectedIndex].value;
        if(opts==null)
        {
            alert("can not find the sms options");
            return;
        }
        for(var i=opts.length-1;i>=0;i--)
        {
            opts.remove(i);
        }
        if(defaults=="")
        {
            defaults = getDefault(type,document.all(city_name).value);
        }        
        
        var doc= new ActiveXObject("Microsoft.XMLDOM");
        var url = "http://"+window.location.host+CONTEXTPATH+"/common/xml.jsp?type=gatewayencrypt";
        url += "&cid="+document.all(city_name).value;
        doc.async = false;
        doc.load(url);
        var root = doc.childNodes.item(1);
        var len = root.childNodes.length;
        var selectIndex = 0;
        for(var i = 0;i< len;i++ )
        {
            var OptValue = root.childNodes.item(i).childNodes.item(0).text;
            var OptName = root.childNodes.item(i).childNodes.item(1).text;
            var opt = new Option(OptName,OptValue);
            if(OptValue==defaults)  selectIndex = i;
            opts.add(opt);
        }
        opts.selectedIndex = selectIndex;
    }
*/
/************************汪涛添加部分************************/
    function getSMSEncrypt(SMSEncrypt_name,city_name,defaults)
    {
        var opts = document.all(SMSEncrypt_name);
        if(opts==null)
        {
            alert("can not find the sms options");
            return;
        }
        for(var i=opts.length-1;i>=0;i--)
        {
            opts.remove(i);
        }
        if(defaults=="")
        {
            defaults = getDefault("gatewayencrypt",document.all(city_name).value);
        }        
        
        var doc= new ActiveXObject("Microsoft.XMLDOM");
        var url = "http://"+window.location.host+CONTEXTPATH+"/common/xml.jsp?type=gatewayencrypt";
        url += "&cid="+document.all(city_name).value;
        doc.async = false;
        doc.load(url);
        var root = doc.childNodes.item(1);
        var len = root.childNodes.length;
        var selectIndex = 0;
        for(var i = 0;i< len;i++ )
        {
            var OptValue = root.childNodes.item(i).childNodes.item(0).text;
            var OptName = root.childNodes.item(i).childNodes.item(1).text;
            var opt = new Option(OptName,OptValue);
            if(OptValue==defaults)  selectIndex = i;
            opts.add(opt);
        }
        opts.selectedIndex = selectIndex;
    }
/************************汪涛添加部分结束*********************/       

    function getSMSCenter(SMS_name, city_name,defaults,type,netcode)
    {
        var opts = document.all(SMS_name);
        if(opts==null)
        {
            alert("can not find the sms options");
            return;
        }
        for(var i=opts.length-1;i>=0;i--)
        {
            opts.remove(i);
        }
        if(defaults=="")
        {
            if(netcode=="C-G")
            {
                defaults = getDefault(type,document.all(city_name).value);
            }
            else
            {
                defaults = getDefault('SMS',document.all(city_name).value);
            }
        }        
        var doc= new ActiveXObject("Microsoft.XMLDOM");
        var url;
        if(netcode=="C-G")
        {
            url = "http://"+window.location.host+CONTEXTPATH+"/common/xml.jsp?type="+type;
        }
        else
        {
            url = "http://"+window.location.host+CONTEXTPATH+"/common/xml.jsp?type=SMS";
        }
        url += "&cid="+document.all(city_name).value;
        doc.async = false;
        doc.load(url);
        var root = doc.childNodes.item(1);
        var len = root.childNodes.length;
        var selectIndex = 0;
        for(var i = 0;i< len;i++ )
        {
            var OptValue = root.childNodes.item(i).childNodes.item(0).text;
            var OptName = root.childNodes.item(i).childNodes.item(1).text;
            var opt = new Option(OptName,OptValue);
            if(OptValue==defaults)  selectIndex = i;
            opts.add(opt);
        }
        opts.selectedIndex = selectIndex;
        if(type=="SMS2")
        {
            getSMSEncrypt('gateway_encrypt_code',city_name);
        }
    }
 
     function getUIMIDCount(Count_name,cardfactorycode)
    {
        var opts = document.all(Count_name);
        if(opts==null)
        {
            alert("can not find the UIMIDCount ");
            return;
        }
        
        var doc= new ActiveXObject("Microsoft.XMLDOM");
        var url = "http://"+window.location.host+CONTEXTPATH+"/common/UIMIDusableCount.jsp?cardfactoryid="+cardfactorycode;
        doc.async = false;
        doc.load(url);
        //prompt("",url);
        //alert(doc.xml);
        var root = doc.childNodes.item(1);
        //alert(root.xml);

        var OptValue = root.childNodes.item(0).childNodes.item(0).text;
        opts.value = OptValue;
    }   
    
    function getCountbyId(Count_name,cardfactoryid)
    {
        var opts = document.all(Count_name);
        if(opts==null)
        {
            alert("can not find the UIMIDCount ");
            return;
        }
        
        var doc= new ActiveXObject("Microsoft.XMLDOM");
        var url = "http://"+window.location.host+CONTEXTPATH+"/common/UIMIDusableCount.jsp?cardfactoryid="+cardfactoryid;
        doc.async = false;
        doc.load(url);
        //prompt("",url);
        //alert(doc.xml);
        var root = doc.childNodes.item(1);
        //alert(root.xml);

        var OptValue = root.childNodes.item(0).childNodes.item(0).text;
        opts.value = OptValue;
    } 
    
    function getNID(NID_name, city_name, type, defaults)
    {
        var opts = document.all(NID_name);
        if(opts==null)
        {
            alert("can not find the NID options");
            return;
        }
        for(var i=opts.length-1;i>=0;i--)
        {
            opts.remove(i);
        }
        if(defaults==""||defaults==null)
        {
            defaults = getDefault(type,document.all(city_name).value);
        }
        
        var doc= new ActiveXObject("Microsoft.XMLDOM");
        var url = "http://"+window.location.host+CONTEXTPATH+"/common/xml.jsp?type="+type;
        url += "&cid="+document.all(city_name).value;
        doc.async = false;
        doc.load(url);
        var root = doc.childNodes.item(1);
        var len = root.childNodes.length;
        var selectIndex = 0;
                
        for(var i = 0;i< len;i++ )
        {
            var OptValue = root.childNodes.item(i).childNodes.item(0).text;
            var OptName = root.childNodes.item(i).childNodes.item(1).text;
            var opt = new Option(OptName,OptValue);
            if(OptValue==defaults)  selectIndex = i;
            opts.add(opt);
        }
        opts.selectedIndex = selectIndex;
    }
    
    function getSID(SID_name, city_name, type, defaults)
    {
        var opts = document.all(SID_name);
        if(opts==null)
        {
            alert("can not find the SID options");
            return;
        }
        for(var i=opts.length-1;i>=0;i--)
        {
            opts.remove(i);
        }
        if(defaults==""|| defaults==null)
        {
            defaults = getDefault(type,document.all(city_name).value);
        }
        
        var doc= new ActiveXObject("Microsoft.XMLDOM");
        var url = "http://"+window.location.host+CONTEXTPATH+"/common/xml.jsp?type="+type;
        url += "&cid="+document.all(city_name).value;
        doc.async = false;
        doc.load(url);
        var root = doc.childNodes.item(1);
        var len = root.childNodes.length;
        var selectIndex = 0;
        for(var i = 0;i< len;i++ )
        {
            var OptValue = root.childNodes.item(i).childNodes.item(0).text;
            var OptName = root.childNodes.item(i).childNodes.item(1).text;
            var opt = new Option(OptName,OptValue);
            if(OptValue==defaults)  selectIndex = i;
            opts.add(opt);
        }
        opts.selectedIndex = selectIndex;
    }
  
    function getDefault(type, cid )
    {
        var doc= new ActiveXObject("Microsoft.XMLDOM");
        var url = "http://"+window.location.host+CONTEXTPATH+"/common/getDefault.jsp?type="+type;
        url += "&cid="+cid;
        doc.async = false;
        doc.load(url);
        var root = doc.childNodes.item(1);
        return root.childNodes.item(0).text;
    }
    
    function trimZero(num)
    {
        for (var i = 0; i < num.length; ++i)
        {
            if (num.charAt(0) == '0')
            {
                num = num.substr(1);
            }
        }
        if (num.length == 0) num = 0;
        return num;
    }
    
    function inputIsDecNumber()
    {
//        alert(event.keyCode);
        if (event.keyCode == 8) return true;
        if (event.keyCode == 39 || event.keyCode==37) return true;
        if (event.keyCode >= 96 && event.keyCode <= 105) return true;
        if (event.keyCode>=48 && event.keyCode<=57) return true;
    }
    
    //---------- modify by yuhuan --------- 2004.8.10 ------- begin
    function int2str(num, bits)
    {
        //var str="0";
        if(num.toString().length >= bits)
            return num;
        while(num.toString().length != bits){
            num = "0" + num.toString();
        }
        return num;
    }
    //---------- modify by yuhuan --------- 2004.8.10 ------- end