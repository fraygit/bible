$(function () {
    //$("#ddCountry").chosen({
    //    placeholder_text_multiple: "Select Some Options",
    //    placeholder_text_single: "Select an Option",
    //    no_result_text: "No results match"
    //});

    //$("#ddTranslation").chosen({
    //    placeholder_text_multiple: "Select Some Options",
    //    placeholder_text_single: "Select an Option",
    //    no_result_text: "No results match"
    //});

    jQuery('#dteExpires').datetimepicker({
        timepicker: false,
        format: 'd/m/Y'
    });
});