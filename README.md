# OrderApp 🚀

O **OrderApp** é uma aplicação desenvolvida em **.NET 8** que utiliza **RabbitMQ**, **MassTransit**, **SignalR** e **Docker** para gerenciar e exibir pedidos em tempo real. O principal objetivo deste projeto é aprofundar os conhecimentos no uso do **RabbitMQ** como sistema de mensageria, além de aplicar conceitos de processamento assíncrono e atualização em tempo real.

## 📌 Funcionalidades
- Gerenciamento de pedidos utilizando banco de dados em memória
- Atualizações em tempo real via **SignalR**
- Processamento assíncrono de mensagens com **RabbitMQ** e **MassTransit**
- Ambiente totalmente conteinerizado com **Docker & Docker Compose**

## 🛠️ Tecnologias Utilizadas
- **.NET 8** (ASP.NET Core)
- **RabbitMQ** (com MassTransit)
- **SignalR** (para atualizações em tempo real)
- **Docker & Docker Compose**

---

## 🚀 Como Executar o Projeto Localmente

### 1️⃣ Clonar o Repositório
```sh
git clone https://github.com/phenriq-dev/OrderApp.git
cd OrderApp
```

### 2️⃣ Construir e Iniciar os Containers
```sh
docker-compose up --build
```
Isso irá iniciar:
- A aplicação **OrderApp**
- O serviço de mensageria **RabbitMQ**

### 3️⃣ Acessar a Aplicação
Após iniciar os containers, abra o navegador e acesse:
```
http://localhost:8080
```

---

## 📝 Como Funciona o RabbitMQ no Projeto
O **RabbitMQ** é um sistema de mensageria baseado no protocolo AMQP, utilizado para comunicação assíncrona entre serviços. No **OrderApp**, ele é responsável por gerenciar eventos de criação e atualização de pedidos.

### 🔹 Fluxo de Mensagens:
1. **Um novo pedido é criado** na aplicação.
2. **O pedido é publicado** em uma fila do **RabbitMQ**.
3. **O consumidor (MassTransit)** escuta essa fila e processa a mensagem.
4. **A atualização do pedido** é feita no banco de dados.
5. **O SignalR notifica** os clientes conectados para atualizar a interface.

### 🔹 Benefícios do RabbitMQ no Projeto:
- **Desacoplamento**: Permite que diferentes partes do sistema comuniquem-se sem depender diretamente umas das outras.
- **Escalabilidade**: Facilita a distribuição de carga e o processamento assíncrono de pedidos.
- **Resiliência**: Mensagens podem ser armazenadas na fila até serem processadas, evitando perda de dados.

---

## 📜 Configuração do Docker
Este projeto inclui um **Dockerfile** e um **docker-compose.yml**.

### **Dockerfile**
- Usa a imagem `mcr.microsoft.com/dotnet/aspnet:8.0` como base.
- Copia o código-fonte e publica a aplicação.

### **docker-compose.yml**
- Define o serviço `orderapp` para executar a aplicação.
- Adiciona o serviço `rabbitmq` para gerenciar mensagens.
- Exponde as portas `8080` (aplicação) e `5672` (RabbitMQ).

---

## 📧 Contato

- Email: hnriq.donha@gmail.com
- LinkedIn: https://www.linkedin.com/in/pedro-donha/
