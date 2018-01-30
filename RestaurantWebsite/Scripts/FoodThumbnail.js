//<script src></script>

function FoodThumbnail(foodName, foodDescription, foodPictureSrc, detailLink) {
    //this.link = $("a").attr("href", detailLink);
    //this.image = $("img").attr("src", foodPictureSrc).attr("alt", foodName);
    //this.caption = $("div").addClass("caption").append($("h3").text(foodName)).append($("p").text(foodDescription))

    return "<div class='thumbnail'><img src ='" + foodPictureSrc + "'></img><div class='caption'><h3>" + foodName + "</h3><p>" + foodDescription + "</p></div></div>";


    //this.htmlThumbnail = function () {
        //var skeleton = "<div class='thumbnail'><img src ='"+ foodPictureSrc + "' ></img><div class='caption'><h3>" + foodName + "</h3><p>" + foodDescription + "</p></div></div>";

        //return skeleton;

        //$(this).(".thumbnail img")
        //    .attr(
        //    {
        //        "src": foodPictureSrc,
        //        "alt" : foodName
        //    })
        //(".caption h3").text(foodName)
        //("caption p").text(foodDescription);
    //}

        //return $("div")
        //.addClass("thumbnail")
        //.append(link)
        //.append(image)
        //.append(caption);
    }

    //this.$htmlSkeleton = ("<div class='thumbnail'><img></img><div class='caption'><h3></h3><p></p></div></div>")

    //$(this.htmlSkeleton).("img").attr("src", foodPictureSrc).attr("alt", foodName)

        //"<div class=>" +
        //"<img src='foodPictureSrc' alt='foodName'>"
        //+ "</div>"