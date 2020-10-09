layui.use(['layer', 'miniTab', 'echarts'], function () {
    var $ = layui.jquery,
        layer = layui.layer,
        miniTab = layui.miniTab,
        echarts = layui.echarts;

    miniTab.listen();

    /**
     * 查看公告信息
     **/
    $('body').on('click', '.layuimini-notice', function () {
        var title = $(this).children('.layuimini-notice-title').text(),
            noticeTime = $(this).children('.layuimini-notice-extra').text(),
            content = $(this).children('.layuimini-notice-content').html();
        var html = '<div style="padding:15px 20px; text-align:justify; line-height: 22px;border-bottom:1px solid #e2e2e2;background-color: #2f4056;color: #ffffff">\n' +
            '<div style="text-align: center;margin-bottom: 20px;font-weight: bold;border-bottom:1px solid #718fb5;padding-bottom: 5px"><h4 class="text-danger">' + title + '</h4></div>\n' +
            '<div style="font-size: 12px">' + content + '</div>\n' +
            '</div>\n';
        parent.layer.open({
            type: 1,
            title: '系统公告' + '<span style="float: right;right: 1px;font-size: 12px;color: #b1b3b9;margin-top: 1px">' + noticeTime + '</span>',
            area: '300px;',
            shade: 0.8,
            id: 'layuimini-notice',
            btn: ['关闭'],
            btnAlign: 'c',
            moveType: 1,
            content: html
            //,success: function (layero) {
            //    var btn = layero.find('.layui-layer-btn');
            //    btn.find('.layui-layer-btn0').attr({
            //        href: '/Message/MessageList',
            //        target: '_self'
            //    });
            //}
        });
    });

    /**
    * 中国地图
    */
    var echartsMap = echarts.init(document.getElementById('map'), 'walden');


    var optionMap = {
        legend: {},
        tooltip: {
            trigger: 'axis',
            showContent: false
        },
        dataset: {
            source: [
                ['总数', '2018', '2019', '2020'],
                ['借阅图书总数',78,83, 18],
                ['已归还总数',78, 82, 9],
                ['延期总数', 17,0, 2],
                ['未归还总数',0, 1, 7]
            ]
        },
        xAxis: { type: 'category' },
        yAxis: { gridIndex: 0 },
        grid: { top: '55%' },
        series: [
            { type: 'line', smooth: true, seriesLayoutBy: 'row' },
            { type: 'line', smooth: true, seriesLayoutBy: 'row' },
            { type: 'line', smooth: true, seriesLayoutBy: 'row' },
            {
                type: 'pie',
                id: 'pie',
                radius: '30%',
                center: ['50%', '30%'],
                label: {
                    formatter: '{b}: {@@2019} ({d}%)'
                },
                encode: {
                    itemName: '数量',
                    value: '2019',
                    tooltip: '2019'
                }
            }
        ]
    };

    echartsMap.setOption(optionMap);


    // echarts 窗口缩放自适应
    window.onresize = function () {
        echartsRecords.resize();
    }

});