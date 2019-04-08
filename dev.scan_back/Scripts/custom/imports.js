var importViewModel = {
    
    import : ko.observableArray([

        { Id: ko.observable() },
        { ClientId: ko.observable() },
        { Date: ko.observable() },
        { Type: ko.observable() },
        { Assort: ko.observableArray() },
        { Scans: ko.observableArray() }

    ]),
 
    formatDate: function (textValue) {
        return ko.observable(moment(textValue).format('DD/MM/YYYY'));
       
    },
    showDetail: function (data, event) {
        $.post("Scan/Details",
            {
                Id: data.Id
       
            },
            function (data, status) {
                alert("Data: " + data + "\nStatus: " + status);
            })     
    },
    removeImport: function (data, event) {

        $.ajax({
            url: 'Scan/DeleteImport',
            data: {
                Id: data.Id

            },
            success: function (data) {
                document.location.reload(true);
            },
            error: function () {
                alert('failure');
            }
        });

        //$.post(
        //    "Scan/DeleteImport",
        //    {
        //        Id: data.Id

        //    },
        //    sucess: function(data, status){  
        //        importViewModel.import.slice(this, 1);
        //    })
    },
    url: ko.observable("Scan/Details"),
    
}
var data = importData;
importViewModel.import = data;
//importViewModel.id = data.Id;
//importViewModel.clientId = data.clientId;
//importViewModel.date = data.date;
//importViewModel.type = data.type;
//importViewModel.assort = data.assort;
////importViewModel.assort.distinct('Ean13');
//importViewModel.scan = data.scan;
////importViewModel.scan.distinct('Ean13');

ko.applyBindings(importViewModel);






