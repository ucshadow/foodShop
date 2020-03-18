function getNutrition(e) {
    return fetch(window.location.origin + '/NutritionAPI/GetNutritionValues', {
        method: 'POST',
        body: JSON.stringify({ Name: e }),
        headers: {
            'Content-Type': 'application/json'
        },
    })
        .then((response) => response.json())
        .then((data) => {
            showData(data);
        })
        .catch((error) => {
            showData({ warning: 'Something went wrong :('})
        });
}

function showData(data) {
    let parent = document.getElementById('showResult')
    parent.innerHTML = null;
    for (let i of Object.keys(data)) {
        let d = document.createElement('div');
        d.classList.add('list-group-item');
        d.classList.add('list-group-item-action');
        d.innerText = i;
        d.innerText += ': ';
        d.innerText += data[i][0] + ' (' + data[i][1] + ')';
        parent.appendChild(d);
    }
}