# GameChallenge-DotNETApplication

GameChallenge Application

## How to run the application
Just run the application, everything configured e.g. data-migration etc.

## Brief explanation of Design <br><br>
This application build on the Clean Architecture, hence its layers are loosely copuples and can be tested independendly. 
and later we can change the database e.g. EF/SQL to any other DB provider, and can take advantages of other benefits.
**Application is containerized and later can deployed on Docker Hub and consume using the Azure Services.**


## TechStack <br><br>
1. .NET 5.0 Core
2. EntityFramework Core
3. ASP Identity
4. Docker Container
5. SQLlite
6. AutoMapper
7. Swagger
8. Custom DB logging
9. xUnit and Moq Integration and Unit Testing

## Application Flow
1. For the sake of this project, Logging is open endpoint to see what is happening.
1. User will Register using the /api/Player/register and get the 10000 points<br>
2. User will login using the email and password and get the token on /api/Player/login <br>
3. User will pass the token, and can have the challenge /api/Player/challenge (With SwaggerUI, You can set the Bearer token and can access the challenge)
4. User will pass the token and can see his/her available points /api/Player/availablePoints  (With SwaggerUI, You can set the Bearer token and can access the challenge)

## Settings table, configure the gaming settings:
User can configure the following settings from the dbo.Settings table: Current settings are follows:<br>
Challenge.RandomNumberMin -> Random minimum number in a challenge<br>
Challenge.RandomNumberMax -> Random maximum number in a challenge<br>
Challenge.RewardHowManyTimes -> e.g. If he is right, he gets 9 times his stake as a prize<br>
User.DefaultPoints -> Default points for a new user.<br>
