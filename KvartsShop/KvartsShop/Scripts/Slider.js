window.onload = () => {
    /*------------------Declaration score---------------------------*/
    var w = document.documentElement.clientWidth;
    var productsSlider = document.getElementById("products-slider");
    var productSlider = document.getElementsByClassName("product-slider");
    var sliderItem = document.getElementById("slider-item");
    var Prev = document.getElementById("Prev");
    var Next = document.getElementById("Next");
    var nowCount = 0;
    productsSlider.hidden = true;

    var products = getNewSliderProduct();
    /*------------------Declaration score---------------------------*/

    /*------------------First display---------------------------*/
    var count = 0;
    if (w > 1200) {
        count = 3;
    }
    else if (w > 640) {
        count = 2;
    }
    else {
        count = 1;
    }
    for (var i = 0; i < count; i++) {
        sliderItem.appendChild(products[i]);
    }
    /*------------------First display---------------------------*/

    /*------------------Event for button---------------------------*/
    Prev.onclick = () => {
        GoToPrev();
    }

    Next.onclick = () => {
        GoToNext();
    }
    /*------------------Event for button---------------------------*/

    /*------------------Event Resize---------------------------*/
    window.addEventListener("resize", displayWindowSize);
    /*------------------Event Resize---------------------------*/

    /*---------------------Function---------------------------*/
    function displayWindowSize() {
        var w = document.documentElement.clientWidth;
        count = 0;
        if (w > 1200) {
            count = 3;
        }
        else if (w > 640) {
            count = 2;
        }
        else {
            count = 1;
        }
        for (var i = 0; i < count; i++) {
            sliderItem.appendChild(products[i]);
        }
        window.location.reload(true);
    }

    function getNewSliderProduct() {
        //productSlider[0].style.right = 25 + 'px';
        //productSlider[1].style.right = 25 + 'px';
        //productSlider[2].style.right = 25 + 'px';
        var products = [];
        for (var i = 0; i < productSlider.length; i++) {
            var product = document.createElement("div");

            var att_class = document.createAttribute("class")
            att_class.value = productSlider[i].attributes.item(0).textContent;
            product.setAttributeNode(att_class);

            var att_style = document.createAttribute("style")
            att_style.value = productSlider[i].attributes.item(1).textContent;
            product.setAttributeNode(att_style);

            product.innerHTML = productSlider[i].innerHTML; 

            //product.onclick = (e) => {
            //    let start = Date.now();
            //    let timer = setInterval(function () {
            //        let timePassed = Date.now() - start;

            //        productSlider[0].style.left = timePassed / 20 + 'px';
            //        productSlider[1].style.left = timePassed / 20 + 'px';
            //        productSlider[2].style.left = timePassed / 20 + 'px';

            //        if (timePassed > 500) clearInterval(timer);
            //    }, 20);
            //}

            products[i] = product;
        }
        return products;
    }

    function GoToNext() {
        if ((nowCount == 7 && count == 3) || (nowCount == 8 && count == 2) || (nowCount == 9 && count == 1)) {
            for (var i = 0; i < count; i++) {
                sliderItem.removeChild(products[nowCount]);
                nowCount++;
            }
            products = getNewSliderProduct();
            for (var i = 0; i < count; i++) {
                sliderItem.appendChild(products[i]);
            }
            nowCount = 0;
        }
        else {
            sliderItem.appendChild(products[count + nowCount]);
            sliderItem.removeChild(products[nowCount]);
            nowCount++;
        }
    }

    function GoToPrev() {
        if ((nowCount == 0)) {
            for (var i = 0; i < count; i++) {
                sliderItem.removeChild(products[nowCount]);
                nowCount++;
            }
            products = getNewSliderProduct();
            for (var i = 10 - count; i < 10; i++) {
                sliderItem.appendChild(products[i]);
            }
            nowCount = 10 - count;
        }
        else {
            sliderItem.prepend(products[nowCount - 1]);
            sliderItem.removeChild(products[nowCount - 1 + count]);
            nowCount--;
        }
    }

    setInterval(GoToNext, 3000);
    /*---------------------Function---------------------------*/
}