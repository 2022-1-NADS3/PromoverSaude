const express = require('express');
const cors = require('cors');
const { Pool } = require('pg');
require('dotenv').config();

const PORT = process.env.PORT || 3000;
 
const pool = new Pool({
   connectionString: process.env.POSTGRES_URL,
   ssl: {
       rejectUnauthorized: false
   }
});
 
const app = express();
 
app.use(express.json());
app.use(cors());
 
app.get('/', (req, res) => {
    res.send({message: 'Server started at port ' + PORT});
    console.log('Server started at port ' + PORT)
});

/*-------------------------------------------------------
  Api para retornar todos os usuários - /todos_usuarios
-------------------------------------------------------*/
app.get('/todos_usuarios', async (req,res) => {
    try {
        const { rows } = await pool.query('SELECT * FROM user_login')
        return res.status(200).send(rows)
    } catch(err) {
        return res.status(400).send(err)
    }
 });

/*-------------------------------------------------------
  Api para retornar todos os exames do usuário - 
  GET | /meus_exames/:user_id
-------------------------------------------------------*/
app.get('/meus_exames/:user_id', async (req, res) => {
    const { user_id } = req.params
    try {
        const allTodos = await pool.query('SELECT * FROM todo_exarms WHERE user_id = ($1)', [user_id])
        return res.status(200).send(allTodos.rows)
    } catch(err) {
        return res.status(400).send(err)
    }
});

/*-------------------------------------------------------
  Api para validar o login do usuário - 
  POST | /login_validacao
-------------------------------------------------------*/
app.post('/login_validacao', async (req, res) => {
 
   const { useremail, userpassword } = req.body
   console.log(useremail,userpassword)
try {
    const allTodos = await pool.query('SELECT * FROM user_login WHERE user_email = ($1) AND user_password = ($2)', [useremail, userpassword])
    return res.status(200).send(allTodos.rows)
    } catch(err) {
        return res.status(400).send(err)
    }
});

/*-------------------------------------------------------
  Api para cadastrar o usuário - 
  POST | /cadastrar_usuarios
-------------------------------------------------------*/
app.post('/cadastrar_usuarios', async (req, res) => {
 
    const { useremail, username, userpassword, usersex } = req.body

    let user = ''
    try {
        user = await pool.query('SELECT * FROM user_login WHERE user_email = ($1)', [useremail])
        if(!user.rows[0]){
            user = await pool.query("INSERT INTO user_login(user_email, user_name, user_password, user_sex) VALUES($1, $2, $3, $4) RETURNING *", [useremail, username, userpassword, usersex])
        }
        return res.status(200).send(user.rows)
    }catch(err){
        return res.status(400).send(err)
    }
  
});

/*-------------------------------------------------------
  Api para cadastrar os exames - 
  POST | /cadastrar_exames/:user_id
-------------------------------------------------------*/
app.post('/cadastrar_exames/:user_id', async (req, res) => {
    const { title, description, dateExams } = req.body
    const { user_id } = req.params
    try {
        const newExams = await pool.query("INSERT INTO todo_exarms (todo_description, todo_title, todo_done, todo_date, user_id) VALUES ($1, $2, $3 ,$4 , $5) RETURNING *", [description, title,false, dateExams, user_id])
        return res.status(200).send(newExams.rows)
    } catch(err) {
        return res.status(400).send(err)
    }
})

/*-------------------------------------------------------
  Api para alterar os exames - 
  PATCH | /meus_exames/:user_id/:todo_id
-------------------------------------------------------*/
app.patch('/meus_exames/:user_id/:todo_id', async (req, res) => {
    const { todo_id, user_id } = req.params
    const data = req.body    
    try {         
        const validarExame = await pool.query('SELECT * FROM todo_exarms WHERE user_id = ($1) AND todo_id = ($2)', [user_id, todo_id])
        if (!validarExame.rows[0]) return res.status(400).send('Operation not allowed')        
        const AtualizarExame = await pool.query('UPDATE todo_exarms SET todo_description = ($1), todo_done = ($2), todo_date = ($3) WHERE todo_id = ($4) RETURNING *',
        [data.description, data.done, data.dateExams, todo_id])
        return res.status(200).send(AtualizarExame.rows)
    } catch(err) {
        return res.status(400).send(err)
    }
})

/*-------------------------------------------------------
  Api para alterar os usuários - 
  PATCH | /alterar_usuario
-------------------------------------------------------*/
app.patch('/alterar_usuario/:user_id', async (req, res) => {
    const { user_id } = req.params
    const data = req.body    
    try {         
        const validarUsuario = await pool.query('SELECT * FROM user_login WHERE user_id = ($1)', [user_id])
        console.log(validarUsuario)
        if (!validarUsuario.rows[0]) return res.status(400).send('Operation not allowed')        
        const AtualizarUsuario = await pool.query('UPDATE user_login SET user_email = ($1), user_password = ($2) WHERE user_id = ($3) RETURNING *',
        [data.email, data.password, user_id])
        return res.status(200).send(AtualizarUsuario.rows)
    } catch(err) {
        return res.status(400).send(err)
    }
})

/*-------------------------------------------------------
  Api para deletar os exames - 
  DELETE | /meus_exames/:user_id/:todo_id
-------------------------------------------------------*/
app.delete('/meus_exames/:user_id/:todo_id', async (req, res) => {
    const { user_id, todo_id } = req.params
    try {
        const validarExame = await pool.query('SELECT * FROM todo_exarms WHERE user_id = ($1) AND todo_id = ($2)', [user_id, todo_id])
        if (!validarExame.rows[0]) return res.status(400).send('Operation not allowed')
        const deletarExame = await pool.query('DELETE FROM todo_exarms WHERE todo_id = ($1) RETURNING *', [todo_id])
        return res.status(200).send({
            message: 'Todo successfully deleted',
            deletarExame: deletarExame.rows
        })
    } catch(err) {
        return res.status(400).send(err)
    }
})


app.listen(PORT, () => console.log(`Server running in port ${PORT}`));
