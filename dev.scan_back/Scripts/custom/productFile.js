

$(function () {
    ko.bindingHandlers.dataTablesForEach = {
        page: 0,
        init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            var options = ko.unwrap(valueAccessor());
            ko.unwrap(options.data);
            if (options.dataTableOptions.paging) {
                valueAccessor().data.subscribe(function (changes) {
                    var table = $(element).closest('table').DataTable();
                    ko.bindingHandlers.dataTablesForEach.page = table.page();
                    table.destroy();
                }, null, 'arrayChange');
            }
            var nodes = Array.prototype.slice.call(element.childNodes, 0);
            ko.utils.arrayForEach(nodes, function (node) {
                if (node && node.nodeType !== 1) {
                    node.parentNode.removeChild(node);
                }
            });
            return ko.bindingHandlers.foreach.init(element, valueAccessor, allBindingsAccessor, viewModel, bindingContext);
        },
        update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            var options = ko.unwrap(valueAccessor()),
                key = 'DataTablesForEach_Initialized';
            ko.unwrap(options.data);
            var table;
            if (!options.dataTableOptions.paging) {
                table = $(element).closest('table').DataTable();
                table.destroy();
            }
            ko.bindingHandlers.foreach.update(element, valueAccessor, allBindings, viewModel, bindingContext);
            table = $(element).closest('table').DataTable(options.dataTableOptions);
            if (options.dataTableOptions.paging) {
                if (table.page.info().pages - ko.bindingHandlers.dataTablesForEach.page == 0)
                    table.page(--ko.bindingHandlers.dataTablesForEach.page).draw(false);
                else
                    table.page(ko.bindingHandlers.dataTablesForEach.page).draw(false);
            }
            if (!ko.utils.domData.get(element, key) && (options.data || options.length))
                ko.utils.domData.set(element, key, true);
            return { controlsDescendantBindings: true };
        }
    }; 
    ko.bindingHandlers.eachProp = {
        transformObject: function (obj) {
            var properties = [];
            for (var key in obj) {
                if (obj.hasOwnProperty(key)) {
                    properties.push({ key: key, value: obj[key] });
                }
            }
            return ko.observableArray(properties);
        },
        init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            var value = ko.utils.unwrapObservable(valueAccessor()),
                properties = ko.bindingHandlers.eachProp.transformObject(value);

            ko.bindingHandlers['foreach'].init(element, properties, allBindingsAccessor, viewModel, bindingContext)
            return { controlsDescendantBindings: true };
        },
        update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            var value = ko.utils.unwrapObservable(valueAccessor()),
                properties = ko.bindingHandlers.eachProp.transformObject(value);

            ko.bindingHandlers['foreach'].update(element, properties, allBindingsAccessor, viewModel, bindingContext)
            return { controlsDescendantBindings: true };
        }
    };

    var JSONdataFromServer = Product;
    //parse into an object
    var dataFromServer = JSONdataFromServer;
    //$('#Mytable').DataTable({
    //    data: dataFromServer,
    //    columns: [

    //        { data: 'NumInvent' },
    //        { data: 'NumFiche'},
    //        { data: 'LabelImmo'},
    //        { data:'CodeProduit' },
    //        { data:'Localisation' },
    //        { data: 'Familly'},
    //        { data: 'SFamilly' },

    //    ],
    //    //responsive: true
    //    //columns: [
    //    //    { title: "N° Inventaire" },
    //    //    { title: "N° Fiche produit" },
    //    //    { title: "Libellé du bien" },
    //    //    { title: "Code produit" },
    //    //    { title: "Localisation" },
    //    //    { title: "Famille" },
    //    //    { title: "Sous-Famille" },
    //    //    { title: "Fiche produit" }
            
    //    //],
    //    //"processing": true,
    //    //"pagingType": "full_numbers"
    //});
    
   
    
    var dialog, form;
    dialog = $("#dialog-form").dialog({
        autoOpen: false,
        height: 400,
        width: 350,
        modal: true,
        buttons: {
            "Sauvegarder": function () { dialog.find("form").submit() } ,
            Annuler: function () {
                dialog.dialog("close");
            }
        },
        open: function () {
            
            url = "/Scan/GetProductCard?number=" + $(this).data('id')();    
            
            $(this).load(url);
        },
        close: function () {
            form[0].reset();

        }

    })
    //form = dialog.find("form").on("submit", function (event) {
    //    event.preventDefault();
    //    //addUser();
    //})
    
   
        function Item(RefImmo,
            LabelImmo,
            CLabelImmo,
            CodeProduit,
            Description,
            Matricule,
            MotifOut,
            Localisation,
            Familly,
            SFamilly,
            AcquiDate,
            ServiceDate,
            OutDate,
            PriceSes,
            Amort,
            Coeff,
            AccountNumber,
            NumInvent,
            NumFiche,
            ValBuy,
            Tva,
            Immobilisation
            , Amortissement) {

            this.RefImmo = ko.observable(RefImmo);
            this.LabelImmo = ko.observable(LabelImmo);
            this.CLabelImmo = ko.observable(CLabelImmo);
            this.CodeProduit = ko.observable(CodeProduit);
            this.Description = ko.observable(Description);
            this.Matricule = ko.observable(Matricule);
            this.MotifOut = ko.observable(MotifOut);
            this.Localisation = ko.observable(Localisation);
            this.Familly = ko.observable(Familly);
            this.SFamilly = ko.observable(SFamilly);
            this.AcquiDate = ko.observable(AcquiDate);
            this.ServiceDate = ko.observable(ServiceDate);
            this.OutDate = ko.observable(OutDate);
            this.PriceSes = ko.observable(PriceSes);
            this.Amort = ko.observable(Amort);
            this.Coeff = ko.observable(Coeff);
            this.AccountNumber = ko.observable(AccountNumber);
            this.NumInvent = ko.observable(NumInvent);
            this.NumFiche = ko.observable(NumFiche);
            this.ValBuy = ko.observable(ValBuy);
            this.Tva = ko.observable(Tva);
            this.Immobilisation = ko.observableArray(['Immobilisation', 'Amortissement', 'Dotation']);
            
    }
