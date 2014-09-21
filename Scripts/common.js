function BindData(fun, params, url) {
    var postData = '';

    for (var item in params) {
        if (params[item])
            postData += item + '=' + params[item] + '&';
    }
    postData = postData.substring(0, postData.length - 1);
    $.ajax({
        type: 'get',
        url: url, // 跨域URL
        data: postData,
        dataType: 'json',
        cache: false,
        success: function (json) { //
            fun(json);
        },
        complete: function (jqXHR, textStatus) {

        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert("请检查网络情况！");
        }
    });
}