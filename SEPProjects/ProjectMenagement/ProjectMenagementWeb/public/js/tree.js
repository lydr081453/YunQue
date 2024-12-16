//////////////////////////////////////////////////////////////////////////////////////////////////
//
// 对树的操作 yangruodong version 1.0
//
//////////////////////////////////////////////////////////////////////////////////////////////////


//用户单击节点，自动选择上、下级节点
function clickCHK()
{
	var oSrc = event.srcElement;
	
	var oCollections = document.getElementsByTagName('input');
	
	if(oCollections.length < 1) return false;
	
	var sCurValue = oSrc.getAttribute(_keyAttribute).trim();
	var sValue = '';
	
	for(var i = 0;i<oCollections.length;i++)
	{
		if(oCollections.item(i).type=='checkbox' && oCollections.item(i).getAttribute(_keyAttribute) != null )
		{
			sValue = oCollections.item(i).getAttribute(_keyAttribute).trim();
			
			//check the parent
			if(sCurValue.substring(0,sValue.length)==sValue)
			{
				if(oSrc.checked)
				{
					oCollections.item(i).checked = oSrc.checked;
				}
			}
			
			//check the child
			if(sValue.substring(0,sCurValue.length)==sCurValue)	oCollections.item(i).checked = oSrc.checked;
			
		}
	}
	
}



//根据用户类别设置节点禁用
function setTreeDisabled(iType)
{
	var oCollections = document.getElementsByTagName('input');
	
	if(oCollections.length < 1) return;
	
	var j = 0;
	
	for(var i = 0;i<oCollections.length;i++)
	{
		if(oCollections.item(i).type=='checkbox' && oCollections.item(i).getAttribute(_keyAttribute) != null )
		{
			if(oCollections.item(i).getAttribute('flag' + iType).trim() =='0')	
			{
				oCollections.item(i).disabled = true;
			}
			else
			{
				oCollections.item(i).disabled = false;
			}
		}
	}
	
}


function setRoleCheck(iArr)
{
	var oCollections = document.getElementsByTagName('input');
	
	if(oCollections.length < 1) return;
	
	var j = 0;
	
	for(var i = 0;i<oCollections.length;i++)
	{
		if(oCollections.item(i).type=='checkbox' && oCollections.item(i).getAttribute(_keyAttribute) != null )
		{
			for(j=0;j<iArr.length;j++)
			{
				if(iArr[j]==parseInt(oCollections.item(i).getAttribute(_posAttribute).trim()))
				{
					oCollections.item(i).checked = true;
					j = iArr.length;
				}
			}
		}
	}
	
}

//build the treecheck string
function getTreeCheck()
{
	var sStr = '';
	
	var oCollections = document.getElementsByTagName('input');
	
	if(oCollections.length < 1) return;
	
	for(var i = 0;i<oCollections.length;i++)
	{
		if(oCollections.item(i).type=='checkbox' && oCollections.item(i).getAttribute(_keyAttribute) != null )
		{
			if(oCollections.item(i).checked)
			{
				sStr += oCollections.item(i).getAttribute(_posAttribute).trim() + ',';
			}
		}
	}
	
	if(sStr.trim()=="")
	{
		return "";
	}
	else
	{
		return sStr.substring(0,sStr.length - 1);
	}
}



//check or uncheck all the nodes in the tree
function setTreeCheck(isCheck)
{
	var oCollections = document.getElementsByTagName('input');
	
	if(oCollections.length < 1) return;
	
	for(var i = 0;i<oCollections.length;i++)
	{
		if(oCollections.item(i).type=='checkbox' && oCollections.item(i).getAttribute(_keyAttribute) != null ) oCollections.item(i).checked = isCheck;
	}
}