var array = []
    var itemArray = []
    var mappedSSearchData = []
    var arrayLocal=[]
    var viewModel = {
        items: ko.observableArray([]),
        observableA : ko.observableArray([]),
        filter: ko.observable(""),
        search: ko.observable(""),
        Sfamilly: ko.observableArray([]),
        Ssfamilly: ko.observable(""),
        Familly: ko.observable(""),
        local: ko.observable(""),
        searchFamilly:  function () {

            var familly = viewModel.Familly();
            var JSONdataFromServer = Product;
            //parse into an object
            var dataFromServer = JSONdataFromServer;
            mappedSSearchData = ko.utils.arrayMap(dataFromServer, function (item) {
                return new Item(item.RefImmo,
                    item.LabelImmo,
                    item.CLabelImmo,
                    item.CodeProduit,
                    item.Description,
                    item.Matricule,
                    item.MotifOut,
                    item.Localisation,
                    item.Familly,
                    item.SFamilly,
                    item.AcquiDate,
                    item.ServiceDate,
                    item.OutDate,
                    item.PriceSes,
                    item.Amort,
                    item.Coeff,
                    item.AccountNumber,
                    item.NumInvent,
                    item.NumFiche,
                    item.ValBuy,
                    item.Tva,
                    item.Immobilisation,
                    item.Amortissement);
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
        addItem: function (item) {
            this.items.push(new Item("New", "", 1));
        },
        removeItem: function (item) {
            this.items.remove(item);
        },
        searchLocalisation: function (item) {

            var local = viewModel.local();
            var dataFromServer = JSONdataFromServer;
            mappedSSearchData = ko.utils.arrayMap(dataFromServer, function (item) {
                return new Item(item.RefImmo,
                    item.LabelImmo,
                    item.CLabelImmo,
                    item.CodeProduit,
                    item.Description,
                    item.Matricule,
                    item.MotifOut,
                    item.Localisation,
                    item.Familly,
                    item.SFamilly,
                    item.AcquiDate,
                    item.ServiceDate,
                    item.OutDate,
                    item.PriceSes,
                    item.Amort,
                    item.Coeff,
                    item.AccountNumber,
                    item.NumInvent,
                    item.NumFiche,
                    item.ValBuy,
                    item.Tva,
                    item.Immobilisation,
                    item.Amortissement);
            });
            if (local) {

                
                var localArray = ko.utils.arrayFilter(mappedSSearchData, function (item) {
                    return item.Localisation().includes(local);;
                });
                this.items(localArray);

            }
        }


    };

    
    viewModel.familly = function () {

        itemArray = viewModel.items();
        itemArray.forEach(function (element) {
            array.push(element.Familly())

        });
        return ko.utils.arrayGetDistinctValues(array).sort();
    }
    viewModel.localisation = function () {
        
        itemArray = viewModel.items();
        itemArray.forEach(function (element) {
            arrayLocal.push(element.Localisation())

        });
        return ko.utils.arrayGetDistinctValues(arrayLocal).sort();
    }

    

    viewModel.filteredItems = ko.computed(function () {
       openDialog =  function (Id) {

           dialog.data('id', Id).dialog("open");
            //this.items.push(new Item("New", "", 1));
        }
        
        
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
    function target(name, len) {
        this.name = ko.observable(name);
        this.len = ko.observable(len);
       this.result =  ko.computed(function () {

         return this.name() + '' + '(' + this.len()+')';

           


        },this)
    };
    viewModel.uniqueCategories = function () {


        var result = [];
       
        
        var propItem = {};
        var itemArray = viewModel.filteredItems();

        itemArray.forEach(function (element) {
            key = element.LabelImmo();
            
            if (key) { 
                propItem[key] = propItem[key] || [];
                propItem[key].push(key);
                
            }
            
            //arr.push(element.LabelImmo())

        });

        
        var data = Object.values(propItem);
          result = ko.utils.arrayMap(data, function (item) {
             return new target(item[0], item.length);
          });
        viewModel.observableA(result)
        return viewModel.observableA();/*ko.utils.arrayGetDistinctValues(arr).sort();*/
    }

  

    //$('#Mytable').DataTable({
    //    responsive: true
    //    //url: viewModel.filteredItems()
    //    //data: dataFromServer,
    //    //columns: [

    //    //    { data: 'NumInvent' },
    //    //    { data: 'NumFiche' },
    //    //    { data: 'LabelImmo' },
    //    //    { data: 'CodeProduit' },
    //    //    { data: 'Localisation' },
    //    //    { data: 'Familly' },
    //    //    { data: 'SFamilly' },

    //    //],
    //    //responsive: true
    //    //columns: [
    //    //    { title: "N° Inventaire" },
    //    //    { title: "N° Fiche produit" },
    //    //    { title: "Libellé du bien" },
    //    //    { title: "Code produit" },
    //    //    { title: "Localisation" },
    //    //    { title: "Famille" },
    //    //    { title: "Sous-Famille" },
    //    //    { title: "Fiche produit" }

    //    //],
    //    //"processing": true,
    //    //"pagingType": "full_numbers"
    //});
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
    var JSONdataFromServer = Product;
    //parse into an object
    var dataFromServer = JSONdataFromServer;
    var mappedData = ko.utils.arrayMap(dataFromServer, function (item) {
        return new Item(item.RefImmo,
            item.LabelImmo,
            item.CLabelImmo,
            item.CodeProduit,
            item.Description,
            item.Matricule,
            item.MotifOut,
            item.Localisation,
            item.Familly,
            item.SFamilly,
            item.AcquiDate,
            item.ServiceDate,
            item.OutDate,
            item.PriceSes,
            item.Amort,
            item.Coeff,
            item.AccountNumber,
            item.NumInvent,
            item.NumFiche,  
            item.ValBuy,
            item.Tva,
            item.Immobilisation,
            item.Amortissement);
    });


    viewModel.items(mappedData);
    ko.applyBindings(viewModel);

 
})