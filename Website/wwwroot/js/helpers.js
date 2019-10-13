$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();

    // Focus an input with autofocus when opening a modal
    $('.modal').on('shown.bs.modal', function () {
        $(this).find('[autofocus]').focus();
    });

    // Activate Alex's disable forms on submit plugin
    $('form[data-disable-on-submit]').disableOnSubmit({
        buttonTemplate: '<span class="mdi mdi-loading mdi-spin-fast"></span>'
    });

    // Show TempData Notification using Bootstrap notify
    var tempDataMessage = $('#tempDataMessage').val();
    if (tempDataMessage) {
        notify(tempDataMessage, 'success');
    }
});

////#region Notification Helper
//function notify(text, type, duration) {
//    if (duration === undefined) {
//        duration = 4000;
//    }

//    $.toast({
//        text,
//        icon:type,
//        showHideTransition: 'slide',
//        allowToastClose: true,
//        hideAfter: duration,
//        stack: false,
//        position: 'bottom-center', 
//    })
//}
////#endregion

//#region Notification Helper
/** @description Generates a notification
 * @param {string} text The text of the notification.
 * @param {string} type The type of the notification. Options are default, success, info, warning, error. Default = default
 * @param {bool} showIcon Sets if the icon show be shown for the notification. Default = false
 * @param {string} icon Class of the icon. Defaults to the default icon for the type if blank or null.
 * @param {string} size sets the size of the notification. Options are normal, mini, large. Default = mini
 * @param {number} duration Duration in milliseconds that the notifciation will stay on the screen. Default = 4000
 */
function notify(text, type, showIcon, icon, size, duration) {
    if (duration === undefined) {
        duration = 4000;
    }
    if (type === undefined || type === null || type.length < 1) {
        type = 'default';
    }
    if (size === undefined || size === null || size.length < 1) {
        size = 'mini';
    }

    if (showIcon) {
        if (icon === undefined || icon === null || icon.length < 1) {
            if (type === 'success') {
                icon = 'mdi mdi-check';
            } else if (type === 'error') {
                icon = 'mdi mdi-alert-octagon-outline';
            } else if (type === 'warning') {
                icon = 'mdi mdi-alert-outline';
            } else if (type === 'info') {
                icon = 'mdi mdi-exclamation';
            }
        }
    } else {
        icon = false;
    }

    Lobibox.notify(type, {
        title: false,
        msg: text,
        size: size,
        sound: false,
        delay: duration,
        icon: icon
    });
}

function notifySimple(text, type) {
    Lobibox.notify(type, {
        title: false,
        msg: text,
        size: 'mini',
        sound: false,
        delay: 4000,
        icon: false
    });
}
//#endregion