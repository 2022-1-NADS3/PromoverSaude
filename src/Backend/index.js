var express = require("express");
var app = express();
var port = process.env.PORT || 3000;
var hostname = "localhost";
const json ='[{"nome":"Vanessa","sobrenome":" Ruama","altura":1.69}]';
//var objeto = JSON.parse(json);
var bodyParser = require("body-parser");

app.use(bodyParser.json("application/json"));

app.get("/", function(req,res){
    res.send("Bem Vindo!");
});

app.listen(port, hostname, () => {
    console.log("Servidor http//"+hostname+":"+port);
});

app.get("/user", function(req,res){
    console.log("Enviei o Get")
    var nome = req.query.nome;
    res.send(JSON.stringify(
        {
            nome:"Vans",
            sobrenome:"Ruama",
            idade:25,
            altura:1.69
        }
    ));

    app.post("/add", function(req,res){
        console.log("Recebi um dado");
            console.log(req.body.nome);
            console.log(req.body.sobrenome);
            console.log(req.body.idade);
            console.log(req.body.altura);
            res.send("JSON Recebido!")
    })
});