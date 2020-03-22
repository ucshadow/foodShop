document.getElementById('searchInput').addEventListener('input', e => {
    if (e.target.value) {
        return getData(e.target.value);
    }
    let container = document.getElementById('searchResultContainer');
    if (container) {
        document.body.removeChild(container);
    }
})

function getData(q) {
    fetch(window.location.origin + '/Product/GetSearchData', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ Name: q }),
    })
        .then((response) => response.json())
        .then((data) => {
            console.log(data)
            createSearchResultPopup(data);
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}


function createSearchResultPopup(data) {
    let parent = document.getElementById('searchResultContainer');
    if (parent) {
        parent.innerHTML = null;
    } else {
        parent = document.createElement('div');
        parent.classList.add('container');
        parent.classList.add('row');
        parent.style = 'border: 1px solid gray; position: absolute; top: 10%; left: 10%; background: steelblue; padding-top:30px; z-index:10000';
        parent.addEventListener('mousedown', () => {
            console.log('down');
        })
        parent.id = 'searchResultContainer';
        document.body.appendChild(parent);
    }   

    for (let d of data) {
        if (document.getElementById('r' + d.ProductID)) continue;

        let h = document.createElement('a')
        h.href = window.location.origin + '/Product/Details?productName=' + d.Name;
        h.style = 'color:whitesmoke; text-decoration:none';

        let image = document.createElement('img');
        image.src = d.Picture;
        image.style = 'position: absolute;top:0px;right:0px;height:100%;'

        let result = document.createElement('div');
        result.id = 'r' + d.ProductID;
        result.classList.add('col-md-4')
        result.classList.add('btn');
        result.classList.add('btn-success');
        result.style = 'border: 2px solid steelblue'
        h.innerText = d.Name;
        result.appendChild(h);
        result.appendChild(image);
        parent.appendChild(result);
    }
}