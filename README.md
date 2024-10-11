
# Executando a aplicação #

 Podem executar normalmente a Solution no Visual Studio. 
 Deixei configurado no AppSettings da aplicação um local fixo para 
 o arquivo rotas.csv.
 Por isso, é importante ter esse path na máquina C:\file\rotas.csv

Ou, podem alterar o caminho no appSettings para onde estiver o csv


# Sobre o projeto #

É um projeto bem pequeno. Por isso fiz apenas uma divisão simples entre
o projeto Console, uma API e um projetinho de Domain só para separar as coisas

Não deu para explorar alguns conceitos do Solid ou criar um projeto de Infra, 
com a parte de context, repositories, EF.. etc.
Até pela simplicidade do arquivo CSV, a parte que faz a leitura do arquivo
está na Domain mesmo. Na vida real, poderia criar um projeto específico

# Projeto Console # 

Utilizei DI para acessar os Services pelo projeto do console.
Poderia ter feito com que o Projeto Console consumisse a API para obter/Gravar a Rota (e seria a melhor ideia no mundo real rs).
Mas como foquei mais no desenrolar da lógica, que era mais complicado, deixei o projeto console acessando via Interface o Service no proj Domain

# Tests #

Acredito que o uso de Unit Test nesse teste seja mais para validar algum conhecimento no tema.
Pela simplicidade da app, criei apenas 2 testes simples, utilizando o Moq, Bogus e outras bibliotecas que uso no dia-a-dia
É possível fazer coisas bem mais avançadas ali numa aplicação real. 

# Thanks # 

Obrigado pela oportunidade e atenção. Fico à disposição!

# end #
