var order = {
    init: function () {
        order.regEvents();
    },

    regEvents: function () {
        $('.btn-detail').off('click').on('click', function () {
            window.location.href = "/Order/Detail?id=" + $(this).data('id');
        });

        $('#btn-Cancel').off('click').on('click', function () {
            $.ajax({
                url: '/Order/OrderCancel',
                data: { id: $(this).data('id') },
                contentData: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/Order";
                    }
                }
            });
        });
    }
}

order.init();