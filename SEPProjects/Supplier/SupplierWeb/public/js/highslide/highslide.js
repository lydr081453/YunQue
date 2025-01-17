/******************************************************************************
Name:    Highslide JS
Version: 3.2.7 (September 9 2007)
Author:  Torstein H�nsi
Support: http://vikjavev.no/highslide/forum
Email:   See http://vikjavev.no/megsjol

Licence:
Highslide JS is licensed under a Creative Commons Attribution-NonCommercial 2.5
License (http://creativecommons.org/licenses/by-nc/2.5/).

You are free:
	* to copy, distribute, display, and perform the work
	* to make derivative works

Under the following conditions:
	* Attribution. You must attribute the work in the manner  specified by  the
	  author or licensor.
	* Noncommercial. You may not use this work for commercial purposes.

* For  any  reuse  or  distribution, you  must make clear to others the license
  terms of this work.
* Any  of  these  conditions  can  be  waived  if  you  get permission from the 
  copyright holder.

Your fair use and other rights are in no way affected by the above.
******************************************************************************/

var hs = {

// Apply your own settings here, or override them in the html file.  
graphicsDir : 'highslide/graphics/',
restoreCursor : "zoomout.cur", // necessary for preload
expandSteps : 10, // number of steps in zoom. Each step lasts for duration/step milliseconds.
expandDuration : 250, // milliseconds
restoreSteps : 10,
restoreDuration : 250,
allowMultipleInstances: true,
hideThumbOnExpand : true,
captionSlideSpeed : 1, // set to 0 to disable slide in effect
outlineWhileAnimating : false, // not recommended, animation gets jaggy on slow systems.
outlineStartOffset : 3, // ends at 10
marginLeft : 10,
marginRight : 35, // leave room for scrollbars + outline
marginTop : 10,
marginBottom : 35, // leave room for scrollbars + outline
numberOfImagesToPreload : 5,
zIndexCounter : 1001, // adjust to other absolutely positioned elements
fullExpandIcon : 'fullexpand.gif',
fullExpandTitle : 'Expand to actual size',
restoreTitle : 'Click to close image, click and drag to move. Use arrow keys for next and previous.',
focusTitle : 'Click to bring to front',
loadingText : 'Loading...',
loadingTitle : 'Click to cancel',
loadingOpacity : 0.75,
showCredits : true, // you can set this to false if you want
creditsText : 'Log Life',
creditsHref : 'http://www.loglife.cn/',
creditsTitle : 'Log Life',
enableKeyListener : true,

// These settings can also be overridden inline for each image
anchor : 'auto', // where the image expands from
align : 'auto', // position in the client (overrides anchor)
targetX: null, // the id of a target element
targetY: null,
captionId : null,
captionTemplateId : null,
slideshowGroup : null, // defines groups for next/previous links and keystrokes
spaceForCaption : 30, // leaves space below images with captions
minWidth: 200,
minHeight: 200,
allowSizeReduction: true, // allow the image to reduce to fit client size. If false, this overrides minWidth and minHeight
outlineType : 'drop-shadow', // set null to disable outlines
wrapperClassName : null, // for enhanced css-control

		
// END OF YOUR SETTINGS


// declare internal properties
preloadTheseImages : [],
continuePreloading: true,
expandedImagesCounter : 0,
expanders : [],
overrides : [
	'anchor',
	'align',
	'targetX',
	'targetY',
	'outlineType',
	'outlineWhileAnimating',
	'spaceForCaption', 
	'wrapperClassName',
	'minWidth',
	'minHeight',
	'captionId',
	'captionTemplateId',
	'allowSizeReduction',
	'slideshowGroup'
],
overlays : [],
pendingOutlines : {},
clones : {},
faders : [],
ie : (document.all && !window.opera),
safari : navigator.userAgent.indexOf("Safari") != -1,
hasFocused : false,

$ : function (id) {
	return document.getElementById(id);
},

push : function (arr, val) {
	arr[arr.length] = val;
},

createElement : function (tag, attribs, styles, parent, nopad) {
	var el = document.createElement(tag);
	if (attribs) hs.setAttribs(el, attribs);
	if (nopad) hs.setStyles(el, {padding: 0, border: 'none', margin: 0});
	if (styles) hs.setStyles(el, styles);
	if (parent) parent.appendChild(el);	
	return el;
},

setAttribs : function (el, attribs) {
	for (var x in attribs) {
		el[x] = attribs[x];
	}
},

setStyles : function (el, styles) {
	for (var x in styles) {
		try { 
			if (hs.ie && x == 'opacity') el.style.filter = 'alpha(opacity='+ (styles[x] * 100) +')';
			else el.style[x] = styles[x]; 
		}
		catch (e) {}
	}
},

ieVersion : function () {
	arr = navigator.appVersion.split("MSIE");
	return parseFloat(arr[1]);
},

clientInfo : function ()	{
	var iebody = document.compatMode && document.compatMode != "BackCompat" 
		? document.documentElement : document.body;
	
	this.width = hs.ie ? iebody.clientWidth : self.innerWidth;
	this.height = hs.ie ? iebody.clientHeight : self.innerHeight;
	this.scrollLeft = hs.ie ? iebody.scrollLeft : pageXOffset;
	this.scrollTop = hs.ie ? iebody.scrollTop : pageYOffset;
} ,

position : function(el)	{ 
	var parent = el;	
	var p = { x: parent.offsetLeft, y: parent.offsetTop };
	while (parent.offsetParent)	{
		parent = parent.offsetParent;
		p.x += parent.offsetLeft;
		p.y += parent.offsetTop;
		if (parent != document.body && parent != document.documentElement) {
			p.x -= parent.scrollLeft;
			p.y -= parent.scrollTop;
		}
	}
	return p;
},

expand : function(a, params, custom) {
	try {
		new HsExpander(a, params, custom);
		return false;		
	} catch (e) { return true; }	
},

focusTopmost : function() {
	var topZ = 0, topmostKey = -1;
	for (i = 0; i < hs.expanders.length; i++) {
		if (hs.expanders[i]) {
			if (hs.expanders[i].wrapper.style.zIndex && hs.expanders[i].wrapper.style.zIndex > topZ) {
				topZ = hs.expanders[i].wrapper.style.zIndex;
				
				topmostKey = i;
			}
		}
	}
	if (topmostKey == -1) hs.focusKey = -1;
	else hs.expanders[topmostKey].focus();
},

closeId : function(id) { // for text links
	return hs.close(id);
},

close : function(el) {
	try { hs.getExpander(el).doClose(); } catch (e) {}
	return false;
},

getAdjacentAnchor : function(key, op) {
	var aAr = document.getElementsByTagName('A'), hsAr = {}, activeI = -1, j = 0;
	for (i = 0; i < aAr.length; i++) {
		if (hs.isHsAnchor(aAr[i]) && ((hs.expanders[key].slideshowGroup == hs.getParam(aAr[i], 'slideshowGroup')))) {
			hsAr[j] = aAr[i];
			if (hs.expanders[key] && aAr[i] == hs.expanders[key].a) {
				activeI = j;
			}
			j++;
		}
	}
	return hsAr[activeI + op];
},

getParam : function (a, param) {
	try {
		var s = a.onclick.toString().replace(/\s/g, ' ').split('{')[2].split('}')[0];
		if (hs.safari) { // stupid bug
			for (var i = 0; i < hs.overrides.length; i++) {
				s = s.replace(hs.overrides[i] +':', ','+ hs.overrides[i] +':').replace(new RegExp("^\\s*?,"), '');
			}
		}
		eval('var arr = {'+ s +'};');
		if (arr[param]) return arr[param];
		else return hs[param];
	} catch (e) {
		return hs[param];
	}
},

getSrc : function (a) {
	var src = hs.getParam(a, 'src');
	if (src) return src;
	return a.rel.replace(/_slash_/g, '/') || a.href;
},

getNode : function (id) {
	var node = hs.$(id), clone = hs.clones[id], a = {};
	if (!node && !clone) return null;
	if (!clone) {
		clone = node.cloneNode(true);
		clone.id = '';
		hs.clones[id] = clone;
		return node;
	} else {
		return clone.cloneNode(true);
	}
},

purge : function(d) {
	if (!hs.ie) return;
	var a = d.attributes, i, l, n;
    if (a) {
        l = a.length;
        for (i = 0; i < l; i += 1) {
            n = a[i].name;
            if (typeof d[n] === 'function') {
                d[n] = null;
            }
        }
    }
    if (hs.geckoBug && hs.geckoBug(d)) return;
	a = d.childNodes;
    if (a) {
        l = a.length;
        for (i = 0; i < l; i += 1) {
            hs.purge(d.childNodes[i]);
        }
    }
},

previousOrNext : function (el, op) {
	var exp = hs.getExpander(el);	
	try { hs.getAdjacentAnchor(exp.key, op).onclick(); } catch (e) {}
	try { exp.doClose(); } catch (e) {}	
	return false;
},

previous : function (el) {
	return hs.previousOrNext(el, -1);
},

next : function (el) {
	return hs.previousOrNext(el, 1);	
},

keyHandler : function(e) {
	if (!e) e = window.event;
	if (!e.target) e.target = e.srcElement; // ie
	if (e.target.form) return; // form element has focus
	
	var op = null;
	switch (e.keyCode) {
		case 34: // Page Down
		case 39: // Arrow right
		case 40: // Arrow down
			op = 1;
			break;
		case 33: // Page Up
		case 37: // Arrow left
		case 38: // Arrow up
			op = -1;
			break;
		case 27: // Escape
		case 13: // Enter
			op = 0;
	}
	if (op !== null) {
		hs.removeEventListener(document, 'keydown', hs.keyHandler);
		try { if (!hs.enableKeyListener) return true; } catch (e) {}
		
		if (e.preventDefault) e.preventDefault();
    	else e.returnValue = false;
		if (op == 0) {
			try { hs.getExpander().doClose(); } catch (e) {}
			return false;
		} else {
			return hs.previousOrNext(hs.focusKey, op);
		}
	} else return true;
},

registerOverlay : function (overlay) {
	hs.push(hs.overlays, overlay);
},

getWrapperKey : function (element) {
	var el, re = /^highslide-wrapper-([0-9]+)$/;
	// 1. look in open expanders
	el = element;
	while (el.parentNode)	{
		el = el.parentNode;
		if (el.id && el.id.match(re)) return el.id.replace(re, "$1");
	}
	// 2. look in thumbnail
	el = element;
	while (el.parentNode)	{
		if (el.tagName && hs.isHsAnchor(el)) {
			for (key = 0; key < hs.expanders.length; key++) {
				exp = hs.expanders[key];
				if (exp && exp.a == el) return key;
			}
		}
		el = el.parentNode;
	}
},

getExpander : function (el) {
	try {
		if (!el) return hs.expanders[hs.focusKey];
		if (typeof el == 'number') return hs.expanders[el];
		if (typeof el == 'string') el = hs.$(el);
		return hs.expanders[hs.getWrapperKey(el)];
	} catch (e) {}
},

cleanUp : function () {
	for (i = 0; i < hs.expanders.length; i++) {
		if (hs.expanders[i] && hs.expanders[i].isExpanded) hs.focusTopmost();
	}
},

mouseClickHandler : function(e) 
{	
	if (!e) e = window.event;
	if (e.button > 1) return true;
	if (!e.target) e.target = e.srcElement;
	if (e.target.form) return;
	
	var fobj = e.target;
	while (fobj.parentNode
		&& !(fobj.className && fobj.className.match(/highslide-(image|move|html)/)))
	{
		fobj = fobj.parentNode;
	}

	if (!fobj.parentNode) return;
	
	hs.dragExp = hs.getExpander(fobj);
	
	if (fobj.className.match(/highslide-(image|move)/)) {
		var isDraggable = true;
		var wLeft = parseInt(hs.dragExp.wrapper.style.left);
		var wTop = parseInt(hs.dragExp.wrapper.style.top);			
	}

	if (e.type == 'mousedown') {
		if (isDraggable) // drag or focus
		{
			if (fobj.className.match('highslide-image')) hs.dragExp.content.style.cursor = 'move';
			
			hs.wLeft = wLeft;
			hs.wTop = wTop;
			
			hs.dragX = e.clientX;
			hs.dragY = e.clientY;
			hs.addEventListener(document, 'mousemove', hs.mouseMoveHandler);
			if (e.preventDefault) e.preventDefault(); // FF
			
			if (hs.dragExp.content.className.match(/highslide-(image|html)-blur/)) {
				hs.dragExp.focus();
				hs.hasFocused = true;
			}
			return false;
		}
		else if (fobj.className.match(/highslide-html/)) { // just focus
			hs.dragExp.focus();
			hs.dragExp.redoShowHide();
			hs.hasFocused = false; // why??
		}
		
	} else if (e.type == 'mouseup') {
		hs.removeEventListener(document, 'mousemove', hs.mouseMoveHandler);
		if (isDraggable && hs.dragExp) {
			if (fobj.className.match('highslide-image')) {
				fobj.style.cursor = hs.styleRestoreCursor;
			}
			var hasMoved = wLeft != hs.wLeft || wTop != hs.wTop;
			if (!hasMoved && !hs.hasFocused && !fobj.className.match(/highslide-move/)) {
				hs.dragExp.onClick();
			} else if (hasMoved || (!hasMoved && hs.hasHtmlexpanders)) {
				hs.dragExp.redoShowHide();
			}
			hs.hasFocused = false;
		
		} else if (fobj.className.match('highslide-image-blur')) {
			fobj.style.cursor = hs.styleRestoreCursor;		
		}
	}
},

mouseMoveHandler : function(e)
{
	if (!hs.dragExp || !hs.dragExp.wrapper) return;
	if (!e) e = window.event;

	hs.dragExp.x.min = hs.wLeft + e.clientX - hs.dragX;
	hs.dragExp.y.min = hs.wTop + e.clientY - hs.dragY;
	
	var w = hs.dragExp.wrapper;
	
	w.style.left = hs.dragExp.x.min +'px';
	w.style.top  = hs.dragExp.y.min +'px';
	
	if (hs.dragExp.objOutline) {
		var o = hs.dragExp.objOutline;
		o.table.style.left = (hs.dragExp.x.min - o.offset) +'px';
		o.table.style.top = (hs.dragExp.y.min - o.offset) +'px';
	}	
	return false;
},

addEventListener : function (el, event, func) {
	try {
		el.addEventListener(event, func, false);
	} catch (e) {
		try {
			el.detachEvent('on'+ event, func);
			el.attachEvent('on'+ event, func);
		} catch (e) {
			el['on'+ event] = func;
		}
	} 
},

removeEventListener : function (el, event, func) {
	try {
		el.removeEventListener(event, func, false);
	} catch (e) {
		try {
			el.detachEvent('on'+ event, func);
		} catch (e) {
			el['on'+ event] = null;
		}
	}
},

isHsAnchor : function (a) {
	return (a.onclick && a.onclick.toString().replace(/\s/g, ' ').match(/hs.(htmlE|e)xpand/));
},

preloadFullImage : function (i) {
	if (hs.continuePreloading && hs.preloadTheseImages[i] && hs.preloadTheseImages[i] != 'undefined') {
		var img = document.createElement('img');
		img.onload = function() { hs.preloadFullImage(i + 1); };
		img.src = hs.preloadTheseImages[i];
	}
},

preloadImages : function (number) {
	if (number && typeof number != 'object') hs.numberOfImagesToPreload = number;
	var re, j = 0;
	
	var aTags = document.getElementsByTagName('A');
	for (i = 0; i < aTags.length; i++) {
		a = aTags[i];
		re = hs.isHsAnchor(a);
		if (re && re[0] == 'hs.expand') {
			if (j < hs.numberOfImagesToPreload) {
				hs.preloadTheseImages[j] = hs.getSrc(a); 
				j++;
			}
		}
	}
	
	// preload outlines
	new HsOutline(hs.outlineType, function () { hs.preloadFullImage(0)} );
	
	// preload cursor
	var cur = hs.createElement('img', { src: hs.graphicsDir + hs.restoreCursor });
},

genContainer : function () {
	if (!hs.container) {
		hs.container = hs.createElement('div', 
			null, 
			{ position: 'absolute', left: 0, top: 0, width: '100%', zIndex: hs.zIndexCounter }, 
			document.body,
			true
		);
	}	
},

fade : function (el, o, oFinal, dir, i) {
	o = parseFloat(o);
	el.style.visibility = (o <= 0) ? 'hidden' : 'visible';
	if (o < 0 || (dir == 1 && o > oFinal)) return;
	if (i == null) i = hs.faders.length;
	if (typeof(el.i) != 'undefined' && el.i != i) {
		clearTimeout(hs.faders[el.i]);
		o = el.tempOpacity;
	}
	el.i = i;
	el.tempOpacity = o;
	el.style.visibility = (o <= 0) ? 'hidden' : 'visible';
	hs.setStyles(el, { opacity: o });
	hs.faders[i] = setTimeout(function() { 
			hs.fade(el, Math.round((o + 0.1 * dir)*100)/100, oFinal, dir, i);
	 	}, 25);
}
}; // end hs object

