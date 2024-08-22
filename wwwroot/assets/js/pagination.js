//function paginate() {
//    $(document).on('click', '.page-link', async function (e) {

//        let datas = []

//        e.preventDefault();

//        var page = $(this).attr('href').split('page=')[1];

//        var prevArrow = document.querySelector(".prev-arrow");
//        var nextArrow = document.querySelector(".next-arrow");

//        var prevPage = prevArrow.getAttribute('href').split('page=')[1];
//        var nextPage = nextArrow.getAttribute('href').split('page=')[1];

//        if ($(this).hasClass("prev-arrow")) {
//            dprevArrow.href = `?page=${parseInt(prevPage) - 1}`
//            nextArrow.href = `?page=${parseInt(nextPage) - 1}`
//        }
//        if ($(this).hasClass("next-arrow")) {

//            prevArrow.href = `?page=${parseInt(prevPage) + 1}`
//            nextArrow.href = `?page=${parseInt(nextPage) + 1}`
//        }

//        if (!$(this).hasClass("prev-arrow") && !$(this).hasClass("next-arrow")) {
//            prevArrow.href = `?page=${parseInt(page) - 1}`
//            nextArrow.href = `?page=${parseInt(page) + 1}`
//        };

//        var pageLinks = document.querySelectorAll(".page-link");

//        for (var i = 0; i < pageLinks.length; i++) {
//            if (parseInt(pageLinks[i].getAttribute('href').split('page=')[1]) == parseInt(page)) {
//                pageLinks[i].classList.add("active")
//            } else {
//                pageLinks[i].classList.remove("active")
//            }
//        }


//        await fetch('/shop/pagedData?pageNumber=' + page)
//            .then(response => {
//                if (!response.ok) {
//                    // Response not OK, throw an error with status text
//                    return response.text().then(text => {
//                        throw new Error(`HTTP error! status: ${response.status} - ${text}`);
//                    });
//                }
//                return response.json(); // Parse JSON response
//            })
//            .then(data => {
//                datas = data.$values
//                console.log(datas);
//                let productContainer = document.getElementById("product-container");

//                let productHTML = "";
//                for (var i = 0; i < datas.length; i++) {
//                    productHTML += `<div class="col-lg-4 col-md-6"> <div class="single-product"> <img class="img-fluid" src="./assets/uploads/images/${datas[i].images.$values.filter(n => n.isMain == true)[0].url}" alt=""> <div class="product-details"> <h6>${datas[i].title}</h6> <div class="price">`

//                    if (datas[i].discountValue != 0) {
//                        // Calculate the discounted price
//                        const discountedPrice = Math.round((datas[i].price - (datas[i].price * datas[i].discountValue / 100)) * 100) / 100;

//                        // Construct the HTML string with both the discounted and original price
//                        productHTML += `<h6>$${discountedPrice}</h6> <h6 class="l-through">$${datas[i].price}</h6>
//                    `;
//                    } else {
//                        // Construct the HTML string with the original price only
//                        productHTML += `<h6>$${datas[i].price}</h6>`;
//                    }

//                    productHTML += `</div> <div class="prd-bottom"> <a href="/Cart/AddToCart/${datas[i].id}" class="social-info"> <span class="ti-bag"></span> <p class="hover-text">add to bag</p> </a> <a href="/Wishlist/AddToWishlist/${datas[i].id}" class="social-info"> <span class="lnr lnr-heart"></span> <p class="hover-text">Wishlist</p> </a> <a href="/Shop/Detail/${datas[i].id}" class="social-info"> <span class="lnr lnr-move"></span> <p class="hover-text">view more</p> </a> </div> </div> </div> </div>'`
//                }

//                productContainer.innerHTML = productHTML;
//            })
//            .catch(error => {
//                console.error('Fetch error:', error);
//            });

//    });
//}

//paginate();

function changeQuantity() {

    $(document).on('input', '.change-quantity', async function (e) {
        let data = [];
        e.preventDefault();

        var productId = $(this).attr('productId')
        var quantity = $(this).val();

        if (typeof(parseInt(quantity)) === "number" && quantity != 0) {
            await fetch(`/cart/changeQuantity?quantity=${quantity}&productId=${productId}`)
                .then(response => {
                    if (!response.ok) {
                        // Response not OK, throw an error with status text
                        return response.text().then(text => {
                            throw new Error(`HTTP error! status: ${response.status} - ${text}`);
                        });
                    }
                    return response.json(); // Parse JSON response
                })
                .then(newData => {
                    data = newData.$values;
                    console.log(data);
                    let productContainer = document.getElementById("cart-container");

                    let productHTML = "";

                    for (var i = 0; i < data.length; i++) {
                        productHTML += `<tr> <td> <div class="media"> <div class="d - flex"> <img style="height: 50px; width: 50px; object-fit: cover" src="./assets/uploads/images/${data[i].images.$values.filter(n => n.isMain == true)[0].url}" alt="" />`;
                        productHTML += `</div> <div class="media-body"> <p>${data[i].title}</p> </div> </div> </td> <td>`
                        if (data[i].discountValue != 0) {
                            const discountedPrice = Math.round((data[i].price - (data[i].price * data[i].discountValue / 100)) * 100) / 100;
                            productHTML += `<h5>$${Math.round((discountedPrice) * 100) / 100}</h5> <h5 class="l-through">$${Math.round((data[i].price) * 100) / 100}</h5>`;
                        } else {
                            productHTML += `<h5>$${Math.round((data[i].price) * 100) / 100}</h5>`;
                        }

                        productHTML += `</td > <td> <div class="product_count"> <input class="change-quantity" productId="${data[i].productId}" type="text" id="sst" maxlength="12" value="${data[i].quantity}" title="Quantity:" class="input-text qty"> </div> </td>`

                        if (data[i].discountValue != 0) {
                            const discountedPrice = Math.round((data[i].price - (data[i].price * data[i].discountValue / 100)) * 100) / 100;
                            productHTML += `<td> <h5>$${Math.round((discountedPrice * data[i].quantity) * 100) / 100}</h5> </td> </tr>`;
                        } else {
                            productHTML += `<td> <h5>$${Math.round((data[i].price * data[i].quantity) * 100) / 100}</h5> </td> </tr>`;
                        }
                    }

                    productHTML += `<tr> <td> </td> <td> </td> <td> <h5>Subtotal</h5> </td> <td> <h5>$${Math.round(data[0].totalPrice * 100) / 100}</h5> </td> </tr> <tr class="shipping_area"> <td> </td> <td> </td>`

                    productContainer.innerHTML = productHTML;
                })
                .catch(error => {
                    console.error('Fetch error:', error);
                });
        }
    });

};

changeQuantity();