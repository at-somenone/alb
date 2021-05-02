# лабораторная 7

1. Что такое объект COM?  
   COM (Component Object Model) — это технологический стандарт от компании Microsoft, предназначенный для создания программного обеспечения на основе взаимодействующих компонентов объекта, каждый из которых может использоваться во многих программах одновременно. Стандарт воплощает в себе идеи полиморфизма и инкапсуляции объектно-ориентированного программирования. Стандарт COM мог бы быть универсальным и платформо-независимым, но закрепился в основном на операционных системах семейства Microsoft Windows. В современных версиях Windows COM используется очень широко.

2. Чем отличаются СОМ объекты от обычных объектов?  
   COM объекты содержат несколько интерфейсов, которые обеспечивают доступ к его свойствам и методам.

3. Объясните основные принципы построения архитектуры приложения на основе com объектов?  
   COM - технологии, обеспечивающие взаимодействие между компонентами приложения и позволяющие развертывать распределенное приложение на платформе Windows. COM является моделью программирования на основе объектов, которая упрощает взаимодействие различных приложений и компонентов.


4. Расскажите о порядке работы с com-объектами?  
   Суть организации взаимодействий с COM-объектами: пользователь объявляет управляемое представление COM-объекта, а среда выполнения создает объект-обертку, реализующий маршалинг.


5. Опишите преимущества и недостатки архитектуры, основанной на COM объектах?  
   Преимущества COM:
   - СОМ обеспечивает удобный способ фиксации услуг, предоставляемых разными фрагментами ПО 
   - Общий подход к созданию всех типов программных услуг в СОМ упрощает проблемы разработки.  
   - СОМ безразличен язык программирования, на котором пишутся СОМ-объекты и клиенты. 
   - СОМ обеспечивает эффективное управление изменением программ — замену текущей версии компонента на новую версию с дополнительными возможностями.
   Недостатки COM:
   - компоненты COM могут быть трудными для кодирования. 
   - компоненты COM могут оказаться трудными для развертывания.
