function postData(url) {
    return fetch(url, {
        method: 'POST',
    }).then(response => response.json())
        .catch((error) => {
            console.error('Error:', error);
        });
}


function addProduct(productId) {
    postData(window.location.origin + '/Cart/AddToCart?productId=' + productId)
        .then(data => {
            console.log(data);
            updateCartValues()
    })
}

function updateCartValues() {
    // not good to have 2 sources of truth, so at the expense of doing one more request, we use the backend to calculate this
    //document.getElementById('cartItems').innerText = data._cart.CartEntries.reduce((a, b) => a + b.Quantity, 0) + '  item(s)'
    //document.getElementById('cartTotal').innerText = data._cart.CartEntries.reduce((a, b) => a + b.Quantity * (b.Product.Price - b.Product.Price * b.Product.Discount / 100), 0) + ' kr.'

    postData(window.location.origin + '/Cart/GetCartTotalForCart').then(data => {
        console.log(data);

        document.getElementById('cartItems').innerText = data.totalProducts + ' item(s)';
        document.getElementById('cartTotal').innerText = data.totalValue + ' kr.';

        for (let i of Object.keys(data.pidToTotal)) {
            let sub = document.getElementById('subtotal' + i);
            if (sub) {
                sub.innerText = data.pidToTotal[i] + ' kr.'
            }
        }

        let totalValueElement = document.getElementById('cartTotalValue');

        // only present on the cart view
        if(totalValueElement) totalValueElement.innerText = data.totalValue + ' kr.'

    })
}

function minusProduct(productId) {
    postData('/Cart/MinusItem?productId=' + productId)
        .then(res => {
            if (res && res.id && !res.missing) {
                document.getElementById('q' + res.id).innerText -= 1;
                updateCartValues();
            } else {
                let row = document.getElementById(res.id);
                row.parentElement.removeChild(row);
                updateCartValues();
            }
        })
}

function plusProduct(productId) {
    postData('/Cart/AddToCart?productId=' + productId)
        .then(res => {
            if (res) {
                let prev = document.getElementById('q' + productId);
                prev.innerText = prev.innerText * 1 + 1;
                updateCartValues();
            }
        })
}