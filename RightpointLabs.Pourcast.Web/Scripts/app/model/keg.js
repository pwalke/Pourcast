﻿define(['jquery', 'ko', 'app/events'], function ($, ko, events) {
    function Keg(kegJSON, beer) {
        var self = this;

        self.id = ko.observable(kegJSON.Id);
        self.percentRemaining = ko.observable((kegJSON.PercentRemaining * 100).toFixed(1));
        self.isEmpty = ko.observable(kegJSON.IsEmpty);
        self.isPouring = ko.observable(kegJSON.IsPouring);
        self.capacity = ko.observable(kegJSON.Capacity);
        self.beer = ko.observable(beer);

        self.isLow = ko.computed(function () {
            return self.percentRemaining() < 25;
        });

        self.percentRemainingStyle = ko.computed(function () {
            return Math.floor(self.percentRemaining()) + '%';
        });
        self.percentRemainingHtml = ko.computed(function () {
            return parseFloat(self.percentRemaining()).toFixed(1) + '<span class="symbol">%</span>';
        });
        self.percentRemainingClass = ko.computed(function () {
            return self.isLow() ? "low" : "high";
        });

        events.on("PourStarted", function (e) {
            console.log("PourStarted");
            if (e.KegId === self.id()) {
                self.isPouring(true);
            }
        });
        events.on("PourStopped", function (e) {
            console.log("PourStopped");

            if (e.KegId === self.id()) {
                self.isPouring(false);
                self.percentRemaining((e.PercentRemaining * 100).toFixed(1));
            }
        });
    };

    return Keg;
});