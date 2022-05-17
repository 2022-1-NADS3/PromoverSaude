const express = require("express");

const app = express();

const port = process.env.PORT || 3000;

app.get("/", function (req, res){
    res.send("Teste de conexão com heroku!");
});

app.get("/user", function(req,res){
    console.log("Enviei po Get");
    res.send(JSON.stringify({
        nome:"Teo",
        sobrenome:"balão",
        idade:22,
        altura:1.75
    }));
});

app.listen(port, () => {
    console.info(`Aplicação rodando em http://localhost:${port}`)
});