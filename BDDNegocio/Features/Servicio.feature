Feature: Servicio
Garantizar el correcto funcionamiento de los servicios para la entidad Servicio
en la api de Negocio

@ListaBarberias
Scenario: Obtener la lista de servicio con un token valido
	Given que la API esta disponible para servicio
	And tengo un token valido para servicio
	When hago una solicitud GET a servicio "/api/servicio"
	Then el código de respuesta para servicio debe ser 200
	And la respuesta debe contener una lista de servicio