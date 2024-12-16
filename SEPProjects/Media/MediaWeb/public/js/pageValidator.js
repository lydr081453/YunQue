var jQuery_formValidator_initConfig;
jQuery.formValidator = 
{
	sustainType : function(id,setting)
	{
		var elem = $("#"+id).get(0);
		var srcTag = elem.tagName;
		var stype = elem.type;
		switch(setting.validateType)
		{
			case "InitValidator":
				return true;
			case "InputValidator":
				if (srcTag == "INPUT" || srcTag == "TEXTAREA") return true;
				return false;
			case "SelectValidator":
				if (srcTag == "SELECT") return true;
				return false;
			case "CompareValidator":
				if (srcTag == "INPUT" || srcTag == "TEXTAREA")
				{
					if (stype == "checkbox" || stype == "radio") return false;
					return true;
				}
				return false;
			case "AjaxValidator":
				return false;
			case "RegexValidator":
				if (srcTag == "INPUT" || srcTag == "TEXTAREA")
				{
					if (stype == "checkbox" || stype == "radio") return false;
					return true;
				}
				return false;
		}
	},
    
	initConfig : function(controlOptions)
	{
		var settings = 
		{
			validatorGroup : "1",
			alertMessage:false,
			onSuccess: function() {return true;},
			onError:function() {},
			submitOnce:false
		};
		controlOptions = controlOptions || {};
		jQuery.extend(settings, controlOptions);
		if (jQuery_formValidator_initConfig == null ) jQuery_formValidator_initConfig = new Array();
		jQuery_formValidator_initConfig.push( settings );
	},
	
	//如果validator对象对应的element对象的validator属性追加要进行的校验。
	appendValid : function(id, setting )
	{
		//如果是各种校验不支持的类型，就不追加到。返回-1表示没有追加成功
		if(!jQuery.formValidator.sustainType(id,setting)) return -1
		var srcjo = $("#"+id).get(0);   
		if (setting.validateType=="InitValidator" || !srcjo.settings || srcjo.settings == undefined ) srcjo.settings = new Array();   
		srcjo.settings.push( setting );
		return srcjo.settings.length - 1;
	},
	
	//如果validator对象对应的element对象的validator属性追加要进行的校验。
	getInitConfig : function( validatorGroup )
	{
		if(jQuery_formValidator_initConfig!=null)
		{
		    for(i=0;i<jQuery_formValidator_initConfig.length;i++)
		    {
		        if(validatorGroup==jQuery_formValidator_initConfig[i].validatorGroup)
		            return jQuery_formValidator_initConfig[i];
		    }
		}
		return null;
	},
	
	//触发每个控件上的各种校验
	triggerValidate : function(id, setting)
	{
		switch(setting.validateType)
		{
			case "InputValidator":
				jQuery.formValidator.InputValid(id, setting);
				break;
			case "SelectValidator":
				jQuery.formValidator.SelectValid(id, setting);
				break;
			case "CompareValidator":
				jQuery.formValidator.CompareValid(id, setting);
				break;
			case "AjaxValidator":
				jQuery.formValidator.InputValid(id, setting);
				break;
			case "RegexValidator":
				jQuery.formValidator.RegexValid(id, setting);
				break;
		}
	},
	
	//根据单个对象,正确:正确提示,错误:错误提示
	ShowMessage : function(returnObj)
	{
	    var id = returnObj.id
		var isValid = returnObj.isValid;
		var setting = returnObj.setting;//正确:setting[0],错误:对应的setting[i]
		var tip = $( "#"+returnObj.id+"Tip" );
		//alert(tip.css("visibility", "visible");
		//tip.style.visibility = visible;
		tip.css("visibility", "visible");
		if (!isValid)				//验证失败,给出提示,并重新设置样式
		{		
			var elem = $("#"+id).get(0);
			var setting0 = elem.settings[0];
			//获取是否自动修正
			var auto = setting0.automodify && (elem.type=="text" || elem.type=="textarea" || elem.type=="file")
			if(jQuery.formValidator.getInitConfig(setting0.validatorGroup).alertMessage)		
			{
				alert(setting.onerror);     
			}
			else
			{
				tip.removeClass();
				if(auto)
				{
					tip.html( setting.onshow ); 
					tip.addClass( "onShow" ); 
					alert(setting.onerror);
				}
				else{
					tip.html( setting.onerror );
					tip.addClass( "onError" ); 
				}
			}
			if(auto) $("#"+id).val(elem.validoldvalue);
		}
		else						//验证成功
		{						
			//验证成功后,如果没有设置成功提示信息,则给出默认提示,否则给出自定义提示;允许为空,值为空的提示
			if(!jQuery.formValidator.getInitConfig(setting.validatorGroup).alertMessage)
			{
				tip.removeClass();
				tip.addClass( "onSuccess" );
				if ( setting.empty && $("#"+returnObj.id).val().length==0 ) 
					tip.html( setting.onempty );
				else
					tip.html( setting.oncorrect );
			}
		}
	},

	//验证单个是否验证通过,正确返回settings[0],错误返回对应的settings[i]
	OneIsValid : function (id,index)
	{
		var returnObj = new Object();
		returnObj.id = id;
		returnObj.isValid = true;
		returnObj.index = 0;
		var settings = $("#"+id).get(0).settings;
		var settingslen = settings.length;
		//一个控件理捆绑了多种校验方式，要逐一校验
		for ( var i = index ; i < settingslen ; i ++ )
		{   
			returnObj.index = i;
			returnObj.setting = settings[i];
			jQuery.formValidator.triggerValidate(id,settings[i]);
			if(!settings[i].isValid)
			{
				returnObj.isValid = false;
				return returnObj;
			}
		}
		settings[0].onvalid($("#"+id).get(0),$("#"+id).val());
		returnObj.setting = settings[0];
	
		return returnObj;
	},

	//验证所有需要验证的对象，并返回是否验证成功。这里只对标志进行判断。
	PageIsValid : function (validatorGroup)
	{
	    if(validatorGroup == null || validatorGroup == undefined) validatorGroup = "1";
		var isValid = true;
		var returnObj;
		var thefirstid = "";
		$("INPUT,TEXTAREA,SELECT").each(function(i,elem)
		{
			if ( elem.settings!=undefined && elem.settings!=null )
			{  
			    if(elem.settings[0].validatorGroup==validatorGroup)
			    {
				    if(jQuery.formValidator.getInitConfig(validatorGroup).alertMessage) 
				    {
					    if(isValid) 		//如果是弹出窗口的,发现一个错误就马上停止,并提示
					    {
						    returnObj = jQuery.formValidator.OneIsValid(elem.id,1);	
						    if (!returnObj.isValid) {
							    jQuery.formValidator.ShowMessage(returnObj);
							    isValid = false;
								if(thefirstid=="") thefirstid = returnObj.id;
						    }
					    }
				    }
				    else
				    {
					    returnObj = jQuery.formValidator.OneIsValid(elem.id,1);	
					    if (!returnObj.isValid) {
							isValid = false;
							if(thefirstid=="") thefirstid = returnObj.id;
						}
					    jQuery.formValidator.ShowMessage(returnObj);
				    }
				}
			}
		});
		//成功或失败后，进行回调函数的处理，以及成功后的灰掉提交按钮的功能
		if(isValid)
		{
            isValid = jQuery.formValidator.getInitConfig(validatorGroup).onSuccess();
			if(jQuery.formValidator.getInitConfig(validatorGroup).submitOnce){$("input[@type='submit']").attr("disabled",true);}
		}
		else
		{
			jQuery.formValidator.getInitConfig(validatorGroup).onError();
			if(thefirstid!="") $("#"+thefirstid).focus();
		}
		return isValid;
	},

	//ajax校验
	AjaxValida : function(id, setting)
	{
		jQuery.ajax(
			{ type: setting.type, url: setting.url, data: setting.data, async:setting.async, dataType:setting.datatype, success: setting.success, complete: setting.complete, beforeSend:setting.beforesend, error:setting.error, processData : setting.processdata } 
		);
	},

	//对正则表达式进行校验（目前只针对input和textarea）
	RegexValid : function(id, setting)
	{
		var srcTag = $("#"+id).get(0).tagName;
		var elem = $("#"+id).get(0);
		//如果有输入正则表达式，就进行表达式校验
		if(elem.settings[0].empty && elem.value==""){
			setting.isValid = true;
		}
		else if (setting.regexp != null && setting.regexp != undefined && typeof setting.regexp == "string" && setting.regexp != "") {
			var exp = new RegExp(setting.regexp, setting.param);
			if (exp.test($("#"+id).val())) 
				setting.isValid = true;
			else 
				setting.isValid = false;
		}
	},
	
	//对input类型控件进行校验
	InputValid : function(id, setting)
	{
		var srcjo = $("#"+id);
		var val = srcjo.val();
		sType = srcjo.attr("type");
		var len = 0 ;
		switch(sType)
		{
			case "text":
			case "hidden":
			case "password":
			case "textarea":
			case "file":
				if (val.length == 0 && srcjo.get(0).settings[0].empty) {
					setting.isValid = true;
					return;
				}
				if (setting.type == "size") {//获得输入的字符长度，并进行校验
					for (var i = 0; i < val.length; i++) {
						if (val.charCodeAt(i) >= 0x4e00 && val.charCodeAt(i) <= 0x9fa5) 
							len += 2;
						else 
							len++;
					}
					if (len < setting.min || len > setting.max) {
						setting.isValid = false;
					}
					else {
						setting.isValid = true;
					}
				}
				else{
					stype = (typeof setting.min);
					if(stype =="number")
					{
						if(!isNaN(val))
						{
							nval = parseFloat(val);
							if(nval>=setting.min && nval<= setting.max)
								setting.isValid = true;
							else
								setting.isValid = false;
						}
						else
							setting.isValid = false;
					}
					if(stype =="string"){
						if(val>=setting.min && val<= setting.max)
							setting.isValid = true;
						else
							setting.isValid = false;
					}
				}
				break;
			case "checkbox":
			case "radio": 
				var sValue = "";
				var objs = $("input[@type='"+sType+"'][@name='"+srcjo.attr("name")+"'][@checked]");
				len = objs.length;
				if ( len == 0 && srcjo.get(0).settings[0].empty ){
					setting.isValid = true;
					break;
				} 
				if(len<setting.min || len > setting.max)
					setting.isValid =false; 
				else
					setting.isValid = true;
				break;
		}
	},
	
	SelectValid : function(id, setting)
	{
		var isValid = true;
		var joeach;
		var groupname = $("#" + id).attr("groupname");
		if (null != groupname && groupname != undefined)
			joeach = $("select[@groupname='" + groupname + "']");
		else
			joeach = $("#"+id);
		//如果存在有关联的一组下拉框，即我选了，其他的也得选
		joeach.each(function(){
			if ( this.options.length > 0 ){
				if ($(this).val() == "" || $(this).val() == "-1") 
					isValid = false;
				else {
					isValid = isValid && true;
				}
			}
			else {
				isValid = isValid && true;
			}
		});
		//都没有选中，并且可以为空
		if (joeach.get(0).settings[0].empty && !isValid) isValid = true;
		setting.isValid = isValid;
	},
	
	CompareValid : function(id, setting)
	{
		var srcjo = $("#"+id);
	    var desjo = $("#"+setting.desID );
	    setting.isValid = false;
		curvalue = srcjo.val();
		ls_data = desjo.val();
        //对数值型的，进行转换
		if(setting.datatype=="number")
        {
            if(!isNaN(curvalue) && !isNaN(ls_data))
			{
				curvalue = parseFloat(curvalue);
                ls_data = parseFloat(ls_data);
			}
			else
			{
			    return;
			}
        }
		
	    switch(setting.operateor)
	    {
	        case "=":
	            if(curvalue == ls_data) setting.isValid = true;
	            break;
	        case "!=":
	            if(curvalue != ls_data) setting.isValid = true;
	            break;
	        case ">":
	            if(curvalue > ls_data) setting.isValid = true;
	            break;
	        case ">=":
	            if(curvalue >= ls_data) setting.isValid = true;
	            break;
	        case "<": 
	            if(curvalue < ls_data) setting.isValid = true;
	            break;
	        case "<=":
	            if(curvalue <= ls_data) setting.isValid = true;
	            break;
	    }
	}
}

