function Item(Ean13, label, price, orderedQte, qteSales, stockReel) {
    this.Ean13 = ko.observable(Ean13);
    this.label = ko.observable(label);
    this.price = ko.observable(price);
    this.orderedQte = ko.observable(0);
    this.qteSales = ko.observable(0).extend({ range: 5000 });;
    this.stockReel = ko.observable(stockReel)
    this.M1 = ko.dependentObservable(function () {
        return ((parseInt(this.orderedQte()) + this.stockReel() * 1 - this.qteSales()));
    }, this);
    this.M2 = ko.dependentObservable(function () {
        return this.M1() - this.qteSales();
    }, this);
this.M3 = ko.dependentObservable(function () {
    return this.M2() - this.qteSales();
    }, this);
}
var viewModel = {
    categories: ["Bread", "Dairy", "Fruits", "Vegetables"],
    items: ko.observableArray([]),
    filter: ko.observable(""),
    search: ko.observable(""),
    addItem: function () {
        this.items.push(new Item("New", "", 1));
    },
    removeItem: function (item) {
        this.items.remove(item);
    }
};


viewModel.filteredItems = ko.dependentObservable(function () {
    var filter = this.filter().toLowerCase();
    if (!filter) {
        return this.items();
    } else {
        return ko.utils.arrayFilter(this.items(), function (item) {
            return item.label().toLowerCase().includes(filter);
            //ko.utils.stringStartsWith(item.label().toLowerCase(), filter);
            
        });
    }
}, viewModel);

//ko.utils.arrayForEach - return a total by adding all prices
viewModel.total = ko.dependentObservable(function () {
    var total = 0;
    ko.utils.arrayForEach(this.filteredItems(), function (item) {
        var value = parseFloat(item.M1());
        if (!isNaN(value)) {
            total += value;
        }
    });
    return total.toFixed(2);
}, viewModel);


//ko.utils.arrayFirst - identify the first matching item by name
viewModel.firstMatch = ko.dependentObservable(function () {
    var search = this.search().toLowerCase();
    if (!search) {
        return null;
    } else {
        return ko.utils.arrayFirst(this.filteredItems(), function (item) {
            return ko.utils.stringStartsWith(item.Ean13(), search);
        });
    }
}, viewModel);

//viewModel.justCategories = ko.dependentObservable(function () {
//    var categories = ko.utils.arrayMap(this.items(), function (item) {
//        return item.category();
//    });
//    return categories.sort();
//}, viewModel);

////ko.utils.arrayGetDistinctValues - get a unique list of used categories
//viewModel.uniqueCategories = ko.dependentObservable(function () {
//    return ko.utils.arrayGetDistinctValues(viewModel.justCategories()).sort();
//}, viewModel);

////ko.utils.compareArrays - find any unused categories
//viewModel.missingCategories = ko.dependentObservable(function () {
//    //find out the categories that are missing from uniqueNames
//    var differences = ko.utils.compareArrays(viewModel.categories, viewModel.uniqueCategories());
//    //return a flat list of differences
//    var results = [];
//    ko.utils.arrayForEach(differences, function (difference) {
//        if (difference.status === "deleted") {
//            results.push(difference.value);
//        }
//    });
//    return results;
//}, viewModel);

//ko.utils.arrayMap - prepare items to be sent back to server
//viewModel.mappedItems = ko.dependentObservable(function () {
//    var items = ko.toJS(this.items);
//    return ko.utils.arrayMap(items, function (item) {
//        delete item.priceWithTax;
//        return item;
//    });
//}, viewModel);

//a JSON string that we got from the server that wasn't automatically converted to an object
var JSONdataFromServer = Simulation;

//parse into an object
var dataFromServer = JSONdataFromServer;

//do some basic mapping (without mapping plugin)
var mappedData = ko.utils.arrayMap(JSONdataFromServer, function (item) {
    return new Item(item.Ean13, item.label, item.Price, item.OrderedQte, item.QteSales, item.StockReel);
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

//var viewModel = function () {

//    var self = this;
//    self.simulationViewMdel = ko.observableArray([{

//        Ean13: ko.observable(),
//        label: ko.observable(),
//        Price: ko.observable(),
//        OrderedQte: ko.observable(),
//        QteSales: ko.observable(),
//        StockReel: ko.observable(),
//        M1: ko.computed(function () {
//            return this.QteSales() - (this.OrderedQte() + this.StockReel());
//        }, self),
//        M2: ko.computed(function () {
//            return 2*this.QteSales() - (this.OrderedQte() + this.StockReel());
//        }, self),
//        M3: ko.computed(function () {
//            return 3* this.QteSales() - (this.OrderedQte() + this.StockReel());
//        }, self),
//        //    M1o: ko.pureComputed(function () {
//        //        return M1((StockReel() + OrderedQte()) - QteSales())
//        //     }, this),
//        //     M2o: ko.pureComputed(function () { return M2(M1o() - QteSales()) }, this),
//        //     M3o: ko.pureComputed(function () { return M3(M2o() - QteSales()) }, this)


//    }]);

//    //self.M1o = 

//}


//var data = ko.mapping.fromJS(Simulation);
//viewModel.simulationViewMdel = data;
////viewModel.simulationViewMdel().forEach(function (element) {
////    element.M1 = ko.computed(function () {
////        return (element.StockReel + element.OrderedQte) - element.QteSales;
////        element.M2 = ko.computed(function () { return element.M1 - element.QteSales });
////        element.M3 = ko.computed(function () { return element.M2 - element.QteSales })

////    });
//ko.applyBindings(viewModel)


