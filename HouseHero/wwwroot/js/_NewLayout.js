document.addEventListener("DOMContentLoaded", function () {
  const profileBtn = document.getElementById("profile-btn");
  const profileMenu = document.getElementById("profile-menu");

  profileBtn.addEventListener("click", function () {

    if (
      profileMenu.style.display === "none" ||
      profileMenu.style.display === ""
    ) {
      profileMenu.style.display = "block";
    } else {
      profileMenu.style.display = "none";
    }
  });

  window.addEventListener("click", function (event) {
    if (!profileBtn.contains(event.target)) {
      profileMenu.style.display = "none";
    }
  });
});

