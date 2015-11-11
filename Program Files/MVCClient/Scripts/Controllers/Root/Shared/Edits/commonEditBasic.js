define([], (function () {

    $(document).ready(function () {
        if ($("#submitButton") != undefined)
            $("#submitButton").click(function () {
                $("#SubmitTypeOption").val(SubmitTypeOption.Save);
            });

        if ($("#closedSubmitButton") != undefined)
            $("#closedSubmitButton").click(function () {
                $("#SubmitTypeOption").val(SubmitTypeOption.Closed);
            });

        if ($("#submitCreateWizard") != undefined)
            $("#submitCreateWizard").click(function () {
                $("#SubmitTypeOption").val(SubmitTypeOption.Popup);
            });

        
    });

}));
