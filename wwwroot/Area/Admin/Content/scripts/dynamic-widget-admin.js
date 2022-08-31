import {initiateDynamicWidget} from './dynamic-widget'
import { initializeHtmlEditor  } from "./html-editor";
import { initializeCssEditor  } from "./css-editor";
import { initializeJavaScriptEditor  } from "./js-editor";
import { setupWebpageUrlSelector  } from "./webpage-url-selector";
$(() => {
    initiateDynamicWidget();
    initializeHtmlEditor();
    initializeCssEditor();
    initializeJavaScriptEditor();
    setupWebpageUrlSelector();
    $(document).on("initialize-plugins", initiateDynamicWidget);
    $(document).on("initialize-plugins", initializeHtmlEditor);
    $(document).on("initialize-plugins", initializeCssEditor);
    $(document).on("initialize-plugins", initializeJavaScriptEditor);
    $(document).on("initialize-plugins", setupWebpageUrlSelector);
});