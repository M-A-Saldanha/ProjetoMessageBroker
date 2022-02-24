<h1>Projeto Message Broker</h1>
##
<h2>Tecnologias</h2>
##
<br>
<div>
    <p>C#</p>
    <p>.NetCore 3.1</p>
    <p>RabbitMQ</p>
</div>
<br>
##
<h2>Resumo</h2>
<div>
    <p>Sistema de mensageria que envia uma mensagem digitada pelo usuário atráves do terminal usando o Publisher(EmitMessage) que é encaminhada para uma Exchange(direct) que envia para uma Queue(fila) e é recebida por um Consumer(ReceiveMessage).</p>
</div>
<br>
<div>
    <p>Para executar o Publisher digite no terminal</p>
    <p>cd EmitMessage</p>
    <p>dotnet run</p>
</div>
<br>
<div>
    <p>Para executar o Consumer,abra um novo terminal e digite</p>
    <p>cd ReceiveMessage</p>
    <p>dotnet run</p>
</div>