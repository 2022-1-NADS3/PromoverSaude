const request = require("supertest");
const ApiUrl = "https://fecap-promoversaude.herokuapp.com";

/*------------------------------------------------
 Teste da api que retorna todos os usuários
------------------------------------------------*/
describe("GET /todos_usuarios", () => {
  it("Deveria retorna 200 OK", () => {
    return request(ApiUrl)
      .get("/todos_usuarios")
      .expect(200)
      .then(response => {
        expect(response.statusCode).toEqual(200);
      });
  });
});

/*------------------------------------------------
 Teste de cadastro de usuário e retorno do 
 usuário cadastrado
------------------------------------------------*/
describe("POST /cadastrar_usuarios", () => {
    it("Deveria retornar 200 e cadastrar o usuário corretamente", () => {
      return request(ApiUrl)
        .post("/cadastrar_usuarios")
        .send({
            "useremail":"smoothcri@mj.com", 
            "username":"smoothc", 
            "userpassword":"1234", 
            "usersex":"Masculino"
        })
        .expect(200)
        .then(() => {
          return request(ApiUrl)
          .get("/login_validacao")
          .query({"useremail":"smoothcri@mj.com", "userpassword":"1234"})
          .expect(200)
          .then(response => {
            expect(response.statusCode).toEqual(200);
          });
        });
    });
});