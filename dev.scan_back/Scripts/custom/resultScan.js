var resultScan = new function () {
    var self = this;
    self.result = ko.observableArray([

        { Id: ko.observable() },
        { importId: ko.observable() },
        { Matricule: ko.observable() },
        { Libellé: ko.observable() },
        { catégorie: ko.observable() },
        { Price: ko.observable() },
        { Stockreel: ko.observable() },
        { Stockthéorique: ko.observable() },
        { demarque: ko.observable() },
        { MtnDemarque: ko.observable() },

    ]),
        self.categories = new function () {

        var array = [];
        var itemArray = resultData;
            itemArray.forEach(function (element) {
                array.push(element.catégorie)

            });
            return ko.utils.arrayGetDistinctValues(array).sort();
        },
      self.selectCategorie = function() {
        var categorie = document.getElementById("categorie").value;
        if (categorie) {

            $.ajax({
                url: 'selectByCategorie',
                data: {
                    categorie: categorie

                },
                success: function (data) {
                    var cat = data;
                    if (cat.length > 0) {
                        self.result([])
                        self.result(cat);
                    }



                },
                error: function () {
                    alert('failure');
                }
            });
        } else {
            var data = resultData;
            self.result(data)

        }
       
    }

}
var data = resultData;
resultScan.result(data);
ko.applyBindings(resultScan);