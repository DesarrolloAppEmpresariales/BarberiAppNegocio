Feature: Barberia
Garantizar el correcto funcionamiento de los servicios para la entidad Barberia
en la api de Negocio


@ListaBarberias
Scenario: Obtener la lista de barberias con un token válido
	Given que la API está disponible
	And tengo un token válido
	When hago una solicitud GET a "/api/barberia/"
	Then el código de respuesta debe ser 200
	And la respuesta debe contener una lista de barberias