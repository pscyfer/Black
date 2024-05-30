"use strict";

let isRtl = window.Helpers.isRtl(),
    isDarkStyle = window.Helpers.isDarkStyle(),
    menu,
    animate,
    isHorizontalLayout = !1;
document.getElementById("layout-menu") &&
    (isHorizontalLayout = document
        .getElementById("layout-menu")
        .classList.contains("menu-horizontal")),
    (function () {
        function e() {
            var e = document.querySelector(".layout-page");
            e &&
                (0 < window.pageYOffset
                    ? e.classList.add("window-scrolled")
                    : e.classList.remove("window-scrolled"));
        }
        "undefined" != typeof Waves &&
            (Waves.init(),
                Waves.attach(
                    ".btn[class*='btn-']:not(.position-relative):not([class*='btn-outline-']):not([class*='btn-label-'])",
                    ["waves-light"]
                ),
                Waves.attach("[class*='btn-outline-']:not(.position-relative)"),
                Waves.attach("[class*='btn-label-']:not(.position-relative)"),
                Waves.attach(".pagination .page-item .page-link"),
                Waves.attach(".dropdown-menu .dropdown-item"),
                Waves.attach(".light-style .list-group .list-group-item-action"),
                Waves.attach(".dark-style .list-group .list-group-item-action", [
                    "waves-light",
                ]),
                Waves.attach(".nav-tabs:not(.nav-tabs-widget) .nav-item .nav-link"),
                Waves.attach(".nav-pills .nav-item .nav-link", ["waves-light"]),
                Waves.attach(".menu-vertical .menu-item .menu-link.menu-toggle")),
            setTimeout(() => {
                e();
            }, 200),
            (window.onscroll = function () {
                e();
            }),
            document.querySelectorAll("#layout-menu").forEach(function (e) {
                (menu = new Menu(e, {
                    orientation: isHorizontalLayout ? "horizontal" : "vertical",
                    closeChildren: !!isHorizontalLayout,
                    showDropdownOnHover: localStorage.getItem(
                        "templateCustomizer-" + templateName + "--ShowDropdownOnHover"
                    )
                        ? "true" ===
                        localStorage.getItem(
                            "templateCustomizer-" + templateName + "--ShowDropdownOnHover"
                        )
                        : void 0 === window.templateCustomizer ||
                        window.templateCustomizer.settings.defaultShowDropdownOnHover,
                })),
                    window.Helpers.scrollToActive((animate = !1)),
                    (window.Helpers.mainMenu = menu);
            }),
            document.querySelectorAll(".layout-menu-toggle").forEach((e) => {
                e.addEventListener("click", (e) => {
                    if (
                        (e.preventDefault(),
                            window.Helpers.toggleCollapsed(),
                            config.enableMenuLocalStorage && !window.Helpers.isSmallScreen())
                    )
                        try {
                            localStorage.setItem(
                                "templateCustomizer-" + templateName + "--LayoutCollapsed",
                                String(window.Helpers.isCollapsed())
                            );
                        } catch (e) { }
                });
            }),
            window.Helpers.swipeIn(".drag-target", function (e) {
                window.Helpers.setCollapsed(!1);
            }),
            window.Helpers.swipeOut("#layout-menu", function (e) {
                window.Helpers.isSmallScreen() && window.Helpers.setCollapsed(!0);
            });
        let t = document.getElementsByClassName("menu-inner"),
            a = document.getElementsByClassName("menu-inner-shadow")[0];
        0 < t.length &&
            a &&
            t[0].addEventListener("ps-scroll-y", function () {
                this.querySelector(".ps__thumb-y").offsetTop
                    ? (a.style.display = "block")
                    : (a.style.display = "none");
            });
        var n = document.querySelector(".style-switcher-toggle");
        function o(a) {
            [].slice
                .call(document.querySelectorAll("[data-app-" + a + "-img]"))
                .map(function (e) {
                    var t = e.getAttribute("data-app-" + a + "-img");
                    e.src = assetsPath + "img/" + t;
                });
        }
        window.templateCustomizer
            ? (n &&
                n.addEventListener("click", function () {
                    window.Helpers.isLightStyle()
                        ? window.templateCustomizer.setStyle("dark")
                        : window.templateCustomizer.setStyle("light");
                }),
                window.Helpers.isLightStyle()
                    ? (n &&
                        (n.querySelector("i").classList.add("mdi-weather-night"),
                            new bootstrap.Tooltip(n, {
                                title: "Dark mode",
                                fallbackPlacements: ["bottom"],
                            })),
                        o("light"))
                    : (n &&
                        (n.querySelector("i").classList.add("mdi-weather-sunny"),
                            new bootstrap.Tooltip(n, {
                                title: "Light mode",
                                fallbackPlacements: ["bottom"],
                            })),
                        o("dark")))
            : n.parentElement.remove(),
            "undefined" != typeof i18next &&
            "undefined" != typeof i18NextHttpBackend &&
            i18next
                .use(i18NextHttpBackend)
                .init({
                    lng: "fa",
                    debug: !1,
                    fallbackLng: "fa",
                    backend: { loadPath: assetsPath + "json/locales/{{lng}}.json" },
                    returnObjects: !0,
                })
                .then(function (e) {
                    l();
                });
        n = document.getElementsByClassName("dropdown-language");
        if (n.length) {
            var s = n[0].querySelectorAll(".dropdown-item");
            for (let e = 0; e < s.length; e++)
                s[e].addEventListener("click", function () {
                    var e,
                        t = this.getAttribute("data-language");
                    for (e of this.parentNode.children)
                        for (var a = e.parentElement.parentNode.firstChild; a;)
                            1 === a.nodeType &&
                                a !== a.parentElement &&
                                a.querySelector(".dropdown-item").classList.remove("active"),
                                (a = a.nextSibling);
                    this.classList.add("active"),
                        i18next.changeLanguage(t, (e, t) => {
                            if (e) return console.log("something went wrong loading", e);
                            l();
                        });
                });
        }
        function l() {
            var e = document.querySelectorAll("[data-i18n]"),
                t = document.querySelector(
                    '.dropdown-item[data-language="' + i18next.language + '"]'
                );
            t && t.click(),
                e.forEach(function (e) {
                    e.innerHTML = i18next.t(e.dataset.i18n);
                });
        }
        n = document.querySelector(".dropdown-notifications-all");
        function i(e) {
            "show.bs.collapse" == e.type || "show.bs.collapse" == e.type
                ? e.target.closest(".accordion-item").classList.add("active")
                : e.target.closest(".accordion-item").classList.remove("active");
        }
        const r = document.querySelectorAll(".dropdown-notifications-read");
        n &&
            n.addEventListener("click", (e) => {
                r.forEach((e) => {
                    e.closest(".dropdown-notifications-item").classList.add(
                        "marked-as-read"
                    );
                });
            }),
            r &&
            r.forEach((t) => {
                t.addEventListener("click", (e) => {
                    t.closest(".dropdown-notifications-item").classList.toggle(
                        "marked-as-read"
                    );
                });
            }),
            document
                .querySelectorAll(".dropdown-notifications-archive")
                .forEach((t) => {
                    t.addEventListener("click", (e) => {
                        t.closest(".dropdown-notifications-item").remove();
                    });
                }),
            [].slice
                .call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
                .map(function (e) {
                    return new bootstrap.Tooltip(e);
                });
        [].slice.call(document.querySelectorAll(".accordion")).map(function (e) {
            e.addEventListener("show.bs.collapse", i),
                e.addEventListener("hide.bs.collapse", i);
        });
        if (
            (isRtl &&
                Helpers._addClass(
                    "dropdown-menu-end",
                    document.querySelectorAll("#layout-navbar .dropdown-menu")
                ),
                window.Helpers.setAutoUpdate(!0),
                window.Helpers.initPasswordToggle(),
                window.Helpers.initSpeechToText(),
                window.Helpers.navTabsAnimation(),
                window.Helpers.initNavbarDropdownScrollbar(),
                window.addEventListener(
                    "resize",
                    function (e) {
                        window.innerWidth >= window.Helpers.LAYOUT_BREAKPOINT &&
                            document.querySelector(".search-input-wrapper") &&
                            (document
                                .querySelector(".search-input-wrapper")
                                .classList.add("d-none"),
                                (document.querySelector(".search-input").value = "")),
                            document.querySelector("[data-template^='horizontal-menu']") &&
                            setTimeout(function () {
                                window.innerWidth < window.Helpers.LAYOUT_BREAKPOINT
                                    ? document.getElementById("layout-menu") &&
                                    document
                                        .getElementById("layout-menu")
                                        .classList.contains("menu-horizontal") &&
                                    menu.switchMenu("vertical")
                                    : document.getElementById("layout-menu") &&
                                    document
                                        .getElementById("layout-menu")
                                        .classList.contains("menu-vertical") &&
                                    menu.switchMenu("horizontal");
                            }, 100),
                            window.Helpers.navTabsAnimation();
                    },
                    !0
                ),
                !isHorizontalLayout &&
                !window.Helpers.isSmallScreen() &&
                ("undefined" != typeof TemplateCustomizer &&
                    window.templateCustomizer.settings.defaultMenuCollapsed &&
                    window.Helpers.setCollapsed(!0, !1),
                    "undefined" != typeof config) &&
                config.enableMenuLocalStorage)
        )
            try {
                null !==
                    localStorage.getItem(
                        "templateCustomizer-" + templateName + "--LayoutCollapsed"
                    ) &&
                    "false" !==
                    localStorage.getItem(
                        "templateCustomizer-" + templateName + "--LayoutCollapsed"
                    ) &&
                    window.Helpers.setCollapsed(
                        "true" ===
                        localStorage.getItem(
                            "templateCustomizer-" + templateName + "--LayoutCollapsed"
                        ),
                        !1
                    );
            } catch (e) { }
    })(),
    "undefined" != typeof $ &&
    $(function () {
        window.Helpers.initSidebarToggle();
        var t,
            a,
            e,
            n = $(".search-toggler"),
            o = $(".search-input-wrapper"),
            s = $(".search-input"),
            l = $(".content-backdrop");
        n.length &&
            n.on("click", function () {
                o.length && (o.toggleClass("d-none"), s.focus());
            }),
            $(document).on("keydown", function (e) {
                var t = e.ctrlKey,
                    e = 191 === e.which;
                t && e && o.length && (o.toggleClass("d-none"), s.focus());
            }),
            s.on("focus", function () {
                o.hasClass("container-xxl") &&
                    o.find(".twitter-typeahead").addClass("container-xxl");
            }),
            s.length &&
            ((t = function (n) {
                return function (t, e) {
                    let a;
                    (a = []),
                        n.filter(function (e) {
                            if (e.name.toLowerCase().startsWith(t.toLowerCase()))
                                a.push(e);
                            else {
                                if (
                                    e.name.toLowerCase().startsWith(t.toLowerCase()) ||
                                    !e.name.toLowerCase().includes(t.toLowerCase())
                                )
                                    return [];
                                a.push(e),
                                    a.sort(function (e, t) {
                                        return t.name < e.name ? 1 : -1;
                                    });
                            }
                        }),
                        e(a);
                };
            }),
                (n = "search-vertical.json"),
                $("#layout-menu").hasClass("menu-horizontal") &&
                (n = "search-horizontal.json"),
                (a = $.ajax({
                    url: assetsPath + "json/" + n,
                    dataType: "json",
                    async: !1,
                }).responseJSON),
                s.each(function () {
                    var e = $(this);
                    s
                        .typeahead(
                            {
                                hint: !1,
                                classNames: {
                                    menu: "tt-menu navbar-search-suggestion",
                                    cursor: "active",
                                    suggestion:
                                        "suggestion d-flex justify-content-between px-3 py-2 w-100",
                                },
                            },
                            {
                                name: "pages",
                                display: "name",
                                limit: 5,
                                source: t(a.pages),
                                templates: {
                                    header:
                                        '<h6 class="suggestions-header text-primary mb-0 mx-3 mt-3 pb-2">Pages</h6>',
                                    suggestion: function ({ url: e, icon: t, name: a }) {
                                        return (
                                            '<a href="' +
                                            e +
                                            '"><div><i class="mdi ' +
                                            t +
                                            ' me-2"></i><span class="align-middle">' +
                                            a +
                                            "</span></div></a>"
                                        );
                                    },
                                    notFound:
                                        '<div class="not-found px-3 py-2"><h6 class="suggestions-header text-primary mb-2">Pages</h6><p class="py-2 mb-0"><i class="mdi mdi-alert-circle-outline me-2 mdi-14px"></i> No Results Found</p></div>',
                                },
                            },
                            {
                                name: "files",
                                display: "name",
                                limit: 4,
                                source: t(a.files),
                                templates: {
                                    header:
                                        '<h6 class="suggestions-header text-primary mb-0 mx-3 mt-3 pb-2">Files</h6>',
                                    suggestion: function ({
                                        src: e,
                                        name: t,
                                        subtitle: a,
                                        meta: n,
                                    }) {
                                        return (
                                            '<a href="javascript:;"><div class="d-flex w-50"><img class="me-3" src="' +
                                            assetsPath +
                                            e +
                                            '" alt="' +
                                            t +
                                            '" height="32"><div class="w-75"><h6 class="mb-0">' +
                                            t +
                                            '</h6><small class="text-muted">' +
                                            a +
                                            '</small></div></div><small class="text-muted">' +
                                            n +
                                            "</small></a>"
                                        );
                                    },
                                    notFound:
                                        '<div class="not-found px-3 py-2"><h6 class="suggestions-header text-primary mb-2">Files</h6><p class="py-2 mb-0"><i class="mdi mdi-alert-circle-outline me-2 mdi-14px"></i> No Results Found</p></div>',
                                },
                            },
                            {
                                name: "members",
                                display: "name",
                                limit: 4,
                                source: t(a.members),
                                templates: {
                                    header:
                                        '<h6 class="suggestions-header text-primary mb-0 mx-3 mt-3 pb-2">Members</h6>',
                                    suggestion: function ({ name: e, src: t, subtitle: a }) {
                                        return (
                                            '<a href="app-user-view-account.html"><div class="d-flex align-items-center"><img class="rounded-circle me-3" src="' +
                                            assetsPath +
                                            t +
                                            '" alt="' +
                                            e +
                                            '" height="32"><div class="user-info"><h6 class="mb-0">' +
                                            e +
                                            '</h6><small class="text-muted">' +
                                            a +
                                            "</small></div></div></a>"
                                        );
                                    },
                                    notFound:
                                        '<div class="not-found px-3 py-2"><h6 class="suggestions-header text-primary mb-2">Members</h6><p class="py-2 mb-0"><i class="mdi mdi-alert-circle-outline me-2 mdi-14px"></i> No Results Found</p></div>',
                                },
                            }
                        )
                        .bind("typeahead:render", function () {
                            l.addClass("show").removeClass("fade");
                        })
                        .bind("typeahead:select", function (e, t) {
                            t.url && (window.location = t.url);
                        })
                        .bind("typeahead:close", function () {
                            s.val(""),
                                e.typeahead("val", ""),
                                o.addClass("d-none"),
                                l.addClass("fade").removeClass("show");
                        }),
                        s.on("keyup", function () {
                            "" == s.val() && l.addClass("fade").removeClass("show");
                        });
                }),
                $(".navbar-search-suggestion").each(function () {
                    e = new PerfectScrollbar($(this)[0], {
                        wheelPropagation: !1,
                        suppressScrollX: !0,
                    });
                }),
                s.on("keyup", function () {
                    e.update();
                }));
    });
