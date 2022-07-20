import {initiateDynamicWidget} from './dynamic-widget'

$(() => {
    initiateDynamicWidget();
    $(document).on("initialize-plugins", initiateDynamicWidget);
});