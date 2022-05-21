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

async function connect() {
    if (global.connection)
        return global.connection.connect();
 
    const { Pool } = require('pg');
    const pool = new Pool({
        connectionString: 'ec2-3-211-6-217.compute-1.amazonaws.com:5432/d1dsvje0jd710s'
    });
 
    //apenas testando a conexão
    const client = await pool.connect();
    console.log("Criou pool de conexões no PostgreSQL!");
 
    const res = await client.query('SELECT NOW()');
    console.log(res.rows[0]);
    client.release();
 
    //guardando para usar sempre o mesmo
    global.connection = pool;
    return pool.connect();
}

//index.js
const db = require("./db");

async function selectUsers() {
    const client = await connect();
    const res = await client.query('SELECT * FROM usuario');
    return res.rows;
}
 
module.exports = { selectUsers }

//index.js
(async () => {
    const db = require("./db");
    console.log('Começou!');

    console.log('SELECT * FROM usuario');
    const user = await db.selectUsers();
    console.log(user)
});

async function insertcaduser(caduser){
    const client = await connect();
    const sql = 'INSERT INTO clientes(nome,email,senha, sexo ) VALUES ($1,$2,$3, $4);';
    const values = [caduser.nome, caduser.email, caduser.senha, caduser.sexo];
    return await client.query(sql, values);
}

//index.js
(async () => {
    const db = require("./db");
    console.log('Começou!');
    
    console.log('INSERT INTO usuario');
    const result = await db.insertcaduser({nome: "Zé", email: "ze@teste.com", senha: "1234", sexo:"M"});
    console.log(result.rowCount);
 
    console.log('SELECT * FROM CLIENTES');
    const clientes = await db.selectUsers();
    console.log(clientes);
})();