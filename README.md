# SpreeTail-Multivalue-Dictionary
This project deals with dictionary operations
We used singleton pattern to create the dictionary which is initialized during start of the application.
The dictionary will stay alive till the application stops running.


The solution has two startup projects 
1. SpreeTail.MultiValueDictionary.SelfHost
   This is hosted on IIS swagger is the UI for this
   The application runs on http://localhost:54244/ to route to swagger UI use http://localhost:54244/swagger/index.html
   It has two controllers HealthController, DictionaryController
   HealthController is to check if the application is up and running on a server.
   Dictionary controller has different endpoints which does CRUD operations on Dictionary.
   Launch the application by setting SelfHost as startup project in visual studio 2019 use the endpoints in Dictionary controller to perform operations on Dictionar
     
   
2. SpreeTail.MultiValueDictionary.SystemWeb
   This is a console app where the inputs are taken using command prompt.
   Set this project as a startup project in visual studio 2019. 
   Follow the instructions on the command prompt to do operations on dictionary.
   
   
