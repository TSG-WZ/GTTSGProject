        function formatDate(val, formatType) {
            if (val == undefined) {
                return '';
            }
            var reg = /^\/Date\(\d+\)\/$/;
            if (!reg.test(val)) return '';//格式不正确 ，返回空
            var strDate = val.substr(1, val.length - 2);
            var obj = eval('(' + "{ date :new " + strDate + "}" + ')')
            var date = obj.date;
            var year = date.getFullYear();
            var month = date.getMonth() + 1 < 10 ? '0' + (date.getMonth() + 1) : date.getMonth() + 1;
            var day = date.getDate() < 10 ? '0' + date.getDate() : date.getDate();
            var datetime = year + '-' + month + '-' + day;

            if (formatType == 'yyyy-MM-dd') {
                return datetime;
            } else if (formatType == 'yyyy-MM-dd HH:mm:ss') {
                var hour = date.getHours() < 10 ? '0' + date.getHours() : date.getHours();
                var minute = date.getMinutes() < 10 ? '0' + date.getMinutes() : date.getMinutes();
                var seconds = date.getSeconds() < 10 ? '0' + date.getSeconds() : date.getSeconds();
                return datetime + ' ' + hour + ':' + minute + ':' + seconds;
            }
            return datetime;
        }