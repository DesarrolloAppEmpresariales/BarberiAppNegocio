Feature: Cita
Garantizar el correcto funcionamiento de los servicios para la entidad Cita
en la api de Negocio
@CitaId
Scenario: Obtener una cita con un token válido y Id
	Given que la API está disponible para citas
	And tengo un token válido para citas
	When hago una solicitud GET a citas "/api/cita/1"
	Then el código de respuesta citas debe ser 200
	And la respuesta debe contener la fecha "4/28/2024 12:00:00 AM"
	And la respuesta debe contener el hora "14:50"
	And la respuesta debe contener el estado "ACTIVO"
	And la la respuesta debe contener el cliente_id 1
	And la la respuesta debe contener el barberia_id 2