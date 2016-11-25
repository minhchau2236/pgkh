; (function ($) {
    $.extend($.ui.tabs.prototype, {
        rotation: null, rotationDelay: null, continuing: null, rotate: function (ms, continuing) {
            var self = this, o = this.options; if ((ms > 1 || self.rotationDelay === null) && ms !== undefined) { self.rotationDelay = ms; }
            if (continuing !== undefined) { self.continuing = continuing; }
            var rotate = self._rotate || (self._rotate = function (e) { clearTimeout(self.rotation); self.rotation = setTimeout(function () { var t = o.active; self.option("active", ++t < self.anchors.length ? t : 0); }, ms); if (e) { e.stopPropagation(); } }); var stop = self._unrotate || (self._unrotate = !continuing ? function (e) { if (e.clientX) { self.rotate(null); } } : function (e) { t = o.active; rotate(); }); if (ms) { this.element.bind("tabsactivate", rotate); this.anchors.bind(o.event + ".tabs", stop); rotate(); } else { clearTimeout(self.rotation); this.element.unbind("tabsactivate", rotate); this.anchors.unbind(o.event + ".tabs", stop); delete this._rotate; delete this._unrotate; }
            if (ms === 1) { ms = self.rotationDelay; }
            return this;
        }, pause: function () { var self = this, o = this.options; self.rotate(0); }, unpause: function () { var self = this, o = this.options; self.rotate(1, self.continuing); }
    });
})(jQuery);