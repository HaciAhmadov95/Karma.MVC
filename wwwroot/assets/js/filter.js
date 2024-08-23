var brandFilters = document.getElementsByClassName("brand-filter");
var catergoryFilters = document.getElementsByClassName("category-filter");
var colorFilters = document.getElementsByClassName("color-filter");

brandFilters.forEach(element => {
    element.addEventListener("click", (fetch('/api/products' + queryString)
        .then(response => response.json())
        .then(data => {
            // Update UI with the filtered data
            console.log(data);
        })
        .catch(error => {
            console.error('Error fetching filtered data:', error);
        });))
})