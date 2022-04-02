# ReadingIsGood-DotNETApplication

Simple online books web api app

## Brief explanation of Design <br><br>
This application build on the Clean Architecture, hence its layers are loosely copuples and can be tested independendly. 
and later we can change the database e.g. EF/SQL to any other DB provider, and can take advantages of other benefits.
**Application is containerized and deployed on Docker Hub and consume using the Azure Services.**

## Concurrency check
Concurrency check has implemented while managing the Stock.


## TechStack <br><br>
1. .NET 5.0 Core
2. EntityFramework Core
3. ASP Identity
4. Docker Container
5. SQLlite
6. AutoMapper
7. Swagger
8. Custom DB logging

## Access Endpoints
Application is deployed on Azure on the following link and APIs can be tested on. <br> But you need to Register first and get a token to access other APIs endpoints :)
**You can access the Logging endpoint (without authentication) anytime and see what is happening behind the scenes**
https://readingisgood.azurewebsites.net/swagger/index.html

## Application Flow
1. User will Register using the https://readingisgood.azurewebsites.net/api/Customer/register <br>
2. User will login using the email and password and get the token on https://readingisgood.azurewebsites.net/api/Customer/login <br>
3. User will pass the token, Order details and create the Order on https://readingisgood.azurewebsites.net/api/Order/create <br>
4. User will pass the token and can see his/her Order listing https://readingisgood.azurewebsites.net/api/Order/getCustomerOrders
5. User will pass the token, and Order number and can see details of an Order https://readingisgood.azurewebsites.net/api/Order/getCustomerOrderDetail <br>
6. User will pass the token and can update any Product https://readingisgood.azurewebsites.net/api/Product/updateProduct <br>
7. User will get all the Products available https://readingisgood.azurewebsites.net/api/Product/getProducts <br>
8. User can see the Logs without authentication at https://readingisgood.azurewebsites.net/api/Log/getAll
### All Request/Request schemas can be accessible on: https://readingisgood.azurewebsites.net/swagger/index.html