//-----------------------------------------------------------------------------
HsOutline = function (outlineType, onLoad) {
	this.onLoad = onLoad;
	this.outlineType = outlineType;
	var v = hs.ieVersion(), tr;
	
	this.hasAlphaImageLoader = hs.ie && v >= 5.5 && v < 7;
	this.hasPngSupport = !hs.ie || (hs.ie && v >= 7);
	if (!outlineType || (!this.hasAlphaImageLoader && !this.hasPngSupport)) {
		if (onLoad) onLoad();
		return;
	}
	
	hs.genContainer();
	this.table = hs.createElement(
		'table',
		{	
			cellSpacing: 0 // saf
		},
		{
			visibility: 'hidden',
			position: 'absolute',
			zIndex: hs.zIndexCounter++,
			borderCollapse: 'collapse'
		},
		hs.container,
		true
	);
	this.tbody = hs.createElement('tbody', null, null, this.table);
	
	this.td = Array();
	for (var i = 0; i <= 8; i++) {
		if (i % 3 == 0) tr = hs.createElement('tr', null, null, this.tbody, true);
		this.td[i] = hs.createElement('td', null, null, tr, true);
		var style = i != 4 ? { lineHeight: 0, fontSize: 0} : { position : 'relative' };
		hs.setStyles(this.td[i], style);
	}
	this.td[4].className = outlineType;
	
	this.preloadGraphic(); 
};