//每个校验控件必须初始化的
jQuery.fn.formValidator = function( msgOptions) 
{
	var setting = 
	{
		validatorGroup : "1",
		empty :false,
		submitonce : false,
		automodify : false,
		onshow :"请输入内容",
		onfocus: "请输入内容",
		oncorrect: "输入正确",
		onempty: "输入内容为空",
		onvalid : function(){},
		onfocusevent : function(){},
		onblurevent : function(){},
		validateType : "InitValidator"
	};
	msgOptions = msgOptions || {};
	jQuery.extend(setting, msgOptions);		//采用映射表,合并同类项
	return this.each(function()
	{
		var triggerID = this.id;
		jQuery.formValidator.appendValid(triggerID,setting);
				
		if(!jQuery.formValidator.getInitConfig(setting.validatorGroup).alertMessage)
		{
			//初始化提示对象的样式和文字
			var tip = $( "#"+triggerID+"Tip" );
			tip.html( setting.onshow );
			tip.removeClass();
			tip.addClass( "onShow" ); 
		}
		//获得element对象
		var srcTag = this.tagName;
		
		if (srcTag == "INPUT" || srcTag=="TEXTAREA")
		{
			var stype = this.type;
			var defaultVal = this.value;
			var joeach = $(this);
			//只要有值,就默认算合法
			if (null!= defaultVal && defaultVal!= undefined && defaultVal!="") setting.isValid = true;
			//设置默认选中的值
			if (stype == "checkbox" || stype == "radio") {
				
				joeach = $("input[@name=" + this.name + "]");
				//设置默认选中的值
				defaultVal = $(this).attr("checkedValue");
				if (null != defaultVal && defaultVal != undefined) {
					joeach.each(function(){
						if (this.value == defaultVal) {
							this.checked = "checked";
						}
					});
				}
			}
			//注册获得焦点的事件。改变提示对象的文字和样式，保存原值
			joeach.focus(function()
			{	
				settings = joeach.get(0).settings;
				if(!jQuery.formValidator.getInitConfig(setting.validatorGroup).alertMessage)
				{
					var tip = $( "#"+triggerID+"Tip" );
					tip.html( settings[0].onfocus );			 
					tip.removeClass();
					tip.addClass( "onFocus" );
				}
				if (stype == "text" || stype == "textarea" || stype == "file") {
					this.validoldvalue = $(this).val();
				}
				settings[0].onfocusevent(joeach.get(0));
			});

			//注册失去焦点的事件。进行校验，改变提示对象的文字和样式；出错就提示处理
			joeach.blur(function()
			{   
				var thefirstsettings = joeach.get(0).settings;
				var settingslen = thefirstsettings.length;
				var returnObj = jQuery.formValidator.OneIsValid(triggerID,1);
				//出错或成功的setting如果是ajax校验,就等待异步返回结果再ShowMessage
				if ( thefirstsettings[returnObj.index].validateType != "AjaxValidator" )
				{
					jQuery.formValidator.ShowMessage(returnObj);
				}
				thefirstsettings[0].onblurevent(joeach.get(0));
			});
		//如果存在一组的select，则只需注册改组的第一个；
		} 
		else if (srcTag == "SELECT")
		{
			//设置默认选中的值，存在就算默认校验通过
			var groupname = $(this).attr("groupname");
			var joeach;
			if (null != groupname && groupname != undefined)
				joeach = $("select[@groupname='" + groupname + "']");
			else
				joeach = $(this);
			joeach.each( function()
			{							
				var defaultVal = $(this).attr('selectedValue');			
				if ( null!= defaultVal && defaultVal!= undefined)
				{			 
					$.each( this.options ,function(){		
						if ( $.trim(this.value)==$.trim(defaultVal) || this.text==defaultVal )
						{    
							this.selected = true;
							setting.isValid = true;				  
						}
					});				  
				}
			});
			//注册获得焦点的事件。改变提示对象的文字和样式
			joeach.focus(function()
			{	
				var settings = joeach.get(0).settings;
				if(!jQuery.formValidator.getInitConfig(setting.validatorGroup).alertMessage)
				{
					var tip = $( "#"+triggerID+"Tip" );
					tip.html( settings[0].onfocus );			 
					tip.removeClass();
					tip.addClass( "onFocus" );
				}
			});
			
			//只有进行初始化的select对象触发change事件后才进行ajax校验
			joeach.bind( "change" , function(){
				var settings = joeach.get(0).settings;	//只取第一个select
				var returnObj = jQuery.formValidator.OneIsValid(triggerID,1);	 
				if ( settings[returnObj.index].validateType != "AjaxValidator" )
				{
					jQuery.formValidator.ShowMessage(returnObj);
				}            
			});
		}
	});

}; 

