# RetroRecordsAPI

## Overview

This is an API that is designed to store data on music records. It has been built using C#, .Net, and ASP.NET Web API. The database that works with the API uses MS SQL Server and Entity Framework Core for CRUD operations. The project is configured to use Swagger for API documentation and testing.

## Cloning This Project
This project has been built with .NET version 7, so the .NET Software Development Kit will be required to run the project.

To clone this project, navigate to the folder of your choice via a terminal and then type the following command:

> git clone <span>https://</span>github.com/RoKen1000/RetroRecords.git

Then open the folder using Visual Studio. Run the project by clicking the launch button in the toolbar. Once launched the browser will then navigate to Swagger for testing.

## Packages and Frameworks Used
Besides the NuGet packages for using the project with Entity Framework Core and MS SQL Server, Newtonsoft Json and JsonPatch are also used to handle Json objects when making PUT and PATCH requests to update records. 

## Making PATCH requests
Because this project uses JsonPatch to handle update requests, the format of the Json that is sent to the controller must be like:
```
[
  {
    "path": "/Artist",      //property to update that corresponds to the RecordDTO
    "op": "replace",        //operation
    "value": "Supertramp"   //value to update with 
  }
]
```
If the property being updated is one that would involve data like a TimeSpan or DateTime structure, such as for RunTime and ReleaseDate respectively, the following needs to be put as the request's value:
<ul>
    <li>RunTimeArray - JavaScript array that corresponds to hours/minutes/seconds e.g. [1, 2, 55] = 1 hour, 2 minutes and 55 seconds. This is then converted into a TimeSpan object when passed from the Record Data Transfer Object to the Record class.</li>
    <li>ReleaseDateArray - JavaScript array that corresponds to year/month/day e.g. [1973, 10, 13] = 13/10/1973. This is then converted into a DateTime object when passed from the Record Data Transfer Object to the Record class.</li>
</ul>

An example:
```
[
  {
    "path": "/RunTimeArray",
    "op": "replace",
    "value": "[1, 5, 43]"    //1 hour, 5 minutes and 43 seconds
  }
]
```

## Challenges
I wanted to experiment with more complex data structures rather than just have primitive data types for each Record property. I decided to use TimeSpan and DateTime for some properties such as a Record's release date, the Record's run time and when the Record object was created or updated in the database. 

The Record Data Transfer Object has two properties that accept an integer array which when passed to the actual Record model are then converted into a TimeSpan or DateTime class. It was an interesting challenge working out how to convert these values to a DateTime or TimeSpan structure, especially when making PATCH/PUT requests where Json objects were involved. 

When making a PATCH/PUT request this is done with a Json object containing a JavaScript array, where it has to be converted into a C# integer array to then create TimeSpan or DateTime classes. This conversion is also needed so that when the patch.ApplyTo() method is run it doesn't produce an error in ModelState and causes the Record to not update. To get around this, conditionals are used to check first if the property that is being updated is a property that takes an integer array in RecordDTO, and then the value is convereted into a C# array by using the JsonConvert.DeserializeObject function.