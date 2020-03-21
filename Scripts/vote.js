function vote(rating, purchaseId) {
    console.log("voting on " + purchaseId + " rating: " + rating);
    fetch('https://localhost:44395/PurchaseHistory/Vote', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ Rating: rating, PurchaseId: purchaseId }),
    })
        .then((response) => response.json())
        .then((data) => {
            console.log(data);
            console.log(data.Result);
            console.log(data.OK);
            if (data.Result && data.OK) window.location.reload(); // :D
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}