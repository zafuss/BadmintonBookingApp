﻿let sidebar = document.querySelector(".sidebar");
let btn = document.querySelector("#btn");


if (btn != null) {
    btn.addEventListener("click", () => {
        sidebar.classList.toggle("open");
        menuBtnChange();
    });
}


function menuBtnChange() {
    if (sidebar.classList.contains("open")) {
        btn.classList.replace("bx-menu", "bx-menu-alt-right");
    } else {
        btn.classList.replace("bx-menu-alt-right", "bx-menu");
    }
}
