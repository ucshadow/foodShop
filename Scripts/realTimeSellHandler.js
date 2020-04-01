(() => {
    function interval(f, t) {
        f();
        return setInterval(f, t);
    }

    interval(() => {
        fetch(window.location.origin + '/RealTimeSellData/GetSellData', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            }
        }).then((response) => response.json())
            .then((data) => {
                updateValues(data);
            })
            .catch((error) => {
                console.error('Error:', error);
            });
    }, 5000)

    function updateValues(data) { 
        for (let i = 0; i < 4; i++) {            

            if (data.length <= i) return;

            let p = document.getElementById('i' + i)
            p.src = data[i].Product.Picture;
            p.title = data[i].Remaining + ' remainig';

            p.parentElement.href = '/Product/Details?productName=' + data[i].Product.Name;

            let t = document.getElementById('i_t' + i)
            t.innerText = data[i].Remaining + ' remainig';

        }
    }
})()