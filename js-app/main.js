const url = "https://localhost:5001/api/Coffee";
const dropHere = document.getElementById("coffeesHere")
const button = document.querySelector("#run-button");
button.addEventListener("click", () => {
    console.log("before calling")
    GetCoffees()
        .then(array => {
            array.map(coffee => {
                dropHere.innerHTML = CoffeeDisplay(coffee)
            }

            )

        })
})



const CoffeeDisplay = (coffeeObject) => {
    return `
    <div class="coffee">
        <h2>${coffeeObject.Title}</h3>
    </div>`
}

function GetCoffees() {
    return fetch(url).then(resp => resp.json());

}
