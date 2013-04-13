(function($){
    'use strict';

    // Globals
    var modified = {
        id: false,
        name: false,
        desc: false,
        price: false,
        quantity: false,
        featured: false,
        showcase: false
    };

    function getProductFromRow(id) {
        var prefix = '#product-' + id + ' ';
        var product = {
            id: id,
            name: $(prefix + '.product-name').text(),
            desc: $(prefix + '.product-desc').attr('title'),
            price: parseFloat($(prefix + '.product-price').text().substr(1)), // remove the leading $
            quantity: parseInt($(prefix + '.product-quantity').text(), 10),
            featured: $(prefix + '.product-featured input').is(':checked'),
            showcase: $(prefix + '.product-showcase input').is(':checked')
        };

        return product;
    }

    function getProductFromForm() {
        var product = {
            id: getOriginalProduct().id,
            name: $('#modal-product-name').text(),
            desc: $('#modal-product-desc').text(),
            price: parseFloat($('#modal-product-price').val()),
            quantity: parseInt($('#modal-product-quantity').val(), 10),
            featured: $('#modal-product-featured').is(':checked'),
            showcase: $('#modal-product-showcase').is(':checked')
        };
        return product;
    }

    function hasProductBeenEdited() {
        for (var property in modified) {
            if ($(modified).prop(property)) return true;
        }
        return false;
    }

    function getOriginalProduct() {
        return $('#edit-modal').data('product-original-value');
    }

    $(document).ready(function() {
        $('.edit-button').click(function() {
            // Get the product ID, which is stored in the parent <tr> attribute in the format "product-{id}"
            var id = $(this).closest('tr').attr('id').split('-')[1];
            var product = getProductFromRow(id);

            // Insert the product values
            $('#modal-header-product-name').text(product.name);
            $('#modal-product-name').val(product.name).data('name', product.name);
            $('#modal-product-desc').text(product.desc).data('desc', product.desc);
            $('#modal-product-price').val(product.price).data('price', product.price);
            $('#modal-product-quantity').val(product.quantity).data('quantity', product.quantity);
            $('#modal-product-featured').prop('checked', product.featured).data('featured', product.featured);
            $('#modal-product-showcase').prop('checked', product.showcase).data('showcase', product.showcase);

            $('#product-form input, #product-form textarea').change(function(e) {
                var prop = $(this).attr('id').split('-')[2];
                var value;
                if (/text(area)?|number/.test(e.target.type)) { // Regex check for 'text', 'textarea', or 'number'
                    value = e.target.value;
                } else if (e.target.type == 'checkbox') {
                    value = $(e.target).is(':checked');
                }

                if(value !== undefined) $(this).data(prop, value);

                if ($(getOriginalProduct()).prop(prop) != $(this).data(prop)) {
                    $(modified).prop(prop, true);

                } else {
                    $(modified).prop(prop, false);
                }

                if (hasProductBeenEdited()) {
                    $('#edit-modal').modal('lock');
                    $('#unsaved-indicator').show();
                } else {
                    $('#edit-modal').modal('unlock');
                    $('#unsaved-indicator').hide();
                }
            });

            $('#edit-modal')
                // Save the original value, which will be used later to check if the product has not been saved
                .data('product-original-value', product)
                .modal('show')
                .on('hidePrevented', function() {
                    if (!hasProductBeenEdited()) return;
                    if (window.confirm('You have unsaved changes on "' + product.name + '". Are you sure you want to stop editing?')) {
                        $('#edit-modal')
                            .modal('unlock') // First unlock the modal so it can be hidden
                            .modal('hide'); // then hide it
                    }
                }).on('hide', function() {
                    // Unbind any events we bound, to prevent "stacking" of handlers
                    $('#product-form input, #product-form textarea').unbind('change');
                    $('#edit-modal').unbind('hidePrevented').unbind('hide');
                    $('#product-form')[0].reset();
                });
        });
    });
})(jQuery);
