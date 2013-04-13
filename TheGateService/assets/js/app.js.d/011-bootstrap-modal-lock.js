// Slightly modified from:
// http://stackoverflow.com/questions/13421750/twitter-bootstrap-jquery-how-to-temporarily-prevent-the-modal-from-being-closed
(function($) {
    var _superModal = $.fn.modal;

    $.extend(_superModal.defaults, {
        locked: false
    });

    var Modal = function(element, options) {
        _superModal.Constructor.apply(this, arguments);
    };

    Modal.prototype = $.extend({}, _superModal.Constructor.prototype, {
        constructor: Modal,
        _super: function() {
            var args = $.makeArray(arguments);
            _superModal.Constructor.prototype[args.shift()].apply(this, args);
        },
        lock: function() {
            this.options.locked = true;
        },
        unlock: function() {
            this.options.locked = false;
        },
        hide: function() {
            if (this.options.locked) {
                this.$element.trigger($.Event('hidePrevented'));
                return;
            }
            this._super('hide');
        },
        hidePrevented: function() {
        },
    });

    $.fn.modal = $.extend(function(option) {
        var args = $.makeArray(arguments),
            option = args.shift();

        return this.each(function() {
            var $this = $(this);
            var data = $this.data('modal'),
                options = $.extend({}, _superModal.defaults, $this.data(), typeof option == 'object' && option);

            if (!data) {
                $this.data('modal', (data = new Modal(this, options)));
            }
            if (typeof option == 'string') {
                data[option].apply(data, args);
            }
        });
    }, $.fn.modal);
})(jQuery);
