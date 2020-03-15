(() => {
    // todo: change for azure
    setInterval(() => {
        fetch('https://localhost:44395/RealTimeSellData/GetSellData', {
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
    }, 3000);

    function updateValues(data) { 
        for (let i = 0; i < 4; i++) {            

            if (data.length <= i) return;

            let p = document.getElementById('i' + i)
            p.src = data[i].Product.Picture;

        }
    }
})()