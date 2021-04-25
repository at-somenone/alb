#лабораторная 4

1. Опишите основные принципы архитектуры приложений, основанной на удалённом вызове процедур?  
   Идея вызова удалённых процедур состоит в расширении механизма передачи управления и данных внутри программы, выполняющейся на одном узле, на передачу управления и данных через сеть. Средства удалённого вызова процедур предназначены для облегчения организации распределённых вычислений и создания распределенных клиент-серверных информационных систем. Наибольшая эффективность использования RPC достигается в тех приложениях, в которых существует интерактивная связь между удалёнными компонентами с небольшим временем ответов и относительно малым количеством передаваемых данных. Такие приложения называются RPC-ориентированными.

2. Опишите механизм работы удалённой процедуры?  
   Клиент посылает на сервер название требуемой процедуры и её параметры и приостанавливает свою работу до получения ответа сервера.

3. Опишите структуру протокола XML-RPC.  
   Запросы XML-RPC состоят из заголовков HTTP и данных в формате XML. Данные состояд из тега methodCall, внутри которого содержатся теги methodName с именем процедуры, и params, содержащий список параметров. Ответ сервера имеет такую же структуру, только в место methodCall используется methodResponse, а methodName отсутствует.

4. Как реализуется механизм удалённого вызова процедур в платформе .NET?  
   Создаются программы клиента и сервера, сервер определяет методы, доступные для выполнения через RPC, клиент подключается к серверу и выполняет эти методы.