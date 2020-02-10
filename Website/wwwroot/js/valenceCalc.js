$(document).ready(function () {

    $('#kuva-weapon-select').on('change', function (e) {
        var option = $('#kuva-weapon-select option:selected');
        var name = option.data('name');

        var damage = option.data('damage');

        $('#kuva-weapon-name').text(name);
        $('#valence-calculator-inputs').show();

        for (var prop in damage) {
            if (damage.hasOwnProperty(prop)) {
                $('#' + prop.toLowerCase() + '-base-damage').text(damage[prop]);
            }
        }
    });

    $('#valence-clear-all').on('click', function (e) {
        $('.valence-bonus-input').val('');
    });

    $('#valence-calculate').on('click', function (e) {
        var valid = false;
        var bonusDamage;
        var bonusDamageName;
        $('.valence-bonus-input').each(function () {
            var ele = $(this);
            if (ele.val() > 0) {
                if (bonusDamage === undefined) {
                    bonusDamage = ele.val();
                    bonusDamageName = ele.data('damage-name');
                    valid = true;
                } else {
                    alert('Multiple bonuses input. Only one bonus is allowed.');
                    valid = false;
                    return;
                }
            }
        });

        if (valid) {
            var totalDamage = $('#kuva-weapon-select option:selected').data('total-damage');
            if (totalDamage.indexOf(' ') > -1) {
                // Some entries have the damage type after the number
                totalDamage = parseFloat(totalDamage.split(' ')[0]);
            }

            var decPercent = parseFloat(bonusDamage) / totalDamage;
            var percent = (parseFloat(decPercent).toFixed(2) * 100).toFixed(0);

            alert(percent + '% ' + bonusDamageName);
        }
    });
});