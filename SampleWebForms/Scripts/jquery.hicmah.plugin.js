/// <reference path="jquery-1.4.1-vsdoc.js" />
/// <reference path="jstz.js" />
(function ($) {
    $(document).ready(function () {
        var endTime = (new Date()).getTime();
        var millisecondsLoading = endTime - startTime;//Assumes time recorded earlier in page
        var timezone = jstz.determine_timezone();//For getting olson time zone

        $.get('hicmah.ashx?d=' 
            + millisecondsLoading 
            + '&tz=' + timezone.name() 
            + '&w=' + screen.width 
            + '&h=' + screen.height
            + '&np=' + navigator.platform
        , null);
    });

})(jQuery);
