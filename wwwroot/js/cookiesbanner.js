/******/ (function () { // webpackBootstrap
    var __webpack_exports__ = {};

    var divCookieBanner = document.getElementById('cookiebanner');
    var divCookieBannerNoJSstyling = document.getElementById('cookie-banner-no-js-styling');
    var divCookieBannerJSstyling = document.getElementById('cookie-banner-js-styling');
    var bannerConfirm = document.getElementById('nhsuk-cookie-confirmation-banner');
    var bannerConfirmOnPost = document.getElementById('nhsuk-cookie-confirmation-banner-post');
    var bannerCookieAccept = document.getElementById('nhsuk-cookie-banner__link_accept_analytics');
    var bannerCookieReject = document.getElementById('nhsuk-cookie-banner__link_accept');
    var cookieConsentPostPath = document.getElementById('CookieConsentPostPath');
    var path = cookieConsentPostPath === null || cookieConsentPostPath === void 0 ? void 0 : cookieConsentPostPath.value;

    if (divCookieBannerNoJSstyling != null) {
        divCookieBannerNoJSstyling.setAttribute("style", "display:none;");
    }

    if (divCookieBannerJSstyling != null) {
        divCookieBannerJSstyling.setAttribute("style", "display:block;");
    }

    bannerConfirmOnPost === null || bannerConfirmOnPost === void 0 ? void 0 : bannerConfirmOnPost.setAttribute("style", "display:none;");

    if (bannerCookieAccept != null) {
        bannerCookieAccept.addEventListener('click', function () {
            return bannerAccept("true");
        });
    }

    if (bannerCookieReject != null) {
        bannerCookieReject.addEventListener('click', function () {
            return bannerAccept("false");
        });
    }

    function bannerAccept(consentValue) {
        if (divCookieBanner != null) {
            divCookieBanner.setAttribute("style", "display:none;");
        }

        if (bannerConfirm != null) {
            bannerConfirm.setAttribute("style", "display:block;");
        }

        changeConsent(consentValue);
    }

    function changeConsent(consent) {
        var params = 'consent=' + consent;
        var request = new XMLHttpRequest();
        request.open('GET', path + '?' + params, true);
        request.setRequestHeader('Content-type', 'application/x-www-form-urlencoded;charset=UTF-8');
        request.send();
    }

    ;
})()
    ;