HsOutline.prototype.preloadGraphic = function () {	
	var src = hs.graphicsDir + "outlines/"+ this.outlineType +".png";
				
	var appendTo = hs.safari ? hs.container : null;
	this.graphic = hs.createElement('img', null, { position: 'absolute', left: '-9999px', 
		top: '-9999px' }, appendTo, true); // for onload trigger
	
	var pThis = this;
	this.graphic.onload = function() { pThis.onGraphicLoad(); };
	
	this.graphic.src = src;
};

HsOutline.prototype.onGraphicLoad = function () {
	var o = this.offset = this.graphic.width / 4;
	var pos = [[0,0],[0,-4],[-2,0],[0,-8],0,[-2,-8],[0,-2],[0,-6],[-2,-2]];
	for (var i = 0; i <= 8; i++) {
		if (pos[i]) {
			if (this.hasAlphaImageLoader) {
				var w = (i == 1 || i == 7) ? '100%' : this.graphic.width +'px';
				var div = hs.createElement('div', null, { width: '100%', height: '100%', position: 'relative', overflow: 'hidden'}, this.td[i], true);
				hs.createElement ('div', null, { 
						filter: "progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale, src='"+ this.graphic.src + "')", 
						position: 'absolute',
						width: w, 
						height: this.graphic.height +'px',
						left: (pos[i][0]*o)+'px',
						top: (pos[i][1]*o)+'px'
					}, 
				div,
				true);
			} else {
				hs.setStyles(this.td[i], { background: 'url('+ this.graphic.src +') '+ (pos[i][0]*o)+'px '+(pos[i][1]*o)+'px'});
			}
			
			// common
			var dim = 2 * o;
			hs.setStyles (this.td[i], { height: dim +'px', width: dim +'px' } );
		}
	}
	
	hs.pendingOutlines[this.outlineType] = this;
	if (this.onLoad) this.onLoad();
};

HsOutline.prototype.destroy = function() {
	hs.purge(this.table);
	try { this.table.parentNode.removeChild(this.table); } catch (e) {}
};

//-----------------------------------------------------------------------------
// The expander object
HsExpander = function(a, params, custom, contentType) {
	hs.continuePreloading = false;
		
	this.custom = custom;
	// override inline parameters
	for (i = 0; i < hs.overrides.length; i++) {
		var name = hs.overrides[i];
		if (params && typeof params[name] != 'undefined') this[name] = params[name];
		else this[name] = hs[name];
	}
	
	// get thumb
	var el;
	if (params && params.thumbnailId) el = hs.$(params.thumbnailId);
	else el = a.getElementsByTagName('IMG')[0];
	if (!el) el = a;
	
	// cancel other
	for (i = 0; i < hs.expanders.length; i++) {
		if (hs.expanders[i] && hs.expanders[i].thumb != el && !hs.expanders[i].onLoadStarted) {
			hs.expanders[i].cancelLoading();
		}
	}
	// check if already open
	for (i = 0; i < hs.expanders.length; i++) {
		if (hs.expanders[i] && hs.expanders[i].a == a) {
			hs.expanders[i].focus();
			return false;
		}		
	}

	if (!hs.allowMultipleInstances) {
		try { hs.expanders[hs.expandedImagesCounter - 1].doClose(); } catch (e){}
	}
	
	var key = this.key = hs.expandedImagesCounter++;
	hs.expanders[this.key] = this;
	if (contentType == 'html') {
		this.isHtml = true;
		this.contentType = 'html';
	} else {
		this.isImage = true;
		this.contentType = 'image';
	}
	this.a = a;
	
	this.thumbsUserSetId = el.id || a.id;
	this.thumb = el;		
	
	this.overlays = [];

	var pos = hs.position(el); 
	
	// instanciate the wrapper
	this.wrapper = hs.createElement(
		'div',
		{
			id: 'highslide-wrapper-'+ this.key,
			className: this.wrapperClassName
		},
		{
			visibility: 'hidden',
			position: 'absolute',
			zIndex: hs.zIndexCounter++
		}, null, true );
	this.wrapper.onmouseover = function (e) { 
    	try { hs.expanders[key].onMouseOver(); } catch (e) {} 
    };
    this.wrapper.onmouseout = function (e) { 
    	try { hs.expanders[key].onMouseOut(); } catch (e) {}
	};
	
	// store properties of thumbnail
	this.thumbWidth = el.width ? el.width : el.offsetWidth;		
	this.thumbHeight = el.height ? el.height : el.offsetHeight;
	this.thumbLeft = pos.x;
	this.thumbTop = pos.y;
	
	// thumb borders
	this.thumbOffsetBorderW = (this.thumb.offsetWidth - this.thumbWidth) / 2;
	this.thumbOffsetBorderH = (this.thumb.offsetHeight - this.thumbHeight) / 2;
	
	// get the wrapper
	hs.genContainer();
	if (hs.pendingOutlines[this.outlineType]) {
		this.connectOutline();
		this[this.contentType +'Create']();
	} else if (!this.outlineType) {
		this[this.contentType +'Create']();
	} else {
		this.displayLoading();
		var pThis = this;
		new HsOutline(this.outlineType, 
			function () { 
				pThis.connectOutline();
				pThis[pThis.contentType +'Create']();
			} 
		);
	}
	
};

HsExpander.prototype.connectOutline = function(x, y) {	
	var w = hs.pendingOutlines[this.outlineType];
	this.objOutline = w;
	hs.pendingOutlines[this.outlineType] = null;
};

HsExpander.prototype.displayLoading = function() {
	if (this.onLoadStarted || this.loading) return;
		
	this.originalCursor = this.a.style.cursor;
	this.a.style.cursor = 'wait';
	
	if (!hs.loading) {
		hs.loading = hs.createElement('a',
			{
				className: 'highslide-loading',
				title: hs.loadingTitle,
				innerHTML: hs.loadingText
			},
			{
				position: 'absolute',
				opacity: hs.loadingOpacity
			}, hs.container
		);
	}
	
	this.loading = hs.loading;
	this.loading.href = 'javascript:hs.expanders['+ this.key +'].cancelLoading()';
	this.loading.visibility = 'visible';		
	
	this.loading.style.left = (this.thumbLeft + this.thumbOffsetBorderW 
		+ (this.thumbWidth - this.loading.offsetWidth) / 2) +'px';
	this.loading.style.top = (this.thumbTop 
		+ (this.thumbHeight - this.loading.offsetHeight) / 2) +'px';
	setTimeout(
		"if (hs.expanders["+ this.key +"] && hs.expanders["+ this.key +"].loading) "
		+ "hs.expanders["+ this.key +"].loading.style.visibility = 'visible';", 
		100
	);
};

HsExpander.prototype.imageCreate = function() {
	var key = this.key;

	var img = document.createElement('img');
    this.content = img;
    img.onload = function () { try { hs.expanders[key].onLoad(); } catch (e) {} };
    img.className = 'highslide-image';
    img.style.visibility = 'hidden'; // prevent flickering in IE
    img.style.display = 'block';
	img.style.position = 'absolute';
	img.style.maxWidth = 'none';
    img.style.zIndex = 3;
    img.title = hs.restoreTitle;
    if (hs.safari) hs.container.appendChild(img);
    // uncomment this to flush img size:
    // if (hs.ie) img.src = null;
	img.src = hs.getSrc(this.a);
	
	this.displayLoading();
};

