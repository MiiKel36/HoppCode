# HoppCode - Aplicativo Educacional de Programação

**HoppCode** é um aplicativo móvel desenvolvido para ensinar programação utilizando a linguagem C#. O aplicativo foi criado como parte do Trabalho de Conclusão de Curso (TCC) do curso de Desenvolvimento de Sistemas da ETEC Polivalente de Americana, realizado no ano de 2023. O principal objetivo do HoppCode é proporcionar um aprendizado interativo e prático de programação, ajudando os usuários a desenvolver suas habilidades de codificação de forma eficaz e divertida.

## Descrição do Aplicativo

O HoppCode é um aplicativo que oferece uma estrutura de aprendizado em programação organizada em **classes**, **aulas** e **exercícios**. Cada aula ou exercício pertence a uma classe, e os usuários podem progredir de forma sequencial, adquirindo conhecimento e praticando por meio de exercícios práticos.

### Funcionalidades Principais

- **Estrutura de Aulas e Exercícios**: O aplicativo possui uma organização hierárquica onde os usuários começam aprendendo a teoria das aulas e, em seguida, praticam com exercícios. Cada exercício corresponde a um conteúdo abordado na aula.
  
- **Comparação de Output de Código**: Usamos uma API para comparar o resultado do código C# enviado pelo usuário com o output esperado dos exercícios. A API recebe o código enviado e retorna o seu resultado, permitindo que o usuário veja se o seu código está correto ou não.

- **Banco de Dados Firebase**: O HoppCode utiliza o **Firebase**, serviço de banco de dados do Google, para armazenar dados dos usuários, como progresso nas aulas, respostas aos exercícios, entre outros.

- **API Codex**: Inicialmente, o aplicativo usava a API **Codex**, que era responsável por executar o código C# enviado pelos usuários e comparar o resultado com a saída esperada dos exercícios. No entanto, devido à instabilidade da API, a equipe de desenvolvimento do HoppCode criou um clone próprio da API **Codex**, garantindo maior segurança e estabilidade no processo de execução e comparação de código.

## Tecnologias Utilizadas

O aplicativo HoppCode foi desenvolvido utilizando as seguintes tecnologias:

- **C#**: Linguagem de programação utilizada para o desenvolvimento do aplicativo.
- **.NET MAUI**: Framework utilizado para criar aplicativos móveis multiplataforma, permitindo que o HoppCode seja executado tanto em dispositivos Android quanto iOS.
- **Firebase**: Serviço de banco de dados em tempo real da Google, utilizado para armazenar dados dos usuários.
- **API Codex**: API utilizada para comparar o output do código enviado pelos usuários com o output esperado dos exercícios. A equipe do HoppCode criou um clone da API original para garantir maior estabilidade e segurança.

## Conquista

O projeto **HoppCode** foi avaliado pelos professores da ETEC Polivalente de Americana e obteve **segundo lugar** na avaliação da sala, sendo reconhecido como um projeto inovador e bem executado.

## Como Rodar o Projeto

Para rodar o aplicativo em seu ambiente local, siga os seguintes passos:

1. **Clone o repositório**:
   ```bash
   git clone https://github.com/MiiKel36/HoppCode.git
   ```

2. **Instale as dependências**:
   Certifique-se de ter o .NET MAUI instalado em seu ambiente de desenvolvimento. Você pode obter instruções completas de instalação [aqui](https://docs.microsoft.com/pt-br/dotnet/maui/).

3. **Abra o projeto no Visual Studio** e selecione a plataforma de sua preferência (Android ou iOS).

4. **Execute o aplicativo** no emulador ou em um dispositivo real.

## Contribuições

Se você deseja contribuir com este projeto, sinta-se à vontade para enviar um pull request ou abrir uma issue com sugestões de melhorias.

## Licença

Este projeto está licenciado sob a **Licença GPL-3.0**. Consulte o arquivo [LICENSE](LICENSE) para mais detalhes.
