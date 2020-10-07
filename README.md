# WooliesX Code Test

# **Introduction**
<hr/>

This project provides API functionality for Excercise 1 and 2

# **Getting Started**
<hr/>

The application is written in [ASP.NET Core](https://github.com/aspnet/Home) 3.1 using Visual Studio 2019.
Unit Test cases are written in XUnit. 
Webapi hosted on azure as appservice at https://wooliesx-techchallenge-api.azurewebsites.net/index.html


# **Access to GIT Hub Repository**
<hr/>

Code is checked in github repository, please suggest incase access required to github repo.

To be able to build the application locally:

* You use VS2017 or higher to run the code locally. 


# **To Build And Run The Code**
<hr/>
1. Clone the repository or download unzip provided zip folder .
2. Open the solution in Visual Studio 2019.
3. Run all unit test cases and all tests should paas
6. Hit Ctrl+F5 to run the web api application.
7. Swagger is included in the web api
8. Check the output by running the console application

Project Design:
* Followed CQRS design
* Followed SOLID principle to make components loosely coupled.
* Inbuilt dependency injection

Project Structure
* VS Solution contains folders - src and deploy and docs
* src folder contains source code containing 2 projects - webapi and test project. 
* XUnit Test Project is used for testing controller and repo/services.
* Moq is used for mocking
* web api project contains 3 main part - repository, services and Commands.
* test folder contains test cases to  test only controller services. Additional time required to cover all the testing/ 

Technical debts:
1. Excercise 3 pending. if time permit I will give a go tomorrow.
2. More unit test needs to be added. Unit test cases can be improved using fluent assertions.
2. Automapper easy to use for mapping.
3. Detailed logging can be implemented.
4. Fluent validation can be implemeted for view model validations by separating models and viewmodels.
5. web application or console app to be used to display data