jQuery.fn.InputValidator = function(controlOptions)
{
	var settings = 
	{
		isValid : false,
		min : 0,
		max : 99999999999999,
		forceValid : false,         //出错时，是否一定要输入正确为止
		type : "size",
		defaultValue:null,
		onerror:"输入错误",
		validateType:"InputValidator"
	};
	controlOptions = controlOptions || {};
	jQuery.extend(settings, controlOptions);
	return this.each(function(){
		jQuery.formValidator.appendValid(this.id,settings);
	});
}

//注意一点：对下拉框进行验证，不选中的时候，你设置的值必须为""或者"-1"
//如果有依赖的下拉框组的话，必须设置groupname为一样
jQuery.fn.SelectValidator = function(controlOptions)
{
	var settings = 
	{
		isValid : false,
		onerror:"必须选择",
		defaultValue:null,
		validateType:"SelectValidator"
	};
	controlOptions = controlOptions || {};
	jQuery.extend(settings, controlOptions);
	return this.each(function(){
		jQuery.formValidator.appendValid(this.id,settings);
	});
}
//提供比较。扩展（比较大小）
jQuery.fn.CompareValidator = function(controlOptions)
{
	var settings = 
	{
		isValid : false,
		desID : "",
		operateor :"=",
		onerror:"输入错误",
		validateType:"CompareValidator"
	};
	controlOptions = controlOptions || {};
	jQuery.extend(settings, controlOptions);
	return this.each(function(){
		var li_index = jQuery.formValidator.appendValid(this.id,settings);
		if(li_index==-1) return;
		var elem = this;
		//当要比较的对象失去焦点的时候，要改变Tip现实的内容
		$("#"+settings.desID).blur(function(){
			var returnObj = jQuery.formValidator.OneIsValid(elem.id,1);
			if (!returnObj.isValid && returnObj.index == li_index) {		//如果是比较这里出错，就从比较这里依次触发校验
				var returnObj = jQuery.formValidator.OneIsValid(elem.id, li_index);
			}
			if ( elem.settings[returnObj.index].validateType != "AjaxValidator" ){
				jQuery.formValidator.ShowMessage(returnObj);
			}
		});
	});
}

jQuery.fn.RegexValidator = function(controlOptions)
{
	var settings = 
	{
		isValid : false,
		regexp : "",
		param : "i",
		onerror:"输入的格式不正确",
		validateType:"RegexValidator"
	};
	controlOptions = controlOptions || {};
	jQuery.extend(settings, controlOptions);
	return this.each(function(){
		jQuery.formValidator.appendValid(this.id,settings);
	});
}

jQuery.fn.AjaxValidator = function(controlOptions)
{
	var settings = 
	{
		isValid : false,
		url : "",
		dataType : "resposeText",
		data : "html",
		async : true,
		beforesend : function(){},
		success : function(){},
		complete : function(){},
		processdata : true,
		onerror:"输入的格式不正确",
		validateType:"RegexValidator"
	};
	controlOptions = controlOptions || {};
	jQuery.extend(settings, controlOptions);
	return this.each(function(){
		jQuery.formValidator.appendValid(this.id,settings);
	});
}