var scanViewModel = {

    scans: ko.observableArray([

        { Id: ko.observable() },
        { Matricule: ko.observable() },
        { Libellé: ko.observable() },
        { Quantité: ko.observable() },
        { importID: ko.observable() },
        
    ]),
    
  
}
var data = DetailData;
scanViewModel.scans = data;
ko.applyBindings(scanViewModel);