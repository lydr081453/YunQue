// 验证某Node的值是否有重复
function CheckNodeIsUnique(objXml,nodeName)
{
	if(typeof(objXml)!="object")
	{
		return true;
	}
	var objNodeList = objXml.selectNodes("//" + nodeName);
	var tmp;
	for(var i=0;i<objNodeList.length;i++)
	{
		tmp = objXml.selectNodes("//[" + nodeName + "='" + objNodeList.childNodes(i).text + "']");
		if(tmp!=null)
		{
			if(tmp.childNodes.length>1)
			{
				return false;
			}
		}
	}
	
	return true;
}