HsExpander.prototype.onLoad = function() {
	try { 
	
		if (!this.content) return;
		if (this.onLoadStarted) return; // old Gecko loop
		else this.onLoadStarted = true;
		
			   
		if (this.loading) {
			this.loading.style.visibility = 'hidden';
			this.loading = null;
			this.a.style.cursor = this.originalCursor || '';
		}
		if (this.isImage) {			
			this.newWidth = this.content.width;
			this.newHeight = this.content.height;
			this.fullExpandWidth = this.newWidth;
			this.fullExpandHeight = this.newHeight;
			
			this.content.width = this.thumbWidth;
			this.content.height = this.thumbHeight;
			
		} else if (this.htmlGetSize) this.htmlGetSize();
		
		this.marginBottom = hs.marginBottom;
		this.getCaption();
		
		this.wrapper.appendChild(this.content);
		this.content.style.position = 'relative'; // Saf
		if (this.caption) this.wrapper.appendChild(this.caption);
		this.wrapper.style.left = this.thumbLeft +'px';
		this.wrapper.style.top = this.thumbTop +'px';
		hs.container.appendChild(this.wrapper);
		
		// correct for borders
		this.offsetBorderW = (this.content.offsetWidth - this.thumbWidth) / 2;
		this.offsetBorderH = (this.content.offsetHeight - this.thumbHeight) / 2;
		var modMarginRight = hs.marginRight + 2 * this.offsetBorderW;
		this.marginBottom += 2 * this.offsetBorderH;
		
		var ratio = this.newWidth / this.newHeight;
		var minWidth = this.allowSizeReduction ? this.minWidth : this.newWidth;
		var minHeight = this.allowSizeReduction ? this.minHeight : this.newHeight;
		
		var justify = { x: 'auto', y: 'auto' };
		if (this.align == 'center') {
			justify.x = 'center';
			justify.y = 'center';
		} else {
			if (this.anchor.match(/^top/)) justify.y = null;
			if (this.anchor.match(/right$/)) justify.x = 'max';
			if (this.anchor.match(/^bottom/)) justify.y = 'max';
			if (this.anchor.match(/left$/)) justify.x = null;
		}
		
		client = new hs.clientInfo();		
		
		// justify
		this.x = { 
			min: parseInt(this.thumbLeft) - this.offsetBorderW + this.thumbOffsetBorderW,
			span: this.newWidth,
			minSpan: this.newWidth < minWidth ? this.newWidth : minWidth,
			justify: justify.x,
			target: this.targetX,
			marginMin: hs.marginLeft, 
			marginMax: modMarginRight,
			scroll: client.scrollLeft,
			clientSpan: client.width,
			thumbSpan: this.thumbWidth
		};
		var oldRight = this.x.min + parseInt(this.thumbWidth);
		this.x = this.justify(this.x);
		this.y = { 
			min: parseInt(this.thumbTop) - this.offsetBorderH + this.thumbOffsetBorderH,
			span: this.newHeight,
			minSpan: this.newHeight < minHeight ? this.newHeight : minHeight,
			justify: justify.y,
			target: this.targetY,
			marginMin: hs.marginTop, 
			marginMax: this.marginBottom, 
			scroll: client.scrollTop,
			clientSpan: client.height,
			thumbSpan: this.thumbHeight
		};
		var oldBottom = this.y.min + parseInt(this.thumbHeight);
		this.y = this.justify(this.y);
		
		if (this.isHtml) this.htmlSizeOperations();	
		if (this.isImage) this.correctRatio(ratio);

		var x = this.x;
		var y = this.y;
		
		this.show();
	} catch (e) {
		if (hs.expanders[this.key] && hs.expanders[this.key].a) 
			window.location.href = hs.getSrc(hs.expanders[this.key].a);
	}
};

HsExpander.prototype.show = function () {
	// Selectbox bug
	var imgPos = {x: this.x.min - 20, y: this.y.min - 20, w: this.x.span + 40, h: this.y.span + 40 + this.spaceForCaption};
	hs.hideSelects = (hs.ie && hs.ieVersion() < 7);
	if (hs.hideSelects) this.showHideElements('SELECT', 'hidden', imgPos);
	// Iframes bug
	hs.hideIframes = (window.opera || navigator.vendor == 'KDE' || (hs.ie && hs.ieVersion() < 5.5));
	if (hs.hideIframes) this.showHideElements('IFRAME', 'hidden', imgPos);
	
	// Make outline ready	
	if (this.objOutline && !this.outlineWhileAnimating) this.positionOutline(this.x.min, this.y.min, this.x.span, this.y.span);
	var o2 = this.objOutline ? this.objOutline.offset : 0;
	
	// Apply size change		
	this.changeSize(
		1,
		this.thumbLeft + this.thumbOffsetBorderW - this.offsetBorderW,
		this.thumbTop + this.thumbOffsetBorderH - this.offsetBorderH,
		this.thumbWidth,
		this.thumbHeight,
		this.x.min,
		this.y.min,
		this.x.span,
		this.y.span, 
		hs.expandDuration,
		hs.expandSteps,
		hs.outlineStartOffset,
		o2
	);
};

HsExpander.prototype.justify = function (p) {
	
	var tgt, dim = p == this.x ? 'x' : 'y';
	if (p.target && p.target.match(/ /)) {
		tgt = p.target.split(' ');
		p.target = tgt[0];
	}
	if (p.target && hs.$(p.target)) {
		p.min = hs.position(hs.$(p.target))[dim];
		if (tgt && tgt[1] && tgt[1].match(/^[-]?[0-9]+px$/)) p.min += parseInt(tgt[1]);
		
	} else if (p.justify == 'auto' || p.justify == 'center') {
		var hasMovedMin = false;
		var allowReduce = true;
		
		// calculate p.min
		if (p.justify == 'center') p.min = Math.round(p.scroll + (p.clientSpan - p.span - p.marginMax) / 2);
		else p.min = Math.round(p.min - ((p.span - p.thumbSpan) / 2)); // auto
		
		if (p.min < p.scroll + p.marginMin) {
			p.min = p.scroll + p.marginMin;
			hasMovedMin = true;		
		}
		
		if (p.span < p.minSpan) {
			p.span = p.minSpan;
			allowReduce = false;
		}
		// calculate right/newWidth
		if (p.min + p.span > p.scroll + p.clientSpan - p.marginMax) {
			if (hasMovedMin && allowReduce) p.span = p.clientSpan - p.marginMin - p.marginMax; // can't expand more
			else if (p.span < p.clientSpan - p.marginMin - p.marginMax) { // move newTop up
				p.min = p.scroll + p.clientSpan - p.span - p.marginMin - p.marginMax;
			} else { // image larger than client
				p.min = p.scroll + p.marginMin;
				if (allowReduce) p.span = p.clientSpan - p.marginMin - p.marginMax;
			}
			
		}
		
		if (p.span < p.minSpan) {
			p.span = p.minSpan;
			allowReduce = false;
		}
		
	} else if (p.justify == 'max') {
		p.min = Math.floor(p.min - p.span + p.thumbSpan);
	}
		
	if (p.min < p.marginMin) {
		tmpMin = p.min;
		p.min = p.marginMin; 
		if (allowReduce) p.span = p.span - (p.min - tmpMin);
	}
	return p;
};

HsExpander.prototype.correctRatio = function(ratio) {
	var x = this.x;
	var y = this.y;
	var changed = false;
	if (x.span / y.span > ratio) { // width greater
		var tmpWidth = x.span;
		x.span = y.span * ratio;
		if (x.span < x.minSpan) { // below minWidth
			x.span = x.minSpan;	
			y.span = x.span / ratio;
		}
		changed = true;
	
	} else if (x.span / y.span < ratio) { // height greater
		var tmpHeight = y.span;
		y.span = x.span / ratio;
		changed = true;
	}
	
	if (changed) {
		x.min = parseInt(this.thumbLeft) - this.offsetBorderW + this.thumbOffsetBorderW;
		x.minSpan = x.span;
		this.x = this.justify(x);
		
		y.min = parseInt(this.thumbTop) - this.offsetBorderH + this.thumbOffsetBorderH;
		y.minSpan = y.span;
		this.y = this.justify(y);
	}
};

HsExpander.prototype.changeSize = function(dir, x1, y1, w1, h1, x2, y2, w2, h2, dur, steps, oo1, oo2) {
	var dW = (w2 - w1) / steps,
	dH = (h2 - h1) / steps,
	dX = (x2 - x1) / steps,
	dY = (y2 - y1) / steps,
	dOo = (oo2 - oo1) /steps,
	t,
	exp = "hs.expanders["+ this.key +"]";
	for (i = 1; i <= steps; i++) {
		w1 += dW;
		h1 += dH;
		x1 += dX;
		y1 += dY;
		oo1 += dOo;
		t = Math.round(i * (dur / steps));
		
		var s = "try {";
		if (i == 1) {
			s += exp +".content.style.visibility = 'visible';"
				+ "if ("+ exp +".thumb.tagName == 'IMG' && hs.hideThumbOnExpand) "+ exp +".thumb.style.visibility = 'hidden';"
		}
		if (i == steps) {
			w1 = w2;
			h1 = h2;
			x1 = x2;
			y1 = y2;
			oo1 = oo2;
		}
		s += exp +"."+ this.contentType +"SetSize("+ Math.round(w1) +", "+ Math.round(h1) +", "
			+ Math.round(x1) +", "+ Math.round(y1) +", "+ Math.round(oo1);
		s += ");} catch (e) {}";
		setTimeout(s, t);
	}
	if (dir == 1) {
		setTimeout('try { '+ exp +'.objOutline.table.style.visibility = "visible"; } catch (e){}', t);
		setTimeout('try { '+ exp +'.onExpanded(); } catch(e){}', t+50);
	}
	else setTimeout('try { '+ exp +'.onEndClose(); } catch(e){}', t);
		
};

