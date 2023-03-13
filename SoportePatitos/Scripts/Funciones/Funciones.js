//Selecciona todos los elementos con el tag i y los almacena en una NOdelist llamada estrellas

const stars = document.querySelectorAll(".stars i");


//Loop a travez del NOdeList
stars.forEach((star, index1) => {
    //Evento tipo listener que se ejecute cuando se da click
    star.addEventListener("click", () => {
        //Se vuelve al loop
        stars.forEach((star, index2) => {
            index1 >= index2 ? star.classList.add("active") : star.classList.remove("active");
        })
    });
});
