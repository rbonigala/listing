# listing
## Description 
> The solution is to showcase how to develop an API in .netcore
This solution used ReactJS to consume the API and list the values returned by the API.

>There are 3 projects in the solution
1. ApiStub - A project that mocks the actual endpoint that returns the list of pet carers and pets.
2. Listing - Project implemention the API endpoint that returns the filtered and required projection of the listing. 
3. ListingTest - Unit test project for the controller and service classes.

## How to Run the `Listing` project
> This is a normal dotnet project, just run the command 
dotnet run from the Listing folder in command prompt

## Hot to Run the `Unit Tests` 
> Before the unit tests can be run, please make sure the ApiStub application is running.
Run the ApiStub by simply executing the command "dotnet run".
Then the unit tests will run successfully.
