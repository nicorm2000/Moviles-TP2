# Moviles-TP2
## Patrones de diseño:

#### COMMAND:
Dentro de la carpeta Scripts > Command Design Pattern. Este patron lo uso para poder dar comandos de ejecucion
cuando se necesita una accion. La idea es que todos estos comandos hereden de una interfaz que tiene la accion
en commun y que luego se llamen a estos comandos cuando es necesario. Se utiliza por ejemplo para el manejo 
entre escenas.

#### STRATEGY:
Dentro de la carpeta Scripts > Strategy Design Pattern. Este patron lo uso para poder elegir la direccion en
la que ira el enemigo de la partida, en base a las dos clases, ClockWise y CounterClockwise puedo delimitar
su movimiento de una forma mas sencilla y eficaz.

#### POOL:
Dentro de la carpeta Scripts > Pool Design Pattern. Este patron lo uso para limitar la cantidad de entidades
vivas en pantalla, las cuales son las monedas. A su vez me permite solamente instanciar una vez los objetos en
vez de cada vez que los necesito.

#### FLYWEIGHT:
Dentro de la carpeta Scripts > FlyWeight Design Pattern. Este patron lo uso para el manejo de la data 
de las skins del player, esto lo hago a traves de un Scriptable Object llamado PlayerData.
