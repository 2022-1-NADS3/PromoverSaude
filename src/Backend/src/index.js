const express = require('express');
const cors = require('cors');
const { Pool } = require('pg');
//const bodyParser = require('body-parser');
require('dotenv').config();

const PORT = process.env.PORT || 3000;
 
const pool = new Pool({
   connectionString: process.env.POSTGRES_URL
})
 
const app = express();
 
app.use(express.json());
app.use(cors());
//app.use(bodyParser.json()) // for parsing application/json
//app.use(bodyParser.urlencoded({ extended: false }))
//app.use(express.urlencoded({ extended: true}));
 
app.get('/', (req, res) => {
    res.send("Teste de conexão com heroku!");
    console.log("Ola Mundo!")
});
 
app.get('/users', async (req,res) => {
   try {
       const { rows } = await pool.query('SELECT * FROM user_login')
       return res.status(200).send(rows)
   } catch(err) {
       return res.status(400).send(err)
   }
});

app.get('/exames/:user_id', async (req, res) => {
    const { user_id } = req.params
    try {
        const allTodos = await pool.query('SELECT * FROM todo_exarms WHERE user_id = ($1)', [user_id])
        return res.status(200).send(allTodos.rows)
    } catch(err) {
        return res.status(400).send(err)
    }
});
 
/*app.post('/session', async (req, res) => {
 
   const { email, password } = res.body
   //const { password } = res.body
   let user = ''
   try {
       user = await pool.query('SELECT * FROM users_login WHERE user_email = ($1)', [email])
       if(!user.rows[0]){
           user = await pool.query('INSERT INTO users_login(user_email,user_password) VALUES($1,$2) RETURNING *' ,[email,password])
       }
       return res.status(200).send(user.rows)
   }catch(err){
       return res.status(400).send(err)
   }
 
});*/
 
app.listen(PORT, () => console.log(`Server running in port ${PORT}`));