HsExpander.prototype.imageSetSize = function (w, h, x, y, offset) {
	try {
		this.content.width = w;
		this.content.height = h;
		
		if (this.objOutline && this.outlineWhileAnimating) {
			var o = this.objOutline.offset - offset;
			this.positionOutline(x + o, y + o, w - 2 * o, h - 2 * o, 1);
		}
		
		hs.setStyles ( this.wrapper,
			{
				'visibility': 'visible',
				'left': x +'px',
				'top': y +'px'
			}
		);
		
	} catch (e) { window.location.href = hs.getSrc(this.a);	}
};

HsExpander.prototype.positionOutline = function(x, y, w, h, vis) {
	if (!this.objOutline) return;
	var o = this.objOutline;
	if (vis) o.table.style.visibility = 'visible';
	o.table.style.left = (x - o.offset) +'px';
	o.table.style.top = (y - o.offset) +'px';
	o.table.style.width = (w + 2 * (this.offsetBorderW + o.offset)) +'px';
	w += 2 * (this.offsetBorderW - o.offset);
	h += + 2 * (this.offsetBorderH - o.offset);
	o.td[4].style.width = w >= 0 ? w +'px' : 0;
	o.td[4].style.height = h >= 0 ? h +'px' : 0;
	if (o.hasAlphaImageLoader) o.td[3].style.height = o.td[5].style.height = o.td[4].style.height;
};

HsExpander.prototype.onExpanded = function() {
	
	this.isExpanded = true;
	this.focus();
	if (this.isHtml && this.objectLoadTime == 'after') this.writeExtendedContent();
	this.createCustomOverlays();
	if (hs.showCredits) this.writeCredits();
	
	if (this.caption) this.writeCaption();
	
	if (this.fullExpandWidth > this.x.span) this.createFullExpand();
	if (!this.caption) this.onDisplayFinished();
};

HsExpander.prototype.onDisplayFinished = function() {
	var key = this.key;
	var outlineType = this.outlineType;
	new HsOutline(outlineType, function () { 
		try { hs.expanders[key].preloadNext();	} catch (e) {}
	});
};

HsExpander.prototype.preloadNext = function() {
	var next = hs.getAdjacentAnchor(this.key, 1);	
	if (next.onclick.toString().match(/hs\.expand/)) 
		var img = hs.createElement('img', { src: hs.getSrc(next) });
};

HsExpander.prototype.cancelLoading = function() {
	this.a.style.cursor = this.originalCursor;	
	if (this.loading) hs.loading.style.visibility = 'hidden';		
	hs.expanders[this.key] = null;
};

HsExpander.prototype.writeCredits = function () {
	var credits = hs.createElement('a',
		{
			href: hs.creditsHref,
			className: 'highslide-credits',
			innerHTML: hs.creditsText,
			title: hs.creditsTitle
		}
	);
	this.createOverlay(credits, 'top left');
};

HsExpander.prototype.getCaption = function() {
	if (!this.captionId && this.thumbsUserSetId)  this.captionId = 'caption-for-'+ this.thumbsUserSetId;
	if (this.captionId) {
		this.caption = hs.getNode(this.captionId);
	}
	if (this.captionTemplateId) {
		var s = (this.caption) ? this.caption.innerHTML : '';
		this.caption = hs.getNode(this.captionTemplateId);
		if (this.caption) this.caption.innerHTML
			= this.caption.innerHTML.replace(/\s/g, ' ').replace('{caption}', s);
	}		
	if (this.caption) this.marginBottom += this.spaceForCaption;

};

HsExpander.prototype.writeCaption = function() {
	try {
		this.wrapper.style.width = this.wrapper.offsetWidth +'px';	
		this.caption.style.visibility = 'hidden';
		this.caption.className += ' highslide-display-block';
		
		var height;
		if (hs.ie && (hs.ieVersion() < 6 || document.compatMode == 'BackCompat')) {
			height = this.caption.offsetHeight;
		} else {
			var temp = hs.createElement('div', {innerHTML: this.caption.innerHTML}, 
				null, null, true); // to get height
			this.caption.innerHTML = '';
			this.caption.appendChild(temp);	
			height = this.caption.childNodes[0].offsetHeight;//parseInt(hs.getStyle(this.caption, 'height'));
			this.caption.innerHTML = this.caption.childNodes[0].innerHTML;
		}
		hs.setStyles(this.caption, { overflow: 'hidden', height: 0, zIndex: 2 });
		
		if (hs.captionSlideSpeed) {
			step = Math.round(height/50);
			if (step == 0) step = 1;
			step = step * hs.captionSlideSpeed;
		} else {
			this.placeCaption(height, 1);
			return;
		}

		var t = 0;
		for (var h = height % step; h <= height; h += step, t += 10) {
			var end = (h == height) ? 1 : 0;
			var eval = "try { "
				+ "hs.expanders["+ this.key +"].placeCaption("+ h +", "+ end +");"
				+ "} catch (e) {}";			
			setTimeout (eval, t);
		}
	
	} catch (e) {}	
};

HsExpander.prototype.placeCaption = function(height, end) {
	if (!this.caption) return;
	this.caption.style.height = height +'px';
	this.caption.style.visibility = 'visible';
	this.y.span = this.wrapper.offsetHeight - 2 * this.offsetBorderH;
	var o = this.objOutline;
	if (o) {
		o.td[4].style.height = (this.wrapper.offsetHeight - 2 * this.objOutline.offset) +'px';
		if (o.hasAlphaImageLoader) o.td[3].style.height = o.td[5].style.height = o.td[4].style.height;
	}
	if (end) this.onDisplayFinished();
};

HsExpander.prototype.showHideElements = function (tagName, visibility, imgPos) {
	var els = document.getElementsByTagName(tagName);
	if (els) {			
		for (i = 0; i < els.length; i++) {
			if (els[i].nodeName == tagName) {  
				var hiddenBy = els[i].getAttribute('hidden-by');
				 
				if (visibility == 'visible' && hiddenBy) {
					hiddenBy = hiddenBy.replace('['+ this.key +']', '');
					els[i].setAttribute('hidden-by', hiddenBy);
					if (!hiddenBy) els[i].style.visibility = 'visible';				
					
				} else if (visibility == 'hidden') { // hide if behind
					var elPos = hs.position(els[i]);
					elPos.w = els[i].offsetWidth;
					elPos.h = els[i].offsetHeight;
				
					var clearsX = (elPos.x + elPos.w < imgPos.x || elPos.x > imgPos.x + imgPos.w);
					var clearsY = (elPos.y + elPos.h < imgPos.y || elPos.y > imgPos.y + imgPos.h);
					var wrapperKey = hs.getWrapperKey(els[i]);
					if (!clearsX && !clearsY && wrapperKey != this.key) { // element falls behind image
						if (!els[i].currentStyle || (els[i].currentStyle && els[i].currentStyle['visibility'] != 'hidden')) { // IE
							if (!hiddenBy) {
								els[i].setAttribute('hidden-by', '['+ this.key +']');
							} else if (!hiddenBy.match('['+ this.key +']')) {
								els[i].setAttribute('hidden-by', hiddenBy + '['+ this.key +']');
							}
							els[i].style.visibility = 'hidden';	  
						}
					} else if (hiddenBy == '['+ this.key +']' || hs.focusKey == wrapperKey) { // on move
						els[i].setAttribute('hidden-by', '');
						els[i].style.visibility = 'visible';
					} else if (hiddenBy && hiddenBy.match('['+ this.key +']')) {
						els[i].setAttribute('hidden-by', hiddenBy.replace('['+ this.key +']', ''));
					}
				}   
			}
		}
	}
};

