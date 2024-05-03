Feature: Test Flow
    # https://github.com/karatelabs/karate/issues/1191
    # https://github.com/karatelabs/karate?tab=readme-ov-file#karate-fork

    Background:
    * url 'http://localhost:1080/mockserver/verify'
    * header Content-Type = 'application/json'

    Scenario: e2e test flow
        # Authentication
        Given url 'http://localhost:1080'
        And path '/auth/login'
        And method POST
        Then status 200
        * def accessToken = karate.toMap(response.accessToken.value)

        # Get all admin compensations - No compensations
        Given url 'http://host.docker.internal:5273'
        And path '/api/compensations/admin/all'
        And method GET
        And header Content-Type = 'accept: text/plain'
        # And header Authorization = 'Bearer ' + accessToken
        Then status 200
        And match response == []

        # # Get all personal compensations - No compensations
        # And request {"httpRequest":{"method":"GET","path":"salary/compensations/all"}}
        # And header Authorization = 'Bearer ' + accessToken
        # When method GET
        # Then status 200
        # And match response == []

        # # Get types
        # And request {"httpRequest":{"method":"GET","path":"salary/compensations/types"}}
        # And header Authorization = 'Bearer ' + accessToken
        # When method GET
        # Then status 200
        # And match response contains [{"typeId":1,"label":"English"},{"typeId":2,"label":"German"},{"typeId":3,"label":"Swimming"},{"typeId":4,"label":"Water"},{"typeId":5,"label":"Coworking"},{"typeId":6,"label":"Massage"},{"typeId":7,"label":"Products"},{"typeId":8,"label":"Consumables"},{"typeId":9,"label":"Periphery"},{"typeId":10,"label":"Business trip"},{"typeId":11,"label":"Psychotherapy"},{"typeId":12,"label":"Other"}]

        # # Create compensation
        # And request {"httpRequest":{"method":"POST","path":"salary/compensations/create","body":{"compensations":[{"typeId":1,"comment":"string","amount":700,"isPaid":false}],"dateCompensation":"2023-12-01T00:00:00Z"}}}
        # And header Authorization = 'Bearer ' + accessToken
        # When method POST
        # Then status 200

        # # Get all personal compensations - Unpaid compensation exists
        # And request {"httpRequest":{"method":"GET","path":"salary/compensations/all"}}
        # And header Authorization = 'Bearer ' + accessToken
        # When method GET
        # Then status 200
        # And match response contains {"list":[{"id":10,"comment":"string","amount":700,"isPaid":false,"compensationType":"English","dateCreateCompensation":"2023-12-01T00:00:00Z","dateCompensation":"2023-12-01T00:00:00Z"}],"totalUnpaidAmount":700}

        # # Get all admin compensations - Unpaid compensation exists
        # And request {"httpRequest":{"method":"GET","path":"salary/compensations/admin/all","queryStringParameters":{"year":["2023"],"month":["12"]}}}
        # And header Authorization = 'Bearer ' + accessToken
        # When method GET
        # Then status 200
        # And match response contains {"totalAmount":700,"totalUnpaidAmount":700,"items":[{"employeeFullName":"Ivanov Ivan Ivanovich","employeeId":11,"dateCompensation":"2023-12-01T00:00:00Z","totalAmount":700,"unpaidAmount":700,"isPaid":false,"compensations":[{"id":10,"compensationType":"English","comment":"string","amount":700,"dateCreateCompensation":"2023-12-01T00:00:00Z"}]}]}

        # # Update compensation status
        # And request {"httpRequest":{"method":"PUT","path":"salary/compensations/admin/update","body":[10]}}
        # And header Authorization = 'Bearer ' + accessToken
        # When method PUT
        # Then status 200

        # # Get all personal compensations - Paid compensation exists
        # And request {"httpRequest":{"method":"GET","path":"salary/compensations/all"}}
        # And header Authorization = 'Bearer ' + accessToken
        # When method GET
        # Then status 200
        # And match response contains {"list":[{"id":10,"comment":"string","amount":700,"isPaid":true,"compensationType":"English","dateCreateCompensation":"2023-12-01T00:00:00Z","dateCompensation":"2023-12-01T00:00:00Z"}],"totalUnpaidAmount":0}

        # # Get all admin compensations - Paid compensation exists
        # And request {"httpRequest":{"method":"GET","path":"salary/compensations/admin/all","queryStringParameters":{"year":["2023"],"month":["12"]}}}
        # And header Authorization = 'Bearer ' + accessToken
        # When method GET
        # Then status 200
        # And match response contains {"totalAmount":700,"totalUnpaidAmount":0,"items":[{"employeeFullName":"Ivanov Ivan Ivanovich","employeeId":11,"dateCompensation":"2023-12-01T00:00:00Z","totalAmount":700,"unpaidAmount":0,"isPaid":true,"compensations":[{"id":10,"compensationType":"English","comment":"string","amount":700,"dateCreateCompensation":"2023-12-01T00:00:00Z"}]}]}
        
