function paginate() {
    $(document).on('click', '.page-link', function (e) {

        let datas = []

        e.preventDefault();

        var page = $(this).attr('href').split('page=')[1];

        var prevArrow = document.querySelector(".prev-arrow");
        var nextArrow = document.querySelector(".next-arrow");

        var prevPage = prevArrow.getAttribute('href').split('page=')[1];
        var nextPage = nextArrow.getAttribute('href').split('page=')[1];

        if ($(this).hasClass("prev-arrow")) {
            dprevArrow.href = `?page=${parseInt(prevPage) - 1}`
            nextArrow.href = `?page=${parseInt(nextPage) - 1}`
        }
        if ($(this).hasClass("next-arrow")) {

            prevArrow.href = `?page=${parseInt(prevPage) + 1}`
            nextArrow.href = `?page=${parseInt(nextPage) + 1}`
        }

        if (!$(this).hasClass("prev-arrow") && !$(this).hasClass("next-arrow")) {
            prevArrow.href = `?page=${parseInt(page) - 1}`
            nextArrow.href = `?page=${parseInt(page) + 1}`
        };

        var pageLinks = document.querySelectorAll(".page-link");

        for (var i = 0; i < pageLinks.length; i++) {
            if (parseInt(pageLinks[i].getAttribute('href').split('page=')[1]) == parseInt(page)) {
                pageLinks[i].classList.add("active")
            } else {
                pageLinks[i].classList.remove("active")
            }
        }


        fetch('/shop/pagedData?pageNumber=' + page)
            .then(response => {
                if (!response.ok) {
                    // Response not OK, throw an error with status text
                    return response.text().then(text => {
                        throw new Error(`HTTP error! status: ${response.status} - ${text}`);
                    });
                }
                return response.json(); // Parse JSON response
            })
            .then(data => {
                datas = data.$values

                let productContainer = document.getElementById("product-container");

                let productHTML = "";
                for (var i = 0; i < datas.length; i++) {
                    productHTML += `<div class="col-lg-4 col-md-6"> <div class="single-product"> <img class="img-fluid" src="./assets/uploads/images/${datas[i].images.$values.filter(n => n.isMain == true)[0].url}" alt=""> <div class="product-details"> <h6>${datas[i].title}</h6> <div class="price">`

                    if (datas[i].discountValue != 0) {
                        // Calculate the discounted price
                        const discountedPrice = Math.round((datas[i].price - (datas[i].price * datas[i].discountValue / 100)) * 100) / 100;

                        // Construct the HTML string with both the discounted and original price
                        productHTML += `<h6>$${discountedPrice}</h6> <h6 class="l-through">$${datas[i].price}</h6>
                    `;
                    } else {
                        // Construct the HTML string with the original price only
                        productHTML += `<h6>$${datas[i].price}</h6>`;
                    }

                    productHTML += `</div> <div class="prd-bottom"> <a href="/Cart/AddToCart/${datas[i].id}" class="social-info"> <span class="ti-bag"></span> <p class="hover-text">add to bag</p> </a> <a href="/Wishlist/AddToWishlist/${datas[i].id}" class="social-info"> <span class="lnr lnr-heart"></span> <p class="hover-text">Wishlist</p> </a> <a href="/Shop/Detail/${datas[i].id}" class="social-info"> <span class="lnr lnr-move"></span> <p class="hover-text">view more</p> </a> </div> </div> </div> </div>'`
                }

                productContainer.innerHTML = productHTML;
            })
            .catch(error => {
                console.error('Fetch error:', error);
            });

    });
}

paginate();