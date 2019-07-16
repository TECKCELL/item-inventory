function Item(LabelImmo, Familly, SFamilly, Quantity) {
    this.LabelImmo = ko.observable(LabelImmo);
    this.Familly = ko.observable(Familly);
    this.SFamilly = ko.observable(SFamilly);
    this.Quantity = ko.observable(Quantity);

}
var array = []
var itemArray = []
var mappedSSearchData = []

var viewModel = { 
    items: ko.observableArray([]),
    filter: ko.observable(""),
    Familly: ko.observable(""),
    Sfamilly: ko.observableArray([]),
    Ssfamilly: ko.observable(""),
    
    searchFamilly: function () {

        var familly = viewModel.Familly();
        var JSONdataFromServer = CountingProductFile;
        //parse into an object
        var dataFromServer = JSONdataFromServer;
        mappedSSearchData = ko.utils.arrayMap(dataFromServer, function (item) {
            return new Item(item.LabelImmo,
                item.Familly,
                item.SFamilly,
                item.Quantity
            )
        });

        if (familly) {

            var array = ko.utils.arrayFilter(mappedSSearchData, function (item) {
                return item.Familly().includes(familly);;
            });


            this.items(array);
            viewModel.Sfamilly([]);
            var sarray = [];
            array.forEach(function (element) {

                sarray.push(element.SFamilly());
            })
            viewModel.Sfamilly(ko.utils.arrayGetDistinctValues(sarray).sort())
        } else {
            return this.items(mappedSSearchData);
        }


    },
    SearchSfamilly: function () {

        var Ssfamilly = viewModel.Ssfamilly();
        if (Ssfamilly) {

            var sFamillyarray = ko.utils.arrayFilter(mappedSSearchData, function (item) {
                return item.SFamilly().includes(Ssfamilly);;
            });
            this.items(sFamillyarray);

        }
    },
};

viewModel.familly = function () {

    itemArray = viewModel.items();
    itemArray.forEach(function (element) {
        array.push(element.Familly())

    });
    return ko.utils.arrayGetDistinctValues(array).sort();
}

viewModel.filteredItems = ko.dependentObservable(function () {
    var filter = this.filter().toLowerCase();
    if (!filter) {
        return this.items();
    } else {
        return ko.utils.arrayFilter(this.items(), function (item) {
            return item.LabelImmo().toLowerCase().includes(filter);
            //ko.utils.stringStartsWith(item.label().toLowerCase(), filter);

        });
    }
}, viewModel);

viewModel.total = ko.computed(function () {
    var total = 0;
    for (var p = 0; p < this.items().length; ++p) {
        total += this.items()[p].Quantity();
    }
    return total;
}, viewModel)



//a JSON string that we got from the server that wasn't automatically converted to an object
var JSONdataFromServer = CountingProductFile;

//parse into an object
var dataFromServer = JSONdataFromServer;

//do some basic mapping (without mapping plugin)
var mappedData = ko.utils.arrayMap(JSONdataFromServer, function (item) {
    return new Item(item.LabelImmo, item.Familly, item.SFamilly, item.Quantity);
});


ko.extenders.range = function (target, intRange) {
    //create a writeable computed observable to intercept writes to our observable
    var result = ko.computed({
        read: target,  //always return the original observables value
        write: function (newValue) {
            var current = target(),
                newValueAsNum = isNaN(newValue) ? 0 : parseInt(+newValue, 10),
                valueToWrite = newValueAsNum;

            if (newValueAsNum < intRange.min) {
                valueToWrite = intRange.min;
            }

            if (newValueAsNum > intRange.max) {
                valueToWrite = intRange.max;
            }
            //only write if it changed
            if (valueToWrite !== current) {
                target(valueToWrite);
            } else {
                //if the tested value is the same, but a different value was written, force a notification for the current field
                if (newValue !== current) {
                    target.notifySubscribers(valueToWrite);
                }
            }
        }
    }).extend({ notify: 'always' });

    //initialize with current value to make sure it is rounded appropriately
    result(target());

    //return the new computed observable
    return result;
};

viewModel.items(mappedData);

ko.applyBindings(viewModel);




