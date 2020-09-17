
// 粒子线条背景
$(document).ready(function () {
    $('.layui-container').particleground({
        dotColor: '#7ec7fd',
        lineColor: '#7ec7fd'
    });
});

$("#IsRemember").attr("lay-skin", "primary");
$("#UserId").attr("lay-verify", "uid");
$("#PassWord").attr("lay-verify", "pass");

//表单验证
layui.use('form', function () {
    var form = layui.form;
    
    // 登录过期的时候，跳出ifram框架
    if (top.location != self.location) top.location = self.location;

    //layer.msg("1", { icon: 2, time: 2000 });
    //自定义验证规则
    form.verify({
        uid: [
            /^[\S]{8,18}$/
            , '账号必须在 8-18 位之间 !'
        ],
        pass: [
            /^[\S]{6,20}$/
            , '密码必须在 6-20 位之间，且不能出现空格 !'
        ]
        , content: function (value) {
            layedit.sync(editIndex);
        }
    });
});


//Ajax登录验证
function onSuccess(data, status, xhr) {
    if (data.Status == 1) {
        layer.msg(data.Msg, {
            icon: 1,
            time: 2000 //2秒关闭（如果不配置，默认是3秒）
        }, function () {
            window.location = '/Home/Index';
        });
    }
    else if (data.Status == 4) {
        layer.msg(data.Msg, { icon: 2, time: 2000 });   //密码错误
        $("#PassWord").val("");
    }
    else if (data.Status == 5) {
        layer.msg(data.Msg, { icon: 4, time: 2000 });   //账号停用
    }
    else if (data.Status == 0) {
        layer.msg(data.Msg, { icon: 5, time: 2000 });   //表单错误
        $("#PassWord").val("");
    }
    else {
        layer.msg("未知问题,请联系管理员!", { icon: 3, time: 2000 });   //其他问题
        $("#PassWord").val("");
    }
}

//显示密码
$('.bind-password').on('click', function () {
    if ($(this).hasClass('fa-eye')) {
        $(this).removeClass('fa-eye');
        $("#PassWord").attr('type', 'password');
        $("#icon-password").attr("src", "/Content/images/hide.png");
    } else {
        $(this).addClass('fa-eye');
        $("#PassWord").attr('type', 'text');
        $("#icon-password").attr("src", "/Content/images/show.png");
    }
});