HsExpander.prototype.focus = function() {
	// blur others
	for (i = 0; i < hs.expanders.length; i++) {
		if (hs.expanders[i] && i == hs.focusKey) {
			var blurExp = hs.expanders[i];
			blurExp.content.className += ' highslide-'+ blurExp.contentType +'-blur';
			if (blurExp.caption) {
				blurExp.caption.className += ' highslide-caption-blur';
			}
			if (blurExp.isImage) {
				blurExp.content.style.cursor = hs.ie ? 'hand' : 'pointer';
				blurExp.content.title = hs.focusTitle;	
			}
		}
	}
	
	// focus this
	this.wrapper.style.zIndex = hs.zIndexCounter++;
	if (this.objOutline) this.objOutline.table.style.zIndex = this.wrapper.style.zIndex;
	
	this.content.className = 'highslide-'+ this.contentType;
	if (this.caption) {
		this.caption.className = this.caption.className.replace(' highslide-caption-blur', '');
	}
	
	if (this.isImage) {
		this.content.title = hs.restoreTitle;
		
		hs.styleRestoreCursor = window.opera ? 'pointer' : 'url('+ hs.graphicsDir + hs.restoreCursor +'), pointer';
		if (hs.ie && hs.ieVersion() < 6) hs.styleRestoreCursor = 'hand';
		this.content.style.cursor = hs.styleRestoreCursor;
	}
	
	hs.focusKey = this.key;	
	hs.addEventListener(document, 'keydown', hs.keyHandler);
	
};

HsExpander.prototype.onClick = function() {
	this.doClose();
};

HsExpander.prototype.doClose = function() {
	hs.removeEventListener(document, 'keydown', hs.keyHandler);
	try {
		this.isClosing = true;
		
		var x = parseInt(this.wrapper.style.left);
		var y = parseInt(this.wrapper.style.top);
		var w = (this.isImage) ? this.content.width : parseInt(this.content.style.width);
		var h = (this.isImage) ? this.content.height : parseInt(this.content.style.height);
		
		if (this.objOutline) {
			if (this.outlineWhileAnimating) this.positionOutline(x, y, w, h);
			else if (this.preserveContent) this.objOutline.table.style.visibility = 'hidden';
			else this.objOutline.destroy();
		}
		
		// remove children
		var n = this.wrapper.childNodes.length;
		for (i = n - 1; i >= 0 ; i--) {
			var child = this.wrapper.childNodes[i];
			if (child != this.content) {
				hs.purge(this.wrapper.childNodes[i]);
				this.wrapper.removeChild(this.wrapper.childNodes[i]);
			}
		}
		if (this.isHtml) this.htmlOnClose();
		
		this.wrapper.style.width = 'auto';
		this.content.style.cursor = 'default';
		var o2 = this.objOutline ? this.objOutline.offset : 0;
		
		this.changeSize(
			-1,
			x,
			y,
			w,
			h,
			this.thumbLeft - this.offsetBorderW + this.thumbOffsetBorderW,
			this.thumbTop - this.offsetBorderH + this.thumbOffsetBorderH,
			this.thumbWidth,
			this.thumbHeight, 
			hs.restoreDuration,
			hs.restoreSteps,
			o2,
			hs.outlineStartOffset
		);
		
	} catch (e) {
		this.onEndClose();
	} 
};

HsExpander.prototype.onEndClose = function () {
	this.thumb.style.visibility = 'visible';
	
	if (hs.hideSelects) this.showHideElements('SELECT', 'visible');
	if (hs.hideIframes) this.showHideElements('IFRAME', 'visible');
	
	if (this.isHtml && this.preserveContent) this.sleep();
	else {
		if (this.objOutline && this.outlineWhileAnimating) this.objOutline.destroy();
		hs.purge(this.wrapper);
		if (hs.ie && hs.ieVersion() < 5.5) this.wrapper.innerHTML = ''; // crash
		else this.wrapper.parentNode.removeChild(this.wrapper);
	}
	hs.expanders[this.key] = null;

	hs.cleanUp();
};

HsExpander.prototype.createOverlay = function (el, position, hideOnMouseOut, opacity) {
	if (typeof el == 'string') el = hs.getNode(el);
	if (!el || typeof el == 'string' || !this.isImage) return;
	
	var overlay = hs.createElement(
		'div',
		null,
		{
			'left' : 0,
			'top' : 0,
			'position' : 'absolute',
			'zIndex' : 3,
			'visibility' : 'hidden'
		},
		this.wrapper,
		true
	);
	if (opacity) hs.setStyles(el, { 'opacity': opacity });
	el.className += ' highslide-display-block';
	overlay.appendChild(el);	
	
	var left = this.offsetBorderW;
	var dLeft = this.content.width - overlay.offsetWidth;
	var top = this.offsetBorderH;
	var dTop = this.content.height - overlay.offsetHeight;
	
	if (!position) position = 'center center';
	if (position.match(/^bottom/)) top += dTop;
	if (position.match(/^center/)) top += dTop / 2;
	if (position.match(/right$/)) left += dLeft;
	if (position.match(/center$/)) left += dLeft / 2;
	overlay.style.left = left +'px';
	overlay.style.top = top +'px';
	
	if (hideOnMouseOut) overlay.setAttribute('hideOnMouseOut', true);
	if (!opacity) opacity = 1;
	overlay.setAttribute('opacity', opacity);
	hs.fade(overlay, 0, opacity, 1);
	
	hs.push(this.overlays, overlay);
};

HsExpander.prototype.createCustomOverlays = function() {
	for (i = 0; i < hs.overlays.length; i++) {
		var o = hs.overlays[i];
		if (o.thumbnailId == null || o.thumbnailId == this.thumbsUserSetId) {
			this.createOverlay(o.overlayId, o.position, o.hideOnMouseOut, o.opacity);
		}
	}
};

HsExpander.prototype.onMouseOver = function () {
	for (i = 0; i < this.overlays.length; i++) {
		var o = this.overlays[i];
		if (o.getAttribute('hideOnMouseOut'))
			hs.fade(o, 0, o.getAttribute('opacity'), 1);
	}
};

HsExpander.prototype.onMouseOut = function() {
	for (i = 0; i < this.overlays.length; i++) {
		var o = this.overlays[i];
		if (o.getAttribute('hideOnMouseOut')) 
			hs.fade(o, o.getAttribute('opacity'), 0, -1);
	}
};

HsExpander.prototype.createFullExpand = function () {
	var a = hs.createElement(
		'a',
		{
			href: 'javascript:hs.expanders['+ this.key +'].doFullExpand();',
			title: hs.fullExpandTitle
		},
		{
			background: 'url('+ hs.graphicsDir + hs.fullExpandIcon+')',
			display: 'block',
			margin: '0 10px 10px 0',
			width: '45px',
			height: '44px'
		}, null, true
	);
	
	this.createOverlay(a, 'bottom right', true, 0.75);
	this.fullExpandIcon = a;
};

HsExpander.prototype.doFullExpand = function () {
	try {
		hs.purge(this.fullExpandIcon);
		this.fullExpandIcon.parentNode.removeChild(this.fullExpandIcon);
		this.focus();
		
		this.x.min = parseInt(this.wrapper.style.left) - (this.fullExpandWidth - this.content.width) / 2;
		if (this.x.min < hs.marginLeft) this.x.min = hs.marginLeft;		
		this.wrapper.style.left = this.x.min +'px';
		
		var borderOffset = this.wrapper.offsetWidth - this.content.width;		
		
		this.content.width = this.fullExpandWidth;
		this.content.height = this.fullExpandHeight;
		
		this.x.span = this.content.width;
		this.wrapper.style.width = (this.x.span + borderOffset) +'px';
		
		this.y.span = this.wrapper.offsetHeight - 2 * this.offsetBorderH;
		this.positionOutline(this.x.min, this.y.min, this.x.span, this.y.span);
		
		// reposition overlays
		for (var i = 0; i < this.overlays.length; i++) {
			hs.purge(this.overlays[i]);
			this.overlays[i].parentNode.removeChild(this.overlays[i]);
		}	
		if (hs.showCredits) this.writeCredits();
		this.createCustomOverlays();
		
		this.redoShowHide();
	
	} catch (e) {
		window.location.href = this.content.src;
	}
};

// on end move and resize
HsExpander.prototype.redoShowHide = function() {
	var imgPos = {
		x: parseInt(this.wrapper.style.left) - 20, 
		y: parseInt(this.wrapper.style.top) - 20, 
		w: this.content.offsetWidth + 40, 
		h: this.content.offsetHeight + 40 + this.spaceForCaption
	};
	if (hs.hideSelects) this.showHideElements('SELECT', 'hidden', imgPos);
	if (hs.hideIframes) this.showHideElements('IFRAME', 'hidden', imgPos);

};

