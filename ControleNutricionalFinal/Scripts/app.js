var urlGrupo = 'http://localhost:50916/SeviceGrupo.svc/'
var urlAlimento = 'http://localhost:50916/ServiceAlimento.svc/';
var urlRefeicao = 'http://localhost:50916/ServiceRefeicao.svc/';

var ControleNutricional = angular.module("ControleNutricional", ['ngRoute', 'ngResource', 'acute.select']).
    config(function ($routeProvider) {
        $routeProvider.
            when('/', { controller: IndexControl, templateUrl: '/pages/home.html' }).
            when('/cadastrar', { controller: CadastroAlimentoControl, templateUrl: '/pages/cadastrarAlimento.html' }).
            when('/cadastrarRefeicao', { controller: CadastroRefeicaoControl, templateUrl: '/pages/cadastrarRefeicao.html' }).

            otherwise({ redirectTo: '/' });
    });

//ControleNutricional.factory('ControleNutricional', function ($resource) {
//    return $resource('/api/alimento/:id', { Id: '@id' }, { update: { method: 'PUT' } });
    
//});
//ControleNutricional.factory('Grupo', function ($resource) {
//    return $resource('/api/grupo/:id', { Id: '@id' }, { update: { method: 'PUT' } });
//});
//ControleNutricional.factory('AlimentoRefeicao', function ($resource) {
//    return $resource('/api/alimentorefeicao/:id', { Id: '@id' }, { update: { method: 'PUT' } });
//});


var IndexControl = function ($scope, $location, $routeParams, $http) {
    $scope.message = "Fulano";
    $scope.listarTodos = function () {

        $http.get(urlAlimento + "findall").success(function (data) {
            console.log(data);
        });      

    };

};
var CadastroAlimentoControl = function ($scope, $location, $routeParams, $http) {
   
    $scope.grupoArray = [];
    var lista;
    $scope.search = function () {
        $http.get(urlGrupo + "findall").success(function (data) {
            $scope.grupoArray = data;
            $scope.grupo = $scope.grupoArray[0];
        });
        //Grupo.query({},
        //    function (data) {
        //        $scope.grupoArray = data;
        //        $scope.grupo = $scope.grupoArray[0];
        //    });
    };
    $scope.search();
    
    $scope.save = function () {
        $scope.alimento.Grupo = $scope.grupo.Id;

        console.log($scope.alimento);
        $http.post(urlAlimento + "create", $scope.alimento).success(function () {
            $location.path('/');
        });
        //ControleNutricional.save($scope.alimento, function () {
        //   $location.path('/');
        //});
    };
};

var CadastroRefeicaoControl = function ($scope, $location, $routeParams, $http) {
    var alimentos = [];
   // $scope.allStates = [];
    var estados = [];
    $scope.allState = [];
    $scope.search = function () {
        $http.get(urlAlimento + "findall").success(function (data) {
            $scope.allStates = data;
            $scope.stateSelected = $scope.allStates[0].Nome;
        });
        //ControleNutricional.query({},
        //    function (data) {
        //        console.log("adicionar estados");
        //        $scope.allStates = data;
        //        console.log($scope.allStates);
        //        console.log("--------------")
        //        //$scope.stateSelected = $scope.allStates[0].Nome;
        //    });
    };
       
   $scope.addItem = function () {
        var alimento = $scope.stateSelected.Nome;
        if (alimento != null && alimento != '') {
            var myEl = angular.element(document.querySelector('#divAlimentos'));
            myEl.append("<br/> " + alimento);
            alimentos.push($scope.stateSelected);
        }
    };
   $scope.saveRefeicao = function () {
       var alimento;
       
      // var refeicao = [{ Descricao: $scope.Descricao }];
      // var refeicao = [{Descricao: $scope.Descricao, Id: null, dataDeCriacao: null}];

       //recomeçar daki
       // obeto está vindo com campo descricao nulo
       $scope.refeicao.dataDeCriacao = Date();
       
           $http.post(urlRefeicao + "create", $scope.refeicao).success(function (data) {
               console.log($scope.refeicao);
               console.log(data);
           });
       
       //console.log(refeicao);
       var quantidade;
    };

    $scope.getAllStates = function (callback) {
        callback($scope.allStates);
    };
    
    $scope.stateSelected = function (state) {
        $scope.stateInfo = state.name + " (" + state.id + ")";
    };
    $scope.search();
       
   
};
