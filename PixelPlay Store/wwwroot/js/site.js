$(document).ready(function () {
    $('select').select2();
});


document.addEventListener("DOMContentLoaded", function () {
    const listItems = document.querySelectorAll("ul li");

    listItems.forEach(item => {
        item.addEventListener("click", function () {
            // Remove 'selected' class from all items
            listItems.forEach(li => li.classList.remove(".select2-results__option[aria-selected]"));

            // Add 'selected' class to the clicked item
            this.classList.add("selected");
        });
    });
});