// set handlers
hs.addEventListener(document, 'mousedown', hs.mouseClickHandler);
hs.addEventListener(document, 'mouseup', hs.mouseClickHandler);
hs.addEventListener(window, 'load', hs.preloadImages);
/******************************************************************************
Name:    Highslide HTML Extension
Version: 3.2.7 (September 9 2007)
Author:  Torstein H�nsi
Support: http://vikjavev.no/highslide/forum
Email:   See http://vikjavev.no/megsjol

Licence:
Highslide JS is licensed under a Creative Commons Attribution-NonCommercial 2.5
License (http://creativecommons.org/licenses/by-nc/2.5/).

You are free:
	* to copy, distribute, display, and perform the work
	* to make derivative works

Under the following conditions:
	* Attribution. You must attribute the work in the manner  specified by  the
	  author or licensor.
	* Noncommercial. You may not use this work for commercial purposes.

* For  any  reuse  or  distribution, you  must make clear to others the license
  terms of this work.
* Any  of  these  conditions  can  be  waived  if  you  get permission from the 
  copyright holder.

Your fair use and other rights are in no way affected by the above.
******************************************************************************/

hs.allowWidthReduction = false;
hs.allowHeightReduction = true;
hs.objectLoadTime = 'before'; // Load iframes 'before' or 'after' expansion.
hs.cacheAjax = true; // Cache ajax popups for instant display. Can be overridden for each popup.
hs.preserveContent = true; // Preserve changes made to the content and position of HTML popups.

// These properties can be overridden in the function call for each expander:
hs.push(hs.overrides, 'contentId');
hs.push(hs.overrides, 'allowWidthReduction');
hs.push(hs.overrides, 'allowHeightReduction');
hs.push(hs.overrides, 'objectType');
hs.push(hs.overrides, 'objectWidth');
hs.push(hs.overrides, 'objectHeight');
hs.push(hs.overrides, 'objectLoadTime');
hs.push(hs.overrides, 'swfObject');
hs.push(hs.overrides, 'cacheAjax');
hs.push(hs.overrides, 'preserveContent');

// Internal
hs.preloadTheseAjax = [];
hs.cacheBindings = [];
hs.sleeping = [];
hs.clearing = hs.createElement('div', null, 
		{ clear: 'both', borderTop: '1px solid white' }, null, true);

hs.htmlExpand = function(a, params, custom) {
	if (!hs.$(params.contentId) && !hs.clones[params.contentId]) return true;
	
	for (var i = 0; i < hs.sleeping.length; i++) {
		if (hs.sleeping[i] && hs.sleeping[i].a == a) {
			hs.sleeping[i].awake();
			hs.sleeping[i] = null;
			return false;
		}
	}
	try {
		hs.hasHtmlexpanders = true;
    	new HsExpander(a, params, custom, 'html');
    	return false;
	} catch (e) {
		return true;
	}	
};

hs.identifyContainer = function (parent, className) {
	for (i = 0; i < parent.childNodes.length; i++) {
    	if (parent.childNodes[i].className == className) {
			return parent.childNodes[i];
		}
	}
};

hs.preloadAjax = function (e) {
	var aTags = document.getElementsByTagName('A');
	var a, re;
	for (i = 0; i < aTags.length; i++) {
		a = aTags[i];
		re = hs.isHsAnchor(a);
		if (re && re[0] == 'hs.htmlExpand' && hs.getParam(a, 'objectType') == 'ajax' && hs.getParam(a, 'cacheAjax')) {
			hs.push(hs.preloadTheseAjax, a);
		}
	}
	hs.preloadAjaxElement(0);
};

hs.preloadAjaxElement = function (i) {
	if (!hs.preloadTheseAjax[i]) return;
	var a = hs.preloadTheseAjax[i];
	var cache = hs.getNode(hs.getParam(a, 'contentId'));
	var ajax = new HsAjax(a, cache);	
   	ajax.onError = function () { };
   	ajax.onLoad = function () {
   		hs.push(hs.cacheBindings, [a, cache]);
   		hs.preloadAjaxElement(i + 1);
   	};
   	ajax.run();
};

hs.getCacheBinding = function (a) {
	for (i = 0; i < hs.cacheBindings.length; i++) {
		if (hs.cacheBindings[i][0] == a) {
			var c = hs.cacheBindings[i][1];
			hs.cacheBindings[i][1] = c.cloneNode(1);
			return c;
		}
	}
};

HsExpander.prototype.htmlCreate = function () {
	this.tempContainer = hs.createElement('div', null,
		{
			padding: '0 '+ hs.marginRight +'px 0 '+ hs.marginLeft +'px',
			position: 'absolute',
			left: 0,
			top: 0
		},
		document.body
	);
	this.innerContent = hs.getCacheBinding(this.a);
	if (!this.innerContent) this.innerContent = hs.getNode(this.contentId);
	
	this.setObjContainerSize(this.innerContent);
	this.tempContainer.appendChild(this.innerContent); // to get full width
	hs.setStyles (this.innerContent, { position: 'relative', visibility: 'hidden' });
	this.innerContent.className += ' highslide-display-block';
	
	this.content = hs.createElement(
    	'div',
    	{	className: 'highslide-html' },
		{
			position: 'relative',
			zIndex: 3,
			overflow: 'hidden',
			width: this.thumbWidth +'px',
			height: this.thumbHeight +'px'
		}
	);
    
	if (this.objectType == 'ajax' && !hs.getCacheBinding(this.a)) {
    	var ajax = new HsAjax(this.a, this.innerContent);
    	var pThis = this;
    	ajax.onLoad = function () {	pThis.onLoad(); };
    	ajax.onError = function () { location.href = hs.getSrc(this.a); };
    	ajax.run();
	}
    else this.onLoad();
};
    
HsExpander.prototype.htmlGetSize = function() {
	this.innerContent.appendChild(hs.clearing);
	this.newWidth = this.innerContent.offsetWidth;
    this.newHeight = this.innerContent.offsetHeight;
    this.innerContent.removeChild(hs.clearing);
    if (hs.ie && this.newHeight > parseInt(this.innerContent.currentStyle.height)) { // ie css bug
		this.newHeight = parseInt(this.innerContent.currentStyle.height);
	}
};

HsExpander.prototype.setObjContainerSize = function(parent, auto) {
	if (this.swfObject || this.objectType == 'iframe') {
		var c = hs.identifyContainer(parent, 'highslide-body');
		c.style.width = this.swfObject ? this.swfObject.attributes.width +'px' : this.objectWidth +'px';
		c.style.height = this.swfObject ? this.swfObject.attributes.height +'px' : this.objectHeight +'px';
	}
};

HsExpander.prototype.writeExtendedContent = function () {
	if (this.hasExtendedContent) return;
	this.objContainer = hs.identifyContainer(this.innerContent, 'highslide-body');
	if (this.objectType == 'iframe') {
		if (hs.ie && hs.ieVersion() < 5.5) window.location.href = hs.getSrc(this.a);
		var key = this.key;
		this.objContainer.innerHTML = '';
		this.iframe = hs.createElement('iframe', { frameBorder: 0 },
		   { width: this.objectWidth +'px', height: this.objectHeight +'px' }, 
		   this.objContainer);
		if (hs.safari) this.iframe.src = null;
		this.iframe.src = hs.getSrc(this.a);
		
		if (this.objectLoadTime == 'after') this.correctIframeSize();
		
	} else if (this.swfObject) {	
		this.objContainer.id = this.objContainer.id || 'hs-flash-id-' + this.key;
		this.swfObject.write(this.objContainer.id);	
	}
	this.hasExtendedContent = true;
};

HsExpander.prototype.correctIframeSize = function () {
	var wDiff = this.innerContent.offsetWidth - this.objContainer.offsetWidth;
	if (wDiff < 0) wDiff = 0;
    
	var hDiff = this.innerContent.offsetHeight - this.objContainer.offsetHeight;
	hs.setStyles(this.iframe, { width: (this.x.span - wDiff) +'px', height: (this.y.span - hDiff) +'px' });
    hs.setStyles(this.objContainer, { width: this.iframe.style.width, height: this.iframe.style.height });
    
    this.scrollingContent = this.iframe;
    this.scrollerDiv = this.scrollingContent;
};

