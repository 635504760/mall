var util = {
    getRegion: function (regionId) {
        if (!province) {
            return '';
        }

        var result = '';

        $(province).each(function () {
            var p = this;
            if (p.id == regionId) {
                result = p.name
                return;
            }

            $(p.city).each(function () {
                var c = this;
                if (c.id == regionId) {
                    result = p.name + ' ' + c.name;
                    return;
                }

                $(c.county).each(function () {
                    if (this.id == regionId) {
                        result = p.name + ' ' + c.name + ' ' + this.name;
                        return;
                    }
                });
            });
        });

        return result;

    }
};