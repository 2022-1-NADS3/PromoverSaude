const express = require('express');
const cors = require('cors');
const { Pool } = require('pg');
//const bodyParser = require('body-parser');
require('dotenv').config();

const PORT = process.env.PORT || 3000;
 
const pool = new Pool({
   connectionString: 'postgres://mlprsemr:2NwJQuD7b6lfH66RvxxbiyocQSwGg0DN@kesavan.db.elephantsql.com/mlprsemr',
   ssl: {
       rejectUnauthorized: false
   }
});
 
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
/*app.get('/users', async (req,res) => {
   try {
       const { rows } = await pool.query('SELECT * FROM user_login')
       //await pool.query("INSERT INTO user_login(user_email, user_name, user_password, user_sex) VALUES ('re@gmail.com','Bie Jeans', '1234', 'Masculino');")
       return res.status(200).send(rows)
   } catch(err) {
       return res.status(400).send(err)
   }
});*/

app.get('/exames/:user_id', async (req, res) => {
    const { user_id } = req.params
    try {
        const allTodos = await pool.query('SELECT * FROM todo_exarms WHERE user_id = ($1)', [user_id])
        return res.status(200).send(allTodos.rows)
    } catch(err) {
        return res.status(400).send(err)
    }
});
 
app.post('/session', async (req, res) => {
 
   const { useremail, username, userpassword, usersex } = req.body
   //const { password } = res.body
   let user = ''
   try {
       user = await pool.query('SELECT * FROM user_login WHERE user_email = ($1)', [useremail])
       if(!user.rows[0]){
           user = await pool.query("INSERT INTO user_login(user_email, user_name, user_password, user_sex) VALUES($1, $2, $3, $4)", [useremail, username, userpassword, usersex])
       }
       return res.status(200).send(user.rows)
   }catch(err){
       return res.status(400).send(err)
   }
 
});
 
app.listen(PORT, () => console.log(`Server running in port ${PORT}`));
