let prev = 0;

function toggelProductForm(e, productId) {

    if (prev !== 0) {
        document.getElementById("productForm" + prev).setAttribute("hidden", "yes :D");
    }

    prev = productId;

    let form = document.getElementById("productForm" + productId);
    form.style.left = e.clientX + 50 + 'px';
    form.style.top = e.clientY + 200 + 'px';
    form.removeAttribute("hidden");
    document.getElementById("productIdInput" + productId).value = productId;
}