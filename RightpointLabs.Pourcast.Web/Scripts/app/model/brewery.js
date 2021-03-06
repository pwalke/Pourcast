﻿define(['jquery', 'ko'], function($, ko) {
    function Brewery(breweryJSON) {
        var self = this;

        self.id = ko.observable(breweryJSON.Id);
        self.name = ko.observable(breweryJSON.Name);
        self.city = ko.observable(breweryJSON.City);
        self.state = ko.observable(breweryJSON.State);
        self.website = ko.observable(breweryJSON.Website);
        self.location = ko.computed(function() {
            return self.city() + ", " + self.state();
        });
    };

    return Brewery;
});