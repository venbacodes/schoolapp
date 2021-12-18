$(document).ready(function () {

    //Update all anchor tags with target=_blank whose href points to a different domain than the current domain
    var currentDomain = location.host;
    currentDomain = new RegExp(currentDomain, "i");

    var anchorTags = document.getElementsByTagName('a');
    for (var i = 0; i < anchorTags.length; i++) {
        var href = anchorTags[i].host;
        if (!currentDomain.test(href)) {
            anchorTags[i].setAttribute('target', '_blank');
        }
    }
    //Update all anchor tags with target=_blank whose href points to a different domain than the current domain


    //Add border-bottom for the active menu
    var pathname = "/" + window.location.pathname.split("/")[1];
    $("ul.navbar-nav > li.nav-item > a.nav-link[href='" + pathname + "']").addClass("active");
    //Add border-bottom for the active menu

});