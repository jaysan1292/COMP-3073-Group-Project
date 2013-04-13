(function($) {
    'use strict';

    // Globals
    var modified = {
        id: false,
        name: false,
        description: false,
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
            description: $(prefix + '.product-description').attr('title'),
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
            name: getFieldValue($('#modal-product-name')[0]),
            description: getFieldValue($('#modal-product-description')[0]),
            price: parseFloat(getFieldValue($('#modal-product-price')[0])),
            quantity: parseInt(getFieldValue($('#modal-product-quantity')[0]), 10),
            featured: getFieldValue($('#modal-product-featured')[0]),
            showcase: getFieldValue($('#modal-product-showcase')[0])
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

    function getFieldValue(element) {
        var value;
        if (/text(area)?|number/.test(element.type)) { // Regex check for 'text', 'textarea', or 'number'
            value = element.value;
        } else if (element.type == 'checkbox') {
            value = $(element).is(':checked');
        }
        return value;
    }

    $(document).ready(function() {
        $('.edit-button').click(function() {
            // Get the product ID, which is stored in the parent <tr> attribute in the format "product-{id}"
            var id = $(this).closest('tr').attr('id').split('-')[1];
            var product = getProductFromRow(id);

            // Insert the product values
            $('#modal-header-product-name').text(product.name);
            $('#modal-product-name').val(product.name).data('name', product.name);
            $('#modal-product-description').text(product.description).data('description', product.description);
            $('#modal-product-price').val(product.price).data('price', product.price);
            $('#modal-product-quantity').val(product.quantity).data('quantity', product.quantity);
            $('#modal-product-featured').prop('checked', product.featured).data('featured', product.featured);
            $('#modal-product-showcase').prop('checked', product.showcase).data('showcase', product.showcase);

            $('#product-form input, #product-form textarea').change(function(e) {
                var prop = $(this).attr('id').split('-')[2];
                var value = getFieldValue(e.target);

                if (value !== undefined) $(this).data(prop, value);

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

            $('#save-button').click(function() {
                var p = getProductFromForm();
                var message = 'Are you sure you want to save your changes to "' + p.name + '"?';
                if (!hasProductBeenEdited() || !window.confirm(message)) {
                    $('#edit-modal').modal('hide');
                    return;
                }

                // send to service, then close modal dialog
                window.console.log(JSON.stringify(p));

                $.ajax({
                    url: window.site_root + '/products/' + p.id,
                    type: 'put',
                    data: p,
                    success: function(data) {
                        window.console.log('success');

                        // Update the product in the list with the new values

                        var prefix = '#product-' + p.id + ' ';
                        $(prefix + '.product-name')
                            .fadeOut()
                            .text(p.name)
                            .fadeIn();
                        $(prefix + '.product-description') // TODO: Truncate this in the same way as before
                            .fadeOut()
                            .attr('title', p.description).text(p.description)
                            .fadeIn();
                        $(prefix + '.product-price')
                            .fadeOut()
                            .text('$' + p.price)
                            .fadeIn();
                        $(prefix + '.product-quantity')
                            .fadeOut()
                            .text(p.quantity)
                            .fadeIn();
                        $(prefix + '.product-featured input')
                            .fadeOut()
                            .prop('checked', p.featured)
                            .fadeIn();
                        $(prefix + '.product-showcase input')
                            .fadeOut()
                            .prop('checked', p.showcase)
                            .fadeIn();

                        // Hide the dialog box
                        $('#edit-modal').modal('unlock').modal('hide');
                    },
                    error: function(req, status, error) {
                        window.console.log(JSON.stringify(req));
                        window.console.log(JSON.stringify(status));
                        window.console.log(JSON.stringify(error));
                    }
                });
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
                    $('#save-button').unbind('click');
                    $('#unsaved-indicator').hide();
                    $('#product-form')[0].reset();
                });
        });
    });
})(jQuery);
