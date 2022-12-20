# APIMiniProject

## Table of Contents

1. [Project Outline](https://github.com/TylerMullingsClarke/APIMiniProject#Project-Outline)
    - To-Be Added
    - To-Be Added
1. [Daily Report](https://github.com/TylerMullingsClarke/APIMiniProject#Daily-Report)
    - [Day 1](https://github.com/TylerMullingsClarke/APIMiniProject#Day-1)
    - [Day 2](https://github.com/TylerMullingsClarke/APIMiniProject#Day-2)
    - [Day 3](https://github.com/TylerMullingsClarke/APIMiniProject#Day-3)
    - [Day 4](https://github.com/TylerMullingsClarke/APIMiniProject#Day-4)
    - [Day 5](https://github.com/TylerMullingsClarke/APIMiniProject#Day-5)
1. [Other](https://github.com/TylerMullingsClarke/APIMiniProject#Other)


## Project Outline

### API usage:

    -GET /api/Customers
        returns a DTO of all customers in JSON Format

    -GET /api/Customers/{id}
        returns single customer DTO with matching ID in JSON format
    
    -POST /api/Customers
        adds customer with new unique id with the data in the body of the request.
        The following values are valid in the body of the request:
            CustomerId - new unqiue ID for the Customer - typed as a string at most 5 characters long
            ContactName - Name of the customer
            CompanyName - Name of the Company Customer works for
            ContactTitle  - Position of the Customer at the company they work for
            PostalCode - Postcode of the Customer
            Address - Address of the customer
            City - City the customer lives/works in
            Country - Country the Customer lives/works in
            Phone - Customers contact phone number 
            Fax - Customers Fax number
            Region - Region Customer lives/works in
    
    
    -PUT /api/Customers/{id}
        updates customer with DTO id matching id with the data in the body of the request, null values are kept the same.
        The following values are valid in the body of the request:
            CustomerId - new unqiue ID for the Customer - typed as a string at most 5 characters long
            ContactName - Name of the customer
            CompanyName - Name of the Company Customer works for
            ContactTitle  - Position of the Customer at the company they work for
            PostalCode - Postcode of the Customer
            Address - Address of the customer
            City - City the customer lives/works in
            Country - Country the Customer lives/works in
            Phone - Customers contact phone number 
            Fax - Customers Fax number
            Region - Region Customer lives/works in

    -DELETE /api/Customers/{id}
        removes Customer with matching id.

## Daily Report

### Day 1

-Bootstraped base project: including controller, classes, scaffolding ect
-constructed personas, user stories and definition of done

### Day 2

-Added in CRUD operations for customers to the CustomerController class  

### Day 3

### Day 4

### Day 5

## Other
