Shade = new function() {
    var handle = {};
    var shade;
    handle.show = function() {
        if (!shade) {
            shade = document.createElement('div');
            shade.className = 'tb-shade';
            document.body.appendChild(shade);
        }
        with ((document.compatMode == 'CSS1Compat') ? document.documentElement : document.body) {
            var ch = clientHeight, sh = scrollHeight;
            shade.style.height = (sh > ch ? sh : ch) + 'px';
            shade.style.width = offsetWidth + 'px';
            shade.style.display = 'block';
        }
    };
    handle.hide = function() {
        shade.style.display = 'none';

    };

    return handle;
}

function showPanel(src) {
    Shade.show();
    document.getElementById('ShadePanel').style.display = 'block';
    document.getElementById('frame').src = src;
    document.getElementById('ShadePanel').scrollIntoView();
}

function hidePanel() {
    Shade.hide();
    document.getElementById('ShadePanel').style.display = 'none';

} 