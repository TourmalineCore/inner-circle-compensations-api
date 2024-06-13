Feature: Test Flow
# https://github.com/karatelabs/karate/issues/1191
# https://github.com/karatelabs/karate?tab=readme-ov-file#karate-fork

Background:
* url 'http://localhost:1080/mockServer/verify'
* header Content-Type = 'application/json'

Scenario: e2e test flow

    * def jsUtils = read('jsUtils.js')
    * def authApiRootUrl = jsUtils().getEnvVariable('AUTH_API_ROOT_URL')
    * def apiRootUrl = jsUtils().getEnvVariable('API_ROOT_URL')
    * def authLogin = jsUtils().getEnvVariable('AUTH_LOGIN')
    * def authPassword = jsUtils().getEnvVariable('AUTH_PASSWORD')

    # Authentication
    Given url authApiRootUrl
    And path '/auth/login'
    And request {"login": #(authLogin), "password": #(authPassword)}
    And method POST
    Then status 403

    # * def accessToken = karate.toMap(response.accessToken.value)

    # * configure headers = jsUtils().getAuthHeaders(accessToken)

    # # Get all personal compensations - No compensations
    # Given url apiRootUrl
    # And path '/api/compensations/all'
    # And method GET
    # Then status 200

    # # Get types
    # Given url apiRootUrl
    # And path '/api/compensations/types'
    # When method GET
    # Then status 200
    # # Check if there is at least one compensation type, massage is chosen as an example
    # And match response contains {"typeId":6,"label":"Massage"}

    # # Create compensation
    # Given url apiRootUrl
    # And path '/api/compensations/create'
    # And request {"compensations":[{"typeId":6,"comment":"my compensation","amount":700}],"compensationRequestedForYearAndMonth":"2023-12"}
    # When method POST
    # Then status 200
    # * def newId = response[0]

    # # Get all personal compensations - Unpaid compensation exists
    # Given url apiRootUrl
    # And path '/api/compensations/all'
    # When method GET
    # Then status 200
    # # Check if the new compensation is present in the response
    # And match response.list[*].id contains newId
    # # Check that the new compensation has isPaid = false
    # * def newCompensation = response.list.find(x => x.id == newId)
    # And newCompensation.isPaid == false

    # # Update compensation status
    # Given url apiRootUrl
    # And path '/api/compensations/mark-as-paid'
    # And request [#(newId)]
    # When method PUT
    # Then status 200

    # # Get all personal compensations - Paid compensation exists
    # Given url apiRootUrl
    # And path '/api/compensations/all'
    # When method GET
    # Then status 200
    # # Check that the new compensation has isPaid = true
    # And newCompensation.isPaid == true