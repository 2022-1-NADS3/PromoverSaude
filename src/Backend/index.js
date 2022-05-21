var express = require("express");
var app = express();
var port = process.env.PORT || 3000;
var hostname = "localhost";
const json ='[{"nome":"Vanessa","sobrenome":" Ruama","altura":1.69}]';
//var objeto = JSON.parse(json);
var bodyParser = require("body-parser");

const usuarios = [];

app.use(express.json());

app.listen(port);

//Recupera usuário
app.get("/user", function(req,res){
    console.log("Enviei o Get")
    var nome = req.query.nome;
    res.send(JSON.stringify(
        {
            nome:"Vans",
            email:"vans@vans.com",
            senha:"1234",
            sexo:"F"
        }
    ))
});

//Adiciona usuário
app.post("/user", function(req,res){
    const { nome, email, senha, sexo } = req.body;

    const usuario = {
        nome,
        email,
        senha,
        sexo
    };
    
    usuarios.push(usuario);
    
    return res.json(usuario);
});