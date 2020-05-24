# listing
## Description 
The solution is to showcase how to develop an API in .netcore
This solution used ReactJS to consume the API and list the values returned by the API.

There are 3 projects in the solution
1. ApiStub - A project that mocks the actual endpoint that returns the list of pet carers and pets.
2. Listing - Project implemention the API endpoint that returns the filtered and required projection of the listing. 
3. ListingTest - Unit test project for the controller and service classes.

## How to Run the `Listing` project
This is a normal dotnet project, just run the command 
dotnet run from the Listing folder in command prompt

## Hot to Run the `Unit Tests` 
Before the unit tests can be run, please make sure the ApiStub application is running.
Run the ApiStub by simply executing the command "dotnet run".
Then the unit tests will run successfully.

## Logging
The solution uses `Serilog` for logging. Currently there are 2 types of sinks configures 
1. Console 
2. File (JSON)

The path of the log file can be changed in AppSettings.json

The possible future extension to logging is to use Seq to create visualization for the generated logs.
