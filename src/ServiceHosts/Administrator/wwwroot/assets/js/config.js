"use strict";
let config = {
    colors: {
      primary: "#666cff",
      secondary: "#6d788d",
      success: "#72e128",
      info: "#26c6f9",
      warning: "#fdb528",
      danger: "#ff4d49",
      dark: "#4b4b4b",
      black: "#000",
      white: "#fff",
      cardColor: "#fff",
      bodyBg: "#f7f7f9",
      bodyColor: "#828393",
      headingColor: "#636578",
      textMuted: "#bbbcc4",
      borderColor: "#eaeaec",
    },
    colors_label: {
      primary: "#666cff29",
      secondary: "#6d788d29",
      success: "#72e12829",
      info: "#26c6f929",
      warning: "#fdb52829",
      danger: "#ff4d4929",
      dark: "#4b4b4b29",
    },
    colors_dark: {
      cardColor: "#30334e",
      bodyBg: "#282a42",
      bodyColor: "#a0a1b8",
      headingColor: "#d2d2e8",
      textMuted: "#777991",
      borderColor: "#464963",
    },
    enableMenuLocalStorage: !0,
  },
  assetsPath = document.documentElement.getAttribute("data-assets-path"),
  templateName = document.documentElement.getAttribute("data-template"),
  rtlSupport = !0;
"undefined" != typeof TemplateCustomizer &&
  (window.templateCustomizer = new TemplateCustomizer({
    cssPath: assetsPath + "vendor/css" + (rtlSupport ? "/rtl" : "") + "/",
    themesPath: assetsPath + "vendor/css" + (rtlSupport ? "/rtl" : "") + "/",
    displayCustomizer: !0,
    defaultShowDropdownOnHover: !0,
  }));
