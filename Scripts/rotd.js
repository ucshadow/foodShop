function init() {
    let data = document.getElementById('rHolder').innerText;
    console.log(JSON.parse(data));
    fill(JSON.parse(data));
}

function fill(d) {
    if (!d || !d.hits || !d.hits[0]) {
        document.getElementById('cTitle').innerText = 'Something went wrong, please reload';
        return;
    }
    document.getElementById('cTitle').innerText = d.hits[0].recipe.label;
    document.getElementById('cDescription').innerText = d.hits[0].recipe.label + '\n';
    document.getElementById('cDescription').innerText += d.hits[0].recipe.dietLabels.join('\n');
    document.getElementById('cImg').src = d.hits[0].recipe.image;
    let ing = d.hits[0].recipe.ingredientLines;
    let bo = document.getElementById('cBody');
    for (let i of ing) {
        let a = document.createElement('a');
        a.href = window.location.origin + '/Search%20Results?q=' + i;
        let b = document.createElement('div');
        b.style.width = '100%';
        a.style.width = '100%';
        b.classList.add('btn');
        b.classList.add('btn-primary');
        b.innerText = i;
        a.appendChild(b);
        bo.appendChild(a);
    }
}