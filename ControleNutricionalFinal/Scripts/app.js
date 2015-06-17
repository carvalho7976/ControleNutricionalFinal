var url = 'http://localhost:64257/WCFNutricao.svc/';

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

        $http.get(url + "findAllAlimento").success(function (data) {
            console.log(data);
        });      

    };

};
var CadastroAlimentoControl = function ($scope, $location, $routeParams, $http) {
   
    $scope.grupoArray = [];
    var lista;
    $scope.search = function () {
        $http.get(url + "findAllGrupo").success(function (data) {
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
        $http.post(url + "createAlimento", $scope.alimento).success(function () {
            $location.path('/');
        });
        //ControleNutricional.save($scope.alimento, function () {
        //   $location.path('/');
        //});
    };
};

var CadastroRefeicaoControl = function ($scope, $location, $routeParams, $http) {
    var alimentos = [];
    var quantidades = [];
   // $scope.allStates = [];
    var estados = [];
    $scope.allState = [];
    $scope.search = function () {
        $http.get(url + "findAllAlimento").success(function (data) {
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
            quantidades.push($scope.quantidade);
        }
    };
   $scope.saveRefeicao = function () {
       var alimento;
       var refeicao = [];
      
       $http.post(url + "createRefeicao", $scope.refeicao).success(function (data) {
           refeicao = data;
          
           for (i = 0; i < alimentos.length; i++) {
               var alimentoRefeicao = {
                   Alimento: alimentos[i],
                   Quantidade: quantidades[i],
                   Refeicao: refeicao
               };
               console.log(alimentoRefeicao);
              // $scope.alimentoRefeicao.alimento = alimentos[i];
               //$scope.alimentoRefeicao.quantidade = quantidades[i];
               $http.post(url + "createAlimentoRefeicao",alimentoRefeicao);
           }
           $location.path('/');
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
