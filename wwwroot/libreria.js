document.addEventListener('DOMContentLoaded', () => { GetData(); });


let GetData = () => {
    fetch('/Book')
        .then(response => response.json())
        .then(json => aggiungiatabella(json));
}

let aggiungiatabella = (books) => {
    return books.map(book => {
        let tr = document.createElement('tr');
        tr.innerHTML = template(book);
        document.getElementById('tabella').appendChild(tr);
    });
}
let template = (book) => {
    return `<td>${book.title}</td><td>${book.idGen}</td><td>${book.idShelf}</td><td>${book.isOut}</td>`
}




