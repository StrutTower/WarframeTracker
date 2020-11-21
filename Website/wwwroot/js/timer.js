$(document).ready(function () {
    var timers = $('.cetus-time-counter');
    if (timers.length) {
        $.each(timers, function () {
            var current = $(this);
            var isDay = current.data('is-day') === 'True';
            var daySeconds = current.data('day-seconds');
            var nightSeconds = current.data('night-seconds');

            var nextCycleDate = new Date(current.data('next-cycle'));
            var nextCycleTime = nextCycleDate.getTime();
            swapIcon($('#cetus-time-icon'), isDay);

            setInterval(function () {
                var now = new Date;
                var utc_timestamp = Date.UTC(now.getUTCFullYear(), now.getUTCMonth(), now.getUTCDate(),
                    now.getUTCHours(), now.getUTCMinutes(), now.getUTCSeconds(), now.getUTCMilliseconds());
                // Find the distance between now and the count down date
                var distance = nextCycleTime - utc_timestamp;

                // If the count down is finished, write some text
                if (distance < 0) {
                    if (isDay) {
                        nextCycleTime += nightSeconds;
                    } else {
                        nextCycleTime += daySeconds;
                    }
                    isDay = !isDay;
                    // Recalc distance
                    var distance = nextCycleTime - utc_timestamp;
                }

                var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
                var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
                //var seconds = Math.floor((distance % (1000 * 60)) / 1000);

                var output = minutes + "m "/* + seconds + "s"*/;
                if (hours > 0) {
                    output = hours + "h " + output;
                }

                current.text((isDay ? "Day for " : "Night for ") + output);
            }, 1000);
        });
    }

    function swapIcon(element, isDay) {
        if (isDay) {
            element.addClass('mdi-weather-sunny');
        } else {
            element.addClass('mdi-weather-night');
        }
    }
});