HsExpander.prototype.htmlSizeOperations = function () {
	this.setObjContainerSize(this.innerContent);
	
	if (this.objectLoadTime == 'before') this.writeExtendedContent();		

    // handle minimum size
    if (this.x.span < this.newWidth && !this.allowWidthReduction) this.x.span = this.newWidth;
    if (this.y.span < this.newHeight && !this.allowHeightReduction) this.y.span = this.newHeight;
    this.scrollerDiv = this.innerContent;
    
    this.mediumContent = hs.createElement('div', null, 
    	{ 
    		width: this.x.span +'px',
    		position: 'relative',
    		left: (this.x.min - this.thumbLeft) +'px',
    		top: (this.y.min - this.thumbTop) +'px'
    	}, this.content, true);
	
    this.mediumContent.appendChild(this.innerContent);
    document.body.removeChild(this.tempContainer);
    hs.setStyles(this.innerContent, { border: 'none', width: 'auto', height: 'auto' });
    
    var node = hs.identifyContainer(this.innerContent, 'highslide-body');
    if (node && !this.swfObject && this.objectType != 'iframe') {    
    	var cNode = node; // wrap to get true size
    	node = hs.createElement(cNode.nodeName, null, {overflow: 'hidden'}, null, true);
    	cNode.parentNode.insertBefore(node, cNode);
    	node.appendChild(hs.clearing); // IE6
    	node.appendChild(cNode);
    	
    	var wDiff = this.innerContent.offsetWidth - node.offsetWidth;
    	var hDiff = this.innerContent.offsetHeight - node.offsetHeight;
    	node.removeChild(hs.clearing);
    	
    	var kdeBugCorr = hs.safari || navigator.vendor == 'KDE' ? 1 : 0; // KDE repainting bug
    	hs.setStyles(node, { 
    			width: (this.x.span - wDiff - kdeBugCorr) +'px', 
    			height: (this.y.span - hDiff) +'px',
    			overflow: 'auto', 
    			position: 'relative' 
    		} 
    	);
		if (kdeBugCorr && cNode.offsetHeight > node.offsetHeight)	{
    		node.style.width = (parseInt(node.style.width) + kdeBugCorr) + 'px';
		}
    	this.scrollingContent = node;
    	this.scrollerDiv = this.scrollingContent;
    	
	} 
	
    if (this.iframe && this.objectLoadTime == 'before') this.correctIframeSize();
    if (!this.scrollingContent && this.y.span < this.mediumContent.offsetHeight) this.scrollerDiv = this.content;
	
	if (this.scrollerDiv == this.content && !this.allowWidthReduction && this.objectType != 'iframe') {
		this.x.span += 17; // room for scrollbars
	}
	if (this.scrollerDiv && this.scrollerDiv.offsetHeight > this.scrollerDiv.parentNode.offsetHeight) {
		setTimeout("try { hs.expanders["+ this.key +"].scrollerDiv.style.overflow = 'auto'; } catch(e) {}",
			 hs.expandDuration);
	}
};

HsExpander.prototype.htmlSetSize = function (w, h, x, y, offset, end) {
	try {
		hs.setStyles(this.wrapper, { visibility: 'visible', left: x +'px', top: y +'px'});
		hs.setStyles(this.content, { width: w +'px', height: h +'px' });
		hs.setStyles(this.mediumContent, { left: (this.x.min - x) +'px', top: (this.y.min - y) +'px' });
		
		this.innerContent.style.visibility = 'visible';
		
		if (this.objOutline && this.outlineWhileAnimating) {
			var o = this.objOutline.offset - offset;
			this.positionOutline(x + o, y + o, w - 2*o, h - 2*o, 1);
		}
				
	} catch (e) {
		window.location.href = hs.getSrc(this.a);
	}
};

HsExpander.prototype.reflow = function () {
	hs.setStyles(this.scrollerDiv, { height: 'auto', width: 'auto' });
	this.x.span = this.innerContent.offsetWidth;
	this.y.span = this.innerContent.offsetHeight;
	var size = { width: this.x.span +'px', height: this.y.span +'px' };
	hs.setStyles(this.content, size);
	this.positionOutline(this.x.min, this.y.min, this.x.span, this.y.span);
};

HsExpander.prototype.htmlOnClose = function() {
	if (this.objectLoadTime == 'after' && !this.preserveContent) this.destroyObject();		
	if (this.scrollerDiv && this.scrollerDiv != this.scrollingContent) 
		this.scrollerDiv.style.overflow = 'hidden';
	if (this.swfObject) hs.$(this.swfObject.getAttribute('id')).StopPlay();
};

HsExpander.prototype.destroyObject = function () {
	this.objContainer.innerHTML = '';
};

HsExpander.prototype.sleep = function() {
	if (this.objOutline) this.objOutline.table.className = 'highslide-display-none';
	this.wrapper.className += ' highslide-display-none';
	hs.push(hs.sleeping, this);
};

HsExpander.prototype.awake = function() {
	hs.expanders[this.key] = this;
	
	this.wrapper.className = this.wrapper.className.replace(/highslide-display-none/, '');
	var z = hs.zIndexCounter++;
	this.wrapper.style.zIndex = z;
	if (o = this.objOutline) {
		if (!this.outlineWhileAnimating) o.table.style.visibility = 'hidden';
		o.table.className = null;
		o.table.style.zIndex = z;
	}
	this.show();
};

// HsAjax object prototype
HsAjax = function (a, content) {
	this.a = a;
	this.content = content;
};

HsAjax.prototype.run = function () {
	try { this.xmlHttp = new XMLHttpRequest(); }
	catch (e) {
		try { this.xmlHttp = new ActiveXObject("Msxml2.XMLHTTP"); }
		catch (e) {
			try { this.xmlHttp = new ActiveXObject("Microsoft.XMLHTTP"); }
			catch (e) { this.onError(); }
		}
	}
	this.src = hs.getSrc(this.a);
	if (this.src.match('#')) {
		var arr = this.src.split('#');
		this.src = arr[0];
		this.id = arr[1];
	}
	var pThis = this;
	this.xmlHttp.onreadystatechange = function() {
		if(pThis.xmlHttp.readyState == 4) {	
			if (pThis.id) pThis.getElementContent();
			else pThis.loadHTML();
		}
	};
	
	this.xmlHttp.open("GET", this.src, true);
	this.xmlHttp.send(null);
};

HsAjax.prototype.getElementContent = function() {
	hs.genContainer();
	var attribs = window.opera ? { src: this.src } : null; // Opera needs local src
	this.iframe = hs.createElement('iframe', attribs, 
		{ position: 'absolute', left: '-9999px' }, hs.container);
		
	try {
		this.loadHTML();
	} catch (e) { // Opera security
		var pThis = this;
		setTimeout(function() {	pThis.loadHTML(); }, 1);
	}
};

HsAjax.prototype.loadHTML = function() {
	var s = this.xmlHttp.responseText;
	if (!hs.ie || hs.ieVersion() >= 5.5) {
		s = s.replace(/\s/g, ' ');
		if (this.iframe) {
			s = s.replace(new RegExp('<link[^>]*>', 'gi'), '');
			s = s.replace(new RegExp('<script[^>]*>.*?</script>', 'gi'), '');
			var doc = this.iframe.contentDocument || this.iframe.contentWindow.document;
			doc.open();
			doc.write(s);
			doc.close();
			try { s = doc.getElementById(this.id).innerHTML; } catch (e) {
				try { s = this.iframe.document.getElementById(this.id).innerHTML; } catch (e) {} // opera
			}
			hs.container.removeChild(this.iframe);
		} else {
			s = s.replace(new RegExp('^.*?<body[^>]*>(.*?)</body>.*?$', 'i'), '$1');
		}
		
	}
	hs.identifyContainer(this.content, 'highslide-body').innerHTML = s;
	this.onLoad();
};

hs.addEventListener(window, 'load', hs.preloadAjax);
       	// remove the registerOverlay call to disable the controlbar
	    hs.registerOverlay(
    	    {
    		    thumbnailId: null,
    		    overlayId: 'controlbar',
    		    position: 'top right',
    		    hideOnMouseOut: true
		    }
	    );
	
        hs.graphicsDir = '/js/highslide/graphics/';
        
        // Identify a caption for all images. This can also be set inline for each image.
        hs.captionId = 'the-caption';
        
        hs.outlineType = 'rounded-white';
        
        hs.outlineWhileAnimating = true;
        
function change2red(obj)
{
    for(var i = 0; i <  obj.childNodes.length; ++i)
    {
        o = obj.childNodes[i];
        if (o.tagName == "TABLE" && o.className=="blogitem")
        {
            o.className = "blogitem1";
        }
        if (o.tagName == "TABLE" && o.className=="blogmiddle")
        {
            o.className = "blogmiddle1";
        }
        if (o.tagName == "TABLE" && o.className=="blogtoolbar")
        {
            o.className = "blogtoolbar1";
        }
        if (o.tagName == "TABLE" && o.className=="comment")
        {
            o.className = "comment1";
        }
        if (o.tagName == "TABLE" && o.className=="everydayitem")
        {
            o.className = "everydayitem1";
        }
    }
}

function change2gray(obj)
{
    for(var i = 0; i <  obj.childNodes.length; ++i)
    {
        o = obj.childNodes[i];
        if (o.tagName == "TABLE" && o.className=="blogitem1")
        {
            o.className = "blogitem";
        }
        if (o.tagName == "TABLE" && o.className=="blogmiddle1")
        {
            o.className = "blogmiddle";
        }
        if (o.tagName == "TABLE" && o.className=="blogtoolbar1")
        {
            o.className = "blogtoolbar";
        }
        if (o.tagName == "TABLE" && o.className=="comment1")
        {
            o.className = "comment";
        }
        if (o.tagName == "TABLE" && o.className=="everydayitem1")
        {
            o.className = "everydayitem";
        }
    }
}
