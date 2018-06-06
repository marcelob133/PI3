Number.prototype.formatMoney = function (c, d, t) {
    var n = this,
        c = isNaN(c = Math.abs(c)) ? 2 : c,
        d = d == undefined ? "." : d,
        t = t == undefined ? "," : t,
        s = n < 0 ? "-" : "",
        i = parseInt(n = Math.abs(+n || 0).toFixed(c)) + "",
        j = (j = i.length) > 3 ? j % 3 : 0;
    return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
};

var overlay = {
    vars: {
        $overlay: $('.overlay')
    },
    init: function () {
        var _this = this;
        $(_this.vars.$overlay).on('click', function () {
            cart.close();
        });
    },
    open: function () {
        var _this = this;
        $(_this.vars.$overlay).stop(true, true).show('fold', 1000);
    },
    close: function () {
        var _this = this;
        $(_this.vars.$overlay).stop(true, true).hide('fold', 1000);
    }
}

var cart = {
    vars: {
        key: 'cart',
        products: [],
        PRODUCTS_TOTAL: 0,
        contentCart: '#cart',
        btnCart: '#btn-cart',
        btnAddCart: '.add-cart',
        btnRemoveCart: '.cancel-cart',
        btnCheckout: '#btn-checkout',
        cartList: $('.cart-body tbody'),
        count: $('.cart-count'),
        total: $('.cart-total-price-products .price')
    },
    init: function () {
        var _this = this;

        if (localStorage.getItem(_this.vars.key) != null && localStorage.getItem(_this.vars.key).length > 0) {            
            $.post("/Produto/Carrinho", { cart: localStorage.getItem(_this.vars.key) }, function (data) {  
                _this.vars.products = JSON.parse(data);
                _this.updateCountAndTotal();
                _this.renderCart();
                if($('#Valor').length > 0){
                    $('#Valor').val(cart.vars.PRODUCTS_TOTAL.replace(',', '.')); 
                }
            });
        }

        if ($('#cart-header-page').length == 0) {
            $(_this.vars.btnCart).addClass('btn-disabled');
        }

        $(_this.vars.btnCart).on('click', function () {
            var contentCart = $(_this.vars.contentCart);
            if($('#cart-header-page').length > 0){
                if (contentCart.hasClass('open')) {
                    _this.close();
                } else {
                    _this.open();                    
                }
                $('html, body').animate({
                    scrollTop: 0
                });
            }
        });

        $(_this.vars.btnCheckout).on('click', function () {
            if (_this.vars.products.length == 0)
            {
                alert('Nenhum produto no carrinho');
                return false;
            }
        });

        $(_this.vars.btnCart).on('dragover', function (event) {
            event.preventDefault();
        });

        $(_this.vars.btnCart).on('drop', function (event) {

            var $this = $('#' + event.originalEvent.dataTransfer.getData('text')), indexProduct, inpQtd = parseFloat($this.parent().find('.product-qtd').val()) || 1;

            indexProduct = _this.findProduct($this.data('product-id'));

            if (indexProduct == -1) {
                _this.add({
                    productId: $this.data('product-id'),
                    productName: $this.data('product-name'),
                    productImage: $this.parent().find('.product-image').attr('src') || $this.parent().parent().find('.product-image').attr('src') || $this.attr('src'),
                    productPrice: parseFloat($this.data('product-price').replace(',', '.')),
                    productQtd: parseFloat($this.parent().find('.product-qtd').val()) || 1,
                    productTotal: parseFloat($this.data('product-price').replace(',', '.')) * inpQtd
                });
            } else {
                if (parseFloat($this.parent().find('.product-qtd').val())) {
                    _this.vars.products[indexProduct].productQtd = inpQtd + _this.vars.products[indexProduct].productQtd;
                }
                else {
                    _this.vars.products[indexProduct].productQtd++;
                }

                _this.update({
                    productId: $this.data('product-id'),
                    productQtd: _this.vars.products[indexProduct].productQtd,
                });
            }

            $this.blur();
            event.preventDefault();
        });

        $('.product a').on('dragstart', function (event) {
            event.originalEvent.dataTransfer.setData('text', $(this).attr('id'));
        });
        
        $(_this.vars.btnAddCart).on('click', function () {
            var $this = $(this), indexProduct, inpQtd = parseFloat($this.parent().find('.product-qtd').val()) || 1;

            indexProduct = _this.findProduct($this.data('product-id'));

            if (indexProduct == -1) {
                _this.add({
                    productId: $this.data('product-id'),
                    productName: $this.data('product-name'),
                    productImage: $this.parent().find('.product-image').attr('src') || $this.parent().parent().find('.product-image').attr('src') || $this.attr('src'),
                    productPrice: parseFloat($this.data('product-price').replace(',', '.')),
                    productQtd: parseFloat($this.parent().find('.product-qtd').val()) || 1,
                    productTotal: parseFloat($this.data('product-price').replace(',', '.')) * inpQtd
                });
            } else {
                if (parseFloat($this.parent().find('.product-qtd').val())) {
                    _this.vars.products[indexProduct].productQtd = inpQtd + _this.vars.products[indexProduct].productQtd;
                }
                else {
                    _this.vars.products[indexProduct].productQtd++;
                }

                _this.update({
                    productId: $this.data('product-id'),
                    productQtd: _this.vars.products[indexProduct].productQtd,
                });
            }
        });

        $(document).on('click', _this.vars.btnRemoveCart, function () {
            var $this = $(this);
            _this.remove($this.data('product-id'));
            if (_this.vars.products.length == 0 && $('#cart-body-page').length == 0) {
                _this.close();
            }
        });
    },
    open: function () {
        var _this = this;
        $(_this.vars.btnCart).toggleClass('btn-cart-active');
        overlay.open();
        $(_this.vars.contentCart).addClass('open').stop(true, true).slideDown(300);
    },
    close: function () {
        var _this = this;
        overlay.close();
        $(_this.vars.btnCart).toggleClass('btn-cart-active');
        $(_this.vars.contentCart).removeClass('open').stop(true, true).slideUp(300);
    },
    template: {
        product: function (product) {
            return '<tr id="cart-product-' + product.productId + '"><td class="cart-product-photo-item"><img src="' + product.productImage + '" alt="' + product.productName + '" title="' + product.productName + '"></td><td class="cart-product-name-item">' + product.productName + '</td><td class="cart-product-price-item">R$ ' + product.productPrice.formatMoney(2, ',', '.') + '</td><td class="cart-product-qtd-item">' + product.productQtd + '</td><td class="cart-product-total-item"> R$ <span class="price">' + (product.productQtd * product.productPrice).formatMoney(2, ',', '.') + '</span></td><td class="cart-actions"><button type="button" class="cancel-cart" data-product-id="' + product.productId + '"><i class="icon icon-cancel"></i></button></td></tr>';
        }
    },
    setProductLocalStorage: function () {
        var _this = this, products = [];
        for (var i = 0; i < _this.vars.products.length; i++) {
            products.push({ productId: _this.vars.products[i].productId, productQtd: _this.vars.products[i].productQtd });
        }
        localStorage.setItem(_this.vars.key, JSON.stringify(products));
    },
    renderCart: function () {
        var _this = this, allProductsCart = '';
        for (var i = 0; i < _this.vars.products.length; i++) {
            allProductsCart += _this.template.product(_this.vars.products[i]);
        }
        _this.vars.cartList.append(allProductsCart);
    },
    add: function (newProduct) {
        var _this = this;

        indexProduct = _this.findProduct(newProduct.productId);

        _this.vars.products.push(newProduct);
        _this.vars.cartList.append(_this.template.product(newProduct));

        _this.updateCountAndTotal();
        _this.setProductLocalStorage();
    },
    clear: function () {
        var _this = this;
        localStorage.clear();
        _this.updateCountAndTotal();
    },
    remove: function (productId) {
        var _this = this, indexProduct;
        $('#cart-product-' + productId).fadeOut(function () {
            $(this).remove();
        });
        indexProduct = _this.findProduct(productId);
        _this.vars.products.splice(indexProduct, 1);
        _this.updateCountAndTotal();
        _this.setProductLocalStorage();
    },
    update: function (newProductInfo) {
        var _this = this, indexProduct;
        indexProduct = _this.findProduct(newProductInfo.productId);
        _this.vars.products[indexProduct].productQtd = newProductInfo.productQtd;
        _this.vars.products[indexProduct].productTotal = newProductInfo.productQtd * _this.vars.products[indexProduct].productPrice;
        $('#cart-product-' + _this.vars.products[indexProduct].productId + ' .cart-product-qtd-item').html(_this.vars.products[indexProduct].productQtd);
        $('#cart-product-' + _this.vars.products[indexProduct].productId + ' .cart-product-total-item .price').html(_this.vars.products[indexProduct].productTotal.formatMoney(2, ',', '.'));
        _this.updateCountAndTotal();
        _this.setProductLocalStorage();
    },
    updateCountAndTotal: function () {
        var _this = this, c = total = 0;
        if (_this.vars.products.length > 0) {
            for (var i = 0; i < _this.vars.products.length; i++) {
                c += _this.vars.products[i].productQtd;
                total += _this.vars.products[i].productTotal;
            }
        }        
        _this.vars.count.html('(' + c + ')');
        _this.vars.total.html(total.formatMoney(2, ',', '.'));
        _this.vars.PRODUCTS_TOTAL = total.formatMoney(2, ',', '.');
    },
    findProduct: function (productId) {
        var _this = this;
        for (var i = 0; i < _this.vars.products.length; i++) {
            if (_this.vars.products[i].productId == productId) {
                return i;
            }
        }
        return -1;
    }    
}

$(document).ready(function () {

    cart.init();
    overlay.init();   

});

$(function () {
    $("#Vencimento").datepicker({
        showAnim: "bounce",
        dateFormat: 'dd/mm/yy',
        dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
        dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
        dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
        monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
        monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
        nextText: 'Próximo',
        prevText: 'Anterior'
    });
})

$(document).tooltip();

$(".cart-body").find("tbody").sortable();

$("#pesquisaProd").autocomplete({
    source: data
})

$.ajax({
    method: "GET",
    url: "msg.js",
    dataType: "script"
});