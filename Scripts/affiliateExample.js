let product = null;

function getProductById() {
    let id = document.getElementById('itemId').value
    if (id && id * 1 > -1) {
        fetch(window.location.origin + '/Affiliate/GetProductById?productId=' + id)
            .then(res => res.json()).then(res => {
                product = res.Product;
                document.getElementById('productName').innerText = "Name: " + res.Product.Name;
                document.getElementById('productPrice').innerText = "Price: " + res.Product.Price;
                document.getElementById('productImage').src = res.Product.Picture;
            });
    }
}

function sellItem() {

    let d = {
        Product: product,
        ShippingDetails: {
            Name: document.getElementById('name').value,
            Line1: document.getElementById('line1').value,
            Country: document.getElementById('country').value,
        },
        Quantity: document.getElementById('count').value * 1,
        AffiliateId: document.getElementById('affiliateId').value

    }
    console.log(d);
    $.ajax({
        type: "POST", url: window.location.origin + "/Affiliate/SellItem",
        success: () => { getProductById() }, // recall the get product function because the product quantity is stored in the product variable and needs to be refreshed.
        data: d,
        accept: 'application/json'
    });

}