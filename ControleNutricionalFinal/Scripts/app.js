﻿var url = 'http://localhost:64257/WCFNutricao.svc/';

var ControleNutricional = angular.module("ControleNutricional", ['ngRoute', 'ngResource', 'acute.select']).
    config(function ($routeProvider) {
        $routeProvider.
            when('/', { controller: IndexControl, templateUrl: '/pages/home.html' }).
            when('/cadastrar', { controller: CadastroAlimentoControl, templateUrl: '/pages/cadastrarAlimento.html' }).
            when('/cadastrarRefeicao', { controller: CadastroRefeicaoControl, templateUrl: '/pages/cadastrarRefeicao.html' }).
            when('/relatorio', { controller: RelatorioControl, templateUrl: '/pages/relatorio.html' }).


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
    var grupo = [];
    var lista;
    $scope.search = function () {
        $http.get(url + "findAllGrupo").success(function (data) {
            $scope.grupoArray = data;
            grupo = data;
            $scope.grupo = $scope.grupoArray[0];
        });
      };
    $scope.search();
    
    $scope.save = function () {
        for (i = 0; i < grupo.length; i++) {
            if (grupo[i].Id == $scope.grupo) {
                $scope.alimento.Grupo = grupo[i].Id;
                console.log(grupo);
                console.log("---------------");
            }
        }
        
        console.log($scope.alimento);
        console.log("---------------");
        $http.post(url + "createAlimento", $scope.alimento).success(function () {
            $location.path('/');
        });
       
    };
};

var CadastroRefeicaoControl = function ($scope, $location, $routeParams, $http) {
    var alimentos = [];
    var quantidades = [];
    var estados = [];
    $scope.allState = [];
    $scope.search = function () {
        $http.get(url + "findAllAlimento").success(function (data) {
            $scope.allStates = data;
            $scope.stateSelected = $scope.allStates[0].Nome;
        });
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
              $http.post(url + "createAlimentoRefeicao",alimentoRefeicao);
           }
           $location.path('/');
       });
       
     
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

var RelatorioControl = function ($scope, $location, $routeParams, $http) {
    var dataCriacao = { "dia": 18, "mes": 06, "ano": 2015 };
    $scope.date = '20140313T00:00:00';
    var refeicoes = [];

  
       $http.get(url + "listaAlimentosPorRefeicao" +"/" + dataCriacao.dia +"/" + dataCriacao.mes +"/" + dataCriacao.ano).success(function (data) {
                // como a lista de alimento refeicão vem em um json um pouco complicado 
                // separo apenas as refeições, tirando as repetidas(vide formato do json) 
                // e na view faço mais dois laços para exibir os alimentos por refeição, 
                var temp_ref = [];
                var temp_al = [];
                var copy = data;
                var alimento_refeicoes = [[]];
                for (i = 0; i < data.length; i++) {
                        var existe = false;
                    for (j = 0; j < temp_ref.length ; j++){
                        if (data[i].Refeicao.Id == temp_ref[j].Id) {
                            existe = true;
                        }
                    }
                    if (!existe) {
                        temp_ref.push(data[i].Refeicao)
                    }
                }
                console.log(temp_ref);

                $scope.refeicoes = temp_ref;
                $scope.alimentos = data;
                console.log(data);  
               
                                
            //
        });

       $http.get(url + "relatorioValorNutricionalTotalDiario").success(function (data) {
           var alimento = data[0];
           for (i = 1 ; i < data.length ; i++){
               alimento.Calcio_valorTotal += data[i].Calcio_valorTotal;
               alimento.Cho_valorTotal += data[i].Cho_valorTotal;
           }
           $scope.alimento_t = alimento;
       });
            

};