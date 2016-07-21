onmessage = function (etv) {
    var obj = etv.data.Content;
    obj = obj.replace(/\n/g, "<br>");
    obj = obj.replace(/INFO/g, "<font color='green'>INFO</font>");
    obj = obj.replace(/ERROR/g, "<font color='red'>ERROR</font>");
    postMessage(obj);
}